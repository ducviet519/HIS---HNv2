using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace System.App.Entities.HCNS
{
    public class NhanVien
    {
        [Display(Name = "STT")]
        public int UserEnrollNumber { get; set; }
        [Display(Name = "Mã nhân viên")]
        public string UserFullCode { get; set; }
        [Display(Name = "Chức danh")]
        public string TATitle { get; set; }
        [Display(Name = "Họ và tên")]
        public string UserFullName { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime NgaySinh { get; set; }
        [Display(Name = "Nơi sinh")]
        public string NoiSinh { get; set; }
        [Display(Name = "Địa chỉ")]
        public string NoiSinhCT { get; set; }
        [Display(Name = "Khoa/Phòng")]
        public int PhongKhoa { get; set; }
        [Display(Name = "Khoa/Phòng làm việc")]
        public int PhongKhoaHC { get; set; }
        [Display(Name = "Nhóm chức danh")]
        public int NhomChucDanh { get; set; }
        [Display(Name = "Chức danh")]
        public string ChucDanh { get; set; }
        [Display(Name = "Quản lý")]
        public int Lead { get; set; }
        [Display(Name = "Email cá nhân")]
        public string Email { get; set; }
        [Display(Name = "Email tổ chức")]
        public string TAEmail { get; set; }
        [Display(Name = "Email nhận thông tin")]
        public int SaEmail { get; set; }
        [Display(Name = "Điện thoại cá nhân")]
        public string SDT1 { get; set; }
        [Display(Name = "Điện thoại tổ chức")]
        public string SDT2 { get; set; }
        [Display(Name = "Số nội bộ")]
        public string SDTNB { get; set; }
        [Display(Name = "Giới tính")]
        public int GioiTinh { get; set; }
        [Display(Name = "Loại giấy tờ")]
        public int LoaiCMT { get; set; }
        [Display(Name = "Giấy tờ số")]
        public string SoCMT { get; set; }
        [Display(Name = "Cấp ngày")]
        public DateTime? CapNgay { get; set; }
        [Display(Name = "Nơi cấp")]
        public string NoiCap { get; set; }
        [Display(Name = "Ngày vào")]
        public DateTime? NgayVao { get; set; }
        [Display(Name = "Ngày nghỉ")]
        public DateTime? NgayNghi { get; set; }
        [Display(Name = "Đã nghỉ")]
        public int DaNghi { get; set; }
        [Display(Name = "Lý do nghỉ")]
        public string LyDoNghi { get; set; }
        [Display(Name = "Số tài khoản")]
        public string SoTK { get; set; }
        [Display(Name = "Ngân hàng")]
        public string NganHang { get; set; }
        [Display(Name = "Số BHXH")]
        public string SoBHXH { get; set; }
        [Display(Name = "Số thẻ BHXH")]
        public string SoTheBHXH { get; set; }
        [Display(Name = "Ngày tham gia BHXH")]
        public DateTime NgayTGBHXH { get; set; }
        [Display(Name = "Loại đóng BHXH")]
        public int LoaiDongBHXH { get; set; }
        [Display(Name = "Quốc gia")]
        public int QGThT { get; set; }
        [Display(Name = "Tỉnh")]
        public int TinhThT { get; set; }
        [Display(Name = "Quận")]
        public int QuanThT { get; set; }
        [Display(Name = "Phường")]
        public int PhuongThT { get; set; }
        [Display(Name = "Địa chỉ thường trú")]
        public string DcThT { get; set; }
    }
}
