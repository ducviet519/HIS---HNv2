using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace System.App.Repositories.HCNS
{
    public class SKNV_Repo
    {
        public IEnumerable<SKNV> List(string connectionString, SKNV sknv = null)
        {
            List<SKNV> lst = new List<SKNV>();

            string filter = String.Empty;

            if (sknv != null)
            {
                if (!String.IsNullOrEmpty(sknv.KhoaPhong))
                {
                    filter += " AND c.KhoaP = N'" + sknv.KhoaPhong + "'";
                }
                if (sknv.KetLuan != null && !String.IsNullOrEmpty(sknv.KetLuan.Trim()))
                {
                    filter += " AND a.Value LIKE N'%" + sknv.KetLuan.Trim() + "%'";
                }
                if (sknv.Nam > 0)
                {
                    filter += " AND a.Year = " + sknv.Nam;
                }
                else
                {
                    filter += " AND a.Year = " + DateTime.UtcNow.AddHours(7).Year;
                }
                if (sknv.LanK > 0)
                {
                    filter += " AND a.Times = " + sknv.LanK;
                }
                if (sknv.PhanLoai > 0)
                {
                    filter += " AND a.Type = " + sknv.PhanLoai;
                }
                filter += " AND b.UserFullCode IS NOT NULL";
            }

            string query = @"SELECT
                  ROW_NUMBER() OVER (order by b.UserFullCode) as STT
                  ,b.UserFullCode AS MaNV
	              ,b.UserFullName AS HoTen
	              ,c.KhoaP AS KhoaPhong
                  ,a.Year AS Nam
                  ,a.Times AS LanK
                  ,a.Value AS KetLuan
                  ,a.Type AS PhanLoai
                  ,a.ICD10
              FROM KSK_KL a LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber AND b.DaNghi = 0
              LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT
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
                        while (reader.Read())
                        {
                            lst.Add(new SKNV()
                            {
                                STT = reader["STT"].ToString(),
                                MaNV = reader["MaNV"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                KhoaPhong = reader["KhoaPhong"].ToString(),
                                Nam = String.IsNullOrEmpty(reader["Nam"].ToString()) ? 0 : int.Parse(reader["Nam"].ToString()),
                                LanK = String.IsNullOrEmpty(reader["LanK"].ToString()) ? 0 : int.Parse(reader["LanK"].ToString()),
                                KetLuan = string.IsNullOrEmpty(reader["KetLuan"].ToString()) ? "Bình thường" : reader["KetLuan"].ToString(),
                                PhanLoai = String.IsNullOrEmpty(reader["PhanLoai"].ToString()) ? 1 : int.Parse(reader["PhanLoai"].ToString()),
                                ICD10 = reader["ICD10"].ToString()
                            });
                        }
                    }
                }

                sqlConnection.Close();
            }

            return lst;
        }

        public PLSK_Tron List_PLSK_Tron(string connectionString, PLSK_Tron obj)
        {
            string query = @"SELECT CAST((100*convert(float,KK)/Total) AS DECIMAL(10,2)) AS TyLeKK,CAST((100*convert(float,L1)/Total) AS DECIMAL(10,2)) AS TyLeL1,CAST((100*convert(float,L2)/Total) AS DECIMAL(10,2)) AS TyLeL2,CAST((100*convert(float,L3)/Total) AS DECIMAL(10,2)) AS TyLeL3,CAST((100*convert(float,L4)/Total) AS DECIMAL(10,2)) AS TyLeL4 FROM
                (SELECT [0] AS KK,[1] AS L1,[2] AS L2,[3] AS L3,[4] AS L4,ISNULL([0],0)+ISNULL([1],0)+ISNULL([2],0)+ISNULL([3],0)+ISNULL([4],0) AS Total
                FROM
                (SELECT a.Year AS Nam
                      ,a.Times AS LanKSK
                      ,ISNULL(a.Type,0) AS PLSK
                   ,1 AS Cnt

                  FROM KSK_KL a LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber
                  LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT  WHERE a.Year = " + obj.Year + @" and a.Times = " + obj.Times + @") xx
                  PIVOT
                  (SUM(Cnt) for PLSK in ([0],[1],[2],[3],[4])) AS PivotTable) yy";

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
                            obj.TyLeKK = float.Parse(reader["TyLeKK"].ToString());
                            obj.TyLeL1 = float.Parse(reader["TyLeL1"].ToString());
                            obj.TyLeL2 = float.Parse(reader["TyLeL2"].ToString());
                            obj.TyLeL3 = float.Parse(reader["TyLeL3"].ToString());
                        }
                    }
                }

                sqlConnection.Close();
            }
            return obj;
        }

        public IEnumerable<PLSK> List_PLSK(string connectionString, PLSK plsk = null)
        {
            List<PLSK> lst = new List<PLSK>();

            string filter = String.Empty;

            if (plsk != null)
            {
                if (!String.IsNullOrEmpty(plsk.KhoaPhong))
                {
                    filter += " AND KhoaPhong = N'" + plsk.KhoaPhong + "'";
                }
            }

            string query = @"SELECT ROW_NUMBER() OVER (order by MaNV) as STT, MaNV,HoTen,KhoaPhong,[2017] as N2017,[2018] as N2018, [2019] as N2019
                    FROM
                    (SELECT b.UserFullCode AS MaNV
                       ,b.UserFullName AS HoTen
                       ,c.KhoaP AS KhoaPhong
                          ,a.Year AS Nam
                          ,a.Times AS LanKSK
                          ,ISNULL(a.Type,0) AS PLSK
                      FROM KSK_KL a LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber
                      LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT) xx
                      PIVOT
                      (SUM(PLSK) for Nam in ([2017],[2018],[2019])) AS PivotTable
              WHERE 1 = 1 AND MaNV IS NOT NULL " + filter;

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
                            lst.Add(new PLSK()
                            {
                                STT = int.Parse(reader["STT"].ToString()),
                                MaNV = reader["MaNV"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                KhoaPhong = reader["KhoaPhong"].ToString(),
                                N2017 = String.IsNullOrEmpty(reader["N2017"].ToString()) ? 1 : int.Parse(reader["N2017"].ToString()),
                                N2018 = String.IsNullOrEmpty(reader["N2018"].ToString()) ? 1 : int.Parse(reader["N2018"].ToString()),
                                N2019 = String.IsNullOrEmpty(reader["N2019"].ToString()) ? 1 : int.Parse(reader["N2019"].ToString())
                            });
                        }
                    }
                }

                sqlConnection.Close();
            }

            return lst;
        }
        public IEnumerable<TDSK> List_TDSK(string connectionString, TDSK tdsk = null)
        {
            List<TDSK> lst = new List<TDSK>();

            string filter = String.Empty;

            if (tdsk != null)
            {
                if (!String.IsNullOrEmpty(tdsk.KhoaPhong))
                {
                    filter += " AND KhoaPhong = N'" + tdsk.KhoaPhong + "'";
                }
            }

            string query = @"SELECT ROW_NUMBER() OVER (order by MaNV) as STT, MaNV,HoTen,KhoaPhong,LYear,CYear,
                CASE
                WHEN (UPPER(LYear) like UPPER(N'%bình thường%') OR UPPER(LYear ) like UPPER(N'%đủ sức khỏe%') OR LTRIM(LYear) = '' OR LYear IS NULL)  and (UPPER(CYear ) like UPPER(N'%bình thường%') OR UPPER(CYear ) like UPPER(N'%đủ sức khỏe%') OR LTRIM(CYear) = '') THEN N'Bình thường'
                WHEN (UPPER(LYear) like UPPER(N'%bình thường%') OR UPPER(LYear ) like UPPER(N'%đủ sức khỏe%') OR LTRIM(LYear) = '' OR LYear IS NULL)  and ((UPPER(CYear) not like UPPER(N'%bình thường%') AND UPPER(CYear) not like UPPER(N'%đủ sức khỏe%')) AND LTRIM(CYear) <> '') THEN N'Mới mắc'
                WHEN ((UPPER(LYear) not like UPPER(N'%bình thường%') AND UPPER(LYear) not like UPPER(N'%đủ sức khỏe%')) AND LTRIM(LYear) <> '')  and ((UPPER(CYear) not like UPPER(N'%bình thường%') AND UPPER(CYear) not like UPPER(N'%đủ sức khỏe%')) AND LTRIM(CYear) <> '') THEN N'Hiện mắc'
                WHEN ((UPPER(LYear ) like UPPER(N'%bình thường%') OR UPPER(LYear ) like UPPER(N'%đủ sức khỏe%') OR LTRIM(LYear) = '') OR LYear IS NULL) and (UPPER(CYear) not like UPPER(N'%bình thường%') AND UPPER(CYear) not like UPPER(N'%đủ sức khỏe%') AND LTRIM(CYear) <> '') THEN N'Mới mắc'
                WHEN CYear IS NULL THEN N'Không theo dõi tiếp'
                ELSE N'Khỏi bệnh'
                END AS DienTien
                FROM

                (SELECT MaNV,HoTen,KhoaPhong,[2018] AS LYear,[2019] AS CYear
                FROM
                (SELECT b.UserFullCode AS MaNV
                   ,b.UserFullName AS HoTen
                   ,c.KhoaP AS KhoaPhong
                      ,a.Year AS Nam
                   ,a.Value AS KetLuan

                  FROM KSK_KL a LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber AND b.DaNghi = 0
                  LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT) xx
                  PIVOT
                  (MAX(KetLuan) for Nam in ([2018],[2019])) AS PivotTable) yy
              WHERE 1 = 1  AND MaNV IS NOT NULL " + filter;

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
                            lst.Add(new TDSK()
                            {
                                STT = int.Parse(reader["STT"].ToString()),
                                MaNV = reader["MaNV"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                KhoaPhong = reader["KhoaPhong"].ToString(),
                                LYear = reader["LYear"].ToString(),
                                CYear = reader["CYear"].ToString(),
                                DienTien = reader["DienTien"].ToString()
                            });
                        }
                    }
                }

                sqlConnection.Close();
            }

            return lst;
        }
        public float TyLe(string connectionString, SKNV sknv = null)
        {
            string filter = String.Empty;
            float val = 0;
            if (sknv != null)
            {
                if (sknv.KetLuan != null && !String.IsNullOrEmpty(sknv.KetLuan.Trim()))
                {
                    filter += " a.Value like N'%" + sknv.KetLuan + @"%'";
                }
            }
            string query = @"SELECT CAST((100*convert(float,x.Num)/y.Num) AS DECIMAL(10,2)) AS TyLe FROM
                (SELECT count(*) AS Num
              FROM KSK_KL a LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber
              LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT WHERE a.Year = " + sknv.Nam + @" and a.Times = " + sknv.LanK + @" and " + filter + @") x
              JOIN
              (SELECT count(*) AS Num
              FROM KSK_KL a LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber
              LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT WHERE a.Year = " + sknv.Nam + @" and a.Times = " + sknv.LanK + @") y ON 1 = 1";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    val = float.Parse(sqlCommand.ExecuteScalar().ToString());
                }

                sqlConnection.Close();
            }

            return val;
        }
        public IEnumerable<BieuDo_PLSK> BieuDo_PLSK(string connectionString, string nam, int times)
        {
            List<BieuDo_PLSK> lst = new List<BieuDo_PLSK>();

            string query = @"
            SELECT zz.KhoaP as KhoaPhong,zz.PLSK,yy.SoLuong FROM
            (SELECT KhoaPhong, PLSK, count(Cnt) AS SoLuong
            FROM
            (SELECT b.PhongKhoaHC
               ,c.KhoaP AS KhoaPhong
                  ,a.Year AS Nam
                  ,a.Times AS LanKSK
                  ,ISNULL(a.Type,0) AS PLSK
               ,1 AS Cnt
              FROM KSK_KL a LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber AND b.DaNghi = 0
              LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT WHERE a.Year = " + nam + @" and a.Times = " + times + @") xx GROUP BY KhoaPhong,PLSK) yy FULL JOIN
            (SELECT KhoaP,PLSK,SoLuongAo FROM
            (SELECT 1 AS ID, STT, KhoaP FROM Depts WHERE STT > 0) a FULL JOIN
            (SELECT 1 AS ID, 0 AS PLSK, NULL AS SoLuongAo
            UNION ALL
            SELECT 1 AS ID, 1 AS PLSK, NULL AS SoLuongAo
            UNION ALL
            SELECT 1 AS ID, 2 AS PLSK, NULL AS SoLuongAo
            UNION ALL
            SELECT 1 AS ID, 3 AS PLSK, NULL AS SoLuongAo
            UNION ALL
            SELECT 1 AS ID, 4 AS PLSK, NULL AS SoLuongAo) b ON a.ID = b.ID) zz ON yy.KhoaPhong = zz.KhoaP AND yy.PLSK = zz.PLSK ORDER BY zz.KhoaP,zz.PLSK";

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
                            lst.Add(new BieuDo_PLSK()
                            {
                                KhoaPhong = string.IsNullOrEmpty(reader["KhoaPhong"].ToString()) ? "Không xác định" : reader["KhoaPhong"].ToString(),
                                PLSK = string.IsNullOrEmpty(reader["PLSK"].ToString()) ? 0 : int.Parse(reader["PLSK"].ToString()),
                                SoLuong = string.IsNullOrEmpty(reader["SoLuong"].ToString()) ? 0 : int.Parse(reader["SoLuong"].ToString())
                            });
                        }
                    }
                }

                sqlConnection.Close();
            }

            return lst;
        }
        public IEnumerable<NCBT> List_NCBT_KP(string connectionString, NCBT obj)
        {
            List<NCBT> lst = new List<NCBT>();
            string filter = "";
            if (!String.IsNullOrEmpty(obj.KhoaP))
                filter += " AND KhoaP = N'" + obj.KhoaP + "'";
            filter += " AND Times = " + obj.LanK;

            string query = @"SELECT bb.KhoaP, bb.ICD10,cc.MoTa,bb.LYear,bb.CYear, CASE WHEN CYear > LYear THEN 1 ELSE 0 END AS Alert FROM
                    (SELECT KhoaP,ICD10,ISNULL([2017],0) AS LYear,ISNULL([2018],0) AS CYear FROM
                    (SELECT KhoaP,Year,ICD10,Count(ICD10) AS Cnt FROM
                    (SELECT c.KhoaP, a.Year, a.Times, a.ICD10 AS ICD10 FROM KSK_KL a
                    LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber
                    LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT
                    UNION ALL
                    SELECT c.KhoaP, a.Year, a.Times, a.ICD102 AS ICD10 FROM KSK_KL a
                    LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber
                    LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT
                       WHERE ICD102 IS NOT NULL
                    UNION ALL
                    SELECT c.KhoaP, a.Year, a.Times, a.ICD103 AS ICD10 FROM KSK_KL a
                    LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber
                      LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT
                       WHERE ICD103 IS NOT NULL) aa WHERE 1 = 1 " + filter + @" GROUP BY KhoaP,Year,ICD10) aa
                     PIVOT
                     (SUM(Cnt) for Year in ([2017],[2018])) AS PivotTable WHERE ICD10 IS NOT NULL) bb LEFT JOIN KSK_ICD10 cc ON bb.ICD10 = cc.Code ORDER BY bb.KhoaP";

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
                            lst.Add(new NCBT()
                            {
                                KhoaP = reader["KhoaP"].ToString(),
                                ICD10 = reader["ICD10"].ToString(),
                                MoTa = reader["MoTa"].ToString(),
                                LYear = int.Parse(reader["LYear"].ToString()),
                                CYear = int.Parse(reader["CYear"].ToString()),
                                Alert = reader["Alert"].ToString() == "0" ? false : true
                            });
                        }
                    }
                }

                sqlConnection.Close();
            }
            return lst;
        }

        public IEnumerable<NCBT> List_NCBT_BV(string connectionString, NCBT obj)
        {
            List<NCBT> lst = new List<NCBT>();

            string query = @"SELECT  bb.ICD10,cc.MoTa,bb.LYear,bb.CYear, CASE WHEN CYear > LYear THEN 1 ELSE 0 END AS Alert FROM
                (SELECT ICD10,ISNULL([2017],0) AS LYear,ISNULL([2018],0) AS CYear FROM
                (SELECT Year,ICD10,Count(ICD10) AS Cnt FROM
                (SELECT a.Year, a.Times,  a.ICD10 AS ICD10 FROM KSK_KL a
                LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber
                  LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT
                UNION ALL
                SELECT  a.Year, a.Times, a.ICD102 AS ICD10 FROM KSK_KL a
                LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber
                  LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT
                   WHERE ICD102 IS NOT NULL
                UNION ALL
                SELECT  a.Year, a.Times,  a.ICD103 AS ICD10 FROM KSK_KL a
                LEFT JOIN UserInfExt b ON a.Code = b.UserEnrollNumber
                  LEFT JOIN Depts c ON b.PhongKhoaHC = c.STT
                   WHERE  ICD103 IS NOT NULL) aa WHERE Times = " + obj.LanK + @" GROUP BY Year,ICD10) aa
                 PIVOT
                 (SUM(Cnt) for Year in ([2017],[2018])) AS PivotTable WHERE ICD10 IS NOT NULL) bb LEFT JOIN KSK_ICD10 cc ON bb.ICD10 = cc.Code";

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
                            lst.Add(new NCBT()
                            {
                                KhoaP = "",
                                ICD10 = reader["ICD10"].ToString(),
                                MoTa = reader["MoTa"].ToString(),
                                LYear = int.Parse(reader["LYear"].ToString()),
                                CYear = int.Parse(reader["CYear"].ToString()),
                                Alert = reader["Alert"].ToString() == "0" ? false : true
                            });
                        }
                    }
                }

                sqlConnection.Close();
            }
            return lst;
        }

        public void UPLOAD_EXCEL_THSK(string connectionString, DataTable data)
        {
            DataRow[] rowArray = data.Select();

            using (var sconnection = new SqlConnection(connectionString))
            {
                if (sconnection.State == ConnectionState.Closed)
                    sconnection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sconnection))
                {
                    bulkCopy.DestinationTableName = "dbo.KSK_KL";

                    try
                    {
                        // Write the array of rows to the destination.
                        bulkCopy.WriteToServer(rowArray);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}