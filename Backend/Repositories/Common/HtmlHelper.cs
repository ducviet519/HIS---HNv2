namespace System.Web.Mvc.Html
{
    using System.Linq;

    public static class HtmlRequestHelper
    {
        //@Html.Controller();
        //@Html.Action();
        //@Html.Id();
        //@Html.ResolveImageAdmin('image_name');
        //@Html.ResolveImageUser('image_name');

        public static string GetID(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("id"))
                return (string)routeValues["id"];
            else if (HttpContext.Current.Request.QueryString.AllKeys.Contains("id"))
                return HttpContext.Current.Request.QueryString["id"];

            return string.Empty;
        }

        public static string GetController(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }

        public static string GetAction(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
                return (string)routeValues["action"];

            return string.Empty;
        }

        public static string ResolveImageAdmin(this HtmlHelper helper, string relativeUrl)
        {
            if (VirtualPathUtility.IsAppRelative(relativeUrl))
            {
                return VirtualPathUtility.ToAbsolute(relativeUrl);
            }
            else
            {
                var curPath = "~/areas/admin/files/images/image";
                var curDir = VirtualPathUtility.GetDirectory(curPath);
                return VirtualPathUtility.ToAbsolute(VirtualPathUtility.Combine(curDir, relativeUrl));
            }
        }

        public static string ResolveImageUser(this HtmlHelper helper, string relativeUrl, string type)
        {
            if (VirtualPathUtility.IsAppRelative(relativeUrl))
            {
                return VirtualPathUtility.ToAbsolute(relativeUrl);
            }
            else
            {
                var curPath = "";
                switch (type)
                {
                    case "thumb":
                        curPath = "~/files/uploads/thumb/image";
                        break;

                    case "large":
                        curPath = "~/files/uploads/large/image";
                        break;

                    case "original":
                        curPath = "~/files/uploads/original/image";
                        break;

                    default:
                        curPath = "~/files/uploads/image";
                        break;
                }

                var curDir = VirtualPathUtility.GetDirectory(curPath);
                return VirtualPathUtility.ToAbsolute(VirtualPathUtility.Combine(curDir, relativeUrl));
            }
        }
    }
}