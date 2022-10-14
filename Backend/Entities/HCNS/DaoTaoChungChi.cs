using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web;

namespace System.App.Entities.HCNS
{
    public class DaoTaoChungChi : ChungChi
    {
        public string IDDaoTao { get; set; }
        public string TenKhoaDaoTao { get; set; }
        public string DonViToChuc { get; set; }
        public int SoTiet { get; set; }
        public string TrangThai { get; set; }
        public string LoaiDaoTao { get; set; }
        public int ChiPhi { get; set; }
        public string LyDo { get; set; }
        public string GhiChu { get; set; }
    }
    public class DaoTaoChungChiVM
    {
        public List<DaoTaoChungChi> DaoTaoChungChiLists { get; set; }

        public DaoTaoChungChi DaoTaoChungChi { get; set; }
    }

    public class SearchDaoTao 
    {
        public string SearchMaNV { get; set; }
        public string SearchKhoaPhong { get; set; }
        public int? SearchNam { get; set; }
        public string SearchTrangThai { get; set; }
        public string SearchTenCC { get; set; }
    }

    public enum TrangThai
    {
        [Display(Name = "Chờ duyệt")] CD = 0,
        [Display(Name = "Xác nhận")] XN = 1,
        [Display(Name = "Từ chối")] TC = -1,
    }
}
