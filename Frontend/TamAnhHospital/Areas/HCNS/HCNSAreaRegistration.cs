using System.Web.Mvc;

namespace TamAnhHospital.Areas.HCNS
{
    public class HCNSAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HCNS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HCNS_default",
                "HCNS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "HCNS.Controllers" }
            );
        }
    }
}