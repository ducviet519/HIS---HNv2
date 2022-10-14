using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace System.App.Services.Interfaces
{
    public interface NhanVien_Interface
    {
        IEnumerable<HCNS_NhanVien> DanhSachNhanVien(HCNS_NhanVien nv = null);
        Dictionary<string, string> DanhSachKhoaPhong();
        Dictionary<string, string> DanhSachNoiAn();
        List<SelectListItem> DanhSachNoiAn_Index();
        Dictionary<string, string> DanhSachKhoaPhongHC();
        Dictionary<int, string> DoiTuongCC();
        HCNS_NhanVien TimThongTinNhanVien(HCNS_NhanVien nv);
        bool ThemNhanVienMoi(HCNS_NhanVien nv);
        bool ThemNhanVienMoiExcel(List<HCNS_NhanVien_Excel> lst, out string message);
        bool SuaThongTinNhanVienMoi(HCNS_NhanVien nv);
        bool SuaTTCapNhatNV(HCNS_NhanVien nv);
        bool XoaNhanVien(HCNS_NhanVien nv);
        IEnumerable<HCNS_NhanVien> DS_NhanVienBoSung(HCNS_NhanVien nv = null);
        IEnumerable<HCNS_NhanVien> DS_NhanVienNghiViec(HCNS_NhanVien nv = null);
        bool SuaTTNghiViec(HCNS_NhanVien nv);
        bool XoaTTNghiViec(int userEnrollNumber);
        Dictionary<int, string> DS_NoiSinh();
        Dictionary<int, string> DS_NoiCapCMT();
        Dictionary<int, string> DS_DoiTuong();
        Dictionary<int, string> DS_QuocGia();
        Dictionary<int, string> DS_ThanhPho();
        Dictionary<int, string> DS_QuanHuyen(int thanhpho);
        Dictionary<int, string> DS_PhuongXa(int quanhuyen);
        Dictionary<int, string> DS_TonGiao();
        Dictionary<int, string> DS_DanToc();

        bool Upload_AnhDaiDien(int id, string path);
        IEnumerable<DanhBaDienThoai> Ds_DanhBaDienThoai();
        IEnumerable<DanhBaDienThoai> DS_DanhBa_Search(HCNS_NhanVien objsearch);
        IEnumerable<DanhBaDienThoai_Excel> DS_DanhBa_Excel(HCNS_NhanVien objsearch);
        List<SelectListItem> DSKhoaPhong(int selected = -1);
        List<SelectListItem> DSToaNha(int selected = -1);
        List<SelectListItem> DSTang(int selected = -1);
        bool AddNew_DanhBa(HCNS_NhanVien obj, ref string errorMessage);
        bool Edit_DanhBaDienThoai(HCNS_NhanVien obj, ref string errorMessage);
        bool Delete_DanhBaDienThoai(HCNS_NhanVien obj, ref string errorMessage);
        HCNS_NhanVien Get_DanhBa(HCNS_NhanVien objsearch);
        string[] GetPermission();
        bool XacNhanNoiAn_Multi(string noian, string[] userenrollnumber, ref string errorMessage);
        Dictionary<string, string> LoaiNhanVien();
        IEnumerable<HCNS_NhanVien> DanhSachTaiKhoan(HCNS_NhanVien nv = null);
        bool CapNhatTaiKhoan(string user, string taikhoan, ref string errorMessage);
        bool CapNhatEmail(string user, string email, ref string errorMessage);
        bool CapNhatMatKhau(string user, string pw, ref string errorMessage);
        bool SalaryChangeExcel(List<HCNS_NhanVien_Excel> lst, out string message);
        bool NgayPCExcel(List<HCNS_NhanVien_Excel> lst, out string message);
        string ImportFile(DataTable dt, string ID, out string message);
        List<SelectListItem> DS_FileExcel(string type);
        IEnumerable<HCNS_NhanVien> DS_TheoDoiThaiSan(HCNS_NhanVien objsearch);
        bool AddNew_TheoDoiThaiSan(HCNS_NhanVien obj, ref string errorMessage);
        bool Edit_TheoDoiThaiSan(HCNS_NhanVien obj, ref string errorMessage);
        bool Delete_TheoDoiThaiSan(HCNS_NhanVien obj, ref string errorMessage);
        HCNS_NhanVien Get_TheoDoiThaiSan(HCNS_NhanVien objsearch);
        DataTable TheoDoiThaiSan_Excel(HCNS_NhanVien obj);
        IEnumerable<DanhBaDienThoai> DS_Upload(string type);
        DataTable Export_Excel(string loaifile, string tungay, string denngay, string khoaphong, string manv);
    }
}