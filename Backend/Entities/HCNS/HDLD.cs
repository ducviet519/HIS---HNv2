namespace System.App.Entities.HCNS
{
    //Hợp đồng lao động
    public class HDLD
    {
        public HDLD()
        {
            ID = 0;
        }

        public int ID { get; set; }
        public string ConNo { get; set; }
        public int UserEnrollNumber { get; set; }
        public int LoaiHD { get; set; }
        public string Ten_LoaiHD { get; set; }
        public string NgayKy { get; set; }
        public string NgayHH { get; set; }
        public string NgayDG { get; set; }
        public string NgayUD { get; set; }
        public int DaHuy { get; set; }
        public string NgayTDLuong { get; set; }
        public string NgayVao { get; set; }
        public string NgayNghi { get; set; }
        public string UserFullCode { get; set; }
        public string UserFullName { get; set; }
        public string KhoaPhong { get; set; }
        public string ChucDanh { get; set; }
        public string ChucNang { get; set; }
        public int TrangThai { get; set; }
        public string TK_NgayHetHanTu { get; set; }
        public string TK_NgayHetHanDen { get; set; }
    }
}