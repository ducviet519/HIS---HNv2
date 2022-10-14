using System.App.Entities;
using System.App.Entities.Common;
using System.App.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.App.Services
{
    public interface IDanhMuc_Service
    {
        string[] GetPermission();
        IEnumerable<DanhMuc_LoaiVang> DS_LoaiVang_Search(DanhMuc_LoaiVang_Search objsearch);
        bool AddNew_LoaiVang(DanhMuc_LoaiVang_Search obj, ref string errorMessage);
        bool Delete_LoaiVang(DanhMuc_LoaiVang_Search obj, ref string errorMessage);
        DanhMuc_LoaiVang Get_LoaiVang(DanhMuc_LoaiVang_Search obj);
        bool Edit_LoaiVang(DanhMuc_LoaiVang_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_NganHang> DS_NganHang_Search(DanhMuc_NganHang_Search objsearch);
        bool Delete_NganHang(DanhMuc_NganHang_Search obj, ref string errorMessage);
        bool AddNew_NganHang(DanhMuc_NganHang_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_HopDong> DS_HopDong_Search(DanhMuc_HopDong_Search objsearch);
        bool Delete_HopDong(DanhMuc_HopDong_Search obj, ref string errorMessage);
        bool AddNew_HopDong(DanhMuc_HopDong_Search obj, ref string errorMessage);
        DanhMuc_HopDong Get_HopDong(DanhMuc_HopDong_Search obj);
        bool Edit_HopDong(DanhMuc_HopDong_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_KhoaPhong> DS_KhoaPhong_Search(DanhMuc_KhoaPhong_Search objsearch);
        bool Delete_KhoaPhong(DanhMuc_KhoaPhong_Search obj, ref string errorMessage);
        bool AddNew_KhoaPhong(DanhMuc_KhoaPhong_Search obj, ref string errorMessage);
        DanhMuc_KhoaPhong Get_KhoaPhong(DanhMuc_KhoaPhong_Search obj);
        bool Edit_KhoaPhong(DanhMuc_KhoaPhong_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_ThanhPho> DS_ThanhPho_Search(DanhMuc_ThanhPho_Search objsearch);
        bool AddNew_ThanhPho(DanhMuc_ThanhPho_Search obj, ref string errorMessage);
        bool Delete_ThanhPho(DanhMuc_ThanhPho_Search obj, ref string errorMessage);
        DanhMuc_ThanhPho Get_ThanhPho(DanhMuc_ThanhPho_Search obj);
        bool Edit_ThanhPho(DanhMuc_ThanhPho_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_QuanHuyen> DS_QuanHuyen_Search(DanhMuc_QuanHuyen_Search objsearch);
        bool AddNew_QuanHuyen(DanhMuc_QuanHuyen_Search obj, ref string errorMessage);
        bool Delete_QuanHuyen(DanhMuc_QuanHuyen_Search obj, ref string errorMessage);
        DanhMuc_QuanHuyen Get_QuanHuyen(DanhMuc_QuanHuyen_Search obj);
        bool Edit_QuanHuyen(DanhMuc_QuanHuyen_Search obj, ref string errorMessage);
        List<SelectListItem> DS_ThanhPho(string selected = "-1");
        IEnumerable<DanhMuc_BenhVien> DS_BenhVien_Search(DanhMuc_BenhVien_Search objsearch);
        bool Delete_BenhVien(DanhMuc_BenhVien_Search obj, ref string errorMessage);
        bool AddNew_BenhVien(DanhMuc_BenhVien_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_QuocGia> DS_QuocGia_Search(DanhMuc_QuocGia_Search objsearch);
        bool Delete_QuocGia(DanhMuc_QuocGia_Search obj, ref string errorMessage);
        bool AddNew_QuocGia(DanhMuc_QuocGia_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_NoiCapCMT> DS_NoiCapCMT_Search(DanhMuc_NoiCapCMT_Search objsearch);
        bool Delete_NoiCapCMT(DanhMuc_NoiCapCMT_Search obj, ref string errorMessage);
        bool AddNew_NoiCapCMT(DanhMuc_NoiCapCMT_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_DanToc> DS_DanToc_Search(DanhMuc_DanToc_Search objsearch);
        bool Delete_DanToc(DanhMuc_DanToc_Search obj, ref string errorMessage);
        bool AddNew_DanToc(DanhMuc_DanToc_Search obj, ref string errorMessage);
        DanhMuc_DanToc Get_DanToc(DanhMuc_DanToc_Search obj);
        bool Edit_DanToc(DanhMuc_DanToc_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_TonGiao> DS_TonGiao_Search(DanhMuc_TonGiao_Search objsearch);
        bool Delete_TonGiao(DanhMuc_TonGiao_Search obj, ref string errorMessage);
        bool AddNew_TonGiao(DanhMuc_TonGiao_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_PhongHop> DS_PhongHop_Search(DanhMuc_PhongHop_Search objsearch);
        bool AddNew_PhongHop(DanhMuc_PhongHop_Search obj, ref string errorMessage);
        bool Delete_PhongHop(DanhMuc_PhongHop_Search obj, ref string errorMessage);
        DanhMuc_PhongHop Get_PhongHop(DanhMuc_PhongHop_Search obj);
        bool Edit_PhongHop(DanhMuc_PhongHop_Search obj, ref string errorMessage);
        bool UnDelete_PhongHop(DanhMuc_PhongHop_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_KhoaPhongCC> DS_KhoaPhongCC_Search(DanhMuc_KhoaPhongCC_Search objsearch);
        bool AddNew_KhoaPhongCC(DanhMuc_KhoaPhongCC_Search obj, ref string errorMessage);
        bool Delete_KhoaPhongCC(DanhMuc_KhoaPhongCC_Search obj, ref string errorMessage);
        DanhMuc_KhoaPhongCC Get_KhoaPhongCC(DanhMuc_KhoaPhongCC_Search obj);
        bool Edit_KhoaPhongCC(DanhMuc_KhoaPhongCC_Search obj, ref string errorMessage);
        List<SelectListItem> DS_QuanHe(string selected = "-1");
        IEnumerable<DanhMuc_PhuongXa> DS_PhuongXa_Search(DanhMuc_PhuongXa_Search objsearch);
        bool AddNew_PhuongXa(DanhMuc_PhuongXa_Search obj, ref string errorMessage);
        bool Delete_PhuongXa(DanhMuc_PhuongXa_Search obj, ref string errorMessage);
        DanhMuc_PhuongXa Get_PhuongXa(DanhMuc_PhuongXa_Search obj);
        bool Edit_PhuongXa(DanhMuc_PhuongXa_Search obj, ref string errorMessage);
        List<SelectListItem> DS_QuanHuyen(string selected = "-1");
        IEnumerable<DanhMuc_Tang> DS_Tang_Search(DanhMuc_Tang_Search objsearch);
        bool Delete_Tang(DanhMuc_Tang_Search obj, ref string errorMessage);
        bool AddNew_Tang(DanhMuc_Tang_Search obj, ref string errorMessage);
        List<SelectListItem> DS_Tang(string selected = "-1");
        IEnumerable<DanhMuc_CaLamViec> DS_CaLamViec_Search(DanhMuc_CaLamViec_Search objsearch);
        bool AddNew_CaLamViec(DanhMuc_CaLamViec_Search obj, ref string errorMessage);
        bool Delete_CaLamViec(DanhMuc_CaLamViec_Search obj, ref string errorMessage);
        DanhMuc_CaLamViec Get_CaLamViec(DanhMuc_CaLamViec_Search obj);
        bool Edit_CaLamViec(DanhMuc_CaLamViec_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_LichLamViec> DS_LichLamViec_Search(DanhMuc_LichLamViec_Search objsearch);
        bool AddNew_LichLamViec(DanhMuc_LichLamViec_Search obj, ref string errorMessage);
        bool Delete_LichLamViec(DanhMuc_LichLamViec_Search obj, ref string errorMessage);
        DanhMuc_LichLamViec Get_LichLamViec(DanhMuc_LichLamViec_Search obj);
        bool Edit_LichLamViec(DanhMuc_LichLamViec_Search obj, ref string errorMessage);
        List<SelectListItem> DS_InOutID(string selected = "-1");
        IEnumerable<DanhMuc_LichTrinh> DS_LichTrinh_Search(DanhMuc_LichTrinh_Search objsearch);
        bool AddNew_LichTrinh(DanhMuc_LichTrinh_Search obj, ref string errorMessage);
        bool Delete_LichTrinh(DanhMuc_LichTrinh_Search obj, ref string errorMessage);
        DanhMuc_LichTrinh Get_LichTrinh(DanhMuc_LichTrinh_Search obj);
        bool Edit_LichTrinh(DanhMuc_LichTrinh_Search obj, ref string errorMessage);
        List<SelectListItem> DS_Schedule(string selected = "-1");
        List<SelectListItem> DS_Shift(string selected = "-1");
        IEnumerable<DanhMuc_NoiAn> DS_NoiAn_Search(DanhMuc_NoiAn_Search objsearch);
        bool Delete_NoiAn(DanhMuc_NoiAn_Search obj, ref string errorMessage);
        bool AddNew_NoiAn(DanhMuc_NoiAn_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_NgayNghi> DS_NgayNghi_Search(DanhMuc_NgayNghi_Search objsearch);
        bool Delete_NgayNghi(DanhMuc_NgayNghi_Search obj, ref string errorMessage);
        bool AddNew_NgayNghi(DanhMuc_NgayNghi_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_LoaiOT> DS_LoaiOT_Search(DanhMuc_LoaiOT_Search objsearch);
        bool Delete_LoaiOT(DanhMuc_LoaiOT_Search obj, ref string errorMessage);
        bool AddNew_LoaiOT(DanhMuc_LoaiOT_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_KyNang> DS_KyNang_Search(DanhMuc_KyNang_Search objsearch);
        bool Delete_KyNang(DanhMuc_KyNang_Search obj, ref string errorMessage);
        bool AddNew_KyNang(DanhMuc_KyNang_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_LoaiYeuCauSuaCong> DS_LoaiYeuCauSuaCong_Search(DanhMuc_LoaiYeuCauSuaCong_Search objsearch);
        bool Delete_LoaiYeuCauSuaCong(DanhMuc_LoaiYeuCauSuaCong_Search obj, ref string errorMessage);
        bool AddNew_LoaiYeuCauSuaCong(DanhMuc_LoaiYeuCauSuaCong_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_DoiTuongCC> DS_DoiTuongCC_Search(DanhMuc_DoiTuongCC_Search objsearch);
        bool AddNew_DoiTuongCC(DanhMuc_DoiTuongCC_Search obj, ref string errorMessage);
        bool Delete_DoiTuongCC(DanhMuc_DoiTuongCC_Search obj, ref string errorMessage);
        DanhMuc_DoiTuongCC Get_DoiTuongCC(DanhMuc_DoiTuongCC_Search obj);
        bool Edit_DoiTuongCC(DanhMuc_DoiTuongCC_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_LoaiLamThem> DS_LoaiLamThem_Search(DanhMuc_LoaiLamThem_Search objsearch);
        bool AddNew_LoaiLamThem(DanhMuc_LoaiLamThem_Search obj, ref string errorMessage);
        bool Delete_LoaiLamThem(DanhMuc_LoaiLamThem_Search obj, ref string errorMessage);
        DanhMuc_LoaiLamThem Get_LoaiLamThem(DanhMuc_LoaiLamThem_Search obj);
        bool Edit_LoaiLamThem(DanhMuc_LoaiLamThem_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_LoaiNhanVien> DS_LoaiNhanVien_Search(DanhMuc_LoaiNhanVien_Search objsearch);
        bool AddNew_LoaiNhanVien(DanhMuc_LoaiNhanVien_Search obj, ref string errorMessage);
        bool Delete_LoaiNhanVien(DanhMuc_LoaiNhanVien_Search obj, ref string errorMessage);
        DanhMuc_LoaiNhanVien Get_LoaiNhanVien(DanhMuc_LoaiNhanVien_Search obj);
        bool Edit_LoaiNhanVien(DanhMuc_LoaiNhanVien_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_LoaiLoiCC> DS_LoaiLoiCC_Search(DanhMuc_LoaiLoiCC_Search objsearch);
        bool AddNew_LoaiLoiCC(DanhMuc_LoaiLoiCC_Search obj, ref string errorMessage);
        bool Delete_LoaiLoiCC(DanhMuc_LoaiLoiCC_Search obj, ref string errorMessage);
        DanhMuc_LoaiLoiCC Get_LoaiLoiCC(DanhMuc_LoaiLoiCC_Search obj);
        bool Edit_LoaiLoiCC(DanhMuc_LoaiLoiCC_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_BangCap> DS_BangCap_Search(DanhMuc_BangCap_Search objsearch);
        bool Delete_BangCap(DanhMuc_BangCap_Search obj, ref string errorMessage);
        bool AddNew_BangCap(DanhMuc_BangCap_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_ThietBiPhongHop> DS_ThietBiPhongHop_Search(DanhMuc_ThietBiPhongHop_Search objsearch);
        bool Delete_ThietBiPhongHop(DanhMuc_ThietBiPhongHop_Search obj, ref string errorMessage);
        bool AddNew_ThietBiPhongHop(DanhMuc_ThietBiPhongHop_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_TTHonNhan> DS_TTHonNhan_Search(DanhMuc_TTHonNhan_Search objsearch);
        bool Delete_TTHonNhan(DanhMuc_TTHonNhan_Search obj, ref string errorMessage);
        bool AddNew_TTHonNhan(DanhMuc_TTHonNhan_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_ThietBi> DS_ThietBi_Search(DanhMuc_ThietBi_Search objsearch);
        bool AddNew_ThietBi(DanhMuc_ThietBi_Search obj, ref string errorMessage);
        bool Delete_ThietBi(DanhMuc_ThietBi_Search obj, ref string errorMessage);
        DanhMuc_ThietBi Get_ThietBi(DanhMuc_ThietBi_Search obj);
        bool Edit_ThietBi(DanhMuc_ThietBi_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_Quyen> DS_Quyen_Search(DanhMuc_Quyen_Search objsearch);
        bool AddNew_Quyen(DanhMuc_Quyen_Search obj, ref string errorMessage);
        bool Delete_Quyen(DanhMuc_Quyen_Search obj, ref string errorMessage);
        DanhMuc_Quyen Get_Quyen(DanhMuc_Quyen_Search obj);
        bool Edit_Quyen(DanhMuc_Quyen_Search obj, ref string errorMessage);
        IEnumerable<DS_DropDownList> DS_Quyen();
        IEnumerable<DanhMuc_Nhom> DS_Nhom_Search(DanhMuc_Nhom_Search objsearch);
        bool AddNew_Nhom(DanhMuc_Nhom_Search obj, ref string errorMessage);
        bool Delete_Nhom(DanhMuc_Nhom_Search obj, ref string errorMessage);
        DanhMuc_Nhom Get_Nhom(DanhMuc_Nhom_Search obj);
        bool Edit_Nhom(DanhMuc_Nhom_Search obj, ref string errorMessage);
        IEnumerable<DS_DropDownList> DS_Nhom();
        IEnumerable<DS_DropDownList> DS_Ca();
        IEnumerable<DanhMuc_Quyen_NguoiDung> DS_Quyen_NguoiDung_Search(DanhMuc_Quyen_NguoiDung_Search objsearch);
        bool AddNew_Quyen_NguoiDung(DanhMuc_Quyen_NguoiDung_Search obj, ref string errorMessage);
        bool Delete_Quyen_NguoiDung(DanhMuc_Quyen_NguoiDung_Search obj, ref string errorMessage);
        DanhMuc_Quyen_NguoiDung Get_Quyen_NguoiDung(DanhMuc_Quyen_NguoiDung_Search obj);
        bool Edit_Quyen_NguoiDung(DanhMuc_Quyen_NguoiDung_Search obj, ref string errorMessage);
        IEnumerable<DanhMuc_Quyen_KhoaPhong> DS_Quyen_KhoaPhong_Search(DanhMuc_Quyen_KhoaPhong_Search objsearch);
        bool AddNew_Quyen_KhoaPhong(DanhMuc_Quyen_KhoaPhong_Search obj, ref string errorMessage);
        bool Delete_Quyen_KhoaPhong(DanhMuc_Quyen_KhoaPhong_Search obj, ref string errorMessage);
        DanhMuc_Quyen_KhoaPhong Get_Quyen_KhoaPhong(DanhMuc_Quyen_KhoaPhong_Search obj);
        bool Edit_Quyen_KhoaPhong(DanhMuc_Quyen_KhoaPhong_Search obj, ref string errorMessage);
        List<SelectListItem> DS_Depts(string selected = "-1");
        List<SelectListItem> DSKhoaPhong();
        IEnumerable<QuyenChamCong> QuyenChamCong_table(QuyenChamCong objsearch);
        bool XoaQuyen(QuyenChamCong obj, ref string errorMessage);
        bool ThemQuyen(QuyenChamCong obj, ref string errorMessage);
        IEnumerable<DanhMuc_Config> DS_Config_Search(DanhMuc_Config_Search objsearch);
        bool AddNew_Config(DanhMuc_Config_Search obj, ref string errorMessage);
        bool Delete_Config(DanhMuc_Config_Search obj, ref string errorMessage);
        DanhMuc_Config Get_Config(DanhMuc_Config_Search obj);
        bool Edit_Config(DanhMuc_Config_Search obj, ref string errorMessage);
        List<SelectListItem> DS_LoaiCa(string selected = "-1");
        IEnumerable<DanhMuc_ViTriTuyenDung> DS_ViTriTuyenDung_Search(DanhMuc_ViTriTuyenDung_Search objsearch);
        bool Delete_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj, ref string errorMessage);
        bool UnDelete_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj, ref string errorMessage);
        bool AddNew_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj, ref string errorMessage);
        DanhMuc_ViTriTuyenDung Get_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj);
        bool Edit_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj, ref string errorMessage);
    }

    public class DanhMuc_Service : IDanhMuc_Service
    {
        private readonly DanhMuc_Repo danhmucRepo;
        private readonly Logs_Repo oLogRepo;
        private string ip = "";
        private string user = "";
        private string[] roles;

        public DanhMuc_Service()
        {
            danhmucRepo = new DanhMuc_Repo();
            oLogRepo = new Logs_Repo();
            ip = HttpContext.Current.Request.UserHostAddress;
            user = HttpContext.Current.User.Identity.Name;
            roles = (string[])HttpContext.Current.Items["Roles"];
        }

        public string[] GetPermission()
        {
            List<string> permission = new List<string>();

            if (string.IsNullOrEmpty(user))
            {
                return null;
            }
            else
            {
                if (roles.Contains(StaticParams.IVF_QLTN_Admin)) // Mức 2
                {
                    permission.Add(StaticParams.IVF_QLTN_Admin);
                }
                if (roles.Contains(StaticParams.IVF_QLTN)) // Mức 1
                {
                    permission.Add(StaticParams.IVF_QLTN);
                }
                if (roles.Contains(StaticParams.IVF_QLGT_Admin)) // Mức 2
                {
                    permission.Add(StaticParams.IVF_QLGT_Admin);
                }
                if (roles.Contains(StaticParams.IVF_QLGT)) // Mức 1
                {
                    permission.Add(StaticParams.IVF_QLGT);
                }
                if (roles.Contains(StaticParams.CaLamViec_Update)) // Mức 1
                {
                    permission.Add(StaticParams.CaLamViec_Update);
                }
            }

            return permission.ToArray();
        }
        public IEnumerable<DanhMuc_LoaiVang> DS_LoaiVang_Search(DanhMuc_LoaiVang_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_LoaiVang> lst = danhmucRepo.DS_LoaiVang_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_LoaiVang_Search",
                    Action = "DS_LoaiVang_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_LoaiVang(DanhMuc_LoaiVang_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_LoaiVang(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_LoaiVang",
                    Action = "AddNew_LoaiVang",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_LoaiVang(DanhMuc_LoaiVang_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_LoaiVang(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_LoaiVang",
                    Action = "Delete_LoaiVang",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_LoaiVang Get_LoaiVang(DanhMuc_LoaiVang_Search obj)
        {
            return danhmucRepo.Get_LoaiVang(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_LoaiVang(DanhMuc_LoaiVang_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_LoaiVang(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_LoaiVang",
                    Action = "Edit_LoaiVang",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_NganHang> DS_NganHang_Search(DanhMuc_NganHang_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_NganHang> lst = danhmucRepo.DS_NganHang_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_NganHang_Search",
                    Action = "DS_NganHang_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_NganHang(DanhMuc_NganHang_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_NganHang(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_NganHang",
                    Action = "Delete_NganHang",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_NganHang(DanhMuc_NganHang_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_NganHang(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_NganHang",
                    Action = "AddNew_NganHang",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_HopDong> DS_HopDong_Search(DanhMuc_HopDong_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_HopDong> lst = danhmucRepo.DS_HopDong_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_HopDong_Search",
                    Action = "DS_HopDong_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_HopDong(DanhMuc_HopDong_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_HopDong(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_HopDong",
                    Action = "Delete_HopDong",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_HopDong(DanhMuc_HopDong_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_HopDong(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_HopDong",
                    Action = "AddNew_HopDong",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_HopDong Get_HopDong(DanhMuc_HopDong_Search obj)
        {
            return danhmucRepo.Get_HopDong(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_HopDong(DanhMuc_HopDong_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_HopDong(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_HopDong",
                    Action = "Edit_HopDong",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_KhoaPhong> DS_KhoaPhong_Search(DanhMuc_KhoaPhong_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_KhoaPhong> lst = danhmucRepo.DS_KhoaPhong_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_KhoaPhong_Search",
                    Action = "DS_KhoaPhong_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_KhoaPhong(DanhMuc_KhoaPhong_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_KhoaPhong(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_KhoaPhong",
                    Action = "Delete_KhoaPhong",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_KhoaPhong(DanhMuc_KhoaPhong_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_KhoaPhong(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_KhoaPhong",
                    Action = "AddNew_KhoaPhong",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_KhoaPhong Get_KhoaPhong(DanhMuc_KhoaPhong_Search obj)
        {
            return danhmucRepo.Get_KhoaPhong(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_KhoaPhong(DanhMuc_KhoaPhong_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_KhoaPhong(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_KhoaPhong",
                    Action = "Edit_KhoaPhong",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_ThanhPho> DS_ThanhPho_Search(DanhMuc_ThanhPho_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_ThanhPho> lst = danhmucRepo.DS_ThanhPho_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_ThanhPho_Search",
                    Action = "DS_ThanhPho_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_ThanhPho(DanhMuc_ThanhPho_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_ThanhPho(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_ThanhPho",
                    Action = "AddNew_ThanhPho",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_ThanhPho(DanhMuc_ThanhPho_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_ThanhPho(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_ThanhPho",
                    Action = "Delete_ThanhPho",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_ThanhPho Get_ThanhPho(DanhMuc_ThanhPho_Search obj)
        {
            return danhmucRepo.Get_ThanhPho(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_ThanhPho(DanhMuc_ThanhPho_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_ThanhPho(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_ThanhPho",
                    Action = "Edit_ThanhPho",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_QuanHuyen> DS_QuanHuyen_Search(DanhMuc_QuanHuyen_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_QuanHuyen> lst = danhmucRepo.DS_QuanHuyen_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_QuanHuyen_Search",
                    Action = "DS_QuanHuyen_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_QuanHuyen(DanhMuc_QuanHuyen_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_QuanHuyen(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_QuanHuyen",
                    Action = "AddNew_QuanHuyen",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_QuanHuyen(DanhMuc_QuanHuyen_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_QuanHuyen(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_QuanHuyen",
                    Action = "Delete_QuanHuyen",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_QuanHuyen Get_QuanHuyen(DanhMuc_QuanHuyen_Search obj)
        {
            return danhmucRepo.Get_QuanHuyen(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_QuanHuyen(DanhMuc_QuanHuyen_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_QuanHuyen(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_QuanHuyen",
                    Action = "Edit_QuanHuyen",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public List<SelectListItem> DS_ThanhPho(string selected = "-1")
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = danhmucRepo.DS_ThanhPho(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = selected, Text = "--Chưa chọn--" });

            foreach (var item in depts)
            {
                if (item.ID == selected)
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa });
            }
            return items;
        }
        public IEnumerable<DanhMuc_BenhVien> DS_BenhVien_Search(DanhMuc_BenhVien_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_BenhVien> lst = danhmucRepo.DS_BenhVien_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_BenhVien_Search",
                    Action = "DS_BenhVien_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_BenhVien(DanhMuc_BenhVien_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_BenhVien(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_BenhVien",
                    Action = "Delete_BenhVien",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_BenhVien(DanhMuc_BenhVien_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_BenhVien(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_BenhVien",
                    Action = "AddNew_BenhVien",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_QuocGia> DS_QuocGia_Search(DanhMuc_QuocGia_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_QuocGia> lst = danhmucRepo.DS_QuocGia_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_QuocGia_Search",
                    Action = "DS_QuocGia_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_QuocGia(DanhMuc_QuocGia_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_QuocGia(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_QuocGia",
                    Action = "Delete_QuocGia",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_QuocGia(DanhMuc_QuocGia_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_QuocGia(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_QuocGia",
                    Action = "AddNew_QuocGia",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_NoiCapCMT> DS_NoiCapCMT_Search(DanhMuc_NoiCapCMT_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_NoiCapCMT> lst = danhmucRepo.DS_NoiCapCMT_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_NoiCapCMT_Search",
                    Action = "DS_NoiCapCMT_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_NoiCapCMT(DanhMuc_NoiCapCMT_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_NoiCapCMT(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_NoiCapCMT",
                    Action = "Delete_NoiCapCMT",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_NoiCapCMT(DanhMuc_NoiCapCMT_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_NoiCapCMT(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_NoiCapCMT",
                    Action = "AddNew_NoiCapCMT",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_DanToc> DS_DanToc_Search(DanhMuc_DanToc_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_DanToc> lst = danhmucRepo.DS_DanToc_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_DanToc_Search",
                    Action = "DS_DanToc_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_DanToc(DanhMuc_DanToc_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_DanToc(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_DanToc",
                    Action = "Delete_DanToc",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_DanToc(DanhMuc_DanToc_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_DanToc(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_DanToc",
                    Action = "AddNew_DanToc",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_DanToc Get_DanToc(DanhMuc_DanToc_Search obj)
        {
            return danhmucRepo.Get_DanToc(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_DanToc(DanhMuc_DanToc_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_DanToc(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_DanToc",
                    Action = "Edit_DanToc",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_TonGiao> DS_TonGiao_Search(DanhMuc_TonGiao_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_TonGiao> lst = danhmucRepo.DS_TonGiao_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_TonGiao_Search",
                    Action = "DS_TonGiao_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_TonGiao(DanhMuc_TonGiao_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_TonGiao(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_TonGiao",
                    Action = "Delete_TonGiao",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_TonGiao(DanhMuc_TonGiao_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_TonGiao(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_TonGiao",
                    Action = "AddNew_TonGiao",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_PhongHop> DS_PhongHop_Search(DanhMuc_PhongHop_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_PhongHop> lst = danhmucRepo.DS_PhongHop_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_PhongHop_Search",
                    Action = "DS_PhongHop_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_PhongHop(DanhMuc_PhongHop_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_PhongHop(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_PhongHop",
                    Action = "AddNew_PhongHop",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_PhongHop(DanhMuc_PhongHop_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_PhongHop(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_PhongHop",
                    Action = "Delete_PhongHop",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_PhongHop Get_PhongHop(DanhMuc_PhongHop_Search obj)
        {
            return danhmucRepo.Get_PhongHop(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_PhongHop(DanhMuc_PhongHop_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_PhongHop(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_PhongHop",
                    Action = "Edit_PhongHop",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool UnDelete_PhongHop(DanhMuc_PhongHop_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.UnDelete_PhongHop(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_PhongHop",
                    Action = "Delete_PhongHop",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_KhoaPhongCC> DS_KhoaPhongCC_Search(DanhMuc_KhoaPhongCC_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_KhoaPhongCC> lst = danhmucRepo.DS_KhoaPhongCC_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_KhoaPhongCC_Search",
                    Action = "DS_KhoaPhongCC_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_KhoaPhongCC(DanhMuc_KhoaPhongCC_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_KhoaPhongCC(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_KhoaPhongCC",
                    Action = "AddNew_KhoaPhongCC",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_KhoaPhongCC(DanhMuc_KhoaPhongCC_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_KhoaPhongCC(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_KhoaPhongCC",
                    Action = "Delete_KhoaPhongCC",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_KhoaPhongCC Get_KhoaPhongCC(DanhMuc_KhoaPhongCC_Search obj)
        {
            return danhmucRepo.Get_KhoaPhongCC(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_KhoaPhongCC(DanhMuc_KhoaPhongCC_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_KhoaPhongCC(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_KhoaPhongCC",
                    Action = "Edit_KhoaPhongCC",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public List<SelectListItem> DS_QuanHe(string selected = "-1")
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = danhmucRepo.DS_QuanHe(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = selected, Text = "--Chưa chọn--" });

            foreach (var item in depts)
            {
                if (item.ID == selected)
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa });
            }
            return items;
        }
        public IEnumerable<DanhMuc_PhuongXa> DS_PhuongXa_Search(DanhMuc_PhuongXa_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_PhuongXa> lst = danhmucRepo.DS_PhuongXa_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_PhuongXa_Search",
                    Action = "DS_PhuongXa_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_PhuongXa(DanhMuc_PhuongXa_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_PhuongXa(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_PhuongXa",
                    Action = "AddNew_PhuongXa",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_PhuongXa(DanhMuc_PhuongXa_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_PhuongXa(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_PhuongXa",
                    Action = "Delete_PhuongXa",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_PhuongXa Get_PhuongXa(DanhMuc_PhuongXa_Search obj)
        {
            return danhmucRepo.Get_PhuongXa(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_PhuongXa(DanhMuc_PhuongXa_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_PhuongXa(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_PhuongXa",
                    Action = "Edit_PhuongXa",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public List<SelectListItem> DS_QuanHuyen(string selected = "-1")
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = danhmucRepo.DS_QuanHuyen(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = selected, Text = "--Chưa chọn--" });

            foreach (var item in depts)
            {
                if (item.ID == selected)
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa });
            }
            return items;
        }
        public IEnumerable<DanhMuc_Tang> DS_Tang_Search(DanhMuc_Tang_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_Tang> lst = danhmucRepo.DS_Tang_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_Tang_Search",
                    Action = "DS_Tang_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_Tang(DanhMuc_Tang_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_Tang(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_Tang",
                    Action = "Delete_Tang",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_Tang(DanhMuc_Tang_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_Tang(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_Tang",
                    Action = "AddNew_Tang",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public List<SelectListItem> DS_Tang(string selected = "-1")
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = danhmucRepo.DS_Tang(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = selected, Text = "--Chưa chọn--" });

            foreach (var item in depts)
            {
                items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa });
            }
            return items;
        }
        public IEnumerable<DanhMuc_CaLamViec> DS_CaLamViec_Search(DanhMuc_CaLamViec_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_CaLamViec> lst = danhmucRepo.DS_CaLamViec_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_CaLamViec_Search",
                    Action = "DS_CaLamViec_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_CaLamViec(DanhMuc_CaLamViec_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_CaLamViec(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_CaLamViec",
                    Action = "AddNew_CaLamViec",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_CaLamViec(DanhMuc_CaLamViec_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_CaLamViec(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_CaLamViec",
                    Action = "Delete_CaLamViec",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_CaLamViec Get_CaLamViec(DanhMuc_CaLamViec_Search obj)
        {
            return danhmucRepo.Get_CaLamViec(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_CaLamViec(DanhMuc_CaLamViec_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_CaLamViec(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_CaLamViec",
                    Action = "Edit_CaLamViec",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_LichLamViec> DS_LichLamViec_Search(DanhMuc_LichLamViec_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_LichLamViec> lst = danhmucRepo.DS_LichLamViec_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_LichLamViec_Search",
                    Action = "DS_LichLamViec_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_LichLamViec(DanhMuc_LichLamViec_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_LichLamViec(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_LichLamViec",
                    Action = "AddNew_LichLamViec",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_LichLamViec(DanhMuc_LichLamViec_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_LichLamViec(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_LichLamViec",
                    Action = "Delete_LichLamViec",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_LichLamViec Get_LichLamViec(DanhMuc_LichLamViec_Search obj)
        {
            return danhmucRepo.Get_LichLamViec(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_LichLamViec(DanhMuc_LichLamViec_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_LichLamViec(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_LichLamViec",
                    Action = "Edit_LichLamViec",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public List<SelectListItem> DS_InOutID(string selected = "-1")
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = danhmucRepo.DS_InOutID(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = selected, Text = "--Chưa chọn--" });

            foreach (var item in depts)
            {
                if (item.ID == selected)
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa });
            }
            return items;
        }
        public IEnumerable<DanhMuc_LichTrinh> DS_LichTrinh_Search(DanhMuc_LichTrinh_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_LichTrinh> lst = danhmucRepo.DS_LichTrinh_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_LichTrinh_Search",
                    Action = "DS_LichTrinh_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_LichTrinh(DanhMuc_LichTrinh_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_LichTrinh(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_LichTrinh",
                    Action = "AddNew_LichTrinh",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_LichTrinh(DanhMuc_LichTrinh_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_LichTrinh(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_LichTrinh",
                    Action = "Delete_LichTrinh",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_LichTrinh Get_LichTrinh(DanhMuc_LichTrinh_Search obj)
        {
            return danhmucRepo.Get_LichTrinh(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_LichTrinh(DanhMuc_LichTrinh_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_LichTrinh(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_LichTrinh",
                    Action = "Edit_LichTrinh",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public List<SelectListItem> DS_Schedule(string selected = "-1")
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = danhmucRepo.DS_Schedule(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = selected, Text = "--Chưa chọn--" });

            foreach (var item in depts)
            {
                if (item.ID == selected)
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa });
            }
            return items;
        }
        public List<SelectListItem> DS_Shift(string selected = "-1")
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = danhmucRepo.DS_Shift(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = selected, Text = "--Chưa chọn--" });

            foreach (var item in depts)
            {
                if (item.ID == selected)
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa });
            }
            return items;
        }
        public IEnumerable<DanhMuc_NoiAn> DS_NoiAn_Search(DanhMuc_NoiAn_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_NoiAn> lst = danhmucRepo.DS_NoiAn_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_NoiAn_Search",
                    Action = "DS_NoiAn_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_NoiAn(DanhMuc_NoiAn_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_NoiAn(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_NoiAn",
                    Action = "Delete_NoiAn",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_NoiAn(DanhMuc_NoiAn_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_NoiAn(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_NoiAn",
                    Action = "AddNew_NoiAn",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_NgayNghi> DS_NgayNghi_Search(DanhMuc_NgayNghi_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_NgayNghi> lst = danhmucRepo.DS_NgayNghi_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_NgayNghi_Search",
                    Action = "DS_NgayNghi_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_NgayNghi(DanhMuc_NgayNghi_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_NgayNghi(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_NgayNghi",
                    Action = "Delete_NgayNghi",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_NgayNghi(DanhMuc_NgayNghi_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_NgayNghi(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_NgayNghi",
                    Action = "AddNew_NgayNghi",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_LoaiOT> DS_LoaiOT_Search(DanhMuc_LoaiOT_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_LoaiOT> lst = danhmucRepo.DS_LoaiOT_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_LoaiOT_Search",
                    Action = "DS_LoaiOT_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_LoaiOT(DanhMuc_LoaiOT_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_LoaiOT(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_LoaiOT",
                    Action = "Delete_LoaiOT",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_LoaiOT(DanhMuc_LoaiOT_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_LoaiOT(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_LoaiOT",
                    Action = "AddNew_LoaiOT",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_KyNang> DS_KyNang_Search(DanhMuc_KyNang_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_KyNang> lst = danhmucRepo.DS_KyNang_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_KyNang_Search",
                    Action = "DS_KyNang_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_KyNang(DanhMuc_KyNang_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_KyNang(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_KyNang",
                    Action = "Delete_KyNang",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_KyNang(DanhMuc_KyNang_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_KyNang(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_KyNang",
                    Action = "AddNew_KyNang",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_LoaiYeuCauSuaCong> DS_LoaiYeuCauSuaCong_Search(DanhMuc_LoaiYeuCauSuaCong_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_LoaiYeuCauSuaCong> lst = danhmucRepo.DS_LoaiYeuCauSuaCong_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_LoaiYeuCauSuaCong_Search",
                    Action = "DS_LoaiYeuCauSuaCong_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_LoaiYeuCauSuaCong(DanhMuc_LoaiYeuCauSuaCong_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_LoaiYeuCauSuaCong(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_LoaiYeuCauSuaCong",
                    Action = "Delete_LoaiYeuCauSuaCong",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_LoaiYeuCauSuaCong(DanhMuc_LoaiYeuCauSuaCong_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_LoaiYeuCauSuaCong(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_LoaiYeuCauSuaCong",
                    Action = "AddNew_LoaiYeuCauSuaCong",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_DoiTuongCC> DS_DoiTuongCC_Search(DanhMuc_DoiTuongCC_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_DoiTuongCC> lst = danhmucRepo.DS_DoiTuongCC_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_DoiTuongCC_Search",
                    Action = "DS_DoiTuongCC_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_DoiTuongCC(DanhMuc_DoiTuongCC_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_DoiTuongCC(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_DoiTuongCC",
                    Action = "AddNew_DoiTuongCC",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_DoiTuongCC(DanhMuc_DoiTuongCC_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_DoiTuongCC(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_DoiTuongCC",
                    Action = "Delete_DoiTuongCC",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_DoiTuongCC Get_DoiTuongCC(DanhMuc_DoiTuongCC_Search obj)
        {
            return danhmucRepo.Get_DoiTuongCC(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_DoiTuongCC(DanhMuc_DoiTuongCC_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_DoiTuongCC(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_DoiTuongCC",
                    Action = "Edit_DoiTuongCC",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_LoaiLamThem> DS_LoaiLamThem_Search(DanhMuc_LoaiLamThem_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_LoaiLamThem> lst = danhmucRepo.DS_LoaiLamThem_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_LoaiLamThem_Search",
                    Action = "DS_LoaiLamThem_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_LoaiLamThem(DanhMuc_LoaiLamThem_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_LoaiLamThem(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_LoaiLamThem",
                    Action = "AddNew_LoaiLamThem",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_LoaiLamThem(DanhMuc_LoaiLamThem_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_LoaiLamThem(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_LoaiLamThem",
                    Action = "Delete_LoaiLamThem",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_LoaiLamThem Get_LoaiLamThem(DanhMuc_LoaiLamThem_Search obj)
        {
            return danhmucRepo.Get_LoaiLamThem(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_LoaiLamThem(DanhMuc_LoaiLamThem_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_LoaiLamThem(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_LoaiLamThem",
                    Action = "Edit_LoaiLamThem",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_LoaiNhanVien> DS_LoaiNhanVien_Search(DanhMuc_LoaiNhanVien_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_LoaiNhanVien> lst = danhmucRepo.DS_LoaiNhanVien_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_LoaiNhanVien_Search",
                    Action = "DS_LoaiNhanVien_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_LoaiNhanVien(DanhMuc_LoaiNhanVien_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_LoaiNhanVien(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_LoaiNhanVien",
                    Action = "AddNew_LoaiNhanVien",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_LoaiNhanVien(DanhMuc_LoaiNhanVien_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_LoaiNhanVien(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_LoaiNhanVien",
                    Action = "Delete_LoaiNhanVien",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_LoaiNhanVien Get_LoaiNhanVien(DanhMuc_LoaiNhanVien_Search obj)
        {
            return danhmucRepo.Get_LoaiNhanVien(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_LoaiNhanVien(DanhMuc_LoaiNhanVien_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_LoaiNhanVien(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_LoaiNhanVien",
                    Action = "Edit_LoaiNhanVien",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_LoaiLoiCC> DS_LoaiLoiCC_Search(DanhMuc_LoaiLoiCC_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_LoaiLoiCC> lst = danhmucRepo.DS_LoaiLoiCC_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_LoaiLoiCC_Search",
                    Action = "DS_LoaiLoiCC_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_LoaiLoiCC(DanhMuc_LoaiLoiCC_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_LoaiLoiCC(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_LoaiLoiCC",
                    Action = "AddNew_LoaiLoiCC",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_LoaiLoiCC(DanhMuc_LoaiLoiCC_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_LoaiLoiCC(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_LoaiLoiCC",
                    Action = "Delete_LoaiLoiCC",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_LoaiLoiCC Get_LoaiLoiCC(DanhMuc_LoaiLoiCC_Search obj)
        {
            return danhmucRepo.Get_LoaiLoiCC(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_LoaiLoiCC(DanhMuc_LoaiLoiCC_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_LoaiLoiCC(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_LoaiLoiCC",
                    Action = "Edit_LoaiLoiCC",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_BangCap> DS_BangCap_Search(DanhMuc_BangCap_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_BangCap> lst = danhmucRepo.DS_BangCap_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_BangCap_Search",
                    Action = "DS_BangCap_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_BangCap(DanhMuc_BangCap_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_BangCap(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_BangCap",
                    Action = "Delete_BangCap",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_BangCap(DanhMuc_BangCap_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_BangCap(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_BangCap",
                    Action = "AddNew_BangCap",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_ThietBiPhongHop> DS_ThietBiPhongHop_Search(DanhMuc_ThietBiPhongHop_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_ThietBiPhongHop> lst = danhmucRepo.DS_ThietBiPhongHop_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_ThietBiPhongHop_Search",
                    Action = "DS_ThietBiPhongHop_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_ThietBiPhongHop(DanhMuc_ThietBiPhongHop_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_ThietBiPhongHop(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_ThietBiPhongHop",
                    Action = "Delete_ThietBiPhongHop",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_ThietBiPhongHop(DanhMuc_ThietBiPhongHop_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_ThietBiPhongHop(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_ThietBiPhongHop",
                    Action = "AddNew_ThietBiPhongHop",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_TTHonNhan> DS_TTHonNhan_Search(DanhMuc_TTHonNhan_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_TTHonNhan> lst = danhmucRepo.DS_TTHonNhan_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_TTHonNhan_Search",
                    Action = "DS_TTHonNhan_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_TTHonNhan(DanhMuc_TTHonNhan_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_TTHonNhan(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_TTHonNhan",
                    Action = "Delete_TTHonNhan",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_TTHonNhan(DanhMuc_TTHonNhan_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_TTHonNhan(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_TTHonNhan",
                    Action = "AddNew_TTHonNhan",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_ThietBi> DS_ThietBi_Search(DanhMuc_ThietBi_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_ThietBi> lst = danhmucRepo.DS_ThietBi_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_ThietBi_Search",
                    Action = "DS_ThietBi_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_ThietBi(DanhMuc_ThietBi_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_ThietBi(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_ThietBi",
                    Action = "AddNew_ThietBi",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_ThietBi(DanhMuc_ThietBi_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_ThietBi(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_ThietBi",
                    Action = "Delete_ThietBi",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_ThietBi Get_ThietBi(DanhMuc_ThietBi_Search obj)
        {
            return danhmucRepo.Get_ThietBi(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_ThietBi(DanhMuc_ThietBi_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_ThietBi(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_ThietBi",
                    Action = "Edit_ThietBi",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_Quyen> DS_Quyen_Search(DanhMuc_Quyen_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_Quyen> lst = danhmucRepo.DS_Quyen_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_Quyen_Search",
                    Action = "DS_Quyen_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_Quyen(DanhMuc_Quyen_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_Quyen(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_Quyen",
                    Action = "AddNew_Quyen",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_Quyen(DanhMuc_Quyen_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_Quyen(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_Quyen",
                    Action = "Delete_Quyen",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_Quyen Get_Quyen(DanhMuc_Quyen_Search obj)
        {
            return danhmucRepo.Get_Quyen(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_Quyen(DanhMuc_Quyen_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_Quyen(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_Quyen",
                    Action = "Edit_Quyen",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }

        public IEnumerable<DS_DropDownList> DS_Quyen()
        {
            try
            {
                return danhmucRepo.DS_Quyen(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception ex)
            {
                oLogRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Table DS_Quyen",
                    Action = "DS_Quyen",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    IP = HttpContext.Current.Request.UserHostAddress
                });
                return null;
            }
        }
        public IEnumerable<DanhMuc_Nhom> DS_Nhom_Search(DanhMuc_Nhom_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_Nhom> lst = danhmucRepo.DS_Nhom_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_Nhom_Search",
                    Action = "DS_Nhom_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_Nhom(DanhMuc_Nhom_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_Nhom(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_Nhom",
                    Action = "AddNew_Nhom",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_Nhom(DanhMuc_Nhom_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_Nhom(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_Nhom",
                    Action = "Delete_Nhom",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_Nhom Get_Nhom(DanhMuc_Nhom_Search obj)
        {
            return danhmucRepo.Get_Nhom(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_Nhom(DanhMuc_Nhom_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_Nhom(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_Nhom",
                    Action = "Edit_Nhom",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DS_DropDownList> DS_Nhom()
        {
            try
            {
                return danhmucRepo.DS_Nhom(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception ex)
            {
                oLogRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Table DS_Nhom",
                    Action = "DS_Nhom",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    IP = HttpContext.Current.Request.UserHostAddress
                });
                return null;
            }
        }
        public IEnumerable<DS_DropDownList> DS_Ca()
        {
            try
            {
                return danhmucRepo.DS_Ca(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception ex)
            {
                oLogRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Table DS_Ca",
                    Action = "DS_Ca",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    IP = HttpContext.Current.Request.UserHostAddress
                });
                return null;
            }
        }
        public IEnumerable<DanhMuc_Quyen_NguoiDung> DS_Quyen_NguoiDung_Search(DanhMuc_Quyen_NguoiDung_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_Quyen_NguoiDung> lst = danhmucRepo.DS_Quyen_NguoiDung_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_Quyen_NguoiDung_Search",
                    Action = "DS_Quyen_NguoiDung_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_Quyen_NguoiDung(DanhMuc_Quyen_NguoiDung_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_Quyen_NguoiDung(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_Quyen_NguoiDung",
                    Action = "AddNew_Quyen_NguoiDung",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_Quyen_NguoiDung(DanhMuc_Quyen_NguoiDung_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_Quyen_NguoiDung(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_Quyen_NguoiDung",
                    Action = "Delete_Quyen_NguoiDung",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_Quyen_NguoiDung Get_Quyen_NguoiDung(DanhMuc_Quyen_NguoiDung_Search obj)
        {
            return danhmucRepo.Get_Quyen_NguoiDung(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_Quyen_NguoiDung(DanhMuc_Quyen_NguoiDung_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_Quyen_NguoiDung(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_Quyen_NguoiDung",
                    Action = "Edit_Quyen_NguoiDung",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public IEnumerable<DanhMuc_Quyen_KhoaPhong> DS_Quyen_KhoaPhong_Search(DanhMuc_Quyen_KhoaPhong_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_Quyen_KhoaPhong> lst = danhmucRepo.DS_Quyen_KhoaPhong_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_Quyen_KhoaPhong_Search",
                    Action = "DS_Quyen_KhoaPhong_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_Quyen_KhoaPhong(DanhMuc_Quyen_KhoaPhong_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_Quyen_KhoaPhong(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_Quyen_KhoaPhong",
                    Action = "AddNew_Quyen_KhoaPhong",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_Quyen_KhoaPhong(DanhMuc_Quyen_KhoaPhong_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_Quyen_KhoaPhong(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_Quyen_KhoaPhong",
                    Action = "Delete_Quyen_KhoaPhong",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_Quyen_KhoaPhong Get_Quyen_KhoaPhong(DanhMuc_Quyen_KhoaPhong_Search obj)
        {
            return danhmucRepo.Get_Quyen_KhoaPhong(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_Quyen_KhoaPhong(DanhMuc_Quyen_KhoaPhong_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_Quyen_KhoaPhong(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_Quyen_KhoaPhong",
                    Action = "Edit_Quyen_KhoaPhong",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public List<SelectListItem> DS_Depts(string selected = "-1")
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = danhmucRepo.DS_Depts(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = selected, Text = "--Chưa chọn--" });

            foreach (var item in depts)
            {
                if (item.ID == selected)
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.ID, Text = item.MoTa });
            }
            return items;
        }
        #region Quyền chấm công
        public IEnumerable<QuyenChamCong> QuyenChamCong_table(QuyenChamCong objsearch)
        {
            try
            {
                IEnumerable<QuyenChamCong> lst = danhmucRepo.QuyenChamCong_table(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: QuyenChamCong_table",
                    Action = "QuyenChamCong_table",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    IP = ip
                });
                return null;
            }
        }
        public bool XoaQuyen(QuyenChamCong obj, ref string errorMessage)
        {
            try
            {
                return danhmucRepo.XoaQuyen(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool ThemQuyen(QuyenChamCong obj, ref string errorMessage)
        {
            try
            {
                return danhmucRepo.ThemQuyen(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        public List<SelectListItem> DSKhoaPhong()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = danhmucRepo.DSKhoaPhong(StaticParams.connectionStringWiseEyeWebOn);

            foreach (var item in depts)
            {
                items.Add(new SelectListItem() { Value = item.STT.ToString(), Text = item.KhoaP });
            }
            return items;
        }
        #endregion
        public IEnumerable<DanhMuc_Config> DS_Config_Search(DanhMuc_Config_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_Config> lst = danhmucRepo.DS_Config_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_Config_Search",
                    Action = "DS_Config_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool AddNew_Config(DanhMuc_Config_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_Config(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_Config",
                    Action = "AddNew_Config",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_Config(DanhMuc_Config_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_Config(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_Config",
                    Action = "Delete_Config",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public DanhMuc_Config Get_Config(DanhMuc_Config_Search obj)
        {
            return danhmucRepo.Get_Config(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_Config(DanhMuc_Config_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_Config(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_Config",
                    Action = "Edit_Config",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public List<SelectListItem> DS_LoaiCa(string selected = "-1")
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = danhmucRepo.DS_LoaiCa(StaticParams.connectionStringWiseEyeWebOn);

            foreach (var item in depts)
            {
                if (item.ID.ToString() == selected)
                {
                    items.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.MoTa, Selected = true });
                }
                else
                {
                    items.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.MoTa });
                }                
            }
            return items;
        }
        public IEnumerable<DanhMuc_ViTriTuyenDung> DS_ViTriTuyenDung_Search(DanhMuc_ViTriTuyenDung_Search objsearch)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhMuc_ViTriTuyenDung> lst = danhmucRepo.DS_ViTriTuyenDung_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_ViTriTuyenDung_Search",
                    Action = "DS_ViTriTuyenDung_Search",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public bool Delete_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Delete_ViTriTuyenDung(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_ViTriTuyenDung",
                    Action = "Delete_ViTriTuyenDung",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool UnDelete_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.UnDelete_ViTriTuyenDung(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: UnDelete_ViTriTuyenDung",
                    Action = "UnDelete_ViTriTuyenDung",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool AddNew_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.AddNew_ViTriTuyenDung(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_ViTriTuyenDung",
                    Action = "AddNew_ViTriTuyenDung",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }        
        public DanhMuc_ViTriTuyenDung Get_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj)
        {
            return danhmucRepo.Get_ViTriTuyenDung(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool Edit_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj, ref string errorMessage)
        {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return danhmucRepo.Edit_ViTriTuyenDung(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                oLogRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_ViTriTuyenDung",
                    Action = "Edit_ViTriTuyenDung",
                    Controller = "DanhMuc_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
