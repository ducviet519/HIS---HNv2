using ClosedXML.Excel;
using Newtonsoft.Json;
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
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HCNS.Controllers
{
    public class ChamCongController : Controller
    {
        private readonly NhanVien_Interface _nhanVienService;
        private readonly ChamCong_Interface _ccService;
        private readonly IKhaiBaoVang _kbService;
        private readonly IConfig _configService;

        private string[] __roles;
        private string __user;
        private int locked = 0;
        private string lockedMessage = "";

        public ChamCongController(IKhaiBaoVang kbInterface, ChamCong_Interface ccInterface, IConfig configInterface, NhanVien_Interface nhanVien_Interface)
        {
            _ccService = ccInterface;
            _kbService = kbInterface;
            __user = System.Web.HttpContext.Current.User.Identity.Name;
            _configService = configInterface;
            _nhanVienService = nhanVien_Interface;

            __roles = (string[])System.Web.HttpContext.Current.Items["Roles"];

            if (KiemTraKhoaSoLieu(ref lockedMessage))
            {
                locked = 1;
            }
        }

        #region Kiểm tra số liệu
        private string NgayDauKyLuong()
        {
            return _configService.NgayBatDauChuKyLuong().DateTimeVal?.ToString("dd/MM/yyyy");
        }
        private string NgayCuoiKyLuong()
        {
            return _configService.NgayKetThucChuKyLuong().DateTimeVal?.ToString("dd/MM/yyyy");
        }

        private bool KiemTraKhoaSoLieu(ref string message)
        {
            bool check = false;
            if (__roles == null)
            {
                return true;
            }
            else if (__roles.Contains(StaticParams.HCNS_KiemTraKhoaSoLieu))
            {
            }
            else
            {
                if (_configService.KhoaDuLieu())
                {
                    message = "Số liệu đã bị khóa, bạn không thể khai báo.";
                    check = true;
                }
            }

            return check;
        }

        private bool KiemTraHetHanKhaiBao(string ngaykhaibao, ref string message)
        {
            bool check = false;

            if (__roles.Contains(StaticParams.HCNS_KiemTraHetHanKhaiBao))
            {
            }
            else
            {
                var quydinh = _configService.NgayQuyDinhKhaiBaoCong().NumberVal;

                //var ngaychotcong = DateTime.UtcNow.AddHours(7).AddMonths(-1).ToString("25/MM/yyyy");
                //var ngaykhaibaocuoi = DateTime.UtcNow.AddHours(7).ToString("24/MM/yyyy");
                var ngaychotcong = DateTime.UtcNow.AddHours(7).ToString("01/MM/yyyy");
                var ngaykhaibaocuoi = DateTime.UtcNow.AddHours(7).ToString("dd/MM/yyyy");
                if (DateTime.UtcNow.AddHours(7).Month == 1 || DateTime.UtcNow.AddHours(7).Month == 3 || DateTime.UtcNow.AddHours(7).Month == 5 || DateTime.UtcNow.AddHours(7).Month == 7 || DateTime.UtcNow.AddHours(7).Month == 8 || DateTime.UtcNow.AddHours(7).Month == 10 || DateTime.UtcNow.AddHours(7).Month == 12)
                {
                    ngaykhaibaocuoi = DateTime.UtcNow.AddHours(7).ToString("31/MM/yyyy");
                }
                else if (DateTime.UtcNow.AddHours(7).Month == 4 || DateTime.UtcNow.AddHours(7).Month == 6 || DateTime.UtcNow.AddHours(7).Month == 9 || DateTime.UtcNow.AddHours(7).Month == 11)
                {
                    ngaykhaibaocuoi = DateTime.UtcNow.AddHours(7).ToString("30/MM/yyyy");
                }
                else
                {
                    if (DateTime.UtcNow.AddHours(7).Year / 4 == 0)
                    {
                        ngaykhaibaocuoi = DateTime.UtcNow.AddHours(7).ToString("29/MM/yyyy");
                    }
                    else
                    {
                        ngaykhaibaocuoi = DateTime.UtcNow.AddHours(7).ToString("28/MM/yyyy");
                    }
                }
                var ngayhientai = DateTime.UtcNow.AddHours(7);
                var ngaychotcongFormat = DateTime.ParseExact(ngaychotcong, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var ngaykhaibaocuoiFormat = DateTime.ParseExact(ngaykhaibaocuoi, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var ngaykhaibaoFormat = DateTime.ParseExact(ngaykhaibao, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (ngaykhaibaoFormat < ngaychotcongFormat)
                {
                    message = "Không được khai báo trước ngày chốt công của tháng trước.";
                    check = true;
                }
                else if ((ngayhientai - ngaykhaibaoFormat).TotalDays > quydinh)
                {
                    message = "Bạn đã khai báo quá quy định, bạn chỉ được khai báo không quá " + quydinh + " ngày.";
                    check = true;
                }
            }
            return check;
        }

        #endregion Kiểm tra số liệu

        //[CustomAuthorize(StaticParams.HCNS_Admin, StaticParams.HCNS_Manager, StaticParams.HCNS_Test3)]
        public ActionResult TheoDoiCong_User()
        {
            ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();

            return View();
        }

        [CustomAuthorize(StaticParams.HCNS_XemGioVaoRaThucTe, StaticParams.IT)]
        public ActionResult SuaCong()
        {
            var auth = "HCNS_User";
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            #region Kiểm tra hạn khai báo

            ViewBag.Locked = locked;
            ViewBag.LockMessage = lockedMessage;

            #endregion Kiểm tra hạn khai báo

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
                auth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_DieuDuong)) 
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
                auth = "HCNS_DieuDuong";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                auth = "HCNS_Manager";
            }
            else
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            }

            ViewBag.Auth = auth;
            ViewBag.UserInfo = userInfo;
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            return View();
        }

        public JsonResult Data_SuaCong(string id = "", string pk = "", string batdau = "", string ketthuc = "")
        {
            return Json(new { data = _ccService.DanhSach_SuaCong(id, pk, batdau, ketthuc) }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_ThemDuLieuCong, StaticParams.IT)]
        public PartialViewResult ThemDuLieuCong(string id = "")
        {
            int pkhc = 0;
            int pk = 0;
            int trangthai = 2;
            string gio = "Full";
            HCNS_NhanVien hcns = new HCNS_NhanVien
            {
                UserFullName = id,
                PhongKhoaHC = pkhc,
                KhoaPhong = pk,
                TrangThai = trangthai,
                SoGioLamViec = gio
            };
            var data = _nhanVienService.DanhSachNhanVien(hcns);
            return PartialView("_ThemDuLieuCong", new CC_CheckInOut() { UserEnrollNumber = data.FirstOrDefault().UserEnrollNumber, UserFullName = data.FirstOrDefault().UserFullName });
        }
        public PartialViewResult GhepCa(string val)
        {
            ViewBag.Val = val;
            return PartialView("_GhepCa");
        }

        [HttpPost]
        public JsonResult Push_ThemDuLieuCong(CC_CheckInOut obj)
        {
            return Json(new { result = _ccService.Add_DuLieuCong(obj), message = "" }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_SuaDuLieuCong, StaticParams.IT)]
        public PartialViewResult SuaDuLieuCong(string id, string date, string time)
        {
            var o = _ccService.Tim_Cong(id, date, time);

            return PartialView("_SuaDuLieuCong", o);
        }

        [HttpPost]
        public JsonResult Push_SuaDuLieuCong(string id, string date, string time, string newTime)
        {
            return Json(new { result = _ccService.Update_DuLieuCong(id, date, time, newTime), message = "" }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_XoaDuLieuCong, StaticParams.IT)]
        [HttpPost]
        public JsonResult XoaDuLieuCong(string id, string date, string time)
        {
            return Json(new { result = _ccService.Xoa_DuLieuCong(id, date, time), message = "" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CongChuan()
        {
            ViewBag.Departments = _ccService.DanhSachKhoaPhong();
            return View();
        }

        public PartialViewResult Table_CongChuan(string s = "", string e = "", string makp = "", string manv = "718")
        {
            var ngaybatdau = DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var ngayketthuc = DateTime.ParseExact(e, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1);
            ViewBag.Date = ngaybatdau;
            ViewBag.StartDate = ngaybatdau;
            ViewBag.EndDate = ngayketthuc;
            ViewBag.TotalDay = (ngayketthuc - ngaybatdau).TotalDays;
            ViewBag.JsonData = JsonConvert.SerializeObject(_ccService.DanhSach_TongCong(makp, manv, s, e));

            return PartialView("_Table_CongChuan");
        }

        [HttpPost]
        public JsonResult Push_DuyetNhanhGiaiTrinh(string dateString)
        {
            string auth = "HCNS_User";

            if (__roles.Contains(StaticParams.HCNS_Admin)) auth = "HCNS_Admin";
            if (__roles.Contains(StaticParams.HCNS_Manager)) auth = "HCNS_Manager";

            return Json(new { result = _ccService.Update_DuyetNhanhGiaiTrinh(auth, dateString) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Push_HuyDuyetNhanhGiaiTrinh(string dateString)
        {
            string auth = "HCNS_User";

            if (__roles.Contains(StaticParams.HCNS_Admin)) auth = "HCNS_Admin";
            if (__roles.Contains(StaticParams.HCNS_Manager)) auth = "HCNS_Manager";

            return Json(new { result = _ccService.Update_HuyDuyetNhanhGiaiTrinh(auth, dateString) }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_DanhSachCa, StaticParams.IT)]
        public ActionResult DanhSachCa()
        {
            return View();
        }

        public JsonResult Data_DanhSachCa(Shifts obj)
        {
            string errorMessage = "";

            return Json(new { data = _ccService.DS_Ca(obj, ref errorMessage) }, JsonRequestBehavior.AllowGet);
        }


        #region XỬ LÝ KHAI BÁO VẮNG

        public PartialViewResult DanhSachKhaiBaoVangChuaDuyet()
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            var userAuth = "";

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
                userAuth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                userAuth = "HCNS_Manager";
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            }
            else
            {
                userAuth = "HCNS_User";
            }
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            ViewBag.UserInfo = userInfo;
            ViewBag.userAuth = userAuth;

            return PartialView("_DanhSachKhaiBaoVangChuaDuyet");
        }

        public JsonResult Data_DanhSachKhaiBaoVangChuaDuyet(string from = "", string to = "", int kp = 0, int userid = 0)
        {
            return Json(new { data = _ccService.KhaiBaoVang_DanhSachChoDuyet(new AbsentR() { From = from, To = to, MaKP = kp, UserEnrollNumber = userid }) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Export_DanhSachKhaiBaoVangChuaDuyet(string from = "", string to = "", int kp = 0, int userid = 0)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Mã nhân viên");
            dataTable.Columns.Add("Họ và tên");
            dataTable.Columns.Add("Khoa/Phòng");
            dataTable.Columns.Add("Ngày nghỉ");
            dataTable.Columns.Add("Lý do");
            dataTable.Columns.Add("Ghi chú");

            var data = _ccService.KhaiBaoVang_DanhSachChoDuyet(new AbsentR() { From = from, To = to, MaKP = kp, UserEnrollNumber = userid });

            foreach (var item in data)
            {
                var dataRow = dataTable.NewRow();

                dataRow[0] = item.UserFullCode;
                dataRow[1] = item.UserFullName;
                dataRow[2] = item.KhoaPhong;
                dataRow[3] = item.Date;
                dataRow[4] = item.AbsentDescription;
                dataRow[5] = item.Reason;

                dataTable.Rows.Add(dataRow);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Đề xuất chưa duyệt");
                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns("C:AM").Width = 20;
                //ws.Row(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Rows(6, 6 + dataTable.Rows.Count).Height = 20;

                ws.Cell(3, 1).InsertTable(dataTable);

                //INSERT Tiêu đề vào dòng đầu tiên và merge nó.
                ws.Column(2).AdjustToContents();
                ws.Range("A1:L1").Merge();
                ws.Cell("A1").Value = "BẢNG ĐỀ XUẤT CHƯA DUYỆT";
                ws.Cell("A1").Style.Font.FontSize = 20;
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //Thêm dòng chữ ký sau khi dữ liệu được in ra
                ws.Cell("B" + (7 + dataTable.Rows.Count).ToString()).Value = "Ban Giám đốc";
                ws.Cell("D" + (7 + dataTable.Rows.Count).ToString()).Value = "Phòng HCNS";
                ws.Cell("F" + (7 + dataTable.Rows.Count).ToString()).Value = "Phòng QLĐD";
                ws.Cell("G" + (7 + dataTable.Rows.Count).ToString()).Value = "Xác nhận của trưởng khoa";
                ws.Cell("I" + (7 + dataTable.Rows.Count).ToString()).Value = "Người lập phiếu";
                ws.Cell("F" + (8 + dataTable.Rows.Count).ToString()).Value = "(Dành cho khối ĐD)";
                ws.Row(7 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var range1 = ws.Range(ws.Cell(3, 1).Address, ws.Cell(3 + dataTable.Rows.Count, 1).Address);
                var range2 = ws.Range(ws.Cell(3, 1).Address, ws.Cell(3 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                range2.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                range2.Style.Border.InsideBorderColor = XLColor.Black;
                range2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                range2.Style.Border.OutsideBorderColor = XLColor.Black; ;

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=cc_" + DateTime.UtcNow.AddHours(7).ToString("yyMMddHHmmss") + ".xlsx");

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
        public JsonResult Push_XoaKhaiBaoVangChoDuyet(int userId, string date)
        {
            string error = "";
            return Json(new { result = _ccService.XoaKhaiBaoVangR(userId, date, ref error), error }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Push_DanhSachKhaiBaoVangChuaDuyet(string[] ids, string[] dates, int type = 0)
        {
            int status = 0;

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                if (type != 0)
                    status = 2;
                else
                    status = -2;
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                if (type != 0)
                    status = 1;
                else
                    status = -1;
            }

            return Json(new { result = _ccService.KhaiBaoVang_XuLyDuyet(ids, dates, status) }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Add_KhaiBaoVang()
        {
            return PartialView("_Add_KhaiBaoVang");
        }

        #endregion XỬ LÝ KHAI BÁO VẮNG

        #region CHI TIẾT CÔNG NHÂN VIÊN

        public PartialViewResult Cell_KhaiBaoLoi(int userid = 0, string date = "")
        {
            string userAuth = "HCNS_User";

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                userAuth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                userAuth = "HCNS_Manager";
            }

            ViewBag.DS_DeXuat = _ccService.DS_LyDo_DeXuat();
            ViewBag.UserEnrollNumber = userid;
            ViewBag.Date = date;
            ViewBag.UserAuth = userAuth;

            return PartialView("_Cell_KhaiBaoLoi", _ccService.ThongTinDeXuat(userid, date));
        }

        public PartialViewResult Cell_ThoiGianLamViec(int userid = 0, string date = "", int kp = 0)
        {
            string userAuth = "HCNS_User";

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                userAuth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                userAuth = "HCNS_Manager";
            }
            ViewBag.S1 = _ccService.TGLV_DDL(1);
            ViewBag.S2 = _ccService.TGLV_DDL(2);
            ViewBag.S3 = _ccService.TGLV_DDL(3);
            ViewBag.TTOps = _ccService.TGLV_TTType_Ops(kp);
            ViewBag.LTGOps = _ccService.TGLV_LTGType_Ops(kp);
            ViewBag.UserEnrollNumber = userid;
            ViewBag.Date = date;
            ViewBag.UserAuth = userAuth;
            return PartialView("_Cell_ThoiGianLamViec", _ccService.TGLV_ThongTin(new UserWStats() { UserEnrollNumber = userid, Date = date }));
        }

        public PartialViewResult Cell_KhaiBaoCa(int userid = 0, string date = "")
        {
            ViewBag.DanhSachCa = _ccService.DanhSachCa_Dropdownlist(userid);
            ViewBag.UserEnrollNumber = userid;
            ViewBag.Date = date;

            if (__roles.Contains(StaticParams.HCNS_Admin) || __roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.UserAuth = "HCNS_Manager";
                return PartialView("_Cell_KhaiBaoCa", _ccService.DanhSachCa_ThongTin(new UserTempShift() { UserEnrollNumber = userid, Date = date }));
            }
            ViewBag.UserAuth = "HCNS_User";
            return PartialView("_Cell_KhaiBaoCa", _ccService.DanhSachCa_ThongTinR(new UserTempShift() { UserEnrollNumber = userid, Date = date }));
        }

        public PartialViewResult Cell_KhaiBaoVang(int userid = 0, string date = "")
        {
            ViewBag.UserEnrollNumber = userid;
            ViewBag.Date = date;
            ViewBag.DS_AbsentSymbol = _kbService.DanhSachKhaiBao();
            var model = _ccService.KhaiBaoVang_ThongTinR(userid, date);
            model.AbsentInfo = _kbService.AbsentInfo(new Absent() { UserEnrollNumber = userid, TimeDate = date });

            return PartialView("_Cell_KhaiBaoVang", model);
        }

        public PartialViewResult Cell_KhaiBaoAn(int userid = 0, string date = "")
        {
            ViewBag.UserEnrollNumber = userid;
            ViewBag.Date = date;
            var model = _ccService.DS_KhaiBaoAn(userid, date);

            return PartialView("_Cell_KhaiBaoAn", model);
        }
        [HttpPost]
        public JsonResult XacNhanHuy(KhaiBaoAn obj)
        {
            string _errorMessage = "";
            return Json(new { result = _ccService.XacNhanHuy(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult XacNhan(KhaiBaoAn obj)
        {
            string _errorMessage = "";
            return Json(new { result = _ccService.XacNhan(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_Cell_KhaiBaoLoi(UserWTRequests obj)
        {
            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                obj.Status = 2;
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                obj.Status = 1;
            }
            else
            {
                obj.Status = 0;
            }
            return Json(new { result = _ccService.Update_Lydo_DeXuat_User(obj) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_Cell_ThoiGianLamViec(UserWStats obj)
        {
            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                obj.W1Stats = (string.IsNullOrEmpty(obj.WStats1) ? 0 : 2);
                obj.W2Stats = (string.IsNullOrEmpty(obj.WStats2) ? 0 : 2);
                obj.W3Stats = (string.IsNullOrEmpty(obj.WStats3) ? 0 : 2);
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                obj.W1Stats = (string.IsNullOrEmpty(obj.WStats1) ? 0 : 1);
                obj.W2Stats = (string.IsNullOrEmpty(obj.WStats2) ? 0 : 1);
                obj.W3Stats = (string.IsNullOrEmpty(obj.WStats3) ? 0 : 1);
            }
            else
            {
                obj.W1Stats = 0;
                obj.W2Stats = 0;
                obj.W3Stats = 0;
            }

            obj.DateString = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");

            return Json(new { result = _ccService.Update_TGLV(obj) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_Cell_KhaiBaoCa(UserTempShift obj)
        {
            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                obj.Status = 2;
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                obj.Status = 1;
            }
            else
            {
                obj.Status = 0;
            }
            obj.DateString = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");

            if (obj.Status > 0)
            {
                return Json(new { result = _ccService.DanhSachCa_AddCaMoi(obj) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = _ccService.DanhSachCa_AddRequestCaMoi(obj) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_Cell_KhaiBaoVang(AbsentR obj)
        {
            var errorMessage = "";

            var ngaydachon = DateTime.ParseExact(obj.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + ",";

            if (_ccService.KhaiBaoVang_KiemTraDeXuat(obj, ngaydachon, ref errorMessage))
            {
                return Json(new { result = false, errorMessage }, JsonRequestBehavior.AllowGet);
            }
            else if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                obj.Status = 2;

                return Json(new
                {
                    result = _kbService.ThemMoiKhaiBao(new List<Absent>()
                    {
                        new Absent()
                        {
                            UserEnrollNumber = obj.UserEnrollNumber,
                            TimeDate = obj.Date,
                            AbsentCode = obj.AbsentCode,
                            Lydo = obj.Reason,
                            Workingday = 1,
                            WorkingTime = 8,
                            UserAccount = System.Web.HttpContext.Current.User.Identity.Name,
                            ForType = obj.Status
                        }
                    },
                    new Absent()
                    {
                        UserEnrollNumber = obj.UserEnrollNumber,
                        AbsentCode = obj.AbsentCode,
                        CheckDateString = ngaydachon,
                        ForType = obj.Status
                    }, ref errorMessage),
                    errorMessage
                });
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                obj.Status = 1;

                string error = "";

                string[] arrayPers = new string[] { "A03", "A07", "A09", "A13", "A14", "A15", "A29", "A30", "A31", "A32", "A33", "A34", "A35", "A37" };

                //Không phải admin thì insert vào bảng đề xuất, admin duyệt mới vào bảng chính. A03, A09, A32
                if (arrayPers.Contains(obj.AbsentCode))
                //
                {
                    if (_ccService.KhaiBaoVang_KiemTraDeXuat(obj.UserEnrollNumber, obj.AbsentCode, ngaydachon, ref error))
                    {
                        return Json(new
                        {
                            result = false,
                            message = error
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var lsObjR = new List<AbsentR>();
                        lsObjR.Add(obj);

                        var result = _ccService.ThemMoiKhaiBaoR(lsObjR, ref error);

                        return Json(new
                        {
                            result = result,
                            message = error
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        result = _kbService.ThemMoiKhaiBao(new List<Absent>()
                    {
                        new Absent()
                        {
                            UserEnrollNumber = obj.UserEnrollNumber,
                            TimeDate = obj.Date,
                            AbsentCode = obj.AbsentCode,
                            Lydo = obj.Reason,
                            Workingday = 1,
                            WorkingTime = 8,
                            UserAccount = System.Web.HttpContext.Current.User.Identity.Name,
                            ForType = obj.Status
                        }
                    },
                    new Absent()
                    {
                        UserEnrollNumber = obj.UserEnrollNumber,
                        AbsentCode = obj.AbsentCode,
                        CheckDateString = ngaydachon,
                        ForType = obj.Status
                    }, ref errorMessage),
                        errorMessage
                    });
                }
            }
            else
            {
                obj.Status = 0;
                //obj.ForType = 0;
                return Json(new { result = _ccService.ThemMoiKhaiBaoR(obj) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Push_XoaDeXuatLoi(int userid, string date)
        {
            string message = "";

            return Json(new { result = _ccService.Delete_DeXuatLoi(userid, date, out message), message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Push_XoaDeXuatCa(int userid, string date)
        {
            string message = "";
            string auth = "";

            if (__roles.Contains(StaticParams.HCNS_Admin) || __roles.Contains(StaticParams.HCNS_Manager))
            {
                auth = "HCNS_Manager";
            }
            else
            {
                auth = "HCNS_User";
            }

            return Json(new { result = _ccService.Delete_DeXuatCa(auth, userid, date, out message), message = message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Push_XoaDeXuatKBVang(int userid, string date)
        {
            string message = "";

            return Json(new { result = _ccService.Delete_DeXuatKBVang(userid, date, out message), message = message }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult QuanLyGiaiTrinh_Duyet(string p, int userid, string date)
        {
            ViewBag.Auth = p;
            return PartialView("_QuanLyGiaiTrinh_Duyet", _ccService.GiaiTrinhSuaLoi_ThongTin(userid, date));
        }

        [HttpPost]
        public JsonResult QLGT_duyet(string p = "", int userid = 0, string date = "", string note = "", string a = "")
        {
            int i = 0;

            if (a == "no")
            {
                if (p == "HCNS_Manager")
                    i = -1;
                if (p == "HCNS_Admin")
                    i = -2;
            }
            else
            {
                if (p == "HCNS_Manager")
                    i = 1;
                if (p == "HCNS_Admin")
                    i = 2;
            }
            return Json(new { result = _ccService.DuyetGiaiTrinh(p, userid, date, note, i) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QLGT_Huyduyet1(string p = "", int userid = 0, string date = "", string note = "")
        {
            int i = 0;

            if (p == "HCNS_Manager")
                i = 0;
            if (p == "HCNS_Admin")
                i = 1;

            return Json(new { result = _ccService.HuyDuyetGiaiTrinh(p, userid, date, note, i) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QLGT_Huyduyet(int user = 0, string date = "", string col = "")
        {
            return Json(new { result = _ccService.HuyDuyetGiaiTrinh(user, date, col) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QLGT_XoaDeXuat(int user = 0, string date = "", string col = "")
        {
            return Json(new { result = _ccService.XoaGiaiTrinh(user, date, col) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QLGT_DuyetAll(string[] vals)
        {
            if (vals.Length == 0)
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);

            return Json(new { result = _ccService.Update_DuyetGiaiTrinh(vals) }, JsonRequestBehavior.AllowGet);
        }

        #endregion CHI TIẾT CÔNG NHÂN VIÊN

        #region KHAI BÁO THỜI GIAN LÀM VIỆC

        [CustomAuthorize(StaticParams.HCNS_KhaiBaoLamThemGio, StaticParams.IT)]
        public ActionResult ThoiGianLamViec()
        {
            #region Kiểm tra hạn khai báo

            ViewBag.Locked = locked;
            ViewBag.LockMessage = lockedMessage;

            #endregion Kiểm tra hạn khai báo

            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            ViewBag.UserInfo = userInfo;

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong();
                ViewBag.Auth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                ViewBag.Auth = "HCNS_Manager";
            }

            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            //ViewBag.Departments = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            return View();
        }

        public JsonResult Data_ThoiGianLamViec(string s = "", string e = "", int kp = -1, int manv = -1, int ot = -1, int tt = -1)
        {
            if (kp == 0)
                kp = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = System.Web.HttpContext.Current.User.Identity.Name }).KhoaPhong;

            var fromDateFormat = DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var toDateFormat = DateTime.ParseExact(e, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            return Json(new { data = _ccService.TGLV_View(fromDateFormat, toDateFormat, manv, kp, ot, tt) }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_Add_ThoiGianLamViec, StaticParams.IT)]
        public PartialViewResult Add_ThoiGianLamViec(string user = "", string s = "", string e = "", int kp = 0)
        {
            if (!string.IsNullOrEmpty(user))
                ViewBag.User = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { UserEnrollNumber = int.Parse(user) });
            else
                ViewBag.User = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.UserAuth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.UserAuth = "HCNS_Manager";
            }
            else
            {
                ViewBag.UserAuth = "HCNS_User";
            }

            ViewBag.From = DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.To = DateTime.ParseExact(e, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ViewBag.S1 = _ccService.TGLV_DDL(1);
            ViewBag.S2 = _ccService.TGLV_DDL(2);
            ViewBag.S3 = _ccService.TGLV_DDL(3);
            ViewBag.TTOps = _ccService.TGLV_TTType_Ops(kp);
            ViewBag.LTGOps = _ccService.TGLV_LTGType_Ops(kp);
            ViewBag.KhoaPhong = kp;

            return PartialView("_Add_ThoiGianLamViec", new UserWStats());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_Update_ThoiGianLamViec(UserWStats obj, FormCollection form)
        {
            obj.DateString = form["chk_date"].ToString();

            var dateDuplicate = _ccService.TGLV_KiemTraTrung(obj);

            if (!string.IsNullOrEmpty(dateDuplicate) && !obj.Confirmed)
            {
                return Json(new { result = false, message = dateDuplicate }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (__roles.Contains(StaticParams.HCNS_Admin))
                {
                    obj.W1Stats = (string.IsNullOrEmpty(obj.WStats1) ? 0 : 2);
                    obj.W2Stats = (string.IsNullOrEmpty(obj.WStats2) ? 0 : 2);
                    obj.W3Stats = (string.IsNullOrEmpty(obj.WStats3) ? 0 : 2);
                }
                else if (__roles.Contains(StaticParams.HCNS_Manager))
                {
                    obj.W1Stats = (string.IsNullOrEmpty(obj.WStats1) ? 0 : 1);
                    obj.W2Stats = (string.IsNullOrEmpty(obj.WStats2) ? 0 : 1);
                    obj.W3Stats = (string.IsNullOrEmpty(obj.WStats3) ? 0 : 1);
                }

                var __result = _ccService.Update_TGLV(obj);

                return Json(new { result = __result }, JsonRequestBehavior.AllowGet);
            }
        }

        [CustomAuthorize(StaticParams.HCNS_Remove_ThoiGianLamViec, StaticParams.IT)]
        [HttpPost]
        public JsonResult Remove_ThoiGianLamViec(int user = 0, string date = "", string action = "", string col = "", string val = "")
        {
            return Json(new { result = _ccService.Clear_TGLV(new UserWStats() { UserEnrollNumber = user, Date = date }) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Export_ThoiGianLamViec()
        {
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();

            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles == null)
            {
                return RedirectToAction("Logout", "User");
            }
            else if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong();
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            }
            else if (__roles.Contains(StaticParams.HCNS_User))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            }
            return PartialView("_Export_ThoiGianLamViec");
        }

        #endregion KHAI BÁO THỜI GIAN LÀM VIỆC

        #region KHAI BÁO CA

        [CustomAuthorize(StaticParams.HCNS_Add_KhaiBaoLichTrinh, StaticParams.IT)]
        public PartialViewResult Add_KhaiBaoLichTrinh(string users)
        {
            ViewBag.Users = users;
            ViewBag.DanhSachLichTrinh = _ccService.LichTrinh_Dropdownlist();
            return PartialView("_Add_KhaiBaoLichTrinh", new UserInfo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_Add_KhaiBaoLichTrinh(string users, int schID)
        {
            var splitUsers = users.Split(',');

            List<UserInfo> lst = new List<UserInfo>();

            for (int i = 0; i < splitUsers.Length; i++)
            {
                lst.Add(new UserInfo() { UserEnrollNumber = int.Parse(splitUsers[i].ToString()), SchID = schID });
            }
            return Json(new { result = _ccService.LichTrinh_CapNhat(lst) }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_Xoa_KhaiBaoCa_Phu, StaticParams.IT)]
        [HttpPost]
        public JsonResult Xoa_KhaiBaoCa_Phu(string user, string date)
        {
            return Json(new { result = _ccService.Xoa_KhaiBaoCa_Phu(user, date) }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_KhaiBaoCaLamViec, StaticParams.IT)]
        public ActionResult KhaiBaoCa()
        {
            #region Kiểm tra hạn khai báo

            ViewBag.Locked = locked;
            ViewBag.LockMessage = lockedMessage;

            #endregion Kiểm tra hạn khai báo

            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            var auth = "HCNS_User";

            if (__roles == null)
            {
                return RedirectToAction("Logout", "User");
            }
            else if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong();
                auth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_DieuDuong))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong();
                auth = "HCNS_DieuDuong";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                auth = "HCNS_Manager";
            }
            

            ViewBag.Auth = auth;
            ViewBag.UserInfo = userInfo;
            ViewBag.UserType = _ccService.DanhSachLoaiNhanVien();
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();

            return View();
        }

        public PartialViewResult Table_KhaiBaoCa(string s = "", string e = "", int makp = 0, int manv = 0, int type = 0, string auth = "")
        {
            #region Kiểm tra hạn khai báo

            ViewBag.Locked = locked;
            ViewBag.LockMessage = lockedMessage;

            #endregion Kiểm tra hạn khai báo

            DateTime fromDateFormat = DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDateFormat = DateTime.ParseExact(e, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ViewBag.StartDate = fromDateFormat;
            ViewBag.EndDate = toDateFormat;
            ViewBag.TotalDay = (toDateFormat - fromDateFormat).TotalDays;
            ViewBag.Auth = auth;

            int qldd = __roles.Contains(StaticParams.HCNS_DieuDuong) ? 1 : 0;

            ViewBag.Data = _ccService.DanhSachCa_View(fromDateFormat, toDateFormat, manv, makp, type, qldd);

            return PartialView("_Table_KhaiBaoCa");
        }

        [CustomAuthorize(StaticParams.HCNS_Add_KhaiBaoCa, StaticParams.IT)]
        public PartialViewResult Add_KhaiBaoCa(int user = 0, string s = "", string e = "", int kp = 0)
        {
            if (user != 0)
            {
                ViewBag.User = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { UserEnrollNumber = user });
                ViewBag.RequestInfo = _ccService.DanhSachCa_ThongTinDaKhaiBao(new UserTempShift() { UserEnrollNumber = user, DateFrom = s, DateTo = e });
            }
            ViewBag.From = DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.To = DateTime.ParseExact(e, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.KhoaPhong = kp;

            return PartialView("_Add_KhaiBaoCa", new UserTempShift());
        }

        [CustomAuthorize(StaticParams.HCNS_Add_KhaiBaoNhieuCa, StaticParams.IT)]
        public PartialViewResult Add_KhaiBaoNhieuCa(string users = "", string s = "", string e = "")
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            ViewBag.UserChosed = users;
            ViewBag.User = userInfo;
            ViewBag.RequestInfo = _ccService.DanhSachCa_ThongTinDaKhaiBao(new UserTempShift() { UserEnrollNumber = userInfo.UserEnrollNumber, DateFrom = s, DateTo = e });
            ViewBag.From = DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ViewBag.To = DateTime.ParseExact(e, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            return PartialView("_Add_KhaiBaoNhieuCa", new UserTempShift());
        }

        public PartialViewResult Add_KhaiBaoCa_Dropdownlist(int user = 0)
        {
            string auth = "";
            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                auth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                auth = "HCNS_Manager";
            }
            ViewBag.DanhSachCa = _ccService.DanhSachCa_Dropdownlist(user, auth);

            return PartialView("_Add_KhaiBaoCa_Dropdownlist");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_Add_KhaiBaoCa(UserTempShift obj, FormCollection form)
        {
            if (obj.UserEnrollNumber == 0)
            {
                return Json(new { result = false, message = "Mã nhân viên không tồn tại. Vui lòng kiểm tra lại." }, JsonRequestBehavior.AllowGet);
            }

            if (form["chk_date"] == null)
            {
                return Json(new { result = false, message = "Bạn chưa chọn ngày khai báo. Vui lòng kiểm tra lại." }, JsonRequestBehavior.AllowGet);
            }
            obj.DateString = form["chk_date"].ToString();

            var dateDuplicate = _ccService.DanhSachCa_KiemTraTrung(obj);
            var dateDuplicateR = _ccService.DanhSachCa_KiemTraTrungR(obj);

            if (!string.IsNullOrEmpty(dateDuplicate) && !obj.Confirmed)
            {
                return Json(new { result = false, message = "Các ngày đã được khai báo\n" + dateDuplicate + "\nXác nhận cập nhật?" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int __stats1 = 0;

                if (__roles.Contains(StaticParams.HCNS_Admin))
                {
                    __stats1 = 2;
                }
                else if (__roles.Contains(StaticParams.HCNS_Manager))
                {
                    __stats1 = 1;
                }

                obj.Status = __stats1;
                var __result = false;

                if (obj.Status > 0)
                {
                    if (!string.IsNullOrEmpty(dateDuplicate))
                    {
                        return Json(new { result = false, message = "Các ngày đã được đề xuất\n" + dateDuplicate + "\nVui lòng kiểm tra lại." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        __result = _ccService.DanhSachCa_AddCaMoi(obj);
                    }
                }
                else
                {
                    __result = _ccService.DanhSachCa_AddRequestCaMoi(obj);
                }

                return Json(new { result = __result }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_Add_KhaiBaoNhieuCa(UserTempShift obj, FormCollection form)
        {
            if (form["chk_date"] == null)
            {
                return Json(new { result = false, message = "Bạn chưa chọn ngày khai báo. Vui lòng kiểm tra lại." }, JsonRequestBehavior.AllowGet);
            }
            obj.DateString = form["chk_date"].ToString();

            //var dateDuplicate = _ccService.DanhSachCa_KiemTraTrung(obj);
            //var dateDuplicateR = _ccService.DanhSachCa_KiemTraTrungR(obj);

            int __stats1 = 0;

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                __stats1 = 2;
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                __stats1 = 1;
            }

            obj.Status = __stats1;
            var __result = false;
            __result = _ccService.DanhSachCa_AddNhieuCaMoi(obj);

            return Json(new { result = __result }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_GioiHanCa, StaticParams.IT)]
        public PartialViewResult GioiHanCa(string users = "")
        {
            ViewBag.DanhSachCa = _ccService.DanhSachCa_Checkbox();
            ViewBag.Users = users;
            var userArr = users.Split(',');
            ViewBag.DanhSachCaChecked = _ccService.DanhSachCa_CheckboxWithDefaultValueUser(int.Parse(userArr[0].ToString()));

            return PartialView("_GioiHanCa");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_GioiHanCa(UserInfExt obj, FormCollection form)
        {
            //if (obj.UserEnrollNumber == 0)
            //{
            //    return Json(new { result = false, message = "Mã nhân viên không tồn tại. Vui lòng kiểm tra lại." }, JsonRequestBehavior.AllowGet);
            //}

            if (obj.UserEnrollNumber > 0)
            {
                obj.Users += obj.UserEnrollNumber;
            }
            return Json(new { result = _ccService.Update_GioiHanCa(obj.Users, form["CaPhu"].ToString()) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Export_khaibaoca(string s = "", string e = "", int kp = 0, int manv = 0, int type = 0)
        {
            DateTime fromDate = DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(e, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            int qldd = __roles.Contains(StaticParams.HCNS_DieuDuong) ? 1 : 0;
            DataTable dt = _ccService.DanhSachCa_View(fromDate, toDate, manv, kp, type, qldd);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Danh sách");
                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns("C:AM").Width = 20;
                ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Rows(1, 1 + dt.Rows.Count).Height = 20;
                ws.Cell(1, 1).InsertTable(dt);
                ws.Column(2).AdjustToContents();

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + DateTime.UtcNow.AddHours(7).ToString("yyMMddHHmmss") + "_khaibaoca.xlsx");

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

        [CustomAuthorize(StaticParams.HCNS_DanhSachCaChuaDuyet, StaticParams.IT)]
        public PartialViewResult DanhSachCaChuaDuyet(int userid = 0)
        {
            ViewBag.UserEnrollNumber = userid;

            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            var auth = "HCNS_User";

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
                auth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                auth = "HCNS_Manager";
            }
            else if (__roles.Contains(StaticParams.HCNS_User))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            }
            ViewBag.UserInfo = userInfo;
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            return PartialView("_DanhSachCaChuaDuyet");
        }

        public JsonResult Data_DanhSachCaChuaDuyet(string from = "", string to = "", int kp = 0, string userid = "")
        {
            return Json(new { data = _ccService.DanhSachCa_ChuaDuyet(from, to, kp, userid) }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult DanhSachTGLVChuaDuyet()
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });
            var auth = "HCNS_User";
            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
                auth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                auth = "HCNS_Manager";
            }
            else if (__roles.Contains(StaticParams.HCNS_User))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            }
            ViewBag.UserAuth = auth;
            ViewBag.UserInfo = userInfo;
            return PartialView("_DanhSachTGLVChuaDuyet");
        }

        public JsonResult Data_DanhSachTGLVChuaDuyet(string from = "", string to = "", int kp = 0, int userid = 0)
        {
            return Json(new { data = _ccService.DanhSachTGLV_ChuaDuyet(from, to, kp, userid) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Push_DanhSachCaChuaDuyet(string[] ids, string[] dates)
        {
            int status = 0;

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                status = 2;
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                status = 1;
            }

            return Json(new { result = _ccService.DanhSachCa_XuLyDuyetCa(ids, dates, status) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Push_HuyDanhSachCaChuaDuyet(string[] ids, string[] dates)
        {
            int status = 0;

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                status = -2;
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                status = -1;
            }

            return Json(new { result = _ccService.DanhSachCa_XuLyHuyCa(ids, dates, status) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Push_DanhSachTGLVChuaDuyet(string[] ids_1, string[] dates_1, string[] ids_2, string[] dates_2, string[] ids_3, string[] dates_3)
        {
            int status = 0;

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                status = 2;
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                status = 1;
            }

            return Json(new { result = _ccService.DanhSachTGLV_XuLyDuyetTGLV(ids_1, dates_1, ids_2, dates_2, ids_3, dates_3, status) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Xoa_TGLV(int userid, string date, string col, string action, string val)
        {
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion KHAI BÁO CA

        #region XEM CHI TIẾT CÔNG THEO NHÂN VIÊN

        [HttpPost]
        public JsonResult GetUser(string prefix = "", int user = 0, int kp = 0)
        {
            //string jsonString = new JavaScriptSerializer().Serialize(_ccService.TimNhanVienTheoKhoa(prefix, user, kp));
            string jsonString = "";

            if (__roles.Contains(StaticParams.HCNS_Admin) && (kp == 0 || kp == -1))
            //if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                jsonString = new JavaScriptSerializer().Serialize(_kbService.SearchUsersAll(prefix));
            }
            else if ((__roles.Contains(StaticParams.HCNS_Manager) && kp > 0) || (__roles.Contains(StaticParams.HCNS_Admin) && kp > 0))
            //else if (__roles.Contains(StaticParams.HCNS_Manager) && kp > 0)
            {
                //jsonString = new JavaScriptSerializer().Serialize(_ccService.TimNhanVienTheoKhoa(prefix, kp.ToString()));
                jsonString = new JavaScriptSerializer().Serialize(_ccService.TimNhanVienTheoKhoa(prefix, user, kp));
            }
            else
            {
                jsonString = new JavaScriptSerializer().Serialize(_ccService.TimNhanVienTheoKhoa(prefix, user));
            }
            return Json(jsonString, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_XemChiTietCongTheoNV, StaticParams.IT)]
        public ActionResult XemCong()
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.UserPermission = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.UserPermission = "HCNS_Manager";
            }
            else
            {
                ViewBag.UserPermission = "HCNS_User";
            }
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            ViewBag.UserInfo = userInfo;
            return View();
        }

        public PartialViewResult Table_XemCong(string from, string to, int user, int? kp)
        {
            ViewBag.TimeQuyDinh = _configService.NgayQuyDinhKhaiBaoCong().NumberVal;
            DiaDiemAn a = new DiaDiemAn();
            a = _ccService.Get_NoiAn(user);
            if (a != null)
                ViewBag.NoiAn = a.MOTA;
            else
                ViewBag.NoiAn = "Chưa đăng ký nơi ăn";
            //string curDate = System.DateTime.UtcNow.AddHours(7).ToString("yyyy-MM-dd");
            string curDate = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            var phep = _ccService.Get_Phep(curDate, user);
            ViewBag.Phep = phep;
            return PartialView("_Table_XemCong", _ccService.DS_Cong_TheoTK(new XemCong() { UserEnrollNumber = user, GioVao = from, GioRa = to, KhoaPhong = kp ?? 0 }));
        }

        public PartialViewResult XemCong_DeXuat(int user = 0, string date = "")
        {
            ViewBag.DS_DeXuat = _ccService.DS_LyDo_DeXuat();
            //ViewBag.DaDuyet = _ccService.KiemTra_DuyetDeXuat(new UserWTRequests() { UserEnrollNumber = user, Date = date });
            return PartialView("_XemCong_DeXuat", _ccService.ThongTinDeXuat(user, date));
        }

        [HttpPost]
        public JsonResult Push_XemCong_DeXuat(UserWTRequests obj)
        {
            return Json(new { result = _ccService.Update_Lydo_DeXuat_User(obj) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Xoa_DeXuat(int userid, string date)
        {
            string _message = "";

            return Json(new { result = _ccService.Delete_DeXuatLoi(userid, date, out _message), message = _message }, JsonRequestBehavior.AllowGet);
        }

        #endregion XEM CHI TIẾT CÔNG THEO NHÂN VIÊN

        #region QUẢN LÝ CÔNG

        [CustomAuthorize(StaticParams.HCNS_TongHopCongTheoKP, StaticParams.IT)]
        public ActionResult QuanLyCong()
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles.Contains(StaticParams.HCNS_Admin) || __roles.Contains(StaticParams.HCNS_DieuDuong))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong();
                ViewBag.UserAuth = StaticParams.HCNS_Admin;
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                ViewBag.UserAuth = StaticParams.HCNS_Manager;
            }
            else if (__roles.Contains(StaticParams.HCNS_User))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);

                ViewBag.UserAuth = StaticParams.HCNS_User;
            }

            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            ViewBag.UserInfo = userInfo;
            return View();
        }

        public PartialViewResult Table_QuanLyCong(string s = "", string e = "", int makp = -1, int manv = 0)
        {
            if (makp == 0)
                makp = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user }).KhoaPhong;

            DateTime fromDateFormat, toDateFormat;

            //Kiểm tra ngày tháng trước khi điền
            if (String.IsNullOrEmpty(s) && String.IsNullOrEmpty(e))
            {
                fromDateFormat = DateTime.UtcNow.AddHours(7).AddMonths(-1);
                toDateFormat = DateTime.UtcNow.AddHours(7);
            }
            else
            {
                fromDateFormat = DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                toDateFormat = DateTime.ParseExact(e, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            ViewBag.StartDate = fromDateFormat;
            ViewBag.EndDate = toDateFormat;
            ViewBag.TotalDay = (toDateFormat - fromDateFormat).TotalDays;
            if (makp == 0)
            {
                ViewBag.Data = new DataTable();
            }
            else
            {
                int is_qldd = __roles.Contains(StaticParams.HCNS_DieuDuong) ? 1 : 0;

                ViewBag.Data = _ccService.QLCong_View(fromDateFormat, toDateFormat, manv, makp, is_qldd);
            }

            return PartialView("_Table_QuanLyCong");
        }

        public PartialViewResult QuanLyCong_ChiTiet(int user, string s, string e)
        {
            var fromDateFormat = DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var toDateFormat = DateTime.ParseExact(e, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ViewBag.FROM = s;
            ViewBag.TO = e;

            return PartialView("_QuanLyCong_ChiTiet", _ccService.QLCong_Detail(fromDateFormat, toDateFormat, user));
        }

        public PartialViewResult QuanLyCong_ChiTiet_Cell(int user, string d)
        {
            #region Kiểm tra hạn khai báo

            ViewBag.Locked = locked;
            ViewBag.LockMessage = lockedMessage;

            #endregion Kiểm tra hạn khai báo

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.UserAuth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.UserAuth = "HCNS_Manager";
            }
            else
            {
                ViewBag.UserAuth = "HCNS_User";
            }

            var dateFormat = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ViewBag.TimeQuyDinh = _configService.NgayQuyDinhKhaiBaoCong().NumberVal;
            ViewBag.USER = user;
            ViewBag.DATE = d;
            return PartialView("_QuanLyCong_ChiTiet_Cell", _ccService.QLCong_Detail_Cell(dateFormat, user));
        }

        [CustomAuthorize(StaticParams.HCNS_QuanLyGiaiTrinh, StaticParams.IT)]
        public ActionResult QuanLyGiaiTrinh()
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            ViewBag.UserInfo = userInfo;

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
                ViewBag.UserAuth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                ViewBag.UserAuth = "HCNS_Manager";
            }
            //ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            return View();
        }

        public PartialViewResult Table_QuanLyGiaiTrinh(string auth = "", string from = "", string to = "", int fix = 0, string kp = "", string user = "", int status = -1)
        {
            DateTime fromFormat = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toFormat = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (auth == "HCNS_Admin")
            {
                kp = kp == "" ? "-1" : kp;
            }
            DataTable data = _ccService.DanhSachDeXuat(fromFormat, toFormat, kp, fix, user, status);

            ViewBag.Permission = auth;

            return PartialView("_Table_QuanLyGiaiTrinh", data);
        }

        #endregion QUẢN LÝ CÔNG

        #region TỔNG HỢP CÔNG

        [CustomAuthorize(StaticParams.HCNS_TongHopCong, StaticParams.IT)]
        public ActionResult TongHopCong()
        {
            string auth_khoasolieu = "";
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });
            ViewBag.Departments = _ccService.DanhSachKhoaPhong();
            ViewBag.UserInfo = userInfo;
            ViewBag.KhoaSoLieuVal = _ccService.KhoaSoLieu_TrangThai();
            var auth = "HCNS_User";

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong();
                auth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.Departments = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                auth = "HCNS_Manager";
            }
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            ViewBag.UserAuth = auth;
            ViewBag.LoaiNV = _ccService.DSLoaiNhanVien();
            if (__roles.Contains(StaticParams.HCNS_KiemTraKhoaSoLieu))
            {
                auth_khoasolieu = StaticParams.HCNS_KiemTraKhoaSoLieu;
            }
            ViewBag.KhoaSoLieu = auth_khoasolieu;
            return View();
        }
        public PartialViewResult Cell_TongHopCong_Admin(HCNS_NhanVien view)
        {
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            ViewBag.LoaiNV = _ccService.DSLoaiNhanVien();

            return PartialView("_Cell_TongHopCong_Admin", view);
        }
        public PartialViewResult Cell_TongHopCong_QL(HCNS_NhanVien view)
        {
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            ViewBag.LoaiNV = _ccService.DSLoaiNhanVien();
            return PartialView("_Cell_TongHopCong_QL", view);
        }

        public JsonResult TongHopCong_View(string from = "", string to = "", int user = -1, int kp = -1, int loainv = -1)
        {
            var jsonResult = Json(new { data = _ccService.TongHopCong_View(from, to, user, kp, loainv) }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public PartialViewResult Table_TongHopCong(string from = "", string to = "", int kp = 0, int userid = 0, int loainv = -1)
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });
            return PartialView("_Table_TongHopCong", _ccService.TongHopCong_View(from, to, userid, userInfo.KhoaPhong, loainv));
            //return PartialView("_Table_TongHopCong");
        }

        [HttpPost]
        public JsonResult TongHopCong_CapNhatChiSo(int userid, string date, string column, string val)
        {
            var error = "";

            return Json(new { result = _ccService.TongHopCong_CapNhatChiSo(userid, date, column, val, ref error), errorMessage = error }, JsonRequestBehavior.AllowGet);
        }
        #endregion TỔNG HỢP CÔNG

        #region ĐĂNG KÝ QUẢN LÝ CÔNG CHO KHOA PHÒNG

        public ActionResult DKQLKP()
        {
            return View();
        }

        #endregion ĐĂNG KÝ QUẢN LÝ CÔNG CHO KHOA PHÒNG

        #region LÀM THÊM TÍNH TIỀN

        [CustomAuthorize(StaticParams.HCNS_LamThemTinhTien, StaticParams.IT)]
        public ActionResult LamThemTT()
        {
            #region Kiểm tra hạn khai báo

            ViewBag.Locked = locked;
            ViewBag.LockMessage = lockedMessage;

            #endregion Kiểm tra hạn khai báo

            var auth = "HCNS_User";

            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
                auth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                auth = "HCNS_Manager";
            }
            else
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            }
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            ViewBag.UserInfo = userInfo;
            ViewBag.UserAuth = auth;
            return View();
        }

        public JsonResult Data_LamThemTT(string ngaybd = "", string ngaykt = "", int manv = -1, int makp = -1, int duyet = -1)
        {
            OTPayment_Filter oFilter = new OTPayment_Filter()
            {
                NgayBD = DateTime.ParseExact(ngaybd, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                NgayKT = DateTime.ParseExact(ngaykt, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                MaNV = manv,
                MaKP = makp,
                Duyet = duyet
            };

            return Json(new { data = _ccService.LamThemTT_DanhSach(oFilter) }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Add_LamThemTT()
        {
            var auth = "HCNS_User";

            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
                auth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                auth = "HCNS_Manager";
            }
            else
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            }
            ViewBag.UserAuth = auth;
            ViewBag.UserInfo = userInfo;
            ViewBag.DDL_Type = _ccService.LamThemTT_Type();

            return PartialView("_Add_LamThemTT");
        }

        [HttpPost]
        public JsonResult Add_LamThemTT(OTPayment obj)
        {
            var approve = 0;

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                approve = 2;
            }
            if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                approve = 1;
            }

            obj.DaDuyet = approve;

            return Json(new { result = _ccService.LamThemTT_Add(obj) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add_LamThemTT_XacNhan(string chooses, string vals, string ddls)
        {
            ViewBag.Chooses = chooses;
            ViewBag.Vals = vals;
            ViewBag.DDLs = ddls;

            return PartialView("_Add_LamThemTT_XacNhan");
        }

        [HttpPost]
        public JsonResult Push_Add_LamThemTT(string chooses, string vals, string ddls, string lydo)
        {
            var approve = 0;
            string auth = "";

            if (chooses.Length == 0)
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);

            var chooseArr = chooses.Split(',');

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                auth = "HCNS_Admin";
                approve = 2;
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                auth = "HCNS_Manager";
                approve = 1;
            }

            List<OTPayment> lst = new List<OTPayment>();

            for (int i = 0; i < chooseArr.Length; i++)
            {
                var userid = chooseArr[i].ToString().Split('.')[0].ToString();
                var date = chooseArr[i].ToString().Split('.')[1].ToString();
                var timekt = chooseArr[i].ToString().Split('.')[2].ToString();
                string hint = "";
                if (auth == "HCNS_Manager")
                    hint = ddls.Split(',')[i].ToString();
                else
                    hint = chooseArr[i].ToString().Split('.')[3].ToString();
                var phut = vals.Split(',')[i].ToString();

                lst.Add(new OTPayment()
                {
                    UserEnrollNumber = int.Parse(userid),
                    Ngay = DateTime.ParseExact(date, "dd/MM/yy", CultureInfo.InvariantCulture).ToString(),
                    GoiY = hint,
                    DaDuyet = approve,
                    LyDo = lydo,
                    TimeStr = DateTime.ParseExact(date + " " + timekt, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture).ToString(),
                    CreatedTime = DateTime.UtcNow.AddHours(7),
                    UpdatedTime = DateTime.UtcNow.AddHours(7),
                    CreatedBy = __user,
                    UpdatedBy = __user,
                    SoPhutDeXuat = string.IsNullOrEmpty(phut) ? 0 : int.Parse(phut)
                });
            }

            return Json(new { result = _ccService.LamThemTT_Approve(lst, auth) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Push_HuyDuyet_LamThemTT(int manv, string ngay, int status)
        {
            OTPayment obj = new OTPayment()
            {
                UserEnrollNumber = manv,
                Ngay = DateTime.ParseExact(ngay, "dd/MM/yy", CultureInfo.InvariantCulture).ToString(),
                GoiY = status.ToString()
            };

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                return Json(new { result = _ccService.LamThemTT_HuyDuyet(obj) }, JsonRequestBehavior.AllowGet);
            }
            if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                return Json(new { result = _ccService.LamThemTT_Xoa(obj) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Push_Xoa_LamThemTT(int manv, string ngay)
        {
            OTPayment obj = new OTPayment()
            {
                UserEnrollNumber = manv,
                Ngay = DateTime.ParseExact(ngay, "dd/MM/yy", CultureInfo.InvariantCulture).ToString()
            };

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                return Json(new { result = _ccService.LamThemTT_HuyDuyet(obj) }, JsonRequestBehavior.AllowGet);
            }
            if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                return Json(new { result = _ccService.LamThemTT_Xoa(obj) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LamThemTT_GhepTT(string data)
        {
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }

        #endregion LÀM THÊM TÍNH TIỀN

        #region Ghép thành ca

        [HttpPost]
        public JsonResult Push_GhepCa(string vals, string lydo)
        {
            string mess = "";
            var strSplit = vals.Split(',');
            int userid = 0;
            string dateString = "";

            List<string> strJoin = new List<string>();

            for (int i = 0; i < strSplit.Length; i++)
            {
                var str = strSplit[i].ToString();
                userid = int.Parse(str.Split('.')[0].ToString());
                dateString += "'" + str.Split('.')[1].ToString() + "',";
                //strJoin.Add(str.Split('.')[1].ToString());
            }

            GhepCa obj = new GhepCa()
            {
                UserEnrollNumber = userid,
                TimeString = dateString.Substring(0, dateString.Length - 1).ToString()
                //TimeString = string.Join(",", strJoin.ToArray())
            };
            return Json(new { result = _ccService.OnCall_Send(obj, lydo, out mess), message = mess }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_CaKhongXacDinh, StaticParams.IT)]
        public ActionResult CaKhongXacDinh()
        {
            var auth = "HCNS_User";
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
                auth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                auth = "HCNS_Manager";
            }
            else
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            }
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            ViewBag.Auth = auth;
            ViewBag.UserInfo = userInfo;

            return View("CaKhongXacDinh");
        }

        public JsonResult Data_CaKhongXacDinh(string from = "", string to = "", int kp = -1, int user = -1, int hienthi = -1)
        {
            CaKhongXacDinh_Filter filter = new CaKhongXacDinh_Filter()
            {
                StartTime = from,
                EndTime = to,
                KhoaPhong = kp,
                UserEnrollNumber = user,
                HienThi = hienthi
            };

            return Json(new { data = _ccService.CaKhongXacDinh_DanhSach(filter) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Xoa_GhepCa(int user, string date, string maql)
        {
            string _message = "";
            return Json(new { result = _ccService.OnCall_Del(new GhepCa() { UserEnrollNumber = user, TimeString = date, MaQL = maql }, out _message), message = _message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update_DuyetCaKhongXacDinh(string chooses)
        {
            var strsplit = chooses.Split(',');

            List<CaKhongXacDinh> lst = new List<CaKhongXacDinh>();

            for (int i = 0; i < strsplit.Length; i++)
            {
                var dateSplit = strsplit[i].ToString().Split('.');

                var ca = new CaKhongXacDinh()
                {
                    UserEnrollNumber = int.Parse(dateSplit[0].ToString()),
                    TimeString = ("'" + DateTime.ParseExact(dateSplit[1].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.ParseExact(dateSplit[2].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss") + "'")
                };
                lst.Add(ca);
            }
            return Json(new { result = _ccService.CaKhongXacDinh_UpdateApprove(lst) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportExcel_CaKhongXacDinh(string from = "", string to = "", int kp = -1, int user = -1, int duyet = -1)
        {
            CaKhongXacDinh_Filter filter = new CaKhongXacDinh_Filter()
            {
                StartTime = from,
                EndTime = to,
                KhoaPhong = kp,
                UserEnrollNumber = user,
                HienThi = duyet
            };

            var data = _ccService.CaKhongXacDinh_DanhSach(filter);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("STT", typeof(int));
            dataTable.Columns.Add("Mã NV", typeof(string));
            dataTable.Columns.Add("Họ tên", typeof(string));
            dataTable.Columns.Add("Khoa / Phòng", typeof(string));
            dataTable.Columns.Add("Bắt đầu", typeof(string));
            dataTable.Columns.Add("Kết thúc", typeof(string));
            dataTable.Columns.Add("Tổng giờ", typeof(string));
            dataTable.Columns.Add("Lý do", typeof(string));
            dataTable.Columns.Add("Ghi chú", typeof(string));
            int stt = 1;

            foreach (var item in data)
            {
                dataTable.Rows.Add(stt, item.UserFullCode, item.UserFullName, item.TenKhoaPhong, item.StartTime, item.EndTime, item.WH, item.LyDo, "");
                stt++;
            }

            var fileName = DateTime.UtcNow.AddHours(7).ToString("yyMMddHHmmss") + "_cakxd.xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Ca KXD");
                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns("C:AM").Width = 20;
                ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Rows(4, 4 + dataTable.Rows.Count).Height = 20;
                ws.Range("A1:I1").Merge();
                ws.Cell("A1").Value = "BẢNG ĐỀ XUẤT THỜI GIAN LÀM THÊM GIỜ ONCALL";
                ws.Cell("A1").Style.Font.FontSize = 15;
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(4, 1).InsertTable(dataTable);
                ws.Column(2).AdjustToContents();

                var range1 = ws.Range(ws.Cell(4, 1).Address, ws.Cell(4 + dataTable.Rows.Count, 1).Address);
                var range2 = ws.Range(ws.Cell(4, 1).Address, ws.Cell(4 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                range2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //Thêm dòng chữ ký sau khi dữ liệu được in ra
                var s1 = "A" + (6 + dataTable.Rows.Count).ToString() + ":B" + (6 + dataTable.Rows.Count).ToString();
                ws.Cell("A" + (6 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Range(s1).Merge();
                ws.Cell("A" + (6 + dataTable.Rows.Count).ToString()).Value = "Ban Giám Đốc";
                ws.Cell("A" + (6 + dataTable.Rows.Count).ToString()).Style.Font.Bold = true;
                var s2 = "C" + (6 + dataTable.Rows.Count).ToString() + ":C" + (6 + dataTable.Rows.Count).ToString();
                ws.Cell("C" + (6 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                ws.Range(s2).Merge();
                ws.Cell("C" + (6 + dataTable.Rows.Count).ToString()).Value = "Phòng HCNS";
                ws.Cell("C" + (6 + dataTable.Rows.Count).ToString()).Style.Font.Bold = true;

                var s3 = "E" + (6 + dataTable.Rows.Count).ToString() + ":E" + (6 + dataTable.Rows.Count).ToString();
                ws.Cell("E" + (6 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Range(s3).Merge();
                ws.Cell("E" + (6 + dataTable.Rows.Count).ToString()).Value = "Trưởng khoa/ ĐD trưởng";
                ws.Cell("E" + (6 + dataTable.Rows.Count).ToString()).Style.Font.Bold = true;
                var s4 = "F" + (6 + dataTable.Rows.Count).ToString() + ":F" + (6 + dataTable.Rows.Count).ToString();
                ws.Cell("F" + (6 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                ws.Range(s4).Merge();
                ws.Cell("F" + (6 + dataTable.Rows.Count).ToString()).Value = "Người lập phiếu";
                ws.Cell("F" + (6 + dataTable.Rows.Count).ToString()).Style.Font.Bold = true;

                ws.Row(7 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
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

        #endregion Ghép thành ca

        #region EXCEL
        public ActionResult QLCong_Excel(string s = "", string e = "", int makp = -1, int manv = 0)
        {
            if (makp == 0)
                makp = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user }).KhoaPhong;

            DateTime fromDateFormat, toDateFormat;

            //Kiểm tra ngày tháng trước khi điền
            if (String.IsNullOrEmpty(s) && String.IsNullOrEmpty(e))
            {
                fromDateFormat = DateTime.UtcNow.AddHours(7).AddMonths(-1);
                toDateFormat = DateTime.UtcNow.AddHours(7);
            }
            else
            {
                fromDateFormat = DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                toDateFormat = DateTime.ParseExact(e, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            int qldd = __roles.Contains(StaticParams.HCNS_DieuDuong) ? 1 : 0;
            DataTable dataTable = _ccService.QLCong_Excel(fromDateFormat.ToString("yyyyMMdd"), toDateFormat.ToString("yyyyMMdd"), manv, makp, qldd);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Quản lý công");

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
        //public ActionResult Excel_QuanLyCong(string s = "", string e = "", int makp = -1, int manv = 0)
        //{
        //    if (makp == 0)
        //        makp = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user }).KhoaPhong;

        //    DateTime fromDateFormat, toDateFormat;

        //    //Kiểm tra ngày tháng trước khi điền
        //    if (String.IsNullOrEmpty(s) && String.IsNullOrEmpty(e))
        //    {
        //        fromDateFormat = DateTime.UtcNow.AddHours(7).AddMonths(-1);
        //        toDateFormat = DateTime.UtcNow.AddHours(7);
        //    }
        //    else
        //    {
        //        fromDateFormat = DateTime.ParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //        toDateFormat = DateTime.ParseExact(e, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    }

        //    DataTable dataTable = _ccService.QLCong_Excel(fromDateFormat.ToString("yyyyMMdd"), toDateFormat.ToString("yyyyMMdd"), manv, makp);

        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        var ws = wb.Worksheets.Add("Quản lý công");
        //        ws.Style.Font.FontName = "Times New Roman";
        //        ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        //        ws.Columns("C:AM").Width = 20;
        //        //ws.Row(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        //ws.Rows(6, 6 + dataTable.Rows.Count).Height = 20;

        //        ws.Cell(3, 1).InsertTable(dataTable);

        //        //INSERT Tiêu đề vào dòng đầu tiên và merge nó.
        //        ws.Column(2).AdjustToContents();
        //        ws.Range("A1:J1").Merge();
        //        ws.Cell("A1").Value = "QUẢN LÝ CÔNG NHÂN VIÊN";
        //        ws.Cell("A1").Style.Font.FontSize = 20;
        //        ws.Cell("A1").Style.Font.Bold = true;
        //        ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        //Thêm dòng chữ ký sau khi dữ liệu được in ra
        //        ws.Cell("B" + (5 + dataTable.Rows.Count).ToString()).Value = "Xác nhận của trưởng khoa";
        //        ws.Cell("F" + (5 + dataTable.Rows.Count).ToString()).Value = "Người lập phiếu";
        //        ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        ws.Row(9 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        //        var range1 = ws.Range(ws.Cell(3, 1).Address, ws.Cell(3 + dataTable.Rows.Count, 1).Address);
        //        var range2 = ws.Range(ws.Cell(3, 1).Address, ws.Cell(3 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

        //        range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
        //        range2.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        //        range2.Style.Border.InsideBorderColor = XLColor.Black;
        //        range2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        range2.Style.Border.OutsideBorderColor = XLColor.Black; ;

        //        System.Web.HttpContext.Current.Response.Clear();
        //        System.Web.HttpContext.Current.Response.Buffer = true;
        //        System.Web.HttpContext.Current.Response.Charset = "";
        //        System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
        //        System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=cc_" + DateTime.UtcNow.AddHours(7).ToString("yyMMddHHmmss") + ".xlsx");

        //        using (MemoryStream MyMemoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(MyMemoryStream);
        //            MyMemoryStream.WriteTo(System.Web.HttpContext.Current.Response.OutputStream);
        //            System.Web.HttpContext.Current.Response.Flush();
        //            System.Web.HttpContext.Current.Response.End();
        //        }
        //    }

        //    return View();
        //}

        public ActionResult ExportExcelGiaiTrinh(string ngaybd = "", string ngaykt = "", string manv = "", string kp = "", string tt = "")
        {
            var obj = new Excel_GiaiTrinh()
            {
                NGAY_BD = DateTime.ParseExact(ngaybd, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"),
                NGAY_KT = DateTime.ParseExact(ngaykt, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"),
                KhoaPhong = kp,
                MANV = manv,
                TTXL = tt
            };

            DataTable dataTable = _ccService.Excel_DanhSachGiaiTrinh(obj);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Danh sách lỗi chấm công");
                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns("C:AM").Width = 20;
                //ws.Row(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Rows(6, 6 + dataTable.Rows.Count).Height = 20;

                ws.Cell(3, 1).InsertTable(dataTable);

                //INSERT Tiêu đề vào dòng đầu tiên và merge nó.
                ws.Column(2).AdjustToContents();
                ws.Range("A1:J1").Merge();
                ws.Cell("A1").Value = "BẢNG BÁO LỖI CHẤM CÔNG";
                ws.Cell("A1").Style.Font.FontSize = 20;
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //Thêm dòng chữ ký sau khi dữ liệu được in ra
                ws.Cell("B" + (7 + dataTable.Rows.Count).ToString()).Value = "Ban Giám đốc";
                ws.Cell("D" + (7 + dataTable.Rows.Count).ToString()).Value = "Phòng HCNS";
                //ws.Cell("F" + (7 + dataTable.Rows.Count).ToString()).Value = "Phòng QLĐD";
                ws.Cell("G" + (7 + dataTable.Rows.Count).ToString()).Value = "Xác nhận của trưởng khoa";
                ws.Cell("I" + (7 + dataTable.Rows.Count).ToString()).Value = "Người lập phiếu";
                //ws.Cell("F" + (8 + dataTable.Rows.Count).ToString()).Value = "(Dành cho khối ĐD)";
                ws.Row(7 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var range1 = ws.Range(ws.Cell(3, 1).Address, ws.Cell(3 + dataTable.Rows.Count, 1).Address);
                var range2 = ws.Range(ws.Cell(3, 1).Address, ws.Cell(3 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                range2.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                range2.Style.Border.InsideBorderColor = XLColor.Black;
                range2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                range2.Style.Border.OutsideBorderColor = XLColor.Black; ;

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=cc_" + DateTime.UtcNow.AddHours(7).ToString("yyMMddHHmmss") + ".xlsx");

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

        public ActionResult ExportExcelTGLV(string ngaybd = "", string ngaykt = "", string manv = "", string kp = "", int ot = -1, int tt = -1, int phanloai = -1)
        {
            //var obj = new Excel_TGLV()
            //{
            //    NGAY_BD = DateTime.ParseExact(ngaybd, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"),
            //    NGAY_KT = DateTime.ParseExact(ngaykt, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"),
            //    KhoaPhong = kp,
            //    MANV = manv,
            //    DuyetOT = ot,
            //    DuyetTT = tt
            //};

            var obj = new Excel_TGLV()
            {
                NGAY_BD = DateTime.ParseExact(ngaybd, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),
                NGAY_KT = DateTime.ParseExact(ngaykt, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),
                KhoaPhong = kp,
                MANV = manv,
                DuyetOT = ot,
                DuyetTT = tt,
                PhanLoai = phanloai
            };

            string tieude;

            switch (phanloai)
            {
                case 1:
                    tieude = "BẢNG ĐỀ XUẤT ĐI MUỘN - VỀ SỚM";
                    break;
                case 2:
                    tieude = "BẢNG ĐỀ XUẤT LÀM THÊM GIỜ";
                    break;
                case 3:
                    tieude = "BẢNG ĐỀ XUẤT TRỰC TRƯA";
                    break;
                default:
                    tieude = "BẢNG ĐỀ XUẤT THỜI GIAN LÀM THÊM GIỜ, TÍNH TIỀN";
                    break;
            }

            DataTable dataTable = _ccService.Excel_DanhSachTGLV(obj);

            if (dataTable.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Đề xuất TGLV");
                    ws.Style.Font.FontName = "Times New Roman";
                    ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    ws.Columns("C:AM").Width = 20;
                    //ws.Row(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //ws.Rows(6, 6 + dataTable.Rows.Count).Height = 20;

                    ws.Cell(3, 1).InsertTable(dataTable);

                    //INSERT Tiêu đề vào dòng đầu tiên và merge nó.
                    ws.Column(2).AdjustToContents();
                    ws.Range("A1:Q1").Merge();
                    ws.Cell("A1").Value = tieude;
                    ws.Cell("A1").Style.Font.FontSize = 20;
                    ws.Cell("A1").Style.Font.Bold = true;
                    ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //Thêm dòng chữ ký sau khi dữ liệu được in ra
                    ws.Cell("B" + (7 + dataTable.Rows.Count).ToString()).Value = "Ban Giám đốc";
                    ws.Cell("D" + (7 + dataTable.Rows.Count).ToString()).Value = "Phòng HCNS";
                    //ws.Cell("F" + (7 + dataTable.Rows.Count).ToString()).Value = "Phòng QLĐD";
                    ws.Cell("F" + (7 + dataTable.Rows.Count).ToString()).Value = "Xác nhận của trưởng khoa";
                    ws.Cell("G" + (7 + dataTable.Rows.Count).ToString()).Value = "Người lập phiếu";
                    //ws.Cell("F" + (8 + dataTable.Rows.Count).ToString()).Value = "(Dành cho khối ĐD)";
                    ws.Row(7 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    var range1 = ws.Range(ws.Cell(3, 1).Address, ws.Cell(3 + dataTable.Rows.Count, 1).Address);
                    var range2 = ws.Range(ws.Cell(3, 1).Address, ws.Cell(3 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                    range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    range2.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    range2.Style.Border.InsideBorderColor = XLColor.Black;
                    range2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    range2.Style.Border.OutsideBorderColor = XLColor.Black; ;

                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.Buffer = true;
                    System.Web.HttpContext.Current.Response.Charset = "";
                    System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                    System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=cc_" + DateTime.UtcNow.AddHours(7).ToString("yyMMddHHmmss") + ".xlsx");

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(System.Web.HttpContext.Current.Response.OutputStream);
                        System.Web.HttpContext.Current.Response.Flush();
                        System.Web.HttpContext.Current.Response.End();
                    }
                }
            }
            return View();
        }

        public ActionResult ExportXemCong(string from = "", string to = "", int user = -1)
        {
            var obj = new XemCong()
            {
                GioVao = from,
                GioRa = to,
                UserEnrollNumber = user
            };

            DataTable dataTable = _ccService.Excel_XemCong(obj);

            var userinfo = _ccService.Excel_XemCong_TongCong(obj);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("BẢNG CÔNG NHÂN VIÊN");
                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns("C:AM").Width = 20;
                //ws.Row(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Rows(6, 6 + dataTable.Rows.Count).Height = 20;

                ws.Cell(5, 1).InsertTable(dataTable);

                //INSERT Tiêu đề vào dòng đầu tiên và merge nó.
                ws.Column(2).AdjustToContents();
                ws.Range("A1:G1").Merge();
                ws.Cell("A1").Value = "BẢNG CÔNG NHÂN VIÊN";
                ws.Cell("A1").Style.Font.FontSize = 20;
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Range("A3:G3").Merge();
                ws.Cell("A3").Value = "Mã nhân viên: " + userinfo.UserEnrollNumber + " | Tổng công: " + userinfo.NgayCong + @"\" + userinfo.CongChuan + " | Đi muộn về sớm: " + userinfo.DiMuonVeSom + " (phút) | Quên ra - quên vào: " + userinfo.SoLanVaoRa + " (lần)";

                //Thêm dòng chữ ký sau khi dữ liệu được in ra
                ws.Cell("B" + (7 + dataTable.Rows.Count).ToString()).Value = "Xác nhận của trưởng khoa";
                ws.Cell("F" + (7 + dataTable.Rows.Count).ToString()).Value = "Người lập phiếu";
                ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(9 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var range1 = ws.Range(ws.Cell(7, 1).Address, ws.Cell(7 + dataTable.Rows.Count, 1).Address);
                var range2 = ws.Range(ws.Cell(7, 1).Address, ws.Cell(7 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                range2.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                range2.Style.Border.InsideBorderColor = XLColor.Black;
                range2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                range2.Style.Border.OutsideBorderColor = XLColor.Black; ;

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=cc_" + DateTime.UtcNow.AddHours(7).ToString("yyMMddHHmmss") + ".xlsx");

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

        public ActionResult ExportExcelLamThemTT(string ngaybd = "", string ngaykt = "", int manv = -1, int makp = -1, int duyet = -1)
        {
            var obj = new OTPayment_Filter()
            {
                NgayBD = DateTime.ParseExact(ngaybd, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                NgayKT = DateTime.ParseExact(ngaykt, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                MaKP = makp,
                MaNV = manv,
                Duyet = duyet
            };

            DataTable dataTable = _ccService.LamThemTT_DanhSachExcel(obj);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Đề xuất TGLV");
                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns("C:AM").Width = 20;
                //ws.Row(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Rows(6, 6 + dataTable.Rows.Count).Height = 20;

                ws.Cell(3, 1).InsertTable(dataTable);

                //INSERT Tiêu đề vào dòng đầu tiên và merge nó.
                ws.Column(2).AdjustToContents();
                ws.Range("A1:L1").Merge();
                ws.Cell("A1").Value = "BẢNG ĐỀ XUẤT THỜI GIAN LÀM THÊM GIỜ TÍNH TIỀN";
                ws.Cell("A1").Style.Font.FontSize = 20;
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //Thêm dòng chữ ký sau khi dữ liệu được in ra
                ws.Cell("B" + (7 + dataTable.Rows.Count).ToString()).Value = "Ban Giám đốc";
                ws.Cell("D" + (7 + dataTable.Rows.Count).ToString()).Value = "Phòng HCNS";
                ws.Cell("F" + (7 + dataTable.Rows.Count).ToString()).Value = "Phòng QLĐD";
                ws.Cell("G" + (7 + dataTable.Rows.Count).ToString()).Value = "Xác nhận của trưởng khoa";
                ws.Cell("I" + (7 + dataTable.Rows.Count).ToString()).Value = "Người lập phiếu";
                ws.Cell("F" + (8 + dataTable.Rows.Count).ToString()).Value = "(Dành cho khối ĐD)";
                ws.Row(7 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var range1 = ws.Range(ws.Cell(3, 1).Address, ws.Cell(3 + dataTable.Rows.Count, 1).Address);
                var range2 = ws.Range(ws.Cell(3, 1).Address, ws.Cell(3 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                range2.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                range2.Style.Border.InsideBorderColor = XLColor.Black;
                range2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                range2.Style.Border.OutsideBorderColor = XLColor.Black; ;

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=cc_" + DateTime.UtcNow.AddHours(7).ToString("yyMMddHHmmss") + ".xlsx");

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

        public ActionResult ExportExcelTongHopCong(string ngaybd = "", string ngaykt = "", int manv = -1, int kp = -1, int loainv = -1)
        {
            var role = 0;

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                role = 2;
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                role = 1;
            }
            var obj = new TongHopCong_Filter()
            {
                TuNgay = DateTime.ParseExact(ngaybd, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"),
                DenNgay = DateTime.ParseExact(ngaykt, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"),
                KhoaPhong = kp,
                UserID = manv,
                Role = role,
                LoaiNV = loainv
            };

            DataTable dataTable = _ccService.TongHopCong_Excel(obj);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Bảng tổng hợp công");
                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns("C:AM").Width = 20;
                //ws.Row(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Rows(6, 6 + dataTable.Rows.Count).Height = 20;

                ws.Cell(3, 1).InsertTable(dataTable);

                //INSERT Tiêu đề vào dòng đầu tiên và merge nó.
                ws.Column(2).AdjustToContents();
                ws.Range("A1:L1").Merge();
                ws.Cell("A1").Value = "TỔNG HỢP CÔNG KHOA/PHÒNG";
                ws.Cell("A1").Style.Font.FontSize = 20;
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //Thêm dòng chữ ký sau khi dữ liệu được in ra
                ws.Cell("B" + (7 + dataTable.Rows.Count).ToString()).Value = "Ban Giám đốc";
                ws.Cell("D" + (7 + dataTable.Rows.Count).ToString()).Value = "Phòng HCNS";
                ws.Cell("F" + (7 + dataTable.Rows.Count).ToString()).Value = "Phòng QLĐD";
                ws.Cell("G" + (7 + dataTable.Rows.Count).ToString()).Value = "Xác nhận của trưởng khoa";
                ws.Cell("I" + (7 + dataTable.Rows.Count).ToString()).Value = "Người lập phiếu";
                //ws.Cell("F" + (8 + dataTable.Rows.Count).ToString()).Value = "(Dành cho khối ĐD)";
                ws.Row(7 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var range1 = ws.Range(ws.Cell(3, 1).Address, ws.Cell(3 + dataTable.Rows.Count, 1).Address);
                var range2 = ws.Range(ws.Cell(3, 1).Address, ws.Cell(3 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                range2.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                range2.Style.Border.InsideBorderColor = XLColor.Black;
                range2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                range2.Style.Border.OutsideBorderColor = XLColor.Black; ;

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=cc_" + DateTime.UtcNow.AddHours(7).ToString("yyMMddHHmmss") + ".xlsx");

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

        #endregion EXCEL

        #region KHÓA SỐ LIỆU

        [HttpPost]
        public JsonResult Push_KhoaSoLieu(int status = -1)
        {
            return Json(new
            {
                result = _ccService.KhoaSoLieu_CapNhat(status)
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion KHÓA SỐ LIỆU

        [CustomAuthorize(StaticParams.HCNS_VanTayThucTe, StaticParams.IT)]
        public ActionResult VanTayThucTe()
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.Departments = _ccService.DSKhoaPhong();
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.Departments = _ccService.DSKhoaPhong(userInfo.UserEnrollNumber);
            }
            ViewBag.UserInfo = userInfo;
            //ViewBag.Departments = _ccService.DSKhoaPhong();
            return View();
        }

        [HttpGet]
        public JsonResult Table_VanTayThucTe(HCNS_NhanVien objsearch)
        {
            return Json(new { data = _ccService.Table_VanTayThucTe(objsearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Excel_VanTayThucTe(HCNS_NhanVien objsearch)
        {
            DataTable dataTable = _ccService.Excel_VanTayThucTe(objsearch);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("bschamcong");
                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns("A:V").Width = 20;
                ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Rows(4, 4 + dataTable.Rows.Count).Height = 20;
                ws.Range("A1:D1").Merge();
                ws.Cell("A1").Value = "THỜI GIAN CHẤM VÂN TAY THỰC TẾ CỦA NHÂN VIÊN";
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(4, 1).InsertTable(dataTable);
                ws.Column(2).AdjustToContents();

                var range1 = ws.Range(ws.Cell(4, 1).Address, ws.Cell(4 + dataTable.Rows.Count, 1).Address);
                var range2 = ws.Range(ws.Cell(4, 1).Address, ws.Cell(4 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                range2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //Thêm dòng chữ ký sau khi dữ liệu được in ra
                //var sfirst = "B" + (6 + dataTable.Rows.Count).ToString() + ":C" + (6 + dataTable.Rows.Count).ToString();
                //var ssecond = "B" + (7 + dataTable.Rows.Count).ToString() + ":C" + (7 + dataTable.Rows.Count).ToString();
                //ws.Cell("B" + (6 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Cell("B" + (7 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Range(sfirst).Merge();
                //ws.Range(ssecond).Merge();
                //ws.Cell("B" + (6 + dataTable.Rows.Count).ToString()).Value = "Hà Nội, Ngày " + DateTime.UtcNow.AddHours(7).Day + " Tháng " + DateTime.UtcNow.AddHours(7).Month + " Năm " + DateTime.UtcNow.AddHours(7).Year;
                //ws.Cell("B" + (7 + dataTable.Rows.Count).ToString()).Value = "Đại diện";
                //ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Row(9 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=dschamcong.xlsx");
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
        public JsonResult Push_TongHopSoLieu(string ngaybd, string ngaykt)
        {
            string error = "";
            return Json(new { result = _ccService.TongHopCong_TongHopSoLieu(ngaybd, ngaykt, ref error), error }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.HCNS_LamViecNgayLe, StaticParams.IT)]
        public ActionResult LamViecNgayLe()
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            ViewBag.UserInfo = userInfo;

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
                ViewBag.UserAuth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                ViewBag.UserAuth = "HCNS_Manager";
            }
            //ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            ViewBag.StartDate = NgayDauKyLuong();
            ViewBag.EndDate = NgayCuoiKyLuong();
            return View();
        }
        [HttpGet]
        public JsonResult Table_LamViecNgayLe(LamViecNgayLe objsearch)
        {
            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                objsearch.KhoaPhong = string.IsNullOrEmpty(objsearch.KhoaPhong) ? "-1" : objsearch.KhoaPhong;
            }
            //if (objsearch.Auth == "HCNS_Admin")
            //{
            //    objsearch.KhoaPhong = objsearch.KhoaPhong == "" ? "-1" : objsearch.KhoaPhong;
            //}
            return Json(new { data = _ccService.Table_LamViecNgayLe(objsearch) }, JsonRequestBehavior.AllowGet);
        }
        //public PartialViewResult Table_LamViecNgayLe(string auth = "", string from = "", string to = "", string kp = "", string user = "", int status = -1)
        //{
        //    DateTime fromFormat = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    DateTime toFormat = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    if (auth == "HCNS_Admin")
        //    {
        //        kp = kp == "" ? "-1" : kp;
        //    }
        //    DataTable data = _ccService.Table_LamViecNgayLe(fromFormat, toFormat, kp, user, status);

        //    ViewBag.Permission = auth;

        //    return PartialView("_Table_LamViecNgayLe", data);
        //}
        [HttpPost]
        public JsonResult Push_DeXuatLamViecNgayLe(string lydo, string loaica, string id)
        {
            return Json(new { result = _ccService.Push_DeXuatLamViecNgayLe(id, lydo, loaica, 1, "") }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Push_HoanDeXuatLamViecNgayLe(string lydo, string loaica, string id)
        {
            return Json(new { result = _ccService.Push_DeXuatLamViecNgayLe(id, lydo, loaica, 2, "") }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Push_DuyetLamViecNgayLe(string lydo, string loaica, string id)
        {
            return Json(new { result = _ccService.Push_DeXuatLamViecNgayLe(id, lydo, loaica, 3, "2") }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Push_HoanLamViecNgayLe(string lydo, string loaica, string id)
        {
            return Json(new { result = _ccService.Push_DeXuatLamViecNgayLe(id, lydo, loaica, 3, "1") }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Push_TuChoiLamViecNgayLe(string lydo, string loaica, string id)
        {
            return Json(new { result = _ccService.Push_DeXuatLamViecNgayLe(id, lydo, loaica, 3, "3") }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Excel_LamViecNgayLe(LamViecNgayLe objsearch)
        {
            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                objsearch.KhoaPhong = string.IsNullOrEmpty(objsearch.KhoaPhong) ? "-1" : objsearch.KhoaPhong;
            }
            DataTable dataTable = _ccService.Excel_LamViecNgayLe(objsearch);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("LamViecNgayLe");
                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns("C:AM").Width = 20;
                ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Rows(4, 4 + dataTable.Rows.Count).Height = 20;
                ws.Range("A1:F1").Merge();
                ws.Cell("A1").Value = "BẢN XÁC NHẬN ĐI TRỰC NGÀY LỄ";
                ws.Cell("A1").Style.Font.FontSize = 15;
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(4, 1).InsertTable(dataTable);
                ws.Column(2).AdjustToContents();

                var range1 = ws.Range(ws.Cell(4, 1).Address, ws.Cell(4 + dataTable.Rows.Count, 1).Address);
                var range2 = ws.Range(ws.Cell(4, 1).Address, ws.Cell(4 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                range2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //Thêm dòng chữ ký sau khi dữ liệu được in ra
                var s1 = "A" + (6 + dataTable.Rows.Count).ToString() + ":B" + (6 + dataTable.Rows.Count).ToString();
                ws.Cell("A" + (6 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Range(s1).Merge();
                ws.Cell("A" + (6 + dataTable.Rows.Count).ToString()).Value = "Tổng Giám Đốc";
                ws.Cell("A" + (6 + dataTable.Rows.Count).ToString()).Style.Font.Bold = true;
                var s2 = "C" + (6 + dataTable.Rows.Count).ToString() + ":C" + (6 + dataTable.Rows.Count).ToString();
                ws.Cell("C" + (6 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                ws.Range(s2).Merge();
                ws.Cell("C" + (6 + dataTable.Rows.Count).ToString()).Value = "Phòng HCNS";
                ws.Cell("C" + (6 + dataTable.Rows.Count).ToString()).Style.Font.Bold = true;
                var s3 = "E" + (6 + dataTable.Rows.Count).ToString() + ":E" + (6 + dataTable.Rows.Count).ToString();
                ws.Cell("E" + (6 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Range(s3).Merge();
                ws.Cell("E" + (6 + dataTable.Rows.Count).ToString()).Value = "Trưởng khoa/ ĐD trưởng";
                ws.Cell("E" + (6 + dataTable.Rows.Count).ToString()).Style.Font.Bold = true;
                var s4 = "F" + (6 + dataTable.Rows.Count).ToString() + ":F" + (6 + dataTable.Rows.Count).ToString();
                ws.Cell("F" + (6 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                ws.Range(s4).Merge();
                ws.Cell("F" + (6 + dataTable.Rows.Count).ToString()).Value = "Người lập";
                ws.Cell("F" + (6 + dataTable.Rows.Count).ToString()).Style.Font.Bold = true;

                ws.Row(7 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //var sfirst = "J" + (6 + dataTable.Rows.Count).ToString() + ":K" + (6 + dataTable.Rows.Count).ToString();
                //var ssecond = "J" + (7 + dataTable.Rows.Count).ToString() + ":K" + (7 + dataTable.Rows.Count).ToString();
                //ws.Cell("J" + (6 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Cell("J" + (7 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Range(sfirst).Merge();
                //ws.Range(ssecond).Merge();
                //ws.Cell("J" + (6 + dataTable.Rows.Count).ToString()).Value = "Hà Nội, Ngày " + DateTime.UtcNow.AddHours(7).Day + " Tháng " + DateTime.UtcNow.AddHours(7).Month + " Năm " + DateTime.UtcNow.AddHours(7).Year;
                //ws.Cell("J" + (7 + dataTable.Rows.Count).ToString()).Value = "Đại diện";
                //ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Row(9 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=lamviecngayle_" + DateTime.UtcNow.AddHours(7).ToString("yyyyMMdd") + ".xlsx");
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
        public JsonResult ChuyenKyLuong(string ngaybd, string ngaykt)
        {
            string error = "";
            return Json(new { result = _ccService.ChuyenKyLuong(ngaybd, ngaykt, ref error), error }, JsonRequestBehavior.AllowGet);
        }


    }
}