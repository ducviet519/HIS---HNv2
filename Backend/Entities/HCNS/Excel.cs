namespace System.App.Entities.HCNS
{
    public class Excel_GiaiTrinh
    {
        public string NGAY_BD { get; set; }
        public string NGAY_KT { get; set; }
        public string MANV { get; set; }
        public string KhoaPhong { get; set; }
        public string TTXL { get; set; }
    }

    public class Excel_TGLV
    {
        public string NGAY_BD { get; set; }
        public string NGAY_KT { get; set; }
        public string MANV { get; set; }
        public string KhoaPhong { get; set; }
        public int DuyetOT { get; set; }
        public int DuyetTT { get; set; }
        public int PhanLoai { get; set; }
    }
}