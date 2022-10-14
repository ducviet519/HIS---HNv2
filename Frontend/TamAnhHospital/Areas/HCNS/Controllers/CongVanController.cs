using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Security;
using System.App.Services.HCNS;
using System.App.Services.Interfaces;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HCNS.Controllers
{
    public class CongVanController : Controller
    {
        private IHCNS_CongVan _cvService;
        private NhanVien_Interface _nvService;
        private IKhaiBaoVang _kbvService;
        private ChamCong_Interface _ccService;

        private string[] __roles;
        private string __user;

        public CongVanController(IHCNS_CongVan congvanService, NhanVien_Interface nhanvienService, IKhaiBaoVang kbvService, ChamCong_Interface ccService)
        {
            _cvService = congvanService;
            _nvService = nhanvienService;
            _kbvService = kbvService;
            _ccService = ccService;

            __roles = (string[])System.Web.HttpContext.Current.Items["Roles"];
            __user = System.Web.HttpContext.Current.User.Identity.Name;
        }

        [CustomAuthorize(StaticParams.HCNS_QuanLyCongVan, StaticParams.IT)]
        public ActionResult Index()
        {
            ViewBag.KhoaPhong = _nvService.DanhSachKhoaPhongHC();
            return View();
        }

        public JsonResult Data_DanhSachCongVan(string cv = "", int kp = 0)
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                return Json(new { data = _cvService.DanhSachCongVan(new HCNS_CongVan() { SO_CV = cv, NOI_NHAN = kp }) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = _cvService.DanhSachCongVan(new HCNS_CongVan() { SO_CV = cv, NOI_NHAN = userInfo.PhongKhoaHC }) }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult Add_CongVan()
        {
            ViewBag.KhoaPhong = _nvService.DanhSachKhoaPhongHC();
            return PartialView("_Add_CongVan", new HCNS_CongVan());
        }

        public PartialViewResult Update_CongVan(int id)
        {
            ViewBag.KhoaPhong = _nvService.DanhSachKhoaPhongHC();
            var userAuth = "";

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                userAuth = "HCNS_Admin";
            }
            else
            {
                userAuth = "HCNS_User";
            }
            ViewBag.UserAuth = userAuth;
            return PartialView("_Update_CongVan", _cvService.ThongTinCongVan(new HCNS_CongVan() { ID = id }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_Add_CongVan(HCNS_CongVan obj)
        {
            HttpPostedFileBase file = obj.LINK_FILE;

            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/CongVan/");
                file.SaveAs(path + file.FileName);
                string extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                string filePath = string.Format(@"{0}\{1}", Server.MapPath("~/Content/Uploads/CongVan"), file.FileName);
                obj.FILE_PATH = filePath;
                obj.TEN_FILE = file.FileName;
            }
            return Json(new { result = _cvService.ThemMoiCongVan(obj) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_Update_CongVan(HCNS_CongVan obj)
        {
            HttpPostedFileBase file = obj.LINK_FILE;

            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/CongVan/");
                file.SaveAs(path + file.FileName);
                string extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                string filePath = string.Format(@"{0}\{1}", Server.MapPath("~/Content/Uploads/CongVan"), file.FileName);
                obj.FILE_PATH = filePath;
                obj.TEN_FILE = file.FileName;
            }
            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                return Json(new { result = _cvService.CapNhatCongVan_Admin(obj) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = _cvService.CapNhatCongVan_User(obj) }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetUser(string prefix = "", string kp = "")
        {
            string jsonString = new JavaScriptSerializer().Serialize(_kbvService.SearchUsersHC(prefix, kp));

            return Json(jsonString, JsonRequestBehavior.AllowGet);
        }
    }
}