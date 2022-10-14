namespace System.App.Entities.HCNS
{
    public class DanhBaDienThoai
    {
        public int ID { get; set; }

        public int Building { get; set; }

        public string Floor { get; set; }

        public string Dept { get; set; }

        public string Pos { get; set; }

        public int Num { get; set; }
        public string MOTA { get; set; }
        public string TOANHA { get; set; }
        public string TANG { get; set; }
        public string KHOAPHONG { get; set; }
        public string VITRI { get; set; }
        public string SODIENTHOAI { get; set; }
        public string TENFILE { get; set; }
    }
    public class DanhBaDienThoai_Excel
    {
        public int ID { get; set; }
        public string TOANHA { get; set; }
        public string TANG { get; set; }
        public string KHOAPHONG { get; set; }
        public string VITRI { get; set; }
        public string SODIENTHOAI { get; set; }
    }
}