using Dapper;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace System.App.Repositories.HCNS
{
    public class DaoTaoChungChi_Repo
    {
        
        public async Task<string> AddDaoTaoAsync(string connectionString, DaoTaoChungChi model)
        {
            string result = "";
            DateTime? SDate = null;
            DateTime? EDate = null;
            if (!String.IsNullOrEmpty(model.SDate) && !String.IsNullOrEmpty(model.EDate))
            {
                SDate = DateTime.ParseExact(model.SDate, "dd/MM/yyyy", null);
                EDate = DateTime.ParseExact(model.EDate, "dd/MM/yyyy", null);
            }
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(connectionString))
                {
                    dbConnection.Open();
                    var data = await dbConnection.QueryAsync<DaoTaoChungChi>("sp_123456",
                        new
                        {
                            UserFullCode = model.UserFullCode,
                            UserFullName = model.UserFullName,
                            ChucDanh = model.ChucDanh,
                            KhoaPhong = model.KhoaPhong,
                            TenKhoaDaoTao = model.TenKhoaDaoTao,
                            DonViToChuc = model.DonViToChuc,
                            SDate = SDate,
                            EDate = EDate,
                            SoTiet = model.SoTiet,
                            ChiPhi = model.ChiPhi,
                            GhiChu = model.GhiChu,
                            LoaiDaoTao = model.LoaiDaoTao,
                            LyDo = model.LyDo
                        },
                        commandType: CommandType.StoredProcedure);
                    if (data != null)
                    {
                        result = "OK";
                    }
                    dbConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return result;
            }
        }

        public async Task<string> EditDaoTaoAsync(string connectionString, DaoTaoChungChi model)
        {
            string result = "";
            DateTime? SDate = null;
            DateTime? EDate = null;
            if (!String.IsNullOrEmpty(model.SDate) && !String.IsNullOrEmpty(model.EDate))
            {
                SDate = DateTime.ParseExact(model.SDate, "dd/MM/yyyy", null);
                EDate = DateTime.ParseExact(model.EDate, "dd/MM/yyyy", null);
            }
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(connectionString))
                {
                    dbConnection.Open();
                    var data = await dbConnection.QueryAsync<DaoTaoChungChi>("sp_123456",
                        new
                        {
                            ID = model.IDDaoTao,
                            UserFullCode = model.UserFullCode,
                            UserFullName = model.UserFullName,
                            ChucDanh = model.ChucDanh,
                            KhoaPhong = model.KhoaPhong,
                            TenKhoaDaoTao = model.TenKhoaDaoTao,
                            DonViToChuc = model.DonViToChuc,
                            SDate = SDate,
                            EDate = EDate,
                            SoTiet = model.SoTiet,
                            ChiPhi = model.ChiPhi,
                            GhiChu = model.GhiChu,
                            LoaiDaoTao = model.LoaiDaoTao,
                            LyDo = model.LyDo
                        },
                        commandType: CommandType.StoredProcedure);
                    if (data != null)
                    {
                        result = "OK";
                    }
                    dbConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return result;
            }
        }

        public async Task<List<DaoTaoChungChi>> GetAllDaoTaoListAsync(string connectionString)
        {
            List<DaoTaoChungChi> data = new List<DaoTaoChungChi>();
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(connectionString))
                {
                    dbConnection.Open();
                    data = (await dbConnection.QueryAsync<DaoTaoChungChi>("sp_123456", commandType: CommandType.StoredProcedure)).ToList();
                    dbConnection.Close();
                }
                return data;
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return data;
            }
        }

        public async Task<DaoTaoChungChi> GetDaoTaoByIDAsync(string connectionString, int id)
        {
            DaoTaoChungChi data = new DaoTaoChungChi();
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(connectionString))
                {
                    dbConnection.Open();
                    data = (await dbConnection.QueryAsync<DaoTaoChungChi>("sp_123456",
                        new
                        {
                            ID = id
                        }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                    dbConnection.Close();
                }
                return data;
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return data;
            }
        }
    }
}
