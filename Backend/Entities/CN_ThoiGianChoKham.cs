using System.ComponentModel.DataAnnotations;

namespace System.App.Entities
{
    public class CN_ThoiGianChoKham
    {
        [Key]
        public Guid ID { get; set; }

        public int BD_ID { get; set; }

        public string ThoiGian { get; set; }

        public string NhanXet { get; set; }

        public DateTime NgayTao { get; set; }

        public string NguoiTao { get; set; }

        public DateTime NgaySua { get; set; }

        public string NguoiSua { get; set; }
    }
}