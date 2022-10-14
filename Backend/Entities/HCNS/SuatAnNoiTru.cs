namespace System.App.Entities.HCNS
{
    public class SuatAnNoiTru_HienDien
    {
        public string KHOAPHONG { get; set; }
        public string TEN_KHOAPHONG { get; set; }
        public string MABN { get; set; }
        public string HOTEN { get; set; }
        public string PHAI { get; set; }
        public string NAMSINH { get; set; }
        public string MAICD { get; set; }
        public string CHANDOAN { get; set; }
        public string MA_BACSI { get; set; }
        public string TEN_BACSI { get; set; }
        public string NGAY_VAO_VIEN { get; set; }
        public int NGAY_DIEU_TRI { get; set; }
        public string MA_CHE_BIEN { get; set; }
        public string DANG_CHE_BIEN { get; set; }
        public string NGAY { get; set; }
        public string NGAY_CAP_NHAT { get; set; }
        public string NGUOI_CAP_NHAT { get; set; }
        public int BUA_SANG { get; set; }
        public int BUA_TRUA { get; set; }
        public int BUA_PHU { get; set; }
        public int BUA_TOI { get; set; }
    }

    public class SuatAnNoiTru_KhaiBao
    {
        public string KHOA_PHONG { get; set; }
        public string MABN { get; set; }
        public string HOTEN { get; set; }
        public string NGAY { get; set; }
        public string MA_CHE_BIEN { get; set; }
        public string NGAY_CAP_NHAT { get; set; }
        public string NGUOI_CAP_NHAT { get; set; }
        public string DOI_TUONG_DK { get; set; }
        public string DOI_TUONG { get; set; }
        public string NHOM_BENH { get; set; }
        public int BUA_SANG { get; set; }
        public int BUA_TRUA { get; set; }
        public int BUA_PHU { get; set; }
        public int BUA_TOI { get; set; }
    }

    public class SuatAnNoiTru_NhomBenh
    {
        public int ID { get; set; }
        public string TEN_NB { get; set; }
    }
    public class SuatAnNoiTru_DoiTuong
    {
        public int ID { get; set; }
        public string TEN_DOITUONG { get; set; }
    }

    public class SuatAnNoiTru_CheBien
    {
        public string KY_HIEU { get; set; }
        public string DANG_CHE_BIEN { get; set; }
        public string GHI_CHU { get; set; }
    }

    public class SuatAnNoiTru_SoLuong
    {
        public string TYPE { get; set; }
        public int S1 { get; set; }
        public int S2 { get; set; }
        public int S3 { get; set; }
        public int S4 { get; set; }
        public int S5 { get; set; }
        public int S6 { get; set; }
        public int S7 { get; set; }
        public int TOTAL { get; set; }
        public DateTime FROM { get; set; }
        public DateTime TO { get; set; }
        public string DISPLAY { get; set; }
    }
}