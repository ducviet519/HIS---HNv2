using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace System.App.Repositories.HCNS
{
    public class HDLD_Repo
    {
        public Dictionary<int, string> DS_LoaiHopDong(string connectionString)
        {
            string _query = "SELECT ID, ConName, ConSym FROM ContractType ORDER BY ID ASC";

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
                            lst.Add(int.Parse(reader["ID"].ToString()), reader["ConName"].ToString());
                        }
                    }
                }
            }
            return lst;
        }
        public IEnumerable<HDLD> DanhSachHopDong(string connectionString, HDLD obj)
        {
            string _query = @"
                    SELECT a.ID as ID, a.ConNo as ConNo, a.UserEnrollNumber as UserEnrollNumber, b.NgayTDLuong as NgayTDLuong, b.NgayVao as NgayVao, b.NgayNghi as NgayNghi, b.UserFullCode as UserFullCode, b.UserFullName as UserFullName, d.KhoaP as KhoaPhong, b.ChucDanh as ChucDanh, c.ConName as Ten_LoaiHD, NgayKy, NgayHH, NgayDG FROM (SELECT ID, ConNo, UserEnrollNumber, LoaiHD, NgayKy, NgayHH, NgayDG, NgayUD, ROW_NUMBER() OVER (PARTITION BY UserEnrollNumber ORDER BY NgayKy DESC) AS RN
                    FROM Contracts) a
	                    LEFT JOIN UserInfExt b ON a.UserEnrollNumber = b.UserEnrollNumber
	                    LEFT JOIN ContractType c ON a.LoaiHD = c.ID
	                    LEFT JOIN Depts d ON b.PhongKhoaHC = d.STT
                    WHERE 1 = 1 " + Filter(obj) + @"
                    ORDER BY NgayVao DESC";

            List<HDLD> lst = new List<HDLD>();

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
                            lst.Add(new HDLD()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                ConNo = reader["ConNo"].ToString(),
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                NgayTDLuong = String.IsNullOrEmpty(reader["NgayTDLuong"].ToString()) ? "" : DateTime.Parse(reader["NgayTDLuong"].ToString()).ToString("dd/MM/yyyy"),
                                NgayVao = String.IsNullOrEmpty(reader["NgayVao"].ToString()) ? "" : DateTime.Parse(reader["NgayVao"].ToString()).ToString("dd/MM/yyyy"),
                                NgayNghi = String.IsNullOrEmpty(reader["NgayNghi"].ToString()) ? "" : DateTime.Parse(reader["NgayNghi"].ToString()).ToString("dd/MM/yyyy"),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                KhoaPhong = reader["KhoaPhong"].ToString(),
                                ChucDanh = reader["ChucDanh"].ToString(),
                                Ten_LoaiHD = reader["Ten_LoaiHD"].ToString(),
                                NgayKy = String.IsNullOrEmpty(reader["NgayKy"].ToString()) ? "" : DateTime.Parse(reader["NgayKy"].ToString()).ToString("dd/MM/yyyy"),
                                NgayHH = String.IsNullOrEmpty(reader["NgayHH"].ToString()) ? "" : DateTime.Parse(reader["NgayHH"].ToString()).ToString("dd/MM/yyyy"),
                                NgayDG = String.IsNullOrEmpty(reader["NgayDG"].ToString()) ? "" : DateTime.Parse(reader["NgayDG"].ToString()).ToString("dd/MM/yyyy"),
                            });
                        }
                    }
                }
            }
            return lst;
        }
        public IEnumerable<HDLD> DanhSachHopDongDaKy(string connectionString, int userid)
        {
            string _query = @"
                SELECT a.ConNo as ConNo, c.ConName as Ten_LoaiHD, NgayKy, NgayHH, NgayDG
                FROM Contracts a
	                LEFT JOIN UserInfExt b ON a.UserEnrollNumber = b.UserEnrollNumber
	                LEFT JOIN ContractType c ON a.LoaiHD = c.ID
	                LEFT JOIN Depts d ON b.PhongKhoaHC = d.STT
                WHERE a.UserEnrollNumber = @UserEnrollNumber
                ORDER BY NgayKy";

            List<HDLD> lst = new List<HDLD>();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = userid;

                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new HDLD()
                            {
                                ConNo = reader["ConNo"].ToString(),
                                Ten_LoaiHD = reader["Ten_LoaiHD"].ToString(),
                                NgayKy = String.IsNullOrEmpty(reader["NgayKy"].ToString()) ? "" : DateTime.Parse(reader["NgayKy"].ToString()).ToString("dd/MM/yyyy"),
                                NgayHH = String.IsNullOrEmpty(reader["NgayHH"].ToString()) ? "" : DateTime.Parse(reader["NgayHH"].ToString()).ToString("dd/MM/yyyy"),
                                NgayDG = String.IsNullOrEmpty(reader["NgayDG"].ToString()) ? "" : DateTime.Parse(reader["NgayDG"].ToString()).ToString("dd/MM/yyyy"),
                            });
                        }
                    }
                }
            }
            return lst;
        }
        public HDLD HopDongInfo(string connectionString, HDLD obj)
        {
            string _query = @"SELECT ID, ConNo, a.UserEnrollNumber, UserFullName, UserFullCode, LoaiHD, NgayKy, NgayHH, NgayDG, NgayUD FROM Contracts a
                LEFT JOIN UserInfExt b ON a.UserEnrollNumber = b.UserEnrollNumber WHERE a.ID = " + obj.ID;

            HDLD o = new HDLD();

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
                            o.ID = int.Parse(reader["ID"].ToString());
                            o.ConNo = reader["ConNo"].ToString();
                            o.UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString());
                            o.UserFullCode = reader["UserFullCode"].ToString();
                            o.UserFullName = reader["UserFullName"].ToString();
                            o.LoaiHD = int.Parse(reader["LoaiHD"].ToString());
                            o.NgayKy = String.IsNullOrEmpty(reader["NgayKy"].ToString()) ? "" : DateTime.Parse(reader["NgayKy"].ToString()).ToString("dd/MM/yyyy");
                            o.NgayHH = String.IsNullOrEmpty(reader["NgayHH"].ToString()) ? "" : DateTime.Parse(reader["NgayHH"].ToString()).ToString("dd/MM/yyyy");
                            o.NgayDG = String.IsNullOrEmpty(reader["NgayDG"].ToString()) ? "" : DateTime.Parse(reader["NgayDG"].ToString()).ToString("dd/MM/yyyy");
                            o.NgayUD = String.IsNullOrEmpty(reader["NgayUD"].ToString()) ? "" : DateTime.Parse(reader["NgayUD"].ToString()).ToString("dd/MM/yyyy");
                        }
                    }
                }
            }
            return o;
        }
        private string Filter(HDLD obj)
        {
            string s = "";

            if (obj.ChucNang == "TimKiem")
            {
                if (!String.IsNullOrEmpty(obj.UserFullName))
                {
                    string filterBonus = ""; int returnRegex = 0;

                    bool regex = int.TryParse(obj.UserFullName, out returnRegex);

                    if (regex)
                    {
                        filterBonus = "OR b.UserEnrollNumber = '" + obj.UserFullName + "'";
                    }
                    s += " AND (UPPER(b.[UserFullName]) LIKE N'%" + obj.UserFullName.ToUpper() + "%' " + filterBonus + ")";
                }
                if (obj.LoaiHD > 0)
                {
                    s += " AND a.LoaiHD = " + obj.LoaiHD;
                }
                if (!String.IsNullOrEmpty(obj.KhoaPhong))
                {
                    s += " AND b.PhongKhoaHC = " + obj.KhoaPhong;
                }
                if (obj.TrangThai < 2)
                {
                    s += " AND b.DaNghi = " + obj.TrangThai;
                }

                //Nếu để trống thông tin ngày, sẽ tự động lấy từ tháng trước
                if (String.IsNullOrEmpty(obj.TK_NgayHetHanTu) && String.IsNullOrEmpty(obj.TK_NgayHetHanDen))
                {
                    //obj.TK_NgayHetHanTu = DateTime.UtcNow.AddHours(7).ToString("yyyy-MM-dd");
                    //s += " AND a.NgayHH > Convert(datetime, '" + obj.TK_NgayHetHanTu + "' )";
                }
                //Nếu chỉ điền từ ngày, sẽ tự động lấy từ ngày đó trở về hiện tại
                else if (!String.IsNullOrEmpty(obj.TK_NgayHetHanTu) && String.IsNullOrEmpty(obj.TK_NgayHetHanDen))
                {
                    obj.TK_NgayHetHanTu = DateTime.ParseExact(obj.TK_NgayHetHanTu, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    s += " AND a.NgayHH > Convert(datetime, '" + obj.TK_NgayHetHanTu + "' )";
                }
                //Nếu chỉ điền đến ngày, sẽ tự động lấy từ tháng trước > ngày đã chọn
                else if (String.IsNullOrEmpty(obj.TK_NgayHetHanTu) && !String.IsNullOrEmpty(obj.TK_NgayHetHanDen))
                {
                    obj.TK_NgayHetHanTu = DateTime.UtcNow.AddHours(7).ToString("yyyy-MM-dd");
                    obj.TK_NgayHetHanDen = DateTime.ParseExact(obj.TK_NgayHetHanDen, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    s += " AND a.NgayHH < Convert(datetime, '" + obj.TK_NgayHetHanDen + "' )";
                }
                //Điền cả 2 ô từ ngày - đến ngày
                else
                {
                    obj.TK_NgayHetHanTu = DateTime.ParseExact(obj.TK_NgayHetHanTu, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    obj.TK_NgayHetHanDen = DateTime.ParseExact(obj.TK_NgayHetHanDen, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                    s += " AND (a.NgayHH BETWEEN '" + obj.TK_NgayHetHanTu + "' AND '" + obj.TK_NgayHetHanDen + "')";
                }
            }
            else
            {
                s += "AND (ISNULL(a.NgayHH , '') = '' OR a.NgayHH > GETDATE())";
            }

            return s;
        }
        public HDLD ThongTinHDLD(string connectionString, HDLD obj)
        {
            string _query = @"
                    SELECT a.ID as ID, a.ConNo as ConNo, a.UserEnrollNumber as UserEnrollNumber, b.NgayTDLuong as NgayTDLuong, b.NgayVao as NgayVao, b.NgayNghi as NgayNghi, b.UserFullCode as UserFullCode, b.UserFullName as UserFullName, d.KhoaP as KhoaPhong, b.ChucDanh as ChucDanh, a.LoaiHD, c.ConName as Ten_LoaiHD, NgayKy, NgayHH, NgayDG FROM (SELECT ID, ConNo, UserEnrollNumber, LoaiHD, NgayKy, NgayHH, NgayDG, NgayUD, ROW_NUMBER() OVER (PARTITION BY UserEnrollNumber ORDER BY NgayKy DESC) AS RN
                    FROM Contracts) a
	                    LEFT JOIN UserInfExt b ON a.UserEnrollNumber = b.UserEnrollNumber
	                    LEFT JOIN ContractType c ON a.LoaiHD = c.ID
	                    LEFT JOIN Depts d ON b.PhongKhoaHC = d.STT
                    WHERE 1 = 1 " + Filter(obj) + @"
                    ORDER BY ID";

            HDLD hdld = new HDLD();

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
                            hdld.ID = int.Parse(reader["ID"].ToString());
                            hdld.ConNo = reader["ConNo"].ToString();
                            hdld.UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString());
                            hdld.NgayTDLuong = String.IsNullOrEmpty(reader["NgayTDLuong"].ToString()) ? "" : DateTime.Parse(reader["NgayTDLuong"].ToString()).ToString("dd/MM/yyyy");
                            hdld.NgayVao = String.IsNullOrEmpty(reader["NgayVao"].ToString()) ? "" : DateTime.Parse(reader["NgayVao"].ToString()).ToString("dd/MM/yyyy");
                            hdld.NgayNghi = String.IsNullOrEmpty(reader["NgayNghi"].ToString()) ? "" : DateTime.Parse(reader["NgayNghi"].ToString()).ToString("dd/MM/yyyy");
                            hdld.UserFullCode = reader["UserFullCode"].ToString();
                            hdld.UserFullName = reader["UserFullName"].ToString();
                            hdld.KhoaPhong = reader["KhoaPhong"].ToString();
                            hdld.ChucDanh = reader["ChucDanh"].ToString();
                            hdld.LoaiHD = int.Parse(reader["LoaiHD"].ToString());
                            hdld.Ten_LoaiHD = reader["Ten_LoaiHD"].ToString();
                            hdld.NgayKy = String.IsNullOrEmpty(reader["NgayKy"].ToString()) ? "" : DateTime.Parse(reader["NgayKy"].ToString()).ToString("dd/MM/yyyy");
                            hdld.NgayHH = String.IsNullOrEmpty(reader["NgayHH"].ToString()) ? "" : DateTime.Parse(reader["NgayHH"].ToString()).ToString("dd/MM/yyyy");
                            hdld.NgayDG = String.IsNullOrEmpty(reader["NgayDG"].ToString()) ? "" : DateTime.Parse(reader["NgayDG"].ToString()).ToString("dd/MM/yyyy");
                        }
                    }
                }
            }
            return hdld;
        }
        public HDLD ThongTinHDLD(string connectionString, int id)
        {
            string _query = @"
                    SELECT ID, ConNo, UserEnrollNumber, LoaiHD, NgayKy, NgayHH, NgayDG, NgayUD, DaHuy
                    FROM Contracts a
                    WHERE ID = @ID";

            HDLD hdld = new HDLD();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            hdld.ID = int.Parse(reader["ID"].ToString());
                            hdld.ConNo = reader["ConNo"].ToString();
                            hdld.UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString());
                            hdld.LoaiHD = int.Parse(reader["LoaiHD"].ToString());
                            hdld.NgayKy = String.IsNullOrEmpty(reader["NgayKy"].ToString()) ? "" : DateTime.Parse(reader["NgayKy"].ToString()).ToString("dd/MM/yyyy");
                            hdld.NgayHH = String.IsNullOrEmpty(reader["NgayHH"].ToString()) ? "" : DateTime.Parse(reader["NgayHH"].ToString()).ToString("dd/MM/yyyy");
                            hdld.NgayDG = String.IsNullOrEmpty(reader["NgayDG"].ToString()) ? "" : DateTime.Parse(reader["NgayDG"].ToString()).ToString("dd/MM/yyyy");
                            hdld.DaHuy = int.Parse(reader["DaHuy"].ToString());
                        }
                    }
                }
            }
            return hdld;
        }
        public bool ThemMoiHopDong(string connectionString, HDLD obj)
        {
            string query = @"INSERT INTO Contracts (ConNo, NgayKy, NgayHH, NgayUD, NgayDG, LoaiHD, UserEnrollNumber)
                    VALUES (@ConNo, @NgayKy, @NgayHH, @NgayUD, @NgayDG, @LoaiHD, @UserEnrollNumber)";

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@ConNo", SqlDbType.VarChar).Value = obj.ConNo;
                    cmd.Parameters.Add("@LoaiHD", SqlDbType.SmallInt).Value = obj.LoaiHD;
                    cmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;

                    if (string.IsNullOrEmpty(obj.NgayKy))
                        cmd.Parameters.Add("@NgayKy", SqlDbType.Date).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NgayKy", SqlDbType.Date).Value = DateTime.ParseExact(obj.NgayKy, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (string.IsNullOrEmpty(obj.NgayHH))
                        cmd.Parameters.Add("@NgayHH", SqlDbType.Date).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NgayHH", SqlDbType.Date).Value = DateTime.ParseExact(obj.NgayHH, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (string.IsNullOrEmpty(obj.NgayUD))
                        cmd.Parameters.Add("@NgayUD", SqlDbType.Date).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NgayUD", SqlDbType.Date).Value = DateTime.ParseExact(obj.NgayUD, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (string.IsNullOrEmpty(obj.NgayDG))
                        cmd.Parameters.Add("@NgayDG", SqlDbType.Date).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NgayDG", SqlDbType.Date).Value = DateTime.ParseExact(obj.NgayDG, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    rowAffected = cmd.ExecuteNonQuery();
                }
            }
            if (rowAffected > 0)
            {
                return true;
            }
            return false;
        }
        public bool CapNhatHopDong(string connectionString, HDLD obj)
        {
            string query = @"UPDATE Contracts SET ConNo = @ConNo, NgayKy = @NgayKy, NgayHH = @NgayHH, NgayUD = @NgayUD, NgayDG = @NgayDG, LoaiHD = @LoaiHD WHERE ID = @ID";

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = obj.ID;
                    cmd.Parameters.Add("@ConNo", SqlDbType.VarChar).Value = obj.ConNo;
                    cmd.Parameters.Add("@LoaiHD", SqlDbType.SmallInt).Value = obj.LoaiHD;

                    if (String.IsNullOrEmpty(obj.NgayKy))
                        cmd.Parameters.Add("@NgayKy", SqlDbType.Date).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NgayKy", SqlDbType.Date).Value = DateTime.ParseExact(obj.NgayKy, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (String.IsNullOrEmpty(obj.NgayHH))
                        cmd.Parameters.Add("@NgayHH", SqlDbType.Date).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NgayHH", SqlDbType.Date).Value = DateTime.ParseExact(obj.NgayHH, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (String.IsNullOrEmpty(obj.NgayUD))
                        cmd.Parameters.Add("@NgayUD", SqlDbType.Date).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NgayUD", SqlDbType.Date).Value = DateTime.ParseExact(obj.NgayUD, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (String.IsNullOrEmpty(obj.NgayDG))
                        cmd.Parameters.Add("@NgayDG", SqlDbType.Date).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NgayDG", SqlDbType.Date).Value = DateTime.ParseExact(obj.NgayDG, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    rowAffected = cmd.ExecuteNonQuery();
                }
            }
            if (rowAffected > 0)
            {
                return true;
            }
            return false;
        }
        public bool HuyHopDong(string connectionString, int id)
        {
            string _query = "UPDATE Contracts SET DaHuy = 1 WHERE ID = @ID";

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            return rowAffected > 0 ? true : false;
        }
        public bool KiemTraTrung(string connectionString, HDLD obj, out string message)
        {
            string _query = @"SELECT ID FROM Contracts WHERE ConNo = '" + obj.ConNo + "'";
            message = "";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var cmd = new SqlCommand(_query, sqlConnection))
                {
                    var o = cmd.ExecuteScalar();

                    if (o != null)
                    {
                        return true;
                    }
                }
            }
            message = "Đã có dữ liệu bị trùng.";
            return false;
        }
        public bool CapNhatExcel(string connectionString, List<HDLD> lstObj)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                foreach (var obj in lstObj)
                {
                    string QUERY = @"INSERT INTO Contracts(UserEnrollNumber, ConNo, LoaiHD, NgayKy, NgayHH, NgayDG, NgayUD)
                        VALUES (@UserEnrollNumber, @ConNo, @LoaiHD, @NgayKy, @NgayHH, @NgayDG, @NgayUD)";

                    using (var cmd = new SqlCommand(QUERY, sqlConnection))
                    {
                        cmd.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = obj.UserEnrollNumber;
                        cmd.Parameters.Add("@ConNo", SqlDbType.NVarChar).Value = obj.ConNo;
                        cmd.Parameters.Add("@LoaiHD", SqlDbType.Int).Value = obj.LoaiHD;

                        if (String.IsNullOrEmpty(obj.NgayKy))
                        {
                            cmd.Parameters.Add("@NgayKy", SqlDbType.DateTime).Value = DBNull.Value;
                        }
                        else
                        {
                            cmd.Parameters.Add("@NgayKy", SqlDbType.DateTime).Value = DateTime.ParseExact(obj.NgayKy, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                        }
                        if (String.IsNullOrEmpty(obj.NgayHH))
                        {
                            cmd.Parameters.Add("@NgayHH", SqlDbType.DateTime).Value = DBNull.Value;
                        }
                        else
                        {
                            cmd.Parameters.Add("@NgayHH", SqlDbType.DateTime).Value = DateTime.ParseExact(obj.NgayHH, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                        }
                        if (String.IsNullOrEmpty(obj.NgayDG))
                        {
                            cmd.Parameters.Add("@NgayDG", SqlDbType.DateTime).Value = DBNull.Value;
                        }
                        else
                        {
                            cmd.Parameters.Add("@NgayDG", SqlDbType.DateTime).Value = DateTime.ParseExact(obj.NgayDG, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                        }
                        cmd.Parameters.Add("@NgayUD", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            return true;
        }
        public bool UpdateNgay(string connectionString, int id, string ngay, string type)
        {
            string query = "";

            switch (type)
            {
                case "tdl":
                    query = "UPDATE UserInfExt SET NgayTDLuong = @Date WHERE UserEnrollNumber = @UserEnrollNumber";
                    break;

                case "ngayvao":
                    query = "UPDATE UserInfExt SET NgayVao = @Date WHERE UserEnrollNumber = @UserEnrollNumber";
                    break;

                case "nghiviec":
                    query = "UPDATE UserInfExt SET NgayNghi = @Date WHERE UserEnrollNumber = @UserEnrollNumber";
                    break;

                default:
                    break;
            }

            if (string.IsNullOrEmpty(query))
                return false;

            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.Add("@UserEnrollNumber", SqlDbType.Int).Value = id;
                    sqlCommand.Parameters.Add("@Date", SqlDbType.VarChar).Value = DateTime.ParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            return rowAffected > 0 ? true : false;
        }
        public string LayMaHopDong(string connectionString, int id)
        {
            string _query = "SELECT ConSym FROM ContractType WHERE ID = @ID";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                    object o = sqlCommand.ExecuteScalar();

                    if (o != null)
                        return o.ToString();
                    return "";
                }
            }
        }
    }
}