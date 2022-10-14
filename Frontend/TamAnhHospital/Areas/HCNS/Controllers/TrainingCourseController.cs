using ClosedXML.Excel;
using System;
using System.App.Entities;
using System.App.Entities.Common;
using System.App.Security;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HCNS.Controllers
{
    public class TrainingCourseController : Controller
    {
        private TrainingCourse_Interface TrainingCourse_Service;
        private Employee_Interface Employee_Service;

        public TrainingCourseController(TrainingCourse_Interface trainingCourse_Interface, Employee_Interface employee_Interface)
        {
            TrainingCourse_Service = trainingCourse_Interface;
            Employee_Service = employee_Interface;
        }

        //[CustomAuthorize(StaticParams.HCNS_QLDT_View)]
        public ActionResult Index()
        {
            ViewBag.Departments = TrainingCourse_Service.ListDepartment();
            return View();
        }

        public PartialViewResult Course_Table()
        {
            var all = TrainingCourse_Service.List();

            return PartialView("_Course_Table", all);
        }

        public PartialViewResult Employee_Table(int depid = 0)
        {
            return PartialView("_Employee_Table", TrainingCourse_Service.ListEmployee(depid));
        }

        //[CustomAuthorize(StaticParams.HCNS_QLDT_Add)]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

       // [CustomAuthorize(StaticParams.HCNS_QLDT_Edit)]
        public ActionResult Update(int id)
        {
            var obj = TrainingCourse_Service.Find(id);

            return PartialView("_Update", obj);
        }

        public ActionResult UpdateClone(int courseId, int empId)
        {
            var employeeInTraining = new EmployeeInTraining()
            {
                TC_ID = courseId,
                EMP_ID = empId,
                NgayKy = (DateTime?)null
            };

            return Json(new { result = TrainingCourse_Service.UpdateClone(employeeInTraining) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateDestroy(int courseId, int empId)
        {
            EmployeeInTraining employeeInTraining = new EmployeeInTraining()
            {
                TC_ID = courseId,
                EMP_ID = empId
            };

            return Json(new { result = TrainingCourse_Service.UpdateDestroy(employeeInTraining) }, JsonRequestBehavior.AllowGet);
        }

        //[CustomAuthorize(StaticParams.HCNS_QLDT_AddNV)]
        public ActionResult Employees(int courseId)
        {
            ViewBag.CourseId = courseId;

            ViewBag.EmployeesSelected = TrainingCourse_Service.ListEmployeeInTrainingCourse(courseId);
            ViewBag.Departments = TrainingCourse_Service.ListDepartment();

            return PartialView("_Employees");
        }

        [HttpPost]
        public ActionResult Save(TrainingCourse trainingCourse, FormCollection formCollection)
        {
            var id = trainingCourse.ID;

            var r = false;

            //CREATE NEW
            trainingCourse.DateFrom = DateTime.ParseExact(formCollection["DateFrom"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            trainingCourse.DateTo = DateTime.ParseExact(formCollection["DateTo"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (id == 0)
            {
                if (TrainingCourse_Service.Insert(trainingCourse) > 0)
                {
                    r = true;
                }
            }
            else
            {
                if (TrainingCourse_Service.Update(trainingCourse))
                {
                    r = true;
                }
            }

            return Json(new { result = r }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Print(int id)
        {
            return PartialView("_Print");
        }

        [HttpGet]
        public ActionResult Download(int id)
        {
            var obj = TrainingCourse_Service.Find(id);
            DataTable dataTable = TrainingCourse_Service.ExportExcel(id);
            dataTable.Columns.Add("Chữ ký");

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Danh sách đào tạo");
                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns("C:AM").Width = 20;
                ws.Row(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Rows(6, 6 + dataTable.Rows.Count).Height = 20;

                ws.Cell(7, 1).InsertTable(dataTable);
                ws.Column(2).AdjustToContents();
                ws.Cell("C1").Value = "Danh sách tham gia đào tạo";
                ws.Cell("C1").Style.Font.FontSize = 22;
                ws.Cell("C1").Style.Font.Bold = true;
                ws.Cell("A3").Value = "Nội dung đào tạo: ";
                ws.Cell("A4").Value = "Người đào tạo: ";
                ws.Cell("A5").Value = "Thời gian: ";
                ws.Cell("C3").Value = obj.Name;
                ws.Cell("C4").Value = "";
                ws.Cell("C5").Value = "Từ " + obj.DateFrom.ToString("dd/MM/yyyy") + " đến " + obj.DateTo.ToString("dd/MM/yyyy");
                //ws.Cell("B" + (8 + dataTable.Rows.Count).ToString()).Value = "Ngày " + DateTime.Now.ToString("dd") + " tháng " + DateTime.Now.ToString("MM") + " năm " + DateTime.Now.ToString("yyyy");
                ws.Cell("B" + (14 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell("B" + (9 + dataTable.Rows.Count).ToString()).Value = "Xác nhận của giảng viên";
                ws.Cell("F" + (9 + dataTable.Rows.Count).ToString()).Value = "Lập biểu";
                ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(9 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var range1 = ws.Range(ws.Cell(8, 1).Address, ws.Cell(7 + dataTable.Rows.Count, 1).Address);
                var range2 = ws.Range(ws.Cell(8, 3).Address, ws.Cell(7 + dataTable.Rows.Count, dataTable.Columns.Count).Address);
                var range3 = ws.Range(ws.Cell(7, 1).Address, ws.Cell(7 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                range2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                range3.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                range3.Style.Border.InsideBorderColor = XLColor.Black;
                range3.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                range3.Style.Border.OutsideBorderColor = XLColor.Black; ;

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Exported.xls");
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

        public ActionResult DanhSach(int courseId)
        {
            ViewBag.CourseID = courseId;
            IEnumerable<Employee> employees = TrainingCourse_Service.ListEmployeeInTrainingCourse(courseId);
            var username = System.Web.HttpContext.Current.User.Identity.Name;

            ViewBag.IsExisted = TrainingCourse_Service.CheckExists(username, courseId);

            return View(employees);
        }

        public ActionResult Signature(int id, int courseId)
        {
            ViewBag.ID = id;
            ViewBag.CourseID = courseId;
            return PartialView("_Signature");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PushSignature(FormCollection formCollection)
        {
            var empId = int.Parse(formCollection["ID"].ToString());
            var courseID = int.Parse(formCollection["courseID"].ToString());
            var fileName = formCollection["fileUpload"].ToString();
            return Json(new { result = TrainingCourse_Service.UpdateSignature(empId, courseID, fileName) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TableEmployee(int dept = 0, int courseId = 0)
        {
            if (dept == 0)
                ViewBag.Employees = TrainingCourse_Service.ListEmployeeNotInTrainingCourse(courseId);
            else
                ViewBag.Employees = TrainingCourse_Service.ListEmployeeNotInTrainingCourse(courseId, dept);

            return PartialView("_TableEmployee");
        }

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(FormCollection form, HttpPostedFileBase file)
        {
            if (Request.Files["file"].ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files["file"].FileName).ToLower();
                string connString = "";

                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["file"].FileName);
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1))
                    { System.IO.File.Delete(path1); }
                    Request.Files["file"].SaveAs(path1);
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

                        TrainingCourse_Service.ImportExcel(dt);
                    }
                }
                else
                {
                    ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                }
            }
            return View();
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

        public PartialViewResult TrainingCourseWithEmpID(int empId)
        {
            ViewBag.EmployeeID = empId;
            return PartialView("_TrainingCourseWithEmpID", TrainingCourse_Service.TrainingCourseWithEmpID(empId));
        }

        public ActionResult ExportExcelTCWithEmpID(int empId)
        {
            var empObj = Employee_Service.Find(empId);
            DataTable dataTable = TrainingCourse_Service.ExportTrainingCourseWithEmpID(empId);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Danh sách");
                ws.Style.Font.FontName = "Times New Roman";
                ws.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns("C:AM").Width = 20;
                ws.Row(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Rows(6, 6 + dataTable.Rows.Count).Height = 20;

                ws.Cell(7, 1).InsertTable(dataTable);
                ws.Column(2).AdjustToContents();
                ws.Cell("C1").Value = "Danh sách khóa đào tạo đào tạo";
                ws.Cell("C1").Style.Font.FontSize = 22;
                ws.Cell("C1").Style.Font.Bold = true;
                //
                ws.Range("A3:B3").Row(1).Merge();
                ws.Cell("A3").Value = "Họ và tên: ";
                ws.Cell("C3").Value = empObj.UserFullName;
                //
                ws.Range("A4:B4").Row(1).Merge();
                ws.Cell("A4").Value = "Khoa/Phòng: ";
                ws.Cell("C4").Value = empObj.PhongKhoaHC;
                //
                ws.Range("A5:B5").Row(1).Merge();
                ws.Cell("A5").Value = "Vị trí: ";
                ws.Cell("C5").Value = empObj.ChucDanh;
                //
                //ws.Cell("B" + (14 + dataTable.Rows.Count).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Cell("B" + (9 + dataTable.Rows.Count).ToString()).Value = "Xác nhận của giảng viên";
                //ws.Cell("F" + (9 + dataTable.Rows.Count).ToString()).Value = "Lập biểu";
                //ws.Row(8 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //ws.Row(9 + dataTable.Rows.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                var range1 = ws.Range(ws.Cell(8, 1).Address, ws.Cell(7 + dataTable.Rows.Count, 1).Address);
                var range2 = ws.Range(ws.Cell(8, 3).Address, ws.Cell(7 + dataTable.Rows.Count, dataTable.Columns.Count).Address);
                var range3 = ws.Range(ws.Cell(7, 1).Address, ws.Cell(7 + dataTable.Rows.Count, dataTable.Columns.Count).Address);

                range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                range2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                range3.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                range3.Style.Border.InsideBorderColor = XLColor.Black;
                range3.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                range3.Style.Border.OutsideBorderColor = XLColor.Black; ;

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Charset = "";
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Exported.xls");

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