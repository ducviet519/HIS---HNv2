using ClosedXML.Excel;
using FastMember;
using System;
using System.App.Entities;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories.Common;
using System.App.Security;
using System.App.Services;
using System.App.Services.HCNS;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HCNS.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly NhanVien_Interface _nhanVienService;
        private readonly IKhaiBaoVang _kbvService;
        private readonly ChamCong_Interface _ccService;
        private readonly IConfig _configService;
        private string[] __roles;
        private string __user;
        private int locked = 0;
        private string lockedMessage = "";

        public NhanVienController(NhanVien_Interface nhanVien_Interface, IKhaiBaoVang kbv, ChamCong_Interface cc, IConfig configInterface)
        {
            _nhanVienService = nhanVien_Interface;
            _kbvService = kbv;
            _ccService = cc;
            _configService = configInterface;

            __user = System.Web.HttpContext.Current.User.Identity.Name;
            __roles = (string[])System.Web.HttpContext.Current.Items["Roles"];

            if (KiemTraKhoaSoLieu(ref lockedMessage))
            {
                locked = 1;
            }
        }

        private bool KiemTraKhoaSoLieu(ref string message)
        {
            bool check = false;

            if (__roles.Contains(StaticParams.HCNS_KiemTraKhoaSoLieu))
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

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        [CustomAuthorize(StaticParams.HCNS_KhaiBaoNhanVienMoi, StaticParams.IT, StaticParams.HCNS_KhaiBaoNhanVienMoi_View)]
        public ActionResult Index()
        {
            ViewBag.DSKP = _nhanVienService.DanhSachKhoaPhong();
            ViewBag.DSKPHC = _nhanVienService.DanhSachKhoaPhongHC();
            ViewBag.NoiAn = _nhanVienService.DanhSachNoiAn_Index();
            ViewBag.Auth = _nhanVienService.GetPermission();
            return View();
        }

        public PartialViewResult Table_Data(string a = "", string id = "", int pkhc = 0, int pk = 0, int trangthai = 3, string gio = "")
        {
            if (a == "s")
            {
                HCNS_NhanVien hcns = new HCNS_NhanVien
                {
                    UserFullName = id,
                    PhongKhoaHC = pkhc,
                    KhoaPhong = pk,
                    TrangThai = trangthai,
                    SoGioLamViec = gio
                };

                return PartialView("_Table_Data", _nhanVienService.DanhSachNhanVien(hcns));
            }
            return PartialView("_Table_Data", _nhanVienService.DanhSachNhanVien());
        }

        [HttpGet]
        public JsonResult AjaxList(string a = "", string id = "", int pkhc = 0, int pk = 0, int trangthai = 3, string gio = "")
        {
            if (a == "s")
            {
                HCNS_NhanVien hcns = new HCNS_NhanVien
                {
                    UserFullName = id,
                    PhongKhoaHC = pkhc,
                    KhoaPhong = pk,
                    TrangThai = trangthai,
                    SoGioLamViec = gio
                };

                return Json(new { data = _nhanVienService.DanhSachNhanVien(hcns) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = _nhanVienService.DanhSachNhanVien() }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_KhaiBaoNhanVienMoi)]
        public PartialViewResult CapNhat(int id = 0)
        {
            ViewBag.DSKP = _nhanVienService.DanhSachKhoaPhong();
            ViewBag.NoiAnCC = _nhanVienService.DanhSachNoiAn();
            ViewBag.DSKPHC = _nhanVienService.DanhSachKhoaPhongHC();
            ViewBag.DTCC = _nhanVienService.DoiTuongCC();
            ViewBag.DS_DoiTuong = _nhanVienService.DS_DoiTuong();
            //ViewBag.LoaiNV = StaticParams.LoaiNhanVien();
            ViewBag.LoaiNV = _nhanVienService.LoaiNhanVien();

            if (id >= 0)
                return PartialView("_CapNhat", _nhanVienService.TimThongTinNhanVien(new HCNS_NhanVien() { UserEnrollNumber = id }));
            return PartialView("_CapNhat", new HCNS_NhanVien());
        }

        [CustomAuthorize(StaticParams.HCNS_UploadThongTin, StaticParams.IT)]
        public ActionResult ImportExcel()
        {
            return View("ImportExcel");
        }

        public DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        public DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {
                oledbConn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
                {
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);

                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oledbConn.Close();
            }
            return dt;
        }

        [CustomAuthorize(StaticParams.HCNS_ThongTinNhanSu, StaticParams.IT)]
        public ActionResult TTNS()
        {
            ViewBag.DSKPHC = _nhanVienService.DanhSachKhoaPhongHC();
            return View();
        }

        public PartialViewResult TTNS_Table_Data(string a = "", string id = "", int pkhc = 0, int trangthai = 3, string gio = "")
        {
            if (a == "s")
            {
                HCNS_NhanVien hcns = new HCNS_NhanVien
                {
                    UserFullName = id,
                    PhongKhoaHC = pkhc,
                    TrangThai = trangthai,
                    SoGioLamViec = gio
                };

                return PartialView("_TTNS_Table_Data", _nhanVienService.DS_NhanVienBoSung(hcns));
            }
            return PartialView("_TTNS_Table_Data", _nhanVienService.DS_NhanVienBoSung());
        }

        [CustomAuthorize(StaticParams.HCNS_NhanSuNghiViec, StaticParams.IT)]
        public ActionResult NghiViec()
        {
            ViewBag.DSKPHC = _nhanVienService.DanhSachKhoaPhongHC();
            return View();
        }

        //public PartialViewResult NghiViec_Table_Data(string a = "", string search = "", int kphc = 0, int tt = 0)
        //{
        //    if (a == "search")
        //    {
        //        HCNS_NhanVien hcns = new HCNS_NhanVien
        //        {
        //            UserFullName = search,
        //            PhongKhoaHC = kphc,
        //            TrangThai = tt
        //        };
        //        return PartialView("_NghiViec_Table_Data", _nhanVienService.DS_NhanVienNghiViec(hcns));
        //    }
        //    return PartialView("_NghiViec_Table_Data", _nhanVienService.DS_NhanVienNghiViec());
        //}

        public JsonResult NghiViec_TableData(string a = "", string search = "", int kphc = 0, int tt = 0)
        {
            if (a == "search")
            {
                HCNS_NhanVien hcns = new HCNS_NhanVien
                {
                    UserFullName = search,
                    PhongKhoaHC = kphc,
                    TrangThai = tt
                };
                return Json(new { data = _nhanVienService.DS_NhanVienNghiViec(hcns) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = _nhanVienService.DS_NhanVienNghiViec() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult NghiViec_XoaThongTin(int user)
        {
            return Json(new { result = _nhanVienService.XoaTTNghiViec(user) }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_ThongTinNhanSu)]
        public PartialViewResult Update_NghiViec(int id)
        {
            var obj = _nhanVienService.TimThongTinNhanVien(new HCNS_NhanVien() { UserEnrollNumber = id });

            return PartialView("_Update_NghiViec", obj);
        }

        [CustomAuthorize(StaticParams.HCNS_NhanSuNghiViec)]
        public PartialViewResult Sua_NghiViec(int id)
        {
            var obj = _nhanVienService.TimThongTinNhanVien(new HCNS_NhanVien() { UserEnrollNumber = id });

            return PartialView("_Sua_NghiViec", obj);
        }

        [HttpPost]
        public ActionResult Update_NghiViec(HCNS_NhanVien nv)
        {
            var _bool = false;

            _bool = _nhanVienService.SuaTTNghiViec(nv);

            return Json(new { result = _bool }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_ThongTinNhanSu)]
        public PartialViewResult Update_TTNS(int id)
        {
            var obj = _nhanVienService.TimThongTinNhanVien(new HCNS_NhanVien() { UserEnrollNumber = id });
            ViewBag.DS_NoiSinh = _nhanVienService.DS_NoiSinh();
            ViewBag.DS_GioiTinh = DS_GioiTinh();
            ViewBag.DS_LoaiCMT = DS_LoaiCMT();
            ViewBag.DS_NoiCapCMT = _nhanVienService.DS_NoiCapCMT();
            ViewBag.DS_DoiTuong = _nhanVienService.DS_DoiTuong();
            ViewBag.DS_DanToc = _nhanVienService.DS_DanToc();
            ViewBag.DS_QuocGia = _nhanVienService.DS_QuocGia();
            ViewBag.DS_ThanhPho = _nhanVienService.DS_ThanhPho();
            ViewBag.DS_QuanHuyenThT = _nhanVienService.DS_QuanHuyen(int.Parse(obj.TinhThT));
            ViewBag.DS_PhuongXaThT = _nhanVienService.DS_PhuongXa(int.Parse(obj.QuanThT));

            //ViewBag.DS_QuocGiaTT = _nhanVienService.DS_QuocGia();
            //ViewBag.DS_ThanhPhoTT = _nhanVienService.DS_ThanhPho();
            ViewBag.DS_QuanHuyenTT = _nhanVienService.DS_QuanHuyen(int.Parse(obj.TinhTT));
            ViewBag.DS_PhuongXaTT = _nhanVienService.DS_PhuongXa(int.Parse(obj.QuanTT));

            ViewBag.DS_TonGiao = _nhanVienService.DS_TonGiao();

            return PartialView("_Update_TTNS", obj);
        }

        private Dictionary<int, string> DS_GioiTinh()
        {
            Dictionary<int, string> lst = new Dictionary<int, string>
            {
                { 0, "Nam" },
                { 1, "Nữ" }
            };
            return lst;
        }

        private Dictionary<int, string> DS_LoaiCMT()
        {
            Dictionary<int, string> lst = new Dictionary<int, string>
            {
                { 1, "CMT" },
                { 2, "Hộ chiếu" },
                { 3, "Thẻ căn cước" }
            };
            return lst;
        }

        public PartialViewResult DDL_QuanHuyenThT(int tinhthanh = 0)
        {
            ViewBag.DS_QuanHuyen = _nhanVienService.DS_QuanHuyen(tinhthanh);

            return PartialView("_DDL_QuanHuyenThT");
        }

        public PartialViewResult DDL_PhuongXaThT(int quanhuyen = 0)
        {
            ViewBag.DS_PhuongXa = _nhanVienService.DS_PhuongXa(quanhuyen);

            return PartialView("_DDL_PhuongXaThT");
        }

        public PartialViewResult DDL_QuanHuyenTT(int tinhthanh = 0)
        {
            ViewBag.DS_QuanHuyen = _nhanVienService.DS_QuanHuyen(tinhthanh);

            return PartialView("_DDL_QuanHuyenTT");
        }

        public PartialViewResult DDL_PhuongXaTT(int quanhuyen = 0)
        {
            ViewBag.DS_PhuongXa = _nhanVienService.DS_PhuongXa(quanhuyen);

            return PartialView("_DDL_PhuongXaTT");
        }

        public PartialViewResult UploadFiles()
        {
            return PartialView("_UploadFiles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatThongTin(HCNS_NhanVien nv)
        {
            var _bool = false;

            if (nv.UserEnrollNumber > 0)
            {
                _bool = _nhanVienService.SuaThongTinNhanVienMoi(nv);
            }
            else
            {
                _bool = _nhanVienService.ThemNhanVienMoi(nv);
            }

            return Json(new { result = _bool }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Push_ImportExcel(FormCollection form, HttpPostedFileBase fileUpload)
        {
            if (Request.Files["fileUpload"].ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files["fileUpload"].FileName).ToLower();
                string connString = "";

                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["fileUpload"].FileName);

                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1)) { System.IO.File.Delete(path1); }

                    Request.Files["fileUpload"].SaveAs(path1);

                    if (extension == ".csv")
                    {
                        DataTable dt = ConvertCSVtoDataTable(path1);
                        ViewBag.Data = dt;
                    }
                    //Connection String to Excel Workbook
                    else if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        DataTable dt = ConvertXSLXtoDataTable(path1, connString);
                        ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                        DataTable dt = ConvertXSLXtoDataTable(path1, connString);

                        List<HCNS_NhanVien_Excel> lst = new List<HCNS_NhanVien_Excel>();

                        foreach (DataRow row in dt.Rows)
                        {
                            if (!String.IsNullOrEmpty(row["MA"].ToString()))
                            {
                                HCNS_NhanVien_Excel excel = new HCNS_NhanVien_Excel();
                                //Thông tin nhân sự
                                excel.HcnsNhanVien.UserEnrollNumber = int.Parse(row["MA"].ToString().Trim());
                                excel.HcnsNhanVien.UserFullCode = String.IsNullOrEmpty(row["MA_NV"].ToString()) ? "" : row["MA_NV"].ToString().Trim();
                                excel.HcnsNhanVien.UserFullName = String.IsNullOrEmpty(row["HO_TEN"].ToString()) ? "" : row["HO_TEN"].ToString().Trim();
                                excel.HcnsNhanVien.NgaySinh = String.IsNullOrEmpty(row["NAM_SINH"].ToString()) ? "" : DateTime.Parse(row["NAM_SINH"].ToString().Trim()).ToString("dd/MM/yyyy");
                                excel.HcnsNhanVien.ChucDanh = String.IsNullOrEmpty(row["CHUC_DANH"].ToString()) ? "" : row["CHUC_DANH"].ToString().Trim();
                                excel.HcnsNhanVien.GioiTinh = String.IsNullOrEmpty(row["GIOI_TINH"].ToString()) ? "" : row["GIOI_TINH"].ToString().Trim();
                                excel.HcnsNhanVien.DoiTuong = String.IsNullOrEmpty(row["DOI_TUONG"].ToString()) ? "" : row["DOI_TUONG"].ToString().Trim();
                                excel.HcnsNhanVien.SDT1 = String.IsNullOrEmpty(row["DI_DONG"].ToString()) ? "" : row["DI_DONG"].ToString().Trim();
                                excel.HcnsNhanVien.TAEmail = String.IsNullOrEmpty(row["EMAIL_NOI_BO"].ToString()) ? "" : row["EMAIL_NOI_BO"].ToString().Trim();
                                excel.HcnsNhanVien.Email = String.IsNullOrEmpty(row["EMAIL_CA_NHAN"].ToString()) ? "" : row["EMAIL_CA_NHAN"].ToString().Trim();
                                excel.HcnsNhanVien.SoCMT = String.IsNullOrEmpty(row["CMT"].ToString()) ? "" : row["CMT"].ToString().Trim();
                                excel.HcnsNhanVien.CapNgay = String.IsNullOrEmpty(row["NGAY_CAP_CMT"].ToString()) ? "" : DateTime.Parse(row["NGAY_CAP_CMT"].ToString().Trim()).ToString("dd/MM/yyyy");
                                excel.HcnsNhanVien.NoiCap = String.IsNullOrEmpty(row["NOI_CAP_CMT"].ToString()) ? "" : row["NOI_CAP_CMT"].ToString().Trim();
                                //excel.HcnsNhanVien.NoiSinh = String.IsNullOrEmpty(row["NOI_SINH"].ToString()) ? "" : row["NOI_SINH"].ToString();
                                excel.HcnsNhanVien.NoiSinhCT = String.IsNullOrEmpty(row["NOI_SINH_CT"].ToString()) ? "" : row["NOI_SINH_CT"].ToString();
                                excel.HcnsNhanVien.Ten_PhongKhoaHC = String.IsNullOrEmpty(row["KHOA_PHONG"].ToString()) ? "Khác" : row["KHOA_PHONG"].ToString();
                                excel.HcnsNhanVien.NgayVao = String.IsNullOrEmpty(row["NGAY_GIA_NHAP"].ToString()) ? "" : DateTime.Parse(row["NGAY_GIA_NHAP"].ToString().Trim()).ToString("dd/MM/yyyy");
                                //excel.HcnsNhanVien.MSTCN = String.IsNullOrEmpty(row["MST_CA_NHAN"].ToString()) ? "" : row["MST_CA_NHAN"].ToString();
                                //excel.HcnsNhanVien.SoTK1 = String.IsNullOrEmpty(row["TAIKHOAN_MB"].ToString()) ? "" : row["TAIKHOAN_MB"].ToString();
                                //excel.HcnsNhanVien.SoTK2 = String.IsNullOrEmpty(row["TAIKHOAN_BIDV"].ToString()) ? "" : row["TAIKHOAN_BIDV"].ToString();
                                excel.HcnsNhanVien.SoTheBHXH = String.IsNullOrEmpty(row["BHXH"].ToString()) ? "" : row["BHXH"].ToString().Trim();
                                excel.HcnsNhanVien.TATitle = String.IsNullOrEmpty(row["HHHV"].ToString()) ? "" : row["HHHV"].ToString().Trim();
                                excel.HcnsNhanVien.SoGioLamViec = String.IsNullOrEmpty(row["GIO_LV"].ToString()) ? "" : row["GIO_LV"].ToString().Trim();
                                excel.HcnsNhanVien.DcThT = String.IsNullOrEmpty(row["DIACHITHTDD"].ToString()) ? "" : row["DIACHITHTDD"].ToString().Trim();
                                excel.HcnsNhanVien.DcTT = String.IsNullOrEmpty(row["DIACHITTDD"].ToString()) ? "" : row["DIACHITTDD"].ToString().Trim();
                                excel.HcnsNhanVien.DanToc = String.IsNullOrEmpty(row["DAN_TOC"].ToString()) ? "Kinh" : row["DAN_TOC"].ToString().Trim();
                                excel.HcnsNhanVien.QuocTich = String.IsNullOrEmpty(row["QUOC_TICH"].ToString()) ? "Việt Nam" : row["QUOC_TICH"].ToString().Trim(); ;
                                excel.HcnsNhanVien.TonGiao = String.IsNullOrEmpty(row["TON_GIAO"].ToString()) ? "Không" : row["TON_GIAO"].ToString().Trim(); ;
                                //Bằng cấp
                                excel.BangCap.Type_Name = String.IsNullOrEmpty(row["HOC_VAN"].ToString()) ? "" : row["HOC_VAN"].ToString().Trim();
                                excel.BangCap.GradSch = String.IsNullOrEmpty(row["TRUONG_TN"].ToString()) ? "" : row["TRUONG_TN"].ToString().Trim();
                                excel.BangCap.Field = String.IsNullOrEmpty(row["CHUYEN_NGANH"].ToString()) ? "" : row["CHUYEN_NGANH"].ToString().Trim();
                                //Chứng chỉ hành nghề
                                excel.ChungChiHanhNghe.ChungChiHN = String.IsNullOrEmpty(row["CC_HANH_NGHE"].ToString()) ? "" : row["CC_HANH_NGHE"].ToString().Trim();
                                excel.ChungChiHanhNghe.PhamViCM = String.IsNullOrEmpty(row["PHAM_VI_CHUYEN_MON"].ToString()) ? "" : row["PHAM_VI_CHUYEN_MON"].ToString().Trim();
                                excel.ChungChiHanhNghe.NgayCapCCHN = String.IsNullOrEmpty(row["NGAY_CAP"].ToString()) ? "" : DateTime.Parse(row["NGAY_CAP"].ToString().Trim()).ToString("dd/MM/yyyy");
                                excel.ChungChiHanhNghe.DaGuiSo = "0";

                                lst.Add(excel);
                            }
                        }
                        string message = "";
                        var r = _nhanVienService.ThemNhanVienMoiExcel(lst, out message);

                        if (r)
                            return Content("Upload file thành công");
                        return Content("Có lỗi xảy ra trong quá trình upload file hoặc ngày cấu hình đã tồn tại." + message);
                    }
                }
                else
                {
                    return Content("Please Upload Files in .xls, .xlsx or .csv format");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update_TTNS(HCNS_NhanVien obj)
        {
            return Json(new { r = _nhanVienService.SuaTTCapNhatNV(obj) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Upload(int id)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                ImageUpload imageUpload = new ImageUpload { Width = 150 };

                string name = id + Path.GetExtension(file.FileName);

                ImageResult imageResult = imageUpload.RenameUploadFile(file, name: name);

                if (imageResult.Success)
                {
                    _nhanVienService.Upload_AnhDaiDien(id, name);
                }
                //else
                //{
                //}
                //var fileName = Path.GetFileName(file.FileName);

                //var path = Path.Combine(Server.MapPath("~/images/upload/"), fileName);

                //file.SaveAs(path);
            }
        }

        [HttpPost]
        public void PushUploadFiles()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                var id = file.FileName.Split('_')[0].ToString();

                ImageUpload imageUpload = new ImageUpload { Width = 150 };

                string name = id + Path.GetExtension(file.FileName);

                ImageResult imageResult = imageUpload.RenameUploadFile(file, name: name);

                if (imageResult.Success)
                {
                    _nhanVienService.Upload_AnhDaiDien(int.Parse(id), name);
                }
            }
        }

        [CustomAuthorize(StaticParams.HCNS_DanhBaDienThoai, StaticParams.IT)]
        public ActionResult DanhBa()
        {
            ViewBag.DSKHOAPHONG = _nhanVienService.DSKhoaPhong();
            ViewBag.DSTOANHA = _nhanVienService.DSToaNha();
            ViewBag.DSTANG = _nhanVienService.DSTang();
            ViewBag.Auth = _nhanVienService.GetPermission();
            return View();
        }

        public ActionResult DanhBa_Table_Data()
        {
            var model = _nhanVienService.Ds_DanhBaDienThoai();

            return PartialView("_DanhBa_Table_Data", model);
        }

        [HttpGet]
        public JsonResult DS_DanhBa_Search(HCNS_NhanVien objSearch)
        {
            return Json(new { data = _nhanVienService.DS_DanhBa_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_DanhBa()
        {
            ViewBag.DSKHOAPHONG = _nhanVienService.DSKhoaPhong();
            ViewBag.DSTOANHA = _nhanVienService.DSToaNha();
            ViewBag.DSTANG = _nhanVienService.DSTang();
            return PartialView("_Create_DanhBa");
        }
        [HttpPost]
        public JsonResult AddNew_DanhBa(HCNS_NhanVien obj)
        {
            string _errorMessage = "";
            return Json(new { result = _nhanVienService.AddNew_DanhBa(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit_DanhBaDienThoai(HCNS_NhanVien obj)
        {
            string _errorMessage = "";
            return Json(new { result = _nhanVienService.Edit_DanhBaDienThoai(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_DanhBaDienThoai(HCNS_NhanVien obj)
        {
            string _errorMessage = "";
            return Json(new { result = _nhanVienService.Delete_DanhBaDienThoai(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Edit_DanhBa(HCNS_NhanVien obj)
        {
            var info = _nhanVienService.Get_DanhBa(obj);
            ViewBag.DSKHOAPHONG = _nhanVienService.DSKhoaPhong(info.KhoaPhong);
            ViewBag.DSTOANHA = _nhanVienService.DSToaNha(info.ToaNha);
            ViewBag.DSTANG = _nhanVienService.DSTang(info.Tang);
            ViewBag.ID = obj.ID;
            return PartialView("_Edit_DanhBa", info);
        }
        public ActionResult DS_DanhBa_Excel(HCNS_NhanVien objSearch)
        {
            //var dataTable = _nhanVienService.DS_DanhBa_Search(objSearch);
            DataTable dataTable = new DataTable();
            using (var reader = ObjectReader.Create(_nhanVienService.DS_DanhBa_Excel(objSearch)))
            {
                dataTable.Load(reader);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Danh bạ điện thoại");

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
        #region Khai báo vắng

        string[] dsnvVIP = { "linhnt2", "hiennt2" };

        //Khai báo vắng
        [CustomAuthorize(StaticParams.HCNS_KhaiBaoVang, StaticParams.IT)]
        public ActionResult Khaibaovang()
        {
            #region Kiểm tra hạn khai báo

            ViewBag.Locked = locked;
            ViewBag.LockMessage = lockedMessage;

            #endregion Kiểm tra hạn khai báo

            ViewBag.DS_AbsentSymbol = _kbvService.DanhSachKhaiBao();

            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles.Contains(StaticParams.HCNS_Admin) || dsnvVIP.Contains(__user))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
                ViewBag.Auth = "HCNS_Admin";
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
                ViewBag.Auth = "HCNS_Manager";
            }

            return View();
        }

        public PartialViewResult Khaibaovang_Table_Data(string a = "", string code = "", string from = "", string to = "", string kp = "", string info = "")
        {
            var user = System.Web.HttpContext.Current.User.Identity.Name;
            var _khoaphong = string.Join(",", (string[])HttpContext.Items["Depts"]);

            // admin
            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                if (a == "search")
                {
                    Absent absent = new Absent()
                    {
                        process = "search",
                        UserFullName = info,
                        AbsentCode = code,
                        fromDate = from,
                        toDate = to,
                        UserAccount = user,
                        IsAdmin = true
                    };

                    return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_All(absent));
                }
                else
                {
                    Absent absent = new Absent()
                    {
                        UserAccount = user
                    };
                    return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_All(absent));
                }
            }
            // danh cho nguoi ke khai
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                if (a == "search")
                {
                    Absent absent = new Absent()
                    {
                        process = "search",
                        UserFullName = info,
                        AbsentCode = code,
                        fromDate = from,
                        toDate = to,
                        UserAccount = user,
                        UserInDepts = _khoaphong
                    };
                    if (__roles.Contains(StaticParams.HCNS_DieuDuong))
                    {
                        return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_DieuDuong(absent));
                    }
                    else
                    {
                        return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_Edit(absent));
                    }
                }
                else
                {
                    Absent absent = new Absent()
                    {
                        UserAccount = user,
                        UserInDepts = _khoaphong
                    };
                    if (__roles.Contains(StaticParams.HCNS_DieuDuong))
                    {
                        return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_DieuDuong(absent));
                    }
                    else
                    {
                        return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_Edit(absent));
                    }
                }
            }
            //user
            else if (__roles.Contains(StaticParams.HCNS_User))
            {
                if (a == "search")
                {
                    Absent absent = new Absent()
                    {
                        process = "search",
                        UserFullName = info,
                        AbsentCode = code,
                        fromDate = from,
                        toDate = to,
                        UserAccount = user
                    };

                    return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_View(absent));
                }
                else
                {
                    Absent absent = new Absent()
                    {
                        UserAccount = user
                    };
                    return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_View(absent));
                }
            }
            Absent obj = new Absent()
            {
                UserAccount = user
            };
            return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_View(obj));
        }

        public JsonResult Absent_AjaxList(string a = "", string code = "", string from = "", string to = "", string info = "", int kp = 0)
        {
            var user = System.Web.HttpContext.Current.User.Identity.Name;
            //var _khoaphong = string.Join(",", (string[])HttpContext.Items["Depts"]);
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            // admin
            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                if (a == "search")
                {
                    Absent absent = new Absent()
                    {
                        process = "search",
                        UserFullName = info,
                        AbsentCode = code,
                        fromDate = from,
                        toDate = to,
                        UserAccount = user,
                        KhoaPhong = kp,
                        IsAdmin = true
                    };

                    //return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_All(absent));
                    return Json(new { data = _kbvService.Ds_LichKhaiBao_All(absent) }, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Absent absent = new Absent()
                    {
                        UserAccount = user
                    };
                    //return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_All(absent));
                    return Json(new { data = _kbvService.Ds_LichKhaiBao_All(absent) }, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
                }
            }
            // danh cho nguoi ke khai
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                if (a == "search")
                {
                    Absent absent = new Absent()
                    {
                        process = "search",
                        UserFullName = info,
                        AbsentCode = code,
                        fromDate = from,
                        toDate = to,
                        UserAccount = user,
                        UserInDepts = kp.ToString()
                    };
                    if (__roles.Contains(StaticParams.HCNS_DieuDuong))
                    {
                        //return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_DieuDuong(absent));
                        return Json(new { data = _kbvService.Ds_LichKhaiBao_DieuDuong(absent) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_Edit(absent));
                        return Json(new { data = _kbvService.Ds_LichKhaiBao_Edit(absent) }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Absent absent = new Absent()
                    {
                        UserAccount = user,
                        UserInDepts = userInfo.KhoaPhong.ToString()
                    };
                    if (__roles.Contains(StaticParams.HCNS_DieuDuong))
                    {
                        //return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_DieuDuong(absent));
                        return Json(new { data = _kbvService.Ds_LichKhaiBao_DieuDuong(absent) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_Edit(absent));
                        return Json(new { data = _kbvService.Ds_LichKhaiBao_Edit(absent) }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            //user
            else if (__roles.Contains(StaticParams.HCNS_User))
            {
                if (a == "search")
                {
                    Absent absent = new Absent()
                    {
                        process = "search",
                        UserFullName = info,
                        AbsentCode = code,
                        fromDate = from,
                        toDate = to,
                        UserAccount = user
                    };

                    //return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_View(absent));
                    return Json(new { data = _kbvService.Ds_LichKhaiBao_View(absent) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Absent absent = new Absent()
                    {
                        UserAccount = user
                    };
                    //return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_View(absent));
                    return Json(new { data = _kbvService.Ds_LichKhaiBao_View(absent) }, JsonRequestBehavior.AllowGet);
                }
            }
            Absent obj = new Absent()
            {
                UserAccount = user
            };
            //return PartialView("_Khaibaovang_Table_Data", _kbvService.Ds_LichKhaiBao_View(obj));
            return Json(new { data = _kbvService.Ds_LichKhaiBao_View(obj) }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_Admin, StaticParams.HCNS_Manager)]
        public PartialViewResult Add_KhaiBaoVang(string user)
        {
            ViewBag.Locked = locked;
            ViewBag.LockMessage = lockedMessage;
            ViewBag.UserEnrollNumber = user;

            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            }
            else
            {
                ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong_KBV(userInfo.KhoaPhong.ToString(), userInfo.UserEnrollNumber);
            }

            ViewBag.DS_AbsentSymbol = _kbvService.DanhSachKhaiBao();

            //if (roles.Contains("hcns16"))
            //{
            //    ViewBag.DSKPHC = _kbvService.DanhSachKhoaPhongHC_Relation();
            //}
            //else
            //{
            //    ViewBag.DSKPHC = _kbvService.DanhSachKhoaPhongHC_Relation(_khoaphong);
            //}
            if (string.IsNullOrEmpty(user))
            {
                return PartialView("_Add_KhaiBaoVang", new Absent());
            }
            var _userinfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { UserEnrollNumber = Convert.ToInt32(user) });
            return PartialView("_Add_KhaiBaoVang", new Absent() { UserEnrollNumber = Convert.ToInt32(user), UserFullName = "[" + user + "] " + _userinfo.UserFullName });
        }

        [CustomAuthorize(StaticParams.HCNS_Admin, StaticParams.HCNS_Manager)]
        public PartialViewResult Update_KhaiBaoVang(int user, string time)
        {
            ViewBag.Locked = locked;
            ViewBag.LockMessage = lockedMessage;
            ViewBag.DS_AbsentSymbol = _kbvService.DanhSachKhaiBao();

            return PartialView("_Update_KhaiBaoVang", _kbvService.AbsentInfo(new Absent() { UserEnrollNumber = user, TimeDate = time }));
        }

        [HttpPost]
        public JsonResult Push_KhaiBaoVang(Absent obj)
        {
            int[] ngays;

            if (obj.process == "update")
            {
                ngays = new int[1];
                ngays[0] = int.Parse(obj.TimeDate.Split('/')[0].ToString());
            }
            else
            {
                ngays = obj.Ngay;
            }

            if (string.IsNullOrEmpty(obj.UserEnrollNumber.ToString()))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                obj.Status = 2;
                obj.ForType = 2;
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                obj.Status = 1;
                obj.ForType = 1;
            }
            else
            {
                obj.Status = 0;
                obj.ForType = 0;
            }
            var ngaydachon = "";

            var lsObj = new List<Absent>();
            var lsObjR = new List<AbsentR>();

            foreach (var i in ngays)
            {
                ngaydachon += (obj.Nam.ToString() + "-" + obj.Thang.ToString("00") + "-" + i.ToString("00")) + ",";

                lsObj.Add(new Absent()
                {
                    UserEnrollNumber = obj.UserEnrollNumber,
                    TimeDate = i.ToString("00") + "/" + obj.Thang.ToString("00") + "/" + obj.Nam.ToString(),
                    AbsentCode = obj.AbsentCode,
                    Workingday = 1,
                    WorkingTime = 8,
                    Lydo = obj.Lydo,
                    UserAccount = System.Web.HttpContext.Current.User.Identity.Name,
                    ForType = obj.ForType
                });
                lsObjR.Add(new AbsentR()
                {
                    UserEnrollNumber = obj.UserEnrollNumber,
                    Date = i.ToString("00") + "/" + obj.Thang.ToString("00") + "/" + obj.Nam.ToString(),
                    AbsentCode = obj.AbsentCode,
                    Reason = obj.Lydo,
                    Status = obj.Status
                });
            }

            string error = "";

            string[] arrayPers = new string[] { "A03", "A07", "A09", "A13", "A14", "A15", "A29", "A30", "A31", "A32", "A33", "A34", "A35", "A37" };

            //Không phải admin thì insert vào bảng đề xuất, admin duyệt mới vào bảng chính. A03, A09, A32
            if (!__roles.Contains(StaticParams.HCNS_Admin) 
                && arrayPers.Contains(obj.AbsentCode))
                //
            {
                if(_ccService.KhaiBaoVang_KiemTraDeXuat(obj.UserEnrollNumber, obj.AbsentCode, ngaydachon, ref error))
                {
                    return Json(new
                    {
                        result = false,
                        message = error
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
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
                var objCheck = new Absent()
                {
                    UserEnrollNumber = obj.UserEnrollNumber,
                    AbsentCode = obj.AbsentCode,
                    CheckDateString = ngaydachon,
                    ForType = obj.ForType
                };

                if (__roles.Contains(StaticParams.HCNS_Admin))
                {
                    if (_kbvService.ThemMoiKhaiBao_Admin(lsObj, objCheck, ref error))
                        return Json(new
                        {
                            result = true,
                            message = error
                        }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (_kbvService.ThemMoiKhaiBao(lsObj, objCheck, ref error))
                        return Json(new
                        {
                            result = true,
                            message = error
                        }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new
            {
                result = false,
                message = error
            }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_Admin, StaticParams.HCNS_Manager)]
        public PartialViewResult Export_KhaiBaoVang()
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

            //ViewBag.DSKPHC = _nhanVienService.DanhSachKhoaPhong();
            return PartialView("_Export_KhaiBaoVang");
        }

        [CustomAuthorize(StaticParams.HCNS_Admin)]
        public PartialViewResult Export_KhaiBaoVang_All()
        {
            ViewBag.DSKPHC = _nhanVienService.DanhSachKhoaPhongHC();
            return PartialView("_Export_KhaiBaoVang_All");
        }

        [HttpGet]
        public ActionResult Push_KhaiBaoVang(string kp = "", string tu = "", string den = "")
        {
            DataTable dt = _kbvService.Export_KhaiBaoVang(kp, tu, den);

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
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmm") + ".xlsx");
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

        [HttpGet]
        public ActionResult Push_KhaiBaoVang_All(string kp = "", string tu = "", string den = "")
        {
            DataTable dt = _kbvService.KhaiBaoVang_TongHop(kp, tu, den);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Danh sách");
                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns("C:AM").Width = 10;
                ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Rows(1, 1 + dt.Rows.Count).Height = 20;
                ws.Cell(1, 1).InsertTable(dt);
                ws.Column(2).AdjustToContents();

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + DateTime.UtcNow.AddHours(7).ToString("yyMMddHHmm") + ".xlsx");
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
        public JsonResult GetUser(string prefix = "", string kp = "")
        {
            string jsonString = "";

            if (__roles.Contains(StaticParams.HCNS_DieuDuong))
            {
                jsonString = new JavaScriptSerializer().Serialize(_kbvService.SearchUsers_DieuDuong(prefix, kp));
            }
            else
            {
                jsonString = new JavaScriptSerializer().Serialize(_kbvService.SearchUsers(prefix, kp));
            }

            return Json(jsonString, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUserAll(string prefix = "")
        {
            string jsonString = new JavaScriptSerializer().Serialize(_kbvService.SearchUsersAll(prefix));

            return Json(jsonString, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_Admin, StaticParams.HCNS_Manager)]
        [HttpPost]
        public JsonResult XoaKhaiBao(int user, string code, string date)
        {
            var _currentDate = DateTime.UtcNow.AddHours(7);
            var _currentDate_MonthOfYear = int.Parse(_currentDate.ToString("yyyyMM"));

            var _startDate = _configService.NgayBatDauChuKyLuong().DateTimeVal.Value; // start 24 tháng trước
            var _endDate = _configService.NgayBatDauChuKyLuong().DateTimeVal.Value.AddDays(1); // end 25 trong tháng hiện tại
            var _deledDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var _deledDate_MonthOfYear = int.Parse(DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMM"));


            bool returnVal = true;

            if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                // TH01. Ngày khai báo bị khóa 25 > 28
                // TH02. Ngày xóa thuộc tháng hiện tại và không quá ngày 31
                //var _addedDate = DateTime.ParseExact(obj.TimeDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
                //var _countDay = Convert.ToInt32((_currentDate - _addedDate).TotalHours) / 24;

                //if (25 <= _currentDate.Day && _currentDate.Day < 29)
                //    return Json(new { result = false, message = "Chức năng khai báo hiện tại đang tạm khóa." }, JsonRequestBehavior.AllowGet);
                //if (_deledDate_MonthOfYear < _currentDate_MonthOfYear)
                //    return Json(new { result = false, message = "Ngày " + date + " đã quá hạn cho phép" }, JsonRequestBehavior.AllowGet);

                if (locked == 1 || _deledDate < _startDate)
                {
                    return Json(new { result = false, message = "Ngày " + date + " đã quá hạn cho phép hoặc dữ liệu đã bị khóa." }, JsonRequestBehavior.AllowGet);
                }
                //if (_deledDate_MonthOfYear < _currentDate_MonthOfYear)
                //{
                //    return Json(new { result = false, message = "Ngày " + date + " đã quá hạn cho phép" }, JsonRequestBehavior.AllowGet);
                //}
            }
            if (_kbvService.XoaKhaiBao(new Absent() { UserEnrollNumber = user, AbsentCode = code, TimeDate = date }))
            {
                returnVal = true;

                //cập nhật bảng AbsentR về trạng thái chưa duyệt
                _ccService.KhaiBaoVang_UpdateStatus(user, date, 0);
            }

            return Json(new { result = returnVal }, JsonRequestBehavior.AllowGet);
        }

        #endregion Khai báo vắng

        [HttpPost]
        public JsonResult XacNhanNoiAn_Multi(string noian, string userenrollnumber)
        {
            string _errorMessage = "";
            return Json(new { result = _nhanVienService.XacNhanNoiAn_Multi(noian, userenrollnumber.Split(','), ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(StaticParams.HCNS_QuanLyTaiKhoan, StaticParams.IT)]
        public ActionResult TaiKhoan()
        {
            return View();
        }

        public JsonResult DanhSachTaiKhoan()
        {
            return Json(new { data = _nhanVienService.DanhSachTaiKhoan() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Push_Update_TaiKhoan(string userid, string taikhoan)
        {
            string _errorMessage = "";

            return Json(new { result = _nhanVienService.CapNhatTaiKhoan(userid, taikhoan, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Push_Update_Email(string userid, string email)
        {
            string _errorMessage = "";

            return Json(new { result = _nhanVienService.CapNhatEmail(userid, email, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Push_Update_PW(string userid, string pw)
        {
            string _errorMessage = "";

            return Json(new { result = _nhanVienService.CapNhatMatKhau(userid, pw, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.HCNS_Import_SalaryChange)]
        public ActionResult ImportSalaryChange()
        {
            return View("ImportSalaryChange");
        }

        [CustomAuthorize(StaticParams.HCNS_Import_NgayPC)]
        public ActionResult ImportNgayPC()
        {
            return View("ImportNgayPC");
        }
        [HttpPost]
        public ActionResult Push_ImportSalaryChange(FormCollection form, HttpPostedFileBase fileUpload)
        {
            if (Request.Files["fileUpload"].ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files["fileUpload"].FileName).ToLower();
                string connString = "";

                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["fileUpload"].FileName);

                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1)) { System.IO.File.Delete(path1); }

                    Request.Files["fileUpload"].SaveAs(path1);

                    if (extension == ".csv")
                    {
                        DataTable dt = ConvertCSVtoDataTable(path1);
                        ViewBag.Data = dt;
                    }
                    //Connection String to Excel Workbook
                    else if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        DataTable dt = ConvertXSLXtoDataTable(path1, connString);
                        ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                        DataTable dt = ConvertXSLXtoDataTable(path1, connString);

                        List<HCNS_NhanVien_Excel> lst = new List<HCNS_NhanVien_Excel>();

                        foreach (DataRow row in dt.Rows)
                        {
                            if (!String.IsNullOrEmpty(row["MA"].ToString()))
                            {
                                HCNS_NhanVien_Excel excel = new HCNS_NhanVien_Excel();
                                //Thông tin nhân sự
                                excel.HcnsNhanVien.UserEnrollNumber = int.Parse(row["MA"].ToString().Trim());
                                excel.HcnsNhanVien.Ngay = String.IsNullOrEmpty(row["NGAY"].ToString()) ? "" : row["NGAY"].ToString().Trim();
                                excel.HcnsNhanVien.Thang = String.IsNullOrEmpty(row["THANG"].ToString()) ? "" : row["THANG"].ToString().Trim();
                                excel.HcnsNhanVien.Nam = String.IsNullOrEmpty(row["NAM"].ToString()) ? "" : row["NAM"].ToString().Trim();

                                lst.Add(excel);
                            }
                        }
                        string message = "";
                        var r = _nhanVienService.SalaryChangeExcel(lst, out message);

                        if (r)
                            return Content("Upload file thành công");
                        return Content("Có lỗi xảy ra trong quá trình upload file hoặc ngày cấu hình đã tồn tại." + message);
                    }
                }
                else
                {
                    return Content("Please Upload Files in .xls, .xlsx or .csv format");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Push_ImportNgayPC(FormCollection form, HttpPostedFileBase fileUpload)
        {
            if (Request.Files["fileUpload"].ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files["fileUpload"].FileName).ToLower();
                string connString = "";

                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["fileUpload"].FileName);

                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1)) { System.IO.File.Delete(path1); }

                    Request.Files["fileUpload"].SaveAs(path1);

                    if (extension == ".csv")
                    {
                        DataTable dt = ConvertCSVtoDataTable(path1);
                        ViewBag.Data = dt;
                    }
                    //Connection String to Excel Workbook
                    else if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        DataTable dt = ConvertXSLXtoDataTable(path1, connString);
                        ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                        DataTable dt = ConvertXSLXtoDataTable(path1, connString);

                        List<HCNS_NhanVien_Excel> lst = new List<HCNS_NhanVien_Excel>();

                        foreach (DataRow row in dt.Rows)
                        {
                            if (!String.IsNullOrEmpty(row["MA"].ToString()))
                            {
                                HCNS_NhanVien_Excel excel = new HCNS_NhanVien_Excel();
                                //Thông tin nhân sự
                                excel.HcnsNhanVien.UserEnrollNumber = int.Parse(row["MA"].ToString().Trim());
                                excel.HcnsNhanVien.Ngay = String.IsNullOrEmpty(row["NGAY"].ToString()) ? "" : row["NGAY"].ToString().Trim();
                                excel.HcnsNhanVien.Thang = String.IsNullOrEmpty(row["THANG"].ToString()) ? "" : row["THANG"].ToString().Trim();
                                excel.HcnsNhanVien.Nam = String.IsNullOrEmpty(row["NAM"].ToString()) ? "" : row["NAM"].ToString().Trim();

                                lst.Add(excel);
                            }
                        }
                        string message = "";
                        var r = _nhanVienService.NgayPCExcel(lst, out message);

                        if (r)
                            return Content("Upload file thành công");
                        return Content("Có lỗi xảy ra trong quá trình upload file hoặc ngày cấu hình đã tồn tại." + message);
                    }
                }
                else
                {
                    return Content("Please Upload Files in .xls, .xlsx or .csv format");
                }
            }
            return View();
        }
        [CustomAuthorize(StaticParams.HCNS_Import_File)]
        public ActionResult ImportFile()
        {
            ViewBag.FileExcel = _nhanVienService.DS_FileExcel("Im");
            return View("ImportFile");
        }

        public JsonResult GetFileLink(int id)
        {
            var model = _nhanVienService.DS_Upload("Im").Where(x => x.ID == id);

            if (model != null)
            {
                return Json(model.Single().TENFILE, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Push_ImportFile(FormCollection form, HttpPostedFileBase fileUpload, string ID)
        {
            string result = "";
            if (Request.Files["fileUpload"].ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files["fileUpload"].FileName).ToLower();
                string connString = "";

                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["fileUpload"].FileName);

                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1)) { System.IO.File.Delete(path1); }

                    Request.Files["fileUpload"].SaveAs(path1);

                    if (extension == ".csv")
                    {
                        DataTable dt = ConvertCSVtoDataTable(path1);
                        ViewBag.Data = dt;
                    }
                    //Connection String to Excel Workbook
                    else if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        DataTable dt = ConvertXSLXtoDataTable(path1, connString);
                        ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                        DataTable dt = ConvertXSLXtoDataTable(path1, connString);
                        string message = "";
                        if (form["fileexcel"] != null)
                            result = _nhanVienService.ImportFile(dt, form["fileexcel"].ToString(), out message);
                        else
                            result = _nhanVienService.ImportFile(dt, "20", out message);

                        if (result == "0")
                            return Content("Upload file thành công");
                        return Content(result);
                    }
                }
                else
                {
                    return Content("Please Upload Files in .xls, .xlsx or .csv format");
                }
            }
            return View();
        }
        [CustomAuthorize(StaticParams.HCNS_TheoDoiThaiSan, StaticParams.IT)]
        public ActionResult TheoDoiThaiSan()
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.Departments = _nhanVienService.DSKhoaPhong();
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.Departments = _nhanVienService.DSKhoaPhong(userInfo.KhoaPhong);
            }
            ViewBag.UserInfo = userInfo;
            return View();
        }
        [HttpGet]
        public JsonResult DS_TheoDoiThaiSan(HCNS_NhanVien objsearch)
        {
            return Json(new { data = _nhanVienService.DS_TheoDoiThaiSan(objsearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_TheoDoiThaiSan()
        {
            var userInfo = _ccService.TimThongTinNhanVien(new HCNS_NhanVien() { TaiKhoan = __user });

            if (__roles.Contains(StaticParams.HCNS_Admin))
            {
                ViewBag.Departments = _nhanVienService.DSKhoaPhong();
            }
            else if (__roles.Contains(StaticParams.HCNS_Manager))
            {
                ViewBag.Departments = _nhanVienService.DSKhoaPhong(userInfo.KhoaPhong);
            }
            return PartialView("_Create_TheoDoiThaiSan");
        }
        [HttpPost]
        public JsonResult AddNew_TheoDoiThaiSan(HCNS_NhanVien obj)
        {
            string _errorMessage = "";
            return Json(new { result = _nhanVienService.AddNew_TheoDoiThaiSan(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit_TheoDoiThaiSan(HCNS_NhanVien obj)
        {
            string _errorMessage = "";
            return Json(new { result = _nhanVienService.Edit_TheoDoiThaiSan(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_TheoDoiThaiSan(HCNS_NhanVien obj)
        {
            string _errorMessage = "";
            return Json(new { result = _nhanVienService.Delete_TheoDoiThaiSan(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_TheoDoiThaiSan(HCNS_NhanVien obj)
        {
            var info = _nhanVienService.Get_TheoDoiThaiSan(obj);
            ViewBag.DSKHOAPHONG = _nhanVienService.DSKhoaPhong(info.KhoaPhong);
            ViewBag.ID = obj.ID;
            return PartialView("_Update_TheoDoiThaiSan", info);
        }
        [CustomAuthorize(StaticParams.HCNS_Export_File)]
        public ActionResult ExportFile()
        {
            ViewBag.FileExcel = _nhanVienService.DS_FileExcel("Ex");
            ViewBag.KhoaPhong = _ccService.DanhSachKhoaPhong();
            return View("ExportFile");
        }
        [HttpGet]
        public ActionResult Export_Excel(string loaifile = "", string tungay = "", string denngay = "", string khoaphong = "-1", string manv = "-1")
        {
            DataTable dt = _nhanVienService.Export_Excel(loaifile, tungay, denngay, khoaphong, manv);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Danh sách export");
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
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmm") + ".xlsx");
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
    }
}