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

        public async Task<IEnumerable<DaoTaoChungChi>> GetAllDaoTaoListAsync(string connectionString)
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

        public async Task<HCNS_NhanVien> TimThongTinNhanVienAsync(string connectionString, HCNS_NhanVien nv)
        {
            string filter = "";

            if (!string.IsNullOrEmpty(nv.TaiKhoan))
            {
                filter += " AND a.UPN = '" + nv.TaiKhoan + "'";
            }
            else if (!string.IsNullOrEmpty(nv.UserFullCode))
            {
                filter += " AND a.UserFullCode = '" + nv.UserFullCode + "'";
            }
            else
            {
                filter += " AND a.UserEnrollNumber = " + nv.UserEnrollNumber;
            }

            string query = @"SELECT a.UserEnrollNumber, a.UserFullCode, a.UserFullName, a.ChucDanh, a.Email, a.SDT1, a.SDT2, a.SDTNB, a.SoGioLV, a.NgayVao, d.KhoaP as Ten_KhoaPhong, a.PhongKhoa, a.PhongKhoaHC, a.NhomChucDanh, a.Lead, a.SaEmail, a.LoaiNV,  a.UType, 
                        a.NoiSinh, a.NoiSinhCT, CONVERT(char(10), a.NgaySinh, 103) as NgaySinh, a.QuocTich, a.DanToc, a.TonGiao, a.LoaiCMT, a.SoCMT, CONVERT(char(10), a.CapNgay, 103) as CapNgay, a.NoiCap, a.TATitle, a.GioiTinh, a.UType as DoiTuong,
                        a.QGThT, a.TinhThT, a.QuanThT, a.PhuongThT, a.DcThT,
                        a.QGTT, a.TinhTT, a.QuanTT, a.PhuongTT, a.DcTT,
                        a.TTHN as TTHonNhan, a.SoTK as SoTK1, a.NganHang as NganHang1, a.SoTK2, a.NganHang2, a.TTSK, a.MSTCN, CONVERT(char(10), x1.SCD, 103) as NgayTDLuong, CONVERT(char(10), a.NgayPC, 103) as NgayPC, a.AnhDaiDien, a.NgayNghi, a.NgayLVCC, a.LyDoNghi, a.DaNghi, ISNULL(a.TinhLuong,0) AS TinhLuong,
                        a.SoBHXH, a.SoTheBHXH, a.NgayTGBHXH, a.LoaiDongBHXH, a.MucDongBHXH, a.UPN, b.ID_EatPlace as NoiAn, CONVERT(char(10), a.NgayTinhPhep, 103) as NgayTinhPhep, a.CongDoan
                        FROM UserInfExt a
						left join CheckEat_Print b on a.UserEnrollNumber = b.UserEnrollNumber
						LEFT JOIN Depts d ON a.PhongKhoaHC = d.STT
                        LEFT JOIN RelationDept r ON a.PhongKhoa = r.ID
                        outer apply
	                    (
		                    SELECT TOP 1 SCD FROM dbo.SalaryChange (nolock) WHERE UserEnrollNumber = a.UserEnrollNumber ORDER BY SCD DESC
	                    ) as x1
                        WHERE 1 = 1" + filter;

            HCNS_NhanVien data = new HCNS_NhanVien();
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(connectionString))
                {
                    dbConnection.Open();
                    data = (await dbConnection.QueryAsync<HCNS_NhanVien>(query)).FirstOrDefault();
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
