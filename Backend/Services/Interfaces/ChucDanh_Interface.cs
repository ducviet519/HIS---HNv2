using System.App.Entities.HCNS;
using System.Collections.Generic;

namespace System.App.Services.Interfaces
{
    public interface IChucDanh_Service
    {
        IEnumerable<ChucDanh> DanhSach();
        ChucDanh ThongTin(int id);
        bool CapNhat(ChucDanh obj, ref string error);
    }
}
