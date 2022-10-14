using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace System.App.Repositories.HCNS
{
    public class KhaiBaoVang_Repo
    {
        private string SearchQuery(Absent obj)
        {
            string fromDate = "", toDate = "", filter = "";

            if (!String.IsNullOrEmpty(obj.process))
            {
                if (!String.IsNullOrEmpty(obj.AbsentCode))
                    filter += " AND (ab.AbsentCode = '" + obj.AbsentCode + "')";

                if (obj.KhoaPhong != 0)
                    filter += " AND (u.PhongKhoa = " + obj.KhoaPhong + ")";

                if (!String.IsNullOrEmpty(obj.UserFullName))
                {
                    string filterBonus = "";
                    int returnRegex = 0;

                    bool regex = int.TryParse(obj.UserFullName, out returnRegex);

                    if (regex)
                    {
                        filterBonus = "OR ab.UserEnrollNumber = '" + obj.UserFullName + "'";
                    }
                    filter += " AND (UPPER(u.[UserFullName]) LIKE N'%" + obj.UserFullName.ToUpper() + "%' " + filterBonus + ")";
                }

                //Nếu để trống thông tin ngày, sẽ tự động lấy từ tháng trước
                if (String.IsNullOrEmpty(obj.fromDate) && String.IsNullOrEmpty(obj.toDate))
                {
                    if (!obj.IsAdmin)
                    {
                        fromDate = DateTime.UtcNow.AddHours(7).AddMonths(-2).ToString("yyyy-MM-24");
                        filter += " AND ab.TimeDate > Convert(datetime, '" + fromDate + "' )";
                    }
                    else
                    {
                        fromDate = DateTime.UtcNow.AddHours(7).ToString("yyyy-01-01");
                        filter += " AND ab.TimeDate > Convert(datetime, '" + fromDate + "' )";
                    }
                }
                //Nếu chỉ điền từ ngày, sẽ tự động lấy từ ngày đó trở về hiện tại
                else if (!String.IsNullOrEmpty(obj.fromDate) && String.IsNullOrEmpty(obj.toDate))
                {
                    if (!obj.IsAdmin)
                    {
                        fromDate = DateTime.ParseExact(obj.fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        filter += " AND ab.TimeDate > Convert(datetime, '" + fromDate + "' )";
                    }
                    else
                    {
                        fromDate = DateTime.UtcNow.AddHours(7).ToString("yyyy-01-01");
                        filter += " AND ab.TimeDate > Convert(datetime, '" + fromDate + "' )";
                    }
                }
                //Nếu chỉ điền đến ngày, sẽ tự động lấy từ tháng trước > ngày đã chọn
                else if (String.IsNullOrEmpty(obj.fromDate) && !String.IsNullOrEmpty(obj.toDate))
                {
                    if (!obj.IsAdmin)
                    {
                        fromDate = DateTime.UtcNow.AddHours(7).AddMonths(-2).ToString("yyyy-MM-24");
                        toDate = DateTime.ParseExact(obj.toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        filter += " AND (ab.TimeDate BETWEEN '" + fromDate + "' AND '" + toDate + "')";
                    }
                    else
                    {
                        fromDate = DateTime.UtcNow.AddHours(7).AddMonths(-2).ToString("yyyy-MM-24");
                        toDate = DateTime.ParseExact(obj.toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        filter += " AND ab.TimeDate < Convert(datetime, '" + toDate + "' )";
                    }
                }
                //Điền cả 2 ô từ ngày - đến ngày
                else
                {
                    fromDate = DateTime.ParseExact(obj.fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    toDate = DateTime.ParseExact(obj.toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                    filter += " AND (ab.TimeDate BETWEEN '" + fromDate + "' AND '" + toDate + "')";
                }
            }
            else
            {
                fromDate = DateTime.UtcNow.AddHours(7).AddMonths(-2).ToString("yyyy-MM-24");
                toDate = DateTime.UtcNow.AddHours(7).ToString("yyyy-MM-dd");
                filter += " AND ab.TimeDate > Convert(datetime, '" + fromDate + "' )";
                //filter += " AND (AddedTime BETWEEN '" + fromDate + "' AND '" + toDate + "')";
            }
            return filter;
        }
        public Dictionary<string, string> DanhSachKhaiBao(string connectionString)
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();

            string q = @"SELECT AbsentCode, AbsentDescription, AbsentSymbol FROM AbsentSymbol ORDER BY AbsentCode";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(q, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(reader["AbsentCode"].ToString(), reader["AbsentDescription"].ToString() + " (" + reader["AbsentSymbol"].ToString() + ")");
                        }
                    }
                }
            }
            return lst;
        }
        public IEnumerable<HCNS_NhanVien> SearchUsers(string connectionString, string prefix, string kp)
        {
            List<HCNS_NhanVien> lst = new List<HCNS_NhanVien>();

            string filter = "";
            if (!String.IsNullOrEmpty(kp))
            {
                if (kp != "-1")
                    filter = " AND PhongKhoa = " + kp;
            }

            string _query = @"select UserEnrollNumber, UserFullCode, TATitle, UserFullName from UserInfExt where (UserFullCode LIKE '%" + prefix + "%' OR UserFullName LIKE N'%" + prefix + "%')" + filter;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new HCNS_NhanVien()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                TATitle = reader["TATitle"].ToString(),
                                UserFullName = reader["UserFullName"].ToString()
                            });
                        }
                    }
                }
            }
            return lst;
        }
        public IEnumerable<HCNS_NhanVien> SearchUsersHC(string connectionString, string prefix, string kp)
        {
            List<HCNS_NhanVien> lst = new List<HCNS_NhanVien>();

            string filter = "";
            if (!String.IsNullOrEmpty(kp))
            {
                filter = " AND PhongKhoaHC = " + kp;
            }

            string _query = @"select UserEnrollNumber, UserFullCode, TATitle, UserFullName from UserInfExt where (UserFullCode LIKE '%" + prefix + "%' OR UserFullName LIKE N'%" + prefix + "%')" + filter;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new HCNS_NhanVien()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                TATitle = reader["TATitle"].ToString(),
                                UserFullName = reader["UserFullName"].ToString()
                            });
                        }
                    }
                }
            }
            return lst;
        }
        public IEnumerable<HCNS_NhanVien> SearchUsers_DieuDuong(string connectionString, string prefix, string kp)
        {
            List<HCNS_NhanVien> lst = new List<HCNS_NhanVien>();

            string filter = "";
            if (!String.IsNullOrEmpty(kp))
            {
                filter = " AND PhongKhoa IN (" + kp + ")";
            }
            //filter += " AND UType = 2";

            string _query = @"select UserEnrollNumber, UserFullCode, TATitle, UserFullName from UserInfExt where (UserFullCode LIKE '%" + prefix + "%' OR UserFullName LIKE N'%" + prefix + "%')" + filter;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new HCNS_NhanVien()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                TATitle = reader["TATitle"].ToString(),
                                UserFullName = reader["UserFullName"].ToString()
                            });
                        }
                    }
                }
            }
            return lst;
        }
        public IEnumerable<Absent> Ds_LichKhaiBao_All(string connectionString, Absent obj)
        {
            List<Absent> lst = new List<Absent>();
            string filter = "";

            //filter += " AND ab.UserAccount = " + obj.UserEnrollNumber;
            filter += SearchQuery(obj);

            string _query = @"SELECT TOP 1000 u.UserEnrollNumber, u.UserFullCode, u.UserFullName, u.PhongKhoaHC, rd.Description, TimeDate, [as].AbsentCode as AbsentCode, [as].AbsentDescription, WorkingDay, WorkingTime, AddedTime, Thang, Nam, Lydo, UserAccount
                        FROM Absent ab
	                        LEFT JOIN UserInfExt u ON u.UserEnrollNumber = ab.UserEnrollNumber
	                        LEFT JOIN AbsentSymbol [as] ON [as].AbsentCode = ab.AbsentCode
                            LEFT JOIN [RelationDept] rd ON [rd].ID = [u].PhongKhoa
                        WHERE (1 = 1) " + filter + " ORDER BY TimeDate desc";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var _timeDate = DateTime.Parse(reader["TimeDate"].ToString()).Date;
                            var _addedDate = DateTime.Parse(reader["AddedTime"].ToString()).Date;
                            //lấy số ngày
                            var _countDay = Convert.ToInt32((_timeDate - _addedDate).TotalHours) / 24;
                            var _warning = false;
                            var _lateDateDiff = String.Empty;

                            if (_countDay < 0) _warning = true;

                            if (Convert.ToInt32(_countDay) == 0)
                            {
                                _lateDateDiff = "Trong ngày";
                            }
                            else if (Convert.ToInt32(_countDay) > 0)
                            {
                                _lateDateDiff = "Trước " + Math.Abs(_countDay).ToString("00") + " ngày";
                            }
                            else
                            {
                                _lateDateDiff = "Sau " + Math.Abs(_countDay).ToString("00") + " ngày";
                            }

                            lst.Add(new Absent()
                            {
                                KhoaPhong = String.IsNullOrEmpty(reader["PhongKhoaHC"].ToString()) ? 0 : int.Parse(reader["PhongKhoaHC"].ToString()),
                                UserEnrollNumber = String.IsNullOrEmpty(reader["UserEnrollNumber"].ToString()) ? 0 : int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                Ten_KhoaPhong_ChamCong = reader["Description"].ToString(),
                                TimeDate = _timeDate.ToString("dd/MM/yyyy"),
                                AbsentCode = reader["AbsentCode"].ToString(),
                                AbsentDescription = reader["AbsentDescription"].ToString(),
                                Workingday = int.Parse(reader["Workingday"].ToString()),
                                WorkingTime = int.Parse(reader["WorkingTime"].ToString()),
                                AddedTime = _addedDate.ToString("dd/MM/yyyy"),
                                Thang = String.IsNullOrEmpty(reader["Thang"].ToString()) ? 0 : int.Parse(reader["Thang"].ToString()),
                                Nam = String.IsNullOrEmpty(reader["Nam"].ToString()) ? 0 : int.Parse(reader["Nam"].ToString()),
                                Lydo = reader["Lydo"].ToString(),
                                UserAccount = reader["UserAccount"].ToString(),
                                Warning = _warning,
                                DiffDate = _countDay,
                                LateDateDiff = _lateDateDiff
                            });
                        }
                    }
                }
            }

            return lst;
        }
        public IEnumerable<Absent> Ds_LichKhaiBao_Edit(string connectionString, Absent obj)
        {
            List<Absent> lst = new List<Absent>();
            string filter = "";

            //filter += " AND ab.UserAccount = '" + obj.UserAccount + "'";
            filter += " AND PhongKhoa IN (" + obj.UserInDepts + ")";
            filter += SearchQuery(obj);

            string _query = @"SELECT u.UserEnrollNumber, u.UserFullCode, u.UserFullName, u.PhongKhoaHC, TimeDate, [as].AbsentCode as AbsentCode, [as].AbsentDescription, WorkingDay, WorkingTime, AddedTime, Thang, Nam, Lydo, UserAccount
                        FROM Absent ab
	                        LEFT JOIN UserInfExt u ON u.UserEnrollNumber = ab.UserEnrollNumber
	                        LEFT JOIN AbsentSymbol [as] ON [as].AbsentCode = ab.AbsentCode
                        WHERE (1 = 1) " + filter + " ORDER BY TimeDate desc";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var _timeDate = DateTime.Parse(reader["TimeDate"].ToString()).Date;
                            var _addedDate = DateTime.Parse(reader["AddedTime"].ToString()).Date;
                            //lấy số ngày
                            var _countDay = Convert.ToInt32((_timeDate - _addedDate).TotalHours) / 24;
                            var _warning = false;

                            if (_countDay < 0) _warning = true;
                            var _lateDateDiff = String.Empty;
                            if (Convert.ToInt32(_countDay) == 0)
                            {
                                _lateDateDiff = "Trong ngày";
                            }
                            else if (Convert.ToInt32(_countDay) > 0)
                            {
                                _lateDateDiff = "Trước " + Math.Abs(_countDay).ToString("00") + " ngày";
                            }
                            else
                            {
                                _lateDateDiff = "Sau " + Math.Abs(_countDay).ToString("00") + " ngày";
                            }

                            lst.Add(new Absent()
                            {
                                //KhoaPhong = int.Parse(reader["PhongKhoaHC"].ToString()),
                                KhoaPhong = string.IsNullOrEmpty(reader["PhongKhoaHC"].ToString()) ? -1 : int.Parse(reader["PhongKhoaHC"].ToString()),
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                TimeDate = _timeDate.ToString("dd/MM/yyyy"),
                                AbsentCode = reader["AbsentCode"].ToString(),
                                AbsentDescription = reader["AbsentDescription"].ToString(),
                                Workingday = int.Parse(reader["Workingday"].ToString()),
                                WorkingTime = int.Parse(reader["WorkingTime"].ToString()),
                                AddedTime = _addedDate.ToString("dd/MM/yyyy"),
                                Thang = String.IsNullOrEmpty(reader["Thang"].ToString()) ? 0 : int.Parse(reader["Thang"].ToString()),
                                Nam = String.IsNullOrEmpty(reader["Nam"].ToString()) ? 0 : int.Parse(reader["Nam"].ToString()),
                                Lydo = reader["Lydo"].ToString(),
                                UserAccount = reader["UserAccount"].ToString(),
                                Warning = _warning,
                                DiffDate = _countDay,
                                LateDateDiff = _lateDateDiff
                            });
                        }
                    }
                }
            }

            return lst;
        }
        public IEnumerable<Absent> Ds_LichKhaiBao_View(string connectionString, Absent obj)
        {
            List<Absent> lst = new List<Absent>();
            string filter = "";

            filter += " AND ab.UserEnrollNumber = " + obj.UserEnrollNumber;
            filter += SearchQuery(obj);

            string _query = @"SELECT u.UserEnrollNumber, u.UserFullCode, u.UserFullName, u.PhongKhoaHC, TimeDate, [as].AbsentCode as AbsentCode, [as].AbsentDescription, WorkingDay, WorkingTime, AddedTime, Thang, Nam, Lydo, UserAccount
                        FROM Absent ab
	                        LEFT JOIN UserInfExt u ON u.UserEnrollNumber = ab.UserEnrollNumber
	                        LEFT JOIN AbsentSymbol [as] ON [as].AbsentCode = ab.AbsentCode
                        WHERE (1 = 1) " + filter + " ORDER BY TimeDate desc";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var _timeDate = DateTime.Parse(reader["TimeDate"].ToString()).Date;
                            var _addedDate = DateTime.Parse(reader["AddedTime"].ToString()).Date;
                            //lấy số ngày
                            var _countDay = Convert.ToInt32((_timeDate - _addedDate).TotalHours) / 24;
                            var _warning = false;

                            if (_countDay < 0) _warning = true;
                            var _lateDateDiff = String.Empty;
                            if (Convert.ToInt32(_countDay) == 0)
                            {
                                _lateDateDiff = "Trong ngày";
                            }
                            else if (Convert.ToInt32(_countDay) > 0)
                            {
                                _lateDateDiff = "Trước " + Math.Abs(_countDay).ToString("00") + " ngày";
                            }
                            else
                            {
                                _lateDateDiff = "Sau " + Math.Abs(_countDay).ToString("00") + " ngày";
                            }

                            lst.Add(new Absent()
                            {
                                KhoaPhong = int.Parse(reader["PhongKhoaHC"].ToString()),
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                TimeDate = _timeDate.ToString("dd/MM/yyyy"),
                                AbsentCode = reader["AbsentCode"].ToString(),
                                AbsentDescription = reader["AbsentDescription"].ToString(),
                                Workingday = int.Parse(reader["Workingday"].ToString()),
                                WorkingTime = int.Parse(reader["WorkingTime"].ToString()),
                                AddedTime = _addedDate.ToString("dd/MM/yyyy"),
                                Thang = String.IsNullOrEmpty(reader["Thang"].ToString()) ? 0 : int.Parse(reader["Thang"].ToString()),
                                Nam = String.IsNullOrEmpty(reader["Nam"].ToString()) ? 0 : int.Parse(reader["Nam"].ToString()),
                                Lydo = reader["Lydo"].ToString(),
                                UserAccount = reader["UserAccount"].ToString(),
                                Warning = _warning,
                                DiffDate = _countDay,
                                LateDateDiff = _lateDateDiff
                            });
                        }
                    }
                }
            }

            return lst;
        }
        public IEnumerable<Absent> Ds_LichKhaiBao_DieuDuong(string connectionString, Absent obj)
        {
            List<Absent> lst = new List<Absent>();
            string filter = "";

            //filter += " AND ab.UserEnrollNumber = " + obj.UserEnrollNumber;
            //filter += " AND u.UType = 2";
            filter += " AND PhongKhoa = 55";
            filter += SearchQuery(obj);

            string _query = @"SELECT u.UserEnrollNumber, u.UserFullCode, u.UserFullName, u.PhongKhoaHC, TimeDate, [as].AbsentCode as AbsentCode, [as].AbsentDescription, WorkingDay, WorkingTime, AddedTime, Thang, Nam, Lydo, UserAccount
                        FROM Absent ab
	                        LEFT JOIN UserInfExt u ON u.UserEnrollNumber = ab.UserEnrollNumber
	                        LEFT JOIN AbsentSymbol [as] ON [as].AbsentCode = ab.AbsentCode
                        WHERE (1 = 1) " + filter + " ORDER BY TimeDate desc";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var _timeDate = DateTime.Parse(reader["TimeDate"].ToString()).Date;
                            var _addedDate = DateTime.Parse(reader["AddedTime"].ToString()).Date;
                            //lấy số ngày
                            var _countDay = Convert.ToInt32((_timeDate - _addedDate).TotalHours) / 24;
                            var _warning = false;

                            if (_countDay < 0) _warning = true;
                            var _lateDateDiff = String.Empty;
                            if (Convert.ToInt32(_countDay) == 0)
                            {
                                _lateDateDiff = "Trong ngày";
                            }
                            else if (Convert.ToInt32(_countDay) > 0)
                            {
                                _lateDateDiff = "Trước " + Math.Abs(_countDay).ToString("00") + " ngày";
                            }
                            else
                            {
                                _lateDateDiff = "Sau " + Math.Abs(_countDay).ToString("00") + " ngày";
                            }
                            lst.Add(new Absent()
                            {
                                KhoaPhong = int.Parse(reader["PhongKhoaHC"].ToString()),
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                TimeDate = _timeDate.ToString("dd/MM/yyyy"),
                                AbsentCode = reader["AbsentCode"].ToString(),
                                AbsentDescription = reader["AbsentDescription"].ToString(),
                                Workingday = int.Parse(reader["Workingday"].ToString()),
                                WorkingTime = int.Parse(reader["WorkingTime"].ToString()),
                                AddedTime = _addedDate.ToString("dd/MM/yyyy"),
                                Thang = String.IsNullOrEmpty(reader["Thang"].ToString()) ? 0 : int.Parse(reader["Thang"].ToString()),
                                Nam = String.IsNullOrEmpty(reader["Nam"].ToString()) ? 0 : int.Parse(reader["Nam"].ToString()),
                                Lydo = reader["Lydo"].ToString(),
                                UserAccount = reader["UserAccount"].ToString(),
                                Warning = _warning,
                                DiffDate = _countDay,
                                LateDateDiff = _lateDateDiff
                            });
                        }
                    }
                }
            }

            return lst;
        }
        public Absent UserInfo(string connectionString, string user)
        {
            string _query = "SELECT UserEnrollNumber, UserFullCode, UserFullName FROM [UserInfExt] WHERE UPN = '" + user + "'";

            Absent obj = new Absent();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            obj.UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString());
                            obj.UserFullCode = reader["UserFullCode"].ToString();
                            obj.UserFullName = reader["UserFullName"].ToString();
                        }
                    }
                }
            }
            return obj;
        }
        public Absent EmployeeInfo(string connectionString, int UserEnrollNumber)
        {
            string _query = "SELECT UserEnrollNumber, UserFullCode, UserFullName FROM [UserInfExt] WHERE UserEnrollNumber = '" + UserEnrollNumber + "'";

            Absent obj = new Absent();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            obj.UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString());
                            obj.UserFullCode = reader["UserFullCode"].ToString();
                            obj.UserFullName = reader["UserFullName"].ToString();
                        }
                    }
                }
            }
            return obj;
        }
        public bool KiemTraKhaiBao(string connectionString, Absent obj, ref string error)
        {
            string mess;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("sp_KhaiBaoVang_KiemTra", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", obj.UserEnrollNumber);
                    sqlCommand.Parameters.AddWithValue("@AbsentCode", obj.AbsentCode);
                    sqlCommand.Parameters.AddWithValue("@DateString", obj.CheckDateString);
                    sqlCommand.Parameters.AddWithValue("@Role", obj.ForType);

                    mess = (string)sqlCommand.ExecuteScalar();
                }
            }
            error = mess;

            if (mess == "0")
                return true;
            return false;
        }
        public bool ThemMoiKhaiBao(string connectionString, List<Absent> objs)
        {
            var _rowAffected = 0;

            var _query = @"INSERT INTO Absent(UserEnrollNumber, TimeDate, AbsentCode, WorkingDay, WorkingTime, AddedTime, Thang, Nam, Lydo, UserAccount, ForType)
                VALUES (@UserEnrollNumber, @TimeDate, @AbsentCode, @WorkingDay, @WorkingTime, @AddedTime, @Thang, @Nam, @Lydo, @UserAccount, @ForType)";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                foreach (var obj in objs)
                {
                    using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", obj.UserEnrollNumber);
                        sqlCommand.Parameters.AddWithValue("@TimeDate", DateTime.ParseExact(obj.TimeDate, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                        sqlCommand.Parameters.AddWithValue("@AbsentCode", obj.AbsentCode);
                        sqlCommand.Parameters.AddWithValue("@WorkingDay", obj.Workingday);
                        sqlCommand.Parameters.AddWithValue("@WorkingTime", obj.WorkingTime);
                        sqlCommand.Parameters.AddWithValue("@AddedTime", DateTime.UtcNow.AddHours(7));
                        sqlCommand.Parameters.AddWithValue("@Thang", DateTime.ParseExact(obj.TimeDate, "dd/MM/yyyy", CultureInfo.CurrentCulture).Month);
                        sqlCommand.Parameters.AddWithValue("@Nam", DateTime.ParseExact(obj.TimeDate, "dd/MM/yyyy", CultureInfo.CurrentCulture).Year);
                        sqlCommand.Parameters.AddWithValue("@Lydo", string.IsNullOrEmpty(obj.Lydo) ? "" : obj.Lydo.ToString());
                        sqlCommand.Parameters.AddWithValue("@UserAccount", obj.UserAccount);
                        sqlCommand.Parameters.AddWithValue("@ForType", obj.ForType);

                        _rowAffected += sqlCommand.ExecuteNonQuery();
                    }
                }
            }

            if (objs.Count == _rowAffected)
                return true;
            return false;
        }

        public Absent AbsentInfo(string connectionString, Absent obj)
        {
            //string _query = "SELECT * FROM Absent WHERE UserEnrollNumber = " + obj.UserEnrollNumber + " AND convert(varchar, TimeDate, 103) = '" + obj.TimeDate + "'";
            string _query = @"SELECT a.UserEnrollNumber, b.UserFullCode, b.UserFullName, a.AbsentCode, a.Workingday, a.WorkingTime, a.Lydo, d.Reason as UserReason,c.AbsentDescription
                FROM Absent a
	                LEFT JOIN UserInfExt b ON a.UserEnrollNumber = b.UserEnrollNumber
	                LEFT JOIN AbsentSymbol c ON a.AbsentCode = c.AbsentCode
                    LEFT JOIN AbsentR d ON a.UserEnrollNumber = d.UserEnrollNumber AND d.Date = @TimeDate
                WHERE a.UserEnrollNumber = @UserEnrollNumber AND a.TimeDate = @TimeDate";

            //var info = EmployeeInfo(connectionString, obj.UserEnrollNumber);
            //obj.UserEnrollNumber = info.UserEnrollNumber;
            //obj.UserFullCode = info.UserFullCode;
            //obj.UserFullName = info.UserFullName;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    sqlCommand.Parameters.Add("@TimeDate", SqlDbType.Date).Value = DateTime.ParseExact(obj.TimeDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            obj.UserEnrollNumber = string.IsNullOrEmpty(reader["UserEnrollNumber"].ToString()) ? 0 : int.Parse(reader["UserEnrollNumber"].ToString());
                            obj.UserFullName = string.IsNullOrEmpty(reader["UserFullName"].ToString()) ? "" : reader["UserFullName"].ToString();
                            obj.UserFullCode = string.IsNullOrEmpty(reader["UserFullCode"].ToString()) ? "" : reader["UserFullCode"].ToString();
                            obj.AbsentCode = string.IsNullOrEmpty(reader["AbsentCode"].ToString()) ? "" : reader["AbsentCode"].ToString();
                            obj.AbsentDescription = string.IsNullOrEmpty(reader["AbsentDescription"].ToString()) ? "" : reader["AbsentDescription"].ToString();
                            obj.Workingday = string.IsNullOrEmpty(reader["Workingday"].ToString()) ? 0 : int.Parse(reader["Workingday"].ToString());
                            obj.WorkingTime = string.IsNullOrEmpty(reader["WorkingTime"].ToString()) ? 0 : int.Parse(reader["WorkingTime"].ToString());
                            obj.Lydo = string.IsNullOrEmpty(reader["Lydo"].ToString()) ? "" : reader["Lydo"].ToString();
                            obj.UserReason = string.IsNullOrEmpty(reader["UserReason"].ToString()) ? "" : reader["UserReason"].ToString();
                        }
                    }
                }
            }
            return obj;
        }
        public bool XoaKhaiBao(string connectionString, Absent obj)
        {
            var dateDelete = String.IsNullOrEmpty(obj.oldDate) ? obj.TimeDate : obj.oldDate;

            string _query = "DELETE Absent WHERE UserEnrollNumber = " + obj.UserEnrollNumber + " AND convert(varchar, TimeDate, 103) = '" + dateDelete + "'";
            int rowAffected = 0;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool KiemTraTrungLich(string connectionString, Absent obj)
        {
            string _query = @"select 1 from Absent a
	                    LEFT JOIN AbsentR b ON a.UserEnrollNumber = b.UserEnrollNumber AND a.TimeDate = b.Date
                    where (a.UserEnrollNumber = @UserEnrollNumber and TimeDate = @TimeDate) OR (b.UserEnrollNumber = @UserEnrollNumber AND Date = @TimeDate)";
            int rowAffected = 0;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    sqlCommand.Parameters.Add("@TimeDate", SqlDbType.Date).Value = DateTime.ParseExact(obj.TimeDate, "dd/MM/yyyy", CultureInfo.CurrentCulture);

                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        rowAffected = 1;
                    }
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
        public DataTable ExportExcel(string connectionString, string kp, string tu, string den)
        {
            string predicate = "";

            DataTable data = new DataTable();
            if (!String.IsNullOrEmpty(kp))
            {
                predicate += " AND (u.PhongKhoa = '" + kp + "')";
            }
            if (String.IsNullOrEmpty(tu) && String.IsNullOrEmpty(den))
            {
                predicate += " AND TimeDate > getdate()";
            }
            else if (String.IsNullOrEmpty(tu) && !String.IsNullOrEmpty(den))
            {
                var TO_DATE = DateTime.ParseExact(den, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                predicate += " AND (TimeDate >= getdate() and TimeDate <= '" + TO_DATE + "')";
            }
            else if (!String.IsNullOrEmpty(tu) && String.IsNullOrEmpty(den))
            {
                var FROM_DATE = DateTime.ParseExact(tu, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                predicate += " AND (TimeDate >= " + FROM_DATE + ")";
            }
            else if (!String.IsNullOrEmpty(tu) && !String.IsNullOrEmpty(den))
            {
                var TO_DATE = DateTime.ParseExact(den, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).ToString("yyyyMMdd");
                var FROM_DATE = DateTime.ParseExact(tu, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                predicate += " AND (TimeDate >= '" + FROM_DATE + "' and TimeDate <= '" + TO_DATE + "')";
            }

            string _select = @"
	            SELECT u.UserFullCode AS N'Mã nhân viên', u.UserFullName AS N'Tên nhân viên', CONVERT (varchar, ab.AddedTime, 103) AS N'Ngày khai báo', CONVERT (varchar, ab.TimeDate, 103) AS N'Ngày nghỉ', [as].AbsentDescription AS N'Nghỉ theo phép', Lydo AS N'Lý do nghỉ', UserAccount AS N'Người khai báo'
                FROM Absent ab
	                LEFT JOIN UserInfExt u ON u.UserEnrollNumber = ab.UserEnrollNumber
	                LEFT JOIN AbsentSymbol [as] ON [as].AbsentCode = ab.AbsentCode
                    LEFT JOIN [RelationDept] rd ON [rd].ID = [u].PhongKhoa
                WHERE (1 = 1) " + predicate + @"
                ORDER BY ab.UserEnrollNumber, ab.TimeDate
            ";
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_select, sqlConnection))
                {
                    data.Load(sqlCommand.ExecuteReader());
                }
            }

            return data;
        }
        public Dictionary<string, string> DanhSachKhoaPhongHC_Relation(string connectionString, string kp = "")
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();

            string filter = "";

            if (!String.IsNullOrEmpty(kp))
            {
                filter += " AND ID IN (" + kp + ") ";
            }

            string q = @"SELECT [ID], [DESCRIPTION] FROM [RelationDept] WHERE ID > 2 " + filter + " ORDER BY DESCRIPTION";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(q, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(reader["ID"].ToString(), reader["DESCRIPTION"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public DataTable KhaiBaoVang_TongHop(string connectionString, string kp, string dateFrom, string dateTo)
        {
            DataTable _dataTable = new DataTable();

            string filter = "";

            if (!String.IsNullOrEmpty(kp))
            {
                filter += " AND usr.PhongKhoaHC = " + kp;
            }

            filter += " AND CONVERT(varchar, TimeDate, 112) BETWEEN " + DateTime.ParseExact(dateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") + " AND " + DateTime.ParseExact(dateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");

            string _query2 = @"
                    DECLARE @Columns as NVARCHAR(MAX)
                    SELECT @Columns = COALESCE(@Columns + ', ','') + QUOTENAME(AbsentSymbol)
                    FROM
                        (
                            SELECT DISTINCT ab.AbsentSymbol
                            FROM AbsentSymbol ab
                        ) AS B
                    ORDER BY B.AbsentSymbol

                    DECLARE @SQL as NVARCHAR(MAX)
                    SET @SQL = N'SELECT UserFullCode, UserFullName, ' + @Columns + '
                    FROM
                    (
	                    SELECT ab.UserEnrollNumber as UserEnrollNumber, usr.UserFullCode, usr.UserFullName, s.AbsentSymbol, ab.Score as Score FROM Absent ab
		                    LEFT JOIN UserInfExt usr ON usr.UserEnrollNumber = ab.UserEnrollNumber
		                    LEFT JOIN AbsentSymbol s ON s.AbsentCode = ab.AbsentCode
	                    WHERE usr.UserEnrollNumber > 0 AND usr.[DaNghi] = 0 " + filter + @"
                    ) as PivotData
                    PIVOT
                    (
	                    SUM([Score]) FOR AbsentSymbol IN (' + @Columns + ')
                    ) AS PivotResult
                    ORDER BY UserEnrollNumber'
                    EXEC Sp_executesql @SQL";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                SqlDataAdapter _adapter = new SqlDataAdapter(_query2, sConnection);
                _adapter.Fill(_dataTable);
            }

            return _dataTable;
        }
    }
}