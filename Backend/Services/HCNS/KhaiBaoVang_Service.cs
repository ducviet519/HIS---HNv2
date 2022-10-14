using System.App.Entities;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories;
using System.App.Repositories.HCNS;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace System.App.Services.HCNS
{
    public class KhaiBaoVang_Service : IKhaiBaoVang
    {
        private readonly KhaiBaoVang_Repo _khaiBaoVangRepo;
        private readonly Logs_Repo log_Repo;
        public KhaiBaoVang_Service()
        {
            _khaiBaoVangRepo = new KhaiBaoVang_Repo();
            log_Repo = new Logs_Repo();
        }
        public Absent AbsentInfo(Absent obj)
        {
            return _khaiBaoVangRepo.AbsentInfo(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public Dictionary<string, string> DanhSachKhaiBao()
        {
            return _khaiBaoVangRepo.DanhSachKhaiBao(StaticParams.connectionStringWiseEyeWebOn);
        }
        public IEnumerable<Absent> Ds_LichKhaiBao_All(Absent obj = null)
        {
            var info = _khaiBaoVangRepo.UserInfo(StaticParams.connectionStringWiseEyeWebOn, HttpContext.Current.User.Identity.Name);
            if (obj.process == "search")
            {
                obj.UserEnrollNumber = info.UserEnrollNumber;
            }
            else
            {
                obj = new Absent();
                obj.UserEnrollNumber = info.UserEnrollNumber;
                obj.UserAccount = HttpContext.Current.User.Identity.Name;
            }

            return _khaiBaoVangRepo.Ds_LichKhaiBao_All(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public IEnumerable<Absent> Ds_LichKhaiBao_Edit(Absent obj = null)
        {
            var info = _khaiBaoVangRepo.UserInfo(StaticParams.connectionStringWiseEyeWebOn, HttpContext.Current.User.Identity.Name);

            if (obj.process == "search")
            {
                obj.UserEnrollNumber = info.UserEnrollNumber;
            }
            else
            {
                if (obj == null)
                {
                    obj = new Absent();
                    obj.UserEnrollNumber = info.UserEnrollNumber;
                    obj.UserAccount = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    obj.UserEnrollNumber = info.UserEnrollNumber;
                }
            }

            return _khaiBaoVangRepo.Ds_LichKhaiBao_Edit(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public IEnumerable<Absent> Ds_LichKhaiBao_View(Absent obj = null)
        {
            var info = _khaiBaoVangRepo.UserInfo(StaticParams.connectionStringWiseEyeWebOn, HttpContext.Current.User.Identity.Name);

            if (obj.process == "search")
            {
                obj.UserEnrollNumber = info.UserEnrollNumber;
            }
            else
            {
                obj = new Absent();
                obj.UserEnrollNumber = info.UserEnrollNumber;
                obj.UserAccount = HttpContext.Current.User.Identity.Name;
            }

            return _khaiBaoVangRepo.Ds_LichKhaiBao_View(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public IEnumerable<Absent> Ds_LichKhaiBao_DieuDuong(Absent obj)
        {
            var info = _khaiBaoVangRepo.UserInfo(StaticParams.connectionStringWiseEyeWebOn, HttpContext.Current.User.Identity.Name);

            if (obj.process == "search")
            {
                obj.UserEnrollNumber = info.UserEnrollNumber;
            }
            else
            {
                if (obj == null)
                {
                    obj = new Absent();
                    obj.UserEnrollNumber = info.UserEnrollNumber;
                    obj.UserAccount = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    obj.UserEnrollNumber = info.UserEnrollNumber;
                }
            }

            return _khaiBaoVangRepo.Ds_LichKhaiBao_DieuDuong(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public bool KiemTraTrungLich(Absent obj)
        {
            return _khaiBaoVangRepo.KiemTraTrungLich(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public IEnumerable<HCNS_NhanVien> SearchUsers(string prefix, string kp)
        {
            return _khaiBaoVangRepo.SearchUsers(StaticParams.connectionStringWiseEyeWebOn, prefix, kp);
        }
        public IEnumerable<HCNS_NhanVien> SearchUsers_DieuDuong(string prefix, string kp)
        {
            return _khaiBaoVangRepo.SearchUsers_DieuDuong(StaticParams.connectionStringWiseEyeWebOn, prefix, kp);
        }
        public bool ThemMoiKhaiBao(List<Absent> objs, Absent checkExist, ref string error)
        {
            try
            {
                if (_khaiBaoVangRepo.KiemTraKhaiBao(StaticParams.connectionStringWiseEyeWebOn, checkExist, ref error))
                {
                    if (_khaiBaoVangRepo.ThemMoiKhaiBao(StaticParams.connectionStringWiseEyeWebOn, objs))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception e)
            {
                error = e.Message;
                return false;
            }
        }
        public bool ThemMoiKhaiBao_Admin(List<Absent> objs, Absent checkExist, ref string error)
        {
            try
            {
                if (_khaiBaoVangRepo.ThemMoiKhaiBao(StaticParams.connectionStringWiseEyeWebOn, objs))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                error = e.Message;
                return false;
            }
        }

        public Absent UserInfo(string user)
        {
            return _khaiBaoVangRepo.UserInfo(StaticParams.connectionStringWiseEyeWebOn, user);
        }
        public bool XoaKhaiBao(Absent obj)
        {
            //IPAddress ip = Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                string jsonString = new JavaScriptSerializer().Serialize(obj);
                if (_khaiBaoVangRepo.XoaKhaiBao(StaticParams.connectionStringWiseEyeWebOn, obj))
                {
                    log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Khai báo vắng",
                        Action = "ThemMoiKhaiBao",
                        Controller = "HCNS.NhanVien",
                        Data = jsonString,
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = user,
                        IP = ip.ToString()
                    });
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Xoa khai báo vắng",
                    Action = "XoaKhaiBao",
                    Controller = "HCNS.NhanVien",
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return false;
            }
        }
        public DataTable Export_KhaiBaoVang(string kp, string tu, string den)
        {
            return _khaiBaoVangRepo.ExportExcel(StaticParams.connectionStringWiseEyeWebOn, kp, tu, den);
        }
        public Dictionary<string, string> DanhSachKhoaPhongHC_Relation(string kp = "")
        {
            try
            {
                return _khaiBaoVangRepo.DanhSachKhoaPhongHC_Relation(StaticParams.connectionStringWiseEyeWebOn, kp);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public DataTable KhaiBaoVang_TongHop(string kp, string dateFrom, string dateTo)
        {
            try
            {
                return _khaiBaoVangRepo.KhaiBaoVang_TongHop(StaticParams.connectionStringWiseEyeWebOn, kp, dateFrom, dateTo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<HCNS_NhanVien> SearchUsersAll(string prefix)
        {
            return _khaiBaoVangRepo.SearchUsers(StaticParams.connectionStringWiseEyeWebOn, prefix, "");
        }

        public IEnumerable<HCNS_NhanVien> SearchUsersHC(string prefix, string kp)
        {
            return _khaiBaoVangRepo.SearchUsersHC(StaticParams.connectionStringWiseEyeWebOn, prefix, kp);
        }
    }
}