using System.App.Entities;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories;
using System.App.Repositories.HCNS;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;

namespace System.App.Services.HCNS
{
    public class BCCC_Service : IBCCC
    {
        private readonly BCCC_Repo _bcccRepo;
        private readonly Logs_Repo _logRepo;
        private readonly string _user, _ip;

        public BCCC_Service()
        {
            _bcccRepo = new BCCC_Repo();
            _logRepo = new Logs_Repo();

            _user = HttpContext.Current.User.Identity.Name;
            _ip = HttpContext.Current.Request.UserHostAddress;
        }

        #region 1. Bằng cấp

        public Dictionary<int, string> DS_LoaiBangCap()
        {
            try
            {
                return _bcccRepo.DS_LoaiBangCap(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool Add_BangCap(BangCap obj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(obj);

            try
            {
                if (_bcccRepo.Add_BangCap(StaticParams.connectionStringWiseEyeWebOn, obj))
                {
                    _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Thêm mới bằng cấp",
                        Action = "CREATE",
                        Controller = "HCNS.BangCap",
                        Data = jsonString,
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = _user,
                        IP = _ip
                    });
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Thêm mới bằng cấp",
                    Action = "ERROR",
                    Controller = "HCNS.BangCap",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip
                });
                return false;
            }
        }
        public bool Update_BangCap(BangCap obj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(obj);

            try
            {
                if (_bcccRepo.Update_BangCap(StaticParams.connectionStringWiseEyeWebOn, obj))
                {
                    _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Cập nhật bằng cấp",
                        Action = "UPDATE",
                        Controller = "HCNS.BangCap",
                        Data = jsonString,
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = _user,
                        IP = _ip
                    });
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Cập nhật bằng cấp",
                    Action = "ERROR",
                    Controller = "HCNS.BangCap",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip
                });
                return false;
            }
        }
        public bool Delete_BangCap(BangCap obj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(obj);

            try
            {
                if (_bcccRepo.Delete_BangCap(StaticParams.connectionStringWiseEyeWebOn, obj))
                {
                    _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Xóa bằng cấp",
                        Action = "DELETE",
                        Controller = "HCNS.BangCap",
                        Data = jsonString,
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = _user,
                        IP = _ip
                    });
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Xóa bằng cấp",
                    Action = "ERROR",
                    Controller = "HCNS.BangCap",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip
                });
                return false;
            }
        }
        public IEnumerable<BangCap> DS_BangCap(BangCap obj)
        {
            try
            {
                return _bcccRepo.DS_BangCap(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public BangCap ThongTin_BangCap(int id)
        {
            try
            {
                return _bcccRepo.ThongTin_BangCap(StaticParams.connectionStringWiseEyeWebOn, id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion 1. Bằng cấp

        #region 2. Chứng chỉ

        public IEnumerable<ChungChi> DS_ChungChi(ChungChi obj)
        {
            try
            {
                return _bcccRepo.DS_ChungChi(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ChungChi ThongTin_ChungChi(int id)
        {
            try
            {
                return _bcccRepo.ThongTin_ChungChi(StaticParams.connectionStringWiseEyeWebOn, id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Add_ChungChi(ChungChi obj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(obj);

            try
            {
                if (_bcccRepo.Add_ChungChi(StaticParams.connectionStringWiseEyeWebOn, obj))
                {
                    _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Thêm mới chứng chỉ",
                        Action = "CREATE",
                        Controller = "HCNS.ChungChi",
                        Data = jsonString,
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = _user,
                        IP = _ip
                    });
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Thêm mới chứng chỉ",
                    Action = "ERROR",
                    Controller = "HCNS.ChungChi",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip
                });
                return false;
            }
        }

        public bool Update_ChungChi(ChungChi obj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(obj);

            try
            {
                if (_bcccRepo.Update_ChungChi(StaticParams.connectionStringWiseEyeWebOn, obj))
                {
                    _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Cập nhật chứng chỉ",
                        Action = "UPDATE",
                        Controller = "HCNS.ChungChi",
                        Data = jsonString,
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = _user,
                        IP = _ip
                    });
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Sửa chứng chỉ",
                    Action = "ERROR",
                    Controller = "HCNS.ChungChi",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip
                });
                return false;
            }
        }

        public bool Delete_ChungChi(ChungChi obj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(obj);

            try
            {
                if (_bcccRepo.Delete_ChungChi(StaticParams.connectionStringWiseEyeWebOn, obj))
                {
                    _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Xóa chứng chỉ",
                        Action = "DELETE",
                        Controller = "HCNS.ChungChi",
                        Data = jsonString,
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = _user,
                        IP = _ip
                    });
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Xóa chứng chỉ",
                    Action = "ERROR",
                    Controller = "HCNS.ChungChi",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip
                });
                return false;
            }
        }

        #endregion 2. Chứng chỉ

        #region 3. Chứng chỉ hành nghề

        public IEnumerable<ChungChiHanhNghe> DS_ChungChiHanhNghe(ChungChiHanhNghe obj)
        {
            try
            {
                return _bcccRepo.DS_ChungChiHanhNghe(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ChungChiHanhNghe ThongTin_ChungChiHanhNghe(int id)
        {
            try
            {
                return _bcccRepo.ThongTin_ChungChiHanhNghe(StaticParams.connectionStringWiseEyeWebOn, id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update_ChungChiHanhNghe(ChungChiHanhNghe obj)
        {
            string jsonString = new JavaScriptSerializer().Serialize(obj);

            try
            {
                if (_bcccRepo.Update_ChungChiHanhNghe(StaticParams.connectionStringWiseEyeWebOn, obj))
                {
                    _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Cập nhật chứng chỉ",
                        Action = "UPDATE",
                        Controller = "HCNS.ChungChiHanhNghe",
                        Data = jsonString,
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = _user,
                        IP = _ip
                    });
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logRepo.Insert_Logs(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Sửa chứng chỉ",
                    Action = "ERROR",
                    Controller = "HCNS.ChungChiHanhNghe",
                    Data = jsonString,
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = _user,
                    IP = _ip
                });
                return false;
            }
        }

        #endregion 3. Chứng chỉ hành nghề
    }
}