using System.App.Entities;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories;
using System.App.Repositories.HCNS;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace System.App.Services.HCNS
{
    public interface DK_SuatAn_Interface
    {
        Dictionary<int, string> DanhSachKhoaPhong(string kp = "");
        Dictionary<int, string> DanhSachKhoaPhong_SuatAn(string kp = "");
        List<SelectListItem> DSKhoaPhong(string kp = "");
        List<ChamAn> DS_ChamAn(string makp, string manv, string thoidiem, string fromDate, string toDate);
        bool CapNhat(List<Person_CA> data);
        IEnumerable<SuatAnNoiTru_HienDien> SANT_HienDien(string makp = "", string mabn = "");
        IEnumerable<SuatAnNoiTru_HienDien> SANT_DanhSachDaDK(string mabn, string tungay, string denngay);
        Dictionary<string, string> SANT_DanhSachKhoaPhong();
        IEnumerable<SuatAnNoiTru_NhomBenh> SANT_DanhSachNhomBenh(string doituong);
        IEnumerable<SuatAnNoiTru_DoiTuong> SANT_DanhSachDoiTuong(int nhombenh);
        IEnumerable<SuatAnNoiTru_CheBien> SANT_DanhSachCheBien(int doituong);
        bool SANT_CapNhatSuatAnNoiTru(SuatAnNoiTru_KhaiBao obj);
        IEnumerable<SuatAnNoiTru_SoLuong> SANT_TongHopSoLuong(string mabn, string from, string to);
        List<SelectListItem> EatPlace();
        List<SelectListItem> DSKhoaPhong_Eat();
        DataTable ThongKe_ChamAn(string dept, string tungay, string denngay, string thoidiem, string noian, string loai, string theo);
    }
    public class DK_SuatAn_Service : DK_SuatAn_Interface
    {
        private readonly DK_SuatAn_Repo _suatAnRepo;
        private readonly Logs_Repo log_Repo;

        public DK_SuatAn_Service()
        {
            _suatAnRepo = new DK_SuatAn_Repo();
            log_Repo = new Logs_Repo();
        }

        public Dictionary<int, string> DanhSachKhoaPhong(string kp = "")
        {
            Dictionary<int, string> listDepartment = new Dictionary<int, string>();

            var models = _suatAnRepo.DanhSachKhoaPhong(StaticParams.connectionStringWiseEyeWebOn, kp);

            foreach (var o in models)
            {
                listDepartment.Add(o.STT, o.KhoaP);
            }

            return listDepartment;
        }
        public Dictionary<int, string> DanhSachKhoaPhong_SuatAn(string kp = "")
        {
            Dictionary<int, string> listDepartment = new Dictionary<int, string>();

            var models = _suatAnRepo.DanhSachKhoaPhong(StaticParams.connectionStringWiseEyeWebOn, kp);
            if (kp.Length == 0)
                listDepartment.Add(-1, "Tất cả");
            foreach (var o in models)
            {
                listDepartment.Add(o.STT, o.KhoaP);
            }

            return listDepartment;
        }

        public List<SelectListItem> DSKhoaPhong(string kp)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = _suatAnRepo.DSKhoaPhong(StaticParams.connectionStringWiseEyeWebOn, kp);

            items.Add(new SelectListItem() { Value = "-1", Text = "-- Tất cả --" });

            foreach (var item in depts)
            {
                if (item.STT.ToString() == kp)
                    items.Add(new SelectListItem() { Value = item.STT.ToString(), Text = item.KhoaP, Selected = true });
                else
                    items.Add(new SelectListItem() { Value = item.STT.ToString(), Text = item.KhoaP });
            }
            return items;
        }

        public bool CapNhat(List<Person_CA> data)
        {
            var ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var user = HttpContext.Current.User.Identity.Name;
            try
            {
                List<Update_ChamAn> lst = new List<Update_ChamAn>();

                foreach (var person in data)
                {
                    var userEnrollNumber = person.UserEnrollNumber;
                    //DateTime ngay = new DateTime(DateTime.ParseExact(person.Ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).Year, DateTime.ParseExact(person.Ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month, DateTime.ParseExact(person.Ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).Day, 16, 30, 00);
                    //if ((ngay- DateTime.Now).TotalMinutes < 0)
                    //{
                    //    return false;
                    //}
                    var cc = person.Scores.Split(',');
                    for (int i = 0; i < cc.Length; i++)
                    {
                        lst.Add(new Update_ChamAn()
                        {
                            UserEnrollNumber = int.Parse(userEnrollNumber),
                            ThoiDiem = person.ThoiDiem,
                            Ngay_CC = DateTime.ParseExact(person.Ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(i).ToString("dd/MM/yyyy"),
                            GiaTri = String.IsNullOrEmpty(cc[i].ToString()) ? 0 : int.Parse(cc[i].ToString())
                        });
                    }
                }

                var result = _suatAnRepo.CapNhat_ChamAn(StaticParams.connectionStringWiseEyeWebOn, lst);

                if (result)
                {
                    string jsonString = new JavaScriptSerializer().Serialize(data);

                    log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                    {
                        ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                        Name = "Chấm ăn",
                        Action = "CapNhat",
                        Controller = "HCNS.DKSA",
                        Data = jsonString,
                        DateCreated = DateTime.UtcNow.AddHours(7),
                        CreatedBy = user,
                        IP = ip.ToString()
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                log_Repo.Insert(StaticParams.connectionStringWiseEyeWebOn, new Logs
                {
                    ID = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    Name = "Chấm ăn",
                    Action = "CapNhat",
                    Controller = "HCNS.DKSA",
                    Message = ex.Message,
                    DateCreated = DateTime.UtcNow.AddHours(7),
                    CreatedBy = user,
                    IP = ip.ToString()
                });
                return false;
            }
        }

        public List<ChamAn> DS_ChamAn(string makp, string manv, string thoidiem, string fromDate, string toDate)
        {
            try
            {
                return _suatAnRepo.DS_ChamAn(StaticParams.connectionStringWiseEyeWebOn, makp, manv, thoidiem, fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region SUẤT ĂN NỘI TRÚ

        public IEnumerable<SuatAnNoiTru_HienDien> SANT_HienDien(string makp = "", string mabn = "")
        {
            try
            {
                return null;
                //return _suatAnRepo.SANT_HienDien(StaticParams.connectionOracle, makp, mabn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SuatAnNoiTru_HienDien> SANT_DanhSachDaDK(string mabn, string tungay, string denngay)
        {
            try
            {
                return null;
                //return _suatAnRepo.SANT_DanhSachDaDK(StaticParams.connectionStringWiseEyeWebOn, StaticParams.connectionOracle, mabn, tungay, denngay);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, string> SANT_DanhSachKhoaPhong()
        {
            try
            {
                return null;
                //return _suatAnRepo.SANT_DanhSachKhoaPhong(StaticParams.connectionOracle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SuatAnNoiTru_NhomBenh> SANT_DanhSachNhomBenh(string doituong)
        {
            try
            {
                return _suatAnRepo.SANT_DanhSachNhomBenh(StaticParams.connectionStringWiseEyeWebOn, doituong);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SuatAnNoiTru_DoiTuong> SANT_DanhSachDoiTuong(int nhombenh)
        {
            try
            {
                return _suatAnRepo.SANT_DanhSachDoiTuong(StaticParams.connectionStringWiseEyeWebOn, nhombenh);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SuatAnNoiTru_CheBien> SANT_DanhSachCheBien(int doituong)
        {
            try
            {
                return _suatAnRepo.SANT_DanhSachCheBien(StaticParams.connectionStringWiseEyeWebOn, doituong);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SANT_CapNhatSuatAnNoiTru(SuatAnNoiTru_KhaiBao obj)
        {
            try
            {
                obj.NGUOI_CAP_NHAT = HttpContext.Current.User.Identity.Name;

                return _suatAnRepo.SANT_CapNhatSuatAnNoiTru(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SuatAnNoiTru_SoLuong> SANT_TongHopSoLuong(string mabn, string from, string to)
        {
            try
            {
                var fromFormat = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var toFormat = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                return _suatAnRepo.SANT_TongHopSoLuong(StaticParams.connectionStringWiseEyeWebOn, mabn, fromFormat, toFormat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion SUẤT ĂN NỘI TRÚ

        public List<SelectListItem> EatPlace()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var noian = _suatAnRepo.EatPlace(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = "-1", Text = "-- Tất cả --" });

            foreach (var item in noian)
            {
                items.Add(new SelectListItem() { Value = item.STT.ToString(), Text = item.KhoaP });
            }
            return items;
        }
        public List<SelectListItem> DSKhoaPhong_Eat()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = _suatAnRepo.DSKhoaPhong_Eat(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = "-1", Text = "-- Tất cả --" });

            foreach (var item in depts)
            {
                items.Add(new SelectListItem() { Value = item.STT.ToString(), Text = item.KhoaP });
            }
            return items;
        }
        public DataTable ThongKe_ChamAn(string dept, string tungay, string denngay, string thoidiem, string noian, string loai, string theo)
        {
            return _suatAnRepo.ThongKe_ChamAn(StaticParams.connectionStringWiseEyeWebOn, dept, tungay, denngay, thoidiem, noian, loai, theo);
        }
    }
}