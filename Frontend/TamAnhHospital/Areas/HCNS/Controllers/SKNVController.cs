using Newtonsoft.Json;
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
using System.Web;
using System.Web.Mvc;
using TamAnhHospital.Areas.HCNS.Models;

namespace HCNS.Controllers
{
    public class SKNVController : Controller
    {
        private readonly ISKNV _sknvService;

        public SKNVController(ISKNV sknvInterface)
        {
            _sknvService = sknvInterface;
        }

        [CustomAuthorize(StaticParams.HCNS_PhanLoaiSKNV, StaticParams.IT)]
        public ActionResult PLSK()
        {
            ViewBag.Departments = _sknvService.Departments();
            return View();
        }

        public PartialViewResult PLSK_Table(SKNV obj = null)
        {
            if (!String.IsNullOrEmpty(obj.KetLuan))
                ViewBag.Tyle = _sknvService.TyLe(obj);
            else
                ViewBag.Tyle = 0;
            return PartialView("_PLSK_Table", _sknvService.List(obj));
        }

        [CustomAuthorize(StaticParams.HCNS_BieuDo_PLSK_DangTron, StaticParams.IT)]
        public ActionResult PLSK_Tron()
        {
            ViewBag.Departments = _sknvService.Departments();
            return View();
        }

        public PartialViewResult PLSK_Tron_Table(PLSK_Tron obj)
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            var valReturn = _sknvService.List_PLSK_Tron(obj);

            dataPoints.Add(new DataPoint("Không khám", valReturn.TyLeKK));
            dataPoints.Add(new DataPoint("Loại 1", valReturn.TyLeL1));
            dataPoints.Add(new DataPoint("Loại 2", valReturn.TyLeL2));
            dataPoints.Add(new DataPoint("Loại 3", valReturn.TyLeL3));
            dataPoints.Add(new DataPoint("Loại 4", valReturn.TyLeL4));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return PartialView("_PLSK_Tron_Table");
        }

        [CustomAuthorize(StaticParams.HCNS_PhanLoaiSKNV_TheoNam, StaticParams.IT)]
        public ActionResult PLSK_Nam()
        {
            ViewBag.Departments = _sknvService.Departments();
            return View();
        }

        public PartialViewResult PLSK_Nam_Table(PLSK obj = null)
        {
            return PartialView("_PLSK_Nam_Table", _sknvService.List_PLSK(obj));
        }

        [CustomAuthorize(StaticParams.HCNS_TheoDoiDienTienSucKhoe, StaticParams.IT)]
        public ActionResult TDSK()
        {
            ViewBag.Departments = _sknvService.Departments();
            return View();
        }

        public PartialViewResult TDSK_Table(TDSK obj = null)
        {
            return PartialView("_TDSK_Table", _sknvService.List_TDSK(obj));
        }

        public ActionResult BieuDo_PLSK()
        {
            return View();
        }

        public PartialViewResult BieuDo_PLSK_Table(string nam = "2018")
        {
            return PartialView("_BieuDo_PLSK_Table", _sknvService.BieuDo_PLSK(nam));
        }

        //Nguy cơ bệnh tật
        [CustomAuthorize(StaticParams.HCNS_NguyCoBenhTat, StaticParams.IT)]
        public ActionResult NCBT()
        {
            ViewBag.Departments = _sknvService.Departments();
            return View();
        }

        public ActionResult NCBT_Table(NCBT obj)
        {
            return PartialView("_NCBT_Table", _sknvService.List_NCBT(obj));
        }

        public ActionResult Upload_SKNV()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Push_Upload_SKNV(HttpPostedFileBase fileUpload)
        {
            if (Request.Files["fileUpload"].ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files["fileUpload"].FileName).ToLower();
                string connString = "";

                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Uploads/hcns/"), Request.Files["fileUpload"].FileName);

                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Uploads/hcns"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1)) { System.IO.File.Delete(path1); }

                    Request.Files["fileUpload"].SaveAs(path1);
                    DataTable dt = new DataTable();

                    if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        dt = ConvertXSLXtoDataTable(path1, connString);
                        ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                        dt = ConvertXSLXtoDataTable(path1, connString);
                    }
                    var r = _sknvService.Upload_Excel(dt); ;
                }
            }
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
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
    }
}