namespace System.App.Entities.HCNS
{
    public class ChamAn
    {
        public int UserEnrollNumber { get; set; }

        public string UserFullName { get; set; }

        public string KhoaPhong { get; set; }

        public int Val_01 { get; set; }
        public int Val_02 { get; set; }
        public int Val_03 { get; set; }
        public int Val_04 { get; set; }
        public int Val_05 { get; set; }
        public int Val_06 { get; set; }
        public int Val_07 { get; set; }
        public int Val_08 { get; set; }
        public int Val_09 { get; set; }
        public int Val_10 { get; set; }
        public int Val_11 { get; set; }
        public int Val_12 { get; set; }
        public int Val_13 { get; set; }
        public int Val_14 { get; set; }
        public int Val_15 { get; set; }
        public int Val_16 { get; set; }
        public int Val_17 { get; set; }
        public int Val_18 { get; set; }
        public int Val_19 { get; set; }
        public int Val_20 { get; set; }
        public int Val_21 { get; set; }
        public int Val_22 { get; set; }
        public int Val_23 { get; set; }
        public int Val_24 { get; set; }
        public int Val_25 { get; set; }
        public int Val_26 { get; set; }
        public int Val_27 { get; set; }
        public int Val_28 { get; set; }
        public int Val_29 { get; set; }
        public int Val_30 { get; set; }

        public string NgayChamCong { get; set; }
    }
    public class Person_CA
    {
        public string UserEnrollNumber { get; set; }
        public string Ngay { get; set; }
        public string ThoiDiem { get; set; }
        public string Scores { get; set; }
    }

    public class Update_ChamAn
    {
        public int UserEnrollNumber { get; set; }
        public string ThoiDiem { get; set; }
        public string Ngay_CC { get; set; }
        public int GiaTri { get; set; }
    }
}