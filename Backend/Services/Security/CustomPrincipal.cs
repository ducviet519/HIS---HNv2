using System.Linq;
using System.Security.Principal;
using System.Web;

namespace System.App.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            string[] roles = role.Split(',');

            var diff = from item in Roles where roles.Contains(item) select item;

            if (diff.Count() > 0)
                return true;
            else
                return false;
        }

        public CustomPrincipal(string Username)
        {
            
            this.Identity = new GenericIdentity(Username);
        }

        public string IPAddress { get; set; }

        public string Account { get; set; }

        public string FullName { get; set; }

        public string[] Roles { get; set; }
        public string[] RoleGroups { get; set; }
    }

    //đang sử dụng
    public class CustomPrincipalSerializeModel
    {
        public string IPAddress { get; set; }

        public string Account { get; set; }

        public string FullName { get; set; }

        public string[] Roles { get; set; }

        public string[] RoleGroups { get; set; }

        public string[] Depts { get; set; }
    }
}