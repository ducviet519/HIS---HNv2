using System;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Security;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HCNS.Controllers
{
    public class HDLDController : Controller
    {
        private readonly HDLD_Interface _HDLDService;
        private readonly NhanVien_Interface _nhanVienService;

        public HDLDController(HDLD_Interface hdld_Interface, NhanVien_Interface nhanvien_Interface)
        {
            _HDLDService = hdld_Interface;
            _nhanVienService = nhanvien_Interface;
        }

        [CustomAuthorize(StaticParams.HCNS_HDLD, StaticParams.IT)]
        public ActionResult Index()
        {
            ViewBag.LoaiHD = _HDLDService.DS_LoaiHopDong();
            ViewBag.DSKPHC = _nhanVienService.DanhSachKhoaPhongHC();
            return View();
        }

        public PartialViewResult Table_Data(string a = "", string id = "", string pkhc = "", int loaihd = 0, int trangthai = 3, string fromHH = "", string toHH = "")
        {
            var obj = new HDLD();

            if (a == "TimKiem")
            {
                obj.ChucNang = "TimKiem";
                obj.UserFullName = id;
                obj.KhoaPhong = pkhc;
                obj.LoaiHD = loaihd;
                obj.TrangThai = trangthai;
                obj.TK_NgayHetHanTu = fromHH;
                obj.TK_NgayHetHanTu = toHH;
            }

            return PartialView("_Table_Data", _HDLDService.DanhSachHopDong(obj));
        }

        [CustomAuthorize(StaticParams.HCNS_HDLD_ThaoTac)]
        public PartialViewResult Create()
        {
            ViewBag.LoaiHD = _HDLDService.DS_LoaiHopDong();

            return PartialView("_Create");
        }

        [CustomAuthorize(StaticParams.HCNS_HDLD_ThaoTac)]
        public PartialViewResult Update(int id)
        {
            ViewBag.LoaiHD = _HDLDService.DS_LoaiHopDong();
            return PartialView("_Update", _HDLDService.HopDongInfo(new HDLD() { ID = id }));
        }

        [CustomAuthorize(StaticParams.HCNS_HDLD_ThaoTac)]
        public PartialViewResult UploadExcel()
        {
            return PartialView("_UploadExcel");
        }

        [HttpPost]
        public ActionResult Push_UploadExcel()
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                List<HDLD> lst = new List<HDLD>();
                bool result = false;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    string path = Server.MapPath("~/Content/Uploads/");
                    file.SaveAs(path + file.FileName);
                    string extension = Path.GetExtension(file.FileName).ToLower();
                    string connString = "", fname = "";
                    string[] validFileTypes = { ".xls", ".xlsx", ".csv" };
                    DataTable _tableData = null;
                    string filePath = string.Format(@"{0}\{1}", Server.MapPath("~/Content/Uploads"), file.FileName);

                    // Checking for Internet Explorer
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    if (validFileTypes.Contains(extension))
                    {
                        if (extension == ".csv")
                        {
                            _tableData = ConvertCSVtoDataTable(filePath);
                        }
                        else if (extension.Trim() == ".xls")
                        {
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            _tableData = ConvertXSLXtoDataTable(filePath, connString);
                        }
                        else if (extension.Trim() == ".xlsx")
                        {
                            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            _tableData = ConvertXSLXtoDataTable(filePath, connString);

                            foreach (DataRow row in _tableData.Rows)
                            {
                                if (!String.IsNullOrEmpty(row["MA_NV"].ToString()))
                                {
                                    HDLD excel = new HDLD();
                                    excel.UserEnrollNumber = int.Parse(row["MA_NV"].ToString().Trim());
                                    excel.ConNo = String.IsNullOrEmpty(row["SO_HD"].ToString()) ? "" : row["SO_HD"].ToString();
                                    excel.LoaiHD = String.IsNullOrEmpty(row["MA_LOAI_HD"].ToString()) ? 0 : int.Parse(row["MA_LOAI_HD"].ToString());
                                    excel.NgayKy = String.IsNullOrEmpty(row["NGAY_KY"].ToString()) ? "" : DateTime.Parse(row["NGAY_KY"].ToString().Trim()).ToString("dd/MM/yyyy");
                                    excel.NgayHH = String.IsNullOrEmpty(row["Ngay_HH"].ToString()) ? "" : DateTime.Parse(row["Ngay_HH"].ToString().Trim()).ToString("dd/MM/yyyy");
                                    excel.NgayDG = String.IsNullOrEmpty(row["Ngay_DG"].ToString()) ? "" : DateTime.Parse(row["Ngay_DG"].ToString().Trim()).ToString("dd/MM/yyyy");
                                    lst.Add(excel);
                                }
                            }
                        }
                        result = _HDLDService.CapNhatExcel(lst);
                    }
                }

                if (result)
                {
                    return Json("File Uploaded Successfully!");
                }
                else
                {
                    return Json("Upload failed");
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_HDLD(HDLD obj)
        {
            bool val = false;
            string message = "";

            if (_HDLDService.KiemTraTrung(obj, out message))
            {
                return Json(new { result = val, message = "" }, JsonRequestBehavior.AllowGet);
            }

            if (obj.ID == 0)
            {
                val = _HDLDService.ThemMoiHopDong(obj);
            }
            else
            {
                val = _HDLDService.CapNhatHopDong(obj);
            }

            return Json(new { result = val, message = "" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DSHD_DaKy(int userid)
        {
            if (userid == 0)
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            return Json(new { result = true, data = _HDLDService.DanhSachHopDongDaKy(userid) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CapSoHopDong(int userid, int mahd, string ngayky)
        {
            return Json(new { result = _HDLDService.CapSoHopDong("TA", userid, mahd, ngayky) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult HuyHopDong(int id)
        {
            return Json(new { result = _HDLDService.HuyHopDong(id) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update_Ngay(int id = 0, string date = "", string type = "")
        {
            return Json(new { result = _HDLDService.UpdateNgay(id, date, type) }, JsonRequestBehavior.AllowGet);
        }
    }
}