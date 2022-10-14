namespace System.App.Entities
{
    public class DanhMuc_LoaiVang_Search
    {
        public string AbsentCode { get; set; }
        public string AbsentDescription { get; set; }
        public string AbsentSymbol { get; set; }
        public string IsYes { get; set; }
        public string IsCount { get; set; }
        public string Score { get; set; }
    }
    public class DanhMuc_LoaiVang
    {
        public string AbsentCode { get; set; }
        public string AbsentDescription { get; set; }
        public string AbsentSymbol { get; set; }
        public string IsYes { get; set; }
        public string IsCount { get; set; }
        public string Score { get; set; }
    }
    public class DanhMuc_NganHang_Search
    {
        public string ID { get; set; }
        public string BankName { get; set; }
    }
    public class DanhMuc_NganHang
    {
        public string ID { get; set; }
        public string BankName { get; set; }
    }
    public class DanhMuc_HopDong_Search
    {
        public string ID { get; set; }
        public string ConName { get; set; }
        public string ConSym { get; set; }
    }
    public class DanhMuc_HopDong
    {
        public string ID { get; set; }
        public string ConName { get; set; }
        public string ConSym { get; set; }
    }
    public class DanhMuc_KhoaPhong
    {
        public string STT { get; set; }
        public string Code { get; set; }
        public string KhoaP { get; set; }
        public string CaPhu { get; set; }
        public string Object { get; set; }
        public string MaKPHCNS { get; set; }
    }
    public class DanhMuc_KhoaPhong_Search
    {
        public string STT { get; set; }
        public string Code { get; set; }
        public string KhoaP { get; set; }
        public string Object { get; set; }
    }
    public class DanhMuc_ThanhPho
    {
        public string ID { get; set; }
        public string TownName { get; set; }
        public string TownShip { get; set; }
        public string Pupulation { get; set; }
        public string Area { get; set; }
    }
    public class DanhMuc_ThanhPho_Search
    {
        public string ID { get; set; }
        public string TownName { get; set; }
        public string TownShip { get; set; }
        public string Pupulation { get; set; }
        public string Area { get; set; }
    }
    public class DanhMuc_QuanHuyen
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string DLevel { get; set; }
        public string City { get; set; }
    }
    public class DanhMuc_QuanHuyen_Search
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string DLevel { get; set; }
        public string City { get; set; }
    }
    public class DS_DropDownList
    {
        public string ID { get; set; }
        public string MoTa { get; set; }
    }
    public class DanhMuc_BenhVien
    {
        public string ID { get; set; }
        public string HospitalName { get; set; }
    }
    public class DanhMuc_BenhVien_Search
    {
        public string ID { get; set; }
        public string HospitalName { get; set; }
    }
    public class DanhMuc_QuocGia
    {
        public string ID { get; set; }
        public string Nationality { get; set; }
    }
    public class DanhMuc_QuocGia_Search
    {
        public string ID { get; set; }
        public string Nationality { get; set; }
    }
    public class DanhMuc_NoiCapCMT
    {
        public string ID { get; set; }
        public string NoiCap { get; set; }
    }
    public class DanhMuc_NoiCapCMT_Search
    {
        public string ID { get; set; }
        public string NoiCap { get; set; }
    }
    public class DanhMuc_DanToc
    {
        public string ID { get; set; }
        public string PeopleName { get; set; }
        public string PeopleOName { get; set; }
        public string PeoplePlace { get; set; }
    }
    public class DanhMuc_DanToc_Search
    {
        public string ID { get; set; }
        public string PeopleName { get; set; }
        public string PeopleOName { get; set; }
        public string PeoplePlace { get; set; }
    }
    public class DanhMuc_TonGiao
    {
        public string ID { get; set; }
        public string Religion { get; set; }
    }
    public class DanhMuc_TonGiao_Search
    {
        public string ID { get; set; }
        public string Religion { get; set; }
    }
    public class DanhMuc_PhongHop
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
    public class DanhMuc_PhongHop_Search
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
    public class DanhMuc_KhoaPhongCC
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public string RelationID { get; set; }
        public string LevelID { get; set; }
        public string DeptCode { get; set; }
        public string TempID { get; set; }
        public string TempRelationID { get; set; }
        public string UngPhepMin { get; set; }
        public string UngPhepMax { get; set; }
    }
    public class DanhMuc_KhoaPhongCC_Search
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public string RelationID { get; set; }
        public string LevelID { get; set; }
        public string DeptCode { get; set; }
        public string TempID { get; set; }
        public string TempRelationID { get; set; }
        public string UngPhepMin { get; set; }
        public string UngPhepMax { get; set; }
    }
    public class DanhMuc_PhuongXa
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string WLevel { get; set; }
        public string Dis { get; set; }
        public string CityID { get; set; }
    }
    public class DanhMuc_PhuongXa_Search
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string WLevel { get; set; }
        public string Dis { get; set; }
        public string CityID { get; set; }
    }
    public class DanhMuc_Tang
    {
        public string ID { get; set; }
        public string Block { get; set; }
        public string Floor { get; set; }
        public string FloorDes { get; set; }
    }
    public class DanhMuc_Tang_Search
    {
        public string ID { get; set; }
        public string Block { get; set; }
        public string Floor { get; set; }
        public string FloorDes { get; set; }
    }
    public class DanhMuc_CaLamViec
    {
        public string ShiftID { get; set; }
        public string ShiftCode { get; set; }
        public string Onduty { get; set; }
        public string Offduty { get; set; }
        public string DayCount { get; set; }
        public string OnTimeIn { get; set; }
        public string OnTimeOut { get; set; }
        public string CutIn { get; set; }
        public string CutOut { get; set; }
        public string OnLunch { get; set; }
        public string OffLunch { get; set; }
        public string WorkingTime { get; set; }
        public string Workingday { get; set; }
        public string IsHolidayOT { get; set; }
        public string WKOTLevel { get; set; }
    }
    public class DanhMuc_CaLamViec_Search
    {
        public string ShiftID { get; set; }
        public string ShiftCode { get; set; }
        public string Onduty { get; set; }
        public string Offduty { get; set; }
        public string DayCount { get; set; }
        public string OnTimeIn { get; set; }
        public string OnTimeOut { get; set; }
        public string CutIn { get; set; }
        public string CutOut { get; set; }
        public string OnLunch { get; set; }
        public string OffLunch { get; set; }
        public string WorkingTime { get; set; }
        public string Workingday { get; set; }
        public string IsHolidayOT { get; set; }
        public string WKOTLevel { get; set; }
    }
    public class DanhMuc_LichLamViec
    {
        public string SchID { get; set; }
        public string SchName { get; set; }
        public string InOutID { get; set; }
        public string Display { get; set; }
    }
    public class DanhMuc_LichLamViec_Search
    {
        public string SchID { get; set; }
        public string SchName { get; set; }
        public string InOutID { get; set; }
        public string Display { get; set; }
    }
    public class DanhMuc_LichTrinh
    {
        public string ID { get; set; }
        public string DayID { get; set; }
        public string SchID { get; set; }
        public string ShiftID { get; set; }
        public string ShiftID1 { get; set; }
        public string ShiftID2 { get; set; }
        public string ShiftID3 { get; set; }
        public string ShiftID4 { get; set; }
        public string ShiftID5 { get; set; }
        public string ShiftID6 { get; set; }
        public string ShiftID7 { get; set; }
    }
    public class DanhMuc_LichTrinh_Search
    {
        public string ID { get; set; }
        public string DayID { get; set; }
        public string SchID { get; set; }
        public string ShiftID { get; set; }
        public string ShiftID1 { get; set; }
        public string ShiftID2 { get; set; }
        public string ShiftID3 { get; set; }
        public string ShiftID4 { get; set; }
        public string ShiftID5 { get; set; }
        public string ShiftID6 { get; set; }
        public string ShiftID7 { get; set; }
    }
    public class DanhMuc_NoiAn
    {
        public string ID { get; set; }
        public string Place { get; set; }
    }
    public class DanhMuc_NoiAn_Search
    {
        public string ID { get; set; }
        public string Place { get; set; }
    }
    public class DanhMuc_NgayNghi
    {
        public string ID { get; set; }
        public string HDate { get; set; }
        public string Holiday { get; set; }
        public string NgayBu { get; set; }
    }
    public class DanhMuc_NgayNghi_Search
    {
        public string ID { get; set; }
        public string HDate { get; set; }
        public string Holiday { get; set; }
        public string NgayBu { get; set; }
    }
    public class DanhMuc_LoaiOT
    {
        public string ID { get; set; }
        public string Des { get; set; }
        public string Code { get; set; }
    }
    public class DanhMuc_LoaiOT_Search
    {
        public string ID { get; set; }
        public string Des { get; set; }
        public string Code { get; set; }
    }
    public class DanhMuc_KyNang
    {
        public string ID { get; set; }
        public string SkillName { get; set; }
    }
    public class DanhMuc_KyNang_Search
    {
        public string ID { get; set; }
        public string SkillName { get; set; }
    }
    public class DanhMuc_LoaiYeuCauSuaCong
    {
        public string ID { get; set; }
        public string Des { get; set; }
    }
    public class DanhMuc_LoaiYeuCauSuaCong_Search
    {
        public string ID { get; set; }
        public string Des { get; set; }
    }
    public class DanhMuc_DoiTuongCC
    {
        public string IDT { get; set; }
        public string TitleName { get; set; }
        public string TitleCode { get; set; }
    }
    public class DanhMuc_DoiTuongCC_Search
    {
        public string IDT { get; set; }
        public string TitleName { get; set; }
        public string TitleCode { get; set; }
    }
    public class DanhMuc_LoaiLamThem
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Des { get; set; }
        public string Type { get; set; }
    }
    public class DanhMuc_LoaiLamThem_Search
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Des { get; set; }
        public string Type { get; set; }
    }
    public class DanhMuc_LoaiNhanVien
    {
        public string ID { get; set; }
        public string TypeDes { get; set; }
    }
    public class DanhMuc_LoaiNhanVien_Search
    {
        public string ID { get; set; }
        public string TypeDes { get; set; }
    }
    public class DanhMuc_LoaiLoiCC
    {
        public string ID { get; set; }
        public string Des { get; set; }
        public string Meth { get; set; }
    }
    public class DanhMuc_LoaiLoiCC_Search
    {
        public string ID { get; set; }
        public string Des { get; set; }
        public string Meth { get; set; }
    }
    public class DanhMuc_BangCap
    {
        public string RID { get; set; }
        public string RName { get; set; }
        public string RLevel { get; set; }
    }
    public class DanhMuc_BangCap_Search
    {
        public string RID { get; set; }
        public string RName { get; set; }
        public string RLevel { get; set; }
    }
    public class DanhMuc_ThietBiPhongHop
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
    }
    public class DanhMuc_ThietBiPhongHop_Search
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
    }
    public class DanhMuc_TTHonNhan
    {
        public string ID { get; set; }
        public string MaSta { get; set; }
    }
    public class DanhMuc_TTHonNhan_Search
    {
        public string ID { get; set; }
        public string MaSta { get; set; }
    }
    public class DanhMuc_ThietBi
    {
        public string ID { get; set; }
        public string MachineID { get; set; }
        public string MachineName { get; set; }
        public string IPAddress { get; set; }
        public string Port { get; set; }
        public string AreaID { get; set; }
        public string DeviceName { get; set; }
        public string IsChamAn { get; set; }
        public string PortSocket { get; set; }
        public string TenMayIn { get; set; }
    }
    public class DanhMuc_ThietBi_Search
    {
        public string ID { get; set; }
        public string MachineID { get; set; }
        public string MachineName { get; set; }
        public string IPAddress { get; set; }
        public string Port { get; set; }
        public string AreaID { get; set; }
        public string DeviceName { get; set; }
        public string IsChamAn { get; set; }
        public string PortSocket { get; set; }
        public string TenMayIn { get; set; }
    }
    public class DanhMuc_Nhom
    {
        public string Object { get; set; }
        public string Depts { get; set; }
        public string TADepts { get; set; }
        public string Roles { get; set; }
    }
    public class DanhMuc_Nhom_Search
    {
        public string Object { get; set; }
        public string Depts { get; set; }
        public string TADepts { get; set; }
        public string Roles { get; set; }
    }
    public class DanhMuc_Quyen
    {
        public string RightTA { get; set; }
        public string PartTA { get; set; }
        public string LevelTA { get; set; }
        public string ValTA { get; set; }
    }
    public class DanhMuc_Quyen_Search
    {
        public string RightTA { get; set; }
        public string PartTA { get; set; }
        public string LevelTA { get; set; }
        public string ValTA { get; set; }
    }
    public class DanhMuc_Quyen_NguoiDung
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Object { get; set; }
    }
    public class DanhMuc_Quyen_NguoiDung_Search
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Object { get; set; }
    }
    public class DanhMuc_Quyen_KhoaPhong
    {
        public string ID { get; set; }
        public string Dept { get; set; }
        public string Object { get; set; }
    }
    public class DanhMuc_Quyen_KhoaPhong_Search
    {
        public string ID { get; set; }
        public string Dept { get; set; }
        public string Object { get; set; }
    }
    public class QuyenChamCong
    {
        public string ID { get; set; }
        public string USERENROLLNUMBER { get; set; }
        public string USERFULLNAME { get; set; }
        public string DESCRIPTION { get; set; }
    }
    public class DanhMuc_Config
    {
        public string ID { get; set; }
        public string Parameter { get; set; }
        public string ParameterID { get; set; }
        public string StringVal { get; set; }
        public string NumberVal { get; set; }
        public string DatetimeVal { get; set; }
    }
    public class DanhMuc_Config_Search
    {
        public string ID { get; set; }
        public string Parameter { get; set; }
        public string ParameterID { get; set; }
        public string StringVal { get; set; }
        public string NumberVal { get; set; }
        public string DatetimeVal { get; set; }
    }
    public class DanhMuc_ViTriTuyenDung
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
    public class DanhMuc_ViTriTuyenDung_Search
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}