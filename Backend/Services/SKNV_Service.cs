using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories;
using System.App.Repositories.HCNS;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace System.App.Services
{
    public class SKNV_Service : ISKNV
    {
        private readonly SKNV_Repo sknvRepos;
        private readonly Department_Repo departmentRepos;

        public SKNV_Service()
        {
            sknvRepos = new SKNV_Repo();
            departmentRepos = new Department_Repo();
        }

        public IEnumerable<SKNV> List(SKNV sknv = null)
        {
            return sknvRepos.List(StaticParams.connectionStringWiseEyeWebOn, sknv);
        }

        public Dictionary<string, string> Departments()
        {
            Dictionary<string, string> listDepartment = new Dictionary<string, string>();

            var models = departmentRepos.ListDepartment(StaticParams.connectionStringWiseEyeWebOn);

            foreach (var o in models)
            {
                listDepartment.Add(o.KhoaP, o.KhoaP);
            }

            return listDepartment;
        }

        public IEnumerable<PLSK> List_PLSK(PLSK plsk = null)
        {
            return sknvRepos.List_PLSK(StaticParams.connectionStringWiseEyeWebOn, plsk);
        }
        public PLSK_Tron List_PLSK_Tron(PLSK_Tron obj)
        {
            return sknvRepos.List_PLSK_Tron(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public IEnumerable<TDSK> List_TDSK(TDSK tdsk = null)
        {
            return sknvRepos.List_TDSK(StaticParams.connectionStringWiseEyeWebOn, tdsk);
        }

        public float TyLe(SKNV sknv = null)
        {
            return sknvRepos.TyLe(StaticParams.connectionStringWiseEyeWebOn, sknv);
        }

        public IEnumerable<BieuDo_PLSK> BieuDo_PLSK(string nam = "2018", int times = 1)
        {
            return sknvRepos.BieuDo_PLSK(StaticParams.connectionStringWiseEyeWebOn, nam, times);
        }

        public IEnumerable<NCBT> List_NCBT(NCBT obj)
        {
            if (!String.IsNullOrEmpty(obj.KhoaP))
            {
                return sknvRepos.List_NCBT_KP(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            return sknvRepos.List_NCBT_BV(StaticParams.connectionStringWiseEyeWebOn, obj);
        }

        public bool Upload_Excel(DataTable data)
        {
            try
            {
                sknvRepos.UPLOAD_EXCEL_THSK(StaticParams.connectionStringWiseEyeWebOn, data);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}