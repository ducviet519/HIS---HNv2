using ClosedXML.Excel;
using System.App.Entities.Common;
using System.App.Security;
using System.App.Services.Interfaces;
using System.IO;
using System.Web.Mvc;

namespace HCNS.Controllers
{
    public class DoiChieuSuatAnController : Controller
    {
        private DoiChieuSuatAn_Interface _doichieusuatan_Service;
        private TrainingCourse_Interface _TrainingCourse_Service;

        public DoiChieuSuatAnController(
            TrainingCourse_Interface trainingCourse_Interface,
            DoiChieuSuatAn_Interface chieuSuatAn_Interface)
        {
            _TrainingCourse_Service = trainingCourse_Interface;
            _doichieusuatan_Service = chieuSuatAn_Interface;
        }

        [CustomAuthorize(StaticParams.HCNS_DoiChieuSuatAn)]
        public ActionResult Index()
        {
            ViewBag.Departments = _doichieusuatan_Service.ListDepartment();
            return View();
        }

        public PartialViewResult BangDoiChieuSuatAn(string dept = "", string tungay = "", string toingay = "")
        {
            var model = _doichieusuatan_Service.DanhSachSuatAn(dept, tungay, toingay);
            return PartialView("_BangDoiChieuSuatAn", model);
        }

        public ActionResult ExportExcel(string dept = "", string tungay = "", string toingay = "")
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
    }
}