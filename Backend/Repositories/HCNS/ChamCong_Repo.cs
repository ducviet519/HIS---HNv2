using Dapper;
using System.App.Entities;
using System.App.Entities.HCNS;
using System.App.Repositories.Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace System.App.Repositories.HCNS
{
    public class ChamCong_Repo
    {
        public Dictionary<int, string> DanhSachLoaiNhanVien(string connectionString)
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();

            string query = "SELECT ID, TypeDes FROM UserType";

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    var reader = scmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(int.Parse(reader["ID"].ToString()), reader["TypeDes"].ToString());
                    }
                }
            }

            return lst;
        }
        public IEnumerable<Department> DanhSachKhoaPhong(string connectionString, string kp)
        {
            var __kp = "";

            if (!String.IsNullOrEmpty(kp))
            {
                __kp = " AND ID IN (" + kp + ")";
            }
            string q = "SELECT [ID] as STT, [Description] as KhoaP FROM [RelationDept] WHERE ID > 2" + __kp + " ORDER BY KhoaP";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<Department>(q);
                sqlConnection.Close();
                return result.ToList();
            }
        }
        public IEnumerable<Department> DanhSachKhoaPhongQL(string connectionString, int user)
        {
            string q = "SELECT [ID] as STT, [Description] as KhoaP FROM [RelationDept] WHERE ID > 2 AND ID IN (SELECT [ID] FROM [RelationDept] WHERE ID > 2 AND ID IN (SELECT [DeptID] FROM [UserRelationDept] WHERE [UserEnrollNumber] = @UserEnrollNumber)) ORDER BY KhoaP";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<Department>(q, new { @UserEnrollNumber = user });
                sqlConnection.Close();
                return result.ToList();
            }
        }
        public List<int> DS_NhanVienDaChamCong(string connectionString, string fromDate, string toDate)
        {
            var _ds = new List<int>();

            using (var sConnection = new SqlConnection(connectionString))
            {
                string q = "SELECT DISTINCT(UserEnrollNumber) FROM CheckInOut WHERE CONVERT(varchar(20), TimeDate, 112) >= " + fromDate + " AND CONVERT(varchar(20), TimeDate, 112) <= " + toDate;

                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand(q, sConnection))
                {
                    var reader = sCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            _ds.Add(int.Parse(reader["UserEnrollNumber"].ToString()));
                        }
                    }
                    reader.Close();
                }
            }
            return _ds;
        }
        public IEnumerable<CC_NhomChamCong> DS_NhomChamCong(string connectionString)
        {
            List<CC_NhomChamCong> lst = new List<CC_NhomChamCong>();

            using (var sConnection = new SqlConnection(connectionString))
            {
                string q = "SELECT ID, TenNhom, MoTa FROM TimeAttendanceGroup";

                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand(q, sConnection))
                {
                    var reader = sCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new CC_NhomChamCong()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                TenNhom = reader["TenNhom"].ToString(),
                                MoTa = reader["MoTa"].ToString()
                            });
                        }
                    }
                    reader.Close();
                }
            }
            return lst;
        }
        public IEnumerable<CC_CaChamCong> DS_CaChamCong(string connectionString)
        {
            List<CC_CaChamCong> lst = new List<CC_CaChamCong>();

            using (var sConnection = new SqlConnection(connectionString))
            {
                string q = @"SELECT c.ID, KhoaPhongCC, d.[Description] as TenKhoaPhong, Nhom, g.TenNhom as TenNhom, TenCa, MaCa, GioVao, GioRa, BatDauVao, KetThucVao, BatDauRa, KetThucRa, SapXep, HeSo
                             FROM TimeAttendanceConfig c
	                            INNER JOIN RelationDept d ON d.ID = c.KhoaPhongCC
                                INNER JOIN TimeAttendanceGroup g ON g.ID = c.Nhom ORDER BY c.TenCa, c.SapXep";

                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand(q, sConnection))
                {
                    var reader = sCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new CC_CaChamCong()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                KhoaPhong = int.Parse(reader["KhoaPhongCC"].ToString()),
                                TenKhoaPhong = reader["TenKhoaPhong"].ToString(),
                                Nhom = int.Parse(reader["Nhom"].ToString()),
                                TenNhom = reader["TenNhom"].ToString(),
                                TenCa = reader["TenCa"].ToString(),
                                MaCa = reader["MaCa"].ToString(),
                                GioVao = reader["GioVvao"].ToString(),
                                GioRa = reader["GioRa"].ToString(),
                                BatDauVao = reader["BatDauVao"].ToString(),
                                KetThucVao = reader["KetThucVao"].ToString(),
                                BatDauRa = reader["BatDauRa"].ToString(),
                                KetThucRa = reader["KetThucRa"].ToString(),
                                SapXep = int.Parse(reader["SapXep"].ToString()),
                                HeSo = int.Parse(reader["HeSo"].ToString())
                            });
                        }
                    }
                    reader.Close();
                }
            }
            return lst;
        }
        public IEnumerable<CC_CaChamCong> DS_CaChamCong_TheoKhoaPhong(string connectionString, int id_nhanvien)
        {
            List<CC_CaChamCong> lst = new List<CC_CaChamCong>();

            string filter = " AND KhoaPhongCC = (SELECT PhongKhoa FROM UserInfExt WHERE UserEnrollNumber = " + id_nhanvien + ")";

            using (var sConnection = new SqlConnection(connectionString))
            {
                string q = @"SELECT c.ID, KhoaPhongCC, d.[Description] as TenKhoaPhong, Nhom, g.TenNhom as TenNhom, TenCa, MaCa, GioVao, GioRa, BatDauVao, KetThucVao, BatDauRa, KetThucRa, SapXep, HeSo
                             FROM TimeAttendanceConfig c
	                            LEFT JOIN RelationDept d ON d.ID = c.KhoaPhongCC LEFT JOIN TimeAttendanceGroup g ON g.ID = c.Nhom WHERE 1 = 1 " + filter + " ORDER BY TenCa, SapXep";

                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand(q, sConnection))
                {
                    var reader = sCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new CC_CaChamCong()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                KhoaPhong = int.Parse(reader["KhoaPhongCC"].ToString()),
                                TenKhoaPhong = reader["TenKhoaPhong"].ToString(),
                                Nhom = int.Parse(reader["Nhom"].ToString()),
                                TenNhom = reader["TenNhom"].ToString(),
                                TenCa = reader["TenCa"].ToString(),
                                MaCa = reader["MaCa"].ToString(),
                                GioVao = reader["GioVao"].ToString(),
                                GioRa = reader["GioRa"].ToString(),
                                BatDauVao = reader["BatDauVao"].ToString(),
                                KetThucVao = reader["KetThucVao"].ToString(),
                                BatDauRa = reader["BatDauRa"].ToString(),
                                KetThucRa = reader["KetThucRa"].ToString(),
                                SapXep = int.Parse(reader["SapXep"].ToString()),
                                HeSo = int.Parse(reader["HeSo"].ToString())
                            });
                        }
                    }
                    reader.Close();
                }
            }
            return lst;
        }
        public IEnumerable<CC_CheckInOut> DS_DuLieuTuMayChamCong(string connectionString, string fromDate, string toDate)
        {
            List<CC_CheckInOut> lst = new List<CC_CheckInOut>();

            string filter = " AND CONVERT(varchar(20), TimeDate, 112) >= " + fromDate;

            filter += " AND CONVERT(varchar(20), TimeDate, 112) <= " + toDate;

            string q = @"
                SELECT [io].UserEnrollNumber, [io].UserFullCode, [usr].UserFullName, TimeDate, [io].TimeStr, WorkCode, [Source], MachineNo
                FROM CheckInOut [io]
	                JOIN UserInfExt usr ON usr.UserEnrollNumber = [io].UserEnrollNumber
                WHERE 1 = 1 " + filter + @"
                ORDER BY TimeDate";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand(q, sConnection))
                {
                    var reader = sCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new CC_CheckInOut()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullName = reader["UserFullName"].ToString(),
                                TimeDate = String.IsNullOrEmpty(reader["TimeDate"].ToString()) ? "" : DateTime.Parse(reader["TimeDate"].ToString()).ToString("dd/MM/yyyy"),
                                TimeStr = String.IsNullOrEmpty(reader["TimeStr"].ToString()) ? "" : DateTime.Parse(reader["TimeStr"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"),
                                WorkCode = String.IsNullOrEmpty(reader["WorkCode"].ToString()) ? 0 : int.Parse(reader["WorkCode"].ToString())
                            });
                        }
                    }
                    reader.Close();
                }
            }

            return lst;
        }
        public IEnumerable<CC_CheckInOut> DS_DuLieuTuMayChamCong_KhoaPhong(string connectionString, string fromDate, string toDate, int khoaphong)
        {
            List<CC_CheckInOut> lst = new List<CC_CheckInOut>();

            string filter = " AND CONVERT(varchar(20), TimeDate, 112) >= " + fromDate;

            filter += " AND CONVERT(varchar(20), TimeDate, 112) <= " + toDate;

            filter += " AND [io].UserEnrollNumber IN (SELECT UserEnrollNumber FROM UserInfExt WHERE PhongKhoa = " + khoaphong + " AND DaNghi = 0)";

            string q = @"
                SELECT [io].UserEnrollNumber, [io].UserFullCode, [usr].UserFullName, TimeDate, [io].TimeStr, WorkCode, [Source], MachineNo
                FROM CheckInOut [io]
	                JOIN UserInfExt usr ON usr.UserEnrollNumber = [io].UserEnrollNumber
                WHERE 1 = 1 " + filter + @"
                ORDER BY TimeDate";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand(q, sConnection))
                {
                    var reader = sCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new CC_CheckInOut()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullName = reader["UserFullName"].ToString(),
                                TimeDate = String.IsNullOrEmpty(reader["TimeDate"].ToString()) ? "" : DateTime.Parse(reader["TimeDate"].ToString()).ToString("dd/MM/yyyy"),
                                TimeStr = String.IsNullOrEmpty(reader["TimeStr"].ToString()) ? "" : DateTime.Parse(reader["TimeStr"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"),
                                WorkCode = String.IsNullOrEmpty(reader["WorkCode"].ToString()) ? 0 : int.Parse(reader["WorkCode"].ToString())
                            });
                        }
                    }
                    reader.Close();
                }
            }

            return lst;
        }
        public IEnumerable<CC_CheckInOut> DS_DuLieuTuMayChamCong_NhanVien(string connectionString, string fromDate, string toDate, int userid)
        {
            List<CC_CheckInOut> lst = new List<CC_CheckInOut>();

            string filter = " AND CONVERT(varchar(20), TimeDate, 112) >= " + fromDate;

            filter += " AND CONVERT(varchar(20), TimeDate, 112) <= " + toDate;

            filter += " AND [io].UserEnrollNumber = " + userid;

            string q = @"
                SELECT [io].UserEnrollNumber, [io].UserFullCode, [usr].UserFullName, TimeDate, [io].TimeStr, WorkCode, [Source], MachineNo
                FROM CheckInOut [io]
	                JOIN UserInfExt usr ON usr.UserEnrollNumber = [io].UserEnrollNumber
                WHERE 1 = 1 " + filter + @"
                ORDER BY TimeDate";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand(q, sConnection))
                {
                    var reader = sCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new CC_CheckInOut()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullName = reader["UserFullName"].ToString(),
                                TimeDate = String.IsNullOrEmpty(reader["TimeDate"].ToString()) ? "" : DateTime.Parse(reader["TimeDate"].ToString()).ToString("dd/MM/yyyy"),
                                TimeStr = String.IsNullOrEmpty(reader["TimeStr"].ToString()) ? "" : DateTime.Parse(reader["TimeStr"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"),
                                WorkCode = String.IsNullOrEmpty(reader["WorkCode"].ToString()) ? 0 : int.Parse(reader["WorkCode"].ToString()),
                                Source = reader["Source"].ToString(),
                                MachineNo = String.IsNullOrEmpty(reader["MachineNo"].ToString()) ? 0 : int.Parse(reader["MachineNo"].ToString()),
                            });
                        }
                    }
                    reader.Close();
                }
            }

            return lst;
        }
        public IEnumerable<XemCong> DS_Cong_TheoTK(string connectionString, XemCong obj)
        {
            List<XemCong> lstCong = new List<XemCong>();

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand("sp_XemCong_View", sConnection))
                {
                    sCommand.CommandType = CommandType.StoredProcedure;
                    sCommand.Parameters.Clear();
                    sCommand.Parameters.Add("@MaNV", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    sCommand.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = DateTime.ParseExact(obj.GioVao, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                    sCommand.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = DateTime.ParseExact(obj.GioRa, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                    var reader = sCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lstCong.Add(new XemCong()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                Date = DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM"),
                                DateFull = DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy"),
                                WD = reader["WD"].ToString(),
                                Ca = reader["DS"].ToString(),
                                NgayCong = string.IsNullOrEmpty(reader["ManDay"].ToString()) ? 0 : float.Parse(reader["ManDay"].ToString()),
                                GioVao = string.IsNullOrEmpty(reader["RIn1"].ToString()) ? "" : DateTime.Parse(reader["RIn1"].ToString()).ToString("HH:mm"),
                                GioRa = string.IsNullOrEmpty(reader["ROut1"].ToString()) ? "" : DateTime.Parse(reader["ROut1"].ToString()).ToString("HH:mm"),
                                DiMuonVeSom = string.IsNullOrEmpty(reader["LE1"].ToString()) ? "" : reader["LE1"].ToString(),
                                SoLanVaoRa = string.IsNullOrEmpty(reader["NIO1"].ToString()) ? 0 : int.Parse(reader["NIO1"].ToString()),
                                Mau = string.IsNullOrEmpty(reader["Color"].ToString()) ? 0 : int.Parse(reader["Color"].ToString()),
                                CongChuan = string.IsNullOrEmpty(reader["CC"].ToString()) ? 0 : float.Parse(reader["CC"].ToString()),
                                Status = string.IsNullOrEmpty(reader["Status"].ToString()) ? -1 : int.Parse(reader["Status"].ToString()),
                                DateOrder = int.Parse(DateTime.Parse(reader["Date"].ToString()).ToString("yyyyMMdd"))
                            });
                        }
                    }
                    return lstCong.OrderBy(x => x.DateOrder);
                }
            }
        }
        public DataTable Excel_DS_Cong(string connectionString, XemCong obj)
        {
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand("sp_XemCong_View", sConnection))
                {
                    sCommand.CommandType = CommandType.StoredProcedure;
                    sCommand.Parameters.Clear();
                    sCommand.Parameters.Add("@MaNV", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    sCommand.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = DateTime.ParseExact(obj.GioVao, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                    sCommand.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = DateTime.ParseExact(obj.GioRa, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");

                    DataTable dtable = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sCommand);

                    da.Fill(dtable);
                    return dtable;
                }
            }
        }

        public IEnumerable<Shifts> DS_Ca(string connectionString, Shifts obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                string procedure = "sp_ShiftList";
                DynamicParameters parameters = new DynamicParameters();

                if (!string.IsNullOrEmpty(obj.ShiftCode))
                    parameters.Add("@Code", obj.ShiftCode);

                if (!string.IsNullOrEmpty(obj.BD_VaoCa))
                    parameters.Add("@MinVao", obj.BD_VaoCa);

                if (!string.IsNullOrEmpty(obj.KT_VaoCa))
                    parameters.Add("@MaxVao", obj.KT_VaoCa);

                if (!string.IsNullOrEmpty(obj.BD_RaCa))
                    parameters.Add("@MinRa", obj.BD_RaCa);

                if (!string.IsNullOrEmpty(obj.KT_RaCa))
                    parameters.Add("@MaxRa", obj.KT_RaCa);

                if (obj.DayCount > 0)
                    parameters.Add("@NC", obj.DayCount);

                return db.Query<Shifts>(sql: procedure, param: parameters, commandType: CommandType.StoredProcedure);
            }
        }

        //Cập nhật dữ liệu vào - ra đã qua xử lý dữ liệu từ máy chấm công
        public bool CapNhat_InOutCol(string connectionString, CC_InOutCol obj)
        {
            int rowAffected = 0;

            string q = @"
                IF NOT EXISTS (SELECT UserEnrollNumber FROM InOutCol WHERE CONVERT(varchar(20), TimeDate, 112) = @TimeDate AND UserEnrollNumber = @UserEnrollNumber)
                    BEGIN
                        INSERT INTO InOutCol(UserEnrollNumber, TimeDate, TimeIn, Overday, MachineNoIn) VALUES (@UserEnrollNumber, @TimeDate, @TimeIn, @Overday, @MachineNoIn)
                    END
                ELSE
                    BEGIN
                        UPDATE InOutCol SET TimeOut = @TimeOut, MachineNoOut = @MachineNoOut WHERE UserEnrollNumber = @UserEnrollNumber AND CONVERT(varchar(20), TimeDate, 112) = @TimeDate
                    END";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand(q, sConnection))
                {
                    sCommand.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;

                    if (String.IsNullOrEmpty(obj.TimeDate))
                    {
                        sCommand.Parameters.Add("@TimeDate", SqlDbType.Date).Value = DBNull.Value;
                    }
                    else
                    {
                        sCommand.Parameters.Add("@TimeDate", SqlDbType.Date).Value = DateTime.Parse(obj.TimeDate);
                    }

                    if (String.IsNullOrEmpty(obj.TimeIn))
                    {
                        sCommand.Parameters.Add("@TimeIn", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        sCommand.Parameters.Add("@TimeIn", SqlDbType.DateTime).Value = DateTime.Parse(obj.TimeIn);
                    }

                    if (String.IsNullOrEmpty(obj.TimeOut))
                    {
                        sCommand.Parameters.Add("@TimeOut", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        sCommand.Parameters.Add("@TimeOut", SqlDbType.DateTime).Value = DateTime.Parse(obj.TimeOut);
                    }

                    sCommand.Parameters.Add("@OverDay", SqlDbType.Int).Value = obj.OverDay;
                    sCommand.Parameters.Add("@MachineNoIn", SqlDbType.Int).Value = obj.MachineNoIn;
                    sCommand.Parameters.Add("@MachineNoOut", SqlDbType.Int).Value = obj.MachineNoOut;

                    rowAffected = sCommand.ExecuteNonQuery();
                }
            }

            return rowAffected == 0 ? false : true;
        }
        public IEnumerable<CC_CheckInOut> DanhSach_SuaCong(string connectionString, string makp, string manv, string tungay, string denngay)
        {
            List<CC_CheckInOut> lst = new List<CC_CheckInOut>();

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand("sp_TaiCong_View", sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Add("@KP", SqlDbType.Int).Value = string.IsNullOrEmpty(makp) ? 0 : int.Parse(makp);
                    sCmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = string.IsNullOrEmpty(manv) ? 0 : int.Parse(manv);
                    sCmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = string.IsNullOrEmpty(tungay) ? DateTime.UtcNow.AddHours(7).AddMonths(-1).ToString("yyyyMM25") : DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                    sCmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = string.IsNullOrEmpty(denngay) ? DateTime.UtcNow.AddHours(7).ToString("yyyyMM24") : DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");

                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(new CC_CheckInOut()
                        {
                            UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                            UserFullName = reader["UserFullName"].ToString(),
                            TimeDate = reader["Date"].ToString(),
                            TimeStr = reader["Time"].ToString(),
                            MachineNo = int.Parse(reader["MachineNo"].ToString()),
                            Source = reader["Source"].ToString(),
                            KhoaPhong = reader["Description"].ToString(),
                            FullDate = DateTime.ParseExact(reader["Date"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " " + reader["Time"].ToString(),
                            WorkCode = int.Parse(reader["WorkCode"].ToString()),
                            MaQL = reader["MaQL"].ToString()
                        });
                    }
                }
            }

            return lst;
        }
        public IEnumerable<CC_TongHopCong_ChiTiet> DanhSach_TongCong(string connectionString, string makp, string manv, string tungay, string denngay)
        {
            List<CC_TongHopCong_ChiTiet> lst = new List<CC_TongHopCong_ChiTiet>();

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand("sp_TongCong_View", sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Add("@KP", SqlDbType.VarChar).Value = makp;
                    sCmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = string.IsNullOrEmpty(manv) ? 0 : int.Parse(manv);
                    sCmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = string.IsNullOrEmpty(tungay) ? "" : DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                    sCmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = string.IsNullOrEmpty(denngay) ? "" : DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");

                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(new CC_TongHopCong_ChiTiet()
                        {
                            UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                            UserFullCode = reader["UserFullCode"].ToString(),
                            UserFullName = reader["UserFullName"].ToString(),
                            KhoaPhong = reader["NoiCC"].ToString(),
                            WD = int.Parse(reader["WD"].ToString()),
                            AbsentSymbol = reader["AbsentSymbol"].ToString(),
                            USC1 = reader["USC1"].ToString(),
                            USC2 = reader["USC2"].ToString(),
                            Ngay = DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy"),
                            WorkDay1 = float.Parse(reader["WorkDay1"].ToString()),
                            WorkDay2 = float.Parse(reader["WorkDay2"].ToString())
                        });
                    }
                }
            }

            return lst;
        }
        public CC_CheckInOut Tim_Cong(string connectionString, string id, string time)
        {
            string query = @"SELECT a.UserEnrollNumber
	                        ,b.UserFullCode
	                        ,b.UserFullName
	                        ,c.Description
	                        ,CONVERT(varchar(10),a.TimeDate,103) AS Date
	                        ,CONVERT(varchar(10),a.TimeStr,108) AS Time
	                        ,a.OriginType
	                        ,a.NewType
	                        ,a.Source
	                        ,a.MachineNo
                        FROM CheckInOut a
                        LEFT JOIN UserInfExt b ON a.UserEnrollNumber = b.UserEnrollNumber
                        LEFT JOIN RelationDept c ON b.PhongKhoa = c.ID
                        WHERE 1 = 1 AND CONVERT(varchar, TimeStr, 120) = '" + time + "'";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.CommandType = CommandType.Text;

                    var reader = sCmd.ExecuteReader();

                    var o = new CC_CheckInOut();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            o = new CC_CheckInOut()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullName = reader["UserFullName"].ToString(),
                                TimeDate = reader["Date"].ToString(),
                                TimeStr = reader["Time"].ToString(),
                                MachineNo = int.Parse(reader["MachineNo"].ToString()),
                                Source = reader["Source"].ToString(),
                                KhoaPhong = reader["Description"].ToString()
                            };
                        }
                    }

                    return o;
                }
            }
        }
        public bool Add_DuLieuCong(string connectionString, CC_CheckInOut obj)
        {
            string query = @"INSERT INTO CheckInOut(UserEnrollNumber, TimeStr, TimeDate, OriginType, NewType, Source, MachineNo, WorkCode, UserFullCode)
                            VALUES (" + obj.UserEnrollNumber + ",'" + obj.TimeStr + "','" + obj.TimeDate + "','I','','" + obj.Source + "','" + obj.MachineNo + "','" + obj.WorkCode + "','" + obj.UserFullCode + "')";

            int rowAffected = 0;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    rowAffected = sCmd.ExecuteNonQuery();
                }
            }

            return rowAffected > 0 ? true : false;
        }
        public bool Update_DuLieuCong(string connectionString, CC_CheckInOut obj)
        {
            string query = "UPDATE CheckInOut SET TimeStr = '" + obj.TimeDate + " " + obj.NewTime + "' WHERE UserEnrollNumber = " + obj.UserEnrollNumber + " AND CONVERT(varchar, TimeStr, 120) = '" + obj.TimeDate + " " + obj.TimeStr + "'";

            int rowAffected = 0;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    rowAffected = sCmd.ExecuteNonQuery();
                }
            }

            return rowAffected > 0 ? true : false;
        }
        public bool Xoa_DuLieuCong(string connectionString, CC_CheckInOut obj)
        {
            string query = "DELETE CheckInOut WHERE UserEnrollNumber = " + obj.UserEnrollNumber + " AND CONVERT(varchar, TimeStr, 120) = '" + obj.TimeDate + " " + obj.TimeStr + "'";

            int rowAffected = 0;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    rowAffected = sCmd.ExecuteNonQuery();
                }
            }

            return rowAffected > 0 ? true : false;
        }
        public IEnumerable<HCNS_NhanVien> TimNhanVienTheoKhoa(string connectionString, string prefix, int user)
        {
            //string query = @"SELECT UserEnrollNumber, UserFullName, UserFullCode FROM UserInfExt WHERE (PhongKhoa IN (SELECT DeptID FROM UserRelationDept WHERE UserEnrollNumber = " + user + ") OR (PhongKhoa = (SELECT PhongKhoa FROM UserInfExt WHERE UserEnrollNumber = " + user + ")) AND (UserFullCode LIKE '%" + prefix + "%' OR UserFullName LIKE N'%" + prefix + "%'))";
            string query = @"SELECT UserEnrollNumber, UserFullName, UserFullCode FROM UserInfExt WHERE UserEnrollNumber = " + user + " and (UserFullCode LIKE '%" + prefix + "%' OR UserFullName LIKE N'%" + prefix + "%')";

            List<HCNS_NhanVien> lst = new List<HCNS_NhanVien>();

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(new HCNS_NhanVien()
                        {
                            UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                            UserFullCode = reader["UserFullCode"].ToString(),
                            UserFullName = reader["UserFullName"].ToString()
                        });
                    }
                }
            }

            return lst;
        }
        public IEnumerable<HCNS_NhanVien> TimNhanVienTheoKhoa(string connectionString, string prefix, int user, int kp)
        {
            string query = string.Empty;
            if (user == 0)
            {
                query = @"SELECT UserEnrollNumber, UserFullName, UserFullCode FROM UserInfExt WHERE PhongKhoa in (" + kp + ") AND (UserFullCode LIKE '%" + prefix + "%' OR UserFullName LIKE N'%" + prefix + "%')";
            }
            else
            {
                query = @"if (select count(*) from UserRelationDept where UserEnrollNumber = " + user + ") > 0 " +
                        "begin " +
                            "SELECT UserEnrollNumber, UserFullName, UserFullCode FROM UserInfExt WHERE PhongKhoa in (select DeptID from UserRelationDept where UserEnrollNumber = " + user + ") AND (UserFullCode LIKE '%" + prefix + "%' OR UserFullName LIKE N'%" + prefix + "%') " +
                        "end " +
                    "else " +
                        "begin " +
                            "SELECT UserEnrollNumber, UserFullName, UserFullCode FROM UserInfExt WHERE PhongKhoa in (select PhongKhoa from UserInfExt where UserEnrollNumber = " + user + ") AND (UserFullCode LIKE '%" + prefix + "%' OR UserFullName LIKE N'%" + prefix + "%') " +
                        "end";
            }


            List<HCNS_NhanVien> lst = new List<HCNS_NhanVien>();

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(new HCNS_NhanVien()
                        {
                            UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                            UserFullCode = reader["UserFullCode"].ToString(),
                            UserFullName = reader["UserFullName"].ToString()
                        });
                    }
                }
            }

            return lst;
        }
        public IEnumerable<HCNS_NhanVien> TimNhanVienTheoKhoaAll(string connectionString, string prefix, int user)
        {
            //string query = @"SELECT UserEnrollNumber, UserFullName, UserFullCode FROM UserInfExt WHERE PhongKhoa = " + kp + " AND (UserFullCode LIKE '%" + prefix + "%' OR UserFullName LIKE N'%" + prefix + "%')";

            string query = @"
                SELECT UserEnrollNumber, UserFullName, UserFullCode
                FROM UserInfExt
                WHERE PhongKhoa IN (SELECT DeptID FROM UserRelationDept WHERE UserEnrollNumber = " + user + @") AND (UserFullCode LIKE '%" + prefix + "%' OR UserFullName LIKE N'%" + prefix + "%')";

            List<HCNS_NhanVien> lst = new List<HCNS_NhanVien>();

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(new HCNS_NhanVien()
                        {
                            UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                            UserFullCode = reader["UserFullCode"].ToString(),
                            UserFullName = reader["UserFullName"].ToString()
                        });
                    }
                }
            }

            return lst;
        }
        public IEnumerable<HCNS_NhanVien> TimNhanVienTheoKhoa(string connectionString, string prefix, string kp)
        {
            string query = @"SELECT UserEnrollNumber, UserFullName, UserFullCode FROM UserInfExt WHERE PhongKhoa = " + kp + " AND (UserFullCode LIKE '%" + prefix + "%' OR UserFullName LIKE N'%" + prefix + "%')";

            List<HCNS_NhanVien> lst = new List<HCNS_NhanVien>();

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(new HCNS_NhanVien()
                        {
                            UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                            UserFullCode = reader["UserFullCode"].ToString(),
                            UserFullName = reader["UserFullName"].ToString()
                        });
                    }
                }
            }

            return lst;
        }

        #region XỬ LÝ KHAI BÁO VẮNG
        public IEnumerable<AbsentR> KhaiBaoVang_DanhSachChoDuyet(string connectionString, AbsentR obj)
        {
            List<AbsentR> objRs = new List<AbsentR>();

            string filter = "";

            filter += " AND d.ID = @MAKP";

            if (obj.UserEnrollNumber > 0)
            {
                filter += " AND a.UserEnrollNumber = @USER";
            }

            string query = @"SELECT a.UserEnrollNumber, c.UserFullCode, c.UserFullName, d.ID AS MaKP, d.Description AS KhoaPhong, CONVERT(VARCHAR, Date, 103) AS Date, a.AbsentCode, b.AbsentDescription, Reason, Status, CreateAcc, UpdateAcc, CreateTime, UpdateTime
                    FROM AbsentR a
                        LEFT JOIN AbsentSymbol b ON a.AbsentCode = b.AbsentCode
                        LEFT JOIN UserInfExt c ON a.UserEnrollNumber = c.UserEnrollNumber
                        LEFT JOIN RelationDept d ON c.PhongKhoa = d.ID
                    WHERE (a.Status = 0 OR a.Status = 1)  AND (Date >= @FROM AND Date <= @TO)" + filter;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@MAKP", SqlDbType.Int).Value = obj.MaKP;
                    sCmd.Parameters.Add("@FROM", SqlDbType.Date).Value = DateTime.ParseExact(obj.From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    sCmd.Parameters.Add("@TO", SqlDbType.Date).Value = DateTime.ParseExact(obj.To, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    sCmd.Parameters.Add("@USER", SqlDbType.Int).Value = obj.UserEnrollNumber;

                    var reader = sCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objRs.Add(new AbsentR()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                MaKP = int.Parse(reader["MaKP"].ToString()),
                                KhoaPhong = reader["KhoaPhong"].ToString(),
                                Date = reader["Date"].ToString(),
                                AbsentCode = reader["AbsentCode"].ToString(),
                                AbsentDescription = reader["AbsentDescription"].ToString(),
                                Reason = reader["Reason"].ToString(),
                                Status = int.Parse(reader["Status"].ToString()),
                                CreateAcc = reader["CreateAcc"].ToString(),
                                UpdateAcc = reader["UpdateAcc"].ToString(),
                                CreateTime = DateTime.Parse(reader["CreateTime"].ToString()),
                                UpdateTime = DateTime.Parse(reader["UpdateTime"].ToString())
                            });
                        }
                    }
                }
            }
            return objRs;
        }
        public AbsentR KhaiBaoVang_ThongTinR(string connectionString, int id, string date)
        {
            string query = @"SELECT UserEnrollNumber, CONVERT(VARCHAR, Date, 103) AS Date, a.AbsentCode, b.AbsentDescription, Reason, Status, CreateAcc, CreateTime
                    FROM AbsentR a
                        LEFT JOIN AbsentSymbol b ON a.AbsentCode = b.AbsentCode
                    WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";
            AbsentR obj = new AbsentR();
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = id;
                    sCmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var reader = sCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            obj.UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString());
                            obj.Date = reader["Date"].ToString();
                            obj.AbsentCode = reader["AbsentCode"].ToString();
                            obj.AbsentDescription = reader["AbsentDescription"].ToString();
                            obj.Reason = reader["Reason"].ToString();
                            obj.Status = int.Parse(reader["Status"].ToString());
                            obj.CreateAcc = reader["CreateAcc"].ToString();
                            obj.CreateTime = DateTime.Parse(reader["CreateTime"].ToString());
                        }
                    }
                    else
                    {
                        obj.UserEnrollNumber = id;
                        obj.Date = date;
                    }
                }
            }
            return obj;
        }
        public bool KhaiBaoVang_XoaKhaiBaoR(string connectionString, int id, string date)
        {
            int rowAffected = 0;

            string query = $@"DELETE AbsentR WHERE UserEnrollNumber = {id} AND FORMAT(Date,'dd/MM/yyyy') = '{date}'";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool ThemMoiKhaiBaoR(string connectionString, AbsentR obj)
        {
            int rowAffected = 0;

            string _query = @"
                IF NOT EXISTS (SELECT 1 FROM AbsentR WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date)
	                BEGIN
		                INSERT INTO AbsentR(UserEnrollNumber, Date, AbsentCode, Reason, Status, CreateAcc, UpdateAcc, CreateTime, UpdateTime)
		                VALUES (@UserEnrollNumber, @Date, @AbsentCode, @Reason, @Status, @CreateAcc, @UpdateAcc, @CreateTime, @UpdateTime)
	                END
                ELSE
	                BEGIN
		                UPDATE AbsentR SET AbsentCode = @AbsentCode, Reason = @Reason, Status = 0, UpdateAcc = @UpdateAcc, UpdateTime = @UpdateTime WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date AND (Status <= 0)
	                END";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", obj.UserEnrollNumber);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.CurrentCulture));
                    sqlCommand.Parameters.AddWithValue("@AbsentCode", obj.AbsentCode);
                    sqlCommand.Parameters.AddWithValue("@Reason", string.IsNullOrEmpty(obj.Reason) ? "" : obj.Reason);
                    sqlCommand.Parameters.AddWithValue("@Status", obj.Status);
                    sqlCommand.Parameters.AddWithValue("@CreateAcc", obj.CreateAcc);
                    sqlCommand.Parameters.AddWithValue("@UpdateAcc", obj.UpdateAcc);
                    sqlCommand.Parameters.AddWithValue("@CreateTime", DateTime.UtcNow.AddHours(7));
                    sqlCommand.Parameters.AddWithValue("@UpdateTime", DateTime.UtcNow.AddHours(7));

                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool ThemMoiKhaiBaoR(string connectionString, List<AbsentR> objs, string nguoiud)
        {
            int rowAffected = 0;

            string _query = @"
                IF NOT EXISTS (SELECT 1 FROM AbsentR WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date)
	                BEGIN
		                INSERT INTO AbsentR(UserEnrollNumber, Date, AbsentCode, Reason, Status, CreateAcc, UpdateAcc, CreateTime, UpdateTime)
		                VALUES (@UserEnrollNumber, @Date, @AbsentCode, @Reason, @Status, @CreateAcc, @UpdateAcc, @CreateTime, @UpdateTime)
	                END
                ELSE
	                BEGIN
		                UPDATE AbsentR SET AbsentCode = @AbsentCode, Reason = @Reason, Status = @Status, UpdateAcc = @UpdateAcc, UpdateTime = @UpdateTime WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date AND (Status <= 0)
	                END";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                foreach (var obj in objs)
                {
                    using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", obj.UserEnrollNumber);
                        sqlCommand.Parameters.AddWithValue("@Date", DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                        sqlCommand.Parameters.AddWithValue("@AbsentCode", obj.AbsentCode);
                        sqlCommand.Parameters.AddWithValue("@Reason", string.IsNullOrEmpty(obj.Reason) ? "" : obj.Reason);
                        sqlCommand.Parameters.AddWithValue("@Status", obj.Status);
                        sqlCommand.Parameters.AddWithValue("@CreateAcc", nguoiud);
                        sqlCommand.Parameters.AddWithValue("@UpdateAcc", nguoiud);
                        sqlCommand.Parameters.AddWithValue("@CreateTime", DateTime.UtcNow.AddHours(7));
                        sqlCommand.Parameters.AddWithValue("@UpdateTime", DateTime.UtcNow.AddHours(7));

                        rowAffected += sqlCommand.ExecuteNonQuery();
                    }
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool KhaiBaoVang_XuLyDuyet(string connectionString, List<AbsentR> objs)
        {
            int rowAffected = 0;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                foreach (var obj in objs)
                {
                    string query = @"
                        UPDATE [AbsentR] SET Status = @Status, UpdateAcc = @UpdateAcc, UpdateTime = @UpdateTime WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @TimeDate
                        IF NOT EXISTS (SELECT 1 FROM [Absent] WHERE UserEnrollNumber = @UserEnrollNumber AND TimeDate = @TimeDate)
	                        BEGIN
		                        INSERT INTO [Absent](UserEnrollNumber, TimeDate, AbsentCode, AddedTime, Lydo, Thang, Nam, UserAccount)
		                        VALUES (@UserEnrollNumber, @TimeDate, @AbsentCode, @AddedTime, @Lydo, @Thang, @Nam, @UserAccount)
	                        END
                        ELSE
                            BEGIN
                                UPDATE [Absent] SET AbsentCode = @AbsentCode, Lydo = @Lydo, UserAccount = @UserAccount WHERE UserEnrollNumber = @UserEnrollNumber AND TimeDate = @TimeDate
                            END
                    ";

                    using (var sCmd = new SqlCommand(query, sConnection))
                    {
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                        sCmd.Parameters.Add("@TimeDate", SqlDbType.SmallDateTime).Value = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        sCmd.Parameters.Add("@AbsentCode", SqlDbType.VarChar).Value = obj.AbsentCode;
                        sCmd.Parameters.Add("@AddedTime", SqlDbType.DateTime).Value = obj.CreateTime;
                        sCmd.Parameters.Add("@Lydo", SqlDbType.NVarChar).Value = obj.Reason;
                        sCmd.Parameters.Add("@Thang", SqlDbType.Int).Value = obj.CreateTime.Month;
                        sCmd.Parameters.Add("@Nam", SqlDbType.Int).Value = obj.CreateTime.Year;
                        sCmd.Parameters.Add("@UserAccount", SqlDbType.VarChar).Value = obj.CreateAcc;
                        sCmd.Parameters.Add("@Status", SqlDbType.SmallInt).Value = obj.Status;
                        sCmd.Parameters.Add("@UpdateAcc", SqlDbType.VarChar).Value = obj.UpdateAcc;
                        sCmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);

                        rowAffected += sCmd.ExecuteNonQuery();
                    }
                }
                if (rowAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool KhaiBaoVang_XuLyHuyDuyet(string connectionString, List<AbsentR> objs)
        {
            int rowAffected = 0;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                foreach (var obj in objs)
                {
                    string query = @"UPDATE [AbsentR] SET Status = @Status WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @TimeDate";

                    using (var sCmd = new SqlCommand(query, sConnection))
                    {
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                        sCmd.Parameters.Add("@TimeDate", SqlDbType.SmallDateTime).Value = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        sCmd.Parameters.Add("@Status", SqlDbType.SmallInt).Value = obj.Status;

                        rowAffected += sCmd.ExecuteNonQuery();
                    }
                }
                if (rowAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool KhaiBaoVang_UpdateStatus(string connectionString, int userid, DateTime date, int status)
        {
            string query = "UPDATE AbsentR SET Status = @Status WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";
            int rowAffected = 0;
            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = userid;
                    scmd.Parameters.Add("@Date", SqlDbType.Date).Value = date;
                    scmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            if (rowAffected > 0)
                return true;
            return false;
        }
        #endregion XỬ LÝ KHAI BÁO VẮNG

        #region XỬ LÝ DANH SÁCH CA.

        public DataTable DanhSachCa_View(string connectionString, string dateString, string fromDate, string toDate, int userID, int makp, int type, int qldd)
        {
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand("sp_KhaiBaoCa_View", sConnection))
                {
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@KP", SqlDbType.Int).Value = makp;
                    sCmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = userID;
                    //sCmd.Parameters.Add("@DateString", SqlDbType.VarChar).Value = dateString.Substring(0, dateString.Length - 1);
                    sCmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = fromDate;
                    sCmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = toDate;
                    sCmd.Parameters.Add("@Type", SqlDbType.Int).Value = type;
                    sCmd.Parameters.Add("@QLDD", SqlDbType.Int).Value = qldd;

                    SqlDataAdapter adapter = new SqlDataAdapter(sCmd);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }
        public IEnumerable<Shifts> DanhSachCa_DropDownList_All(string connectionString)
        {
            List<Shifts> lst = new List<Shifts>();
            string query = "SELECT ShiftID AS MaCa, CAST(ShiftCode AS varchar(12)) + ' - [ Vào: ' + Onduty + ' - Ra: ' + Offduty + ' ]' AS Ca " +
                " FROM Shifts a" +
                " WHERE HolidayOTLevel = 1";
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(new Shifts()
                        {
                            ShiftID = int.Parse(reader["MaCa"].ToString()),
                            ShiftCode = reader["Ca"].ToString()
                        });
                    }
                }
            }
            return lst;
        }
        public IEnumerable<Shifts> DanhSachCa_Dropdownlist(string connectionString, int user)
        {
            List<Shifts> lst = new List<Shifts>();

            string dsca = "SELECT CaPhu FROM UserInfExt WHERE UserEnrollNumber = " + user;
            string strca = "", filter = "";
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(dsca, sConnection))
                {
                    var o = sCmd.ExecuteScalar();

                    if (o != null)
                        strca = o.ToString();
                }

                if (!string.IsNullOrEmpty(strca))
                {
                    filter = " AND ShiftID IN (";
                    for (int i = 0; i < strca.Split(',').Length; i++)
                    {
                        filter += int.Parse(strca.Split(',')[i].ToString()) + ",";
                    }
                    filter = filter.Substring(0, filter.Length - 1) + ")";
                }

                string query = "SELECT ShiftID AS MaCa, CAST(ShiftCode AS varchar(12)) + ' - [ Vào: ' + Onduty + ' - Ra: ' + Offduty + ' ]' AS Ca " +
                " FROM Shifts a" +
                " WHERE HolidayOTLevel = 1" + filter;

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(new Shifts()
                        {
                            ShiftID = int.Parse(reader["MaCa"].ToString()),
                            ShiftCode = reader["Ca"].ToString()
                        });
                    }
                }
            }

            return lst;
        }
        public IEnumerable<Shifts> DanhSachCa_Checkbox(string connectionString)
        {
            List<Shifts> lst = new List<Shifts>();

            string query = "SELECT ShiftID, ShiftCode FROM Shifts WHERE HolidayOTLevel = 1";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(new Shifts()
                        {
                            ShiftID = int.Parse(reader["ShiftID"].ToString()),
                            ShiftCode = reader["ShiftCode"].ToString()
                        });
                    }
                }
            }

            return lst;
        }
        public int[] DanhSachCa_CheckboxWithDefaultValueUser(string connectionString, int user)
        {
            List<int> lst = new List<int>();
            string cp = "";
            string query = "SELECT CaPhu FROM UserInfExt WHERE UserEnrollNumber = @user";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@user", SqlDbType.Int).Value = user;
                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cp = reader["CaPhu"].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(cp))
                {
                    var cpSplit = cp.Split(',');

                    for (int i = 0; i < cpSplit.Length; i++)
                    {
                        lst.Add(int.Parse(cpSplit[i].ToString()));
                    }
                }
                else
                {
                    lst.Add(0);
                }
            }

            return lst.ToArray();
        }
        public UserTempShift DanhSachCa_ThongTin(string connectionString, UserTempShift obj)
        {
            string query = "SELECT UserEnrollNumber, Date, Shift1, b.ShiftCode, CreateAcc, UpdateAcc, UpdateTime, CreateTime FROM UserTempShift a LEFT JOIN Shifts b ON a.Shift1 = b.ShiftID WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";

            UserTempShift o = new UserTempShift();

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    sCmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var reader = sCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            o = new UserTempShift()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                Date = DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy"),
                                Shift1 = int.Parse(reader["Shift1"].ToString()),
                                ShiftCode1 = reader["ShiftCode"].ToString(),
                                CreateAcc = reader["CreateAcc"].ToString(),
                                UpdateAcc = reader["UpdateAcc"].ToString(),
                                CreateTime = string.IsNullOrEmpty(reader["CreateTime"].ToString()) ? "" : DateTime.Parse(reader["CreateTime"].ToString()).ToString("dd/MM/yyyy"),
                                UpdateTime = string.IsNullOrEmpty(reader["UpdateTime"].ToString()) ? "" : DateTime.Parse(reader["UpdateTime"].ToString()).ToString("dd/MM/yyyy")
                            };
                        }
                    }
                    else
                    {
                        o.UserEnrollNumber = obj.UserEnrollNumber;
                        o.Date = obj.Date;
                    }
                    return o;
                }
            }
        }
        public UserTempShift DanhSachCa_ThongTinR(string connectionString, UserTempShift obj)
        {
            string query = "SELECT UserEnrollNumber, Date, Shift1, a.Status, b.ShiftCode, CreateAcc, UpdateAcc, UpdateTime, CreateTime FROM [UserTempShiftR] a LEFT JOIN Shifts b ON a.Shift1 = b.ShiftID WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";

            UserTempShift o = new UserTempShift();

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    sCmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var reader = sCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            o = new UserTempShift()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                Date = DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy"),
                                Shift1 = int.Parse(reader["Shift1"].ToString()),
                                ShiftCode1 = reader["ShiftCode"].ToString(),
                                Status = int.Parse(reader["Status"].ToString()),
                                CreateAcc = reader["CreateAcc"].ToString(),
                                UpdateAcc = reader["UpdateAcc"].ToString(),
                                CreateTime = string.IsNullOrEmpty(reader["CreateTime"].ToString()) ? "" : DateTime.Parse(reader["CreateTime"].ToString()).ToString("dd/MM/yyyy"),
                                UpdateTime = string.IsNullOrEmpty(reader["UpdateTime"].ToString()) ? "" : DateTime.Parse(reader["UpdateTime"].ToString()).ToString("dd/MM/yyyy")
                            };
                        }
                    }
                    else
                    {
                        o.UserEnrollNumber = obj.UserEnrollNumber;
                        o.Date = obj.Date;
                    }
                    return o;
                }
            }
        }
        public IEnumerable<UserTempShift> DanhSachCa_ThongTinDaKhaiBao(string connectionString, UserTempShift obj)
        {
            string query = @"SELECT UserEnrollNumber, Date, Shift1, (b.ShiftCode + N' [Từ: ' + b.Onduty + N' - Đến: ' + b.Offduty + ']') as ShiftDesc, a.Status, CreateAcc, UpdateAcc, UpdateTime, CreateTime FROM UserTempShiftR a
                    LEFT JOIN Shifts b ON a.Shift1 = b.ShiftID
                    WHERE(Date >= @DateFrom AND Date <= @DateTo) AND UserEnrollNumber = @UserEnrollNumber AND Status = 0";

            List<UserTempShift> o = new List<UserTempShift>();

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    sCmd.Parameters.Add("@DateFrom", SqlDbType.Date).Value = DateTime.ParseExact(obj.DateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    sCmd.Parameters.Add("@DateTo", SqlDbType.Date).Value = DateTime.ParseExact(obj.DateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var reader = sCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            o.Add(new UserTempShift()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                Date = DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy"),
                                Shift1 = int.Parse(reader["Shift1"].ToString()),
                                ShiftDesc = reader["ShiftDesc"].ToString(),
                                Status = int.Parse(reader["Status"].ToString()),
                                CreateAcc = reader["CreateAcc"].ToString(),
                                UpdateAcc = reader["UpdateAcc"].ToString(),
                                CreateTime = string.IsNullOrEmpty(reader["CreateTime"].ToString()) ? "" : DateTime.Parse(reader["CreateTime"].ToString()).ToString("dd/MM/yyyy"),
                                UpdateTime = string.IsNullOrEmpty(reader["UpdateTime"].ToString()) ? "" : DateTime.Parse(reader["UpdateTime"].ToString()).ToString("dd/MM/yyyy")
                            });
                        }
                    }
                }
            }
            return o;
        }
        public string DanhSachCa_KiemTraTrung(string connectionString, UserTempShift obj)
        {
            string query = "SELECT Date, CAST(s.ShiftCode AS varchar(12)) + ' - [ Vào: ' + s.Onduty + ' - Ra: ' + s.Offduty + ' ]' AS Shift1 FROM UserTempShift us LEFT JOIN Shifts s ON us.Shift1 = s.ShiftID WHERE UserEnrollNumber = @UserEnrollNumber AND convert(varchar, Date, 112) IN (" + obj.DateString + ")";

            string _result = "";
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    //sCmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //sCmd.Parameters.Add("@Date", SqlDbType.VarChar).Value = obj.DateString;

                    var reader = sCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            _result += DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy") + " - Ca: " + reader["Shift1"].ToString() + "\n";
                        }
                    }
                    return _result;
                }
            }
        }
        public string DanhSachCa_KiemTraTrungR(string connectionString, UserTempShift obj)
        {
            string query = "SELECT Date, CAST(s.ShiftCode AS varchar(12)) + ' - [ Vào: ' + s.Onduty + ' - Ra: ' + s.Offduty + ' ]' AS Shift1 FROM UserTempShiftR us LEFT JOIN Shifts s ON us.Shift1 = s.ShiftID WHERE UserEnrollNumber = @UserEnrollNumber AND convert(varchar, Date, 112) IN (" + obj.DateString + ")";

            string _result = "";
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    //sCmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //sCmd.Parameters.Add("@Date", SqlDbType.VarChar).Value = obj.DateString;

                    var reader = sCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            _result += DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy") + " - Ca: " + reader["Shift1"].ToString() + "\n";
                        }
                    }
                    return _result;
                }
            }
        }
        public bool DanhSachCa_AddCaMoi(string connectionString, UserTempShift obj)
        {
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                var rowAffected = 0;

                var splitDate = obj.DateString.Split(',');

                for (int i = 0; i < splitDate.Length; i++)
                {
                    DateTime dateFormat = DateTime.ParseExact(splitDate[i].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

                    string query =
                    "IF NOT EXISTS (SELECT 1 FROM UserTempShift WHERE UserEnrollNumber = @UserEnrollNumber AND convert(varchar, Date, 112) = '" + splitDate[i].ToString() + "')" +
                    " BEGIN INSERT INTO UserTempShift(UserEnrollNumber, Date, Shift1, CreateAcc, UpdateAcc, CreateTime, UpdateTime) VALUES (@UserEnrollNumber, @Date, @Shift1, @CreateAcc, @UpdateAcc, @CreateTime, @UpdateTime) END" +
                    " ELSE BEGIN UPDATE UserTempShift SET Shift1 = @Shift1, UpdateTime = @UpdateTime, UpdateAcc = @UpdateAcc WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date END";

                    using (var sCmd = new SqlCommand(query, sConnection))
                    {
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                        sCmd.Parameters.Add("@Date", SqlDbType.Date).Value = dateFormat;
                        sCmd.Parameters.Add("@Shift1", SqlDbType.Int).Value = obj.Shift1;
                        sCmd.Parameters.Add("@CreateAcc", SqlDbType.VarChar).Value = obj.CreateAcc;
                        sCmd.Parameters.Add("@UpdateAcc", SqlDbType.VarChar).Value = obj.UpdateAcc;
                        sCmd.Parameters.Add("@CreateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                        sCmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);

                        rowAffected += sCmd.ExecuteNonQuery();
                    }
                }
                if (rowAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool DanhSachCa_AddNhieuCaMoi(string connectionString, List<UserTempShift> objs)
        {
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                var rowAffected = 0;

                foreach (var obj in objs)
                {
                    var splitDate = obj.DateString.Split(',');

                    for (int i = 0; i < splitDate.Length; i++)
                    {
                        DateTime dateFormat = DateTime.ParseExact(splitDate[i].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

                        string query =
                        "IF NOT EXISTS (SELECT 1 FROM UserTempShift WHERE UserEnrollNumber = @UserEnrollNumber AND convert(varchar, Date, 112) = '" + splitDate[i].ToString() + "')" +
                        " BEGIN INSERT INTO UserTempShift(UserEnrollNumber, Date, Shift1, CreateAcc, UpdateAcc, CreateTime, UpdateTime) VALUES (@UserEnrollNumber, @Date, @Shift1, @CreateAcc, @UpdateAcc, @CreateTime, @UpdateTime) END" +
                        " ELSE BEGIN UPDATE UserTempShift SET Shift1 = @Shift1, UpdateTime = @UpdateTime, UpdateAcc = @UpdateAcc WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date END";

                        using (var sCmd = new SqlCommand(query, sConnection))
                        {
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                            sCmd.Parameters.Add("@Date", SqlDbType.Date).Value = dateFormat;
                            sCmd.Parameters.Add("@Shift1", SqlDbType.Int).Value = obj.Shift1;
                            sCmd.Parameters.Add("@CreateAcc", SqlDbType.VarChar).Value = obj.CreateAcc;
                            sCmd.Parameters.Add("@UpdateAcc", SqlDbType.VarChar).Value = obj.UpdateAcc;
                            sCmd.Parameters.Add("@CreateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                            sCmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);

                            rowAffected += sCmd.ExecuteNonQuery();
                        }
                    }
                }

                if (rowAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool DanhSachCa_AddRequestCaMoi(string connectionString, UserTempShift obj)
        {
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                var rowAffected = 0;

                var splitDate = obj.DateString.Split(',');

                for (int i = 0; i < splitDate.Length; i++)
                {
                    DateTime dateFormat = DateTime.ParseExact(splitDate[i].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

                    string query =
                    "IF NOT EXISTS (SELECT 1 FROM UserTempShiftR WHERE UserEnrollNumber = @UserEnrollNumber AND convert(varchar, Date, 112) = '" + splitDate[i].ToString() + "')" +
                    " BEGIN INSERT INTO UserTempShiftR(UserEnrollNumber, Date, Shift1, Status, CreateAcc, UpdateAcc, CreateTime, UpdateTime) VALUES (@UserEnrollNumber, @Date, @Shift1, @Status, @CreateAcc, @UpdateAcc, @CreateTime, @UpdateTime) END" +
                    " ELSE BEGIN UPDATE UserTempShiftR SET Shift1 = @Shift1, Status = @Status, UpdateTime = @UpdateTime, UpdateAcc = @UpdateAcc WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date END";

                    using (var sCmd = new SqlCommand(query, sConnection))
                    {
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                        sCmd.Parameters.Add("@Date", SqlDbType.Date).Value = dateFormat;
                        sCmd.Parameters.Add("@Shift1", SqlDbType.Int).Value = obj.Shift1;
                        sCmd.Parameters.Add("@Status", SqlDbType.Int).Value = obj.Status;
                        sCmd.Parameters.Add("@CreateAcc", SqlDbType.VarChar).Value = obj.CreateAcc;
                        sCmd.Parameters.Add("@UpdateAcc", SqlDbType.VarChar).Value = obj.UpdateAcc;
                        sCmd.Parameters.Add("@CreateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                        sCmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);

                        rowAffected += sCmd.ExecuteNonQuery();
                    }
                }
                if (rowAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool DanhSachCa_UpdateCaMoi(string connectionString, UserTempShift obj)
        {
            string query = "UPDATE UserTempShift SET Shift1 = @Shift1, UpdateTime = @UpdateTime, UpdateAcc = @UpdateAcc WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    sCmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    sCmd.Parameters.Add("@Shift1", SqlDbType.Int).Value = obj.Shift1;
                    sCmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                    sCmd.Parameters.Add("@UpdateAcc", SqlDbType.VarChar).Value = obj.UpdateAcc;

                    var o = sCmd.ExecuteNonQuery();

                    if (o > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        public bool Xoa_KhaiBaoCa_Phu(string connectionString, string user, string date)
        {
            int rowAffected = 0;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                string query = "" +
                    "DELETE UserTempShift WHERE UserEnrollNumber = " + user + " AND date = '" + DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + "'" +
                    " DELETE UserTempShiftR WHERE UserEnrollNumber = " + user + " AND date = '" + DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + "'";

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    rowAffected += sCmd.ExecuteNonQuery();
                }
                if (rowAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public IEnumerable<ShiftUnconfirmed> DanhSachCa_ChuaDuyet(string connectionString, DateTime from, DateTime to, int kp, string userid)
        {
            List<ShiftUnconfirmed> lst = new List<ShiftUnconfirmed>();
            string filter = "";
            if (!string.IsNullOrEmpty(userid))
            {
                string filterBonus = "";
                int returnRegex = 0;
                bool regex = int.TryParse(userid, out returnRegex);

                if (regex)
                {
                    filterBonus = "OR a.UserEnrollNumber = '" + userid + "'";
                }
                filter += " AND (UPPER(b.UserFullName) LIKE UPPER(N'%" + userid + "%') " + filterBonus + ")";
            }

            string query = @"SELECT a.UserEnrollNumber, b.UserFullCode, b.UserFullName, c.Description, a.Date, a.Shift1, d.ShiftCode, a.Status
                    FROM UserTempShiftR a
	                    LEFT JOIN UserInfExt b ON a.UserEnrollNumber = b.UserEnrollNumber
	                    LEFT JOIN RelationDept c ON b.PhongKhoa = c.ID
	                    LEFT JOIN Shifts d ON d.ShiftID = a.Shift1
                    WHERE Status = 0 AND a.Date >= @StartDate AND a.Date <= @EndDate AND (b.PhongKhoa = @KP) " + filter;

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = from;
                    scmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = to;
                    scmd.Parameters.Add("@KP", SqlDbType.Int).Value = kp;
                    scmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = userid;

                    var reader = scmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new ShiftUnconfirmed()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                KhoaPhong = reader["Description"].ToString(),
                                Date = DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy"),
                                ShiftID = int.Parse(reader["Shift1"].ToString()),
                                ShiftName = reader["ShiftCode"].ToString(),
                                Status1 = string.IsNullOrEmpty(reader["Status"].ToString()) ? 0 : int.Parse(reader["Status"].ToString())
                            });
                        }
                    }
                }
            }

            return lst;
        }
        public bool DanhSachCa_XuLyDuyetCa(string connectionString, List<ShiftUnconfirmed> lstObj)
        {
            int rowAffected = 0;

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                string query = @"
                    UPDATE UserTempShiftR SET Status = @Status WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date
                    IF NOT EXISTS (SELECT 1 FROM UserTempShift WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date)
	                    BEGIN
		                    INSERT INTO UserTempShift (UserEnrollNumber, Date, Shift1, CreateAcc, UpdateAcc, CreateTime, UpdateTime)
			                    SELECT UserEnrollNumber, Date, Shift1, CreateAcc, UpdateAcc, CreateTime, UpdateTime FROM UserTempShiftR WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date
	                    END
                    ELSE
                        BEGIN
                            UPDATE a SET a.Shift1 = b.Shift1 FROM UserTempShift a INNER JOIN UserTempShiftR b ON a.UserEnrollNumber = b.UserEnrollNumber AND a.Date = b.Date  WHERE a.UserEnrollNumber = @UserEnrollNumber AND a.Date = @Date
                        END
                ";

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    foreach (var item in lstObj)
                    {
                        scmd.Parameters.Clear();
                        scmd.Parameters.Add("@Status", SqlDbType.Int).Value = item.Status1;
                        scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = item.UserEnrollNumber;
                        scmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(item.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        rowAffected += scmd.ExecuteNonQuery();
                    }
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool DanhSachCa_XuLyHuyCa(string connectionString, List<ShiftUnconfirmed> lstObj)
        {
            int rowAffected = 0;

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                string query = @"UPDATE UserTempShiftR SET Status = @Status WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    foreach (var item in lstObj)
                    {
                        scmd.Parameters.Clear();
                        scmd.Parameters.Add("@Status", SqlDbType.Int).Value = item.Status1;
                        scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = item.UserEnrollNumber;
                        scmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(item.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        rowAffected += scmd.ExecuteNonQuery();
                    }
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
        public IEnumerable<UserWStats> DanhSachTGLV_ChuaDuyet(string connectionString, DateTime from, DateTime to, int kp, int userid)
        {
            List<UserWStats> lst = new List<UserWStats>();

            string query = @"SELECT a.UserEnrollNumber, b.UserFullCode, b.UserFullName, c.Description as KhoaPhong, a.Date, a.WStats1, d.Code as WStats1Code, a.WStats2, e.Code as WStats2Code, a.WStats3, f.Code as WStats3Code, a.W1Stats, a.W2Stats, a.W3Stats
                        FROM UserWStats a
	                        LEFT JOIN UserInfExt b ON a.UserEnrollNumber = b.UserEnrollNumber
	                        LEFT JOIN RelationDept c ON b.PhongKhoa = c.ID
                            LEFT JOIN [WorkStatsType] d ON d.ID = a.WStats1
	                        LEFT JOIN [WorkStatsType] e ON e.ID = a.WStats2
	                        LEFT JOIN [WorkStatsType] f ON f.ID = a.WStats3
                        WHERE ((a.WStats1 IS NOT NULL AND a.W1Stats = 1) OR (a.WStats2 IS NOT NULL AND a.W2Stats = 1) OR (a.WStats3 IS NOT NULL AND a.W3Stats = 1)) AND a.Date >= @StartDate AND a.Date <= @EndDate AND b.PhongKhoa = @KP AND (@User = 0 OR a.UserEnrollNumber = @User)";

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = from;
                    scmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = to;
                    scmd.Parameters.Add("@KP", SqlDbType.Int).Value = kp;
                    scmd.Parameters.Add("@User", SqlDbType.Int).Value = userid;

                    var reader = scmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new UserWStats()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                KhoaPhong = reader["KhoaPhong"].ToString(),
                                Date = DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy"),
                                WStats1 = reader["WStats1"].ToString(),
                                WStats2 = reader["WStats2"].ToString(),
                                WStats3 = reader["WStats3"].ToString(),
                                WStats1Code = reader["WStats1Code"].ToString(),
                                WStats2Code = reader["WStats2Code"].ToString(),
                                WStats3Code = reader["WStats3Code"].ToString(),
                                W1Stats = string.IsNullOrEmpty(reader["W1Stats"].ToString()) ? 0 : int.Parse(reader["W1Stats"].ToString()),
                                W2Stats = string.IsNullOrEmpty(reader["W2Stats"].ToString()) ? 0 : int.Parse(reader["W2Stats"].ToString()),
                                W3Stats = string.IsNullOrEmpty(reader["W3Stats"].ToString()) ? 0 : int.Parse(reader["W3Stats"].ToString())
                            });
                        }
                    }
                }
            }

            return lst;
        }
        public bool DanhSachTGLV_XuLyDuyetTGLV(string connectionString, List<UserWStats> lstObj)
        {
            int rowAffected = 0;

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                foreach (var item in lstObj)
                {
                    string str = "";

                    if (item.W1Stats > 0)
                        str += "W1Stats = " + item.W1Stats + ",";
                    if (item.W2Stats > 0)
                        str += "W2Stats = " + item.W2Stats + ",";
                    if (item.W3Stats > 0)
                        str += "W3Stats = " + item.W3Stats + ",";

                    string query = @"UPDATE UserWStats SET " + str.Substring(0, str.Length - 1) + " WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";

                    using (var scmd = new SqlCommand(query, sconnection))
                    {
                        scmd.Parameters.Clear();
                        scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = item.UserEnrollNumber;
                        scmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(item.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        rowAffected += scmd.ExecuteNonQuery();
                    }
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }

        #endregion XỬ LÝ DANH SÁCH CA.

        #region XỬ LÝ KHAI BÁO LỊCH TRÌNH.

        public IEnumerable<Schedule> LichTrinh_Dropdownlist(string connectionString)
        {
            List<Schedule> lst = new List<Schedule>();

            string query = @"SELECT m.SchID, CAST(n.SchName AS NVARCHAR(500)) as SchName, Ca AS MoTaCa FROM
	                    (Select SchID, Ca =  Stuff((Select ', '+ '[' + CAST(y.DayID as varchar(1)) + ' | ' + y.ShiftCode + ']' from
	                    (SELECT b.SchID, b.SchName,c.DayID, c.ShiftID ,d.ShiftCode
	                     FROM Schedule b LEFT JOIN WSchedules c ON b.SchID = c.SchID
	                     LEFT JOIN Shifts d ON c.ShiftID = d.ShiftID) y
	                    where y.SchID = x.SchID for xml path('')),1,2,'')
                        from
	                    (SELECT b.SchID, b.SchName,c.DayID, c.ShiftID ,d.ShiftCode
	                     FROM Schedule b
	                     LEFT JOIN WSchedules c ON b.SchID = c.SchID
	                     LEFT JOIN Shifts d ON c.ShiftID = d.ShiftID) x group by SchID) m
		                    LEFT JOIN Schedule n ON m.SchID = n.SchID
	                    WHERE n.Display = 1";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(new Schedule()
                        {
                            SchID = int.Parse(reader["SchID"].ToString()),
                            SchName = reader["SchName"].ToString(),
                            MoTaCa = reader["MoTaCa"].ToString()
                        });
                    }
                }
            }

            return lst;
        }
        public bool LichTrinh_CapNhat(string connectionString, List<UserInfo> objs)
        {
            int rowAffected = 0;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                foreach (var obj in objs)
                {
                    string query = "UPDATE UserInfo SET SchID = @SchID WHERE UserEnrollNumber = @UserEnrollNumber";

                    using (var sCmd = new SqlCommand(query, sConnection))
                    {
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                        sCmd.Parameters.Add("@SchID", SqlDbType.Int).Value = obj.SchID;

                        rowAffected += sCmd.ExecuteNonQuery();
                    }
                }

                if (rowAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }

        #endregion XỬ LÝ KHAI BÁO LỊCH TRÌNH.

        #region XỬ LÝ KHAI BÁO THỜI GIAN LÀM VIỆC.

        public string TGLV_KiemTraTrung(string connectionString, UserWStats obj)
        {
            string query = "SELECT Date FROM UserWStats WHERE 1 = 1 AND UserEnrollNumber = @UserEnrollNumber AND convert(varchar, Date, 112) IN (" + obj.DateString + ")";

            string _result = "";
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;

                    var reader = sCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            _result += DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy") + "\n";
                        }
                    }
                    return _result;
                }
            }
        }
        public UserWStats TGLV_ThongTin(string connectionString, UserWStats obj)
        {
            string query = @"select UserEnrollNumber, Date, WStats1, b.Des AS DesW1, WStats2, c.Des AS DesW2, WStats3, d.Des AS DesW3, W1Stats, W2Stats, W3Stats, CreateAcc, UpdateAcc, CreateTime, UpdateTime
                FROM UserWStats a
                    LEFT JOIN WorkStatsType b ON a.WStats1 IS NOT NULL AND a.WStats1 = b.ID
	                LEFT JOIN WorkStatsType c ON a.WStats2 IS NOT NULL AND a.WStats2 = c.ID
	                LEFT JOIN WorkStatsType d ON a.WStats3 IS NOT NULL AND a.WStats3 = d.ID
                WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";
            UserWStats o = new UserWStats();
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    sCmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var reader = sCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            o = new UserWStats()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                Date = DateTime.Parse(reader["Date"].ToString()).ToString(),
                                WStats1 = reader["WStats1"].ToString(),
                                WStats2 = reader["WStats2"].ToString(),
                                WStats3 = reader["WStats3"].ToString(),
                                S1 = reader["DesW1"].ToString(),
                                S2 = reader["DesW2"].ToString(),
                                S3 = reader["DesW3"].ToString(),
                                W1Stats = string.IsNullOrEmpty(reader["W1Stats"].ToString()) ? 0 : int.Parse(reader["W1Stats"].ToString()),
                                W2Stats = string.IsNullOrEmpty(reader["W2Stats"].ToString()) ? 0 : int.Parse(reader["W2Stats"].ToString()),
                                W3Stats = string.IsNullOrEmpty(reader["W3Stats"].ToString()) ? 0 : int.Parse(reader["W3Stats"].ToString()),
                                CreateAcc = reader["CreateAcc"].ToString(),
                                UpdateAcc = reader["UpdateAcc"].ToString(),
                                CreateTime = string.IsNullOrEmpty(reader["CreateTime"].ToString()) ? new DateTime() : DateTime.Parse(reader["CreateTime"].ToString()),
                                UpdateTime = string.IsNullOrEmpty(reader["UpdateTime"].ToString()) ? new DateTime() : DateTime.Parse(reader["UpdateTime"].ToString())
                            };
                        }
                    }
                    else
                    {
                        o.UserEnrollNumber = obj.UserEnrollNumber;
                        o.Date = obj.Date;
                    }
                    return o;
                }
            }
        }
        public IEnumerable<WorkStatsType> TGLV_DDL(string connectionString, int type = 0)
        {
            List<WorkStatsType> lst = new List<WorkStatsType>();

            string query = @"SELECT ID, Code, Des FROM WorkStatsType WHERE Type = " + type;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(new WorkStatsType()
                        {
                            ID = reader["ID"].ToString(),
                            Des = reader["Des"].ToString() + " (" + reader["Code"].ToString() + ")"
                        });
                    }
                }
            }

            return lst;
        }

        public Dictionary<int, string> TGLV_TTType_Ops(string connectionString, int department = 0)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            string query = $@"SELECT ID,Description FROM db_core.ReasonOvertimes WHERE Type = 'TT' AND DepartmentID = {department}";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        dict.Add(int.Parse(reader["ID"].ToString()), reader["Description"].ToString());
                    }
                }
            }

            return dict;
        }

        public Dictionary<int, string> TGLV_LTGType_Ops(string connectionString, int department = 0)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            string query = $@"SELECT ID,Description FROM db_core.ReasonOvertimes WHERE Type = 'LTG' AND DepartmentID= {department}";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    var reader = sCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        dict.Add(int.Parse(reader["ID"].ToString()), reader["Description"].ToString());
                    }
                }
            }

            return dict;
        }

        public bool Update_TGLV(string connectionString, List<UserWStats> objs)
        {
            int rowAffected = 0;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                foreach (var obj in objs)
                {
                    string updateStr = "";

                    if (obj.W1Stats != 0)
                    {
                        updateStr += "WStats1 = @WStats1, W1Stats = @W1Stats,";
                    }
                    if (obj.W2Stats != 0)
                    {
                        updateStr += "WStats2 = @WStats2, W2Stats = @W2Stats,";
                    }
                    if (obj.W3Stats != 0)
                    {
                        updateStr += "WStats3 = @WStats3, W3Stats = @W3Stats,";
                    }
                    string query = "IF NOT EXISTS (SELECT 1 FROM UserWStats WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date)" +
                    " BEGIN INSERT INTO UserWStats(UserEnrollNumber, Date, WStats1, WStats2, WStats3, W1Stats, W2Stats, W3Stats, Note, CreateAcc, UpdateAcc, CreateTime, UpdateTime, LTGType, TTType) VALUES (@UserEnrollNumber, @Date, @WStats1, @WStats2, @WStats3, @W1Stats, @W2Stats, @W3Stats, @Note, @CreateAcc, @UpdateAcc, @CreateTime, @UpdateTime, @LTGType, @TTType) END" +
                    " ELSE" +
                    " BEGIN UPDATE UserWStats SET " + updateStr + " UpdateAcc = @UpdateAcc, UpdateTime = @UpdateTime WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date END";

                    using (var sCmd = new SqlCommand(query, sConnection))
                    {
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                        sCmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = DateTime.ParseExact(obj.Date, "yyyyMMdd", CultureInfo.InvariantCulture);

                        if (string.IsNullOrEmpty(obj.WStats1))
                            sCmd.Parameters.Add("@WStats1", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@WStats1", SqlDbType.VarChar).Value = obj.WStats1;

                        if (string.IsNullOrEmpty(obj.WStats2))
                            sCmd.Parameters.Add("@WStats2", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@WStats2", SqlDbType.VarChar).Value = obj.WStats2;

                        if (string.IsNullOrEmpty(obj.WStats3))
                            sCmd.Parameters.Add("@WStats3", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@WStats3", SqlDbType.VarChar).Value = obj.WStats3;

                        if (obj.W1Stats == 0)
                            sCmd.Parameters.Add("@W1Stats", SqlDbType.Int).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@W1Stats", SqlDbType.Int).Value = obj.W1Stats;

                        if (obj.W2Stats == 0)
                            sCmd.Parameters.Add("@W2Stats", SqlDbType.Int).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@W2Stats", SqlDbType.Int).Value = obj.W2Stats;

                        if (obj.W3Stats == 0)
                            sCmd.Parameters.Add("@W3Stats", SqlDbType.Int).Value = DBNull.Value;
                        else
                            sCmd.Parameters.Add("@W3Stats", SqlDbType.Int).Value = obj.W3Stats;

                        sCmd.Parameters.Add("@LTGType", SqlDbType.NVarChar).Value = obj.LTGType ?? "";
                        sCmd.Parameters.Add("@TTType", SqlDbType.NVarChar).Value = obj.TTType ?? "";

                        sCmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = obj.Note ?? "";
                        sCmd.Parameters.Add("@CreateAcc", SqlDbType.VarChar).Value = obj.CreateAcc;
                        sCmd.Parameters.Add("@UpdateAcc", SqlDbType.VarChar).Value = obj.UpdateAcc;
                        sCmd.Parameters.Add("@CreateTime", SqlDbType.DateTime).Value = obj.CreateTime;
                        sCmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime).Value = obj.UpdateTime;
                        rowAffected += sCmd.ExecuteNonQuery();
                    }
                }
                if (rowAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public IEnumerable<UserWStats> TGLV_View(string connectionString, string fromDate, string toDate, int userID, int makp, int ot, int tt)
        {
            List<UserWStats> lst = new List<UserWStats>();
            DataTable data = new DataTable();
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand("sp_TGLV_View", sConnection))
                {
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.CommandTimeout = 0;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@KP1", SqlDbType.Int).Value = makp;
                    sCmd.Parameters.Add("@MaNV1", SqlDbType.Int).Value = userID;
                    sCmd.Parameters.Add("@NgayBD1", SqlDbType.VarChar).Value = fromDate;
                    sCmd.Parameters.Add("@NgayKT1", SqlDbType.VarChar).Value = toDate;
                    sCmd.Parameters.Add("@DuyetOT1", SqlDbType.Int).Value = ot;
                    sCmd.Parameters.Add("@DuyetTT1", SqlDbType.Int).Value = tt;

                    DataTable dtable = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(dtable);

                    foreach (DataRow row in dtable.Rows)
                    {
                        var datedb = DateTime.Parse(row["Date"].ToString());

                        if (int.Parse(datedb.ToString("yyyyMMdd")) >= int.Parse(fromDate) && int.Parse(datedb.ToString("yyyyMMdd")) <= int.Parse(toDate))
                        {
                            lst.Add(new UserWStats()
                            {
                                UserEnrollNumber = int.Parse(row["UserEnrollNumber"].ToString()),
                                UserFullCode = row["UserFullCode"].ToString(),
                                UserFullName = row["UserFullName"].ToString(),
                                KhoaPhong = row["Description"].ToString(),
                                Date = datedb.ToString("dd/MM/yyyy"),
                                S1 = string.IsNullOrEmpty(row["MuSo"].ToString()) ? "" : row["MuSo"].ToString(),
                                S2 = string.IsNullOrEmpty(row["OT"].ToString()) ? "" : row["OT"].ToString(),
                                S3 = string.IsNullOrEmpty(row["TT"].ToString()) ? "" : row["TT"].ToString(),
                                TTXuLyMuSo = string.IsNullOrEmpty(row["TTXuLyMuSo"].ToString()) ? "" : row["TTXuLyMuSo"].ToString(),
                                TTXuLyOT = string.IsNullOrEmpty(row["TTXuLyOT"].ToString()) ? "" : row["TTXuLyOT"].ToString(),
                                TTXuLyTT = string.IsNullOrEmpty(row["TTXuLyTT"].ToString()) ? "" : row["TTXuLyTT"].ToString(),
                                MuSoStats = string.IsNullOrEmpty(row["MuSoStats"].ToString()) ? 0 : int.Parse(row["MuSoStats"].ToString()),
                                OTStats = string.IsNullOrEmpty(row["OTStats"].ToString()) ? 0 : int.Parse(row["OTStats"].ToString()),
                                TTStats = string.IsNullOrEmpty(row["TTStats"].ToString()) ? 0 : int.Parse(row["TTStats"].ToString()),
                                VC = string.IsNullOrEmpty(row["VC"].ToString()) ? 0 : int.Parse(row["VC"].ToString()),
                                In1 = string.IsNullOrEmpty(row["In1"].ToString()) ? "" : row["In1"].ToString(),
                                Onduty = string.IsNullOrEmpty(row["Onduty"].ToString()) ? "" : row["Onduty"].ToString(),
                                Out1 = string.IsNullOrEmpty(row["Out1"].ToString()) ? "" : row["Out1"].ToString(),
                                Offduty = string.IsNullOrEmpty(row["Offduty"].ToString()) ? "" : row["Offduty"].ToString(),
                                Note = string.IsNullOrEmpty(row["LyDo"].ToString()) ? "" : row["LyDo"].ToString(),
                                TTType = string.IsNullOrEmpty(row["TTType"].ToString()) ? "" : row["TTType"].ToString(),
                                LTGType = string.IsNullOrEmpty(row["LTGType"].ToString()) ? "" : row["LTGType"].ToString()
                            });
                        }
                    }
                }
            }
            return lst;
        }
        public bool Clear_TGLV(string connectionString, UserWStats obj)
        {
            string query = "DELETE UserWStats WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    sCmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    int rowAffected = sCmd.ExecuteNonQuery();

                    if (rowAffected > 0)
                        return true;
                    return false;
                }
            }
        }

        #endregion XỬ LÝ KHAI BÁO THỜI GIAN LÀM VIỆC.

        #region XỬ LÝ CA PHỤ

        public bool Update_GioiHanCa(string connectionString, List<UserInfExt> objs)
        {
            int rowAffected = 0;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                foreach (var obj in objs)
                {
                    string query = "UPDATE UserInfExt SET CaPhu = @CaPhu WHERE UserEnrollNumber = @UserEnrollNumber";

                    using (var sCmd = new SqlCommand(query, sConnection))
                    {
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                        sCmd.Parameters.Add("@CaPhu", SqlDbType.VarChar).Value = obj.CaPhu;

                        rowAffected += sCmd.ExecuteNonQuery();
                    }
                }

                if (rowAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }

        #endregion XỬ LÝ CA PHỤ

        #region XỬ LÝ ĐỀ XUẤT SỬA CÔNG

        public bool Update_DuyetGiaiTrinh(string connectionString, string auth, int userid, DateTime date, string note, int status)
        {
            string plus = "";

            if (auth == "HCNS_Manager")
            {
                plus = "DeptNote1 = N'" + note + "'";
            }
            if (auth == "HCNS_Admin")
            {
                plus = "HRNote1 = N'" + note + "'";
            }

            string query = "UPDATE UserWTRequests SET Status = @Status, " + plus + ", UpdateAcc = @UpdateAcc, UpdateTime = @UpdateTime WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";
            int rowAffected = 0;
            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = userid;
                    scmd.Parameters.Add("@Date", SqlDbType.Date).Value = date;
                    scmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                    scmd.Parameters.Add("@UpdateAcc", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
                    scmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool Update_HuyDuyetGiaiTrinh(string connectionString, int userid, DateTime date, string col)
        {
            string query = "UPDATE UserWStats SET " + col + " = 1, UpdateAcc = @UpdateAcc, UpdateTime = @UpdateTime WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";
            int rowAffected = 0;
            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = userid;
                    scmd.Parameters.Add("@Date", SqlDbType.Date).Value = date;
                    scmd.Parameters.Add("@UpdateAcc", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
                    scmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool Update_XoaGiaiTrinh(string connectionString, int userid, DateTime date, string col)
        {
            int rowAffected = 0;
            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand("sp_Del_TGVL", sconnection))
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = userid.ToString();
                    scmd.Parameters.Add("@Date", SqlDbType.VarChar).Value = date.ToString("yyyyMMdd");
                    scmd.Parameters.Add("@ColN", SqlDbType.VarChar).Value = col;
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool Update_DuyetGiaiTrinh(string connectionString, List<UserWStats> objs)
        {
            int rowAffected = 0;

            foreach (var item in objs)
            {
                string query = "UPDATE UserWStats SET " + item.Col + " = 2, UpdateAcc = @UpdateAcc, UpdateTime = @UpdateTime WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";

                using (var sconnection = new SqlConnection(connectionString))
                {
                    if (sconnection.State == ConnectionState.Closed)
                        sconnection.Open();

                    using (var scmd = new SqlCommand(query, sconnection))
                    {
                        scmd.Parameters.Clear();
                        scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = item.UserEnrollNumber;
                        scmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(item.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        scmd.Parameters.Add("@UpdateAcc", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
                        scmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                        rowAffected = scmd.ExecuteNonQuery();
                    }
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool Update_DuyetNhanhGiaiTrinh(string connectionString, string auth, string dateString)
        {
            var spl = dateString.Split(',');

            int rowAffected = 0;

            for (int i = 0; i < spl.Length; i++)
            {
                var item = spl[i].ToString().Split('.');
                var date = DateTime.ParseExact(item[0].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var userid = int.Parse(item[1].ToString());
                int status = 0;

                if (auth == "HCNS_Manager") status = 1;
                if (auth == "HCNS_Admin") status = 2;

                string query = "UPDATE UserWTRequests SET Status = @Status, UpdateAcc = @UpdateAcc, UpdateTime = @UpdateTime WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";

                using (var sconnection = new SqlConnection(connectionString))
                {
                    if (sconnection.State == ConnectionState.Closed)
                        sconnection.Open();

                    using (var scmd = new SqlCommand(query, sconnection))
                    {
                        scmd.Parameters.Clear();
                        scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = userid;
                        scmd.Parameters.Add("@Date", SqlDbType.Date).Value = date;
                        scmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                        scmd.Parameters.Add("@UpdateAcc", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
                        scmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                        rowAffected = scmd.ExecuteNonQuery();
                    }
                }
            }
            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool Update_HuyDuyetNhanhGiaiTrinh(string connectionString, string auth, string dateString)
        {
            var spl = dateString.Split(',');

            int rowAffected = 0;

            for (int i = 0; i < spl.Length; i++)
            {
                var item = spl[i].ToString().Split('.');
                var date = DateTime.ParseExact(item[0].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var userid = int.Parse(item[1].ToString());
                int status = 0;

                if (auth == "HCNS_Manager") status = -1;
                if (auth == "HCNS_Admin") status = -2;

                string query = "UPDATE UserWTRequests SET Status = @Status, UpdateAcc = @UpdateAcc, UpdateTime = @UpdateTime WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";

                using (var sconnection = new SqlConnection(connectionString))
                {
                    if (sconnection.State == ConnectionState.Closed)
                        sconnection.Open();

                    using (var scmd = new SqlCommand(query, sconnection))
                    {
                        scmd.Parameters.Clear();
                        scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = userid;
                        scmd.Parameters.Add("@Date", SqlDbType.Date).Value = date;
                        scmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                        scmd.Parameters.Add("@UpdateAcc", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
                        scmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                        rowAffected = scmd.ExecuteNonQuery();
                    }
                }
            }
            if (rowAffected > 0)
                return true;
            return false;
        }
        public UserWTRequests GiaiTrinhSuaLoi_ThongTin(string connectionString, int userid, DateTime date)
        {
            UserWTRequests obj = new UserWTRequests();

            string query = "SELECT UserEnrollNumber, CONVERT(VARCHAR, Date, 103) AS Date, Fix1, UserNote1 FROM UserWTRequests WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = userid;
                    scmd.Parameters.Add("@Date", SqlDbType.Date).Value = date;

                    var reader = scmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            obj.UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString());
                            obj.Date = reader["Date"].ToString();
                            obj.Fix1 = int.Parse(reader["Fix1"].ToString());
                            obj.UserNote1 = reader["UserNote1"].ToString();
                        }
                    }
                }
            }
            return obj;
        }
        public Dictionary<int, string> DS_LyDo_DeXuat(string connectionString)
        {
            Dictionary<int, string> dics = new Dictionary<int, string>();

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand("SELECT ID, Des FROM TimeFType", sconnection))
                {
                    var reader = scmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dics.Add(int.Parse(reader["ID"].ToString()), reader["Des"].ToString());
                        }
                    }
                }
            }

            return dics;
        }
        public int KiemTra_DuyetDeXuat(string connectionString, UserWTRequests obj)
        {
            string query = "SELECT HRStats1 FROM UserWTRequests WHERE Date = @Date AND UserEnrollNumber = @UserEnrollNumber";

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    var o = scmd.ExecuteScalar();

                    return (int)o;
                }
            }
        }
        public UserWTRequests ThongTinDeXuat(string connectionString, int user, string date)
        {
            string query = "SELECT UserEnrollNumber, Date, Fix1, b.Des AS Fix1Des, In1F, Out1F, Status, HRNote1, DeptNote1, UserNote1 FROM UserWTRequests a LEFT JOIN TimeFType b ON a.Fix1 =  b.ID WHERE Date = @Date AND UserEnrollNumber = @UserEnrollNumber";

            UserWTRequests obj = new UserWTRequests();

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = user;

                    var reader = scmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            obj.UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString());
                            obj.Date = DateTime.Parse(reader["Date"].ToString()).ToString("dd/MM/yyyy");
                            obj.Fix1 = int.Parse(reader["Fix1"].ToString());
                            obj.Fix1Des = reader["Fix1Des"].ToString();
                            obj.In1F = string.IsNullOrEmpty(reader["In1F"].ToString()) ? "" : DateTime.Parse(reader["In1F"].ToString()).ToString("HH:mm");
                            obj.Out1F = string.IsNullOrEmpty(reader["Out1F"].ToString()) ? "" : DateTime.Parse(reader["Out1F"].ToString()).ToString("HH:mm");
                            obj.Status = string.IsNullOrEmpty(reader["Status"].ToString()) ? 0 : int.Parse(reader["Status"].ToString());
                            obj.HRNote1 = reader["HRNote1"].ToString();
                            obj.DeptNote1 = reader["DeptNote1"].ToString();
                            obj.UserNote1 = reader["UserNote1"].ToString();
                        }
                    }
                    else
                    {
                        obj.UserEnrollNumber = user;
                        obj.Date = date;
                    }
                }
            }

            return obj;
        }
        public bool Update_Lydo_DeXuat_User(string connectionString, UserWTRequests obj)
        {
            string query = "IF NOT EXISTS (SELECT 1 FROM UserWTRequests WHERE Date = @Date AND UserEnrollNumber = @UserEnrollNumber)" +
                " BEGIN INSERT INTO UserWTRequests(UserEnrollNumber, Date, Fix1, In1F, Out1F, Status, UserNote1, CreateAcc, UpdateAcc, CreateTime, UpdateTime) VALUES (@UserEnrollNumber, @Date, @Fix1, @In1F, @Out1F, @Status, @UserNote1, @CreateAcc, @UpdateAcc, @CreateTime, @UpdateTime) END" +
                " ELSE " +
                " BEGIN UPDATE UserWTRequests SET Fix1 = @Fix1, In1F = @In1F, Out1F = @Out1F, UserNote1 = @UserNote1, UpdateAcc = @UpdateAcc, UpdateTime = @UpdateTime, Status = @Status WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date AND [Status] <= 0 END";

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    scmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    scmd.Parameters.Add("@Fix1", SqlDbType.Int).Value = obj.Fix1;
                    if (string.IsNullOrEmpty(obj.In1F))
                    {
                        scmd.Parameters.Add("@In1F", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        scmd.Parameters.Add("@In1F", SqlDbType.DateTime).Value = DateTime.ParseExact(obj.Date + " " + obj.In1F, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                    }
                    if (string.IsNullOrEmpty(obj.Out1F))
                    {
                        scmd.Parameters.Add("@Out1F", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        scmd.Parameters.Add("@Out1F", SqlDbType.DateTime).Value = DateTime.ParseExact(obj.Date + " " + obj.Out1F, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture); ;
                    }
                    scmd.Parameters.Add("@UserNote1", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(obj.UserNote1) ? "" : obj.UserNote1;
                    scmd.Parameters.Add("@CreateAcc", SqlDbType.NVarChar).Value = System.Web.HttpContext.Current.User.Identity.Name;
                    scmd.Parameters.Add("@UpdateAcc", SqlDbType.NVarChar).Value = System.Web.HttpContext.Current.User.Identity.Name;
                    scmd.Parameters.Add("@CreateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                    scmd.Parameters.Add("@UpdateTime", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                    scmd.Parameters.Add("@Status", SqlDbType.SmallInt).Value = obj.Status;

                    int rowAffected = scmd.ExecuteNonQuery();

                    if (rowAffected > 0)
                        return true;
                    return false;
                }
            }
        }

        public bool Delete_DeXuatLoi(string connectionString, int userid, DateTime date)
        {
            string query = "DELETE UserWTRequests WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date AND [Status] = 0";
            int rowAffected = 0;
            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = userid;
                    scmd.Parameters.Add("@Date", SqlDbType.Date).Value = date;
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            if (rowAffected > 0)
                return true;
            return false;
        }

        public bool Delete_DeXuatCa(string connectionString, int userid, DateTime date)
        {
            string query = "DELETE UserTempShift WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date";
            int rowAffected = 0;
            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = userid;
                    scmd.Parameters.Add("@Date", SqlDbType.Date).Value = date;
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool Delete_DeXuatCaUser(string connectionString, int userid, DateTime date)
        {
            string query = "DELETE UserTempShiftR WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date AND [Status] = 0";
            int rowAffected = 0;
            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = userid;
                    scmd.Parameters.Add("@Date", SqlDbType.Date).Value = date;
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            if (rowAffected > 0)
                return true;
            return false;
        }

        public bool Delete_DeXuatKBVang(string connectionString, int userid, DateTime date)
        {
            string query = "DELETE AbsentR WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date AND [Status] = 0";
            int rowAffected = 0;
            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = userid;
                    scmd.Parameters.Add("@Date", SqlDbType.Date).Value = date;
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            if (rowAffected > 0)
                return true;
            return false;
        }

        public DataTable DanhSachDeXuat(string connectionString, DateTime fromDate, DateTime toDate, string kp, int fix, string userID, int status)
        {
            string filter = "";

            if (!string.IsNullOrEmpty(kp) && kp != "-1")
            {
                filter += " AND b.PhongKhoa = '" + kp + "'";
            }
            if (fix != 0)
            {
                filter += " AND a.Fix1 = " + fix;
            }
            if (status > -1)
            {
                filter += " AND a.Status = " + status;
            }
            if (!string.IsNullOrEmpty(userID))
            {
                string filterBonus = "";
                int returnRegex = 0;
                bool regex = int.TryParse(userID, out returnRegex);
                if (regex)
                {
                    filterBonus = "OR a.UserEnrollNumber = '" + userID + "'";
                }
                filter += " AND (UPPER(b.UserFullName) LIKE UPPER(N'%" + userID + "%') " + filterBonus + ")";
            }

            string query = @"SELECT a.UserEnrollNumber, b.UserFullCode, b.UserFullName, c.Description, a.Date, d.Des, In1F, Out1F, Status, HRNote1, DeptNote1, UserNote1, CreateTime, UpdateTime FROM UserWTRequests a
                    LEFT JOIN UserInfExt b ON a.UserEnrollNumber = b.UserEnrollNumber
                    LEFT JOIN RelationDept c ON b.PhongKhoa = c.ID
                    LEFT JOIn TimeFType d ON d.ID = a.Fix1 WHERE 1 = 1 AND FORMAT(a.Date,'yyyyMMdd') >= '" + fromDate.ToString("yyyyMMdd") + "' AND FORMAT(a.Date,'yyyyMMdd') <= '" + toDate.ToString("yyyyMMdd") + "'" + filter;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sCmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
        public DataTable DanhSachDeXuat_Admin(string connectionString, DateTime fromDate, DateTime toDate, string kp, int userID)
        {
            string filter = "";

            if (!string.IsNullOrEmpty(kp))
            {
                filter += " AND b.PhongKhoa = '" + kp + "'";
            }
            if (userID > 0)
            {
                filter += " AND a.UserEnrollNumber = " + userID;
            }

            string query = @"SELECT a.UserEnrollNumber, b.UserFullCode, b.UserFullName, c.Description, a.Date, d.Des, In1F, Out1F, Status, HRNote1, DeptNote1, UserNote1, CreateTime, UpdateTime FROM UserWTRequests a
                    LEFT JOIN UserInfExt b ON a.UserEnrollNumber = b.UserEnrollNumber
                    LEFT JOIN RelationDept c ON b.PhongKhoa = c.ID
                    LEFT JOIn TimeFType d ON d.ID = a.Fix1 WHERE 1 = 1 AND Status > 0 AND a.Date >= '" + fromDate.ToShortDateString() + "' AND a.Date <= '" + toDate.ToShortDateString() + "'" + filter;

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand(query, sConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sCmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        public Absent KhaiBaoVang_ThongTin(string connectionString, int userid, string date)
        {
            string query = "SELECT UserEnrollNumber, UserFullCode, TimeDate, a.AbsentCode, b.AbsentDescription, Workingday, WorkingTime, AddedTime, Thang, Nam, LyDo, ForType, ShiftID, UserAccount FROM [Absent] a LEFT JOIN AbsentSymbol b ON a.AbsentCode = b.AbsentCode WHERE UserEnrollNumber = @UserEnrollNumber AND TimeDate = @TimeDate";

            Absent obj = new Absent();

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@TimeDate", SqlDbType.Date).Value = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = userid;

                    var reader = scmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            obj = new Absent()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                TimeDate = DateTime.Parse(reader["TimeDate"].ToString()).ToString("dd/MM/yyyy"),
                                AbsentCode = reader["AbsentCode"].ToString(),
                                AbsentDescription = reader["AbsentDescription"].ToString(),
                                UserAccount = reader["UserAccount"].ToString()
                            };
                        }
                    }
                    else
                    {
                        obj.UserEnrollNumber = userid;
                        obj.TimeDate = date;
                    }
                }

                return obj;
            }
        }
        public DataTable QLCong_Excel(string connectionString, string fromDateFormat, string toDateFormat, int userID, int makp, int qldd)
        {
            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand("sp_QLCong_Excel", sconn))
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = fromDateFormat;
                    scmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = toDateFormat;
                    scmd.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = userID == 0 ? "" : userID.ToString();
                    scmd.Parameters.Add("@KP", SqlDbType.VarChar).Value = makp == -1 ? "" : makp.ToString();
                    scmd.Parameters.Add("@QLDD", SqlDbType.Int).Value = qldd;
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(scmd);
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable Excel_DanhSachGiaiTrinh(string connectionString, Excel_GiaiTrinh obj)
        {
            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand("sp_ExcelGiaiTrinh", sconn))
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = obj.NGAY_BD;
                    scmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = obj.NGAY_KT;
                    scmd.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = obj.MANV;
                    scmd.Parameters.Add("@KP", SqlDbType.VarChar).Value = obj.KhoaPhong;
                    scmd.Parameters.Add("@TTXL", SqlDbType.VarChar).Value = obj.TTXL;

                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(scmd);
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
        public DataTable Excel_DanhSachTGLV(string connectionString, Excel_TGLV obj)
        {
            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                string storeName = "sp_Excel_PheDuyetLamThemGio";
                //string storeName = "sp_ExcelLamThemGio";

                using (var scmd = new SqlCommand(storeName, sconn))
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.CommandTimeout = 0;
                    scmd.Parameters.Clear();

                    //scmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = obj.NGAY_BD;
                    //scmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = obj.NGAY_KT;
                    //scmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = string.IsNullOrEmpty(obj.MANV) ? -1 : int.Parse(obj.MANV);
                    //scmd.Parameters.Add("@KP", SqlDbType.Int).Value = string.IsNullOrEmpty(obj.KhoaPhong) ? -1 : int.Parse(obj.KhoaPhong);
                    //scmd.Parameters.Add("@DuyetOT", SqlDbType.Int).Value = obj.DuyetOT;
                    //scmd.Parameters.Add("@DuyetTT", SqlDbType.Int).Value = obj.DuyetTT;

                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = obj.NGAY_BD;
                    scmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = obj.NGAY_KT;
                    scmd.Parameters.Add("@KP", SqlDbType.Int).Value = obj.KhoaPhong;
                    scmd.Parameters.Add("@PhanLoai", SqlDbType.Int).Value = obj.PhanLoai;
                    

                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(scmd);
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        #endregion XỬ LÝ ĐỀ XUẤT SỬA CÔNG

        #region QUẢN LÝ CÔNG

        public DataTable QLCong_View(string connectionString, string dateString, string fromDate, string toDate, int userID, int makp, int qldd)
        {
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand("sp_QLCong_View", sConnection))
                {
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@KP", SqlDbType.VarChar).Value = makp.ToString();
                    sCmd.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = userID == 0 ? "" : userID.ToString();
                    sCmd.Parameters.Add("@DateString", SqlDbType.VarChar).Value = dateString.Substring(0, dateString.Length - 1);
                    sCmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = fromDate;
                    sCmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = toDate;
                    sCmd.Parameters.Add("@QLDD ", SqlDbType.Int).Value = qldd; //1: Quản lý điều dưỡng; 0: Bình thường

                    SqlDataAdapter adapter = new SqlDataAdapter(sCmd);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }

        public DataTable QLCong_Detail(string connectionString, string fromDate, string toDate, int userID)
        {
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand("sp_QLCong_Detail", sConnection))
                {
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = userID.ToString();
                    sCmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = fromDate;
                    sCmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = toDate;

                    SqlDataAdapter adapter = new SqlDataAdapter(sCmd);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }

        public DataTable QLCong_Detail_Cell(string connectionString, string date, int userID)
        {
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCmd = new SqlCommand("sp_QLCong_Detail_Cell", sConnection))
                {
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = userID.ToString();
                    sCmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = date;
                    //sCmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = date;

                    SqlDataAdapter adapter = new SqlDataAdapter(sCmd);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }

        #endregion QUẢN LÝ CÔNG

        #region LÀM THÊM TÍNH TIỀN

        public IEnumerable<OTPayment> LamThemTT_DanhSach(string connectionString, OTPayment_Filter oFilter)
        {
            List<OTPayment> lst = new List<OTPayment>();

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand("sp_DKTTTienLTG_View", sconn))
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = oFilter.NgayBD.ToString("yyyyMMdd");
                    scmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = oFilter.NgayKT.ToString("yyyyMMdd");
                    scmd.Parameters.Add("@KP", SqlDbType.Int).Value = oFilter.MaKP;
                    scmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = oFilter.MaNV;
                    scmd.Parameters.Add("@Duyet", SqlDbType.Int).Value = oFilter.Duyet;

                    var reader = scmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new OTPayment()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = string.IsNullOrEmpty(reader["MaNV"].ToString()) ? "" : reader["MaNV"].ToString(),
                                UserFullName = string.IsNullOrEmpty(reader["HoTen"].ToString()) ? "" : reader["HoTen"].ToString(),
                                KhoaPhong = string.IsNullOrEmpty(reader["NoiChamCong"].ToString()) ? "" : reader["NoiChamCong"].ToString(),
                                Ngay = string.IsNullOrEmpty(reader["Ngay"].ToString()) ? "" : reader["Ngay"].ToString(),
                                Thu = string.IsNullOrEmpty(reader["Thu"].ToString()) ? "" : reader["Thu"].ToString(),
                                LyDo = string.IsNullOrEmpty(reader["LyDo"].ToString()) ? "" : reader["LyDo"].ToString(),
                                KieuTTLamThemGio = string.IsNullOrEmpty(reader["KieuTTLTG"].ToString()) ? "" : reader["KieuTTLTG"].ToString(),
                                ThoiGianBD = string.IsNullOrEmpty(reader["ST"].ToString()) ? "" : reader["ST"].ToString(),
                                ThoiGianKT = string.IsNullOrEmpty(reader["ET"].ToString()) ? "" : reader["ET"].ToString(),
                                TongLamThemGio = string.IsNullOrEmpty(reader["TT"].ToString()) ? "" : reader["TT"].ToString(),
                                CaLamViec = string.IsNullOrEmpty(reader["Ca"].ToString()) ? "" : reader["Ca"].ToString(),
                                GioVaoCa = string.IsNullOrEmpty(reader["VaoCa"].ToString()) ? "" : reader["VaoCa"].ToString(),
                                GioRaCa = string.IsNullOrEmpty(reader["RaCa"].ToString()) ? "" : reader["RaCa"].ToString(),
                                //MaThoiGianLamThem = string.IsNullOrEmpty(reader["HType"].ToString()) ? 0 : int.Parse(reader["HType"].ToString()),
                                TenThoiGianLamThem = string.IsNullOrEmpty(reader["TGLT"].ToString()) ? "" : reader["TGLT"].ToString(),
                                DaDuyet = string.IsNullOrEmpty(reader["Approve"].ToString()) ? 0 : int.Parse(reader["Approve"].ToString()),
                                HienThi = string.IsNullOrEmpty(reader["Display"].ToString()) ? 0 : int.Parse(reader["Display"].ToString()),
                                GoiY = string.IsNullOrEmpty(reader["Hint"].ToString()) ? "" : reader["Hint"].ToString(),
                                PhanLoai = string.IsNullOrEmpty(reader["Type"].ToString()) ? 0 : int.Parse(reader["Type"].ToString()),
                                MA = string.IsNullOrEmpty(reader["Ma"].ToString()) ? 0 : int.Parse(reader["Ma"].ToString()),
                                KyHieuDuyet = string.IsNullOrEmpty(reader["DeXuat"].ToString()) ? "" : reader["DeXuat"].ToString(),
                                OTPayTypes = LamThemTT_TypeDDL(connectionString, string.IsNullOrEmpty(reader["Hint"].ToString()) ? "" : reader["Hint"].ToString())
                            });
                        }
                    }
                }
            }

            return lst;
        }
        public DataTable LamThemTT_DanhSachExcel(string connectionString, OTPayment_Filter oFilter)
        {
            List<OTPayment> lst = new List<OTPayment>();
            DataTable dtable = new DataTable();
            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand("sp_DKTTTienLTG_View", sconn))
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = oFilter.NgayBD.ToString("yyyyMMdd");
                    scmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = oFilter.NgayKT.ToString("yyyyMMdd");
                    scmd.Parameters.Add("@KP", SqlDbType.Int).Value = oFilter.MaKP;
                    scmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = oFilter.MaNV;
                    scmd.Parameters.Add("@Duyet", SqlDbType.Int).Value = oFilter.Duyet;

                    SqlDataAdapter da = new SqlDataAdapter(scmd);
                    da.Fill(dtable);
                }

                //using (var scmd = new SqlCommand("sp_Excel_PheDuyetLamThemGio", sconn))
                //{
                //    scmd.CommandType = CommandType.StoredProcedure;
                //    scmd.Parameters.Clear();
                //    scmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = oFilter.NgayBD.ToString("yyyy-MM-dd");
                //    scmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = oFilter.NgayKT.ToString("yyyy-MM-dd");
                //    scmd.Parameters.Add("@KP", SqlDbType.Int).Value = oFilter.MaKP;

                //    SqlDataAdapter da = new SqlDataAdapter(scmd);
                //    da.Fill(dtable);
                //}
            }

            return dtable;
        }
        public Dictionary<int, string> LamThemTT_Type(string connectionString)
        {
            Dictionary<int, string> dics = new Dictionary<int, string>();

            string query = @"SELECT [ID] ,[Des] ,[Code] FROM [OTPaymentType]";

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand(query, sconn))
                {
                    var reader = scmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var text = "[" + reader["Code"].ToString() + "] " + reader["Des"].ToString();
                        dics.Add(int.Parse(reader["ID"].ToString()), text);
                    }
                }
            }

            return dics;
        }

        public string LamThemTT_TypeDDL(string connectionString, string hint = "")
        {
            string ddl = "<select class=\"form-control input-sm hint-sl\" style=\"width: 100px;\">";

            List<int> hints = new List<int>();

            string filter = "";

            if (!string.IsNullOrEmpty(hint) || hint != "0")
            {
                var hintArr = hint.Split(',');

                for (int h = 0; h < hintArr.Length; h++)
                {
                    hints.Add(int.Parse(hintArr[h].ToString()));
                }
            }

            if (hints.Count > 0)
            {
                //filter += " AND ID IN (" + string.Join(",", hints) + ")";
            }

            string query = @"SELECT [ID] ,[Des] ,[Code] FROM [OTPaymentType] WHERE 1 = 1 " + filter;

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand(query, sconn))
                {
                    var reader = scmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (hints.Contains(int.Parse(reader["ID"].ToString())))
                        {
                            var text = reader["Code"].ToString();
                            ddl += "<option value=" + int.Parse(reader["ID"].ToString()) + " selected>" + text + "</option>";
                        }
                        else
                        {
                            var text = reader["Code"].ToString();
                            ddl += "<option value=" + int.Parse(reader["ID"].ToString()) + ">" + text + "</option>";
                        }
                    }
                }
            }

            ddl += "</select>";

            return ddl;
        }

        public bool LamThemTT_Add(string connectionString, OTPayment obj)
        {
            int rowAffected = 0;

            string query = @"INSERT INTO [OTPayment] ([UserEnrollNumber], [Date], [Status], [Approve], [Reason])
                VALUES (@UserEnrollNumber, @Date, @Status, @Approve, @Reason)";

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand(query, sconn))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    scmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.ParseExact(obj.Ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    scmd.Parameters.Add("@Status", SqlDbType.Int).Value = obj.PhanLoai;
                    scmd.Parameters.Add("@Approve", SqlDbType.Int).Value = obj.DaDuyet;
                    scmd.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = obj.LyDo;
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            return rowAffected > 0 ? true : false;
        }

        public bool LamThemTT_Approve(string connectionString, List<OTPayment> objs)
        {
            string query = @"
                IF NOT EXISTS (SELECT 1 FROM OTPayment WHERE UserEnrollNumber = @UserEnrollNumber AND TimeStr = @TimeStr AND Status = @Status)
                    INSERT INTO [OTPayment] ([UserEnrollNumber], [Date], [Status], [Approve], [Reason], [TimeStr], [WH], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES(@UserEnrollNumber, @Date, @Status, @Approve, @Reason, @TimeStr, @WH, @CreatedTime, @CreatedBy, @UpdatedTime, @UpdatedBy)
                ELSE
                    UPDATE [OTPayment] SET [Status] = @Status, [Approve] = @Approve, [Reason] = @Reason, [WH] = @WH, [TimeStr] = @TimeStr, [UpdatedTime] = @UpdatedTime, [UpdatedBy] = @UpdatedBy WHERE [UserEnrollNumber] = @UserEnrollNumber AND [TimeStr] = @TimeStr AND [Approve] = @Approve AND Status = @Status";

            int rowAffected = 0;
            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand(query, sconn))
                {
                    foreach (var item in objs)
                    {
                        scmd.Parameters.Clear();
                        scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = item.UserEnrollNumber;
                        scmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Parse(item.Ngay);
                        scmd.Parameters.Add("@Status", SqlDbType.Int).Value = int.Parse(item.GoiY);
                        scmd.Parameters.Add("@Approve", SqlDbType.Int).Value = item.DaDuyet;
                        scmd.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = item.LyDo;
                        scmd.Parameters.Add("@TimeStr", SqlDbType.DateTime).Value = item.TimeStr;
                        scmd.Parameters.Add("@WH", SqlDbType.Int).Value = item.SoPhutDeXuat;
                        scmd.Parameters.Add("@CreatedTime", SqlDbType.DateTime).Value = item.CreatedTime;
                        scmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = item.CreatedBy;
                        scmd.Parameters.Add("@UpdatedTime", SqlDbType.DateTime).Value = item.UpdatedTime;
                        scmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = item.UpdatedBy;
                        rowAffected = scmd.ExecuteNonQuery();
                    }
                }
            }

            return rowAffected > 0 ? true : false;
            //return true;
        }
        public bool LamThemTT_UpdateApprove(string connectionString, List<OTPayment> objs)
        {
            string query = @"
                IF NOT EXISTS (SELECT 1 FROM OTPayment WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date)
                    INSERT INTO [OTPayment] ([UserEnrollNumber], [Date], [Status], [Approve], [Reason], [TimeStr], [WH], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES(@UserEnrollNumber, @Date, @Status, @Approve, @Reason, @TimeStr, @WH, @CreatedTime, @CreatedBy, @UpdatedTime, @UpdatedBy)
                ELSE
                    UPDATE OTPayment SET Approve = @Approve, WH = @WH WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date AND Status = @Status";

            int rowAffected = 0;
            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand(query, sconn))
                {
                    foreach (var item in objs)
                    {
                        scmd.Parameters.Clear();
                        scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = item.UserEnrollNumber;
                        scmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Parse(item.Ngay);
                        scmd.Parameters.Add("@Status", SqlDbType.Int).Value = string.IsNullOrEmpty(item.GoiY) ? 0 : int.Parse(item.GoiY);
                        scmd.Parameters.Add("@Approve", SqlDbType.Int).Value = item.DaDuyet;
                        scmd.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = item.LyDo;
                        scmd.Parameters.Add("@TimeStr", SqlDbType.DateTime).Value = item.TimeStr;
                        scmd.Parameters.Add("@WH", SqlDbType.Int).Value = item.SoPhutDeXuat;
                        scmd.Parameters.Add("@CreatedTime", SqlDbType.DateTime).Value = item.CreatedTime;
                        scmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = item.CreatedBy;
                        scmd.Parameters.Add("@UpdatedTime", SqlDbType.DateTime).Value = item.UpdatedTime;
                        scmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = item.UpdatedBy;
                        rowAffected = scmd.ExecuteNonQuery();
                    }
                }
            }

            return rowAffected > 0 ? true : false;
        }
        public bool LamThemTT_HuyDuyet(string connectionString, OTPayment obj)
        {
            string query = @"UPDATE OTPayment SET Approve = 1 WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date AND Status = @Status";

            int rowAffected = 0;
            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand(query, sconn))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    scmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Parse(obj.Ngay);
                    scmd.Parameters.Add("@Status", SqlDbType.Int).Value = int.Parse(obj.GoiY);
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }

            return rowAffected > 0 ? true : false;
        }
        public bool LamThemTT_Xoa(string connectionString, OTPayment obj)
        {
            string query = @"DELETE OTPayment WHERE UserEnrollNumber = @UserEnrollNumber AND Date = @Date AND Status = @Status";

            int rowAffected = 0;
            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand(query, sconn))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    scmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Parse(obj.Ngay);
                    scmd.Parameters.Add("@Status", SqlDbType.Int).Value = int.Parse(obj.GoiY);
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }

            return rowAffected > 0 ? true : false;
        }

        public GhepCa GhepCa_KT(string connectionString, GhepCa obj)
        {
            GhepCa o = new GhepCa();

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand("sp_OnCall_KT", sconn))
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    scmd.Parameters.Add("@TimeString", SqlDbType.VarChar).Value = obj.TimeString;
                    var reader = scmd.ExecuteReader();

                    while (reader.Read())
                    {
                        o.Val = int.Parse(reader["Val1"].ToString());
                        o.Par = reader["Par"].ToString();
                    }
                }
            }

            return o;
        }
        public bool GhepCa_Add(string connectionString, GhepCa obj, string lydo)
        {
            int rowAffected;

            var maql = StaticHelper.RandomID();

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand("sp_OnCall_Send", sconn))
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    scmd.Parameters.Add("@TimeString", SqlDbType.VarChar).Value = obj.TimeString;
                    scmd.Parameters.Add("@LyDo", SqlDbType.NVarChar).Value = lydo;
                    scmd.Parameters.Add("@MaQL", SqlDbType.VarChar).Value = maql;
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            //return rowAffected > 0 ? true : false;
            return true;
        }
        public bool GhepCa_Del(string connectionString, GhepCa obj)
        {
            int rowAffected = 0;

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand("sp_OnCall_Del", sconn))
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    scmd.Parameters.Add("@TimeString", SqlDbType.VarChar).Value = obj.TimeString;
                    scmd.Parameters.Add("@MaQL", SqlDbType.VarChar).Value = obj.MaQL;
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            return rowAffected > 0 ? true : false;
            //return true;
        }
        public IEnumerable<CaKhongXacDinh> CaKhongXacDinh_DanhSach(string connectionString, CaKhongXacDinh_Filter obj)
        {
            List<CaKhongXacDinh> lst = new List<CaKhongXacDinh>();

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand("sp_DuyetCaKoCoDinh_View", sconn))
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = DateTime.ParseExact(obj.StartTime, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                    scmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = DateTime.ParseExact(obj.EndTime, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                    scmd.Parameters.Add("@KP", SqlDbType.Int).Value = obj.KhoaPhong;
                    scmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    scmd.Parameters.Add("@Duyet", SqlDbType.Int).Value = obj.HienThi;
                    var reader = scmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst.Add(new CaKhongXacDinh()
                        {
                            UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                            UserFullName = reader["UserFullName"].ToString(),
                            UserFullCode = reader["UserFullCode"].ToString(),
                            KhoaPhong = string.IsNullOrEmpty(reader["PhongKhoa"].ToString()) ? 0 : int.Parse(reader["PhongKhoa"].ToString()),
                            TenKhoaPhong = reader["Description"].ToString(),
                            StartTime = DateTime.Parse(reader["ST"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"),
                            EndTime = DateTime.Parse(reader["ET"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"),
                            WH = string.IsNullOrEmpty(reader["WH"].ToString()) ? "" : reader["WH"].ToString(),
                            STApp = string.IsNullOrEmpty(reader["STApp"].ToString()) ? "" : reader["STApp"].ToString(),
                            ETApp = string.IsNullOrEmpty(reader["ETApp"].ToString()) ? "" : reader["ETApp"].ToString(),
                            STWC = string.IsNullOrEmpty(reader["STWC"].ToString()) ? 0 : int.Parse(reader["STWC"].ToString()),
                            ETWC = string.IsNullOrEmpty(reader["ETWC"].ToString()) ? 0 : int.Parse(reader["ETWC"].ToString()),
                            Display = string.IsNullOrEmpty(reader["Display"].ToString()) ? 0 : int.Parse(reader["Display"].ToString()),
                            LyDo = reader["LyDo"].ToString()
                        });
                    }
                }
            }

            return lst;
        }
        public bool CaKhongXacDinh_UpdateApprove(string connectionString, List<CaKhongXacDinh> obj)
        {
            int rowAffected = 0;

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand("sp_OnCall_App", sconn))
                {
                    scmd.CommandType = CommandType.StoredProcedure;

                    foreach (var item in obj)
                    {
                        scmd.Parameters.Clear();
                        scmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = item.UserEnrollNumber;
                        scmd.Parameters.Add("@TimeString", SqlDbType.VarChar).Value = item.TimeString;
                        rowAffected = scmd.ExecuteNonQuery();
                    }
                }
            }
            //return rowAffected > 0 ? true : false;
            return true;
        }
        public bool CaKhongXacDinh_Del(string connectionString, CaKhongXacDinh obj)
        {
            int rowAffected = 0;

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand("sp_OnCall_Del", sconn))
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = obj.UserEnrollNumber;
                    scmd.Parameters.Add("@TimeString", SqlDbType.VarChar).Value = obj.TimeString;
                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            return rowAffected > 0 ? true : false;
        }

        #endregion LÀM THÊM TÍNH TIỀN

        #region TỔNG HỢP CÔNG

        //public DataTable TongHopCong_View(string connectionString, string fromDate, string toDate, int userid, int kp)
        //{
        //    using (var sConnection = new SqlConnection(connectionString))
        //    {
        //        if (sConnection.State == ConnectionState.Closed)
        //            sConnection.Open();

        //        using (var sCmd = new SqlCommand("sp_TongHopCong_View", sConnection))
        //        {
        //            sCmd.CommandType = CommandType.StoredProcedure;
        //            sCmd.Parameters.Clear();
        //            sCmd.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = userid.ToString();
        //            sCmd.Parameters.Add("@KP", SqlDbType.Int).Value = kp;
        //            sCmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = fromDate;
        //            sCmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = toDate;

        //            SqlDataAdapter adapter = new SqlDataAdapter(sCmd);
        //            DataTable dataTable = new DataTable();

        //            adapter.Fill(dataTable);

        //            return dataTable;
        //        }
        //    }
        //}

        public IEnumerable<TongHopCong> TongHopCong_View(string connectionString, TongHopCong_Filter oFilter)
        {
            List<TongHopCong> lst = new List<TongHopCong>();

            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == Data.ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand("sp_TongHopCong_View", sConnection))
                {
                    sCommand.CommandTimeout = 0;
                    sCommand.CommandType = CommandType.StoredProcedure;
                    sCommand.Parameters.Clear();
                    sCommand.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = oFilter.TuNgay;
                    sCommand.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = oFilter.DenNgay;
                    sCommand.Parameters.Add("@KP", SqlDbType.Int).Value = oFilter.KhoaPhong;
                    sCommand.Parameters.Add("@MaNV", SqlDbType.Int).Value = oFilter.UserID;
                    if (oFilter.LoaiNV != -1)
                        sCommand.Parameters.Add("@LoaiNV", SqlDbType.Int).Value = oFilter.LoaiNV;

                    DataTable dtable = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sCommand);
                    da.Fill(dtable);

                    foreach (DataRow dr in dtable.Rows)
                    {
                        var o = new TongHopCong()
                        {
                            MA_SO_NV = int.Parse(dr["UserEnrollNumber"].ToString()),
                            MA_NV = dr["UserFullCode"].ToString(),
                            HO_TEN = dr["UserFullName"].ToString(),
                            KHOA_PHONG = dr["NCC"].ToString(),

                            //CÔNG TRƯỚC NGÀY THAY ĐỔI LƯƠNG
                            CONG_TRUOC_TD_LUONG_FIX = dr["FWF"].ToString(),
                            CONG_TRUOC_TD_LUONG_FIX_E = dr["FWFE"].ToString(),

                            //CÔNG SAU NGÀY THAY ĐỔI LƯƠNG
                            CONG_SAU_TD_LUONG_FIX = dr["LWF"].ToString(),
                            CONG_SAU_TD_LUONG_FIX_E = dr["LWFE"].ToString(),
                            //
                            HE_SO_MUON = dr["LIRD"].ToString(),
                            //SỐ PHÚT ĐI MUỘN
                            SO_PHUT_DI_MUON_THUC = dr["LI"].ToString(),
                            SO_PHUT_DI_MUON_E = dr["LIE"].ToString(),
                            //
                            HE_SO_SOM = dr["EORD"].ToString(),
                            //SỐ PHÚT VỀ SỚM
                            SO_PHUT_VE_SOM_THUC = dr["EO"].ToString(),
                            SO_PHUT_VE_SOM_E = dr["EOE"].ToString(),
                            //TRỰC
                            //PHU_CAP_TRUC = dr["TrB"].ToString(),
                            //PHU_CAP_TRUC_E = dr["TrBE"].ToString(),
                            //TRỰC ĐÊM
                            //PHU_CAP_TRUC_DEM = dr["NB"].ToString(),
                            //PHU_CAP_TRUC_DEM_E = dr["NBE"].ToString(),
                            //
                            SO_TK_1 = dr["SoTK1"].ToString(),
                            TEN_NH_1 = dr["NganHang1"].ToString(),
                            //
                            SO_TK_2 = dr["SoTK2"].ToString(),
                            TEN_NH_2 = dr["NganHang2"].ToString(),
                            //
                            SO_CMT = dr["SoCMT"].ToString(),
                            //
                            SO_GIO_LV = String.IsNullOrEmpty(dr["SoGioLV"].ToString()) ? "" : dr["SoGioLV"].ToString(),
                            //
                            TONG_SUAT_AN = dr["An"].ToString(),
                            //
                            TONG_SUAT_AN_E = dr["AE"].ToString(),
                            //
                            CONG_THUC = dr["CongThuc"].ToString(),
                            //
                            EMAIL = dr["EM"].ToString(),
                            //
                            CONG_CHUAN = dr["CChuan"].ToString(),
                            //
                            MA_SO_THUE = dr["MST"].ToString(),
                            //
                            SO_LAN_KHONG_VR = dr["IOF"].ToString(),
                            //
                            TIEN_QUEN_CC = dr["TienPhat"].ToString(),
                            //
                            NGHI_NGUNGVIEC = dr["PD"].ToString(),
                            NGHI_NGUNGVIEC_E = dr["PDE"].ToString(),
                            //
                            NGHI_BHXH = dr["BH"].ToString(),
                            NGHI_BHXH_E = dr["BHE"].ToString(),
                            //
                            NGHI_KL = dr["KL"].ToString(),
                            NGHI_KL_E = dr["KLE"].ToString(),
                            //
                            TONG_CONG = dr["WD"].ToString(),
                            //
                            TONG_CONG_VA_NGHI = dr["TCN"].ToString(),
                            //
                            LOI_HSBA = dr["LHS"].ToString(),
                            //
                            PHU_CAP_DI_LAI = dr["PCDL"].ToString(),
                            //
                            PHU_CAP_CA_GAY = dr["PCD"].ToString(),
                            //
                            PHU_CAP_CA_GAY_E = dr["PCG"].ToString(),
                            //
                            DANG_DAO_TAO = dr["DT"].ToString(),
                            //
                            QUA_SN = dr["SN"].ToString(),
                            QUA_SN_E = dr["SNE"].ToString(),
                            //
                            NT_TRUOC_TD_LUONG = dr["FOvTWH"].ToString(),
                            NT_TRUOC_TD_LUONG_E = dr["FOvTWHE"].ToString(),
                            //
                            NN_TRUOC_TD_LUONG = dr["FOffWH"].ToString(),
                            NN_TRUOC_TD_LUONG_E = dr["FOffWHE"].ToString(),
                            //
                            NL_TRUOC_TD_LUONG = dr["FLWH"].ToString(),
                            NL_TRUOC_TD_LUONG_E = dr["FLWHE"].ToString(),
                            //
                            NT_SAU_TD_LUONG = dr["LOvTWH"].ToString(),
                            NT_SAU_TD_LUONG_E = dr["LOvTWHE"].ToString(),
                            //
                            NN_SAU_TD_LUONG = dr["LOffWH"].ToString(),
                            NN_SAU_TD_LUONG_E = dr["LOffWHE"].ToString(),
                            //
                            NL_SAU_TD_LUONG = dr["LLWH"].ToString(),
                            NL_SAU_TD_LUONG_E = dr["LLWHE"].ToString(),
                            //
                            MUC_DONG_BHXH = dr["BHSE"].ToString(),
                            //
                            DOAN_PHI = dr["DP"].ToString(),
                            //
                            LUONG_NGUNG_VIEC = dr["NgVi"].ToString(),
                            //
                            BU_TRU_LUONG = dr["BTL"].ToString(),
                            //
                            LYDO_BT = dr["LyDoBT"].ToString(),
                            //
                            GIU_LUONG = dr["GiuLuong"].ToString(),
                            //
                            TIEN_THUONG = dr["TienThuong"].ToString(),
                            //
                            TIEN_THUONG_KHAC = dr["TienThuongKhac"].ToString(),
                            //
                            PC_KHAC = dr["PCKhac"].ToString(),
                            //
                            DOI_CHIEU = dr["DoiChieu"].ToString(),
                            //
                            GHI_CHU = dr["GhiChu"].ToString(),
                            //
                            NGAY_THUA_R = dr["WDR"].ToString(),
                            NGAY_THUA_E = dr["WDRE"].ToString(),
                            //
                            LOAI_BHXH = dr["LoaiDongBHXH"].ToString(),
                            //Chưa xác định
                            DOI_TUONG = dr["ST"].ToString(),
                            PHONG_DICH = dr["PD"].ToString(),
                            QUEN_CC = dr["IOF"].ToString(),
                            MUC_BHXH = dr["BHSE"].ToString(),
                            SO_NGAY_THUA = dr["WDR"].ToString(),
                            MO = dr["Mo"].ToString(),
                            MS = dr["MS"].ToString(),
                            //
                            //PHU_CAP_TR08H = dr["XLe"].ToString(),
                            //PHU_CAP_TR20H = dr["DNLe"].ToString(),
                            //PHU_CAP_TR24H = dr["TRLe"].ToString(),
                            TienPhuCap = dr["TienPhuCap"].ToString(),
                            DienGiaiPC = dr["DienGiaiPC"].ToString(),
                            KiemTraBuThua = int.Parse(dr["KiemTraBuThua"].ToString()),
                            //======== Dành cho quản lý
                            CONG_TRUOC_TD_LUONG_FIX_D = dr["FWFD"].ToString(),
                            CONG_SAU_TD_LUONG_FIX_D = dr["LWFD"].ToString(),
                            TONG_MUON_D = dr["LID"].ToString(),
                            TONG_SOM_D = dr["EOD"].ToString(),
                            //PHU_CAP_TRUC_D = dr["TrBD"].ToString(),
                            //PHU_CAP_TRUC_DEM_D = dr["NBD"].ToString(),
                            TONG_SUAT_AN_D = dr["AED"].ToString(),
                            NGHI_PHONG_DICH_D = dr["PDD"].ToString(),
                            NGHI_BHXH_D = dr["BHD"].ToString(),
                            NGHI_KL_D = dr["KLD"].ToString()
                        };
                        lst.Add(o);
                    }
                }
            }
            return lst;
        }

        public DataTable TongHopCong_Excel(string connectionString, TongHopCong_Filter oFilter)
        {
            List<TongHopCong> lst = new List<TongHopCong>();
            DataTable dtable = new DataTable();
            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand("sp_TongHopCong_Excel", sconn))
                {
                    scmd.CommandTimeout = 0;
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = oFilter.TuNgay;
                    scmd.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = oFilter.DenNgay;
                    scmd.Parameters.Add("@KP", SqlDbType.Int).Value = oFilter.KhoaPhong;
                    scmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = oFilter.UserID;
                    scmd.Parameters.Add("@Role", SqlDbType.Int).Value = oFilter.Role;
                    if (oFilter.LoaiNV != -1)
                        scmd.Parameters.Add("@LoaiNV", SqlDbType.Int).Value = oFilter.LoaiNV;

                    SqlDataAdapter da = new SqlDataAdapter(scmd);
                    da.Fill(dtable);
                }
            }

            return dtable;
        }
        public int TongHopCong_CapNhatChiSo(string connectionString, int userid, string date, string column, string val)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UserEnrollNumber", userid);
                parameters.Add("NgayKT", date);
                parameters.Add("Type", column);
                parameters.Add("Text", string.IsNullOrEmpty(val) ? null : val);
                return db.Execute("sp_TongHopCong_ChinhSua", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int TongHopCong_TongHopSoLieu(string connectionString, string ngaybd, string ngaykt)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("NgayBD", ngaybd);
                parameters.Add("NgayKT", ngaykt);
                return db.Execute("sp_TongHopCong_LuuDLThang", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        #endregion TỔNG HỢP CÔNG

        #region Khóa số liệu

        public int KhoaSoLieu_TrangThai(string connectionString)
        {
            string query = "SELECT NumberVal FROM Config WHERE ParameterID = 'T04'";

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand(query, sconn))
                {
                    scmd.Parameters.Clear();

                    var rowAffected = scmd.ExecuteScalar();

                    return rowAffected == null ? -1 : (int)rowAffected;
                }
            }
        }
        public bool KhoaSoLieu_CapNhat(string connectionString, int status)
        {
            string query = "UPDATE Config SET NumberVal = (CASE WHEN @status = 0 THEN 1 WHEN @status = 1 THEN 0 END), DatetimeVal = @date WHERE ParameterID = 'T04'";

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand(query, sconn))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@status", SqlDbType.Int).Value = status;
                    scmd.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);

                    var rowAffected = scmd.ExecuteNonQuery();

                    return rowAffected > 0 ? true : false;
                }
            }
        }

        #endregion Khóa số liệu

        public DataTable DS_KhaiBaoAn(string connectionString, int id, string date)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_CheckEat_Info", sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@param_UserEnrollNumber", SqlDbType.VarChar).Value = id.ToString();
                    cmd.Parameters.Add("@param_Ngay", SqlDbType.VarChar).Value = date.Substring(6, 4) + date.Substring(3, 2) + date.Substring(0, 2);
                    cmd.Parameters.Add("@param_Type", SqlDbType.Int).Value = 12;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
        public int XacNhanHuy(string connectionString, KhaiBaoAn obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_UserEnrollNumber", obj.UserEnrollNumber);
                parameters.Add("param_Ngay", obj.Date.Substring(6, 4) + obj.Date.Substring(3, 2) + obj.Date.Substring(0, 2));
                parameters.Add("param_Loai", obj.Loai);
                parameters.Add("param_Type", 13);
                return db.Execute("sp_CheckEat_Info", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int XacNhan(string connectionString, KhaiBaoAn obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_UserEnrollNumber", obj.UserEnrollNumber);
                parameters.Add("param_Ngay", obj.Date.Substring(6, 4) + obj.Date.Substring(3, 2) + obj.Date.Substring(0, 2));
                parameters.Add("param_Loai", obj.Loai);
                parameters.Add("param_Type", 14);
                return db.Execute("sp_CheckEat_Info", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public DiaDiemAn Get_NoiAn(string connectionString, int user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_UserEnrollNumber", user.ToString());
                parameters.Add("param_Type", 15);

                return db.Query<DiaDiemAn>("sp_CheckEat_Info", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public IEnumerable<Department> DSKhoaPhong(string connectionString, int userenrollnumber)
        {
            string sql = string.Empty;
            if (userenrollnumber == -1)
            {
                sql = @"select ID as STT, Description as KhoaP from RelationDept where ID > 2 order by ID";
            }
            else
            {
                sql = @"if (select count(*) from UserRelationDept where UserEnrollNumber = " + userenrollnumber + ") > 0 " +
                        "begin " +
                            "SELECT ID as STT, Description as KhoaP FROM RelationDept WHERE ID in (select DeptID from UserRelationDept where UserEnrollNumber = " + userenrollnumber + ") " +
                        "end " +
                    "else " +
                        "begin " +
                            "SELECT ID as STT, Description as KhoaP FROM RelationDept WHERE ID in (select PhongKhoa from UserInfExt where UserEnrollNumber = " + userenrollnumber + ") " +
                        "end";
            }


            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<Department>(sql);
                sqlConnection.Close();
                return result.ToList();
            }
        }
        public IEnumerable<Department> DSLoaiNhanVien(string connectionString)
        {
            string sql = string.Empty;

            sql = @"select ID as STT, LoaiNV as KhoaP from LoaiNhanVien order by ID";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<Department>(sql);
                sqlConnection.Close();
                return result.ToList();
            }
        }
        public IEnumerable<HCNS_NhanVien> Table_VanTayThucTe(string connectionString, HCNS_NhanVien obj, string loai)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_UserEnrollNumber", obj.UserEnrollNumber == 0 ? "-1" : obj.UserEnrollNumber.ToString());
                parameters.Add("param_Dept", obj.PhongKhoaHC);
                parameters.Add("param_Loai", loai);
                parameters.Add("param_TuNgay", DateTime.ParseExact(obj.TuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("param_DenNgay", DateTime.ParseExact(obj.DenNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("param_Type", 20);

                return db.Query<HCNS_NhanVien>("sp_CheckEat_Info", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public TinhPhep Get_Phep(string connectionString, string ngay, int user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ngay", ngay);
                parameters.Add("user", user);
                parameters.Add("param", "tinhphep");

                return db.Query<TinhPhep>("sp_TinhPhep", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public IEnumerable<LamViecNgayLe> Table_LamViecNgayLe(string connectionString, LamViecNgayLe obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("NgayBD", DateTime.ParseExact(obj.TuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
                parameters.Add("NgayKT", DateTime.ParseExact(obj.DenNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
                parameters.Add("KP", obj.KhoaPhong);
                parameters.Add("MaNV", string.IsNullOrEmpty(obj.MaNV) ? "-1" : obj.MaNV);
                parameters.Add("TrangThai", obj.Status);

                return db.Query<LamViecNgayLe>("sp_LamNgayLe_View", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public string Push_DeXuatLamViecNgayle(string connectionString, DataTable dt, string lydo, string loaica, string userAcc, int type, string approve)
        {
            string result = string.Empty;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_HWStatus"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@UserAcc", userAcc);
                    cmd.Parameters.AddWithValue("@LyDo", lydo);
                    cmd.Parameters.AddWithValue("@Approve", approve);
                    cmd.Parameters.AddWithValue("@HType", loaica);
                    cmd.Parameters.AddWithValue("@HWStatus", dt);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = reader["Result"].ToString();
                        }
                    }
                    con.Close();
                }
            }
            return result;
        }
        public int ChuyenKyLuong(string connectionString, string ngaybd, string ngaykt)
        {
            //using (IDbConnection db = new SqlConnection(connectionString))
            //{
            //    if (db.State == ConnectionState.Closed)
            //        db.Open();

            //    DynamicParameters parameters = new DynamicParameters();
            //    return db.Execute("sp_NTDL", parameters, commandType: CommandType.StoredProcedure);
            //}
            string query = "EXEC [dbo].[sp_NTDL]";

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand(query, sconn))
                {
                    scmd.Parameters.Clear();

                    var rowAffected = scmd.ExecuteScalar();

                    return rowAffected == null ? -1 : (int)rowAffected;
                }
            }
        }
    }
}