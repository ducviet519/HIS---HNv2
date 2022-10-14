using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace System.App.Repositories.HCNS
{
    public class BCCC_Repo
    {
        #region 1. Bằng cấp

        public Dictionary<int, string> DS_LoaiBangCap(string connectionString)
        {
            string _query = "SELECT RID, RName FROM Ranks ORDER BY RID ASC";

            Dictionary<int, string> lst = new Dictionary<int, string>();

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
                            lst.Add(int.Parse(reader["RID"].ToString()), reader["RName"].ToString());
                        }
                    }
                }
            }
            return lst;
        }
        public IEnumerable<BangCap> DS_BangCap(string connectionString, BangCap obj)
        {
            string filter = "";

            if (!String.IsNullOrEmpty(obj.UserFullName.Trim()))
            {
                filter += " AND (u.UserEnrollNumber = " + obj.UserFullName + " OR u.UserFullName LIKE N'%" + obj.UserFullName + "%')";
            }
            if (!String.IsNullOrEmpty(obj.SType))
            {
                filter += " AND Type = " + obj.SType;
            }

            List<BangCap> lst = new List<BangCap>();

            string _query = @"
                SELECT d.[ID], u.UserEnrollNumber, u.UserFullCode, u.UserFullName, de.KhoaP as KhoaPhong, u.ChucDanh, r.RName as BangCap, d.GradSch as TruongTN, d.GYear as NamTN, d.Field as LinhVuc
                FROM [Degrees] d
	                LEFT JOIN UserInfExt u ON u.UserEnrollNumber = d.UserEnrollNumber
	                LEFT JOIN Depts de ON de.STT = u.PhongKhoaHC
	                LEFT JOIN Ranks r ON r.RID = d.Type WHERE (1 = 1) " + filter + @" ORDER BY UserEnrollNumber";

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
                            lst.Add(new BangCap()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                UserEnrollNumber = String.IsNullOrEmpty(reader["UserEnrollNumber"].ToString()) ? 0 : int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                KhoaPhong = reader["KhoaPhong"].ToString(),
                                ChucDanh = reader["ChucDanh"].ToString(),
                                TenBangCap = reader["BangCap"].ToString(),
                                GradSch = reader["TruongTN"].ToString(),
                                GYear = String.IsNullOrEmpty(reader["NamTN"].ToString()) ? 0 : int.Parse(reader["NamTN"].ToString()),
                                Field = reader["LinhVuc"].ToString()
                            });
                        }
                    }
                }
            }
            return lst;
        }
        public BangCap ThongTin_BangCap(string connectionString, int id)
        {
            string _query = @"SELECT d.[ID], d.[UserEnrollNumber], u.UserFullCode, u.UserFullName, d.[Type], d.[GradSch], d.[GYear], d.[Field]
                    FROM Degrees d
	                    LEFT JOIN UserInfExt u ON u.UserEnrollNumber = d.UserEnrollNumber WHERE d.ID = " + id;

            BangCap obj = null;

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
                            obj = new BangCap()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                UserEnrollNumber = String.IsNullOrEmpty(reader["UserEnrollNumber"].ToString()) ? 0 : int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                Type = int.Parse(reader["Type"].ToString()),
                                GradSch = reader["GradSch"].ToString(),
                                GYear = String.IsNullOrEmpty(reader["GYear"].ToString()) ? 0 : int.Parse(reader["GYear"].ToString()),
                                Field = reader["Field"].ToString()
                            };
                        }
                    }
                }
            }
            return obj;
        }
        public bool Add_BangCap(string connectionString, BangCap obj)
        {
            string _query = @"INSERT INTO Degrees (UserEnrollNumber, Type, GradSch, GYear, Field)
                VALUES (@UserEnrollNumber, @Type, @GradSch, @GYear, @Field)";

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", obj.UserEnrollNumber);
                    sqlCommand.Parameters.AddWithValue("@Type", obj.Type);
                    sqlCommand.Parameters.AddWithValue("@GradSch", String.IsNullOrEmpty(obj.GradSch) ? "" : obj.GradSch.ToString());
                    sqlCommand.Parameters.AddWithValue("@GYear", String.IsNullOrEmpty(obj.GYear.ToString()) ? 0 : obj.GYear);
                    sqlCommand.Parameters.AddWithValue("@Field", String.IsNullOrEmpty(obj.Field) ? "" : obj.Field.ToString());

                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool Update_BangCap(string connectionString, BangCap obj)
        {
            string _query = @"UPDATE Degrees
                SET Type = @Type, GradSch = @GradSch, GYear = @GYear, Field = @Field WHERE ID = @ID";

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ID", obj.ID);
                    sqlCommand.Parameters.AddWithValue("@Type", obj.Type);
                    sqlCommand.Parameters.AddWithValue("@GradSch", obj.GradSch);
                    sqlCommand.Parameters.AddWithValue("@GYear", obj.GYear);
                    sqlCommand.Parameters.AddWithValue("@Field", obj.Field);

                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool Delete_BangCap(string connectionString, BangCap obj)
        {
            string _query = "DELETE Degrees WHERE ID = @ID";

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ID", obj.ID);

                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }

        #endregion 1. Bằng cấp

        #region 2. Chứng chỉ

        public IEnumerable<ChungChi> DS_ChungChi(string connectionString, ChungChi obj)
        {
            string filter = "";

            if (!String.IsNullOrEmpty(obj.UserFullName.Trim()))
            {
                filter += " AND (b.UserEnrollNumber = " + obj.UserFullName + " OR b.UserFullName LIKE N'%" + obj.UserFullName + "%')";
            }
            if (obj.KhoaPhong > -1)
            {
                filter += " AND d.STT = " + obj.KhoaPhong;
            }

            List<ChungChi> lst = new List<ChungChi>();

            string _query = @"
                SELECT c.[ID]
                      ,c.[UserEnrollNumber]
	                  ,b.[UserFullCode]
	                  ,b.[UserFullName]
                      ,b.[ChucDanh]
	                  ,d.[KhoaP]
                      ,c.[Des]
                      ,c.[IssuedBy]
                      ,c.[SDate]
                      ,c.[EDate]
                      ,c.[Les]
                  FROM [Certificates] c
	                LEFT JOIN UserInfExt b ON c.UserEnrollNumber = b.UserEnrollNumber
	                LEFT JOIN Depts d ON b.PhongKhoaHC = d.STT
                WHERE (1 = 1) " + filter + @" ORDER BY c.UserEnrollNumber";

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
                            lst.Add(new ChungChi()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                Ten_KhoaPhong = reader["KhoaP"].ToString(),
                                ChucDanh = reader["ChucDanh"].ToString(),
                                Des = reader["Des"].ToString(),
                                IssuedBy = reader["IssuedBy"].ToString(),
                                SDate = String.IsNullOrEmpty(reader["SDate"].ToString()) ? "" : DateTime.Parse(reader["SDate"].ToString()).ToString("dd/MM/yyyy"),
                                EDate = String.IsNullOrEmpty(reader["EDate"].ToString()) ? "" : DateTime.Parse(reader["EDate"].ToString()).ToString("dd/MM/yyyy"),
                                Les = String.IsNullOrEmpty(reader["Les"].ToString()) ? 0 : int.Parse(reader["Les"].ToString())
                            });
                        }
                    }
                }
            }
            return lst;
        }
        public ChungChi ThongTin_ChungChi(string connectionString, int id)
        {
            string _query = @"SELECT c.[ID], c.[UserEnrollNumber], u.UserFullCode, u.UserFullName, c.[Des], c.[IssuedBy], c.[SDate], c.[EDate], c.[Les]
                    FROM [Certificates] c
	                    LEFT JOIN UserInfExt u ON u.UserEnrollNumber = c.UserEnrollNumber WHERE c.ID = " + id;

            ChungChi obj = null;

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
                            obj = new ChungChi()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                UserEnrollNumber = String.IsNullOrEmpty(reader["UserEnrollNumber"].ToString()) ? 0 : int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                Des = reader["Des"].ToString(),
                                IssuedBy = reader["IssuedBy"].ToString(),
                                SDate = String.IsNullOrEmpty(reader["SDate"].ToString()) ? "" : DateTime.Parse(reader["SDate"].ToString()).ToString("dd/MM/yyyy"),
                                EDate = String.IsNullOrEmpty(reader["EDate"].ToString()) ? "" : DateTime.Parse(reader["EDate"].ToString()).ToString("dd/MM/yyyy"),
                                Les = int.Parse(reader["Les"].ToString())
                            };
                        }
                    }
                }
            }
            return obj;
        }
        public bool Add_ChungChi(string connectionString, ChungChi obj)
        {
            string _query = @"INSERT INTO Certificates (UserEnrollNumber, Des, IssuedBy, SDate, EDate, Les)
                        VALUES (@UserEnrollNumber, @Des, @IssuedBy, @SDate, @EDate, @Les)";

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", obj.UserEnrollNumber);
                    sqlCommand.Parameters.AddWithValue("@Des", obj.Des);
                    sqlCommand.Parameters.AddWithValue("@IssuedBy", obj.IssuedBy);

                    if (String.IsNullOrEmpty(obj.SDate))
                        sqlCommand.Parameters.Add("@SDate", SqlDbType.SmallDateTime).Value = DBNull.Value;
                    else
                        sqlCommand.Parameters.Add("@SDate", SqlDbType.SmallDateTime).Value = DateTime.ParseExact(obj.SDate, "dd/MM/yyyy", CultureInfo.CurrentCulture);

                    if (String.IsNullOrEmpty(obj.EDate))
                        sqlCommand.Parameters.Add("@EDate", SqlDbType.SmallDateTime).Value = DBNull.Value;
                    else
                        sqlCommand.Parameters.Add("@EDate", SqlDbType.SmallDateTime).Value = DateTime.ParseExact(obj.EDate, "dd/MM/yyyy", CultureInfo.CurrentCulture);

                    sqlCommand.Parameters.AddWithValue("@Les", obj.Les);

                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool Update_ChungChi(string connectionString, ChungChi obj)
        {
            string _query = @"UPDATE Certificates
                SET UserEnrollNumber = @UserEnrollNumber, Des = @Des, IssuedBy = @IssuedBy, SDate = @SDate, EDate = @EDate, Les = @Les WHERE ID = @ID";

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ID", obj.ID);
                    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", obj.UserEnrollNumber);
                    sqlCommand.Parameters.AddWithValue("@Des", obj.Des);
                    sqlCommand.Parameters.AddWithValue("@IssuedBy", obj.IssuedBy);

                    if (String.IsNullOrEmpty(obj.SDate))
                        sqlCommand.Parameters.Add("@SDate", SqlDbType.SmallDateTime).Value = DBNull.Value;
                    else
                        sqlCommand.Parameters.Add("@SDate", SqlDbType.SmallDateTime).Value = DateTime.ParseExact(obj.SDate, "dd/MM/yyyy", CultureInfo.CurrentCulture);

                    if (String.IsNullOrEmpty(obj.EDate))
                        sqlCommand.Parameters.Add("@EDate", SqlDbType.SmallDateTime).Value = DBNull.Value;
                    else
                        sqlCommand.Parameters.Add("@EDate", SqlDbType.SmallDateTime).Value = DateTime.ParseExact(obj.EDate, "dd/MM/yyyy", CultureInfo.CurrentCulture);

                    sqlCommand.Parameters.AddWithValue("@Les", obj.Les);

                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }
        public bool Delete_ChungChi(string connectionString, ChungChi obj)
        {
            string _query = "DELETE Certificates WHERE ID = @ID";

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ID", obj.ID);

                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }

        #endregion 2. Chứng chỉ

        #region 3. Chứng chỉ hành nghề

        public IEnumerable<ChungChiHanhNghe> DS_ChungChiHanhNghe(string connectionString, ChungChiHanhNghe obj)
        {
            string filter = "";

            if (!String.IsNullOrEmpty(obj.UserFullName.Trim()))
            {
                filter += " AND (a.UserEnrollNumber = " + obj.UserFullName + " OR a.UserFullName LIKE N'%" + obj.UserFullName + "%')";
            }
            if (obj.KhoaPhong > -1)
            {
                filter += " AND b.STT = " + obj.KhoaPhong;
            }

            List<ChungChiHanhNghe> lst = new List<ChungChiHanhNghe>();

            string _query = @"
                SELECT a.UserEnrollNumber, a.UserFullCode, a.UserFullName, b.KhoaP AS KhoaPhong,
                    a.ChucDanh, a.ChungChiHN, a.PhamViCM, a.NgayCapCCHN, a.NoiCapCCHN,
                    (CASE WHEN a.DaGuiSo = 0 THEN N'Không gửi Sở' WHEN a.DaGuiSo = 1 THEN N'Đã gửi Sở' ELSE '' END) as DaGuiSo, a.NgayGuiSo AS NgayGuiSo
                    FROM UserInfExt a
	                    LEFT JOIN Depts b ON a.PhongKhoaHC = b.STT
                    WHERE (1 = 1) " + filter + @" AND ISNULL(DaNghi, 0) = 0 ORDER BY a.UserEnrollNumber";

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
                            lst.Add(new ChungChiHanhNghe()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                Ten_KhoaPhong = reader["KhoaPhong"].ToString(),
                                ChucDanh = reader["ChucDanh"].ToString(),
                                ChungChiHN = reader["ChungChiHN"].ToString(),
                                PhamViCM = reader["PhamViCM"].ToString(),
                                NgayCapCCHN = String.IsNullOrEmpty(reader["NgayCapCCHN"].ToString()) ? "" : DateTime.Parse(reader["NgayCapCCHN"].ToString()).ToString("dd/MM/yyyy"),
                                NoiCapCCHN = reader["NoiCapCCHN"].ToString(),
                                DaGuiSo = String.IsNullOrEmpty(reader["DaGuiSo"].ToString()) ? "" : reader["DaGuiSo"].ToString(),
                                NgayGuiSo = String.IsNullOrEmpty(reader["NgayGuiSo"].ToString()) ? "" : DateTime.Parse(reader["NgayGuiSo"].ToString()).ToString("dd/MM/yyyy")
                            });
                        }
                    }
                }
            }
            return lst;
        }
        public ChungChiHanhNghe ThongTin_ChungChiHanhNghe(string connectionString, int id)
        {
            string _query = @"SELECT UserEnrollNumber, UserFullCode, UserFullName, ChungChiHN, PhamViCM, NgayCapCCHN, NoiCapCCHN, DaGuiSo, NgayGuiSo FROM UserInfExt WHERE UserEnrollNumber = " + id;

            ChungChiHanhNghe obj = null;

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
                            obj = new ChungChiHanhNghe()
                            {
                                UserEnrollNumber = String.IsNullOrEmpty(reader["UserEnrollNumber"].ToString()) ? 0 : int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                ChungChiHN = reader["ChungChiHN"].ToString(),
                                PhamViCM = reader["PhamViCM"].ToString(),
                                NgayCapCCHN = String.IsNullOrEmpty(reader["NgayCapCCHN"].ToString()) ? "" : DateTime.Parse(reader["NgayCapCCHN"].ToString()).ToString("dd/MM/yyyy"),
                                NoiCapCCHN = reader["NoiCapCCHN"].ToString(),
                                DaGuiSo = reader["DaGuiSo"].ToString(),
                                NgayGuiSo = String.IsNullOrEmpty(reader["NgayGuiSo"].ToString()) ? "" : DateTime.Parse(reader["NgayGuiSo"].ToString()).ToString("dd/MM/yyyy"),
                            };
                        }
                    }
                }
            }
            return obj;
        }
        public bool Update_ChungChiHanhNghe(string connectionString, ChungChiHanhNghe obj)
        {
            string _query = @"UPDATE UserInfExt SET
                ChungChiHN = @ChungChiHN, PhamViCM = @PhamViCM, NgayCapCCHN = @NgayCapCCHN, NoiCapCCHN = @NoiCapCCHN, DaGuiSo = @DaGuiSo, NgayGuiSo = @NgayGuiSo WHERE UserEnrollNumber = @UserEnrollNumber";

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", obj.UserEnrollNumber);
                    sqlCommand.Parameters.AddWithValue("@ChungChiHN", String.IsNullOrEmpty(obj.ChungChiHN) ? "" : obj.ChungChiHN);
                    sqlCommand.Parameters.AddWithValue("@PhamViCM", String.IsNullOrEmpty(obj.PhamViCM) ? "" : obj.PhamViCM);

                    if (String.IsNullOrEmpty(obj.NgayCapCCHN))
                        sqlCommand.Parameters.Add("@NgayCapCCHN", SqlDbType.SmallDateTime).Value = DBNull.Value;
                    else
                        sqlCommand.Parameters.Add("@NgayCapCCHN", SqlDbType.SmallDateTime).Value = DateTime.ParseExact(obj.NgayCapCCHN, "dd/MM/yyyy", CultureInfo.CurrentCulture);

                    sqlCommand.Parameters.AddWithValue("@NoiCapCCHN", String.IsNullOrEmpty(obj.NoiCapCCHN) ? "" : obj.NoiCapCCHN);
                    sqlCommand.Parameters.AddWithValue("@DaGuiSo", int.Parse(obj.DaGuiSo));

                    if (String.IsNullOrEmpty(obj.NgayGuiSo))
                        sqlCommand.Parameters.Add("@NgayGuiSo", SqlDbType.SmallDateTime).Value = DBNull.Value;
                    else
                        sqlCommand.Parameters.Add("@NgayGuiSo", SqlDbType.SmallDateTime).Value = DateTime.ParseExact(obj.NgayGuiSo, "dd/MM/yyyy", CultureInfo.CurrentCulture);

                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            if (rowAffected > 0)
                return true;
            return false;
        }

        #endregion 3. Chứng chỉ hành nghề
    }
}