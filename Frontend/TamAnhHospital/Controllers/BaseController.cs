using Newtonsoft.Json;
using System.App.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TamAnhHospital.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            
        }

        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

        [ChildActionOnly]
        public ActionResult TopBar()
        {
            return PartialView("_TopBar");
        }
    }
}