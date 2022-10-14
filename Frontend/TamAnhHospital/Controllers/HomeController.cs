using System;
using System.App.Entities.Common;
using System.App.Security;
using System.Web.Mvc;

namespace TamAnhHospital.Controllers
{
    public class HomeController : BaseController
    {
        //ChamCong_Service _chamcong = null;

        public HomeController()
        {
            //_chamcong = new ChamCong_Service();

            //_chamcong.XuLyDuLieuTuMayChamCong("25/02/2019", "24/03/2019");

            //try
            //{
            //    Session["Roles"] = User.Roles;
            //}
            //catch (System.Exception e)
            //{
            //    FormsAuthentication.SignOut();
            //}
        }

        public ActionResult Welcome()
        {
            DateTime time = new DateTime(2018, 11, 22);

            return Content(time.GetWeekOfMonth().ToString());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Menu()
        {
            return PartialView();
        }
    }
}