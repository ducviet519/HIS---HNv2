using System.Web;

namespace System.App.Entities.HCNS
{
    public class HCNS_CongVan
    {
        public int ID { get; set; }
        public string SO_CV { get; set; }
        public string NOI_DUNG { get; set; }
        public int NOI_NHAN { get; set; }
        public string TEN_NOI_NHAN { get; set; }
        public int NGUOI_NHAN { get; set; }
        public string MA_NGUOI_NHAN { get; set; }
        public string TEN_NGUOI_NHAN { get; set; }
        public string NGAY_GUI { get; set; }
        public int THANG_GUI { get; set; }
        public string NGAY_NHAN { get; set; }
        public int THANG_NHAN { get; set; }
        public int NGUOI_THUC_HIEN { get; set; }
        public string MA_NGUOI_THUC_HIEN { get; set; }
        public string TEN_NGUOI_THUC_HIEN { get; set; }
        public string NGAY_XL_DU_KIEN { get; set; }
        public string THOI_GIAN_XL_DU_KIEN { get; set; }
        public string NGAY_XL_THUC_TE { get; set; }
        public string THOI_GIAN_XL_THUC_TE { get; set; }
        public string NGAY_TAO { get; set; }
        public string NGUOI_TAO { get; set; }
        public string NGAY_CAP_NHAT { get; set; }
        public string NGUOI_CAP_NHAT { get; set; }
        public string FILE_PATH { get; set; }
        public string TEN_FILE { get; set; }
        public HttpPostedFileBase LINK_FILE { get; set; }
        public int TRANG_THAI { get; set; }
    }
}