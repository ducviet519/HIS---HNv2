using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.App.Entities;
using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.App.Repositories.HCNS
{
    public class DK_SuatAn_Repo
    {
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

        public IEnumerable<Department> DSKhoaPhong(string connectionString, string kp)
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

        public string TimKiem(string makp, string manv)
        {
            string filter = "";

            if (!String.IsNullOrEmpty(makp))
            {
                filter += " AND b.PhongKhoa = " + makp;
            }
            if (!String.IsNullOrEmpty(manv))
            {
                filter += " AND (b.UserEnrollNumber = " + manv + " OR b.UserFullName LIKE N''%" + manv + "%'')";
            }
            return filter;
        }

        public List<ChamAn> DS_ChamAn(string connectionString, string makp, string manv, string thoidiem, string fromDate, string toDate)
        {
            var _ds = new List<ChamAn>();

            StringBuilder strBuilder = new StringBuilder();

            DateTime _from = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime _to = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            for (int i = 0; i < 30; i++)
            {
                var strObj = _from.AddDays(i).ToString("yyyyMMdd");

                strBuilder.Append(",[" + strObj + "]");
            }

            using (var sConnection = new SqlConnection(connectionString))
            {
                string q = @"EXEC[dbo].[sp_QuanLySuatAn] @NgayBD = N'" + _from.ToString("yyyyMMdd") + "', @KP = N'" + (String.IsNullOrEmpty(makp) ? "-1" : makp) + "', @MaNV = N'" + (String.IsNullOrEmpty(manv) ? "-1" : manv) + "', @thoidiem = N'" + thoidiem + "', @param = N'DangKySuatAn' ";

                //string q = @"EXEC[dbo].[sp_QuanLySuatAn] @colums = N'"+strBuilder.ToString()+ "', @timkiem = N'" + TimKiem(makp, manv) + "', @thoidiem = N'" + thoidiem + "', @colums2 = N'" + strBuilder.ToString().Substring(1) + "', @param = N'DangKySuatAn' ";

                //string q = @"SELECT UserEnrollNumber, UserFullName, Description" + strBuilder.ToString() + @" FROM
                //            (
                //             SELECT b.UserEnrollNumber, b.UserFullName, c.Description, DateReg, a." + thoidiem + @"
                //             FROM [UserInfExt] b
                //              LEFT JOIN [Sets] a ON b.UserEnrollNumber = a.UserEnrollNumber
                //              LEFT JOIN [RelationDept] c ON b.PhongKhoa = c.ID
                //             WHERE 1 = 1 AND b.DaNghi = 0" + TimKiem(makp, manv) + @") AS pv
                //            PIVOT (SUM(" + thoidiem + @") FOR DateReg IN (" + strBuilder.ToString().Substring(1) + @")) AS PVT ORDER BY UserEnrollNumber";

                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand(q, sConnection))
                {
                    var reader = sCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            _ds.Add(new ChamAn()
                            {
                                UserEnrollNumber = String.IsNullOrEmpty(reader["UserEnrollNumber"].ToString()) ? 0 : int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullName = String.IsNullOrEmpty(reader["UserFullName"].ToString()) ? "" : reader["UserFullName"].ToString(),
                                KhoaPhong = String.IsNullOrEmpty(reader["Description"].ToString()) ? "" : reader["Description"].ToString(),
                                Val_01 = String.IsNullOrEmpty(reader[3].ToString()) ? 0 : int.Parse(reader[3].ToString()),
                                Val_02 = String.IsNullOrEmpty(reader[4].ToString()) ? 0 : int.Parse(reader[4].ToString()),
                                Val_03 = String.IsNullOrEmpty(reader[5].ToString()) ? 0 : int.Parse(reader[5].ToString()),
                                Val_04 = String.IsNullOrEmpty(reader[6].ToString()) ? 0 : int.Parse(reader[6].ToString()),
                                Val_05 = String.IsNullOrEmpty(reader[7].ToString()) ? 0 : int.Parse(reader[7].ToString()),
                                Val_06 = String.IsNullOrEmpty(reader[8].ToString()) ? 0 : int.Parse(reader[8].ToString()),
                                Val_07 = String.IsNullOrEmpty(reader[9].ToString()) ? 0 : int.Parse(reader[9].ToString()),
                                Val_08 = String.IsNullOrEmpty(reader[10].ToString()) ? 0 : int.Parse(reader[10].ToString()),
                                Val_09 = String.IsNullOrEmpty(reader[11].ToString()) ? 0 : int.Parse(reader[11].ToString()),
                                Val_10 = String.IsNullOrEmpty(reader[12].ToString()) ? 0 : int.Parse(reader[12].ToString()),
                                Val_11 = String.IsNullOrEmpty(reader[13].ToString()) ? 0 : int.Parse(reader[13].ToString()),
                                Val_12 = String.IsNullOrEmpty(reader[14].ToString()) ? 0 : int.Parse(reader[14].ToString()),
                                Val_13 = String.IsNullOrEmpty(reader[15].ToString()) ? 0 : int.Parse(reader[15].ToString()),
                                Val_14 = String.IsNullOrEmpty(reader[16].ToString()) ? 0 : int.Parse(reader[16].ToString()),
                                Val_15 = String.IsNullOrEmpty(reader[17].ToString()) ? 0 : int.Parse(reader[17].ToString()),
                                Val_16 = String.IsNullOrEmpty(reader[18].ToString()) ? 0 : int.Parse(reader[18].ToString()),
                                Val_17 = String.IsNullOrEmpty(reader[19].ToString()) ? 0 : int.Parse(reader[19].ToString()),
                                Val_18 = String.IsNullOrEmpty(reader[20].ToString()) ? 0 : int.Parse(reader[20].ToString()),
                                Val_19 = String.IsNullOrEmpty(reader[21].ToString()) ? 0 : int.Parse(reader[21].ToString()),
                                Val_20 = String.IsNullOrEmpty(reader[22].ToString()) ? 0 : int.Parse(reader[22].ToString()),
                                Val_21 = String.IsNullOrEmpty(reader[23].ToString()) ? 0 : int.Parse(reader[23].ToString()),
                                Val_22 = String.IsNullOrEmpty(reader[24].ToString()) ? 0 : int.Parse(reader[24].ToString()),
                                Val_23 = String.IsNullOrEmpty(reader[25].ToString()) ? 0 : int.Parse(reader[25].ToString()),
                                Val_24 = String.IsNullOrEmpty(reader[26].ToString()) ? 0 : int.Parse(reader[26].ToString()),
                                Val_25 = String.IsNullOrEmpty(reader[27].ToString()) ? 0 : int.Parse(reader[27].ToString()),
                                Val_26 = String.IsNullOrEmpty(reader[28].ToString()) ? 0 : int.Parse(reader[28].ToString()),
                                Val_27 = String.IsNullOrEmpty(reader[29].ToString()) ? 0 : int.Parse(reader[29].ToString()),
                                Val_28 = String.IsNullOrEmpty(reader[30].ToString()) ? 0 : int.Parse(reader[30].ToString()),
                                Val_29 = String.IsNullOrEmpty(reader[31].ToString()) ? 0 : int.Parse(reader[31].ToString()),
                                Val_30 = String.IsNullOrEmpty(reader[32].ToString()) ? 0 : int.Parse(reader[32].ToString())
                            });
                        }
                    }
                    reader.Close();
                }
            }
            return _ds;
        }

        public bool CapNhat_ChamAn(string connectionString, List<Update_ChamAn> data)
        {
            int count = 0, demsolanchiahet = 0;
            int loopLength = data.Count; // 58 ban ghi
            int quydinh = 30;
            int solanchiahet = loopLength / quydinh;
            int sodu = loopLength % quydinh;
            int rowAffected = 0;
            string thoidiem = "";
            using (var sConnection = new SqlConnection(connectionString))
            {
                StringBuilder strBuilder = new StringBuilder();

                foreach (var item in data)
                {
                    if (String.IsNullOrEmpty(thoidiem)) thoidiem = item.ThoiDiem;

                    count++;

                    string dateFormat = DateTime.ParseExact(item.Ngay_CC, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    string query = @"INSERT INTO [SetsCache](UserEnrollNumber, DateReg, " + item.ThoiDiem + @") VALUES (" + item.UserEnrollNumber + @", '" + dateFormat + @"', " + item.GiaTri + @")";

                    strBuilder.Append(query);

                    if (count == quydinh && demsolanchiahet <= solanchiahet)
                    {
                        if (sConnection.State == ConnectionState.Closed)
                            sConnection.Open();

                        using (var sCommand = new SqlCommand(strBuilder.ToString(), sConnection))
                        {
                            rowAffected += sCommand.ExecuteNonQuery();
                        }

                        if (sConnection.State == ConnectionState.Open)
                            sConnection.Close();
                        count = 0;
                        demsolanchiahet++;
                        strBuilder = new StringBuilder();
                    }
                    else if (count < quydinh && demsolanchiahet == solanchiahet)
                    {
                        if (sConnection.State == ConnectionState.Closed)
                            sConnection.Open();

                        using (var sCommand = new SqlCommand(strBuilder.ToString(), sConnection))
                        {
                            rowAffected += sCommand.ExecuteNonQuery();
                        }

                        if (sConnection.State == ConnectionState.Open)
                            sConnection.Close();
                        count = 0;
                        strBuilder = new StringBuilder();
                    }
                }

                //string transUpdate = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE; BEGIN TRANSACTION;
                //        MERGE INTO Sets
                //        WITH (HOLDLOCK) AS target
                //        USING SetsCache AS source ON
                //         target.UserEnrollNumber = source.UserEnrollNumber
                //         AND target.DateReg = source.DateReg
                //        WHEN MATCHED THEN UPDATE SET target." + thoidiem + @" = source." + thoidiem + @" WHEN NOT MATCHED BY TARGET THEN INSERT (UserEnrollNumber, DateReg, " + thoidiem + @") VALUES (source.UserEnrollNumber, source.DateReg, source." + thoidiem + @");
                //        DELETE SetsCache;
                //        DELETE FROM Sets WHERE (Br IS NULL OR Br = 0) AND (BrR IS NULL OR BrR = 0) AND (" + thoidiem + @" IS NULL OR " + thoidiem + @" = 0) AND (" + thoidiem + @"R IS NULL OR " + thoidiem + @"R = 0) AND (Di IS NULL OR Di = 0) AND (DiR IS NULL OR DiR = 0);
                //        COMMIT TRANSACTION;";

                string transUpdate = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
                        BEGIN TRANSACTION;
                        MERGE INTO Sets
                        WITH (HOLDLOCK) AS target
                        USING SetsCache AS source ON target.UserEnrollNumber = source.UserEnrollNumber AND target.DateReg = source.DateReg
                        WHEN MATCHED THEN UPDATE SET target." + thoidiem + @" = source." + thoidiem + @" WHEN NOT MATCHED BY TARGET THEN INSERT (UserEnrollNumber, DateReg, " + thoidiem + @") VALUES (source.UserEnrollNumber, source.DateReg, source." + thoidiem + @");
                        DELETE SetsCache;
                        COMMIT TRANSACTION;";

                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand(transUpdate, sConnection))
                {
                    sCommand.ExecuteNonQuery();
                }

                if (sConnection.State == ConnectionState.Open)
                    sConnection.Close();
            }
            return rowAffected == loopLength ? true : false;
        }

        public IEnumerable<SuatAnNoiTru_HienDien> SANT_HienDien(string connectionString, string makp, string mabn)
        {
            List<SuatAnNoiTru_HienDien> lst = new List<SuatAnNoiTru_HienDien>();

            string filter = "";

            if (!string.IsNullOrEmpty(makp))
            {
                filter += " AND a.MAKP = '" + makp + "'";
            }
            if (!string.IsNullOrEmpty(mabn))
            {
                filter += " AND a.MABN LIKE '%" + mabn + "%'";
            }

            string query = @"SELECT
                  NVL(e.tenvt,e.tenkp) AS khoa,
                  a.mabn,
                  b.hoten,
                  CASE WHEN b.phai = 0 THEN 'Nam' ELSE 'Nữ' END AS phai,
                  b.namsinh,
                  SUBSTR(w.tuoivao,1,3)||
                  CASE WHEN SUBSTR(w.tuoivao,4,1) = 0 THEN 'TU' ELSE CASE WHEN SUBSTR(w.tuoivao,4,1)=1 THEN 'TH' ELSE CASE WHEN SUBSTR(w.tuoivao,4,1)=2 THEN 'NG' ELSE 'GI' END END END AS tuoi,
                  dantoc.dantoc,
                  TO_CHAR(a.ngay,'dd/mm/yyyy hh24:mi') AS ngay,
                  f.doituong,
                  x.maicd,
                  x.chandoan,
                  x.mabs,
                  y.hoten AS tenbs,
                  a.makp,
                  d.sovaovien,
                  TO_CHAR(d.ngay,'dd/mm/yyyy hh24:mi') AS ngayvv,
                  TO_CHAR(a.ngay,'yymmddhh24:mi') AS yymmdd,
                  trim(TO_CHAR(sysdate-x.ngay+DECODE(x.khoachuyen,'001',1,0),'999999999')) AS ngaydt,
                  TO_CHAR(sysdate,'dd/mm/yyyy hh24:mi') AS ngayrv,
                  CASE WHEN a.nhapkhoa = 1 AND a.xuatkhoa = 0 THEN 0 ELSE CASE WHEN a.nhapkhoa = 1 AND a.xuatkhoa = 1 THEN -1 ELSE 1 END END AS chonhapkhoa
                FROM hsofttamanh.hiendien a
                INNER JOIN hsofttamanh.btdbn b ON a.mabn=b.mabn
                INNER JOIN hsofttamanh.btdkp_bv c ON a.noichuyen=c.makp
                INNER JOIN hsofttamanh.benhandt d ON a.maql=d.maql
                INNER JOIN hsofttamanh.btdkp_bv e ON a.makp=e.makp
                INNER JOIN hsofttamanh.doituong f ON d.madoituong=f.madoituong
                LEFT JOIN hsofttamanh.bhyt g ON a.maql=g.maql
                LEFT JOIN hsofttamanh.dmgiuong h ON a.idgiuong=h.id
                INNER JOIN hsofttamanh.nhapkhoa x ON a.id=x.id
                LEFT JOIN hsofttamanh.dmbs y ON x.mabs=y.ma
                LEFT JOIN hsofttamanh.lienhe w ON a.maql=w.maql
                INNER JOIN hsofttamanh.btdtt z1 ON b.matt=z1.matt
                INNER JOIN hsofttamanh.btdquan z2 ON b.maqu=z2.maqu
                INNER JOIN hsofttamanh.btdpxa z3 ON b.maphuongxa=z3.maphuongxa
                LEFT JOIN hsofttamanh.nhantu nt ON d.nhantu=nt.ma
                LEFT JOIN hsofttamanh.btdnn_bv nn ON b.mann=nn.mann
                LEFT JOIN hsofttamanh.dmcanbo cb ON g.canbo=cb.id
                LEFT JOIN hsofttamanh.noigioithieu noigt ON d.maql=noigt.maql
                LEFT JOIN hsofttamanh.dstt tv ON noigt.mabv=tv.mabv
                LEFT JOIN hsofttamanh.btddt dantoc ON b.madantoc=dantoc.madantoc
                WHERE d.loaiba =1
                AND a.nhapkhoa =1
                AND a.xuatkhoa =0
                AND (g.sudung IS NULL OR g.sudung = 1)
                AND a.mabn not like '15%'" + filter + @"
                ORDER BY khoa, chonhapkhoa DESC, yymmdd DESC";

            using (var oconnection = new OracleConnection(connectionString))
            {
                if (oconnection.State == ConnectionState.Closed)
                    oconnection.Open();

                using (var ocmd = new OracleCommand(query, oconnection))
                {
                    var reader = ocmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new SuatAnNoiTru_HienDien()
                            {
                                KHOAPHONG = reader["MAKP"].ToString(),
                                TEN_KHOAPHONG = reader["KHOA"].ToString(),
                                MABN = reader["MABN"].ToString(),
                                HOTEN = reader["HOTEN"].ToString(),
                                PHAI = reader["PHAI"].ToString(),
                                NAMSINH = reader["NAMSINH"].ToString(),
                                MAICD = reader["MAICD"].ToString(),
                                CHANDOAN = reader["CHANDOAN"].ToString(),
                                MA_BACSI = reader["MABS"].ToString(),
                                TEN_BACSI = reader["TENBS"].ToString(),
                                NGAY_VAO_VIEN = reader["NGAYVV"].ToString(),
                                NGAY_DIEU_TRI = int.Parse(reader["NGAYDT"].ToString())
                            });
                        }
                    }
                }
            }

            return lst;
        }

        public SuatAnNoiTru_HienDien SANT_HienDien_ThongTinBenhNhan(string connectionString, string mabn)
        {
            SuatAnNoiTru_HienDien obj = new SuatAnNoiTru_HienDien();

            string filter = "";

            if (!string.IsNullOrEmpty(mabn))
            {
                filter += " AND a.MABN = '" + mabn + "'";
            }
            else
            {
                return null;
            }

            string query = @"SELECT
                  NVL(e.tenvt,e.tenkp) AS khoa,
                  a.mabn,
                  b.hoten,
                  CASE WHEN b.phai = 0 THEN 'Nam' ELSE 'Nữ' END AS phai,
                  b.namsinh,
                  SUBSTR(w.tuoivao,1,3)||
                  CASE WHEN SUBSTR(w.tuoivao,4,1) = 0 THEN 'TU' ELSE CASE WHEN SUBSTR(w.tuoivao,4,1)=1 THEN 'TH' ELSE CASE WHEN SUBSTR(w.tuoivao,4,1)=2 THEN 'NG' ELSE 'GI' END END END AS tuoi,
                  dantoc.dantoc,
                  TO_CHAR(a.ngay,'dd/mm/yyyy hh24:mi') AS ngay,
                  f.doituong,
                  x.maicd,
                  x.chandoan,
                  x.mabs,
                  y.hoten AS tenbs,
                  a.makp,
                  d.sovaovien,
                  TO_CHAR(d.ngay,'dd/mm/yyyy hh24:mi') AS ngayvv,
                  TO_CHAR(a.ngay,'yymmddhh24:mi') AS yymmdd,
                  trim(TO_CHAR(sysdate-x.ngay+DECODE(x.khoachuyen,'001',1,0),'999999999')) AS ngaydt,
                  TO_CHAR(sysdate,'dd/mm/yyyy hh24:mi') AS ngayrv,
                  CASE WHEN a.nhapkhoa = 1 AND a.xuatkhoa = 0 THEN 0 ELSE CASE WHEN a.nhapkhoa = 1 AND a.xuatkhoa = 1 THEN -1 ELSE 1 END END AS chonhapkhoa
                FROM hsofttamanh.hiendien a
                INNER JOIN hsofttamanh.btdbn b ON a.mabn=b.mabn
                INNER JOIN hsofttamanh.btdkp_bv c ON a.noichuyen=c.makp
                INNER JOIN hsofttamanh.benhandt d ON a.maql=d.maql
                INNER JOIN hsofttamanh.btdkp_bv e ON a.makp=e.makp
                INNER JOIN hsofttamanh.doituong f ON d.madoituong=f.madoituong
                LEFT JOIN hsofttamanh.bhyt g ON a.maql=g.maql
                LEFT JOIN hsofttamanh.dmgiuong h ON a.idgiuong=h.id
                INNER JOIN hsofttamanh.nhapkhoa x ON a.id=x.id
                LEFT JOIN hsofttamanh.dmbs y ON x.mabs=y.ma
                LEFT JOIN hsofttamanh.lienhe w ON a.maql=w.maql
                INNER JOIN hsofttamanh.btdtt z1 ON b.matt=z1.matt
                INNER JOIN hsofttamanh.btdquan z2 ON b.maqu=z2.maqu
                INNER JOIN hsofttamanh.btdpxa z3 ON b.maphuongxa=z3.maphuongxa
                LEFT JOIN hsofttamanh.nhantu nt ON d.nhantu=nt.ma
                LEFT JOIN hsofttamanh.btdnn_bv nn ON b.mann=nn.mann
                LEFT JOIN hsofttamanh.dmcanbo cb ON g.canbo=cb.id
                LEFT JOIN hsofttamanh.noigioithieu noigt ON d.maql=noigt.maql
                LEFT JOIN hsofttamanh.dstt tv ON noigt.mabv=tv.mabv
                LEFT JOIN hsofttamanh.btddt dantoc ON b.madantoc=dantoc.madantoc
                WHERE d.loaiba = 1
                AND a.nhapkhoa = 1
                AND a.xuatkhoa = 0
                AND (g.sudung IS NULL OR g.sudung = 1)
                AND a.mabn not like '15%'" + filter + @"
                ORDER BY khoa, chonhapkhoa DESC, yymmdd DESC";

            using (var oconnection = new OracleConnection(connectionString))
            {
                if (oconnection.State == ConnectionState.Closed)
                    oconnection.Open();

                using (var ocmd = new OracleCommand(query, oconnection))
                {
                    var reader = ocmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            obj = new SuatAnNoiTru_HienDien()
                            {
                                KHOAPHONG = reader["MAKP"].ToString(),
                                TEN_KHOAPHONG = reader["KHOA"].ToString(),
                                MABN = reader["MABN"].ToString(),
                                HOTEN = reader["HOTEN"].ToString(),
                                PHAI = reader["PHAI"].ToString(),
                                NAMSINH = reader["NAMSINH"].ToString(),
                                MAICD = reader["MAICD"].ToString(),
                                CHANDOAN = reader["CHANDOAN"].ToString(),
                                MA_BACSI = reader["MABS"].ToString(),
                                TEN_BACSI = reader["TENBS"].ToString(),
                                NGAY_VAO_VIEN = reader["NGAYVV"].ToString(),
                                NGAY_DIEU_TRI = int.Parse(reader["NGAYDT"].ToString())
                            };
                        }
                    }
                }
            }

            return obj;
        }

        public SuatAnNoiTru_HienDien SANT_ThongTinBenhNhan(string connectionString, string mabn)
        {
            SuatAnNoiTru_HienDien obj = new SuatAnNoiTru_HienDien();

            string query = @"select MABN, HOTEN, NAMSINH, CASE WHEN phai = 0 THEN 'Nam' ELSE 'Nữ' END AS PHAI from btdbn where mabn = :MABN";

            using (var oconnection = new OracleConnection(connectionString))
            {
                if (oconnection.State == ConnectionState.Closed)
                    oconnection.Open();

                using (var ocmd = new OracleCommand(query, oconnection))
                {
                    ocmd.Parameters.Clear();
                    ocmd.Parameters.Add(":MABN", OracleDbType.Varchar2).Value = mabn;

                    var reader = ocmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            obj = new SuatAnNoiTru_HienDien()
                            {
                                MABN = reader["MABN"].ToString(),
                                HOTEN = reader["HOTEN"].ToString(),
                                PHAI = reader["PHAI"].ToString(),
                                NAMSINH = reader["NAMSINH"].ToString()
                            };
                        }
                    }
                }
            }

            return obj;
        }

        public Dictionary<string, string> SANT_DanhSachKhoaPhong(string connectionString)
        {
            string query = "select MAKP, TENKP from hsofttamanh.btdkp_add WHERE LOAI = 0 ORDER BY TENKP";

            Dictionary<string, string> data = new Dictionary<string, string>();

            using (var oconnection = new OracleConnection(connectionString))
            {
                if (oconnection.State == ConnectionState.Closed)
                    oconnection.Open();

                using (var ocmd = new OracleCommand(query, oconnection))
                {
                    var reader = ocmd.ExecuteReader();

                    while (reader.Read())
                    {
                        data.Add(reader["MAKP"].ToString(), reader["TENKP"].ToString());
                    }
                }
            }

            return data;
        }

        public IEnumerable<SuatAnNoiTru_NhomBenh> SANT_DanhSachNhomBenh(string connectionString, string doituong)
        {
            string query = "SELECT ID, TEN_NB FROM DD_NhomBenh WHERE TRANG_THAI = 1 AND DOI_TUONG = @DOI_TUONG";

            List<SuatAnNoiTru_NhomBenh> data = new List<SuatAnNoiTru_NhomBenh>();

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@DOI_TUONG", SqlDbType.VarChar).Value = doituong;

                    var reader = scmd.ExecuteReader();

                    while (reader.Read())
                    {
                        data.Add(new SuatAnNoiTru_NhomBenh()
                        {
                            ID = int.Parse(reader["ID"].ToString()),
                            TEN_NB = reader["TEN_NB"].ToString()
                        });
                    }
                }
            }

            return data;
        }

        public IEnumerable<SuatAnNoiTru_DoiTuong> SANT_DanhSachDoiTuong(string connectionString, int nhombenh)
        {
            string query = "SELECT ID, TEN_DOITUONG FROM DD_NhomBenh_DoiTuong WHERE TRANG_THAI = 1 AND ID_NHOMBENH = @ID_NHOMBENH ORDER BY SAP_XEP";

            List<SuatAnNoiTru_DoiTuong> data = new List<SuatAnNoiTru_DoiTuong>();

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@ID_NHOMBENH", SqlDbType.Int).Value = nhombenh;

                    var reader = scmd.ExecuteReader();

                    while (reader.Read())
                    {
                        data.Add(new SuatAnNoiTru_DoiTuong()
                        {
                            ID = int.Parse(reader["ID"].ToString()),
                            TEN_DOITUONG = reader["TEN_DOITUONG"].ToString()
                        });
                    }
                }
            }

            return data;
        }

        public IEnumerable<SuatAnNoiTru_CheBien> SANT_DanhSachCheBien(string connectionString, int doituong)
        {
            string query = "SELECT KY_HIEU, DANG_CHE_BIEN, GHI_CHU FROM DD_NhomBenh_CheBien WHERE TRANG_THAI = 1 AND ID_DOITUONG = @ID_DOITUONG";

            List<SuatAnNoiTru_CheBien> data = new List<SuatAnNoiTru_CheBien>();

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@ID_DOITUONG", SqlDbType.Int).Value = doituong;

                    var reader = scmd.ExecuteReader();

                    while (reader.Read())
                    {
                        data.Add(new SuatAnNoiTru_CheBien()
                        {
                            KY_HIEU = reader["KY_HIEU"].ToString(),
                            DANG_CHE_BIEN = reader["DANG_CHE_BIEN"].ToString(),
                            GHI_CHU = reader["GHI_CHU"].ToString()
                        });
                    }
                }
            }

            return data;
        }

        public bool SANT_CapNhatSuatAnNoiTru(string connectionString, SuatAnNoiTru_KhaiBao obj)
        {
            string query = @"IF EXISTS (SELECT 1 FROM DD_SuatAn WHERE MABN = @MABN AND NGAY = @NGAY)
	                    BEGIN
		                    UPDATE DD_SuatAn SET MA_CHE_BIEN = @MA_CHE_BIEN, BUA_SANG = @BUA_SANG, BUA_TRUA = @BUA_TRUA, BUA_PHU = @BUA_PHU, BUA_TOI = @BUA_TOI, NGAY_CAP_NHAT = @NGAY_CAP_NHAT, NGUOI_CAP_NHAT = @NGUOI_CAP_NHAT WHERE MABN = @MABN AND NGAY = @NGAY
	                    END
                    ELSE
	                    BEGIN
		                    INSERT INTO DD_SuatAn(MABN, NGAY, MA_CHE_BIEN, BUA_SANG, BUA_TRUA, BUA_PHU, BUA_TOI, NGAY_CAP_NHAT, NGUOI_CAP_NHAT) VALUES (@MABN, @NGAY, @MA_CHE_BIEN, @BUA_SANG, @BUA_TRUA, @BUA_PHU, @BUA_TOI, @NGAY_CAP_NHAT, @NGUOI_CAP_NHAT)
	                    END";

            int rowAffected = 0;

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (var scmd = new SqlCommand(query, sconnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@MABN", SqlDbType.VarChar).Value = obj.MABN;
                    scmd.Parameters.Add("@NGAY", SqlDbType.Date).Value = DateTime.ParseExact(obj.NGAY, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    scmd.Parameters.Add("@MA_CHE_BIEN", SqlDbType.VarChar).Value = obj.MA_CHE_BIEN;
                    scmd.Parameters.Add("@BUA_SANG", SqlDbType.Int).Value = obj.BUA_SANG;
                    scmd.Parameters.Add("@BUA_TRUA", SqlDbType.Int).Value = obj.BUA_TRUA;
                    scmd.Parameters.Add("@BUA_PHU", SqlDbType.Int).Value = obj.BUA_PHU;
                    scmd.Parameters.Add("@BUA_TOI", SqlDbType.Int).Value = obj.BUA_TOI;
                    scmd.Parameters.Add("@NGAY_CAP_NHAT", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                    scmd.Parameters.Add("@NGUOI_CAP_NHAT", SqlDbType.VarChar).Value = obj.NGUOI_CAP_NHAT;

                    rowAffected = scmd.ExecuteNonQuery();
                }
            }

            return rowAffected > 0 ? true : false;
        }

        public IEnumerable<SuatAnNoiTru_HienDien> SANT_DanhSachDaDK(string sConn, string oConn, string mabn, string tungay, string denngay)
        {
            string filter = "";

            if (!string.IsNullOrEmpty(mabn))
            {
                filter += " AND a.MABN = " + mabn;
            }
            if (!string.IsNullOrEmpty(tungay) && !string.IsNullOrEmpty(denngay))
            {
                tungay = DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                denngay = DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                filter += " AND (a.NGAY >= '" + tungay + "' AND a.NGAY <= '" + denngay + "')";
            }

            List<SuatAnNoiTru_HienDien> lst = new List<SuatAnNoiTru_HienDien>();

            string squery = @"SELECT a.MABN, CONVERT(VARCHAR, a.NGAY, 103) AS NGAY, a.MA_CHE_BIEN, CONVERT(VARCHAR, a.NGAY_CAP_NHAT, 103) AS NGAY_CAP_NHAT, a.NGUOI_CAP_NHAT, b.DANG_CHE_BIEN, a.BUA_SANG, a.BUA_TRUA, a.BUA_PHU, a.BUA_TOI, c.TEN_DOITUONG, d.TEN_NB FROM DD_SuatAn a
	                            LEFT JOIN DD_NhomBenh_CheBien b ON b.KY_HIEU = a.MA_CHE_BIEN
	                            LEFT JOIN DD_NhomBenh_DoiTuong c ON c.ID = b.ID_DOITUONG
	                            LEFT JOIN DD_NhomBenh d ON d.ID = c.ID_NHOMBENH WHERE 1 = 1" + filter;

            using (var sConnection = new SqlConnection(sConn))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var scmd = new SqlCommand(squery, sConnection))
                {
                    var reader = scmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var _mabn = reader["MABN"].ToString();
                            var info = SANT_ThongTinBenhNhan(oConn, _mabn);

                            lst.Add(new SuatAnNoiTru_HienDien()
                            {
                                MABN = _mabn,
                                HOTEN = info.HOTEN,
                                NAMSINH = info.NAMSINH,
                                PHAI = info.PHAI,
                                NGAY = reader["NGAY"].ToString(),
                                MA_CHE_BIEN = reader["MA_CHE_BIEN"].ToString(),
                                DANG_CHE_BIEN = reader["DANG_CHE_BIEN"].ToString(),
                                BUA_SANG = int.Parse(reader["BUA_SANG"].ToString()),
                                BUA_TRUA = int.Parse(reader["BUA_TRUA"].ToString()),
                                BUA_PHU = int.Parse(reader["BUA_PHU"].ToString()),
                                BUA_TOI = int.Parse(reader["BUA_TOI"].ToString()),
                                NGAY_CAP_NHAT = reader["NGAY_CAP_NHAT"].ToString(),
                                NGUOI_CAP_NHAT = reader["NGUOI_CAP_NHAT"].ToString()
                            });
                        }
                    }
                }
            }

            return lst;
        }
        public IEnumerable<SuatAnNoiTru_SoLuong> SANT_TongHopSoLuong(string connectionString, string mabn, DateTime from, DateTime to)
        {
            string sql = @"
                SELECT * FROM (
	                SELECT
		                N'Bữa sáng' as TYPE,
		                COUNT(CASE WHEN BUA_SANG = 1 THEN 1 ELSE NULL END) AS S1,
		                COUNT(CASE WHEN BUA_SANG = 2 THEN 1 ELSE NULL END) AS S2,
		                COUNT(CASE WHEN BUA_SANG = 3 THEN 1 ELSE NULL END) AS S3,
		                COUNT(CASE WHEN BUA_SANG = 4 THEN 1 ELSE NULL END) AS S4,
		                COUNT(CASE WHEN BUA_SANG = 5 THEN 1 ELSE NULL END) AS S5,
		                COUNT(CASE WHEN BUA_SANG = 6 THEN 1 ELSE NULL END) AS S6,
		                COUNT(CASE WHEN BUA_SANG = 7 THEN 1 ELSE NULL END) AS S7,
		                COUNT(BUA_SANG) AS BS_ToTal
	                FROM [DD_SuatAn] WITH ( NOLOCK ) WHERE NGAY >= @FROM AND NGAY <= @TO AND (@MABN = '' OR MABN = @MABN)
	                UNION
	                SELECT
		                N'Bữa trưa' as TYPE,
		                COUNT(CASE WHEN BUA_TRUA = 1 THEN 1 ELSE NULL END) AS S1,
		                COUNT(CASE WHEN BUA_TRUA = 2 THEN 1 ELSE NULL END) AS S2,
		                COUNT(CASE WHEN BUA_TRUA = 3 THEN 1 ELSE NULL END) AS S3,
		                COUNT(CASE WHEN BUA_TRUA = 4 THEN 1 ELSE NULL END) AS S4,
		                COUNT(CASE WHEN BUA_TRUA = 5 THEN 1 ELSE NULL END) AS S5,
		                COUNT(CASE WHEN BUA_TRUA = 6 THEN 1 ELSE NULL END) AS S6,
		                COUNT(CASE WHEN BUA_TRUA = 7 THEN 1 ELSE NULL END) AS S7,
		                COUNT(BUA_TRUA) AS BS_ToTal
	                FROM [DD_SuatAn] WITH ( NOLOCK ) WHERE NGAY >= @FROM AND NGAY <= @TO AND (@MABN = '' OR MABN = @MABN)
	                UNION
	                SELECT
		                N'Bữa phụ' as TYPE,
		                COUNT(CASE WHEN BUA_PHU = 1 THEN 1 ELSE NULL END) AS S1,
		                COUNT(CASE WHEN BUA_PHU = 2 THEN 1 ELSE NULL END) AS S2,
		                COUNT(CASE WHEN BUA_PHU = 3 THEN 1 ELSE NULL END) AS S3,
		                COUNT(CASE WHEN BUA_PHU = 4 THEN 1 ELSE NULL END) AS S4,
		                COUNT(CASE WHEN BUA_PHU = 5 THEN 1 ELSE NULL END) AS S5,
		                COUNT(CASE WHEN BUA_PHU = 6 THEN 1 ELSE NULL END) AS S6,
		                COUNT(CASE WHEN BUA_PHU = 7 THEN 1 ELSE NULL END) AS S7,
		                COUNT(BUA_PHU) AS BS_ToTal
	                FROM [DD_SuatAn] WITH ( NOLOCK ) WHERE NGAY >= @FROM AND NGAY <= @TO AND (@MABN = '' OR MABN = @MABN)
	                UNION
	                SELECT
		                N'Bữa tối' as TYPE,
		                COUNT(CASE WHEN BUA_TOI = 1 THEN 1 ELSE NULL END) AS S1,
		                COUNT(CASE WHEN BUA_TOI = 2 THEN 1 ELSE NULL END) AS S2,
		                COUNT(CASE WHEN BUA_TOI = 3 THEN 1 ELSE NULL END) AS S3,
		                COUNT(CASE WHEN BUA_TOI = 4 THEN 1 ELSE NULL END) AS S4,
		                COUNT(CASE WHEN BUA_TOI = 5 THEN 1 ELSE NULL END) AS S5,
		                COUNT(CASE WHEN BUA_TOI = 6 THEN 1 ELSE NULL END) AS S6,
		                COUNT(CASE WHEN BUA_TOI = 7 THEN 1 ELSE NULL END) AS S7,
		                COUNT(BUA_TOI) AS BS_ToTal
	                FROM [DD_SuatAn] WITH ( NOLOCK ) WHERE NGAY >= @FROM AND NGAY <= @TO AND (@MABN = '' OR MABN = @MABN)
                ) as tbl";

            List<SuatAnNoiTru_SoLuong> lst = new List<SuatAnNoiTru_SoLuong>();

            using (var sconn = new SqlConnection(connectionString))
            {
                if (sconn.State == ConnectionState.Closed)
                    sconn.Open();

                using (var scmd = new SqlCommand(sql, sconn))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@MABN", SqlDbType.VarChar).Value = mabn;
                    scmd.Parameters.Add("@FROM", SqlDbType.Date).Value = from;
                    scmd.Parameters.Add("@TO", SqlDbType.Date).Value = to;
                    var reader = scmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new SuatAnNoiTru_SoLuong()
                            {
                                TYPE = reader["TYPE"].ToString(),
                                S1 = int.Parse(reader["S1"].ToString()),
                                S2 = int.Parse(reader["S2"].ToString()),
                                S3 = int.Parse(reader["S3"].ToString()),
                                S4 = int.Parse(reader["S4"].ToString()),
                                S5 = int.Parse(reader["S5"].ToString()),
                                S6 = int.Parse(reader["S6"].ToString()),
                                S7 = int.Parse(reader["S7"].ToString()),
                                TOTAL = int.Parse(reader["BS_ToTal"].ToString()),
                                DISPLAY = "<b>" + reader["TYPE"].ToString() + "</b>"
                                    + (reader["S1"].ToString() == "0" ? "" : " - Set 1: " + reader["S1"].ToString() + " suất")
                                    + (reader["S2"].ToString() == "0" ? "" : " - Set 2: " + reader["S2"].ToString() + " suất")
                                    + (reader["S3"].ToString() == "0" ? "" : " - Set 3: " + reader["S3"].ToString() + " suất")
                                    + (reader["S4"].ToString() == "0" ? "" : " - Set 4: " + reader["S4"].ToString() + " suất")
                                    + (reader["S5"].ToString() == "0" ? "" : " - Set 5: " + reader["S5"].ToString() + " suất")
                                    + (reader["S6"].ToString() == "0" ? "" : " - Set 6: " + reader["S6"].ToString() + " suất")
                                    + (reader["S7"].ToString() == "0" ? "" : " - Set 7: " + reader["S7"].ToString() + " suất")
                            });
                        }
                    }
                }
            }

            return lst;
        }
        public IEnumerable<Department> EatPlace(string connectionString)
        {
            string q = "SELECT ID as STT, Place as KhoaP FROM EatPlace";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<Department>(q);
                sqlConnection.Close();
                return result.ToList();
            }
        }
        public IEnumerable<Department> DSKhoaPhong_Eat(string connectionString)
        {
            string q = "SELECT STT, KhoaP FROM Depts";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<Department>(q);
                sqlConnection.Close();
                return result.ToList();
            }
        }
        public DataTable ThongKe_ChamAn(string connectionString, string dept, string tungay, string denngay, string thoidiem, string noian, string loai, string theo)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(tungay) && !string.IsNullOrEmpty(denngay))
            {
                tungay = DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                denngay = DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
            }
            using (var sConnection = new SqlConnection(connectionString))
            {
                if (sConnection.State == ConnectionState.Closed)
                    sConnection.Open();

                using (var sCommand = new SqlCommand("sp_CheckEat_Info", sConnection))
                {
                    sCommand.CommandType = CommandType.StoredProcedure;
                    sCommand.Parameters.Clear();
                    sCommand.Parameters.Add("@param_ThoiDiem", SqlDbType.VarChar).Value = thoidiem;
                    sCommand.Parameters.Add("@param_Dept", SqlDbType.Int).Value = dept;
                    sCommand.Parameters.Add("@param_TuNgay", SqlDbType.VarChar).Value = tungay;
                    sCommand.Parameters.Add("@param_DenNgay", SqlDbType.VarChar).Value = denngay;
                    sCommand.Parameters.Add("@param_NoiAn", SqlDbType.Int).Value = noian;
                    sCommand.Parameters.Add("@param_Loai", SqlDbType.VarChar).Value = loai;
                    sCommand.Parameters.Add("@param_Theo", SqlDbType.VarChar).Value = theo;
                    sCommand.Parameters.Add("@param_Type", SqlDbType.Int).Value = 19;
                    var reader = sCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                    return dt;
                }
            }
        }
    }
}