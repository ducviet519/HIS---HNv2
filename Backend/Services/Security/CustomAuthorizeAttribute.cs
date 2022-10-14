using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using System.Web.Security;

namespace System.App.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }

        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string[] authorizedRoleGroup = null;
            
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                //var sessionRoles = HttpContext.Current.Session["Roles"];

                var authorizedUsers = CurrentUser.Account;
                var authorizedRoles = Roles.Split(',');

                //authorizedRoleGroup = Roles.Split(',') ?? null;
                //authorizedRoleGroup = (string[])HttpContext.Current.Session["Roles"] ?? null;
                var rolesContext = (string[])HttpContext.Current.Items["Roles"];
                
                if (rolesContext == null)
                {
                    FormsAuthentication.SignOut();
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login", area = "" }));
                }
                else
                {
                    authorizedRoleGroup = rolesContext;

                    //authorizedRoleGroup = (string[])HttpContext.Current.Session["Roles"] ?? (string[])CurrentUser.Roles;

                    //var authorizedRoleGroup = (string[])HttpContext.Current.Session["Roles"] ?? (string[])CurrentUser.Roles;

                    //Lấy thông tin user hiện tại
                    Users = String.IsNullOrEmpty(Users) ? authorizedUsers : Users;

                    //Thông tin quyền user có thể truy cập từ controller
                    //Roles = String.IsNullOrEmpty(Roles) ? authorizedRoleGroup : Roles;

                    if (authorizedRoleGroup.Length > 0)
                    {
                        //Kiểm tra quyền có trong danh sách không
                        //if (!CurrentUser.IsInRole(Roles))

                        int containtCheck = (from item in authorizedRoles where authorizedRoleGroup.Contains(item) select item).Count();

                        if (!authorizedRoleGroup.Contains(Roles))

                        if (containtCheck == 0)
                        {
                            filterContext.HttpContext.Session["auth"] = 0;
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied", area = "" }));
                        }
                        else
                        {
                            filterContext.HttpContext.Session["auth"] = 1;
                        }
                    }
                }
                //if (!String.IsNullOrEmpty(Users))
                //{
                //    if (!Users.Contains(CurrentUser.Account.ToString()))
                //    {
                //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied", area = "" }));
                //    }
                //}
            }
            else
            {
                base.OnAuthorization(filterContext); //returns to login url
            }
        }
    }


}