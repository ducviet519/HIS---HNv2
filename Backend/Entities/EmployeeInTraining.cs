using System.ComponentModel.DataAnnotations;

namespace System.App.Entities
{
    public class EmployeeInTraining
    {
        [Key]
        public Guid ID { get; set; }

        public int TC_ID { get; set; }

        public int EMP_ID { get; set; }

        public string CHUKY { get; set; }

        public DateTime? NgayKy { get; set; }
    }
}