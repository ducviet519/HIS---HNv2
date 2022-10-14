using System.App.Entities;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories;
using System.App.Repositories.HCNS;
using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace System.App.Services.HCNS
{
    public interface ZK_Interface
    {
        IEnumerable<ZK_Machine> DS_ThietBi();
        IEnumerable<ZK_Person_Finger> DS_VanTay(ArrayList arrObj);
        IEnumerable<ZK_Person> DS_User();
        IEnumerable<ZK_Person> DS_UserNghiViec();
        bool CapNhat_Template(List<ZK_Person_Finger> lsObj);
    }

    public class ZK_Service : ZK_Interface
    {
        private readonly ZK_Repo _ZK = null;
        private readonly Logs_Repo _LOG = null;

        public ZK_Service()
        {
            _ZK = new ZK_Repo();
            _LOG = new Logs_Repo();
        }

        public IEnumerable<ZK_Machine> DS_ThietBi()
        {
            try
            {
                return _ZK.DS_ThietBi(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception ex)
            {
                _LOG.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "ZK",
                    Action = "DS_ThietBi",
                    Controller = "HCNS.ZK",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    IP = HttpContext.Current.Request.UserHostAddress
                });
                return null;
            }
        }
        public IEnumerable<ZK_Person> DS_User()
        {
            try
            {
                var abc = _ZK.DS_User(StaticParams.connectionStringWiseEyeWebOn);
                return abc;
            }
            catch (Exception ex)
            {
                _LOG.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "ZK",
                    Action = "DS_User",
                    Controller = "HCNS.ZK",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    IP = HttpContext.Current.Request.UserHostAddress
                });
                return null;
            }
        }
        public IEnumerable<ZK_Person> DS_UserNghiViec()
        {
            try
            {
                return _ZK.DS_UserNghiViec(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception ex)
            {
                _LOG.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "ZK",
                    Action = "DS_User",
                    Controller = "HCNS.ZK",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    IP = HttpContext.Current.Request.UserHostAddress
                });
                return null;
            }
        }

        public IEnumerable<ZK_Person_Finger> DS_VanTay(ArrayList arrObj)
        {
            try
            {
                return _ZK.DS_VanTay(StaticParams.connectionStringWiseEyeWebOn, arrObj);
            }
            catch (Exception ex)
            {
                _LOG.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "ZK",
                    Action = "DS_User",
                    Controller = "HCNS.ZK",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    IP = HttpContext.Current.Request.UserHostAddress
                });
                return null;
            }
        }

        public bool CapNhat_Template(List<ZK_Person_Finger> lsObj)
        {
            try
            {
                return _ZK.CapNhat_Template(StaticParams.connectionStringWiseEyeWebOn, lsObj);
            }
            catch (Exception ex)
            {
                _LOG.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "ZK",
                    Action = "DS_User",
                    Controller = "HCNS.ZK",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = HttpContext.Current.User.Identity.Name,
                    IP = HttpContext.Current.Request.UserHostAddress
                });
                return false;
            }
        }
    }
}