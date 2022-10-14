using System.Web.Mvc;

namespace TamAnhHospital.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Cập nhật điện thoại tự đông",
                "admin/cap-nhat-dien-thoai",
                new { action = "CapNhatSoDienThoaiTuDong", controller = "Tool", id = UrlParameter.Optional },
                new string[] { "Admin.Controllers" }
            );

            context.MapRoute(
                "Đổi PID",
                "admin/ghep-benh-nhan",
                new { action = "GhepMaBenhNhan", controller = "Tool", id = UrlParameter.Optional },
                new string[] { "Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Admin.Controllers" }
            );
        }
    }
}