using System.App.Entities;
using System.App.Entities.Common;
using System.App.Repositories;
using System.App.Services.Interfaces;
using System.Collections.Generic;

namespace System.App.Services
{
    public class KhoaPhong_Service : KhoaPhong_Interface
    {
        KhoaPhong_Repo khoaPhong_Repo;

        public KhoaPhong_Service()
        {
            khoaPhong_Repo = new KhoaPhong_Repo();
        }

        public IEnumerable<KhoaPhong> List()
        {
            return null;
            //return khoaPhong_Repo.List(StaticParams.connectionOracle);
        }
    }
}