using System;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace System.App.Services
{
    public class ChucDanh_Service : IChucDanh_Service
    {
        private readonly ChucDanh_Repo chucDanhRepos;

        public ChucDanh_Service()
        {
            chucDanhRepos = new ChucDanh_Repo();
        }
        public IEnumerable<ChucDanh> DanhSach()
        {
            try
            {
                return chucDanhRepos.DanhSach(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ChucDanh ThongTin(int id)
        {
            try
            {
                return chucDanhRepos.ThongTin(StaticParams.connectionStringWiseEyeWebOn, id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool CapNhat(ChucDanh obj, ref string error)
        {
            try
            {
                return chucDanhRepos.CapNhat(StaticParams.connectionStringWiseEyeWebOn, obj) > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return false;
        }
    }
}
