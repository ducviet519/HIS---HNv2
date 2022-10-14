using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories.HCNS;
using System.Collections.Generic;
using System.Web;

namespace System.App.Services.HCNS
{
    public interface IHCNS_CongVan
    {
        IEnumerable<HCNS_CongVan> DanhSachCongVan(HCNS_CongVan obj);
        HCNS_CongVan ThongTinCongVan(HCNS_CongVan obj);
        bool ThemMoiCongVan(HCNS_CongVan obj);
        bool CapNhatCongVan_Admin(HCNS_CongVan obj);
        bool CapNhatCongVan_User(HCNS_CongVan obj);
    }
    public class HCNS_CongVan_Service : IHCNS_CongVan
    {
        private readonly HCNS_CongVan_Repo _cvRepo = null;

        public HCNS_CongVan_Service()
        {
            _cvRepo = new HCNS_CongVan_Repo();
        }

        public IEnumerable<HCNS_CongVan> DanhSachCongVan(HCNS_CongVan obj)
        {
            try
            {
                return _cvRepo.DanhSachCongVan(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public HCNS_CongVan ThongTinCongVan(HCNS_CongVan obj)
        {
            try
            {
                return _cvRepo.ThongTinCongVan(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ThemMoiCongVan(HCNS_CongVan obj)
        {
            try
            {
                obj.NGUOI_TAO = HttpContext.Current.User.Identity.Name;
                obj.NGUOI_CAP_NHAT = HttpContext.Current.User.Identity.Name;

                return _cvRepo.ThemMoiCongVan(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CapNhatCongVan_User(HCNS_CongVan obj)
        {
            try
            {
                var old_data = ThongTinCongVan(obj);
                old_data.NGAY_NHAN = obj.NGAY_NHAN;
                old_data.THANG_NHAN = obj.THANG_NHAN;
                old_data.NGUOI_THUC_HIEN = obj.NGUOI_THUC_HIEN;
                old_data.NGAY_XL_DU_KIEN = obj.NGAY_XL_DU_KIEN;
                old_data.NGAY_XL_THUC_TE = obj.NGAY_XL_THUC_TE;
                old_data.TRANG_THAI = obj.TRANG_THAI;

                obj.NGUOI_CAP_NHAT = HttpContext.Current.User.Identity.Name;

                return _cvRepo.CapNhatCongVan(StaticParams.connectionStringWiseEyeWebOn, old_data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CapNhatCongVan_Admin(HCNS_CongVan obj)
        {
            try
            {
                obj.NGUOI_CAP_NHAT = HttpContext.Current.User.Identity.Name;

                return _cvRepo.CapNhatCongVan(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}