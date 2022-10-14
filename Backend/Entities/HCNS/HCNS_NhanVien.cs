namespace System.App.Entities.HCNS
{
    public class HCNS_NhanVien
    {
        public HCNS_NhanVien()
        {
            UserEnrollNumber = 0;
            //LoaiNV = "TA";

            QGThT = "0";
            TinhThT = "0";
            QuanThT = "0";
            PhuongThT = "0";
            QGTT = "0";
            TinhTT = "0";
            QuanTT = "0";
            PhuongTT = "0";
            TTSK = String.Empty;
            ThongTinSucKhoe = "";
            NgayPhuCap = "";
            TinhLuong = 1;
        }

        public int UserEnrollNumber { get; set; }

        public string UserFullCode { get; set; }

        public string UserFullName { get; set; }

        public string TATitle { get; set; }

        public string ChucDanh { get; set; }

        public string NgaySinh { get; set; }

        public string GioiTinh { get; set; }

        public string LoaiCMT { get; set; }

        public string SoCMT { get; set; }

        public string CapNgay { get; set; }

        public string NoiCap { get; set; }

        public string DaNghi { get; set; }

        public string SoTK1 { get; set; }

        public string NganHang1 { get; set; }

        public string SoTK2 { get; set; }

        public string NganHang2 { get; set; }

        public string NoiSinh { get; set; }

        public string NoiSinhCT { get; set; }

        public string TinhThanh { get; set; }

        public string Quan { get; set; }

        public string Phuong { get; set; }

        public string DcTT { get; set; }

        public string NgayPhuCap { get; set; }

        public string DanToc { get; set; }

        public string QuocTich { get; set; }

        public string TonGiao { get; set; }

        public string ThongTinSucKhoe { get; set; }

        public string ThongTinHonNhan { get; set; }

        public string NgayThayDoiLuong { get; set; }

        public string MSTCN { get; set; }

        public int QuanLy { get; set; }


        public int KhoaPhong { get; set; }
        public int NoiAn { get; set; }
        public int ToaNha { get; set; }
        public int Tang { get; set; }
        public string ViTri { get; set; }
        public string SoDienThoai { get; set; }
        public int ID { get; set; }

        public string Ten_KhoaPhong { get; set; }

        public int PhongKhoaHC { get; set; }

        public string Ten_PhongKhoaHC { get; set; }

        public string TAEmail { get; set; }

        public int SDTNB { get; set; }

        public string SDT1 { get; set; }

        public string SDT2 { get; set; }

        public string Email { get; set; }

        public string NgayVao { get; set; }

        public int DTCC { get; set; }

        public string Ten_DTCC { get; set; }

        public string SoGioLamViec { get; set; }

        public int EmailNhanLuong { get; set; }

        public string LoaiNV { get; set; }

        public int TrangThai { get; set; }

        public string DoiTuong { get; set; }

        public string QGThT { get; set; }

        public string TinhThT { get; set; }

        public string QuanThT { get; set; }

        public string PhuongThT { get; set; }

        public string DcThT { get; set; }

        public string QGTT { get; set; }

        public string TinhTT { get; set; }

        public string QuanTT { get; set; }

        public string PhuongTT { get; set; }

        public string TTHonNhan { get; set; }

        public string TTSK { get; set; }

        public string AnhDaiDien { get; set; }

        public string NgayNghi { get; set; }

        public string NgayLVCC { get; set; }

        public string LyDoNghi { get; set; }

        public string SoBHXH { get; set; }

        public string SoTheBHXH { get; set; }

        public string NgayTGBHXH { get; set; }

        public string LoaiDongBHXH { get; set; }
        public string MucDongBHXH { get; set; }
        public string TaiKhoan { get; set; }
        public string UPN { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public string TimeStr { get; set; }
        public int UType { get; set; }
        public int TinhLuong { get; set; }
        public string Ngay { get; set; }
        public string Thang { get; set; }
        public string Nam { get; set; }
        public string NgayTinhPhep { get; set; }
        public int KhoaSoLieu { get; set; }
        public string PhanLoai { get; set; }
        public string NgayThongBao { get; set; }
        public string NgayBDMangThai { get; set; }
        public string NgayDuSinh { get; set; }
        public string TuoiThai { get; set; }
        public string NgaySinhCon { get; set; }
        public string NgayConMat { get; set; }
        public string GhiChu { get; set; }
        public string SoThangNghiSinh { get; set; }
        public string NghiSinhTuNgay { get; set; }
        public string NghiSinhDen { get; set; }
        public string GiamBH_TuThang { get; set; }
        public string GiamBH_DenThang { get; set; }
        public string SoNgayNghi_DS { get; set; }
        public string NgayNghi_DS { get; set; }
        public string Nghi_DS_DenNgay { get; set; }
        public string NgayDiLamLai { get; set; }
        public string VeSomDenNgay { get; set; }
        public int CongDoan { get; set; }
    }

    public class HCNS_NhanVien_Excel
    {
        public HCNS_NhanVien_Excel()
        {
            HcnsNhanVien = new HCNS_NhanVien();
            BangCap = new BangCap();
            ChungChi = new ChungChi();
            ChungChiHanhNghe = new ChungChiHanhNghe();
        }

        public HCNS_NhanVien HcnsNhanVien { get; set; }

        public BangCap BangCap { get; set; }

        public ChungChi ChungChi { get; set; }

        public ChungChiHanhNghe ChungChiHanhNghe { get; set; }
    }
    public class XacNhanAn_Multi
    {
        public string MASO { get; set; }
        public string MA { get; set; }
     }
}