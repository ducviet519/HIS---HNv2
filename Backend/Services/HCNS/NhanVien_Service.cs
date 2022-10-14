using System.App.Entities;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories;
using System.App.Repositories.HCNS;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace System.App.Services.HCNS
{
    public class NhanVien_Service : NhanVien_Interface
    {
        private readonly NhanVien_Repo nhanVien_Repo;
        private readonly Logs_Repo log_Repo;
        private string user = "";
        private string[] roles;
        public NhanVien_Service()
        {
            nhanVien_Repo = new NhanVien_Repo();
            log_Repo = new Logs_Repo();
            user = HttpContext.Current.User.Identity.Name;
            roles = (string[])HttpContext.Current.Items["Roles"];
        }
        public Dictionary<string, string> DanhSachKhoaPhong()
        {
            return nhanVien_Repo.DanhSachKhoaPhong(StaticParams.connectionStringWiseEyeWebOn);
        }
        public Dictionary<string, string> DanhSachNoiAn()
        {
            return nhanVien_Repo.DanhSachNoiAn(StaticParams.connectionStringWiseEyeWebOn);
        }
        public List<SelectListItem> DanhSachNoiAn_Index()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = nhanVien_Repo.DanhSachNoiAn_Index(StaticParams.connectionStringWiseEyeWebOn);

            foreach (var item in depts)
            {
                items.Add(new SelectListItem() { Value = item.ID, Text = item.MOTA });
            }
            return items;
        }
        public Dictionary<string, string> DanhSachKhoaPhongHC()
        {
            return nhanVien_Repo.DanhSachKhoaPhongHC(StaticParams.connectionStringWiseEyeWebOn);
        }
        public IEnumerable<HCNS_NhanVien> DanhSachNhanVien(HCNS_NhanVien nv = null)
        {
            return nhanVien_Repo.DanhSachNhanVien(StaticParams.connectionStringWiseEyeWebOn, nv);
        }
        public Dictionary<int, string> DoiTuongCC()
        {
            return nhanVien_Repo.DoiTuongCC(StaticParams.connectionStringWiseEyeWebOn);
        }
        public bool ThemNhanVienMoi(HCNS_NhanVien nv)
        {
            IPAddress ip = Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
            var user = HttpContext.Current.User.Identity.Name;
            string message = "";
            try
            {
                string jsonString = new JavaScriptSerializer().Serialize(nv);

                //if (!nhanVien_Repo.KiemTraThongTin(StaticParams.connectionStringWiseEyeWebOn, nv, out message))
                //    return false;

                if (nhanVien_Repo.ThemNhanVienMoi(StaticParams.connectionStringWiseEyeWebOn, nv))
                {
                    log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Cập nhật thông tin nhân viên",
                        Action = "ThemNhanVienMoi",
                        Controller = "HCNS.NhanVien",
                        Data = jsonString,
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = user,
                        IP = ip.ToString()
                    });
                }
                return true;
            }
            catch (Exception e)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Cập nhật thông tin nhân viên",
                    Action = "ThemNhanVienMoi",
                    Controller = "HCNS.NhanVien",
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return false;
            }
        }
        public bool ThemNhanVienMoiExcel(List<HCNS_NhanVien_Excel> lst, out string message)
        {
            message = "";
            try
            {
                return nhanVien_Repo.CapNhatTT_Excel(StaticParams.connectionStringWiseEyeWebOn, lst);
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }
        public bool SuaThongTinNhanVienMoi(HCNS_NhanVien nv)
        {
            IPAddress ip = Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                string jsonString = new JavaScriptSerializer().Serialize(nv);
                string message = "";

                //if (!nhanVien_Repo.KiemTraThongTin(StaticParams.connectionStringWiseEyeWebOn, nv, out message))
                //return false;

                if (nhanVien_Repo.SuaThongTinNhanVienMoi(StaticParams.connectionStringWiseEyeWebOn, nv))
                {
                    log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Cập nhật thông tin nhân viên",
                        Action = "SuaThongTinNhanVienMoi",
                        Controller = "HCNS.NhanVien",
                        Data = jsonString,
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = user,
                        IP = ip.ToString()
                    });
                }
                return true;
            }
            catch (Exception e)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Cập nhật thông tin nhân viên",
                    Action = "SuaThongTinNhanVienMoi",
                    Controller = "HCNS.NhanVien",
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return false;
            }
        }
        public bool SuaTTCapNhatNV(HCNS_NhanVien nv)
        {
            IPAddress ip = Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                string jsonString = new JavaScriptSerializer().Serialize(nv);

                if (nhanVien_Repo.SuaTTCapNhatNV(StaticParams.connectionStringWiseEyeWebOn, nv))
                {
                    log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Cập nhật thông tin nhân viên",
                        Action = "SuaThongTinNhanVienMoi",
                        Controller = "HCNS.NhanVien",
                        Data = jsonString,
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = user,
                        IP = ip.ToString()
                    });
                }
                return true;
            }
            catch (Exception e)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Cập nhật thông tin nhân viên",
                    Action = "SuaTTCapNhatNV",
                    Controller = "HCNS.NhanVien",
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return false;
            }
        }
        public bool XoaNhanVien(HCNS_NhanVien nv)
        {
            IPAddress ip = Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
            var user = HttpContext.Current.User.Identity.Name;

            try
            {
                string jsonString = new JavaScriptSerializer().Serialize(nv);

                if (nhanVien_Repo.XoaNhanVien(StaticParams.connectionStringWiseEyeWebOn, nv))
                {
                    log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Cập nhật thông tin nhân viên",
                        Action = "XoaNhanVien",
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
                    Name = "Cập nhật thông tin nhân viên",
                    Action = "XoaNhanVien",
                    Controller = "HCNS.NhanVien",
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return false;
            }
        }
        public HCNS_NhanVien TimThongTinNhanVien(HCNS_NhanVien nv)
        {
            return nhanVien_Repo.TimThongTinNhanVien(StaticParams.connectionStringWiseEyeWebOn, nv);
        }
        public IEnumerable<HCNS_NhanVien> DS_NhanVienBoSung(HCNS_NhanVien nv = null)
        {
            return nhanVien_Repo.DS_NhanVienBoSung(StaticParams.connectionStringWiseEyeWebOn, nv);
        }
        public IEnumerable<HCNS_NhanVien> DS_NhanVienNghiViec(HCNS_NhanVien nv = null)
        {
            return nhanVien_Repo.DS_NhanVienNghiViec(StaticParams.connectionStringWiseEyeWebOn, nv);
        }
        public Dictionary<int, string> DS_NoiSinh()
        {
            return nhanVien_Repo.DS_NoiSinh(StaticParams.connectionStringWiseEyeWebOn);
        }
        public Dictionary<int, string> DS_NoiCapCMT()
        {
            return nhanVien_Repo.DS_NoiCapCMT(StaticParams.connectionStringWiseEyeWebOn);
        }
        public Dictionary<int, string> DS_DoiTuong()
        {
            return nhanVien_Repo.DS_DoiTuong(StaticParams.connectionStringWiseEyeWebOn);
        }
        public Dictionary<int, string> DS_QuocGia()
        {
            return nhanVien_Repo.DS_QuocGia(StaticParams.connectionStringWiseEyeWebOn);
        }
        public Dictionary<int, string> DS_ThanhPho()
        {
            return nhanVien_Repo.DS_ThanhPho(StaticParams.connectionStringWiseEyeWebOn);
        }
        public Dictionary<int, string> DS_QuanHuyen(int thanhpho)
        {
            return nhanVien_Repo.DS_QuanHuyen(StaticParams.connectionStringWiseEyeWebOn, thanhpho);
        }
        public Dictionary<int, string> DS_PhuongXa(int quanhuyen)
        {
            return nhanVien_Repo.DS_PhuongXa(StaticParams.connectionStringWiseEyeWebOn, quanhuyen);
        }
        public Dictionary<int, string> DS_TonGiao()
        {
            return nhanVien_Repo.DS_TonGiao(StaticParams.connectionStringWiseEyeWebOn);
        }
        public bool Upload_AnhDaiDien(int id, string path)
        {
            //IPAddress ip = Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;

            try
            {
                return nhanVien_Repo.Upload_AnhDaiDien(StaticParams.connectionStringWiseEyeWebOn, id, path);
            }
            catch (Exception e)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Cập nhật thông tin nhân viên",
                    Action = "Upload",
                    Controller = "HCNS.NhanVien",
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return false;
            }
        }
        public Dictionary<int, string> DS_DanToc()
        {
            return nhanVien_Repo.DS_DanToc(StaticParams.connectionStringWiseEyeWebOn);
        }
        public bool SuaTTNghiViec(HCNS_NhanVien nv)
        {
            //IPAddress ip = Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;

            try
            {
                string jsonString = new JavaScriptSerializer().Serialize(nv);

                if (nhanVien_Repo.SuaTTNghiViec(StaticParams.connectionStringWiseEyeWebOn, nv))
                {
                    log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Cập nhật thông tin nhân viên",
                        Action = "SuaTTNghiViec",
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
                    Name = "Cập nhật thông tin nhân viên",
                    Action = "SuaTTNghiViec",
                    Controller = "HCNS.NhanVien",
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return false;
            }
        }
        public bool XoaTTNghiViec(int userEnrollNumber)
        {
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            string jsonString = "{UserEnrollNumber:" + userEnrollNumber + "}";
            try
            {
                if (nhanVien_Repo.XoaTTNghiViec(StaticParams.connectionStringWiseEyeWebOn, userEnrollNumber))
                {
                    log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Cập nhật thông tin nhân viên",
                        Action = "XoaTTNghiViec",
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
                    Name = "Cập nhật thông tin nhân viên",
                    Action = "XoaTTNghiViec",
                    Controller = "HCNS.NhanVien",
                    Message = e.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return false;
            }
        }
        public IEnumerable<DanhBaDienThoai> Ds_DanhBaDienThoai()
        {
            return nhanVien_Repo.Ds_DanhBaDienThoai(StaticParams.connectionStringWiseEyeWebOn);
        }
        private async Task<bool> Send_Email(HCNS_NhanVien nv)
        {
            var obj = nhanVien_Repo.TimThongTinNhanVien(StaticParams.connectionStringWiseEyeWebOn, nv);

            return false;
        }
        public IEnumerable<DanhBaDienThoai> DS_DanhBa_Search(HCNS_NhanVien objsearch)
        {
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhBaDienThoai> lst = nhanVien_Repo.DS_DanhBa_Search(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_DanhBa_Search",
                    Action = "DS_DanhBa_Search",
                    Controller = "HCNS.NhanVien",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public IEnumerable<DanhBaDienThoai_Excel> DS_DanhBa_Excel(HCNS_NhanVien objsearch)
        {
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                IEnumerable<DanhBaDienThoai_Excel> lst = nhanVien_Repo.DS_DanhBa_Excel(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: DS_DanhBa_Excel",
                    Action = "DS_DanhBa_Excel",
                    Controller = "HCNS.NhanVien",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return null;
            }
        }
        public HCNS_NhanVien Get_DanhBa(HCNS_NhanVien obj)
        {
            return nhanVien_Repo.Get_DanhBa(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public List<SelectListItem> DSKhoaPhong(int selected)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = nhanVien_Repo.DSKhoaPhong(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = "-1", Text = "-- Chưa chọn --" });

            foreach (var item in depts)
            {
                if (item.ID == selected)
                    items.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.MOTA, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.MOTA });
            }
            return items;
        }
        public List<SelectListItem> DSToaNha(int selected)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = nhanVien_Repo.DSToaNha(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = "-1", Text = "-- Chưa chọn --" });

            foreach (var item in depts)
            {
                if (item.ID == selected)
                    items.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.MOTA, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.MOTA });
            }
            return items;
        }
        public List<SelectListItem> DSTang(int selected)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = nhanVien_Repo.DSTang(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = "-1", Text = "-- Chưa chọn --" });

            foreach (var item in depts)
            {
                if (item.ID == selected)
                    items.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.MOTA, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.MOTA });
            }
            return items;
        }
        public bool AddNew_DanhBa(HCNS_NhanVien obj, ref string errorMessage)
        {
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return nhanVien_Repo.AddNew_DanhBa(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_DanhBa",
                    Action = "AddNew_DanhBa",
                    Controller = "NhanVien_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Edit_DanhBaDienThoai(HCNS_NhanVien obj, ref string errorMessage)
        {
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return nhanVien_Repo.Edit_DanhBaDienThoai(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_DanhBaDienThoai",
                    Action = "Edit_DanhBaDienThoai",
                    Controller = "NhanVien_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_DanhBaDienThoai(HCNS_NhanVien obj, ref string errorMessage)
        {
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return nhanVien_Repo.Delete_DanhBaDienThoai(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_DanhBaDienThoai",
                    Action = "Delete_DanhBaDienThoai",
                    Controller = "NhanVien_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public string[] GetPermission()
        {
            List<string> permission = new List<string>();

            if (string.IsNullOrEmpty(user))
            {
                return null;
            }
            else
            {
                if (roles.Contains(StaticParams.HCNS_DanhBaDienThoai)) // Mức 2
                {
                    permission.Add(StaticParams.HCNS_DanhBaDienThoai);
                }
                if (roles.Contains(StaticParams.HCNS_Update_DanhBaDienThoai)) // Mức 2
                {
                    permission.Add(StaticParams.HCNS_Update_DanhBaDienThoai);
                }
                if (roles.Contains(StaticParams.HCNS_KhaiBaoNhanVienMoi_View)) // Mức 2
                {
                    permission.Add(StaticParams.HCNS_KhaiBaoNhanVienMoi_View);
                }
            }

            return permission.ToArray();
        }
        public bool XacNhanNoiAn_Multi(string noian, string[] userenrollnumber, ref string errorMessage)
        {
            try
            {
                var userenrollnumberStr = StaticParams.ConvertStringArrayToString(userenrollnumber);
                var obj = new XacNhanAn_Multi()
                {
                    MASO = userenrollnumberStr,
                    MA = noian
                };
                return nhanVien_Repo.XacNhanNoiAn_Multi(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        public Dictionary<string, string> LoaiNhanVien()
        {
            return nhanVien_Repo.LoaiNhanVien(StaticParams.connectionStringWiseEyeWebOn);
        }
        public IEnumerable<HCNS_NhanVien> DanhSachTaiKhoan(HCNS_NhanVien nv = null)
        {
            return nhanVien_Repo.DanhSachTaiKhoan(StaticParams.connectionStringWiseEyeWebOn, nv);
        }

        public bool CapNhatTaiKhoan(string user, string taikhoan, ref string errorMessage)
        {
            try
            {
                return nhanVien_Repo.CapNhatTaiKhoan(StaticParams.connectionStringWiseEyeWebOn, user, taikhoan) > 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

        }
        public bool CapNhatEmail(string user, string email, ref string errorMessage)
        {
            try
            {
                return nhanVien_Repo.CapNhatEmail(StaticParams.connectionStringWiseEyeWebOn, user, email) > 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

        }
        public bool CapNhatMatKhau(string user, string pw, ref string errorMessage)
        {
            try
            {
                return nhanVien_Repo.CapNhatMatKhau(StaticParams.connectionStringWiseEyeWebOn, user, pw) > 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

        }
        public bool SalaryChangeExcel(List<HCNS_NhanVien_Excel> lst, out string message)
        {
            message = "";
            try
            {
                var user = HttpContext.Current.User.Identity.Name;
                return nhanVien_Repo.SalaryChangeExcel(StaticParams.connectionStringWiseEyeWebOn, lst, user);
                //return false;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }
        
        public bool NgayPCExcel(List<HCNS_NhanVien_Excel> lst, out string message)
        {
            message = "";
            try
            {
                return nhanVien_Repo.NgayPCExcel(StaticParams.connectionStringWiseEyeWebOn, lst);
                //return false;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }
        public string ImportFile(DataTable dt, string ID, out string message)
        {
            message = "";
            try
            {
                var user = HttpContext.Current.User.Identity.Name;
                return nhanVien_Repo.ImportFile_Table(StaticParams.connectionStringWiseEyeWebOn, dt, ID, user);
            }
            catch (Exception e)
            {
                message = e.Message;
                return message;
            }
        }

        public IEnumerable<DanhBaDienThoai> DS_Upload(string type)
        {
            return nhanVien_Repo.DS_FileExcel(StaticParams.connectionStringWiseEyeWebOn, type);
        }

        public List<SelectListItem> DS_FileExcel(string type)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var fileExcel = nhanVien_Repo.DS_FileExcel(StaticParams.connectionStringWiseEyeWebOn, type);

            items.Add(new SelectListItem() { Value = "-1", Text = "-- Chưa chọn --", Selected = true });

            foreach (var item in fileExcel)
            {
                items.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.MOTA });
            }
            return items;
        }
        public IEnumerable<HCNS_NhanVien> DS_TheoDoiThaiSan(HCNS_NhanVien objsearch)
        {
            try
            {
                IEnumerable<HCNS_NhanVien> lst = nhanVien_Repo.DS_TheoDoiThaiSan(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool AddNew_TheoDoiThaiSan(HCNS_NhanVien obj, ref string errorMessage)
        {
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return nhanVien_Repo.AddNew_TheoDoiThaiSan(StaticParams.connectionStringWiseEyeWebOn, obj, user) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: AddNew_TheoDoiThaiSan",
                    Action = "AddNew_TheoDoiThaiSan",
                    Controller = "NhanVien_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Edit_TheoDoiThaiSan(HCNS_NhanVien obj, ref string errorMessage)
        {
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return nhanVien_Repo.Edit_TheoDoiThaiSan(StaticParams.connectionStringWiseEyeWebOn, obj, user) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Edit_TheoDoiThaiSan",
                    Action = "Edit_TheoDoiThaiSan",
                    Controller = "NhanVien_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool Delete_TheoDoiThaiSan(HCNS_NhanVien obj, ref string errorMessage)
        {
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                return nhanVien_Repo.Delete_TheoDoiThaiSan(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Lỗi: Delete_TheoDoiThaiSan",
                    Action = "Delete_TheoDoiThaiSan",
                    Controller = "NhanVien_Service",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                errorMessage = ex.Message;
                return false;
            }
        }
        public HCNS_NhanVien Get_TheoDoiThaiSan(HCNS_NhanVien obj)
        {
            return nhanVien_Repo.Get_TheoDoiThaiSan(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public DataTable TheoDoiThaiSan_Excel(HCNS_NhanVien obj)
        {
            return nhanVien_Repo.TheoDoiThaiSan_Excel(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public DataTable Export_Excel(string loaifile, string tungay, string denngay, string khoaphong, string manv)
        {
            return nhanVien_Repo.Export_Excel(StaticParams.connectionStringWiseEyeWebOn, loaifile, tungay, denngay, khoaphong, manv);
        }
    }
}