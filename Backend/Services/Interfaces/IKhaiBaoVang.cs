using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;

namespace System.App.Services.Interfaces
{
    public interface IKhaiBaoVang
    {
        IEnumerable<Absent> Ds_LichKhaiBao_View(Absent obj = null);
        IEnumerable<Absent> Ds_LichKhaiBao_Edit(Absent obj = null);
        IEnumerable<Absent> Ds_LichKhaiBao_All(Absent obj = null);
        IEnumerable<Absent> Ds_LichKhaiBao_DieuDuong(Absent obj);
        IEnumerable<HCNS_NhanVien> SearchUsersAll(string prefix);
        IEnumerable<HCNS_NhanVien> SearchUsers(string prefix, string kp);
        IEnumerable<HCNS_NhanVien> SearchUsersHC(string prefix, string kp);
        IEnumerable<HCNS_NhanVien> SearchUsers_DieuDuong(string prefix, string kp);
        Dictionary<string, string> DanhSachKhaiBao();
        bool ThemMoiKhaiBao(List<Absent> objs, Absent checkExist, ref string error);
        bool ThemMoiKhaiBao_Admin(List<Absent> objs, Absent checkExist, ref string error);
        bool XoaKhaiBao(Absent obj);
        Absent UserInfo(string user);
        bool KiemTraTrungLich(Absent obj);
        Absent AbsentInfo(Absent obj);
        DataTable Export_KhaiBaoVang(string kp, string tu, string den);
        Dictionary<string, string> DanhSachKhoaPhongHC_Relation(string kp = "");
        DataTable KhaiBaoVang_TongHop(string kp, string dateFrom, string dateTo);
    }
}