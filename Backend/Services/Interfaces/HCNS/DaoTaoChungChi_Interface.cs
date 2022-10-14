using System;
using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.App.Services.Interfaces
{
    public interface DaoTaoChungChi_Interface
    {
        Task<List<DaoTaoChungChi>> GetAllDaoTaoListAsync();

        Task<List<DaoTaoChungChi>> SearchDaoTaoAsync(SearchDaoTao search);

        Task<DaoTaoChungChi> GetDaoTaoByIDAsync(int id);

        Task<string> AddDaoTaoAsync(DaoTaoChungChi model);

        Task<string> EditDaoTaoAsync(DaoTaoChungChi model);


    }
}
