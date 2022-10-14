using System;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Security;
using System.App.Services.HCNS;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace HCNS.Controllers
{
    public class DangKySuatAnController : Controller
    {
        private readonly DoiChieuSuatAn_Interface _doichieusuatan_Service;
        private readonly DK_SuatAn_Interface _dkSuatAn;

        public DangKySuatAnController(DoiChieuSuatAn_Interface doiChieuSuatAn_Interface, DK_SuatAn_Interface dangKySuatAn)
        {
            _doichieusuatan_Service = doiChieuSuatAn_Interface;
            _dkSuatAn = dangKySuatAn;
        }

        [CustomAuthorize(StaticParams.HCNS_DangKySuatAn, StaticParams.IT)]
        public ActionResult Index()
        {
            var roles = (string[])System.Web.HttpContext.Current.Items["Roles"];
            var user = System.Web.HttpContext.Current.User.Identity.Name;
            var khoaphong = string.Join(",", (string[])HttpContext.Items["Depts"]);
            Dictionary<int, string> lstKhoaPhong = new Dictionary<int, string>();
            var _disabled = true;

            if (roles.Contains(StaticParams.HCNS_Manager_QuanLySuatAn))
            {
                lstKhoaPhong = _dkSuatAn.DanhSachKhoaPhong(khoaphong);
            }
            if (roles.Contains(StaticParams.HCNS_Admin_QuanLySuatAn))
            {
                _disabled = false;
                lstKhoaPhong = _dkSuatAn.DanhSachKhoaPhong();
            }
            ViewBag.Disabled = _disabled;
            ViewBag.Departments = lstKhoaPhong;

            return View();
        }

        public PartialViewResult Table(string d)
        {
            ViewBag.Date = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return PartialView("_Table");
        }

        public JsonResult DanhSach(string manv = "", string kp = "", string thoidiem = "Lu", string ngay = "")
        {
            string startDate, endDate;

            if (String.IsNullOrEmpty(ngay))
            {
                startDate = DateTime.UtcNow.AddHours(7).AddDays(1).ToString("dd/MM/yyyy");
            }
            else
            {
                startDate = ngay;
            }

            endDate = DateTime.ParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(29).ToString("dd/MM/yyyy");

            var modelList = _dkSuatAn.DS_ChamAn(kp, manv, thoidiem, startDate, endDate);

            return Json(new { data = modelList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CapNhat(List<Person_CA> data)
        {
            if (_dkSuatAn.CapNhat(data))
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.HCNS_BaoCaoChamAn, StaticParams.IT)]
        public ActionResult BaoCaoChamAn()
        {
            ViewBag.Departments = _dkSuatAn.DSKhoaPhong_Eat();
            ViewBag.EatPlace = _dkSuatAn.EatPlace();
            return View();
        }
        public PartialViewResult ThongKe_ChamAn(string dept = "", string tungay = "", string denngay = "", string thoidiem = "", string noian = "", string loai = "", string theo = "")
        {
            DataTable dt = new DataTable();
            dt = _dkSuatAn.ThongKe_ChamAn(dept, tungay, denngay, thoidiem, noian, loai, theo);
            return PartialView("_ThongKe_ChamAn", dt);
        }
    }
}