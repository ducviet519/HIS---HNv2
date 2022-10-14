using System;
using System.App.Services.Interfaces;
using System.Web.Mvc;

namespace HCNS.Controllers
{
    public class BCSKController : Controller
    {
        private readonly Employee_Interface _employeeService;

        public BCSKController(Employee_Interface employee_Interface)
        {
            _employeeService = employee_Interface;
        }

        public ActionResult Index(string month = "")
        {
            string time = "";

            if (string.IsNullOrEmpty(month))
                time = DateTime.UtcNow.AddHours(7).ToString("MMyy");
            else
                time = month + DateTime.UtcNow.AddHours(7).ToString("yy");

            return View(_employeeService.SucKhoeNhanVien(time));
        }
    }
}