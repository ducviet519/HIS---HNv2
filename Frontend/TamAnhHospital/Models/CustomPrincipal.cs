using System.Linq;

namespace TamAnhHospital.Models
{
    public class CustomSerializeModel
    {
        public string Account { get; set; }
        public string DisplayName { get; set; }
        public string IP { get; set; }
        public string Description { get; set; }
        public string Roles { get; set; }

        public bool IsInRole(string role)
        {
            if (Roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}