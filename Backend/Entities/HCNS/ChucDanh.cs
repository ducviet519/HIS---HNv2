namespace System.App.Entities.HCNS
{
    public class ChucDanh
    {
        public int ID { get; set; }
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public string MaChucDanh { get; set; }
        public string TenChucDanh { get; set; }
        public string PhanLoai { get; set; }
        public bool Status { get; set; }
    }
}
