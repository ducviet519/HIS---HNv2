using System.App.Entities.HCNS;
using System.Collections.Generic;
using System.Data;

namespace System.App.Services.Interfaces
{
    public interface ISKNV
    {
        IEnumerable<SKNV> List(SKNV sknv = null);
        IEnumerable<PLSK> List_PLSK(PLSK plsk = null);

        PLSK_Tron List_PLSK_Tron(PLSK_Tron obj);

        IEnumerable<TDSK> List_TDSK(TDSK tdsk = null);
        float TyLe(SKNV sknv = null);
        Dictionary<string, string> Departments();

        IEnumerable<BieuDo_PLSK> BieuDo_PLSK(string nam = "2019", int times = 1);

        IEnumerable<NCBT> List_NCBT(NCBT obj);
        bool Upload_Excel(DataTable data);
    }
}