using System.App.Entities.HCNS;
using System.Collections.Generic;

namespace System.App.Services.Interfaces
{
    public interface HDLD_Interface
    {
        Dictionary<int, string> DS_LoaiHopDong();
        IEnumerable<HDLD> DanhSachHopDong(HDLD obj);
        HDLD ThongTinHDLD(HDLD obj);
        bool ThemMoiHopDong(HDLD obj);
        bool CapNhatHopDong(HDLD obj);
        bool KiemTraTrung(HDLD obj, out string message);
        bool HuyHopDong(int id);
        bool CapNhatExcel(List<HDLD> lstObj);
        bool UpdateNgay(int id, string ngay, string type);
        HDLD HopDongInfo(HDLD obj);
        IEnumerable<HDLD> DanhSachHopDongDaKy(int userid);
        string CapSoHopDong(string prefix, int userid, int mahd, string ngayky);
    }
}