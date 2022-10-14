using Dapper;
using System.App.Entities;
using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace System.App.Repositories
{
    public class DoiChieuSuatAn_Repo
    {
        public IEnumerable<Department> ListDepartment(string connectionString)
        {
            string q = "SELECT [ID] as STT, [Description] as KhoaP FROM [RelationDept] WHERE ID > 2";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<Department>(q);
                sqlConnection.Close();
                return result.ToList();
            }
        }

        public IEnumerable<DoiChieuSuatAn> DanhSachSuatAn(string connectionString, string dept, string tungay, string denngay)
        {
            string predicate = "";
            List<DoiChieuSuatAn> lst = new List<DoiChieuSuatAn>();

            if (!String.IsNullOrEmpty(dept) && dept != "0")
            {
                //predicate += " AND c.ID = " + dept;
                predicate += " AND (c.ID = " + dept + " or -1 = " + dept + ")";
            }

            if (String.IsNullOrEmpty(tungay) && String.IsNullOrEmpty(denngay))
            {
                string timeFrom = DateTime.UtcNow.AddHours(7).AddMonths(-1).ToString("yyyyMM24");
                predicate += " AND CONVERT(CHAR(10), a.DateReg, 112) > " + timeFrom;
            }
            else if (!String.IsNullOrEmpty(tungay) && String.IsNullOrEmpty(denngay))
            {
                string timeFrom = DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                predicate += " AND CONVERT(CHAR(10), a.DateReg, 112) >= " + timeFrom;
            }
            else if (String.IsNullOrEmpty(tungay) && !String.IsNullOrEmpty(denngay))
            {
                string timeFrom = DateTime.UtcNow.AddHours(7).AddMonths(-1).ToString("yyyyMM24");
                string timeTo = DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                predicate += " AND CONVERT(CHAR(10), a.DateReg, 112) > " + timeFrom + " AND CONVERT(CHAR(10), a.DateReg, 112) <= " + timeTo;
            }
            else if (!String.IsNullOrEmpty(tungay) && !String.IsNullOrEmpty(denngay))
            {
                string timeFrom = DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                string timeTo = DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                predicate += " AND CONVERT(CHAR(10), a.DateReg, 112) >= " + timeFrom + " AND CONVERT(CHAR(10), a.DateReg, 112) <= " + timeTo;
            }

            string q = "SELECT a.UserEnrollNumber, b.UserFullName, c.Description, CONVERT(CHAR(10), a.DateReg, 103) AS DateReg, a.Lu, a.LuR, a.Di, a.DiR FROM [Sets] a LEFT JOIN UserInfExt b ON a.UserEnrollNumber = b.UserEnrollNumber LEFT JOIN RelationDept c ON b.PhongKhoa = c.ID WHERE 1 = 1 " + predicate + " ORDER BY a.UserEnrollNumber, a.DateReg";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand(q, sqlConnection))
                {
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst.Add(new DoiChieuSuatAn()
                            {
                                UserEnrollNumber = int.Parse(reader["UserEnrollNumber"].ToString()),
                                UserFullName = reader["UserFullName"].ToString(),
                                Description = reader["Description"].ToString(),
                                DateReg = reader["DateReg"].ToString(),
                                Lu = String.IsNullOrEmpty(reader["Lu"].ToString()) ? 0 : int.Parse(reader["Lu"].ToString()),
                                LuR = String.IsNullOrEmpty(reader["LuR"].ToString()) ? 0 : int.Parse(reader["LuR"].ToString()),
                                Di = String.IsNullOrEmpty(reader["Di"].ToString()) ? 0 : int.Parse(reader["Di"].ToString()),
                                DiR = String.IsNullOrEmpty(reader["DiR"].ToString()) ? 0 : int.Parse(reader["DiR"].ToString())
                            });
                        }
                    }
                }
            }

            return lst;
        }

        public DataTable DanhSachThongKe(string connectionString, string dept, string tungay, string denngay, string thoidiem)
        {
            DataTable dt = new DataTable();
            string slstr = "";
            string setstr = "";
            string tostr = "(";
            string sumstr = "";
            string depsel = "";
            if (Convert.ToInt32(dept) > 0)
                depsel = " WHERE a.PhongKhoa = " + dept + " AND DaNghi = 0";
            else
                depsel = " WHERE DaNghi = 0";
            string sttype = thoidiem;
            int days = (DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture) - DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture)).Days;
            DateTime dTungay = DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            for (int i = 0; i <= days; i++)
            {
                slstr += "[" + dTungay.AddDays(i).ToString("yyyy-MM-dd") + "] , ";
                sumstr += "SUM([" + dTungay.AddDays(i).ToString("yyyy-MM-dd") + "]) , ";
                setstr += "[" + dTungay.AddDays(i).ToString("yyyy-MM-dd") + "] , ";
                tostr += "IsNull([" + dTungay.AddDays(i).ToString("yyyy-MM-dd") + "], 0)+";
                //slstr += "[" + dTungay.AddDays(i).ToString("dd/MM/yyyy") + "] , ";
                //sumstr += "SUM([" + dTungay.AddDays(i).ToString("dd/MM/yyyy") + "]) , ";
                //setstr += "[" + dTungay.AddDays(i).ToString("dd/MM/yyyy") + "] , ";
                //tostr += "IsNull([" + dTungay.AddDays(i).ToString("dd/MM/yyyy") + "], 0)+";
            }
            if (days >= 0)
            {
                setstr = setstr.Remove(setstr.Length - 2);
                slstr = slstr.Remove(slstr.Length - 2);
                sumstr = sumstr.Remove(sumstr.Length - 2);
                tostr = tostr.Remove(tostr.Length - 1) + ")";
            }

            string qstr = "SELECT 1 sortby, UserFullCode, UserFullName, KhoaP, " + tostr + " AS Total, " + slstr + " FROM (SELECT a.UserFullCode, a.UserFullName, d.Description as KhoaP, b.DateReg, b.TASet FROM UserInfExt a LEFT JOIN RelationDept d ON d.ID = a.PhongKhoa LEFT JOIN (SELECT UserEnrollNumber, DateReg, (SELECT MAX(TASet) FROM (VALUES (" + sttype + "),(" + sttype + "R)) AS TASet(TASet)) AS TASet FROM Sets) b ON a.UserEnrollNumber = b.UserEnrollNumber" + depsel + ") src pivot (sum(TASet) FOR DateReg IN (" + setstr + ")) AS piv" +
                " UNION ALL SELECT 2 sortby, '', N'Tổng', '', SUM" + tostr + " AS Total, " + sumstr + " FROM (SELECT a.UserFullCode, a.UserFullName, d.Description as KhoaP, b.DateReg, b.TASet FROM UserInfExt a LEFT JOIN RelationDept d ON d.ID = a.PhongKhoa LEFT JOIN (SELECT UserEnrollNumber, DateReg, (SELECT MAX(TASet) FROM (VALUES (" + sttype + "),(" + sttype + "R)) AS TASet(TASet)) AS TASet FROM Sets) b ON a.UserEnrollNumber = b.UserEnrollNumber" + depsel + ") src pivot (sum(TASet) FOR DateReg IN (" + setstr + ")) AS piv ORDER BY sortby,UserFullCode ASC";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand(qstr, sqlConnection))
                {
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
            }

            return dt;
        }

        public DataTable DanhSachThongKe_Excel(string connectionString, string dept, string tungay, string denngay, string thoidiem)
        {
            DataTable dt = new DataTable();
            string slstr = "";
            string setstr = "";
            string tostr = "(";
            string sumstr = "";
            string depsel = "";
            if (Convert.ToInt32(dept) > 0)
                depsel = " WHERE a.PhongKhoa = " + dept + " AND DaNghi = 0";
            else
                depsel = " WHERE DaNghi = 0";
            string sttype = thoidiem;
            int days = (DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture) - DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture)).Days;
            DateTime dTungay = DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            for (int i = 0; i <= days; i++)
            {
                slstr += "[" + dTungay.AddDays(i).ToString("yyyy-MM-dd") + "] , ";
                sumstr += "SUM([" + dTungay.AddDays(i).ToString("yyyy-MM-dd") + "]) , ";
                setstr += "[" + dTungay.AddDays(i).ToString("yyyy-MM-dd") + "] , ";
                tostr += "IsNull([" + dTungay.AddDays(i).ToString("yyyy-MM-dd") + "], 0)+";
            }
            if (days >= 0)
            {
                setstr = setstr.Remove(setstr.Length - 2);
                slstr = slstr.Remove(slstr.Length - 2);
                sumstr = sumstr.Remove(sumstr.Length - 2);
                tostr = tostr.Remove(tostr.Length - 1) + ")";
            }

            string qstr = "SELECT 1 sortby, UserFullCode as 'Mã nhân viên', UserFullName as N'Họ tên', KhoaP as 'Khoa phòng', " + tostr + " AS 'Tổng', " + slstr + " FROM (SELECT a.UserFullCode, a.UserFullName, d.Description as KhoaP, b.DateReg, b.TASet FROM UserInfExt a LEFT JOIN RelationDept d ON d.ID = a.PhongKhoa LEFT JOIN (SELECT UserEnrollNumber, DateReg, (SELECT MAX(TASet) FROM (VALUES (" + sttype + "),(" + sttype + "R)) AS TASet(TASet)) AS TASet FROM Sets) b ON a.UserEnrollNumber = b.UserEnrollNumber" + depsel + ") src pivot (sum(TASet) FOR DateReg IN (" + setstr + ")) AS piv" +
                " UNION ALL SELECT 2 sortby, '' as 'Mã nhân viên', N'Tổng' as 'Họ tên', '' as 'Khoa phòng', SUM" + tostr + " AS 'Tổng', " + sumstr + " FROM (SELECT a.UserFullCode, a.UserFullName, d.Description as KhoaP, b.DateReg, b.TASet FROM UserInfExt a LEFT JOIN RelationDept d ON d.ID = a.PhongKhoa LEFT JOIN (SELECT UserEnrollNumber, DateReg, (SELECT MAX(TASet) FROM (VALUES (" + sttype + "),(" + sttype + "R)) AS TASet(TASet)) AS TASet FROM Sets) b ON a.UserEnrollNumber = b.UserEnrollNumber" + depsel + ") src pivot (sum(TASet) FOR DateReg IN (" + setstr + ")) AS piv ORDER BY sortby,UserFullCode ASC";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand(qstr, sqlConnection))
                {
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
            }

            return dt;
        }

        public DataTable DanhSachSuatAnExcel(string connectionString, string dept, string tungay, string denngay)
        {
            string predicate = "";
            DataTable dataTable = new DataTable();

            if (!String.IsNullOrEmpty(dept) && dept != "0")
            {
                //predicate += " AND c.ID = " + dept;
                predicate += " AND (c.ID = " + dept + " or -1 = " + dept + ")";
            }
            if (String.IsNullOrEmpty(tungay) && String.IsNullOrEmpty(denngay))
            {
                string timeFrom = DateTime.UtcNow.AddHours(7).AddMonths(-1).ToString("yyyyMM24");
                predicate += " AND CONVERT(CHAR(10), a.DateReg, 112) > " + timeFrom;
            }
            else if (!String.IsNullOrEmpty(tungay) && String.IsNullOrEmpty(denngay))
            {
                string timeFrom = DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                predicate += " AND CONVERT(CHAR(10), a.DateReg, 112) >= " + timeFrom;
            }
            else if (String.IsNullOrEmpty(tungay) && !String.IsNullOrEmpty(denngay))
            {
                string timeFrom = DateTime.UtcNow.AddHours(7).AddMonths(-1).ToString("yyyyMM24");
                string timeTo = DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                predicate += " AND CONVERT(CHAR(10), a.DateReg, 112) > " + timeFrom + " AND CONVERT(CHAR(10), a.DateReg, 112) <= " + timeTo;
            }
            else if (!String.IsNullOrEmpty(tungay) && !String.IsNullOrEmpty(denngay))
            {
                string timeFrom = DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                string timeTo = DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                predicate += " AND CONVERT(CHAR(10), a.DateReg, 112) >= " + timeFrom + " AND CONVERT(CHAR(10), a.DateReg, 112) <= " + timeTo;
            }

            string q = "SELECT a.UserEnrollNumber as 'Mã nhân viên', b.UserFullName as 'Họ và tên', c.Description as 'Khoa/Phòng', CONVERT(CHAR(10), a.DateReg, 103) AS 'Ngày đăng ký', a.Lu as 'ĐK - Ăn trưa', a.LuR as 'VT - Ăn trưa', a.Di as 'ĐK - Ăn tối', a.DiR as 'VT - Ăn tối' FROM [Sets] a LEFT JOIN UserInfExt b ON a.UserEnrollNumber = b.UserEnrollNumber LEFT JOIN RelationDept c ON b.PhongKhoa = c.ID WHERE 1 = 1 " + predicate + " ORDER BY a.UserEnrollNumber, a.DateReg";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand(q, sqlConnection))
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    sqlDataAdapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
        public bool PushData(string connectionString, AbsentR obj)
        {
            //string query = @"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
            //        BEGIN TRANSACTION;
            //          INSERT INTO ChamAn.dbo.SetsCache (DateReg, UserEnrollNumber, LuR, DiR)
            //            (SELECT
            //              *
            //            FROM (SELECT DISTINCT
            //              (CASE
            //                WHEN SDL IS NOT NULL AND
            //                  SDD IS NOT NULL THEN SDL
            //                WHEN SDL IS NULL AND
            //                  SDD IS NOT NULL THEN SDD
            //                WHEN SDL IS NOT NULL AND
            //                  SDD IS NULL THEN SDL
            //              END) AS DateReg,
            //              (CASE
            //                WHEN UL IS NOT NULL AND
            //                  UD IS NOT NULL THEN UL
            //                WHEN UL IS NULL AND
            //                  UD IS NOT NULL THEN UD
            //                WHEN UL IS NOT NULL AND
            //                  UD IS NULL THEN UL
            //              END) AS UserEnrollNumber,
            //              LuR,
            //              DiR
            //            FROM (SELECT
            //              a.SetDate AS SDL,
            //              b.SetDate AS SDD,
            //              a.UserEnrollNumber AS UL,
            //              b.UserEnrollNumber AS UD,
            //              LuR,
            //              DiR
            //            FROM (SELECT
            //              UserEnrollNumber,
            //              1 AS LuR,
            //              CONVERT(date, TimeStr) AS SetDate
            //            FROM CheckInOutEat
            //            WHERE CAST(TimeStr AS time) > '10:00:00'
            //            AND CAST(TimeStr AS time) < '15:00:00') a
            //            FULL OUTER JOIN (SELECT
            //              UserEnrollNumber,
            //              1 AS DiR,
            //              CONVERT(date, TimeStr) AS SetDate
            //            FROM CheckInOutEat
            //            WHERE CAST(TimeStr AS time) > '17:00:00'
            //            AND CAST(TimeStr AS time) < '22:00:00') b
            //              ON a.UserEnrollNumber = b.UserEnrollNumber
            //              AND a.SetDate = b.SetDate) mintab1) CacheTab
            //            WHERE DateReg > DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 23)
            //            AND DateReg < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 25))
            //          MERGE INTO Sets WITH (HOLDLOCK) AS target USING ChamAn.dbo.SetsCache AS SOURCE ON target.UserEnrollNumber = source.UserEnrollNumber AND target.DateReg = source.DateReg AND (source.DiR IS NOT NULL OR source.LuR IS NOT NULL) WHEN MATCHED THEN UPDATE SET target.DiR = source.DiR, target.LuR = source.LuR WHEN NOT MATCHED BY TARGET THEN INSERT (UserEnrollNumber, DateReg, Lur, DiR) VALUES (source.UserEnrollNumber, source.DateReg, source.LuR, source.DiR);
            //          DELETE FROM [ChamAn].[dbo].[SetsCache]
            //        COMMIT TRANSACTION;";

            //int rowAffected = 0;

            //using (var sconnection = new SqlConnection(connectionString))
            //{
            //    if (sconnection.State == ConnectionState.Closed)
            //        sconnection.Open();

            //    using (var scmd = new SqlCommand(query, sconnection))
            //    {
            //        rowAffected = scmd.ExecuteNonQuery();
            //    }
            //}

            //return rowAffected > 0 ? true : false;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("param_TuNgay", DateTime.ParseExact(obj.From, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                parameters.Add("param_DenNgay", DateTime.ParseExact(obj.To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).ToString("yyyyMMdd"));
                parameters.Add("param_Type", 18);
                return db.Execute("sp_CheckEat_Info", parameters, commandType: CommandType.StoredProcedure) > 0 ? true : false;
            }
        }
    }
}