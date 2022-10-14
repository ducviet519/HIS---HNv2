using Dapper;
using System.App.Entities.HCNS;
using System.App.Repositories.Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace System.App.Repositories.HCNS
{
    public class NhanVien_Repo
    {
        public bool KiemTraThongTin(string connectionString, HCNS_NhanVien nv, out string message)
        {
            message = "";
            int rowAffected = 0;

            if (nv != null)
            {
                string q = @"SELECT *
                    FROM [UserInfExt]
                    WHERE ((UserFullName = N'" + nv.UserFullName + @"' AND NgaySinh = convert(datetime,'" + nv.NgaySinh + @"',103))
                    OR (SoCMT = '" + nv.SoCMT + @"')
                    AND (UserEnrollNumber <> '" + nv.UserEnrollNumber + "')";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(q, sqlConnection))
                    {
                        object o = sqlCommand.ExecuteScalar();

                        if (o != null)
                            rowAffected = int.Parse(o.ToString());
                    }
                }

                if (rowAffected > 0)
                {
                    message = "Thông tin Họ tên, Ngày sinh hoặc số CMT đã bị trùng, vui lòng kiểm tra lại";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                message = "Lỗi khởi tạo đối tượng.";
                return false;
            }
        }
        public IEnumerable<HCNS_NhanVien> DanhSachNhanVien(string connectionString, HCNS_NhanVien nv = null)
        {
            List<HCNS_NhanVien> lst = new List<HCNS_NhanVien>();

            string filter = String.Empty;

            if (nv != null)
            {
                if (!String.IsNullOrEmpty(nv.UserFullName))
                {
                    string filterBonus = "";
                    int returnRegex = 0;
                    bool regex = int.TryParse(nv.UserFullName, out returnRegex);
                    if (regex)
                    {
                        filterBonus = "OR a.UserEnrollNumber = '" + nv.UserFullName + "'";
                    }
                    //filter += "AND (UPPER(a.UserFullName) LIKE UPPER(N'%" + nv.UserFullName + "%') OR a.UserFullCode LIKE N'%" + nv.UserFullName + "%')";
                    filter += " AND (UPPER(a.UserFullName) LIKE UPPER(N'%" + nv.UserFullName + "%') " + filterBonus + ")";
                }
                if (nv.KhoaPhong > 0)
                {
                    filter += " AND a.PhongKhoa = " + nv.KhoaPhong;
                }
                if (nv.PhongKhoaHC > 0)
                {
                    filter += " AND a.PhongKhoaHC = " + nv.PhongKhoaHC;
                }
                if (nv.TrangThai < 2)
                {
                    filter += " AND ISNULL(DaNghi, 0) = " + nv.TrangThai;
                }

                if (!String.IsNullOrEmpty(nv.SoGioLamViec) && nv.SoGioLamViec != "Full")
                {
                    filter += " AND a.SoGioLV = '" + nv.SoGioLamViec + "'";
                }
            }
            else
            {
                filter += " AND ISNULL(DaNghi, 0) = 0";
            }
            string query = @"SELECT a.UserEnrollNumber AS UserEnrollNumber ,a.UserFullCode AS UserFullCode ,(IsNull(a.TATitle,'') + ' ' + a.UserFullName) AS UserFullName ,a.ChucDanh, IsNull(b.KhoaP,'') AS PhongKhoaHC ,a.TAEmail AS TAEmail ,a.SDTNB, SDT1 ,SDT2, a.Email, a.LoaiNV, a.NgayVao, a.DaNghi, r.Description as NoiChamCong,ISNULL(a.TinhLuong,0) AS TinhLuong 
                        FROM UserInfExt a
	                        LEFT JOIN Depts b ON a.PhongKhoaHC = b.STT
                            LEFT JOIN RelationDept r ON a.PhongKhoa = r.ID
                        WHERE (1 = 1) " + filter + " ORDER BY UserEnrollNumber DESC";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new HCNS_NhanVien()
                            {
                                UserEnrollNumber = String.IsNullOrEmpty(reader["UserEnrollNumber"].ToString()) ? 0 : int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                ChucDanh = reader["ChucDanh"].ToString(),
                                Ten_PhongKhoaHC = reader["PhongKhoaHC"].ToString(),
                                Ten_KhoaPhong = reader["NoiChamCong"].ToString(),
                                SDT1 = reader["SDT1"].ToString(),
                                SDT2 = reader["SDT2"].ToString(),
                                TAEmail = reader["TAEmail"].ToString(),
                                SDTNB = String.IsNullOrEmpty(reader["SDTNB"].ToString()) ? 0 : int.Parse(reader["SDTNB"].ToString()),
                                Email = reader["Email"].ToString(),
                                LoaiNV = reader["LoaiNV"].ToString(),
                                NgayVao = String.IsNullOrEmpty(reader["NgayVao"].ToString()) ? "" : DateTime.Parse(reader["NgayVao"].ToString()).ToString("dd/MM/yyyy"),
                                DaNghi = reader["DaNghi"].ToString(),
                                TinhLuong = int.Parse(reader["TinhLuong"].ToString())
                            });
                        }
                    }
                }

                sqlConnection.Close();
            }

            return lst;
        }
        public Dictionary<string, string> DanhSachKhoaPhongHC(string connectionString)
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();

            string q = @"SELECT [STT], [KhoaP] FROM [Depts] ORDER BY STT ASC";

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
                            lst.Add(reader["stt"].ToString(), reader["KhoaP"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public Dictionary<string, string> DanhSachKhoaPhong(string connectionString)
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();

            string q = @"SELECT [ID], [Description] FROM [RelationDept] WHERE ID > 2 ORDER BY Description";

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
                            lst.Add(reader["ID"].ToString(), reader["Description"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public Dictionary<string, string> DanhSachNoiAn(string connectionString)
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();

            string q = @"SELECT [ID], [PLACE] FROM [EatPlace] order by ID";

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
                            lst.Add(reader["ID"].ToString(), reader["PLACE"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public IEnumerable<DiaDiemAn> DanhSachNoiAn_Index(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("param_Type", 8);

                return db.Query<DiaDiemAn>("sp_CheckEat_Info", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public Dictionary<int, string> DoiTuongCC(string connectionString)
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();

            string q = @"SELECT [IDT], [TitleName] FROM [Title]";

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
                            lst.Add(int.Parse(reader["IDT"].ToString()), reader["TitleName"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public HCNS_NhanVien TimThongTinNhanVien(string connectionString, HCNS_NhanVien nv)
        {
            string filter = "";

            if (!string.IsNullOrEmpty(nv.TaiKhoan))
            {
                filter += " AND a.UPN = '" + nv.TaiKhoan + "'";
            }
            else
            {
                filter += " AND a.UserEnrollNumber = " + nv.UserEnrollNumber;
            }

            string query = @"SELECT a.UserEnrollNumber, a.UserFullCode, a.UserFullName, a.ChucDanh, a.Email, a.SDT1, a.SDT2, a.SDTNB, a.SoGioLV, a.NgayVao, a.PhongKhoa, a.PhongKhoaHC, a.NhomChucDanh, a.Lead, a.SaEmail, a.LoaiNV,  a.UType, 
                        a.NoiSinh, a.NoiSinhCT, CONVERT(char(10), a.NgaySinh, 103) as NgaySinh, a.QuocTich, a.DanToc, a.TonGiao, a.LoaiCMT, a.SoCMT, CONVERT(char(10), a.CapNgay, 103) as CapNgay, a.NoiCap, a.TATitle, a.GioiTinh, a.UType as DoiTuong,
                        a.QGThT, a.TinhThT, a.QuanThT, a.PhuongThT, a.DcThT,
                        a.QGTT, a.TinhTT, a.QuanTT, a.PhuongTT, a.DcTT,
                        a.TTHN as TTHonNhan, a.SoTK as SoTK1, a.NganHang as NganHang1, a.SoTK2, a.NganHang2, a.TTSK, a.MSTCN, CONVERT(char(10), x1.SCD, 103) as NgayTDLuong, CONVERT(char(10), a.NgayPC, 103) as NgayPC, a.AnhDaiDien, a.NgayNghi, a.NgayLVCC, a.LyDoNghi, a.DaNghi, ISNULL(a.TinhLuong,0) AS TinhLuong,
                        a.SoBHXH, a.SoTheBHXH, a.NgayTGBHXH, a.LoaiDongBHXH, a.MucDongBHXH, a.UPN, b.ID_EatPlace as NoiAn, CONVERT(char(10), a.NgayTinhPhep, 103) as NgayTinhPhep, a.CongDoan
                        FROM UserInfExt a
						left join CheckEat_Print b on a.UserEnrollNumber = b.UserEnrollNumber
                        outer apply
	                    (
		                    SELECT TOP 1 SCD FROM dbo.SalaryChange (nolock) WHERE UserEnrollNumber = a.UserEnrollNumber ORDER BY SCD DESC
	                    ) as x1
                        WHERE 1 = 1" + filter;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            nv.UserEnrollNumber = String.IsNullOrEmpty(reader["UserEnrollNumber"].ToString()) ? 0 : int.Parse(reader["UserEnrollNumber"].ToString());
                            nv.UserFullCode = reader["UserFullCode"].ToString();
                            nv.UserFullName = reader["UserFullName"].ToString();
                            nv.ChucDanh = reader["ChucDanh"].ToString();
                            nv.Email = reader["Email"].ToString();
                            nv.SDT1 = reader["SDT1"].ToString();
                            nv.SDT2 = reader["SDT2"].ToString();
                            nv.SDTNB = String.IsNullOrEmpty(reader["SDTNB"].ToString()) ? 0 : int.Parse(reader["SDTNB"].ToString());
                            nv.SoGioLamViec = reader["SoGioLV"].ToString();
                            nv.NgayVao = String.IsNullOrEmpty(reader["NgayVao"].ToString()) ? "" : DateTime.Parse(reader["NgayVao"].ToString()).ToString("dd/MM/yyyy");
                            nv.KhoaPhong = String.IsNullOrEmpty(reader["PhongKhoa"].ToString()) ? 0 : int.Parse(reader["PhongKhoa"].ToString());
                            nv.PhongKhoaHC = String.IsNullOrEmpty(reader["PhongKhoaHC"].ToString()) ? 0 : int.Parse(reader["PhongKhoaHC"].ToString());
                            nv.DTCC = int.Parse(reader["NhomChucDanh"].ToString());
                            nv.QuanLy = String.IsNullOrEmpty(reader["Lead"].ToString()) ? 0 : int.Parse(reader["Lead"].ToString());
                            nv.EmailNhanLuong = String.IsNullOrEmpty(reader["SaEmail"].ToString()) ? 0 : int.Parse(reader["SaEmail"].ToString());
                            nv.LoaiNV = reader["LoaiNV"].ToString();
                            nv.NoiSinh = reader["NoiSinh"].ToString();
                            nv.NoiSinhCT = reader["NoiSinhCT"].ToString();
                            nv.NgaySinh = reader["NgaySinh"].ToString();
                            nv.QuocTich = reader["QuocTich"].ToString();
                            nv.DanToc = reader["DanToc"].ToString();
                            nv.TonGiao = reader["TonGiao"].ToString();
                            nv.LoaiCMT = reader["LoaiCMT"].ToString();
                            nv.SoCMT = reader["SoCMT"].ToString();
                            nv.CapNgay = reader["CapNgay"].ToString();
                            nv.NoiCap = reader["NoiCap"].ToString();
                            nv.TATitle = reader["TATitle"].ToString();
                            nv.GioiTinh = reader["GioiTinh"].ToString();
                            nv.DoiTuong = reader["DoiTuong"].ToString();
                            nv.QGThT = !String.IsNullOrEmpty(reader["QGThT"].ToString()) ? reader["QGThT"].ToString() : "0";
                            nv.TinhThT = !String.IsNullOrEmpty(reader["TinhThT"].ToString()) ? reader["TinhThT"].ToString() : "0";
                            nv.QuanThT = !String.IsNullOrEmpty(reader["QuanThT"].ToString()) ? reader["QuanThT"].ToString() : "0";
                            nv.PhuongThT = !String.IsNullOrEmpty(reader["PhuongThT"].ToString()) ? reader["PhuongThT"].ToString() : "0";
                            nv.DcThT = reader["DcThT"].ToString();
                            nv.QGTT = !String.IsNullOrEmpty(reader["QGTT"].ToString()) ? reader["QGTT"].ToString() : "0";
                            nv.TinhTT = !String.IsNullOrEmpty(reader["TinhTT"].ToString()) ? reader["TinhTT"].ToString() : "0";
                            nv.QuanTT = !String.IsNullOrEmpty(reader["QuanTT"].ToString()) ? reader["QuanTT"].ToString() : "0";
                            nv.PhuongTT = !String.IsNullOrEmpty(reader["PhuongTT"].ToString()) ? reader["PhuongTT"].ToString() : "0";
                            nv.DcTT = reader["DcTT"].ToString();
                            nv.TTHonNhan = reader["TTHonNhan"].ToString();
                            nv.SoTK1 = reader["SoTK1"].ToString();
                            nv.NganHang1 = reader["NganHang1"].ToString();
                            nv.SoTK2 = reader["SoTK2"].ToString();
                            nv.NganHang2 = reader["NganHang2"].ToString();
                            nv.TTSK = reader["TTSK"].ToString();
                            nv.MSTCN = reader["MSTCN"].ToString();
                            nv.NgayThayDoiLuong = reader["NgayTDLuong"].ToString();
                            nv.NgayPhuCap = reader["NgayPC"].ToString();
                            nv.AnhDaiDien = reader["AnhDaiDien"].ToString();
                            nv.NgayNghi = String.IsNullOrEmpty(reader["NgayNghi"].ToString()) ? "" : DateTime.Parse(reader["NgayNghi"].ToString()).ToString("dd/MM/yyyy");
                            nv.NgayLVCC = String.IsNullOrEmpty(reader["NgayLVCC"].ToString()) ? "" : DateTime.Parse(reader["NgayLVCC"].ToString()).ToString("dd/MM/yyyy");
                            nv.LyDoNghi = reader["LyDoNghi"].ToString();
                            nv.DaNghi = reader["DaNghi"].ToString();
                            nv.SoBHXH = reader["SoBHXH"].ToString();
                            nv.SoTheBHXH = reader["SoTheBHXH"].ToString();
                            nv.NgayTGBHXH = String.IsNullOrEmpty(reader["NgayTGBHXH"].ToString()) ? "" : DateTime.Parse(reader["NgayTGBHXH"].ToString()).ToString("dd/MM/yyyy");
                            nv.MucDongBHXH = reader["MucDongBHXH"].ToString();
                            nv.LoaiDongBHXH = reader["LoaiDongBHXH"].ToString();
                            nv.UPN = reader["UPN"] == null ? "" : reader["UPN"].ToString();
                            nv.UType = string.IsNullOrEmpty(reader["UType"].ToString()) ? 0 : int.Parse(reader["UType"].ToString());
                            nv.TinhLuong = int.Parse(reader["TinhLuong"].ToString());
                            nv.NoiAn = String.IsNullOrEmpty(reader["NoiAn"].ToString()) ? 0 : int.Parse(reader["NoiAn"].ToString());
                            nv.NgayTinhPhep = reader["NgayTinhPhep"] == null ? "" : reader["NgayTinhPhep"].ToString();
                            nv.CongDoan = int.Parse(reader["CongDoan"].ToString());
                        }
                    }
                }

                sqlConnection.Close();
            }
            return nv;
        }
        public bool ThemNhanVienMoi(string connectionString, HCNS_NhanVien nv)
        {


            //string q_insert = @"INSERT INTO UserInfExt (UserEnrollNumber, UserFullCode, UserFullName, ChucDanh, Email, SDT1, SDTNB, SoGioLV, NgayVao, PhongKhoa, PhongKhoaHC, NhomChucDanh, Lead, SaEmail, LoaiNV, DaNghi, NhanVienMoi, TinhLuong)
            //VALUES (@UserEnrollNumber, @UserFullCode, @UserFullName, @ChucDanh, @Email, @SDT1, @SDTNB, @SoGioLV, @NgayVao, @PhongKhoa, @PhongKhoaHC, @NhomChucDanh, @Lead, @SaEmail, @LoaiNV, @DaNghi, @NhanVienMoi, @TinhLuong)";
            string q_nextID = "SELECT MAX(UserEnrollNumber) + 1 AS NEW_ID FROM [UserInfExt] Where UserEnrollNumber < 8000";
            int rowAffected = 0, new_id = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(q_nextID, sqlConnection))
                {
                    object o = sqlCommand.ExecuteScalar();

                    if (o != null) int.TryParse(o.ToString(), out new_id);
                }
                if (new_id > 0)
                {
                    //using (var sqlCommand = new SqlCommand(q_insert, sqlConnection))
                    //{
                    //    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", new_id.ToString("0000"));
                    //    sqlCommand.Parameters.AddWithValue("@UserFullCode", nv.LoaiNV + "" + new_id.ToString("0000"));
                    //    sqlCommand.Parameters.AddWithValue("@UserFullName", String.IsNullOrEmpty(nv.UserFullName) ? "" : nv.UserFullName);
                    //    sqlCommand.Parameters.AddWithValue("@ChucDanh", String.IsNullOrEmpty(nv.ChucDanh) ? "" : nv.ChucDanh);
                    //    sqlCommand.Parameters.AddWithValue("@Email", String.IsNullOrEmpty(nv.Email) ? "" : nv.Email);
                    //    sqlCommand.Parameters.AddWithValue("@SDT1", String.IsNullOrEmpty(nv.SDT1) ? "" : nv.SDT1);
                    //    sqlCommand.Parameters.AddWithValue("@SDT2", String.IsNullOrEmpty(nv.SDT2) ? "" : nv.SDT2);
                    //    sqlCommand.Parameters.AddWithValue("@SDTNB", nv.SDTNB);
                    //    sqlCommand.Parameters.AddWithValue("@SoGioLV", nv.SoGioLamViec);

                    //    if (String.IsNullOrEmpty(nv.NgayVao))
                    //    {
                    //        sqlCommand.Parameters.Add("@NgayVao", SqlDbType.DateTime).Value = DBNull.Value;
                    //    }
                    //    else
                    //    {
                    //        sqlCommand.Parameters.AddWithValue("@NgayVao", DateTime.ParseExact(nv.NgayVao, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    //    }

                    //    sqlCommand.Parameters.AddWithValue("@PhongKhoa", nv.KhoaPhong);
                    //    sqlCommand.Parameters.AddWithValue("@PhongKhoaHC", nv.PhongKhoaHC);
                    //    sqlCommand.Parameters.AddWithValue("@NhomChucDanh", nv.DTCC);
                    //    sqlCommand.Parameters.AddWithValue("@Lead", nv.QuanLy);
                    //    sqlCommand.Parameters.AddWithValue("@SaEmail", nv.EmailNhanLuong);
                    //    sqlCommand.Parameters.AddWithValue("@LoaiNV", String.IsNullOrEmpty(nv.LoaiNV) ? "" : nv.LoaiNV);
                    //    sqlCommand.Parameters.AddWithValue("@DaNghi", 0);
                    //    sqlCommand.Parameters.AddWithValue("@NhanVienMoi", 1);
                    //    sqlCommand.Parameters.AddWithValue("@TinhLuong", nv.TinhLuong);
                    //    rowAffected = sqlCommand.ExecuteNonQuery();
                    //}
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        if (db.State == ConnectionState.Closed)
                            db.Open();

                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("UserEnrollNumber", new_id.ToString("0000"));
                        parameters.Add("UserFullCode", nv.LoaiNV + "" + new_id.ToString("0000"));
                        parameters.Add("UserFullName", String.IsNullOrEmpty(nv.UserFullName) ? "" : nv.UserFullName);
                        parameters.Add("ChucDanh", String.IsNullOrEmpty(nv.ChucDanh) ? "" : nv.ChucDanh);
                        parameters.Add("Email", String.IsNullOrEmpty(nv.Email) ? "" : nv.Email);
                        parameters.Add("SDT1", String.IsNullOrEmpty(nv.SDT1) ? "" : nv.SDT1);
                        parameters.Add("SDT2", String.IsNullOrEmpty(nv.SDT2) ? "" : nv.SDT2);
                        parameters.Add("SDTNB", nv.SDTNB);
                        parameters.Add("SoGioLV", nv.SoGioLamViec);
                        parameters.Add("NgayVao", String.IsNullOrEmpty(nv.NgayVao) ? DateTime.ParseExact(DateTime.Now.AddHours(7).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact(nv.NgayVao, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                        parameters.Add("PhongKhoa", nv.KhoaPhong);
                        parameters.Add("PhongKhoaHC", nv.PhongKhoaHC);
                        parameters.Add("NhomChucDanh", nv.DTCC);
                        parameters.Add("Lead", nv.QuanLy);
                        parameters.Add("SaEmail", nv.EmailNhanLuong);
                        parameters.Add("LoaiNV", String.IsNullOrEmpty(nv.LoaiNV) ? "" : nv.LoaiNV);
                        parameters.Add("DaNghi", 0);
                        parameters.Add("NhanVienMoi", 1);
                        parameters.Add("TinhLuong", nv.TinhLuong);
                        parameters.Add("UserEnrollName", String.IsNullOrEmpty(nv.UserFullName) ? "" : StaticHelper.LocDau(nv.UserFullName.Trim()));
                        parameters.Add("UserHireDay", DateTime.UtcNow.AddHours(7));
                        parameters.Add("UserIDTitle", 0);
                        parameters.Add("UserSex", 0);
                        parameters.Add("UserIDD", nv.KhoaPhong);
                        parameters.Add("NoiAn", nv.NoiAn);
                        parameters.Add("DoiTuong", nv.UType);
                        parameters.Add("param", "themmoi");
                        rowAffected = db.Execute("sp_NhanVienMoi", parameters, commandType: CommandType.StoredProcedure);
                    }


                    //string _insertAsync = @"
                    //    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
                    //    BEGIN TRANSACTION;
                    //    INSERT INTO UserInfo (UserEnrollNumber,UserFullCode,UserFullName,UserEnrollName,UserCardNo,UserHireDay,UserIDTitle, UserSex, UserIDD, UserPhoto, UserPW, UserPrivilege, UserEnabled , UserGroup , UserTZ , UserAddress)
                    //        SELECT @UserEnrollNumber ,@UserFullCode ,@UserFullName ,@UserEnrollName ,'0000000000' ,@UserHireDay ,@UserIDTitle ,@UserSex ,@UserIDD, UserPhoto, UserPW, UserPrivilege, UserEnabled, UserGroup, UserTZ, UserAddress FROM UserInfo WHERE UserEnrollNumber = 2;
                    //    INSERT INTO ChamAn.dbo.UserInfo (UserEnrollNumber ,UserFullCode ,UserFullName ,UserEnrollName ,UserCardNo ,UserHireDay ,UserIDTitle ,UserSex ,UserIDD ,UserPhoto ,UserPW ,UserPrivilege ,UserEnabled ,UserGroup ,UserTZ ,UserAddress)
                    //        SELECT @UserEnrollNumber ,@UserFullCode ,@UserFullName ,@UserEnrollName ,'0000000000' ,@UserHireDay ,@UserIDTitle ,@UserSex ,@UserIDD ,UserPhoto ,UserPW ,UserPrivilege ,UserEnabled ,UserGroup ,UserTZ ,UserAddress FROM ChamAn.dbo.UserInfo WHERE UserEnrollNumber = 2;
                    //    INSERT INTO CheckEat_Print (UserEnrollNumber ,ID_EatPlace)
                    //        SELECT @UserEnrollNumber ,@NoiAn;
                    //    COMMIT TRANSACTION;
                    //";
                    //using (var sqlCommand = new SqlCommand(_insertAsync, sqlConnection))
                    //{
                    //    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", new_id.ToString("0000"));
                    //    sqlCommand.Parameters.AddWithValue("@UserFullCode", nv.LoaiNV + " " + new_id.ToString("0000"));
                    //    sqlCommand.Parameters.AddWithValue("@UserFullName", String.IsNullOrEmpty(nv.UserFullName) ? "" : nv.UserFullName);
                    //    sqlCommand.Parameters.AddWithValue("@UserEnrollName", String.IsNullOrEmpty(nv.UserFullName) ? "" : StaticHelper.LocDau(nv.UserFullName.Trim()));
                    //    sqlCommand.Parameters.AddWithValue("@UserHireDay", DateTime.UtcNow.AddHours(7));
                    //    sqlCommand.Parameters.AddWithValue("@UserIDTitle", 0);
                    //    sqlCommand.Parameters.AddWithValue("@UserSex", 0);
                    //    sqlCommand.Parameters.AddWithValue("@UserIDD", nv.KhoaPhong);
                    //    sqlCommand.Parameters.AddWithValue("@NoiAn", nv.NoiAn);

                    //    rowAffected = sqlCommand.ExecuteNonQuery();
                    //}
                }
            }
            return rowAffected > 0 ? true : false;
        }
        public bool SuaThongTinNhanVienMoi(string connectionString, HCNS_NhanVien nv)
        {
            int rowAffected = 0;

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UserEnrollNumber", nv.UserEnrollNumber);
                parameters.Add("UserFullCode", (nv.LoaiNV + "" + nv.UserEnrollNumber.ToString("0000")));
                parameters.Add("UserFullName", String.IsNullOrEmpty(nv.UserFullName) ? "" : nv.UserFullName);
                parameters.Add("ChucDanh", String.IsNullOrEmpty(nv.ChucDanh) ? "" : nv.ChucDanh);
                parameters.Add("Email", String.IsNullOrEmpty(nv.Email) ? "" : nv.Email);
                parameters.Add("SDT1", String.IsNullOrEmpty(nv.SDT1) ? "" : nv.SDT1);
                parameters.Add("SDT2", String.IsNullOrEmpty(nv.SDT2) ? "" : nv.SDT2);
                parameters.Add("SDTNB", nv.SDTNB);
                parameters.Add("SoGioLV", String.IsNullOrEmpty(nv.SoGioLamViec) ? "" : nv.SoGioLamViec);
                parameters.Add("NgayVao", String.IsNullOrEmpty(nv.NgayVao) ? (DateTime?)null : DateTime.ParseExact(nv.NgayVao, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                parameters.Add("PhongKhoa", nv.KhoaPhong);
                parameters.Add("PhongKhoaHC", nv.PhongKhoaHC);
                parameters.Add("NhomChucDanh", nv.DTCC);
                parameters.Add("Lead", nv.QuanLy);
                parameters.Add("SaEmail", nv.EmailNhanLuong);
                parameters.Add("LoaiNV", nv.LoaiNV);
                parameters.Add("TinhLuong", nv.TinhLuong);
                parameters.Add("UserEnrollName", String.IsNullOrEmpty(nv.UserFullName) ? "" : StaticHelper.LocDau(nv.UserFullName.Trim()));
                parameters.Add("UserIDD", nv.KhoaPhong);
                parameters.Add("NoiAn", nv.NoiAn);
                parameters.Add("DoiTuong", nv.UType);
                parameters.Add("param", "capnhat");
                rowAffected = db.Execute("sp_NhanVienMoi", parameters, commandType: CommandType.StoredProcedure);
            }
            return rowAffected > 0 ? true : false;
        }
        public bool XoaNhanVien(string connectionString, HCNS_NhanVien nv)
        {
            string q_delete = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;  BEGIN TRANSACTION;
                                DELETE UserInfExt WHERE UserEnrollNumber = @UserEnrollNumber
                                DELETE UserInfo WHERE UserEnrollNumber = @UserEnrollNumber
                                DELETE ChamAn.dbo.UserInfo WHERE UserEnrollNumber = @UserEnrollNumber
                                COMMIT TRANSACTION;";
            int rowAffected = 0;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(q_delete, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", nv.UserEnrollNumber);

                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }
            return rowAffected > 0 ? true : false;
        }
        public bool SuaTTCapNhatNV(string connectiontring, HCNS_NhanVien nv)
        {
            var rowAffected = 0;

            string q_update = @"UPDATE UserInfExt
                            SET NoiSinh = @NoiSinh, NoiSinhCT = @NoiSinhCT, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh,
                            UType = @DoiTuong, LoaiCMT = @LoaiCMT, SoCMT = @SoCMT, CapNgay = @CapNgay, NoiCap = @NoiCap,
                            TTHN = @TTHN, NgayPC = @NgayPC, TTSK = @TTSK, MSTCN = @MSTCN, TonGiao = @TonGiao, QuocTich = @QuocTich, DanToc = @DanToc,
                            SoTK = @SoTK1, NganHang = @NganHang1, SoTK2 = @SoTK2, NganHang2 = @NganHang2,
                            QGThT = @QGThT, TinhThT = @TinhThT, QuanThT = @QuanThT, PhuongThT = @PhuongThT, DcThT = @DcThT,
                            QGTT = @QGTT, TinhTT = @TinhTT, QuanTT = @QuanTT, PhuongTT = @PhuongTT, DcTT = @DcTT, TATitle = @TATitle,
                            SoBHXH = @SoBHXH, SoTheBHXH = @SoTheBHXH, NgayTGBHXH = @NgayTGBHXH, LoaiDongBHXH = @LoaiDongBHXH, 
                            MucDongBHXH = @MucDongBHXH, NgayTinhPhep = @NgayTinhPhep, CongDoan = @CongDoan
                            WHERE UserEnrollNumber = @UserEnrollNumber";
            using (var sqlConnection = new SqlConnection(connectiontring))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(q_update, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@NoiSinh", String.IsNullOrEmpty(nv.NoiSinh) ? "" : nv.NoiSinh);
                    sqlCommand.Parameters.AddWithValue("@NoiSinhCT", String.IsNullOrEmpty(nv.NoiSinhCT) ? "" : nv.NoiSinhCT.ToString());

                    if (String.IsNullOrEmpty(nv.NgaySinh))
                    {
                        sqlCommand.Parameters.Add("@NgaySinh", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        sqlCommand.Parameters.Add("@NgaySinh", SqlDbType.DateTime).Value = DateTime.ParseExact(nv.NgaySinh, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    sqlCommand.Parameters.AddWithValue("@GioiTinh", String.IsNullOrEmpty(nv.GioiTinh) ? "" : nv.GioiTinh);
                    sqlCommand.Parameters.AddWithValue("@DoiTuong", String.IsNullOrEmpty(nv.DoiTuong) ? "" : nv.DoiTuong);
                    sqlCommand.Parameters.AddWithValue("@LoaiCMT", String.IsNullOrEmpty(nv.LoaiCMT) ? "" : nv.LoaiCMT);
                    sqlCommand.Parameters.AddWithValue("@SoCMT", String.IsNullOrEmpty(nv.SoCMT) ? "" : nv.SoCMT.ToString());

                    if (String.IsNullOrEmpty(nv.CapNgay))
                    {
                        sqlCommand.Parameters.Add("@CapNgay", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        sqlCommand.Parameters.Add("@CapNgay", SqlDbType.DateTime).Value = DateTime.ParseExact(nv.CapNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    sqlCommand.Parameters.AddWithValue("@NoiCap", String.IsNullOrEmpty(nv.NoiCap) ? "" : nv.NoiCap);
                    sqlCommand.Parameters.AddWithValue("@TATitle", String.IsNullOrEmpty(nv.TATitle) ? "" : nv.TATitle);
                    sqlCommand.Parameters.AddWithValue("@TTHN", String.IsNullOrEmpty(nv.TTHonNhan) ? "" : nv.TTHonNhan);
                    sqlCommand.Parameters.AddWithValue("@QuocTich", String.IsNullOrEmpty(nv.QuocTich) ? "" : nv.QuocTich);
                    sqlCommand.Parameters.AddWithValue("@DanToc", String.IsNullOrEmpty(nv.DanToc) ? "" : nv.DanToc);

                    sqlCommand.Parameters.AddWithValue("@TonGiao", String.IsNullOrEmpty(nv.TonGiao) ? "" : nv.TonGiao);

                    if (String.IsNullOrEmpty(nv.NgayPhuCap))
                    {
                        sqlCommand.Parameters.Add("@NgayPC", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        sqlCommand.Parameters.Add("@NgayPC", SqlDbType.DateTime).Value = DateTime.ParseExact(nv.NgayPhuCap, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    sqlCommand.Parameters.AddWithValue("@TTSK", String.IsNullOrEmpty(nv.TTSK) ? "" : nv.TTSK.ToString());
                    sqlCommand.Parameters.AddWithValue("@MSTCN", String.IsNullOrEmpty(nv.MSTCN) ? "" : nv.MSTCN.ToString());
                    sqlCommand.Parameters.AddWithValue("@SoTK1", String.IsNullOrEmpty(nv.SoTK1) ? "" : nv.SoTK1.ToString());
                    sqlCommand.Parameters.AddWithValue("@NganHang1", String.IsNullOrEmpty(nv.NganHang1) ? "" : nv.NganHang1.ToString());
                    sqlCommand.Parameters.AddWithValue("@SoTK2", String.IsNullOrEmpty(nv.SoTK2) ? "" : nv.SoTK2.ToString());
                    sqlCommand.Parameters.AddWithValue("@NganHang2", String.IsNullOrEmpty(nv.NganHang2) ? "" : nv.NganHang2.ToString());
                    sqlCommand.Parameters.AddWithValue("@QGThT", nv.QGThT);
                    sqlCommand.Parameters.AddWithValue("@TinhThT", nv.TinhThT);
                    sqlCommand.Parameters.AddWithValue("@QuanThT", nv.QuanThT);
                    sqlCommand.Parameters.AddWithValue("@PhuongThT", nv.PhuongThT);
                    sqlCommand.Parameters.AddWithValue("@DcThT", String.IsNullOrEmpty(nv.DcThT) ? "" : nv.DcThT.ToString());
                    sqlCommand.Parameters.AddWithValue("@QGTT", nv.QGTT);
                    sqlCommand.Parameters.AddWithValue("@TinhTT", nv.TinhTT);
                    sqlCommand.Parameters.AddWithValue("@QuanTT", nv.QuanTT);
                    sqlCommand.Parameters.AddWithValue("@PhuongTT", nv.PhuongTT);
                    sqlCommand.Parameters.AddWithValue("@DcTT", String.IsNullOrEmpty(nv.DcTT) ? "" : nv.DcTT.ToString());
                    sqlCommand.Parameters.AddWithValue("@SoBHXH", String.IsNullOrEmpty(nv.SoBHXH) ? "" : nv.SoBHXH.ToString());
                    sqlCommand.Parameters.AddWithValue("@SoTheBHXH", String.IsNullOrEmpty(nv.SoTheBHXH) ? "" : nv.SoTheBHXH.ToString());
                    if (String.IsNullOrEmpty(nv.NgayTGBHXH))
                    {
                        sqlCommand.Parameters.Add("@NgayTGBHXH", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        sqlCommand.Parameters.Add("@NgayTGBHXH", SqlDbType.DateTime).Value = DateTime.ParseExact(nv.NgayTGBHXH, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    sqlCommand.Parameters.AddWithValue("@LoaiDongBHXH", String.IsNullOrEmpty(nv.LoaiDongBHXH) ? "" : nv.LoaiDongBHXH.ToString());
                    sqlCommand.Parameters.AddWithValue("@MucDongBHXH", String.IsNullOrEmpty(nv.MucDongBHXH) ? "" : nv.MucDongBHXH.ToString());
                    
                    //sqlCommand.Parameters.AddWithValue("@NgayTinhPhep", SqlDbType.DateTime).Value = string.IsNullOrEmpty(nv.NgayTinhPhep) ? (DateTime?)null : DateTime.ParseExact(nv.NgayTinhPhep, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (String.IsNullOrEmpty(nv.NgayTinhPhep))
                    {
                        sqlCommand.Parameters.Add("@NgayTinhPhep", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        sqlCommand.Parameters.Add("@NgayTinhPhep", SqlDbType.DateTime).Value = DateTime.ParseExact(nv.NgayTinhPhep, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    sqlCommand.Parameters.AddWithValue("@CongDoan", nv.CongDoan);
                    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", nv.UserEnrollNumber);

                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }
            return rowAffected > 0 ? true : false;
        }
        public IEnumerable<HCNS_NhanVien> DS_NhanVienBoSung(string connectionString, HCNS_NhanVien nv = null)
        {
            List<HCNS_NhanVien> lst = new List<HCNS_NhanVien>();

            string filter = String.Empty;

            if (nv != null)
            {
                int returnRegex = 0; string filterbonus = "";
                bool regex = int.TryParse(nv.UserFullName, out returnRegex);
                if (regex)
                {
                    filterbonus = "OR a.UserEnrollNumber = '" + nv.UserFullName + "'";
                }

                if (!String.IsNullOrEmpty(nv.UserFullName))
                {
                    filter += "AND (UPPER(a.UserFullName) LIKE UPPER(N'%" + nv.UserFullName + "%') " + filterbonus + " )";
                }
                if (nv.PhongKhoaHC > 0)
                {
                    filter += " AND a.PhongKhoaHC = " + nv.PhongKhoaHC;
                }
                if (nv.TrangThai < 2)
                {
                    filter += " AND ISNULL(DaNghi, 0) = " + nv.TrangThai;
                }
                if (!String.IsNullOrEmpty(nv.SoGioLamViec) && nv.SoGioLamViec != "Full")
                {
                    filter += " AND a.SoGioLV = '" + nv.SoGioLamViec + "'";
                }
            }
            else
            {
                filter += " AND ISNULL(DaNghi, 0) = 0";
            }

            string query = @"SELECT TOP 500
	            a.UserEnrollNumber AS UserEnrollNumber,
	            a.UserFullCode AS UserFullCode,
	            a.TATitle AS Title,
	            a.UserFullName AS UserFullName,
	            CONVERT(CHAR(10), a.NgaySinh, 103) AS NgaySinh,
	            c.TownName AS NoiSinh,
	            a.NoiSinhCT AS NoiSinhCT,
	            d.KhoaP AS PhongKhoaHC,
	            a.ChucDanh AS ChucDanh,
	            (CASE WHEN a.GioiTinh = 0 THEN N'Nam' WHEN a.GioiTinh = 1 THEN N'Nữ' ELSE '' END) AS GioiTinh,
	            (CASE WHEN a.LoaiCMT = 1 THEN N'CMT' WHEN a.LoaiCMT = 2 THEN N'Hộ chiếu' WHEN a.LoaiCMT = 3 THEN N'Thẻ căn cước' ELSE '' END) AS LoaiCMT,
	            a.SoCMT AS SoCMT,
	            CONVERT(CHAR(10), a.CapNgay, 103) AS CapNgay,
	            k.NoiCap AS NoiCap,
	            (CASE WHEN a.DaNghi = 0 THEN N'Đang công tác' WHEN a.DaNghi = 1 THEN N'Đã nghỉ' ELSE '' END) AS DaNghi,
	            a.SoTK AS SoTK1,
	            a.NganHang AS NganHang1,
	            a.SoTK2 AS SoTK2,
	            a.NganHang2 AS NganHang2,
	            l.TownName AS TinhThT,
	            m.Name AS QuanThT,
	            n.Name AS PhuongThT,
	            a.DcThT AS DcThT,
	            o.TownName AS TinhTT,
	            p.Name AS QuanTT,
	            q.Name AS PhuongTT,
	            a.DcTT AS DcTT,
	            CONVERT(CHAR(10), a.NgayPC, 103) AS NgayPC,
	            CONVERT(CHAR(10), x1.SCD, 103) AS NgayTDLuong,
	            a.SoGioLV AS SoGioLV,
	            e.PeopleName AS DanToc,
	            f.Nationality AS QuocTich,
	            g.Religion AS TonGiao,
	            a.TTSK AS TTSK,
	            t.MaSta AS TTHN,
	            u.Nationality AS QGThT,
	            v.Nationality AS QGTT,
	            x.TypeDes AS UType,
	            a.MSTCN AS MSTCN,
                a.SoBHXH, a.SoTheBHXH, a.NgayTGBHXH, a.LoaiDongBHXH, a.NgayTinhPhep
            FROM UserInfExt a
	            LEFT JOIN [Towns] c ON a.NoiSinh = c.ID
	            LEFT JOIN [Depts] d ON a.PhongKhoaHC = d.STT
	            LEFT JOIN [Peoples] e ON a.DanToc = e.ID
	            LEFT JOIN [Nationality] f ON a.QuocTich = f.ID
	            LEFT JOIN [Religion] g ON a.TonGiao = g.ID
	            LEFT JOIN [NoiCapCMT] k ON a.NoiCap = k.ID
	            LEFT JOIN [Towns] l ON a.TinhThT = l.ID
	            LEFT JOIN [Districts] m ON a.QuanThT = m.ID
	            LEFT JOIN [Wards] n ON a.PhuongThT = n.ID
	            LEFT JOIN [Towns] o ON a.TinhTT = o.ID
	            LEFT JOIN [Districts] p ON a.QuanTT = p.ID
	            LEFT JOIN [Wards] q ON a.PhuongTT = q.ID
	            LEFT JOIN [MaritalSta] t ON a.TTHN = t.ID
	            LEFT JOIN [Nationality] u ON a.QGThT = u.ID
	            LEFT JOIN [Nationality] v ON a.QGTT = v.ID
	            LEFT JOIN UserType x ON a.UType = x.ID
                outer apply
	            (
		            SELECT TOP 1 SCD FROM dbo.SalaryChange (nolock) WHERE UserEnrollNumber = a.UserEnrollNumber ORDER BY SCD DESC
	            ) as x1
            WHERE (a.UserEnrollNumber < 10000) " + filter + " ORDER BY UserEnrollNumber DESC";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new HCNS_NhanVien()
                            {
                                UserEnrollNumber = String.IsNullOrEmpty(reader["UserEnrollNumber"].ToString()) ? 0 : int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                TATitle = reader["Title"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                NgaySinh = reader["NgaySinh"].ToString(),
                                NoiSinh = reader["NoiSinh"].ToString(),
                                NoiSinhCT = reader["NoiSinhCT"].ToString(),
                                Ten_PhongKhoaHC = reader["PhongKhoaHC"].ToString(),
                                ChucDanh = reader["ChucDanh"].ToString(),
                                GioiTinh = reader["GioiTinh"].ToString(),
                                LoaiCMT = reader["LoaiCMT"].ToString(),
                                SoCMT = reader["SoCMT"].ToString(),
                                CapNgay = reader["CapNgay"].ToString(),
                                NoiCap = reader["NoiCap"].ToString(),
                                DaNghi = reader["DaNghi"].ToString(),
                                SoTK1 = reader["SoTK1"].ToString(),
                                NganHang1 = reader["NganHang1"].ToString(),
                                SoTK2 = reader["SoTK2"].ToString(),
                                NganHang2 = reader["NganHang2"].ToString(),
                                TinhThanh = reader["TinhThT"].ToString(),
                                Quan = reader["QuanThT"].ToString(),
                                Phuong = reader["PhuongThT"].ToString(),
                                DcTT = reader["DcTT"].ToString(),
                                NgayPhuCap = reader["NgayPC"].ToString(),
                                NgayThayDoiLuong = reader["NgayTDLuong"].ToString(),
                                SoGioLamViec = reader["SoGioLV"].ToString(),
                                DanToc = reader["DanToc"].ToString(),
                                QuocTich = reader["QuocTich"].ToString(),
                                TonGiao = reader["TonGiao"].ToString(),
                                ThongTinSucKhoe = reader["TTSK"].ToString(),
                                ThongTinHonNhan = reader["TTHN"].ToString(),
                                MSTCN = reader["MSTCN"].ToString(),
                                SoBHXH = reader["SoBHXH"].ToString(),
                                SoTheBHXH = reader["SoTheBHXH"].ToString(),
                                NgayTGBHXH = String.IsNullOrEmpty(reader["NgayTGBHXH"].ToString()) ? "" : DateTime.Parse(reader["NgayTGBHXH"].ToString()).ToString("dd/MM/yyyy"),
                                LoaiDongBHXH = reader["LoaiDongBHXH"].ToString(),
                                NgayTinhPhep = string.IsNullOrEmpty(reader["NgayTinhPhep"].ToString()) ? "" : DateTime.Parse(reader["NgayTinhPhep"].ToString()).ToString("dd/MM/yyyy")
                            });
                        }
                    }
                }

                sqlConnection.Close();
            }

            return lst;
        }
        public IEnumerable<HCNS_NhanVien> DS_NhanVienNghiViec(string connectionString, HCNS_NhanVien nv = null)
        {
            List<HCNS_NhanVien> lst = new List<HCNS_NhanVien>();

            string filter = String.Empty;

            if (nv != null)
            {
                if (!String.IsNullOrEmpty(nv.UserFullName))
                {
                    filter += "AND (UPPER(a.UserFullName) LIKE UPPER(N'%" + nv.UserFullName + "%') OR a.UserFullCode LIKE N'%" + nv.UserFullName + "%')";
                }
                if (nv.PhongKhoaHC > 0)
                {
                    filter += " AND a.PhongKhoaHC = " + nv.PhongKhoaHC;
                }
                if (nv.TrangThai > 0)
                {
                    filter += " AND ISNULL(DaNghi, 0) = " + nv.TrangThai;
                }
                else
                {
                    filter += " AND ISNULL(DaNghi, 0) = 1";
                }
            }
            else
            {
                filter += " AND ISNULL(DaNghi, 0) = 1";
            }

            string query = @"SELECT
	            a.UserEnrollNumber AS UserEnrollNumber,
	            a.UserFullCode AS UserFullCode,
	            a.TATitle AS Title,
	            a.UserFullName AS UserFullName,
	            CONVERT(CHAR(10), a.NgaySinh, 103) AS NgaySinh,
	            d.KhoaP AS PhongKhoaHC,
	            a.ChucDanh AS ChucDanh,
	            (CASE WHEN a.GioiTinh = 0 THEN N'Nam' WHEN a.GioiTinh = 1 THEN N'Nữ' ELSE '' END) AS GioiTinh,
	            CONVERT(CHAR(10), a.CapNgay, 103) AS CapNgay,
	            (CASE WHEN a.DaNghi = 0 THEN N'Đang công tác' WHEN a.DaNghi = 1 THEN N'Đã nghỉ' ELSE '' END) AS DaNghi,
	            x.TypeDes AS UType,
                a.NgayVao AS NgayVao ,a.NgayNghi AS NgayNghi ,a.NgayLVCC AS NgayLVCC ,a.LyDoNghi AS LyDoNghi
            FROM UserInfExt a
	            LEFT JOIN [Depts] d ON a.PhongKhoaHC = d.STT
	            LEFT JOIN UserType x ON a.UType = x.ID
            WHERE (a.UserEnrollNumber < 10000) " + filter + " ORDER BY UserEnrollNumber DESC";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new HCNS_NhanVien()
                            {
                                UserEnrollNumber = String.IsNullOrEmpty(reader["UserEnrollNumber"].ToString()) ? 0 : int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                TATitle = reader["Title"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                NgaySinh = reader["NgaySinh"].ToString(),
                                Ten_PhongKhoaHC = reader["PhongKhoaHC"].ToString(),
                                ChucDanh = reader["ChucDanh"].ToString(),
                                GioiTinh = reader["GioiTinh"].ToString(),
                                CapNgay = reader["CapNgay"].ToString(),
                                DaNghi = reader["DaNghi"].ToString(),
                                NgayVao = String.IsNullOrEmpty(reader["NgayVao"].ToString()) ? "-" : DateTime.Parse(reader["NgayVao"].ToString()).ToString("dd/MM/yyyy"),
                                NgayNghi = String.IsNullOrEmpty(reader["NgayNghi"].ToString()) ? "-" : DateTime.Parse(reader["NgayNghi"].ToString()).ToString("dd/MM/yyyy"),
                                NgayLVCC = String.IsNullOrEmpty(reader["NgayLVCC"].ToString()) ? "-" : DateTime.Parse(reader["NgayLVCC"].ToString()).ToString("dd/MM/yyyy"),
                                LyDoNghi = reader["LyDoNghi"].ToString()
                            });
                        }
                    }
                }

                sqlConnection.Close();
            }

            return lst;
        }
        public bool SuaTTNghiViec(string connectionString, HCNS_NhanVien nv)
        {
            int rowAffected = 0;
            string _query = "UPDATE UserInfExt SET NgayNghi = @NgayNghi, NgayLVCC = @NgayLVCC, LyDoNghi = @LyDoNghi, DaNghi = @DaNghi, TinhLuong = @TinhLuong WHERE UserEnrollNumber = @UserEnrollNumber";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    if (String.IsNullOrEmpty(nv.NgayNghi))
                    {
                        sqlCommand.Parameters.Add("@NgayNghi", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        sqlCommand.Parameters.Add("@NgayNghi", SqlDbType.DateTime).Value = DateTime.ParseExact(nv.NgayNghi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    if (String.IsNullOrEmpty(nv.NgayLVCC))
                    {
                        sqlCommand.Parameters.Add("@NgayLVCC", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        sqlCommand.Parameters.Add("@NgayLVCC", SqlDbType.DateTime).Value = DateTime.ParseExact(nv.NgayLVCC, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    sqlCommand.Parameters.AddWithValue("@LyDoNghi", String.IsNullOrEmpty(nv.LyDoNghi) ? "" : nv.LyDoNghi.ToString());
                    sqlCommand.Parameters.AddWithValue("@DaNghi", String.IsNullOrEmpty(nv.DaNghi) ? 0 : int.Parse(nv.DaNghi));
                    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", nv.UserEnrollNumber);
                    sqlCommand.Parameters.AddWithValue("@TinhLuong", nv.TinhLuong);
                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }
            return rowAffected > 0 ? true : false;
        }

        public bool XoaTTNghiViec(string connectionString, int userEnrollNumber)
        {
            int rowAffected = 0;
            string _query = "UPDATE UserInfExt SET NgayNghi = null, NgayLVCC = null, LyDoNghi = null, DaNghi = 0 WHERE UserEnrollNumber = @UserEnrollNumber";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var sqlCommand = new SqlCommand(_query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserEnrollNumber", userEnrollNumber);
                    rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }
            return rowAffected > 0 ? true : false;
        }
        public Dictionary<int, string> DS_NoiSinh(string connectionString)
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();

            string _query = @"SELECT ID, TownName FROM [Towns] WHERE ID > 0 ORDER BY ID ASC";

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
                            lst.Add(int.Parse(reader["ID"].ToString()), reader["TownName"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public Dictionary<int, string> DS_NoiCapCMT(string connectionString)
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();

            string _query = @"SELECT ID, NoiCap FROM NoiCapCMT ORDER BY ID ASC";

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
                            lst.Add(int.Parse(reader["ID"].ToString()), reader["NoiCap"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public Dictionary<int, string> DS_DoiTuong(string connectionString)
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();

            string _query = @"SELECT ID, TypeDes FROM UserType WHERE Enabled = 1 ORDER BY ID ASC";

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
                            lst.Add(int.Parse(reader["ID"].ToString()), reader["TypeDes"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public Dictionary<int, string> DS_DanToc(string connectionString)
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();

            string _query = @"SELECT ID,PeopleName FROM Peoples ORDER BY ID ASC";

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
                            lst.Add(int.Parse(reader["ID"].ToString()), reader["PeopleName"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public Dictionary<int, string> DS_QuocGia(string connectionString)
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();

            string _query = @"SELECT ID, Nationality FROM Nationality ORDER BY ID ASC";

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
                            lst.Add(int.Parse(reader["ID"].ToString()), reader["Nationality"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public Dictionary<int, string> DS_ThanhPho(string connectionString)
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();

            string _query = @"SELECT ID, TownName FROM Towns ORDER BY ID ASC";

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
                            lst.Add(int.Parse(reader["ID"].ToString()), reader["TownName"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public Dictionary<int, string> DS_QuanHuyen(string connectionString, int thanhpho)
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();

            string _query = @"SELECT ID, Name FROM Districts WHERE City = " + thanhpho;

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
                            lst.Add(int.Parse(reader["ID"].ToString()), reader["Name"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public Dictionary<int, string> DS_PhuongXa(string connectionString, int quanhuyen)
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();

            string _query = @"SELECT ID, Name FROM Wards WHERE Dis = " + quanhuyen;

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
                            lst.Add(int.Parse(reader["ID"].ToString()), reader["Name"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public Dictionary<int, string> DS_TonGiao(string connectionString)
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();

            string _query = @"SELECT ID, Religion FROM Religion ORDER BY ID ASC";

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
                            lst.Add(int.Parse(reader["ID"].ToString()), reader["Religion"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public bool Upload_AnhDaiDien(string connectionString, int id, string path)
        {
            string _query = @"UPDATE UserInfExt SET AnhDaiDien = '" + path + "' WHERE UserEnrollNumber = " + id;

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
            return rowAffected > 0 ? true : false;
        }
        public IEnumerable<DanhBaDienThoai> Ds_DanhBaDienThoai(string connectionString)
        {
            List<DanhBaDienThoai> lst = new List<DanhBaDienThoai>();

            string _query = @"SELECT a.ID , Building , b.FloorDes AS Floor ,c.KhoaP ,a.Pos ,a.Num, a.STT
                            FROM TelDir a
	                            LEFT JOIN Floors b ON a.Floor = b.ID
	                            LEFT JOIN Depts c ON a.Dept = c.STT
                            WHERE 1 = 1 ORDER BY Building, Floor, Num";

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
                            lst.Add(new DanhBaDienThoai()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Building = int.Parse(reader["Building"].ToString()),
                                Floor = reader["Floor"].ToString(),
                                Dept = reader["KhoaP"].ToString(),
                                Pos = reader["Pos"].ToString(),
                                Num = int.Parse(reader["Num"].ToString())
                            });
                        }
                    }
                }
            }

            return lst;
        }
        public IEnumerable<DanhBaDienThoai> DS_DanhBa_Search(string connectionString, HCNS_NhanVien obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_KhoaPhong", obj.KhoaPhong);
                parameters.Add("param_ToaNha", obj.ToaNha);
                parameters.Add("param_Tang", obj.Tang);
                parameters.Add("param_SoDienThoai", string.IsNullOrEmpty(obj.SoDienThoai) ? "-1" : obj.SoDienThoai);
                parameters.Add("param_Type", 4);

                return db.Query<DanhBaDienThoai>("sp_DanhBaDienThoai", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<DanhBaDienThoai_Excel> DS_DanhBa_Excel(string connectionString, HCNS_NhanVien obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_KhoaPhong", obj.KhoaPhong);
                parameters.Add("param_ToaNha", obj.ToaNha);
                parameters.Add("param_Tang", obj.Tang);
                parameters.Add("param_SoDienThoai", string.IsNullOrEmpty(obj.SoDienThoai) ? "-1" : obj.SoDienThoai);
                parameters.Add("param_Type", 9);

                return db.Query<DanhBaDienThoai_Excel>("sp_DanhBaDienThoai", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<DanhBaDienThoai> DSKhoaPhong(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_TYPE", 1);

                return db.Query<DanhBaDienThoai>("sp_DanhBaDienThoai", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<DanhBaDienThoai> DSToaNha(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_TYPE", 2);

                return db.Query<DanhBaDienThoai>("sp_DanhBaDienThoai", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<DanhBaDienThoai> DSTang(string connectionString)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_TYPE", 3);

                return db.Query<DanhBaDienThoai>("sp_DanhBaDienThoai", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_DanhBa(string connectionString, HCNS_NhanVien obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_KhoaPhong", obj.KhoaPhong);
                parameters.Add("param_ToaNha", obj.ToaNha);
                parameters.Add("param_Tang", obj.Tang);
                parameters.Add("param_ViTri", obj.ViTri);
                parameters.Add("param_SoDienThoai", obj.SoDienThoai);
                parameters.Add("param_TYPE", 5);
                return db.Execute("sp_DanhBaDienThoai", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Edit_DanhBaDienThoai(string connectionString, HCNS_NhanVien obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_KhoaPhong", obj.KhoaPhong);
                parameters.Add("param_ToaNha", obj.ToaNha);
                parameters.Add("param_Tang", obj.Tang);
                parameters.Add("param_ViTri", obj.ViTri);
                parameters.Add("param_SoDienThoai", obj.SoDienThoai);
                parameters.Add("param_ID", obj.ID);
                parameters.Add("param_TYPE", 7);
                return db.Execute("sp_DanhBaDienThoai", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_DanhBaDienThoai(string connectionString, HCNS_NhanVien obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_ID", obj.ID);
                parameters.Add("param_TYPE", 8);
                return db.Execute("sp_DanhBaDienThoai", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public HCNS_NhanVien Get_DanhBa(string connectionString, HCNS_NhanVien obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_ID", obj.ID);
                parameters.Add("param_TYPE", 6);

                return db.Query<HCNS_NhanVien>("sp_DanhBaDienThoai", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public bool CapNhatTT_Excel(string connectionString, List<HCNS_NhanVien_Excel> ex)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                foreach (var obj in ex)
                {
                    string QUERY_USERINEXT = @"
                        UPDATE UserInfExt SET
                            UserFullCode = @UserFullCode,
                            UserFullName = @UserFullName,
                            NgaySinh = @NgaySinh,
                            ChucDanh = @ChucDanh,
                            GioiTinh = @GioiTinh,
                            UType = (SELECT ID FROM UserType WHERE TypeDes = @DoiTuong),
                            SDT1 = @SDT1,
                            TAEmail = @TAEmail,
                            Email = @Email,
                            SoCMT = @SoCMT,
                            CapNgay = @CapNgay,
                            NoiCap = (SELECT ID FROM NoiCapCMT WHERE NoiCap = @NoiCap),
                            NoiSinhCT = @NoiSinhCT,
                            PhongKhoaHC = (SELECT STT FROM Depts WHERE KhoaP = @PhongKhoaHC),
                            NgayVao = @NgayVao,
                            SoTheBHXH = @SoTheBHXH,
                            TATitle = @TATitle,
                            SoGioLV = @SoGioLamViec,
                            DcThT = @DcThT,
                            DcTT = @DcTT,
                            DanToc = (SELECT ID FROM Peoples WHERE PeopleName = @DanToc),
                            QuocTich = (SELECT ID FROM Nationality WHERE Nationality = @QuocTich),
                            TonGiao = (SELECT ID FROM Religion WHERE Religion = @TonGiao),
                            ChungChiHN = @ChungChiHN,
                            PhamViCM = @PhamViCM,
                            NgayCapCCHN = @NgayCapCCHN,
                            DaGuiSo = @DaGuiSo
                        WHERE UserEnrollNumber = @UserEnrollNumber";

                    using (var sqlCommand = new SqlCommand(QUERY_USERINEXT, sqlConnection))
                    {
                        sqlCommand.Parameters.Add("@UserEnrollNumber", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.UserEnrollNumber;
                        sqlCommand.Parameters.Add("@UserFullCode", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.UserFullCode;
                        sqlCommand.Parameters.Add("@UserFullName", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.UserFullName;

                        if (String.IsNullOrEmpty(obj.HcnsNhanVien.NgaySinh))
                        {
                            sqlCommand.Parameters.Add("@NgaySinh", SqlDbType.DateTime).Value = DBNull.Value;
                        }
                        else
                        {
                            sqlCommand.Parameters.Add("@NgaySinh", SqlDbType.DateTime).Value = DateTime.ParseExact(obj.HcnsNhanVien.NgaySinh, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                        }

                        sqlCommand.Parameters.Add("@ChucDanh", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.ChucDanh;
                        sqlCommand.Parameters.Add("@GioiTinh", SqlDbType.SmallInt).Value = obj.HcnsNhanVien.GioiTinh == "Nam" ? 0 : 1;
                        sqlCommand.Parameters.Add("@TATitle", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.TATitle;
                        sqlCommand.Parameters.Add("@DoiTuong", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.DoiTuong;
                        sqlCommand.Parameters.Add("@SDT1", SqlDbType.VarChar).Value = obj.HcnsNhanVien.SDT1;
                        sqlCommand.Parameters.Add("@TAEmail", SqlDbType.VarChar).Value = obj.HcnsNhanVien.TAEmail;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = obj.HcnsNhanVien.Email;
                        sqlCommand.Parameters.Add("@SoCMT", SqlDbType.VarChar).Value = obj.HcnsNhanVien.SoCMT;
                        sqlCommand.Parameters.Add("@NoiCap", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.NoiCap;

                        if (String.IsNullOrEmpty(obj.HcnsNhanVien.CapNgay))
                        {
                            sqlCommand.Parameters.Add("@CapNgay", SqlDbType.DateTime).Value = DBNull.Value;
                        }
                        else
                        {
                            sqlCommand.Parameters.Add("@CapNgay", SqlDbType.DateTime).Value = DateTime.ParseExact(obj.HcnsNhanVien.CapNgay, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                        }

                        sqlCommand.Parameters.Add("@NoiSinhCT", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.NoiSinhCT;
                        sqlCommand.Parameters.Add("@PhongKhoaHC", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.Ten_PhongKhoaHC;

                        if (String.IsNullOrEmpty(obj.HcnsNhanVien.NgayVao))
                        {
                            sqlCommand.Parameters.Add("@NgayVao", SqlDbType.DateTime).Value = DBNull.Value;
                        }
                        else
                        {
                            sqlCommand.Parameters.Add("@NgayVao", SqlDbType.DateTime).Value = DateTime.ParseExact(obj.HcnsNhanVien.NgayVao, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                        }

                        sqlCommand.Parameters.Add("@SoTheBHXH", SqlDbType.VarChar).Value = obj.HcnsNhanVien.SoTheBHXH;
                        sqlCommand.Parameters.Add("@SoGioLamViec", SqlDbType.VarChar).Value = obj.HcnsNhanVien.SoGioLamViec;

                        sqlCommand.Parameters.Add("@DanToc", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.DanToc;
                        sqlCommand.Parameters.Add("@QuocTich", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.QuocTich;
                        sqlCommand.Parameters.Add("@TonGiao", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.TonGiao;

                        sqlCommand.Parameters.Add("@ChungChiHN", SqlDbType.NVarChar).Value = obj.ChungChiHanhNghe.ChungChiHN;
                        sqlCommand.Parameters.Add("@PhamViCM", SqlDbType.NVarChar).Value = obj.ChungChiHanhNghe.PhamViCM;

                        if (String.IsNullOrEmpty(obj.ChungChiHanhNghe.NgayCapCCHN))
                        {
                            sqlCommand.Parameters.Add("@NgayCapCCHN", SqlDbType.DateTime).Value = DBNull.Value;
                        }
                        else
                        {
                            sqlCommand.Parameters.Add("@NgayCapCCHN", SqlDbType.DateTime).Value = DateTime.ParseExact(obj.ChungChiHanhNghe.NgayCapCCHN, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                        }
                        sqlCommand.Parameters.Add("@DaGuiSo", SqlDbType.SmallInt).Value = int.Parse(obj.ChungChiHanhNghe.DaGuiSo);
                        sqlCommand.Parameters.Add("@DcThT", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.DcThT;
                        sqlCommand.Parameters.Add("@DcTT", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.DcTT;

                        int rowAffected = sqlCommand.ExecuteNonQuery();
                    }

                    //string QUERY_DEGREES = @"UPDATE Degrees SET Type = (SELECT RID FROM Ranks WHERE RName = @Type), GradSch = @GradSch, Field = @Field WHERE UserEnrollNumber = @UserEnrollNumber";

                    string QUERY_DEGREES = @"
                        IF EXISTS (SELECT ID FROM Degrees WHERE UserEnrollNumber = @UserEnrollNumber)
	                        BEGIN
		                        UPDATE Degrees SET Type = (SELECT RID FROM Ranks WHERE RName = @Type), GradSch = @GradSch, Field = @Field WHERE UserEnrollNumber = @UserEnrollNumber
	                        END
                        ELSE
	                        BEGIN
		                        INSERT INTO Degrees(UserEnrollNumber, Type, GradSch, Field) VALUES (@UserEnrollNumber, (SELECT RID FROM Ranks WHERE RName = @Type), @GradSch, @Field)
	                        END
                    ";

                    using (var sqlCommand = new SqlCommand(QUERY_DEGREES, sqlConnection))
                    {
                        sqlCommand.Parameters.Add("@UserEnrollNumber", SqlDbType.NVarChar).Value = obj.HcnsNhanVien.UserEnrollNumber;
                        sqlCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = obj.BangCap.Type_Name;
                        sqlCommand.Parameters.Add("@GradSch", SqlDbType.NVarChar).Value = obj.BangCap.GradSch;
                        sqlCommand.Parameters.Add("@Field", SqlDbType.NVarChar).Value = obj.BangCap.Field;
                        int rowAffected = sqlCommand.ExecuteNonQuery();
                    }
                }
            }

            return true;
        }
        public int XacNhanNoiAn_Multi(string connectionString, XacNhanAn_Multi obj)
        {
            int rowAffected = 0;


            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_UserEnrollNumberStr", obj.MASO.Substring(0, obj.MASO.Length - 1));
                parameters.Add("param_NoiAn", obj.MA);
                parameters.Add("param_Type", 9);

                rowAffected += db.Execute("sp_CheckEat_Info", parameters, commandType: CommandType.StoredProcedure);
            }
            return rowAffected;
        }
        public Dictionary<string, string> LoaiNhanVien(string connectionString)
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();

            string q = @"SELECT MaLoaiNV, LoaiNV FROM LoaiNhanVien where TrangThai = 1 ORDER BY ID";

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
                            lst.Add(reader["MaLoaiNV"].ToString(), reader["LoaiNV"].ToString());
                        }
                    }
                }
            }

            return lst;
        }
        public IEnumerable<HCNS_NhanVien> DanhSachTaiKhoan(string connectionString, HCNS_NhanVien nv = null)
        {
            List<HCNS_NhanVien> lst = new List<HCNS_NhanVien>();

            string query = @"SELECT a.UserEnrollNumber AS UserEnrollNumber, a.UserFullCode AS UserFullCode, (IsNull(a.TATitle,'') + ' ' + a.UserFullName) AS UserFullName, a.ChucDanh, IsNull(b.KhoaP,'') AS PhongKhoaHC, a.TAEmail AS TAEmail, a.SDTNB, a.Email, a.LoaiNV, a.NgayVao, a.DaNghi, IsNull(a.UPN,'') as UPN
                        FROM UserInfExt a
	                        LEFT JOIN Depts b ON a.PhongKhoaHC = b.STT
                        WHERE (1 = 1) AND a.danghi = 0 ORDER BY UserEnrollNumber DESC";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new HCNS_NhanVien()
                            {
                                UserEnrollNumber = string.IsNullOrEmpty(reader["UserEnrollNumber"].ToString()) ? 0 : int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullCode = reader["UserFullCode"].ToString(),
                                UserFullName = reader["UserFullName"].ToString(),
                                ChucDanh = reader["ChucDanh"].ToString(),
                                Ten_PhongKhoaHC = reader["PhongKhoaHC"].ToString(),
                                TAEmail = reader["TAEmail"].ToString(),
                                SDTNB = String.IsNullOrEmpty(reader["SDTNB"].ToString()) ? 0 : int.Parse(reader["SDTNB"].ToString()),
                                Email = reader["Email"].ToString(),
                                LoaiNV = reader["LoaiNV"].ToString(),
                                NgayVao = String.IsNullOrEmpty(reader["NgayVao"].ToString()) ? "" : DateTime.Parse(reader["NgayVao"].ToString()).ToString("dd/MM/yyyy"),
                                DaNghi = reader["DaNghi"].ToString(),
                                UPN = reader["UPN"].ToString(),
                            });
                        }
                    }
                }

                sqlConnection.Close();
            }

            return lst;
        }

        public int CapNhatTaiKhoan(string connectionString, string user, string upn)
        {
            var rowAffected = 0;

            string query = @"update UserInfExt set upn = @upn where userenrollnumber = @user";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                var param = new DynamicParameters();

                param.Add("user", user);
                param.Add("upn", upn);

                rowAffected = sqlConnection.Execute(query, param, commandType: CommandType.Text);

                sqlConnection.Close();
            }

            return rowAffected;
        }
        public int CapNhatEmail(string connectionString, string user, string email)
        {
            var rowAffected = 0;

            string query = @"update UserInfExt set TAEmail = @email where userenrollnumber = @user";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                var param = new DynamicParameters();

                param.Add("user", user);
                param.Add("email", email);

                rowAffected = sqlConnection.Execute(query, param, commandType: CommandType.Text);

                sqlConnection.Close();
            }

            return rowAffected;
        }
        public int CapNhatMatKhau(string connectionString, string user, string pw)
        {
            var rowAffected = 0;

            string query = @"update UserInfo set userpw = @pw where userenrollnumber = @user";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                var param = new DynamicParameters();

                param.Add("user", user);
                param.Add("pw", pw);

                rowAffected = sqlConnection.Execute(query, param, commandType: CommandType.Text);

                sqlConnection.Close();
            }

            return rowAffected;
        }
        public bool SalaryChangeExcel(string connectionString, List<HCNS_NhanVien_Excel> ex, string user)
        {
            string str = "";
            foreach (var item in ex)
            {
                string qstr = @"select * from SalaryChange where UserEnrollNumber = " + item.HcnsNhanVien.UserEnrollNumber + " and scd = '" + item.HcnsNhanVien.Nam + item.HcnsNhanVien.Thang + item.HcnsNhanVien.Ngay + "'";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(qstr, sqlConnection))
                    {
                        var reader = cmd.ExecuteReader();

                        if (!reader.HasRows)
                        {
                            str += ",(" + item.HcnsNhanVien.UserEnrollNumber + ",'" + item.HcnsNhanVien.Nam + "-" + item.HcnsNhanVien.Thang + "-" + item.HcnsNhanVien.Ngay + " 00:00:00')";
                        }
                    }
                }
            }


            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                string QUERY_USERINEXT = @"DECLARE @TempTable table
					                                (UserEnrollNumber int,
					                                SCD datetime)

                                INSERT INTO @TempTable
                                VALUES
                                " + str.Substring(1) + @"

                                INSERT INTO SalaryChange
                                SELECT
                                x.UserEnrollNumber,
                                x.SCD,
                                1 + ISNULL(y.SCTimeOld,0) AS SCTime,
                                '' AS Reason,
                                '' AS ConNum,
                                GETDATE() AS CreateDate,
                                NULL AS UpdateDate,
                                '" + user + @"' AS CreateAcc,
                                NULL AS UpdateAcc
                                FROM @TempTable x LEFT JOIN
                                (SELECT UserEnrollNumber, SCD AS SCDOld, SCTime AS SCTimeOld FROM
                                (SELECT UserEnrollNumber,SCD,SCTime,CreateDate,ROW_NUMBER() OVER (PARTITION BY UserEnrollNumber ORDER BY SCTime DESC) AS Rn 
                                FROM WiseEyeWebOn.dbo.SalaryChange WHERE UserEnrollNumber IN 
                                (SELECT DISTINCT UserEnrollNumber FROM @TempTable)) a
                                WHERE Rn = 1) y ON x.UserEnrollNumber = y.UserEnrollNumber";

                using (var sqlCommand = new SqlCommand(QUERY_USERINEXT, sqlConnection))
                {
                    int rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            return true;
        }

        public bool NgayPCExcel(string connectionString, List<HCNS_NhanVien_Excel> ex)
        {
            string str = "";
            string str2 = "";
            foreach (var item in ex)
            {
                str += "WHEN " + item.HcnsNhanVien.UserEnrollNumber + " THEN '" + item.HcnsNhanVien.Nam + "-" + item.HcnsNhanVien.Thang + "-" + item.HcnsNhanVien.Ngay + " 00:00:00' ";
                str2 += "," + item.HcnsNhanVien.UserEnrollNumber + "";
            }

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                string QUERY_USERINEXT = @"UPDATE UserInfExt
                    SET NgayPC = CASE UserEnrollNumber " + str + @" ELSE NgayPC END
                    WHERE UserEnrollNumber IN (" + str2.Substring(1) + ")";

                using (var sqlCommand = new SqlCommand(QUERY_USERINEXT, sqlConnection))
                {
                    int rowAffected = sqlCommand.ExecuteNonQuery();
                }
            }

            return true;
        }
        public string ImportFile(string connectionString, DataTable dt, string id, string user)
        {
            string result = string.Empty;

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("LoaiFile", Convert.ToInt32(id));
                parameters.Add("UserAcc", user);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        parameters.Add("col" + (j + 1).ToString(), dt.Rows[i][j].ToString());
                    }
                    var a = db.ExecuteScalar("sp_FileImport", parameters, commandType: CommandType.StoredProcedure);
                    result = a.ToString();
                }
            }
            return result;
        }
        public string ImportFile_Table(string connectionString, DataTable dt, string id, string user)
        {
            string result = string.Empty;

            DataTable dataTable = new DataTable();
            for (int i = 1; i <= 50; i++)
            {
                dataTable.Columns.Add("Col" + i.ToString(), typeof(string));
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow workRow = dataTable.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    workRow[j] = dt.Rows[i][j].ToString();                    
                }
                dataTable.Rows.Add(workRow);
            }

            string consString = connectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_FileImport"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@LoaiFile", Convert.ToInt32(id));
                    cmd.Parameters.AddWithValue("@UserAcc", user);
                    cmd.Parameters.AddWithValue("@FileTable", dataTable);
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
        public IEnumerable<DanhBaDienThoai> DS_FileExcel(string connectionString, string type)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_Condition1", type);
                parameters.Add("param_Type", 154);

                return db.Query<DanhBaDienThoai>("sp_DanhMuc", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public IEnumerable<HCNS_NhanVien> DS_TheoDoiThaiSan(string connectionString, HCNS_NhanVien obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                if (obj.UserEnrollNumber > 0)
                {
                    parameters.Add("UserEnrollNumber", obj.UserEnrollNumber == 0 ? "-1" : obj.UserEnrollNumber.ToString());
                    parameters.Add("param", "ds_theodoithaisan_nv");
                }
                else
                {
                    parameters.Add("UserEnrollNumber", obj.UserEnrollNumber == 0 ? "-1" : obj.UserEnrollNumber.ToString());
                    parameters.Add("Dept", obj.PhongKhoaHC);
                    parameters.Add("TuNgay", DateTime.ParseExact(obj.TuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                    parameters.Add("DenNgay", DateTime.ParseExact(obj.DenNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                    parameters.Add("param", "ds_theodoithaisan");
                }

                return db.Query<HCNS_NhanVien>("sp_TheoDoiThaiSan", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public int AddNew_TheoDoiThaiSan(string connectionString, HCNS_NhanVien obj, string user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UserEnrollNumber", obj.UserEnrollNumber);
                parameters.Add("NgayThongBao", string.IsNullOrEmpty(obj.NgayThongBao) ? null : DateTime.ParseExact(obj.NgayThongBao, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("NgayDuSinh", string.IsNullOrEmpty(obj.NgayDuSinh) ? null : DateTime.ParseExact(obj.NgayDuSinh, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("NgaySinhCon", string.IsNullOrEmpty(obj.NgaySinhCon) ? null : DateTime.ParseExact(obj.NgaySinhCon, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("NgayConMat", string.IsNullOrEmpty(obj.NgayConMat) ? null : DateTime.ParseExact(obj.NgayConMat, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("GhiChu", obj.GhiChu);
                parameters.Add("NghiSinhTuNgay", string.IsNullOrEmpty(obj.NghiSinhTuNgay) ? null : DateTime.ParseExact(obj.NghiSinhTuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("SoNgayNghi_DS", obj.SoNgayNghi_DS);
                parameters.Add("NgayNghi_DS", string.IsNullOrEmpty(obj.NgayNghi_DS) ? null : DateTime.ParseExact(obj.NgayNghi_DS, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("NguoiTao", user);
                parameters.Add("param", "themmoi");
                return db.Execute("sp_TheoDoiThaiSan", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Edit_TheoDoiThaiSan(string connectionString, HCNS_NhanVien obj, string user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ID", obj.ID);
                parameters.Add("NgayThongBao", string.IsNullOrEmpty(obj.NgayThongBao) ? null : DateTime.ParseExact(obj.NgayThongBao, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("NgayDuSinh", string.IsNullOrEmpty(obj.NgayDuSinh) ? null : DateTime.ParseExact(obj.NgayDuSinh, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("NgaySinhCon", string.IsNullOrEmpty(obj.NgaySinhCon) ? null : DateTime.ParseExact(obj.NgaySinhCon, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("NgayConMat", string.IsNullOrEmpty(obj.NgayConMat) ? null : DateTime.ParseExact(obj.NgayConMat, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("GhiChu", obj.GhiChu);
                parameters.Add("NghiSinhTuNgay", string.IsNullOrEmpty(obj.NghiSinhTuNgay) ? null : DateTime.ParseExact(obj.NghiSinhTuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("SoNgayNghi_DS", obj.SoNgayNghi_DS);
                parameters.Add("NgayNghi_DS", string.IsNullOrEmpty(obj.NgayNghi_DS) ? null : DateTime.ParseExact(obj.NgayNghi_DS, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("NguoiTao", user);
                parameters.Add("param", "capnhat");
                return db.Execute("sp_TheoDoiThaiSan", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete_TheoDoiThaiSan(string connectionString, HCNS_NhanVien obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ID", obj.ID);
                parameters.Add("param", "xoa");
                return db.Execute("sp_TheoDoiThaiSan", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public HCNS_NhanVien Get_TheoDoiThaiSan(string connectionString, HCNS_NhanVien obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ID", obj.ID);
                parameters.Add("param", "get_ds");

                return db.Query<HCNS_NhanVien>("sp_TheoDoiThaiSan", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        } 
        public DataTable TheoDoiThaiSan_Excel(string connectionString, HCNS_NhanVien obj)
        {

            try
            {
                using (var sConnection = new SqlConnection(connectionString))
                {
                    if (sConnection.State == ConnectionState.Closed)
                        sConnection.Open();

                    using (var sCommand = new SqlCommand("sp_TheoDoiThaiSan", sConnection))
                    {
                        if (obj.UserEnrollNumber > 0)
                        {
                            sCommand.CommandTimeout = 0;

                            sCommand.CommandType = CommandType.StoredProcedure;
                            sCommand.Parameters.Clear();
                            sCommand.Parameters.Add("@UserEnrollNumber", SqlDbType.VarChar).Value = obj.UserEnrollNumber == 0 ? "-1" : obj.UserEnrollNumber.ToString();
                            sCommand.Parameters.Add("@param", SqlDbType.VarChar).Value = "ds_theodoithaisan_nv_excel";
                        }
                        else
                        {
                            sCommand.CommandTimeout = 0;

                            sCommand.CommandType = CommandType.StoredProcedure;
                            sCommand.Parameters.Clear();
                            sCommand.Parameters.Add("@UserEnrollNumber", SqlDbType.VarChar).Value = obj.UserEnrollNumber == 0 ? "-1" : obj.UserEnrollNumber.ToString();
                            sCommand.Parameters.Add("@Dept", SqlDbType.VarChar).Value = obj.PhongKhoaHC;
                            sCommand.Parameters.Add("@TuNgay", SqlDbType.VarChar).Value = DateTime.ParseExact(obj.TuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                            sCommand.Parameters.Add("@DenNgay", SqlDbType.VarChar).Value = DateTime.ParseExact(obj.DenNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                            sCommand.Parameters.Add("@param", SqlDbType.VarChar).Value = "ds_theodoithaisan_excel";
                        }

                        DataTable dtable = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(sCommand);

                        da.Fill(dtable);
                        return dtable;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public DataTable Export_Excel(string connectionString, string loaifile, string tungay, string denngay, string khoaphong, string manv)
        {
            try
            {
                using (var sConnection = new SqlConnection(connectionString))
                {
                    if (sConnection.State == ConnectionState.Closed)
                        sConnection.Open();

                    using (var sCommand = new SqlCommand("sp_FileExport", sConnection))
                    {
                        sCommand.CommandTimeout = 0;

                        sCommand.CommandType = CommandType.StoredProcedure;
                        sCommand.Parameters.Clear();
                        sCommand.Parameters.Add("@LoaiFile", SqlDbType.VarChar).Value = loaifile;
                        sCommand.Parameters.Add("@NgayBD", SqlDbType.VarChar).Value = DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                        sCommand.Parameters.Add("@NgayKT", SqlDbType.VarChar).Value = DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                        sCommand.Parameters.Add("@KP", SqlDbType.VarChar).Value = string.IsNullOrEmpty(khoaphong) ? "-1" : khoaphong;
                        sCommand.Parameters.Add("@manv", SqlDbType.VarChar).Value = string.IsNullOrEmpty(manv) ? "-1" : manv;

                        DataTable dtable = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(sCommand);

                        da.Fill(dtable);
                        return dtable;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}