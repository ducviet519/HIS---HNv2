namespace System.App.Entities.HCNS
{
    public class CC_NhomChamCong
    {
        public int ID { get; set; }
        public string TenNhom { get; set; }
        public string MoTa { get; set; }
    }
    public class CC_CheckInOut
    {
        public int UserEnrollNumber { get; set; }
        public string UserFullName { get; set; }
        public string UserFullCode { get; set; }
        public string KhoaPhong { get; set; }
        public string TimeDate { get; set; }
        public string TimeStr { get; set; }
        public string Source { get; set; }
        public int MachineNo { get; set; }
        public int WorkCode { get; set; }
        public string NewTime { get; set; }
        public string FullDate { get; set; }
        public string MaQL { get; set; }
    }
    public class CC_InOutCol
    {
        public CC_InOutCol()
        {
            OverDay = 0;
            MachineNoIn = 0;
            MachineNoOut = 0;
        }

        public int UserEnrollNumber { get; set; }
        public string TimeDate { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public int OverDay { get; set; }
        public int MachineNoIn { get; set; }
        public int MachineNoOut { get; set; }
    }
    public class CC_CaChamCong
    {
        public int ID { get; set; }
        public int KhoaPhong { get; set; }
        public string TenKhoaPhong { get; set; }
        public int Nhom { get; set; }
        public string TenNhom { get; set; }
        public string TenCa { get; set; }
        public string MaCa { get; set; }
        public string GioVao { get; set; }
        public string GioRa { get; set; }
        public string BatDauVao { get; set; }
        public string KetThucVao { get; set; }
        public string BatDauRa { get; set; }
        public string KetThucRa { get; set; }
        public int SapXep { get; set; }
        public int HeSo { get; set; }
    }

    public class CC_TongHopCong_ChiTiet
    {
        public int UserEnrollNumber { get; set; }
        public string UserFullName { get; set; }
        public string UserFullCode { get; set; }
        public string KhoaPhong { get; set; }
        /// <summary>
        /// Week Day => Ngày trong tuần
        /// </summary>
        public int WD { get; set; }
        /// <summary>
        /// User Sys Code 1 => Mã ca 1
        /// </summary>
        public string USC1 { get; set; }
        /// <summary>
        /// User Sys Code 2 => Mã ca 2
        /// </summary>
        public string USC2 { get; set; }
        /// <summary>
        /// Mã khai báo vắng
        /// </summary>
        public string AbsentSymbol { get; set; }
        public string Ngay { get; set; }
        public float WorkDay1 { get; set; }
        public float WorkDay2 { get; set; }
    }

    public class UserTempShift
    {
        public UserTempShift()
        {
            UserEnrollNumber = 0;
            Shift1 = 0;
            Status = 0;
            NewShift1 = 0;
            NewShift2 = 0;
            Confirmed = false;
        }
        public int UserEnrollNumber { get; set; }
        public string UserFullCode { get; set; }
        public string UserFullName { get; set; }
        public int KhoaPhong { get; set; }
        public string Date { get; set; }
        public int Shift1 { get; set; }
        public int Status { get; set; }
        public string ShiftCode1 { get; set; }
        public int NewShift1 { get; set; }
        public int NewShift2 { get; set; }
        public string CreateAcc { get; set; }
        public string UpdateAcc { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public bool Confirmed { get; set; }
        public string DateString { get; set; }
        public string ShiftDesc { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Users { get; set; }
    }

    public class Shifts
    {
        public int ShiftID { get; set; }
        public string ShiftCode { get; set; }
        public string VaoCa { get; set; }
        public string RaCa { get; set; }
        public string NgayCong { get; set; }
        public string BD_VaoCa { get; set; }
        public string KT_VaoCa { get; set; }
        public string BD_RaCa { get; set; }
        public string KT_RaCa { get; set; }
        public string QuaDem { get; set; }
        public string KGH { get; set; }
        public int DayCount { get; set; }
        public int IsHolidayOT { get; set; }
    }

    public class ShiftUnconfirmed
    {
        public int UserEnrollNumber { get; set; }
        public string UserFullName { get; set; }
        public string UserFullCode { get; set; }
        public string KhoaPhong { get; set; }
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public string Date { get; set; }
        public int Status1 { get; set; }
    }

    public class Schedule
    {
        public int SchID { get; set; }
        public string SchName { get; set; }
        public string MoTaCa { get; set; }
    }
    public class UserInfo
    {
        public string Users { get; set; }
        public int UserEnrollNumber { get; set; }
        public int SchID { get; set; }
    }

    public class UserInfExt
    {
        public int UserEnrollNumber { get; set; }
        public string UserFullCode { get; set; }
        public string UserFullName { get; set; }
        public string CaPhu { get; set; }
        public string Users { get; set; }
    }

    public class WorkStatsType
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Des { get; set; }
        public int Type { get; set; }
    }

    public class UserWStats
    {
        public UserWStats()
        {
            Confirmed = false;
            W1Stats = 0;
            W2Stats = 0;
            W3Stats = 0;
        }
        public int UserEnrollNumber { get; set; }
        public string UserFullCode { get; set; }
        public string UserFullName { get; set; }
        public string KhoaPhong { get; set; }
        public string Date { get; set; }
        public string WStats1 { get; set; }
        public string WStats1Code { get; set; }
        public string WStats2 { get; set; }
        public string WStats2Code { get; set; }
        public string WStats3 { get; set; }
        public string WStats3Code { get; set; }
        public int W1Stats { get; set; }
        public int W2Stats { get; set; }
        public int W3Stats { get; set; }
        public string CreateAcc { get; set; }
        public string UpdateAcc { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string S1 { get; set; }
        public string TTXuLyMuSo { get; set; }
        public string S2 { get; set; }
        public string TTXuLyOT { get; set; }
        public string S3 { get; set; }
        public string TTXuLyTT { get; set; }
        public string USC1 { get; set; }
        public string UserEnrollNumbers { get; set; }
        public bool Confirmed { get; set; }
        public string DateString { get; set; }
        public int MuSoStats { get; set; }
        public int OTStats { get; set; }
        public int TTStats { get; set; }
        public int VC { get; set; }
        public string Col { get; set; }
        public string In1 { get; set; }
        public string Onduty { get; set; }
        public string Out1 { get; set; }
        public string Offduty { get; set; }
        public string Note { get; set; }
        public string LTGType { get; set; }
        public string TTType { get; set; }
    }
    public class UserWTRequests
    {
        public int UserEnrollNumber { get; set; }
        public string Date { get; set; }
        public int Fix1 { get; set; }
        public string Fix1Des { get; set; }
        public string In1F { get; set; }
        public string Out1F { get; set; }
        public int Status { get; set; }
        public int HRStats1 { get; set; }
        public int DeptStats1 { get; set; }
        public string DeptNote1 { get; set; }
        public string HRNote1 { get; set; }
        public string UserNote1 { get; set; }
    }
    public class XemCong
    {
        public int UserEnrollNumber { get; set; }
        public int KhoaPhong { get; set; }
        public string Date { get; set; }
        public string DateFull { get; set; }
        public string WD { get; set; }
        public string Ca { get; set; }
        public float NgayCong { get; set; }
        public string GioRa { get; set; }
        public string GioVao { get; set; }
        public string DiMuonVeSom { get; set; }
        public int SoLanVaoRa { get; set; }
        public int Mau { get; set; }
        public float CongChuan { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public int HRStats1 { get; set; }
        public int Status { get; set; }
        public int DateOrder { get; set; }
    }

    public class XemCong_TongCong
    {
        public int UserEnrollNumber { get; set; }
        public string NgayCong { get; set; }
        public string CongChuan { get; set; }
        public string DiMuonVeSom { get; set; }
        public int SoLanVaoRa { get; set; }
    }

    public class OTPayment
    {
        public int ID { get; set; }
        public int UserEnrollNumber { get; set; }
        public string UserFullCode { get; set; }
        public string UserFullName { get; set; }
        public string KhoaPhong { get; set; }
        public string Ngay { get; set; }
        public string Thu { get; set; }
        public string LyDo { get; set; }
        public string KieuTTLamThemGio { get; set; }
        public string ThoiGianBD { get; set; }
        public string ThoiGianKT { get; set; }
        public string TongLamThemGio { get; set; }
        public string CaLamViec { get; set; }
        public string GioVaoCa { get; set; }
        public string GioRaCa { get; set; }
        public int MaThoiGianLamThem { get; set; }
        public string TenThoiGianLamThem { get; set; }
        public int DaDuyet { get; set; }
        public int HienThi { get; set; }
        public string GoiY { get; set; }
        public int PhanLoai { get; set; }
        public string KyHieuDuyet { get; set; }
        public string OTPayTypes { get; set; }
        public string TimeStr { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public int SoPhutDeXuat { get; set; }
        public int MA { get; set; }
    }

    public class OTPayment_Filter
    {
        public DateTime NgayBD { get; set; }
        public DateTime NgayKT { get; set; }
        public int MaKP { get; set; }
        public int MaNV { get; set; }
        public int Duyet { get; set; }
    }
    public class OTPaymentType
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Des { get; set; }
    }
    public class GhepCa
    {
        public int UserEnrollNumber { get; set; }
        public string TimeString { get; set; }
        public string MaQL { get; set; }
        //
        public int Val { get; set; }
        public string Par { get; set; }
    }

    public class CaKhongXacDinh
    {
        //WH: Tổng thời gian làm việc
        //STApp: Thời gian bắt đầu được duyệt
        //ETApp: Thời gian bắt đầu được duyệt

        public int UserEnrollNumber { get; set; }
        public string UserFullName { get; set; }
        public string UserFullCode { get; set; }
        public string TenKhoaPhong { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime ST { get; set; }
        public DateTime ET { get; set; }
        public string WH { get; set; }
        public string STApp { get; set; }
        public string ETApp { get; set; }
        public string TTWH { get; set; }
        public int STWC { get; set; }
        public int ETWC { get; set; }
        public int Display { get; set; }
        public int KhoaPhong { get; set; }
        public int Approve { get; set; }
        public string TimeString { get; set; }
        public string LyDo { get; set; }
        public string MaQL { get; set; }
    }
    public class CaKhongXacDinh_Filter
    {
        public int UserEnrollNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int KhoaPhong { get; set; }
        public int HienThi { get; set; }
    }
    public class TinhPhep
    {
        public int UserEnrollNumber { get; set; }
        public float PhepTon { get; set; }
        public float TongPhepT3 { get; set; }
        public float TongPhepSauT3 { get; set; }
        public float UngPhep { get; set; }
        public float PhepCon { get; set; }
        public float KhaiBaoP { get; set; }
        public float PhepDaDung { get; set; }
        public float NgayThuaKyLuongTruoc { get; set; }
        public float BuDaDung { get; set; }
        public float BuConLai { get; set; }
        public float BuPhatSinhNew { get; set; }
    }
    public class DiaDiemAn
    {
        public string ID { get; set; }
        public string MOTA { get; set; }
        public string UuTien { get; set; }
    }
}