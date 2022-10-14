using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Security;
using System.App.Services.HCNS;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HCNS.Controllers
{
    public class ZKController : Controller
    {
        private readonly ZK_Service _ZKService = null;

        public ZKController()
        {
            _ZKService = new ZK_Service();
        }

        [CustomAuthorize(StaticParams.HCNS_QuanLyVanTay, StaticParams.IT)]
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorize(StaticParams.HCNS_ThongTinThietBi, StaticParams.IT)]
        public ActionResult ThietBi()
        {
            return View(_ZKService.DS_ThietBi());
        }

        public PartialViewResult ThongTin(string ip, int totalUser, int totalFinger, int totalAtt)
        {
            string json = "";
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString("http://" + objAppsettings.Settings["ZK_Host"].Value + "/action=info/device=" + ip);
            }

            JavaScriptSerializer json_serializer = new JavaScriptSerializer();

            var routes_list = json_serializer.DeserializeObject(json) as object;
            var obj = (Dictionary<string, object>)routes_list;
            ZK_Device_Info template = new ZK_Device_Info();
            template.IP = ip;
            template.AdminCount = int.Parse(obj["AdminCount"].ToString());
            template.UserCount = int.Parse(obj["UserCount"].ToString());
            template.FingerCount = int.Parse(obj["FingerCount"].ToString());
            template.RecordCount = int.Parse(obj["RecordCount"].ToString());
            template.PasswordCount = int.Parse(obj["PasswordCount"].ToString());
            template.OptionCount = int.Parse(obj["OptionCount"].ToString());
            template.FaceCount = int.Parse(obj["FaceCount"].ToString());
            template.TotalFinger = totalFinger;
            template.TotalUser = totalUser;
            template.TotalAtt = totalAtt;
            return PartialView("_ThongTin", template);
        }

        public PartialViewResult LayVanTay()
        {
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
            var ipHost = objAppsettings.Settings["ZK_Host"].Value;

            ViewBag.IP_Host = ipHost;
            ViewData["Device"] = _ZKService.DS_ThietBi();

            return PartialView("_LayVanTay");
        }

        public PartialViewResult DongBo()
        {
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
            var ipHost = objAppsettings.Settings["ZK_Host"].Value;

            ViewBag.IP_Host = ipHost;
            ViewData["Device"] = _ZKService.DS_ThietBi();

            return PartialView("_DongBo");
        }

        public PartialViewResult NghiViec()
        {
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
            var ipHost = objAppsettings.Settings["ZK_Host"].Value;

            ViewBag.IP_Host = ipHost;
            ViewData["Device"] = _ZKService.DS_ThietBi();
            return PartialView("_NghiViec");
        }

        public JsonResult DS_NhanVien()
        {
            var abc = _ZKService.DS_User();
            return Json(new { data = abc }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DS_NhanVien_NghiViec()
        {
            return Json(new { data = _ZKService.DS_UserNghiViec() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Push_VanTay(string users, string devices)
        {
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
            var ipHost = objAppsettings.Settings["ZK_Host"].Value;

            string json = "";
            string[] device = devices.Split(',');

            for (int c = 0; c < device.Length; c++)
            {
                using (WebClient wc = new WebClient())
                {
                    json = wc.DownloadString("http://" + ipHost + "/action=push/connect=" + device[c].ToString() + "/user=" + users);
                }
            }

            return Json(new
            {
                //result = _ZKService.CapNhat_Template(template)
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XL_VanTay(string url)
        {
            string json = "";

            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(url);
            }

            JavaScriptSerializer json_serializer = new JavaScriptSerializer();

            var routes_list = json_serializer.DeserializeObject(json) as object[];

            List<ZK_Person_Finger> template = new List<ZK_Person_Finger>();

            for (int i = 0; i < routes_list.Length; i++)
            {
                var obj = (Dictionary<string, object>)routes_list[i];

                template.Add(new ZK_Person_Finger()
                {
                    UserEnrollNumber = int.Parse(obj["UserEnrollNumber"].ToString()),
                    FingerIndex = int.Parse(obj["FingerIndex"].ToString()),
                    DataFinger = obj["DataFinger"].ToString(),
                    DataLength = int.Parse(obj["DataLength"].ToString()),
                    Flag = int.Parse(obj["Flag"].ToString())
                });
            }

            return Json(new
            {
                result = _ZKService.CapNhat_Template(template)
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xoa_VanTay(string users, string devices)
        {
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
            var ipHost = objAppsettings.Settings["ZK_Host"].Value;

            string[] device = devices.Split(',');

            for (int i = 0; i < device.Length; i++)
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadString("http://" + ipHost + "/action=delete/device=" + device[i].ToString() + "/user=" + users);
                }
            }

            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_Face(string users, string devices)
        {
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
            var ipHost = objAppsettings.Settings["ZK_Host"].Value;

            string json = "";
            string[] device = devices.Split(',');

            for (int c = 0; c < device.Length; c++)
            {
                using (WebClient wc = new WebClient())
                {
                    json = wc.DownloadString("http://" + ipHost + "/action=getface/connect=" + device[c].ToString() + "/user=" + users);
                }
            }

            return Json(new
            {
                //result = _ZKService.CapNhat_Template(template)
                result = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Insert_Face(string users, string devices)
        {
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
            var ipHost = objAppsettings.Settings["ZK_Host"].Value;

            string json = "";
            string[] device = devices.Split(',');

            for (int c = 0; c < device.Length; c++)
            {
                using (WebClient wc = new WebClient())
                {
                    json = wc.DownloadString("http://" + ipHost + "/action=syncface/connect=" + device[c].ToString() + "/user=" + users);
                }
            }

            return Json(new
            {
                //result = _ZKService.CapNhat_Template(template)
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}