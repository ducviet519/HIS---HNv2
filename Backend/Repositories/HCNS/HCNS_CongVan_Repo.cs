using Dapper;
using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace System.App.Repositories.HCNS
{
    public class HCNS_CongVan_Repo
    {
        public IEnumerable<HCNS_CongVan> DanhSachCongVan(string connectionString, HCNS_CongVan obj)
        {
            string filter = "";

            if (!String.IsNullOrEmpty(obj.NOI_NHAN.ToString()) && obj.NOI_NHAN != 0)
            {
                filter += " AND NOI_NHAN = " + obj.NOI_NHAN;
            }
            if (!String.IsNullOrEmpty(obj.SO_CV))
            {
                filter += " AND SO_CV LIKE '%" + obj.SO_CV + "%'";
            }
            filter += " AND [SO_CV] != ''";
            string q = @"
                    SELECT TOP 1000 [ID]
                      ,[SO_CV]
                      ,[NOI_DUNG]
                      ,[NOI_NHAN]
                      , d.KhoaP AS [TEN_NOI_NHAN]
                      ,[NGUOI_NHAN]
	                  , b.UserFullCode AS [MA_NGUOI_NHAN]
	                  , b.UserFullName AS [TEN_NGUOI_NHAN]
                      ,CONVERT(VARCHAR,[NGAY_GUI], 103) AS [NGAY_GUI]
                      ,[THANG_GUI]
                      ,CONVERT(VARCHAR,[NGAY_NHAN], 103) AS [NGAY_NHAN]
                      ,[THANG_NHAN]
                      ,[NGUOI_THUC_HIEN]
	                  , c.UserFullCode AS [MA_NGUOI_THUC_HIEN]
	                  , c.UserFullName AS [TEN_NGUOI_THUC_HIEN]
                      ,CONVERT(VARCHAR,[NGAY_XL_DU_KIEN], 103) AS [NGAY_XL_DU_KIEN]
                      ,[THOI_GIAN_XL_DU_KIEN]
                      ,CONVERT(VARCHAR,[NGAY_XL_THUC_TE], 103) AS [NGAY_XL_THUC_TE]
                      ,[THOI_GIAN_XL_THUC_TE]
                      , CONVERT(VARCHAR,[NGAY_TAO], 103) AS [NGAY_TAO]
                      ,[NGUOI_TAO]
                      ,CONVERT(VARCHAR,[NGAY_CAP_NHAT], 103) AS [NGAY_CAP_NHAT]
                      ,[NGUOI_CAP_NHAT]
                      ,[FILE_PATH]
                      ,[TEN_FILE]
                      ,[TRANG_THAI]
                  FROM [HCNS_CongVan] a
	                LEFT JOIN UserInfExt b ON b.UserEnrollNumber = a.NGUOI_NHAN
	                LEFT JOIN UserInfExt c ON c.UserEnrollNumber = a.NGUOI_THUC_HIEN
                    LEFT JOIN Depts d ON d.STT = a.NOI_NHAN
                WHERE 1 = 1" + filter;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<HCNS_CongVan>(q);
                sqlConnection.Close();
                return result.ToList();
            }
        }
        public HCNS_CongVan ThongTinCongVan(string connectionString, HCNS_CongVan obj)
        {
            string query = @"
                SELECT [ID]
                      ,[SO_CV]
                      ,[NOI_DUNG]
                      ,[NOI_NHAN]
                      , d.KhoaP AS [TEN_NOI_NHAN]
                      ,[NGUOI_NHAN]
	                  , b.UserFullCode AS [MA_NGUOI_NHAN]
	                  , b.UserFullName AS [TEN_NGUOI_NHAN]
                      ,CONVERT(VARCHAR,[NGAY_GUI], 103) AS [NGAY_GUI]
                      ,[THANG_GUI]
                      ,CONVERT(VARCHAR,[NGAY_NHAN], 103) AS [NGAY_NHAN]
                      ,[THANG_NHAN]
                      ,[NGUOI_THUC_HIEN]
	                  , c.UserFullCode AS [MA_NGUOI_THUC_HIEN]
	                  , c.UserFullName AS [TEN_NGUOI_THUC_HIEN]
                      ,CONVERT(VARCHAR,[NGAY_XL_DU_KIEN], 103) AS [NGAY_XL_DU_KIEN]
                      ,[THOI_GIAN_XL_DU_KIEN]
                      ,CONVERT(VARCHAR,[NGAY_XL_THUC_TE], 103) AS [NGAY_XL_THUC_TE]
                      ,[THOI_GIAN_XL_THUC_TE]
                      , CONVERT(VARCHAR,[NGAY_TAO], 103) AS [NGAY_TAO]
                      ,[NGUOI_TAO]
                      ,CONVERT(VARCHAR,[NGAY_CAP_NHAT], 103) AS [NGAY_CAP_NHAT]
                      ,[NGUOI_CAP_NHAT]
                      ,[FILE_PATH]
                      ,[TEN_FILE]
                      ,[TRANG_THAI]
                  FROM [HCNS_CongVan] a
	                LEFT JOIN UserInfExt b ON b.UserEnrollNumber = a.NGUOI_NHAN
	                LEFT JOIN UserInfExt c ON c.UserEnrollNumber = a.NGUOI_THUC_HIEN
                    LEFT JOIN Depts d ON d.STT = a.NOI_NHAN
                WHERE 1 = 1 AND ID = @ID";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var result = sqlConnection.Query<HCNS_CongVan>(query, new { @ID = obj.ID });
                sqlConnection.Close();
                return result.SingleOrDefault();
            }
        }
        public bool ThemMoiCongVan(string connectionString, HCNS_CongVan obj)
        {
            int rowAffected = 0;

            string query = "INSERT INTO [HCNS_CongVan](SO_CV, NOI_DUNG, NOI_NHAN, NGUOI_NHAN, NGAY_GUI, THANG_GUI, NGAY_NHAN, THANG_NHAN, NGUOI_THUC_HIEN, NGAY_XL_DU_KIEN, NGAY_XL_THUC_TE, NGAY_TAO, NGUOI_TAO, NGAY_CAP_NHAT, NGUOI_CAP_NHAT, FILE_PATH, TEN_FILE, TRANG_THAI)" +
                "VALUES (@SO_CV, @NOI_DUNG, @NOI_NHAN, @NGUOI_NHAN, @NGAY_GUI, @THANG_GUI, @NGAY_NHAN, @THANG_NHAN, @NGUOI_THUC_HIEN, @NGAY_XL_DU_KIEN, @NGAY_XL_THUC_TE, @NGAY_TAO, @NGUOI_TAO, @NGAY_CAP_NHAT, @NGUOI_CAP_NHAT, @FILE_PATH, @TEN_FILE, @TRANG_THAI)";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var scmd = new SqlCommand(query, sqlConnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@SO_CV", SqlDbType.VarChar).Value = obj.SO_CV;
                    scmd.Parameters.Add("@NOI_DUNG", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(obj.NOI_DUNG) ? "" : obj.NOI_DUNG.ToString();
                    scmd.Parameters.Add("@NOI_NHAN", SqlDbType.Int).Value = obj.NOI_NHAN;
                    scmd.Parameters.Add("@NGUOI_NHAN", SqlDbType.Int).Value = obj.NGUOI_NHAN;

                    if (string.IsNullOrEmpty(obj.NGAY_GUI))
                    {
                        scmd.Parameters.Add("@NGAY_GUI", SqlDbType.Date).Value = DBNull.Value;
                    }
                    else
                    {
                        scmd.Parameters.Add("@NGAY_GUI", SqlDbType.Date).Value = DateTime.ParseExact(obj.NGAY_GUI, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    scmd.Parameters.Add("@THANG_GUI", SqlDbType.Int).Value = obj.THANG_GUI;

                    if (string.IsNullOrEmpty(obj.NGAY_NHAN))
                    {
                        scmd.Parameters.Add("@NGAY_NHAN", SqlDbType.Date).Value = DBNull.Value;
                    }
                    else
                    {
                        scmd.Parameters.Add("@NGAY_NHAN", SqlDbType.Date).Value = DateTime.ParseExact(obj.NGAY_NHAN, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    scmd.Parameters.Add("@THANG_NHAN", SqlDbType.Int).Value = obj.THANG_NHAN;
                    scmd.Parameters.Add("@NGUOI_THUC_HIEN", SqlDbType.Int).Value = obj.NGUOI_THUC_HIEN;

                    if (string.IsNullOrEmpty(obj.NGAY_XL_DU_KIEN))
                    {
                        scmd.Parameters.Add("@NGAY_XL_DU_KIEN", SqlDbType.Date).Value = DBNull.Value;
                    }
                    else
                    {
                        scmd.Parameters.Add("@NGAY_XL_DU_KIEN", SqlDbType.Date).Value = DateTime.ParseExact(obj.NGAY_XL_DU_KIEN, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (string.IsNullOrEmpty(obj.NGAY_XL_THUC_TE))
                    {
                        scmd.Parameters.Add("@NGAY_XL_THUC_TE", SqlDbType.Date).Value = DBNull.Value;
                    }
                    else
                    {
                        scmd.Parameters.Add("@NGAY_XL_THUC_TE", SqlDbType.Date).Value = DateTime.ParseExact(obj.NGAY_XL_THUC_TE, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    //scmd.Parameters.Add("@THOI_GIAN_XL_DU_KIEN", SqlDbType.Int).Value = obj.THOI_GIAN_XL_DU_KIEN;
                    //scmd.Parameters.Add("@THOI_GIAN_XL_THUC_TE", SqlDbType.Int).Value = obj.THOI_GIAN_XL_THUC_TE;
                    scmd.Parameters.Add("@NGAY_TAO", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                    scmd.Parameters.Add("@NGUOI_TAO", SqlDbType.VarChar).Value = obj.NGUOI_TAO;
                    scmd.Parameters.Add("@NGAY_CAP_NHAT", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                    scmd.Parameters.Add("@NGUOI_CAP_NHAT", SqlDbType.VarChar).Value = obj.NGUOI_CAP_NHAT;
                    scmd.Parameters.Add("@FILE_PATH", SqlDbType.VarChar).Value = string.IsNullOrEmpty(obj.FILE_PATH) ? "" : obj.FILE_PATH.ToString();
                    scmd.Parameters.Add("@TEN_FILE", SqlDbType.VarChar).Value = string.IsNullOrEmpty(obj.TEN_FILE) ? "" : obj.TEN_FILE.ToString();
                    scmd.Parameters.Add("@TRANG_THAI", SqlDbType.Int).Value = obj.TRANG_THAI;

                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            return rowAffected > 0 ? true : false;
        }
        public bool CapNhatCongVan(string connectionString, HCNS_CongVan obj)
        {
            int rowAffected = 0;

            string query = @"UPDATE [HCNS_CongVan] SET
                        SO_CV = @SO_CV,
                        NOI_DUNG = @NOI_DUNG,
                        NOI_NHAN = @NOI_NHAN,
                        NGUOI_NHAN = @NGUOI_NHAN,
                        NGAY_GUI = @NGAY_GUI,
                        THANG_GUI = THANG_GUI,
                        NGAY_NHAN = @NGAY_NHAN,
                        THANG_NHAN = @THANG_NHAN,
                        NGUOI_THUC_HIEN = @NGUOI_THUC_HIEN,
                        NGAY_XL_DU_KIEN = @NGAY_XL_DU_KIEN,
                        NGAY_XL_THUC_TE = @NGAY_XL_THUC_TE,
                        NGAY_CAP_NHAT = @NGAY_CAP_NHAT,
                        NGUOI_CAP_NHAT = @NGUOI_CAP_NHAT,
                        FILE_PATH = @FILE_PATH,
                        TEN_FILE = @TEN_FILE,
                        TRANG_THAI = @TRANG_THAI
                    WHERE ID = @ID";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using (var scmd = new SqlCommand(query, sqlConnection))
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.Add("@ID", SqlDbType.Int).Value = obj.ID;
                    scmd.Parameters.Add("@SO_CV", SqlDbType.VarChar).Value = string.IsNullOrEmpty(obj.SO_CV) ? "" : obj.SO_CV;
                    scmd.Parameters.Add("@NOI_DUNG", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(obj.NOI_DUNG) ? "" : obj.NOI_DUNG.ToString();
                    scmd.Parameters.Add("@NOI_NHAN", SqlDbType.Int).Value = obj.NOI_NHAN;
                    scmd.Parameters.Add("@NGUOI_NHAN", SqlDbType.Int).Value = obj.NGUOI_NHAN;

                    if (string.IsNullOrEmpty(obj.NGAY_GUI))
                    {
                        scmd.Parameters.Add("@NGAY_GUI", SqlDbType.Date).Value = DBNull.Value;
                    }
                    else
                    {
                        scmd.Parameters.Add("@NGAY_GUI", SqlDbType.Date).Value = DateTime.ParseExact(obj.NGAY_GUI, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    scmd.Parameters.Add("@THANG_GUI", SqlDbType.Int).Value = obj.THANG_GUI;

                    if (string.IsNullOrEmpty(obj.NGAY_NHAN))
                    {
                        scmd.Parameters.Add("@NGAY_NHAN", SqlDbType.Date).Value = DBNull.Value;
                    }
                    else
                    {
                        scmd.Parameters.Add("@NGAY_NHAN", SqlDbType.Date).Value = DateTime.ParseExact(obj.NGAY_NHAN, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    scmd.Parameters.Add("@THANG_NHAN", SqlDbType.Int).Value = obj.THANG_NHAN;
                    scmd.Parameters.Add("@NGUOI_THUC_HIEN", SqlDbType.Int).Value = obj.NGUOI_THUC_HIEN;

                    if (string.IsNullOrEmpty(obj.NGAY_XL_DU_KIEN))
                    {
                        scmd.Parameters.Add("@NGAY_XL_DU_KIEN", SqlDbType.Date).Value = DBNull.Value;
                    }
                    else
                    {
                        scmd.Parameters.Add("@NGAY_XL_DU_KIEN", SqlDbType.Date).Value = DateTime.ParseExact(obj.NGAY_XL_DU_KIEN, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (string.IsNullOrEmpty(obj.NGAY_XL_THUC_TE))
                    {
                        scmd.Parameters.Add("@NGAY_XL_THUC_TE", SqlDbType.Date).Value = DBNull.Value;
                    }
                    else
                    {
                        scmd.Parameters.Add("@NGAY_XL_THUC_TE", SqlDbType.Date).Value = DateTime.ParseExact(obj.NGAY_XL_THUC_TE, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    //scmd.Parameters.Add("@THOI_GIAN_XL_DU_KIEN", SqlDbType.Int).Value = obj.THOI_GIAN_XL_DU_KIEN;
                    //scmd.Parameters.Add("@THOI_GIAN_XL_THUC_TE", SqlDbType.Int).Value = obj.THOI_GIAN_XL_THUC_TE;
                    scmd.Parameters.Add("@NGAY_TAO", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                    scmd.Parameters.Add("@NGUOI_TAO", SqlDbType.VarChar).Value = string.IsNullOrEmpty(obj.NGUOI_TAO) ? "" : obj.NGUOI_TAO.ToString();
                    scmd.Parameters.Add("@NGAY_CAP_NHAT", SqlDbType.DateTime).Value = DateTime.UtcNow.AddHours(7);
                    scmd.Parameters.Add("@NGUOI_CAP_NHAT", SqlDbType.VarChar).Value = string.IsNullOrEmpty(obj.NGUOI_CAP_NHAT) ? "" : obj.NGUOI_CAP_NHAT.ToString();
                    scmd.Parameters.Add("@FILE_PATH", SqlDbType.VarChar).Value = string.IsNullOrEmpty(obj.FILE_PATH) ? "" : obj.FILE_PATH.ToString();
                    scmd.Parameters.Add("@TEN_FILE", SqlDbType.VarChar).Value = string.IsNullOrEmpty(obj.TEN_FILE) ? "" : obj.TEN_FILE.ToString();
                    scmd.Parameters.Add("@TRANG_THAI", SqlDbType.Int).Value = obj.TRANG_THAI;

                    rowAffected = scmd.ExecuteNonQuery();
                }
            }
            return rowAffected > 0 ? true : false;
        }
    }
}