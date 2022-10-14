using System.App.Entities;
using System.App.Entities.Common;
using System.App.Repositories;

namespace System.App.Services
{
    public interface IConfig
    {
        Config NgayQuyDinhKhaiBaoCong();
        bool KhoaDuLieu();
        Config NgayBatDauChuKyLuong();
        Config NgayKetThucChuKyLuong();
    }
    public class Config_Service : IConfig
    {
        private readonly Config_Repo configRepo;

        public Config_Service()
        {
            configRepo = new Config_Repo();
        }

        public bool KhoaDuLieu()
        {
            try
            {
                var dakhoa = configRepo.GetConfig(StaticParams.connectionStringWiseEyeWebOn, "T04").NumberVal;

                return dakhoa == 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Config NgayQuyDinhKhaiBaoCong()
        {
            try
            {
                return configRepo.GetConfig(StaticParams.connectionStringWiseEyeWebOn, "T03");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Config NgayBatDauChuKyLuong()
        {
            return configRepo.GetConfig(StaticParams.connectionStringWiseEyeWebOn, "T06");
        }

        public Config NgayKetThucChuKyLuong()
        {
            return configRepo.GetConfig(StaticParams.connectionStringWiseEyeWebOn, "T07");
        }
    }
}