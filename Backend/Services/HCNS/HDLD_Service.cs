using System.App.Entities;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories;
using System.App.Repositories.HCNS;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Script.Serialization;

namespace System.App.Services.HCNS
{
    public class HDLD_Service : HDLD_Interface
    {
        private readonly HDLD_Repo _hdldRepo;
        private readonly Logs_Repo _logRepo;
        private string _ip, _user;

        public HDLD_Service()
        {
            _hdldRepo = new HDLD_Repo();
            _logRepo = new Logs_Repo();
            _ip = HttpContext.Current.Request.UserHostAddress;
            _user = HttpContext.Current.User.Identity.Name;
        }

        public HDLD ThongTinHDLD(HDLD obj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(obj);

            try
            {
                return _hdldRepo.ThongTinHDLD(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception e)
            {
                _logRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Hợp đồng lao động",
                    Action = "ThongTinHDLD",
                    Controller = "HDLD.Error",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip.ToString()
                });
                return null;
            }
        }

        public IEnumerable<HDLD> DanhSachHopDong(HDLD obj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(obj);
            try
            {
                return _hdldRepo.DanhSachHopDong(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception e)
            {
                _logRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Hợp đồng lao động",
                    Action = "ThongTinHDLD",
                    Controller = "HDLD.Error",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip.ToString()
                });
                return null;
            }
        }

        public Dictionary<int, string> DS_LoaiHopDong()
        {
            try
            {
                return _hdldRepo.DS_LoaiHopDong(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception e)
            {
                _logRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Hợp đồng lao động",
                    Action = "ThongTinHDLD",
                    Controller = "HDLD.Error",
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip.ToString()
                });
                return null;
            }
        }

        public bool ThemMoiHopDong(HDLD obj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(obj);

            try
            {
                if (string.IsNullOrEmpty(obj.NgayDG))
                {
                    var date = DateTime.ParseExact(obj.NgayKy, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    switch (obj.LoaiHD)
                    {
                        case 2:
                            date = date.AddMonths(6).AddDays(-1);
                            break;

                        case 3:
                            date = date.AddMonths(2).AddDays(-1);
                            break;

                        case 5:
                            date = date.AddYears(1).AddDays(-1);
                            break;

                        default:
                            break;
                    }
                }

                return _hdldRepo.ThemMoiHopDong(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception e)
            {
                _logRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Hợp đồng lao động",
                    Action = "ThongTinHDLD",
                    Controller = "HDLD.Error",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip.ToString()
                });
                return false;
            }
        }

        public bool CapNhatHopDong(HDLD obj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(obj);
            try
            {
                return _hdldRepo.CapNhatHopDong(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception e)
            {
                _logRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Hợp đồng lao động",
                    Action = "ThongTinHDLD",
                    Controller = "HDLD.Error",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip.ToString()
                });
                return false;
            }
        }

        public bool KiemTraTrung(HDLD obj, out string message)
        {
            message = "";
            string jsonString = new JavaScriptSerializer().Serialize(obj);
            try
            {
                return _hdldRepo.KiemTraTrung(StaticParams.connectionStringWiseEyeWebOn, obj, out message);
            }
            catch (Exception e)
            {
                _logRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Hợp đồng lao động",
                    Action = "ThongTinHDLD",
                    Controller = "HDLD.Error",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip.ToString()
                });
                return false;
            }
        }

        public HDLD HopDongInfo(HDLD obj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(obj);

            try
            {
                return _hdldRepo.HopDongInfo(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception e)
            {
                _logRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Hợp đồng lao động",
                    Action = "ThongTinHDLD",
                    Controller = "HDLD.Error",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip.ToString()
                });
                return null;
            }
        }

        public bool CapNhatExcel(List<HDLD> lstObj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(lstObj);

            try
            {
                return _hdldRepo.CapNhatExcel(StaticParams.connectionStringWiseEyeWebOn, lstObj);
            }
            catch (Exception e)
            {
                _logRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Hợp đồng lao động",
                    Action = "ThongTinHDLD",
                    Controller = "HDLD.Error",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip.ToString()
                });
                return false;
            }
        }

        public IEnumerable<HDLD> DanhSachHopDongDaKy(int userid)
        {
            try
            {
                return _hdldRepo.DanhSachHopDongDaKy(StaticParams.connectionStringWiseEyeWebOn, userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CapSoHopDong(string prefix, int userid, int mahd, string ngayky)
        {
            var hd_no = _hdldRepo.LayMaHopDong(StaticParams.connectionStringWiseEyeWebOn, mahd);
            var date = DateTime.ParseExact(ngayky, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            return prefix + userid.ToString().PadLeft(4, '0') + "/" + date.ToString("dd.MM.yyyy") + "/" + hd_no;
        }

        public bool HuyHopDong(int id)
        {
            var obj = _hdldRepo.ThongTinHDLD(StaticParams.connectionStringWiseEyeWebOn, id);

            string jsonString = new JavaScriptSerializer().Serialize(obj);

            try
            {
                if (_hdldRepo.HuyHopDong(StaticParams.connectionStringWiseEyeWebOn, id))
                {
                    _logRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Hợp đồng lao động",
                        Action = "HuyHopDong",
                        Controller = "HDLD",
                        Data = jsonString,
                        Message = "",
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = _user,
                        IP = _ip.ToString()
                    });
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Hợp đồng lao động",
                    Action = "ThongTinHDLD",
                    Controller = "HDLD.Error",
                    Data = jsonString,
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip.ToString()
                });
                return false;
            }
        }

        public bool UpdateNgay(int id, string ngay, string type)
        {
            var obj = _hdldRepo.ThongTinHDLD(StaticParams.connectionStringWiseEyeWebOn, id);

            string jsonString = new JavaScriptSerializer().Serialize(obj);

            try
            {
                if (_hdldRepo.UpdateNgay(StaticParams.connectionStringWiseEyeWebOn, id, ngay, type))
                {
                    _logRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Hợp đồng lao động",
                        Action = "UpdateNgay",
                        Controller = "HDLD",
                        Data = jsonString,
                        Message = "",
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = _user,
                        IP = _ip.ToString()
                    });
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logRepo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Hợp đồng lao động",
                    Action = "ThongTinHDLD",
                    Controller = "HDLD.Error",
                    Data = jsonString,
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip.ToString()
                });
                return false;
            }
        }
    }
}