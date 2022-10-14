using System.App.Entities.HCNS;
using System.Collections.Generic;

namespace System.App.Services.Interfaces
{
    public interface IBCCC
    {
        Dictionary<int, string> DS_LoaiBangCap();
        IEnumerable<BangCap> DS_BangCap(BangCap obj);
        BangCap ThongTin_BangCap(int id);
        bool Add_BangCap(BangCap obj);
        bool Update_BangCap(BangCap obj);
        bool Delete_BangCap(BangCap obj);

        IEnumerable<ChungChi> DS_ChungChi(ChungChi obj);
        ChungChi ThongTin_ChungChi(int id);
        bool Add_ChungChi(ChungChi obj);
        bool Update_ChungChi(ChungChi obj);
        bool Delete_ChungChi(ChungChi obj);

        IEnumerable<ChungChiHanhNghe> DS_ChungChiHanhNghe(ChungChiHanhNghe obj);
        ChungChiHanhNghe ThongTin_ChungChiHanhNghe(int id);
        bool Update_ChungChiHanhNghe(ChungChiHanhNghe obj);
    }
}