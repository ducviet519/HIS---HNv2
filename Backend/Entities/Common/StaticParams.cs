using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace System.App.Entities.Common
{
    public class StaticParams
    {
        //public static string connectionString = ConfigurationManager.ConnectionStrings["TamAnhConnection"].ConnectionString;

        public static string connectionStringWiseEyeWebOn = ConfigurationManager.ConnectionStrings["TamAnhConnectionWiseEyeWebOn"].ConnectionString;

        //public static string connectionStringSFAPI = ConfigurationManager.ConnectionStrings["TamAnhConnectionSFAPI"].ConnectionString;

        //public static string connectionStringWiseEyeWebOn2014 = ConfigurationManager.ConnectionStrings["TamAnhConnectionWiseEyeWebOn2014"].ConnectionString;

        //public static string connectionStringWiseEyeWebOnTest = ConfigurationManager.ConnectionStrings["TamAnhConnectionWiseEyeWebOnTest"].ConnectionString;

        //public static string connectionOracle = ConfigurationManager.ConnectionStrings["TamAnhOracle"].ConnectionString;

        //public static string connectionOracleTest = ConfigurationManager.ConnectionStrings["TamAnhOracleTest"].ConnectionString;

        public const string Tool_SDT = "Tool_SDT";
        public const string Tool_InLaiPhieuAn = "Tool_InLaiPhieuAn";

        public const string Role_IVF_View = "ivf10";
        public const string Role_IVF_Add = "ivf11";
        public const string Role_IVF_Edit = "ivf12";
        public const string Role_IVF_EditEx = "ivf16";

        public const string IVF_DONGRA_View = "ivf_3_xem";
        public const string IVF_DONGRA_Edit = "ivf_3_sua";
        public const string IVF_DONGRA_Edit_Admin = "ivf_3_sua_all";
        public const string IVF_DONGRA_Add = "ivf_3_them";
        public const string IVF_DONGRA_Remove = "ivf_3_huy";

        //public const string IVF_LT_Guest = "ivf_lt_guest";
        //public const string IVF_LT_User = "ivf_lt_user";
        //public const string IVF_LT_Manager = "ivf_lt_manager";
        //public const string IVF_LT_Admin = "ivf_lt_admin";

        public const string IVF_GH_BaoPhat_Scan = "IVF_GH_BaoPhat_Scan";
        public const string IVF_GH_BaoPhat_XoaAnh = "IVF_GH_BaoPhat_XoaAnh";
        public const string IVF_GH_BaoPhat_DoiChieuKoBP = "IVF_GH_BaoPhat_DoiChieuKoBP";
        public const string IVF_GH_BaoPhat_DoiChieu = "IVF_GH_BaoPhat_DoiChieu";
        public const string IVF_GH_BaoPhat_btnChuyenPhat = "IVF_GH_BaoPhat_btnChuyenPhat";
        public const string IVF_GH_BaoPhat_btnLuu = "IVF_GH_BaoPhat_btnLuu";

        //Tinh dịch đồ
        public const string IVF_TDD_View = "ivf1";
        public const string IVF_TDD_Add = "ivf2";
        public const string IVF_TDD_Approve = "ivf3";
        public const string IVF_TDD_DisApprove = "ivf4";

        /// <summary>
        /// Phân quyền lưu trữ đông phôi/noãn
        /// </summary>
        public const string IVF_LT_DanhSach = "IVF_LT_DanhSach";
        public const string IVF_LT_UpdateTT = "IVF_LT_UpdateTT";
        public const string IVF_LT_UpdateTT24h = "IVF_LT_UpdateTT24h";
        public const string IVF_LT_ViewDsChoHuy = "IVF_LT_ViewDsChoHuy";
        public const string IVF_LT_UpdateXnDsChoHuy = "IVF_LT_UpdateXnDsChoHuy";
        public const string IVF_LT_UpdateTTCong = "IVF_LT_UpdateTTCong";
        public const string IVF_LT_UpdateTTCong24 = "IVF_LT_UpdateTTCong24";
        public const string IVF_LT_Undo = "IVF_LT_Undo";
        public const string IVF_LT_UpdateQH = "IVF_LT_QuanHe";

        public const string IVF_PGS_Add = "ivf13";
        public const string IVF_PGS_Edit = "ivf14";
        public const string IVF_PGS_View = "ivf15";
        public const string IVF_PGS_DSCH = "ivf_dsch";
        public const string IVF_QUANHE_USER = "ivf_quanhe_user";
        public const string IVF_QUANHE_ADMIN = "ivf_quanhe_admin";
        public const string IVF_GIAHAN_CSKH = "ivf_giahan_cskh";
        public const string IVF_GIAHAN_CSKH_QL = "ivf_giahan_cskh_ql";
        public const string IVF_GIAHAN_LAB = "ivf_giahan_lab";
        public const string IVF_GIAHAN_THUNGAN = "ivf_giahan_thungan";
        public const string IVF_HSBA = "ivf_hsba";
        public const string IVF_QLTN = "ivf_tinnhan";
        public const string IVF_QLTN_Admin = "ivf_tinnhan_admin";
        public const string IVF_QLGT = "ivf_guithu";
        public const string IVF_QLGT_Admin = "ivf_guithu_admin";
        public const string IVF_DOICHIEU = "ivf_doichieu";

        public const string TMH_CHIDINH = "tmh_chidinh";
        public const string TMH_CHIDINH_UPDATE = "tmh_chidinh_upd";

        public const string DD_View = "DD1";
        public const string DD_Add = "DD2";
        public const string DD_Edit = "DD3";

        public const string IT = "IT";
        public const string QLCL = "QLCL";

        //public const string HCNS_EDIT_EMP_NEW = "hcns1"; //nhập, sửa thông tin nhân viên mới
        //public const string HCNS_EDIT_EMP_INFO = "hcns18"; //nhập, sửa thông tin nhân viên
        //public const string HCNS_EDIT_EMP_QUIT = "hcns3"; //nhập, sửa thông tin nhân viên nghỉ việc
        //public const string HCNS_EDIT_EMP_VIEW_CONTACT = "hcns4"; //Xem danh bạ
        //public const string HCNS_CHAMAN = "hcns5";
        //public const string HCNS_CHAMAN_ADMIN = "hcns6";
        public const string HCNS_Admin = "HCNS_Admin";
        public const string HCNS_Manager = "HCNS_Manager";
        public const string HCNS_Admin_QuanLySuatAn = "HCNS_Admin_QuanLySuatAn";
        public const string HCNS_Manager_QuanLySuatAn = "HCNS_Manager_QuanLySuatAn";
        public const string HCNS_User = "HCNS_User";
        public const string HCNS_DieuDuong = "HCNS_DieuDuong";
        public const string HCNS_KhaiBaoNhanVienMoi = "HCNS_KhaiBaoNhanVienMoi";
        public const string HCNS_KhaiBaoNhanVienMoi_View = "HCNS_KhaiBaoNhanVienMoi_View";
        public const string HCNS_SuaDuLieuCong = "HCNS_SuaDuLieuCong";
        public const string HCNS_ThemDuLieuCong = "HCNS_ThemDuLieuCong";
        public const string HCNS_XoaDuLieuCong = "HCNS_XoaDuLieuCong";
        public const string HCNS_KiemTraKhoaSoLieu = "HCNS_KiemTraKhoaSoLieu";
        public const string HCNS_KiemTraHetHanKhaiBao = "HCNS_KiemTraHetHanKhaiBao";
        public const string HCNS_ThongTinThietBi = "HCNS_ThongTinThietBi";
        public const string HCNS_DangKySuatAn = "HCNS_DangKySuatAn";        
        public const string HCNS_DoiChieuSuatAn = "HCNS_DoiChieuSuatAn";
        public const string HCNS_ThongKeSuatAn = "HCNS_ThongKeSuatAn";
        public const string HCNS_PhanLoaiSKNV = "HCNS_PhanLoaiSKNV";
        public const string HCNS_PhanLoaiSKNV_TheoNam = "HCNS_PhanLoaiSKNV_TheoNam";
        public const string HCNS_BieuDo_PLSK_DangTron = "HCNS_BieuDo_PLSK_DangTron";
        public const string HCNS_TheoDoiDienTienSucKhoe = "HCNS_TheoDoiDienTienSucKhoe";
        public const string HCNS_NguyCoBenhTat = "HCNS_NguyCoBenhTat";
        public const string HCNS_LamThemTinhTien = "HCNS_LamThemTinhTien";
        public const string HCNS_CaKhongXacDinh = "HCNS_CaKhongXacDinh";
        public const string HCNS_TongHopCong = "HCNS_TongHopCong";
        public const string HCNS_DanhSachCa = "HCNS_DanhSachCa";
        public const string HCNS_ThongTinNhanSu = "HCNS_ThongTinNhanSu";
        public const string HCNS_NhanSuNghiViec = "HCNS_NhanSuNghiViec";
        public const string HCNS_UploadThongTin = "HCNS_UploadThongTin";
        public const string HCNS_DanhBaDienThoai = "HCNS_DanhBaDienThoai";
        public const string HCNS_Update_DanhBaDienThoai = "HCNS_Update_DanhBaDienThoai";
        public const string HCNS_KhaiBaoVang = "HCNS_KhaiBaoVang";
        public const string HCNS_KhaiBaoCaLamViec = "HCNS_KhaiBaoCaLamViec";
        public const string HCNS_KhaiBaoLamThemGio = "HCNS_KhaiBaoLamThemGio";
        public const string HCNS_TongHopCongTheoKP = "HCNS_TongHopCongTheoKP";
        public const string HCNS_QuanLyGiaiTrinh = "HCNS_QuanLyGiaiTrinh";
        public const string HCNS_QuanLyVanTay = "HCNS_QuanLyVanTay";
        public const string HCNS_HDLD = "HCNS_HDLD";
        public const string HCNS_HDLD_ThaoTac = "HCNS_HDLD_ThaoTac";
        public const string HCNS_QuanLyTaiKhoan = "HCNS_QuanLyTaiKhoan";
        public const string HCNS_BangCap = "HCNS_BangCap";
        public const string HCNS_ChungChi = "HCNS_ChungChi";
        public const string HCNS_ChungChiHanhNghe = "HCNS_ChungChiHanhNghe";
        public const string HCNS_QuanLyCongVan = "HCNS_QuanLyCongVan";
        public const string HCNS_XemGioVaoRaThucTe = "HCNS_XemGioVaoRaThucTe";
        public const string HCNS_XemChiTietCongTheoNV = "HCNS_XemChiTietCongTheoNV";
        public const string HCNS_Remove_ThoiGianLamViec = "HCNS_Remove_ThoiGianLamViec";
        public const string HCNS_Add_KhaiBaoLichTrinh = "HCNS_Add_KhaiBaoLichTrinh";
        public const string HCNS_Xoa_KhaiBaoCa_Phu = "HCNS_Xoa_KhaiBaoCa_Phu";
        public const string HCNS_Add_KhaiBaoCa = "HCNS_Add_KhaiBaoCa";
        public const string HCNS_Add_KhaiBaoNhieuCa = "HCNS_Add_KhaiBaoNhieuCa";
        public const string HCNS_GioiHanCa = "HCNS_GioiHanCa";
        public const string HCNS_DanhSachCaChuaDuyet = "HCNS_DanhSachCaChuaDuyet";
        public const string HCNS_Add_ThoiGianLamViec = "HCNS_Add_ThoiGianLamViec";
        public const string HCNS_VanTayThucTe = "HCNS_VanTayThucTe";
        public const string HCNS_Import_SalaryChange = "HCNS_Import_SalaryChange";
        public const string HCNS_Import_NgayPC = "HCNS_Import_NgayPC";
        public const string HCNS_Import_File = "HCNS_Import_File";
        public const string HCNS_Export_File = "HCNS_Export_File";
        public const string HCNS_BaoCaoChamAn = "HCNS_BaoCaoChamAn";
        public const string HCNS_TheoDoiThaiSan = "HCNS_TheoDoiThaiSan";
        public const string HCNS_LamViecNgayLe = "HCNS_LamViecNgayLe";

        public const string DM_HanhChinh = "DM_HanhChinh";
        public const string DM_HanhChinhBV = "DM_HanhChinhBV";
        public const string DM_CaLamViec = "DM_CaLamViec";
        public const string DM_ThietBi = "DM_ThietBi";
        public const string DM_PhanQuyen = "DM_PhanQuyen";

        public const string CaLamViec_Update = "CaLamViec_Update";


        //public const string HCNS_VIEW_EMP_ABSENT = "hcns15"; // Danh cho user (chỉ xem danh sách các ngày mình đã được khai báo)
        //public const string HCNS_ALL_EMP_ABSENT = "hcns16"; // Danh cho admin

        //public const string HCNS_EDIT_EMP_ABSENT = "hcns17"; // Danh cho người khai báo vắng
        //public const string HCNS_FINGER = "finger";


        //public const string HCNS_QLDT_View = "hcns11";
        //public const string HCNS_QLDT_Add = "hcns12";
        //public const string HCNS_QLDT_Edit = "hcns13";
        //public const string HCNS_QLDT_AddNV = "hcns14";
        //public const string HCNS_DCSA_View = "hcns6";
        //public const string HCNS_CC = "hcns_cc";

        //public const string HCNS_CV_Admin = "hcns_cva";
        //public const string HCNS_CV_User = "hcns_cvu";

        //public const string HCNS_Test1 = "hcns_test1";
        //public const string HCNS_Test2 = "hcns_test2";
        //public const string HCNS_Test3 = "hcns_test3";

        //public const string HCNS_Test4 = "hcns_test4"; //Chỉ dành cho IT

        //public const string HCNS_DanhBa = "hcns10";



        public const string HCNS_RoomAdmin = "hcns_room_admin";
        public const string HCNS_RoomUser = "hcns_room_user";

        public const string FO_CC_FULL = "cc_full";
        public const string FO_CC_VIEW = "cc_view";
        public const string FO_CC_EDIT = "cc_edit";
        public const string FO_CC_CONFIG = "cc_config"; //CC Cấu hình
        public const string FO_CC_EXPORT = "cc_export"; //CC Xuất file

        public const string CNG = "CNG";

        public const string FO_DSDK = "dsdh";

        public const string XN_STT = "xn01";

        public const string TTDT = "DaoTao";

        public const string UTIL_BHYT = "HoTro_BHYT";

        public const string KHO_VTTH = "tool_khovtth";

        public const string TIMELAPSE_TD = "Timelapse_TongDai";
        public const string TIMELAPSE_BS = "Timelapse_BacSi";
        public const string TIMELAPSE_CSKH = "Timelapse_CSKH";
        public const string TIMELAPSE_LAB = "Timelapse_Lab";
        public const string TIMELAPSE_LAB_EDIT = "Timelapse_Lab_Edit";
        public const string TIMELAPSE_LAB_EXPORT = "Timelapse_Lab_Export";
        public const string TIMELAPSE_ADMIN = "Timelapse_Admin";
        public const string TIMELAPSE_VIEW = "Timelapse_View";

        public const int TRANGTHAI_LUU = 0;
        public const int TRANGTHAI_RA = 1;
        public const int TRANGTHAI_HIEN = 2;
        public const int TRANGTHAI_CHOHUY = 3;
        public const int TRANGTHAI_CHUYEN = 4;
        public const int TRANGTHAI_HUY = 5;

        public const string QLTG_ADMIN = "QLTG_ADMIN"; // Quản lý tiền giường
        public const string NT_KhamBenh = "NT_KhamBenh"; // Ngoại trú - ds Khám bệnh

        public static string ConvertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string ConvertStringArrayToString(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(',');
            }
            return builder.ToString();
        }

        public static string ConvertDataTableToJson(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }


        public static Dictionary<string, string> DoiTuongTuVanGiaiDoan()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "Vo", "Vợ" },
                { "Chong", "Chồng" },
                { "GiayTo", "Giấy tờ" },
                { "Thuoc", "Thuốc" }
            };

            return keyValues;
        }

        public static Dictionary<string, string> PhuongPhapPGT()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "Không", "Không" },
                { "PGT-A", "PGT-A" },
                { "PGT-SR", "PGT-SR" },
                { "PGT-M", "PGT-M" }
            };

            return keyValues;
        }

        public static Dictionary<string, string> MauSacCong()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "#FFFFFF", "Trắng" },
                { "#FFFF33", "Vàng" },
                { "#FF33CC", "Hồng" },
                { "#33FF00", "Xanh lá" },
                { "#0066FF", "Xanh biển" },
                { "#FF6600", "Cam" }
            };

            return keyValues;
        }
        public static Dictionary<string, string> MauSacCassette()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "#FFFFFF", "Trắng" },
                { "#FFFF33", "Vàng nhạt" },
                { "#FFCC00", "Vàng đậm" },
                { "#FF6600", "Cam" },
                { "#33FF00", "Xanh lá" },
                { "#0066FF", "Xanh biển" },
                { "#FF0000", "Đỏ" }
            };

            return keyValues;
        }
        public static Dictionary<string, string> DoiTuongLuuTruPhoi()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "NguoiHienPhoi", "Hiến phôi" },
                { "NguoiHienTT", "Hiến tinh trùng" },
                { "NguoiHienNoan", "Hiến noãn" },
                { "ChuyenPhoiDen", "Chuyển phôi từ nơi khác" },
                { "ChuyenNoanDen", "Chuyển noãn từ nơi khác" },
                { "ChuyenTTDen", "Chuyển tinh trùng từ nơi khác" }
            };

            return keyValues;
        }

        public static Dictionary<string, string> DanhSachDonViThucHienPGS()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "Bệnh viên Đa khoa Tâm Anh", "Bệnh viên Đa khoa Tâm Anh" },
                { "Học viện Quân Y 103", "Học viện Quân Y 103" },
                { "Gentis", "Gentis" },
            };

            return keyValues;
        }

        public static Dictionary<string, string> DanhSachThang()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "1", "Tháng 01" },
                { "2", "Tháng 02" },
                { "3", "Tháng 03" },
                { "4", "Tháng 04" },
                { "5", "Tháng 05" },
                { "6", "Tháng 06" },
                { "7", "Tháng 07" },
                { "8", "Tháng 08" },
                { "9", "Tháng 09" },
                { "10", "Tháng 10" },
                { "11", "Tháng 11" },
                { "12", "Tháng 12" }
            };

            return keyValues;
        }
        public static Dictionary<string, string> CapQuanLy()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "Cấp nhà nước", "Cấp nhà nước" },
                { "Cấp bộ", "Cấp bộ" },
                { "Cấp cơ sở", "Cấp cơ sở" },
                { "Hợp tác quốc tế", "Hợp tác quốc tế" },
                { "Hợp tác trong nước", "Hợp tác trong nước" }
            };

            return keyValues;
        }
        public static Dictionary<string, string> DanhSachTuoiPhoi()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "N0", "Ngày ICSI (Ngày 0)" },
                { "N1", "Ngày 1" },
                { "N2", "Ngày 2" },
                { "N3", "Ngày 3" },
                { "N4", "Ngày 4" },
                { "N5", "Ngày 5" },
                { "N6", "Ngày 6" },
                { "N7", "Ngày 7" },
                { "D2IVM", "Ngày 2 IVM" },
                { "D3IVM", "Ngày 3 IVM" },
                { "D5IVM", "Ngày 5 IVM" },
                { "D6IVM", "Ngày 6 IVM" }
            };

            return keyValues;
        }

        public static Dictionary<string, string> DanhSachBacSi()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "TS.BS. Nguyễn Thị Liên Hương", "TS.BS. Nguyễn Thị Liên Hương" },
                { "BSNT. Lưu Xuân Kỳ", "BSNT. Lưu Xuân Kỳ" },
                { "BSNT. Hà Mai Linh", "BSNT. Hà Mai Linh" },
                { "BS. Phí Thị Tú Anh", "BS. Phí Thị Tú Anh" },
                { "BS. Nguyễn Thị Thùy Linh", "BS. Nguyễn Thị Thùy Linh" },
                { "BS. Vũ Thị Mai Anh", "BS. Vũ Thị Mai Anh" },
                { "ThS.Nguyễn Thị Hoa", "ThS.Nguyễn Thị Hoa" },
                { "ThS. Đinh Văn Bôn", "ThS. Đinh Văn Bôn" },
                { "ThS. Vũ Đình Hợp", "ThS. Vũ Đình Hợp" },
                { "ThS. Vũ Thị Bích Phượng", "ThS. Vũ Thị Bích Phượng" },
                { "ThS. Kiều Mạnh Hùng", "ThS. Kiều Mạnh Hùng" },
                { "Trần Mai Hương", "Trần Mai Hương" },
                { "Hà Viết Hùng", "Hà Viết Hùng" },
                { "An Mạnh Cường", "An Mạnh Cường" },
                { "Chuyển phôi về", "Chuyển phôi về" },
            };

            return keyValues;
        }

        public static Dictionary<string, string> LoaiNhanVien()
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "TA", "Nhân viên chính thức" },
                { "PT", "Cộng tác viên và có đến khám" },
                { "CC", "Thuê chứng chỉ hoặc Ngoại giao" },
                { "GT", "Giới thiệu khách hàng" },
                { "HV", "Học việc" }
            };

            return keyValues;
        }
    }
}