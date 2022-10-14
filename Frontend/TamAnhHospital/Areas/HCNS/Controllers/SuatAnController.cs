using ClosedXML.Excel;
using System;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Security;
using System.App.Services;
using System.App.Services.HCNS;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace HCNS.Controllers
{
    public class SuatAnController : Controller
    {
        private readonly DoiChieuSuatAn_Interface _doichieusuatan_Service;
        private readonly DK_SuatAn_Interface _dkSuatAn;
        private readonly ChamCong_Interface _ccService;
        private readonly IConfig _configService;

        private string[] _roles = null;
        private string _user = "";
        private string _khoaphong = null;

        public SuatAnController(DoiChieuSuatAn_Interface doiChieuSuatAn_Interface, DK_SuatAn_Interface dangKySuatAn, ChamCong_Interface ccInterface, IConfig configInterface)
        {
            _roles = (string[])System.Web.HttpContext.Current.Items["Roles"];
            _user = System.Web.HttpContext.Current.User.Identity.Name;
            //_khoaphong = string.Join(",", (string[])System.Web.HttpContext.Current.Items["Depts"]);

            _doichieusuatan_Service = doiChieuSuatAn_Interface;
            _dkSuatAn = dangKySuatAn;
            _ccService = ccInterface;
            _configService = configInterface;
        }

        [CustomAuthorize(StaticParams.HCNS_DangKySuatAn, StaticParams.IT)]
        public ActionResult DangKy()
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = _user });

            //Dictionary<int, string> lstKhoaPhong = new Dictionary<int, string>();
            var _disabled = true;

            if (_roles.Contains(StaticParams.HCNS_Manager_QuanLySuatAn))
            {
                ViewBag.Departments = _ccService.DSKhoaPhong(userInfo.UserEnrollNumber);
                //lstKhoaPhong = _dkSuatAn.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString());
            }
            if (_roles.Contains(StaticParams.HCNS_Admin_QuanLySuatAn))
            {
                _disabled = false;
                ViewBag.Departments = _ccService.DSKhoaPhong();
            }
            ViewBag.Disabled = _disabled;
            //ViewBag.Departments = lstKhoaPhong;
            ViewBag.UserInfo = userInfo;

            return View();
        }

        [CustomAuthorize(StaticParams.HCNS_ThongKeSuatAn, StaticParams.IT)]
        public ActionResult ThongKe()
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = _user });

            //Dictionary<int, string> lstKhoaPhong = new Dictionary<int, string>();
            //var _disabled = true;

            if (_roles.Contains(StaticParams.HCNS_Manager_QuanLySuatAn))
            {
                ViewBag.Departments = _ccService.DSKhoaPhong(userInfo.UserEnrollNumber);
            }
            if (_roles.Contains(StaticParams.HCNS_Admin_QuanLySuatAn))
            {
                ViewBag.Departments = _ccService.DSKhoaPhong();
            }
            ViewBag.Auth = _roles;
            ViewBag.UserInfo = userInfo;
            return View();
        }

        public PartialViewResult ThongKe_Table(string dept = "", string tungay = "", string denngay = "", string thoidiem = "")
        {
            DataTable dt = new DataTable();
            if (dept == "-1")
            {
                if (_roles.Contains(StaticParams.HCNS_Admin_QuanLySuatAn))
                {
                    dt = _doichieusuatan_Service.DanhSachThongKe(dept, tungay, denngay, thoidiem);
                }
                else
                {
                    dt = null;
                }
            }
            else
                dt = _doichieusuatan_Service.DanhSachThongKe(dept, tungay, denngay, thoidiem);
            int songay = (DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture) - DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture)).Days;
            ViewBag.SONGAY = songay;
            ViewBag.TUNGAY = DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.DENNGAY = DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return PartialView("_ThongKe_Table", dt);
        }

        public ActionResult ThongKe_Excel(string dept = "", string tungay = "", string denngay = "", string thoidiem = "")
        {
            var dataTable = _doichieusuatan_Service.DanhSachThongKe_Excel(dept, tungay, denngay, thoidiem);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Thống kê suất ăn");

                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                ws.Cell(1, 1).InsertTable(dataTable);
                var range1 = ws.Range(ws.Cell(1, 1).Address, ws.Cell(1 + dataTable.Rows.Count, 1).Address);
                var range2 = ws.Range(ws.Cell(1, 3).Address, ws.Cell(1 + dataTable.Rows.Count, dataTable.Columns.Count).Address);
                var range3 = ws.Range(ws.Cell(1, 1).Address, ws.Cell(1 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                range2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                range3.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                range3.Style.Border.InsideBorderColor = XLColor.Black;
                range3.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                range3.Style.Border.OutsideBorderColor = XLColor.Black; ;

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=export.xls");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(System.Web.HttpContext.Current.Response.OutputStream);
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.End();
                }
            }

            return View();
        }

        [HttpPost]
        public JsonResult PushData(AbsentR obj)
        {
            string _errorMessage = "";
            return Json(new { result = _doichieusuatan_Service.PushData(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult DangKy_Table(string d)
        {
            ViewBag.Date = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return PartialView("_DangKy_Table");
        }

        public JsonResult DangKy_DanhSach(string manv = "", string kp = "", string thoidiem = "Lu", string ngay = "")
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
        public ActionResult DangKy_CapNhat(List<Person_CA> data)
        {
            if (_dkSuatAn.CapNhat(data))
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_DoiChieuSuatAn, StaticParams.IT)]
        public ActionResult DoiChieu()
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = _user });
            //Dictionary<int, string> lstKhoaPhong = new Dictionary<int, string>();
            //ViewBag.Departments = _doichieusuatan_Service.ListDepartment();
            if (_roles.Contains(StaticParams.HCNS_Manager_QuanLySuatAn))
            {
                ViewBag.Departments = _ccService.DSKhoaPhong(userInfo.UserEnrollNumber);
            }
            if (_roles.Contains(StaticParams.HCNS_Admin_QuanLySuatAn))
            {
                ViewBag.Departments = _ccService.DSKhoaPhong();
            }
            //ViewBag.Departments = _ccService.DSKhoaPhong();
            ViewBag.UserInfo = userInfo;
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();

            return View();
        }

        public PartialViewResult DoiChieu_Table(string dept = "", string tungay = "", string toingay = "")
        {
            var model = _doichieusuatan_Service.DanhSachSuatAn(dept, tungay, toingay);
            return PartialView("_DoiChieu_Table", model);
        }

        public ActionResult DoiChieu_Excel(string dept = "", string tungay = "", string toingay = "")
        {
            var dataTable = _doichieusuatan_Service.DanhSachSuatAnExcel(dept, tungay, toingay);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Đối chiếu suất ăn");

                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                ws.Cell(1, 1).InsertTable(dataTable);
                var range1 = ws.Range(ws.Cell(1, 1).Address, ws.Cell(1 + dataTable.Rows.Count, 1).Address);
                var range2 = ws.Range(ws.Cell(1, 3).Address, ws.Cell(1 + dataTable.Rows.Count, dataTable.Columns.Count).Address);
                var range3 = ws.Range(ws.Cell(1, 1).Address, ws.Cell(1 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                range2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                range3.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                range3.Style.Border.InsideBorderColor = XLColor.Black;
                range3.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                range3.Style.Border.OutsideBorderColor = XLColor.Black; ;

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=export.xls");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(System.Web.HttpContext.Current.Response.OutputStream);
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.End();
                }
            }

            return View();
        }
        private string NgayDauKyLuong()
        {
            return _configService.NgayBatDauChuKyLuong().DateTimeVal?.ToString("dd/MM/yyyy");
        }
        private string NgayCuoiKyLuong()
        {
            return _configService.NgayKetThucChuKyLuong().DateTimeVal?.ToString("dd/MM/yyyy");
        }
    }
}