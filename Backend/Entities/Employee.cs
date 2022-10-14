namespace System.App.Entities
{
    public class Employee
    {
        public int UserEnrollNumber { get; set; }

        public string UserFullCode { get; set; }

        public string UserFullName { get; set; }

        public DateTime NgaySinh { get; set; }

        public string PhongKhoaHC { get; set; }

        public string ChucDanh { get; set; }

        public string TAEmail { get; set; }

        public string CHUKY { get; set; }

        public DateTime? NgayKy { get; set; }

        public string UPN { get; set; }

        public int Total_Course { get; set; }
    }

    public class EmployeeHealth
    {
        public string Type { get; set; }

        public int Amount { get; set; }

        public int Total { get; set; }

        public float Percentage { get; set; }
    }
}