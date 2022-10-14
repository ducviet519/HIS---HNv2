using System.ComponentModel.DataAnnotations;

namespace TamAnhHospital.Models
{
    public class UserModel
    {
        public string Domain { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}