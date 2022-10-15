using System;
using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.App.Services.Interfaces
{

    public interface IDaoTaoChungChi_Services
    {
        Task<IEnumerable<DaoTaoChungChi>> GetAllDaoTaoListAsync();

        Task<IEnumerable<DaoTaoChungChi>> SearchDaoTaoAsync(SearchDaoTao search);

        Task<DaoTaoChungChi> GetDaoTaoByIDAsync(int id);

        Task<string> AddDaoTaoAsync(DaoTaoChungChi model);

        Task<string> EditDaoTaoAsync(DaoTaoChungChi model);

        Task<HCNS_NhanVien> TimThongTinNhanVienAsync(HCNS_NhanVien nv);
    }
}
