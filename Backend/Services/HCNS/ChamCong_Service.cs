using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Repositories.HCNS;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.App.Services.HCNS
{
    public interface ChamCong_Interface
    {
        IEnumerable<CC_CheckInOut> DanhSach_SuaCong(string manv, string makp, string tungay, string denngay);
        CC_CheckInOut Tim_Cong(string id, string date, string time);
        bool Update_DuLieuCong(string id, string date, string time, string newTime);
        bool Xoa_DuLieuCong(string id, string date, string time);
        bool Add_DuLieuCong(CC_CheckInOut obj);
        IEnumerable<CC_TongHopCong_ChiTiet> DanhSach_TongCong(string makp, string manv, string tungay, string denngay);
        DataTable DanhSachCa_View(DateTime fromDate, DateTime toDate, int userID, int makp, int type, int qldd = 0);
        UserTempShift DanhSachCa_ThongTin(UserTempShift obj);
        UserTempShift DanhSachCa_ThongTinR(UserTempShift obj);
        IEnumerable<Shifts> DanhSachCa_Dropdownlist(int user, string auth = "");
        IEnumerable<Shifts> DanhSachCa_Checkbox();
        IEnumerable<Shifts> DS_Ca(Shifts obj, ref string errorMessage);
        string DanhSachCa_KiemTraTrung(UserTempShift obj);
        string DanhSachCa_KiemTraTrungR(UserTempShift obj);
        bool DanhSachCa_AddCaMoi(UserTempShift obj);
        bool DanhSachCa_AddNhieuCaMoi(UserTempShift obj);
        bool DanhSachCa_AddRequestCaMoi(UserTempShift obj);
        bool DanhSachCa_UpdateCaMoi(UserTempShift obj);
        bool Xoa_KhaiBaoCa_Phu(string user, string date);
        IEnumerable<Schedule> LichTrinh_Dropdownlist();
        bool LichTrinh_CapNhat(List<UserInfo> objs);
        Dictionary<int, string> DanhSachKhoaPhong(string kp = "", int userid = 0);
        Dictionary<int, string> DanhSachKhoaPhong_KBV(string kp = "", int userid = 0);
        Dictionary<int, string> DanhSachKhoaPhongQL(int userid);
        HCNS_NhanVien TimThongTinNhanVien(HCNS_NhanVien nv);
        IEnumerable<WorkStatsType> TGLV_DDL(int type = 0);
        Dictionary<int, string> TGLV_TTType_Ops(int department_id);
        Dictionary<int, string> TGLV_LTGType_Ops(int department_id);

        string TGLV_KiemTraTrung(UserWStats obj);
        bool Update_TGLV(UserWStats obj);
        UserWStats TGLV_ThongTin(UserWStats obj);
        IEnumerable<UserWStats> TGLV_View(DateTime fromDateFormat, DateTime toDateFormat, int userID, int makp, int ot, int tt);
        bool Clear_TGLV(UserWStats obj);
        bool Update_GioiHanCa(string users, string caphu);
        IEnumerable<XemCong> DS_Cong_TheoTK(XemCong obj);
        Dictionary<int, string> DS_LyDo_DeXuat();
        bool Update_Lydo_DeXuat_User(UserWTRequests obj);
        bool Delete_DeXuatLoi(int userid, string date, out string message);
        bool Delete_DeXuatCa(string auth, int userid, string date, out string message);
        bool Delete_DeXuatKBVang(int userid, string date, out string message);
        int KiemTra_DuyetDeXuat(UserWTRequests obj);
        UserWTRequests ThongTinDeXuat(int user, string date);
        IEnumerable<HCNS_NhanVien> TimNhanVienTheoKhoa(string prefix, int user);
        IEnumerable<HCNS_NhanVien> TimNhanVienTheoKhoa(string prefix, string kp);
        IEnumerable<HCNS_NhanVien> TimNhanVienTheoKhoa(string prefix, int user, int kp);
        //IEnumerable<HCNS_NhanVien> TimNhanVienTheoKhoaAll(string prefix, int user, int kp);
        DataTable QLCong_View(DateTime fromDateFormat, DateTime toDateFormat, int userID, int makp, int is_qldd = 0);
        DataTable QLCong_Excel(string fromDateFormat, string toDateFormat, int userID, int makp, int qldd = 0);
        DataTable QLCong_Detail(DateTime fromDateFormat, DateTime toDateFormat, int userID);
        DataTable QLCong_Detail_Cell(DateTime dateFormat, int userID);
        DataTable DanhSachDeXuat(DateTime fromDate, DateTime toDate, string kp, int fix, string userID, int status);
        Absent KhaiBaoVang_ThongTin(int userid, string date);

        IEnumerable<ShiftUnconfirmed> DanhSachCa_ChuaDuyet(string from, string to, int kp, string userid);
        IEnumerable<UserWStats> DanhSachTGLV_ChuaDuyet(string from, string to, int kp, int userid);
        bool DanhSachCa_XuLyDuyetCa(string[] ids, string[] dates, int status);
        bool DanhSachCa_XuLyHuyCa(string[] ids, string[] dates, int status);
        bool DanhSachTGLV_XuLyDuyetTGLV(string[] ids_1, string[] dates_1, string[] ids_2, string[] dates_2, string[] ids_3, string[] dates_3, int status);
        IEnumerable<UserTempShift> DanhSachCa_ThongTinDaKhaiBao(UserTempShift obj);
        bool ThemMoiKhaiBaoR(AbsentR obj);
        bool ThemMoiKhaiBaoR(List<AbsentR> objs, ref string error);
        AbsentR KhaiBaoVang_ThongTinR(int userid, string date);
        bool XoaKhaiBaoVangR(int id, string date, ref string error);
        IEnumerable<AbsentR> KhaiBaoVang_DanhSachChoDuyet(AbsentR obj);
        bool KhaiBaoVang_KiemTraDeXuat(AbsentR obj, string ngaydachon, ref string errorMessage);
        bool KhaiBaoVang_KiemTraDeXuat(int userenrollnumber, string absentCode, string ngaydachon, ref string errorMessage);
        bool KhaiBaoVang_XuLyDuyet(string[] ids, string[] dates, int status);
        DataTable DanhSachDeXuat_Admin(DateTime fromDate, DateTime toDate, string kp, int userID);
        bool DuyetGiaiTrinh(string auth, int userid, string date, string note, int status);
        bool HuyDuyetGiaiTrinh(string auth, int userid, string date, string note, int status);
        bool HuyDuyetGiaiTrinh(int userid, string date, string col);
        bool XoaGiaiTrinh(int userid, string date, string col);
        bool Update_DuyetGiaiTrinh(string[] vals);
        UserWTRequests GiaiTrinhSuaLoi_ThongTin(int userid, string date);
        bool KhaiBaoVang_UpdateStatus(int userid, string date, int status);
        Dictionary<int, string> DanhSachLoaiNhanVien();
        bool Update_DuyetNhanhGiaiTrinh(string auth, string dateString);
        bool Update_HuyDuyetNhanhGiaiTrinh(string auth, string dateString);
        int[] DanhSachCa_CheckboxWithDefaultValueUser(int user);

        DataTable DS_KhaiBaoAn(int userid, string date);
        bool XacNhanHuy(KhaiBaoAn obj, ref string errorMessage);
        bool XacNhan(KhaiBaoAn obj, ref string errorMessage);

        DiaDiemAn Get_NoiAn(int user);
        List<SelectListItem> DSKhoaPhong(int userenrollnumber = -1);
        List<SelectListItem> DSLoaiNhanVien();
        IEnumerable<HCNS_NhanVien> Table_VanTayThucTe(HCNS_NhanVien objsearch);
        DataTable Excel_VanTayThucTe(HCNS_NhanVien oSearch);
        TinhPhep Get_Phep(string curDate, int user);
        #region EXCEL

        DataTable Excel_DanhSachGiaiTrinh(Excel_GiaiTrinh obj);
        DataTable Excel_DanhSachTGLV(Excel_TGLV obj);
        DataTable Excel_XemCong(XemCong obj);
        XemCong_TongCong Excel_XemCong_TongCong(XemCong obj);
        DataTable TongHopCong_Excel(TongHopCong_Filter obj);

        #endregion EXCEL

        #region LÀM THÊM TÍNH TIỀN

        bool LamThemTT_Approve(List<OTPayment> objs, string auth);
        bool LamThemTT_Add(OTPayment obj);
        IEnumerable<OTPayment> LamThemTT_DanhSach(OTPayment_Filter oFilter);
        DataTable LamThemTT_DanhSachExcel(OTPayment_Filter oFilter);
        bool LamThemTT_HuyDuyet(OTPayment obj);
        bool LamThemTT_Xoa(OTPayment obj);
        Dictionary<int, string> LamThemTT_Type();
        bool OnCall_Send(GhepCa obj, string lydo, out string mess);
        bool OnCall_Del(GhepCa obj, out string mess);

        #endregion LÀM THÊM TÍNH TIỀN

        #region CA KHÔNG XÁC ĐỊNH

        IEnumerable<CaKhongXacDinh> CaKhongXacDinh_DanhSach(CaKhongXacDinh_Filter obj);
        bool CaKhongXacDinh_UpdateApprove(List<CaKhongXacDinh> obj);
        bool CaKhongXacDinh_Del(CaKhongXacDinh obj);

        #endregion CA KHÔNG XÁC ĐỊNH

        IEnumerable<TongHopCong> TongHopCong_View(string fromDate, string toDate, int userid, int kp, int loainv);
        bool TongHopCong_CapNhatChiSo(int userid, string date, string column, string val, ref string errorMessage);
        int KhoaSoLieu_TrangThai();
        bool KhoaSoLieu_CapNhat(int status);
        bool TongHopCong_TongHopSoLieu(string ngaybd, string ngaykt, ref string error);
        //DataTable Table_LamViecNgayLe(DateTime fromDate, DateTime toDate, string kp, string userID, int status);
        IEnumerable<LamViecNgayLe> Table_LamViecNgayLe(LamViecNgayLe objsearch);
        bool Push_DeXuatLamViecNgayLe(string dataString, string lydo, string loaica, int type, string approve);
        DataTable Excel_LamViecNgayLe(LamViecNgayLe oSearch);
        bool ChuyenKyLuong(string ngaybd, string ngaykt, ref string error);
    }
    public class ChamCong_Service : ChamCong_Interface
    {
        private readonly ChamCong_Repo _chamCongRepo;

        private readonly NhanVien_Repo _nhanvienRepo;

        private readonly KhaiBaoVang_Repo _khaiBaoVangRepo;

        public ChamCong_Service()
        {
            _chamCongRepo = new ChamCong_Repo();
            _nhanvienRepo = new NhanVien_Repo();
            _khaiBaoVangRepo = new KhaiBaoVang_Repo();
        }

        public Dictionary<int, string> DanhSachKhoaPhong(string kp = "", int userid = 0)
        {
            Dictionary<int, string> listDepartment = new Dictionary<int, string>();

            var lst = _chamCongRepo.DanhSachKhoaPhongQL(StaticParams.connectionStringWiseEyeWebOn, userid);

            if (lst.Count() > 0)
            {
                foreach (var o in lst)
                {
                    listDepartment.Add(o.STT, o.KhoaP);
                }

                return listDepartment;
            }
            else
            {
                var models = _chamCongRepo.DanhSachKhoaPhong(StaticParams.connectionStringWiseEyeWebOn, kp);

                //listDepartment.Add(-1, "--- Tất cả ---");
                foreach (var o in models)
                {
                    listDepartment.Add(o.STT, o.KhoaP);
                }

                return listDepartment;
            }
        }

        public Dictionary<int, string> DanhSachKhoaPhong_KBV(string kp = "", int userid = 0)
        {
            Dictionary<int, string> listDepartment = new Dictionary<int, string>();

            var lst = _chamCongRepo.DanhSachKhoaPhongQL(StaticParams.connectionStringWiseEyeWebOn, userid);

            if (lst.Count() > 0)
            {
                foreach (var o in lst)
                {
                    listDepartment.Add(o.STT, o.KhoaP);
                }

                return listDepartment;
            }
            else
            {
                var models = _chamCongRepo.DanhSachKhoaPhong(StaticParams.connectionStringWiseEyeWebOn, kp);

                listDepartment.Add(-1, "--- Tất cả ---");
                foreach (var o in models)
                {
                    listDepartment.Add(o.STT, o.KhoaP);
                }

                return listDepartment;
            }
        }

        public Dictionary<int, string> DanhSachKhoaPhongQL(int userid)
        {
            Dictionary<int, string> listDepartment = new Dictionary<int, string>();

            var models = _chamCongRepo.DanhSachKhoaPhongQL(StaticParams.connectionStringWiseEyeWebOn, userid);

            foreach (var o in models)
            {
                listDepartment.Add(o.STT, o.KhoaP);
            }

            return listDepartment;
        }

        public bool Add_DuLieuCong(CC_CheckInOut obj)
        {
            try
            {
                obj.TimeDate = DateTime.ParseExact(obj.TimeDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                obj.TimeStr = obj.TimeDate + " " + obj.TimeStr;

                return _chamCongRepo.Add_DuLieuCong(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DanhSachCa_AddCaMoi(UserTempShift obj)
        {
            try
            {
                obj.CreateAcc = HttpContext.Current.User.Identity.Name;
                obj.UpdateAcc = HttpContext.Current.User.Identity.Name;

                return _chamCongRepo.DanhSachCa_AddCaMoi(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DanhSachCa_AddNhieuCaMoi(UserTempShift obj)
        {
            try
            {
                List<UserTempShift> objs = new List<UserTempShift>();

                var usersplit = obj.Users.Split(',');
                var datesplit = obj.DateString.Split(',');

                for (int u = 0; u < usersplit.Length; u++)
                {
                    UserTempShift us = new UserTempShift();
                    us.UserEnrollNumber = int.Parse(usersplit[u].ToString());
                    us.DateString = obj.DateString;
                    us.Shift1 = obj.Shift1;
                    us.CreateAcc = HttpContext.Current.User.Identity.Name;
                    us.UpdateAcc = HttpContext.Current.User.Identity.Name;
                    objs.Add(us);
                }

                return _chamCongRepo.DanhSachCa_AddNhieuCaMoi(StaticParams.connectionStringWiseEyeWebOn, objs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DanhSachCa_AddRequestCaMoi(UserTempShift obj)
        {
            try
            {
                obj.CreateAcc = HttpContext.Current.User.Identity.Name;
                obj.UpdateAcc = HttpContext.Current.User.Identity.Name;

                return _chamCongRepo.DanhSachCa_AddRequestCaMoi(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Shifts> DanhSachCa_Dropdownlist(int user, string auth = "")
        {
            if (auth == "HCNS_Admin")
            {
                return _chamCongRepo.DanhSachCa_DropDownList_All(StaticParams.connectionStringWiseEyeWebOn);
            }
            return _chamCongRepo.DanhSachCa_Dropdownlist(StaticParams.connectionStringWiseEyeWebOn, user);
        }

        public IEnumerable<Shifts> DanhSachCa_Checkbox()
        {
            return _chamCongRepo.DanhSachCa_Checkbox(StaticParams.connectionStringWiseEyeWebOn);
        }

        public string DanhSachCa_KiemTraTrung(UserTempShift obj)
        {
            try
            {
                return _chamCongRepo.DanhSachCa_KiemTraTrung(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DanhSachCa_KiemTraTrungR(UserTempShift obj)
        {
            try
            {
                return _chamCongRepo.DanhSachCa_KiemTraTrungR(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DanhSachCa_UpdateCaMoi(UserTempShift obj)
        {
            try
            {
                obj.UpdateAcc = HttpContext.Current.User.Identity.Name;

                return _chamCongRepo.DanhSachCa_UpdateCaMoi(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DanhSachCa_View(DateTime fromDateFormat, DateTime toDateFormat, int userID, int makp, int type, int qldd = 0)
        {
            try
            {
                string dateString = "";

                //Format ngày tháng
                string fromDate = fromDateFormat.ToString("yyyyMMdd");
                string toDate = toDateFormat.ToString("yyyyMMdd");

                //var countDate = (toDateFormat - fromDateFormat).TotalDays;

                //for (int i = 0; i <= countDate; i++)
                //{
                //    dateString += "[" + fromDateFormat.AddDays(i).ToString("dd/MM") + "],";
                //}

                return _chamCongRepo.DanhSachCa_View(StaticParams.connectionStringWiseEyeWebOn, dateString, fromDate, toDate, userID, makp, type, qldd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CC_CheckInOut> DanhSach_SuaCong(string manv, string makp, string tungay, string denngay)
        {
            try
            {
                return _chamCongRepo.DanhSach_SuaCong(StaticParams.connectionStringWiseEyeWebOn, makp, manv, tungay, denngay);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CC_TongHopCong_ChiTiet> DanhSach_TongCong(string makp, string manv, string tungay, string denngay)
        {
            try
            {
                return _chamCongRepo.DanhSach_TongCong(StaticParams.connectionStringWiseEyeWebOn, makp, manv, tungay, denngay);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool LichTrinh_CapNhat(List<UserInfo> objs)
        {
            try
            {
                return _chamCongRepo.LichTrinh_CapNhat(StaticParams.connectionStringWiseEyeWebOn, objs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Schedule> LichTrinh_Dropdownlist()
        {
            try
            {
                return _chamCongRepo.LichTrinh_Dropdownlist(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Shifts> DS_Ca(Shifts obj, ref string errorMessage)
        {
            errorMessage = "";

            try
            {
                return _chamCongRepo.DS_Ca(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

                return null;
            }
        }

        public CC_CheckInOut Tim_Cong(string id, string date, string time)
        {
            try
            {
                var timeStr = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " " + time;

                return _chamCongRepo.Tim_Cong(StaticParams.connectionStringWiseEyeWebOn, id, timeStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update_DuLieuCong(string id, string date, string time, string newTime)
        {
            try
            {
                var timeStr = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " " + time;
                var obj = _chamCongRepo.Tim_Cong(StaticParams.connectionStringWiseEyeWebOn, id, timeStr);
                obj.TimeDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                obj.NewTime = newTime;

                return _chamCongRepo.Update_DuLieuCong(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Xoa_DuLieuCong(string id, string date, string time)
        {
            try
            {
                var timeStr = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " " + time;
                var obj = _chamCongRepo.Tim_Cong(StaticParams.connectionStringWiseEyeWebOn, id, timeStr);
                obj.TimeDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                return _chamCongRepo.Xoa_DuLieuCong(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Xoa_KhaiBaoCa_Phu(string user, string date)
        {
            try
            {
                return _chamCongRepo.Xoa_KhaiBaoCa_Phu(StaticParams.connectionStringWiseEyeWebOn, user, date);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool XuLyDuLieuTuMayChamCong(string fromDate, string toDate)
        {
            try
            {
                var _cc_FromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var _cc_ToDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                fromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");

                toDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");

                var ds_nhanvien_da_cc = _chamCongRepo.DS_NhanVienDaChamCong(StaticParams.connectionStringWiseEyeWebOn, fromDate, toDate);

                lock (ds_nhanvien_da_cc)
                {
                    //for (int i = 0; i < ds_nhanvien_da_cc.Count; i++)
                    //{
                    //int id_nhanvien = ds_nhanvien_da_cc[i];

                    int id_nhanvien = 718;

                    var ds_ca = _chamCongRepo.DS_CaChamCong_TheoKhoaPhong(StaticParams.connectionStringWiseEyeWebOn, id_nhanvien);

                    if (ds_ca.Count() > 0)
                    {
                        var ds_congdacham = _chamCongRepo.DS_DuLieuTuMayChamCong_NhanVien(StaticParams.connectionStringWiseEyeWebOn, fromDate, toDate, id_nhanvien);

                        if (ds_congdacham.Count() > 0)
                        {
                            foreach (var o in ds_congdacham)
                            {
                                //ngay cham cham cong
                                var _date = o.TimeDate;
                                var _time = o.TimeStr;
                                var _machine = o.MachineNo;
                                var _chamcong = DateTime.ParseExact(o.TimeStr, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                                //tinh toan ngay cham cong theo ca
                                foreach (var ca in ds_ca)
                                {
                                    var _heso = ca.HeSo;

                                    var _sGioVao = DateTime.ParseExact(_date + " " + ca.GioVao, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                                    var _eGioRa = DateTime.ParseExact(_date + " " + ca.GioRa, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                                    var _sVao = DateTime.ParseExact(_date + " " + ca.BatDauVao, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                                    var _eVao = DateTime.ParseExact(_date + " " + ca.KetThucVao, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                                    var _sRa = DateTime.ParseExact(_date + " " + ca.BatDauRa, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                                    var _eRa = DateTime.ParseExact(_date + " " + ca.KetThucRa, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                                    // Tính công theo ca
                                    if (_heso > 0)
                                    {
                                        _sRa = _sRa.AddDays(_heso);
                                        _eRa = _eRa.AddDays(_heso);
                                    }

                                    if (_chamcong.Ticks >= _sVao.Ticks && _chamcong.Ticks <= _eVao.Ticks)
                                    {
                                        //insert du lieu vao in-out-col
                                        var cc = new CC_InOutCol()
                                        {
                                            UserEnrollNumber = id_nhanvien,
                                            TimeDate = _chamcong.ToString(),
                                            TimeIn = _chamcong.ToString(),
                                            TimeOut = _chamcong.ToString(),
                                            MachineNoIn = _machine,
                                            MachineNoOut = _machine,
                                            OverDay = _heso
                                        };
                                        _chamCongRepo.CapNhat_InOutCol(StaticParams.connectionStringWiseEyeWebOn, cc);
                                    }
                                    else if (_chamcong.Ticks >= _sRa.Ticks && _chamcong.Ticks <= _eRa.Ticks)
                                    {
                                    }
                                }
                            }
                        }
                    }

                    //}
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public HCNS_NhanVien TimThongTinNhanVien(HCNS_NhanVien nv)
        {
            try
            {
                return _nhanvienRepo.TimThongTinNhanVien(StaticParams.connectionStringWiseEyeWebOn, nv);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<WorkStatsType> TGLV_DDL(int type = 0)
        {
            try
            {
                return _chamCongRepo.TGLV_DDL(StaticParams.connectionStringWiseEyeWebOn, type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<int, string> TGLV_TTType_Ops(int department_id)
        {
            try
            {
                return _chamCongRepo.TGLV_TTType_Ops(StaticParams.connectionStringWiseEyeWebOn, department_id);
            }
            catch (Exception)
            {
                return new Dictionary<int, string>();
            }
        }

        public Dictionary<int, string> TGLV_LTGType_Ops(int department_id)
        {
            try
            {
                return _chamCongRepo.TGLV_LTGType_Ops(StaticParams.connectionStringWiseEyeWebOn, department_id);
            }
            catch (Exception)
            {
                return new Dictionary<int, string>();
            }
        }

        public string TGLV_KiemTraTrung(UserWStats obj)
        {
            try
            {
                return _chamCongRepo.TGLV_KiemTraTrung(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update_TGLV(UserWStats obj)
        {
            try
            {
                List<UserWStats> objs = new List<UserWStats>();
                var date = obj.DateString.Split(',');
                for (int i = 0; i < obj.DateString.Split(',').Length; i++)
                {
                    objs.Add(new UserWStats()
                    {
                        UserEnrollNumber = obj.UserEnrollNumber,
                        Date = date[i].ToString(),
                        WStats1 = string.IsNullOrEmpty(obj.WStats1) ? "" : obj.WStats1.ToString(),
                        WStats2 = string.IsNullOrEmpty(obj.WStats2) ? "" : obj.WStats2.ToString(),
                        WStats3 = string.IsNullOrEmpty(obj.WStats3) ? "" : obj.WStats3.ToString(),
                        W1Stats = obj.W1Stats,
                        W2Stats = obj.W2Stats,
                        W3Stats = obj.W3Stats,
                        Note = obj.Note,
                        TTType = obj.TTType,
                        LTGType = obj.LTGType,
                        CreateAcc = HttpContext.Current.User.Identity.Name,
                        UpdateAcc = HttpContext.Current.User.Identity.Name,
                        CreateTime = DateTime.UtcNow.AddHours(7),
                        UpdateTime = DateTime.UtcNow.AddHours(7)
                    });
                }

                return _chamCongRepo.Update_TGLV(StaticParams.connectionStringWiseEyeWebOn, objs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<UserWStats> TGLV_View(DateTime fromDateFormat, DateTime toDateFormat, int userID, int makp, int ot, int tt)
        {
            try
            {
                //Format ngày tháng
                string fromDate = fromDateFormat.ToString("yyyyMMdd");
                string toDate = toDateFormat.ToString("yyyyMMdd");

                return _chamCongRepo.TGLV_View(StaticParams.connectionStringWiseEyeWebOn, fromDate, toDate, userID, makp, ot, tt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Clear_TGLV(UserWStats obj)
        {
            try
            {
                return _chamCongRepo.Clear_TGLV(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update_GioiHanCa(string users, string caphu)
        {
            try
            {
                List<UserInfExt> objs = new List<UserInfExt>();
                var splitUser = users.Split(',');

                for (int i = 0; i < splitUser.Length; i++)
                {
                    objs.Add(new UserInfExt() { UserEnrollNumber = int.Parse(splitUser[i].ToString()), CaPhu = caphu });
                }

                return _chamCongRepo.Update_GioiHanCa(StaticParams.connectionStringWiseEyeWebOn, objs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<XemCong> DS_Cong_TheoTK(XemCong obj)
        {
            try
            {
                return _chamCongRepo.DS_Cong_TheoTK(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<int, string> DS_LyDo_DeXuat()
        {
            try
            {
                return _chamCongRepo.DS_LyDo_DeXuat(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update_Lydo_DeXuat_User(UserWTRequests obj)
        {
            try
            {
                return _chamCongRepo.Update_Lydo_DeXuat_User(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete_DeXuatLoi(int userid, string date, out string message)
        {
            message = "";
            try
            {
                if (string.IsNullOrEmpty(date) || userid == 0)
                    return false;

                DateTime _date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (_chamCongRepo.Delete_DeXuatLoi(StaticParams.connectionStringWiseEyeWebOn, userid, _date))
                {
                    return true;
                }
                else
                {
                    message = "Đề xuất đã được duyệt, bạn không thể hủy.";
                }

                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete_DeXuatCa(string auth, int userid, string date, out string message)
        {
            message = "";
            try
            {
                if (string.IsNullOrEmpty(date) || userid == 0)
                    return false;

                DateTime _date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (auth == "HCNS_Manager" && _chamCongRepo.Delete_DeXuatCa(StaticParams.connectionStringWiseEyeWebOn, userid, _date))
                {
                    return true;
                }
                else
                {
                    message = "Bạn không thể hủy đề xuất này.";
                }

                if (_chamCongRepo.Delete_DeXuatCaUser(StaticParams.connectionStringWiseEyeWebOn, userid, _date))
                {
                    return true;
                }
                else
                {
                    message = "Đề xuất đã được duyệt, bạn không thể hủy.";
                }

                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Delete_DeXuatKBVang(int userid, string date, out string message)
        {
            message = "";
            try
            {
                if (string.IsNullOrEmpty(date) || userid == 0)
                    return false;

                DateTime _date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (_chamCongRepo.Delete_DeXuatKBVang(StaticParams.connectionStringWiseEyeWebOn, userid, _date))
                {
                    return true;
                }
                else
                {
                    message = "Đề xuất đã được duyệt, bạn không thể hủy.";
                }

                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int KiemTra_DuyetDeXuat(UserWTRequests obj)
        {
            try
            {
                return _chamCongRepo.KiemTra_DuyetDeXuat(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public UserWTRequests ThongTinDeXuat(int user, string date)
        {
            try
            {
                return _chamCongRepo.ThongTinDeXuat(StaticParams.connectionStringWiseEyeWebOn, user, date);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<HCNS_NhanVien> TimNhanVienTheoKhoa(string prefix, int user)
        {
            try
            {
                return _chamCongRepo.TimNhanVienTheoKhoa(StaticParams.connectionStringWiseEyeWebOn, prefix, user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<HCNS_NhanVien> TimNhanVienTheoKhoa(string prefix, string kp)
        {
            try
            {
                return _chamCongRepo.TimNhanVienTheoKhoa(StaticParams.connectionStringWiseEyeWebOn, prefix, kp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<HCNS_NhanVien> TimNhanVienTheoKhoa(string prefix, int user, int kp)
        {
            try
            {
                return _chamCongRepo.TimNhanVienTheoKhoa(StaticParams.connectionStringWiseEyeWebOn, prefix, user, kp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataTable QLCong_View(DateTime fromDateFormat, DateTime toDateFormat, int userID, int makp, int is_qldd = 0)
        {
            try
            {
                string dateString = "";

                //Format ngày tháng
                string fromDate = fromDateFormat.ToString("yyyyMMdd");
                string toDate = toDateFormat.ToString("yyyyMMdd");

                var countDate = (toDateFormat - fromDateFormat).TotalDays;

                for (int i = 0; i <= countDate; i++)
                {
                    dateString += "[" + fromDateFormat.AddDays(i).ToString("dd/MM") + "],";
                }

                return _chamCongRepo.QLCong_View(StaticParams.connectionStringWiseEyeWebOn, dateString, fromDate, toDate, userID, makp, is_qldd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable QLCong_Detail(DateTime fromDateFormat, DateTime toDateFormat, int userID)
        {
            try
            {
                //Format ngày tháng
                string fromDate = fromDateFormat.ToString("yyyyMMdd");
                string toDate = toDateFormat.ToString("yyyyMMdd");

                return _chamCongRepo.QLCong_Detail(StaticParams.connectionStringWiseEyeWebOn, fromDate, toDate, userID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable QLCong_Detail_Cell(DateTime dateFormat, int userID)
        {
            try
            {
                //Format ngày tháng
                string date = dateFormat.ToString("yyyyMMdd");

                return _chamCongRepo.QLCong_Detail_Cell(StaticParams.connectionStringWiseEyeWebOn, date, userID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DanhSachDeXuat(DateTime fromDate, DateTime toDate, string kp, int fix, string userID, int status)
        {
            try
            {
                return _chamCongRepo.DanhSachDeXuat(StaticParams.connectionStringWiseEyeWebOn, fromDate, toDate, kp, fix, userID, status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DanhSachDeXuat_Admin(DateTime fromDate, DateTime toDate, string kp, int userID)
        {
            try
            {
                return _chamCongRepo.DanhSachDeXuat_Admin(StaticParams.connectionStringWiseEyeWebOn, fromDate, toDate, kp, userID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserTempShift DanhSachCa_ThongTin(UserTempShift obj)
        {
            try
            {
                return _chamCongRepo.DanhSachCa_ThongTin(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserTempShift DanhSachCa_ThongTinR(UserTempShift obj)
        {
            try
            {
                return _chamCongRepo.DanhSachCa_ThongTinR(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserWStats TGLV_ThongTin(UserWStats obj)
        {
            try
            {
                return _chamCongRepo.TGLV_ThongTin(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Absent KhaiBaoVang_ThongTin(int userid, string date)
        {
            try
            {
                return _chamCongRepo.KhaiBaoVang_ThongTin(StaticParams.connectionStringWiseEyeWebOn, userid, date);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool KhaiBaoVang_KiemTraDeXuat(AbsentR obj, string ngaydachon, ref string errorMessage)
        {
            try
            {
                return !_khaiBaoVangRepo.KiemTraKhaiBao(StaticParams.connectionStringWiseEyeWebOn,
                    new Absent()
                    {
                        UserEnrollNumber = obj.UserEnrollNumber,
                        AbsentCode = obj.AbsentCode,
                        CheckDateString = ngaydachon,
                        ForType = 0
                    }, ref errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return false;
        }
        public bool KhaiBaoVang_KiemTraDeXuat(int userenrollnumber, string absentCode, string ngaydachon, ref string errorMessage)
        {
            try
            {
                return !_khaiBaoVangRepo.KiemTraKhaiBao(StaticParams.connectionStringWiseEyeWebOn,
                    new Absent()
                    {
                        UserEnrollNumber = userenrollnumber,
                        AbsentCode = absentCode,
                        CheckDateString = ngaydachon,
                        ForType = 0
                    }, ref errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return false;
        }
        public AbsentR KhaiBaoVang_ThongTinR(int userid, string date)
        {
            try
            {
                return _chamCongRepo.KhaiBaoVang_ThongTinR(StaticParams.connectionStringWiseEyeWebOn, userid, date);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ShiftUnconfirmed> DanhSachCa_ChuaDuyet(string from, string to, int kp, string userid)
        {
            try
            {
                DateTime fromFormat = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime toFormat = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                return _chamCongRepo.DanhSachCa_ChuaDuyet(StaticParams.connectionStringWiseEyeWebOn, fromFormat, toFormat, kp, userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DanhSachCa_XuLyDuyetCa(string[] ids, string[] dates, int status)
        {
            try
            {
                List<ShiftUnconfirmed> lst = new List<ShiftUnconfirmed>();

                var countItem = ids.Count();

                for (int i = 0; i < countItem; i++)
                {
                    var _id = int.Parse(ids[i].ToString());
                    var _date = dates[i].ToString();

                    lst.Add(new ShiftUnconfirmed()
                    {
                        UserEnrollNumber = _id,
                        Date = _date,
                        Status1 = status
                    });
                }

                return _chamCongRepo.DanhSachCa_XuLyDuyetCa(StaticParams.connectionStringWiseEyeWebOn, lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DanhSachCa_XuLyHuyCa(string[] ids, string[] dates, int status)
        {
            try
            {
                List<ShiftUnconfirmed> lst = new List<ShiftUnconfirmed>();

                var countItem = ids.Count();

                for (int i = 0; i < countItem; i++)
                {
                    var _id = int.Parse(ids[i].ToString());
                    var _date = dates[i].ToString();

                    lst.Add(new ShiftUnconfirmed()
                    {
                        UserEnrollNumber = _id,
                        Date = _date,
                        Status1 = status
                    });
                }

                return _chamCongRepo.DanhSachCa_XuLyHuyCa(StaticParams.connectionStringWiseEyeWebOn, lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<UserWStats> DanhSachTGLV_ChuaDuyet(string from, string to, int kp, int userid)
        {
            try
            {
                DateTime fromFormat = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime toFormat = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                return _chamCongRepo.DanhSachTGLV_ChuaDuyet(StaticParams.connectionStringWiseEyeWebOn, fromFormat, toFormat, kp, userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DanhSachTGLV_XuLyDuyetTGLV(string[] ids_1, string[] dates_1, string[] ids_2, string[] dates_2, string[] ids_3, string[] dates_3, int status)
        {
            try
            {
                List<UserWStats> lst = new List<UserWStats>();

                var countItem_1 = ids_1 == null ? 0 : ids_1.Count();
                var countItem_2 = ids_2 == null ? 0 : ids_2.Count();
                var countItem_3 = ids_3 == null ? 0 : ids_3.Count();

                for (int i = 0; i < countItem_1; i++)
                {
                    var _id = int.Parse(ids_1[i].ToString());
                    var _date = dates_1[i].ToString();
                    lst.Add(new UserWStats() { UserEnrollNumber = _id, Date = _date, W1Stats = status });
                }
                for (int i = 0; i < countItem_2; i++)
                {
                    var _id = int.Parse(ids_2[i].ToString());
                    var _date = dates_2[i].ToString();
                    lst.Add(new UserWStats() { UserEnrollNumber = _id, Date = _date, W2Stats = status });
                }
                for (int i = 0; i < countItem_3; i++)
                {
                    var _id = int.Parse(ids_3[i].ToString());
                    var _date = dates_3[i].ToString();
                    lst.Add(new UserWStats() { UserEnrollNumber = _id, Date = _date, W3Stats = status });
                }

                return _chamCongRepo.DanhSachTGLV_XuLyDuyetTGLV(StaticParams.connectionStringWiseEyeWebOn, lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ThemMoiKhaiBaoR(AbsentR obj)
        {
            try
            {
                obj.CreateAcc = HttpContext.Current.User.Identity.Name;
                obj.UpdateAcc = HttpContext.Current.User.Identity.Name;

                return _chamCongRepo.ThemMoiKhaiBaoR(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool XoaKhaiBaoVangR(int id, string date, ref string error)
        {
            try
            {
                var data = _chamCongRepo.KhaiBaoVang_ThongTinR(StaticParams.connectionStringWiseEyeWebOn, id, date);

                if (data.AbsentCode != null && data.Status < 2)
                {
                    return _chamCongRepo.KhaiBaoVang_XoaKhaiBaoR(StaticParams.connectionStringWiseEyeWebOn, id, date);
                }
                else if (data.AbsentCode == null)
                {
                    error = "Không tìm thấy thông tin cần xóa.";
                }
                else if (data.Status == 2)
                {
                    error = "Khai báo đã được duyệt, không thể thao tác.";
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return false;
        }
        public bool ThemMoiKhaiBaoR(List<AbsentR> objs, ref string error)
        {
            try
            {
                return _chamCongRepo.ThemMoiKhaiBaoR(StaticParams.connectionStringWiseEyeWebOn, objs, HttpContext.Current.User.Identity.Name);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return false;
        }
        public IEnumerable<AbsentR> KhaiBaoVang_DanhSachChoDuyet(AbsentR obj)
        {
            try
            {
                return _chamCongRepo.KhaiBaoVang_DanhSachChoDuyet(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool KhaiBaoVang_XuLyDuyet(string[] ids, string[] dates, int status)
        {
            try
            {
                List<AbsentR> lst = new List<AbsentR>();

                var countItem = ids.Count();

                for (int i = 0; i < countItem; i++)
                {
                    var _id = int.Parse(ids[i].ToString());
                    var _date = dates[i].ToString();

                    var info = _chamCongRepo.KhaiBaoVang_ThongTinR(StaticParams.connectionStringWiseEyeWebOn, _id, _date);

                    if (info == null)
                        return false;

                    lst.Add(new AbsentR()
                    {
                        UserEnrollNumber = _id,
                        Date = _date,
                        AbsentCode = info.AbsentCode,
                        Reason = info.Reason,
                        CreateTime = info.CreateTime,
                        CreateAcc = info.CreateAcc,
                        UpdateAcc = HttpContext.Current.User.Identity.Name,
                        Status = status
                    });
                }
                if (status < 0)
                {
                    return _chamCongRepo.KhaiBaoVang_XuLyHuyDuyet(StaticParams.connectionStringWiseEyeWebOn, lst);
                }
                return _chamCongRepo.KhaiBaoVang_XuLyDuyet(StaticParams.connectionStringWiseEyeWebOn, lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DuyetGiaiTrinh(string auth, int userid, string date, string note, int status)
        {
            try
            {
                if (string.IsNullOrEmpty(date) || userid == 0)
                    return false;

                DateTime _date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (_chamCongRepo.Update_DuyetGiaiTrinh(StaticParams.connectionStringWiseEyeWebOn, auth, userid, _date, note, status))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool HuyDuyetGiaiTrinh(string auth, int userid, string date, string note, int status)
        {
            try
            {
                if (string.IsNullOrEmpty(date) || userid == 0)
                    return false;

                DateTime _date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                note = string.Empty;

                if (_chamCongRepo.Update_DuyetGiaiTrinh(StaticParams.connectionStringWiseEyeWebOn, auth, userid, _date, note, status))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool HuyDuyetGiaiTrinh(int userid, string date, string col)
        {
            try
            {
                if (string.IsNullOrEmpty(date) || userid == 0)
                    return false;

                var dateFormat = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (_chamCongRepo.Update_HuyDuyetGiaiTrinh(StaticParams.connectionStringWiseEyeWebOn, userid, dateFormat, col))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool XoaGiaiTrinh(int userid, string date, string col)
        {
            try
            {
                if (string.IsNullOrEmpty(date) || userid == 0)
                    return false;

                var dateFormat = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (_chamCongRepo.Update_XoaGiaiTrinh(StaticParams.connectionStringWiseEyeWebOn, userid, dateFormat, col))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public UserWTRequests GiaiTrinhSuaLoi_ThongTin(int userid, string date)
        {
            try
            {
                if (string.IsNullOrEmpty(date) || userid == 0)
                    return null;

                DateTime _date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return _chamCongRepo.GiaiTrinhSuaLoi_ThongTin(StaticParams.connectionStringWiseEyeWebOn, userid, _date);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool KhaiBaoVang_UpdateStatus(int userid, string date, int status)
        {
            try
            {
                if (string.IsNullOrEmpty(date) || userid == 0)
                    return false;

                DateTime _date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                return _chamCongRepo.KhaiBaoVang_UpdateStatus(StaticParams.connectionStringWiseEyeWebOn, userid, _date, status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<int, string> DanhSachLoaiNhanVien()
        {
            try
            {
                return _chamCongRepo.DanhSachLoaiNhanVien(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update_DuyetNhanhGiaiTrinh(string auth, string dateString)
        {
            try
            {
                return _chamCongRepo.Update_DuyetNhanhGiaiTrinh(StaticParams.connectionStringWiseEyeWebOn, auth, dateString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update_HuyDuyetNhanhGiaiTrinh(string auth, string dateString)
        {
            try
            {
                return _chamCongRepo.Update_HuyDuyetNhanhGiaiTrinh(StaticParams.connectionStringWiseEyeWebOn, auth, dateString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Excel
        public DataTable QLCong_Excel(string fromDateFormat, string toDateFormat, int userID, int makp, int qldd = 0)
        {
            try
            {
                return _chamCongRepo.QLCong_Excel(StaticParams.connectionStringWiseEyeWebOn, fromDateFormat, toDateFormat, userID, makp, qldd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Excel_DanhSachGiaiTrinh(Excel_GiaiTrinh obj)
        {
            try
            {
                return _chamCongRepo.Excel_DanhSachGiaiTrinh(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Excel_DanhSachTGLV(Excel_TGLV obj)
        {
            try
            {
                return _chamCongRepo.Excel_DanhSachTGLV(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Excel_XemCong(XemCong obj)
        {
            var data = _chamCongRepo.Excel_DS_Cong(StaticParams.connectionStringWiseEyeWebOn, obj);
            DataTable dtable = new DataTable();
            dtable.Columns.Add("Mã NV", typeof(int));
            dtable.Columns.Add("Ngày", typeof(string));
            dtable.Columns.Add("Thứ", typeof(string));
            dtable.Columns.Add("Ca làm việc", typeof(string));
            dtable.Columns.Add("Công", typeof(string));
            dtable.Columns.Add("Giờ vào", typeof(string));
            dtable.Columns.Add("Giờ ra", typeof(string));

            foreach (DataRow row in data.Rows)
            {
                if (row["WD"].ToString() != "0")
                {
                    var ngay_vi = "";

                    switch (row["WD"].ToString())
                    {
                        case "Monday":
                            ngay_vi = "Thứ hai";
                            break;

                        case "Tuesday":
                            ngay_vi = "Thứ ba";
                            break;

                        case "Wednesday":
                            ngay_vi = "Thứ bốn";
                            break;

                        case "Thursday":
                            ngay_vi = "Thứ năm";
                            break;

                        case "Friday":
                            ngay_vi = "Thứ sáu";
                            break;

                        case "Saturday":
                            ngay_vi = "Thứ bảy";
                            break;

                        case "Sunday":
                            ngay_vi = "Chủ nhật";
                            break;

                        default:
                            ngay_vi = "Không xác định";
                            break;
                    }

                    var manv = int.Parse(row["UserEnrollNumber"].ToString());
                    var ngay = DateTime.Parse(row["Date"].ToString()).ToString("dd/MM/yyyy");
                    var thu = ngay_vi;
                    var ca = row["DS"].ToString();
                    var cong = row["ManDay"].ToString();
                    var gio_vao = string.IsNullOrEmpty(row["RIn1"].ToString()) ? "" : DateTime.Parse(row["RIn1"].ToString()).ToString("HH:mm");
                    var gio_ra = string.IsNullOrEmpty(row["ROut1"].ToString()) ? "" : DateTime.Parse(row["ROut1"].ToString()).ToString("HH:mm");

                    dtable.Rows.Add(manv, ngay, thu, ca, cong, gio_vao, gio_ra);
                }
            }
            return dtable;
        }

        public XemCong_TongCong Excel_XemCong_TongCong(XemCong obj)
        {
            var row = _chamCongRepo.Excel_DS_Cong(StaticParams.connectionStringWiseEyeWebOn, obj).Rows[0];

            XemCong_TongCong o = new XemCong_TongCong()
            {
                UserEnrollNumber = int.Parse(row["UserEnrollNumber"].ToString()),
                NgayCong = row["ManDay"].ToString(),
                CongChuan = row["CC"].ToString(),
                DiMuonVeSom = row["LE1"].ToString(),
                SoLanVaoRa = int.Parse(row["NIO1"].ToString())
            };
            return o;
        }

        #endregion Excel

        public IEnumerable<UserTempShift> DanhSachCa_ThongTinDaKhaiBao(UserTempShift obj)
        {
            try
            {
                return _chamCongRepo.DanhSachCa_ThongTinDaKhaiBao(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update_DuyetGiaiTrinh(string[] vals)
        {
            try
            {
                List<UserWStats> lst = new List<UserWStats>();

                for (int i = 0; i < vals.Length; i++)
                {
                    var val = vals[i].ToString().Split('.');
                    lst.Add(new UserWStats()
                    {
                        UserEnrollNumber = int.Parse(val[0].ToString()),
                        Date = val[1].ToString(),
                        Col = val[2].ToString()
                    });
                }

                return _chamCongRepo.Update_DuyetGiaiTrinh(StaticParams.connectionStringWiseEyeWebOn, lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int[] DanhSachCa_CheckboxWithDefaultValueUser(int user)
        {
            try
            {
                return _chamCongRepo.DanhSachCa_CheckboxWithDefaultValueUser(StaticParams.connectionStringWiseEyeWebOn, user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region LÀM THÊM TÍNH TIỀN

        public IEnumerable<OTPayment> LamThemTT_DanhSach(OTPayment_Filter oFilter)
        {
            return _chamCongRepo.LamThemTT_DanhSach(StaticParams.connectionStringWiseEyeWebOn, oFilter);
        }
        public DataTable LamThemTT_DanhSachExcel(OTPayment_Filter oFilter)
        {
            return _chamCongRepo.LamThemTT_DanhSachExcel(StaticParams.connectionStringWiseEyeWebOn, oFilter);
        }
        public bool LamThemTT_Add(OTPayment obj)
        {
            try
            {
                return _chamCongRepo.LamThemTT_Add(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool LamThemTT_Approve(List<OTPayment> objs, string auth)
        {
            try
            {
                if (auth == "HCNS_Admin")
                {
                    return _chamCongRepo.LamThemTT_UpdateApprove(StaticParams.connectionStringWiseEyeWebOn, objs);
                }
                else
                {
                    return _chamCongRepo.LamThemTT_Approve(StaticParams.connectionStringWiseEyeWebOn, objs);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool LamThemTT_HuyDuyet(OTPayment obj)
        {
            try
            {
                return _chamCongRepo.LamThemTT_HuyDuyet(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool LamThemTT_Xoa(OTPayment obj)
        {
            try
            {
                return _chamCongRepo.LamThemTT_Xoa(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Dictionary<int, string> LamThemTT_Type()
        {
            try
            {
                return _chamCongRepo.LamThemTT_Type(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool OnCall_Send(GhepCa obj, string lydo, out string mess)
        {
            mess = "";
            var onCall_KT = _chamCongRepo.GhepCa_KT(StaticParams.connectionStringWiseEyeWebOn, obj);

            if (onCall_KT.Val == 0)
            {
                mess = "Chọn sai thứ tự quy định. Yêu cầu chọn lại";
                return false;
            }
            if (onCall_KT.Val == 1 && !string.IsNullOrEmpty(onCall_KT.Par))
            {
                mess = "Danh sách đã có giờ được ghép ca. Yêu cầu chọn lại";
                return false;
            }
            return _chamCongRepo.GhepCa_Add(StaticParams.connectionStringWiseEyeWebOn, obj, lydo);
        }
        public bool OnCall_Del(GhepCa obj, out string mess)
        {
            mess = "";

            if (string.IsNullOrEmpty(obj.TimeString))
            {
                mess = "Chọn sai ngày. Yêu cầu chọn lại";
                return false;
            }
            return _chamCongRepo.GhepCa_Del(StaticParams.connectionStringWiseEyeWebOn, obj);
        }
        public IEnumerable<CaKhongXacDinh> CaKhongXacDinh_DanhSach(CaKhongXacDinh_Filter obj)
        {
            try
            {
                return _chamCongRepo.CaKhongXacDinh_DanhSach(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CaKhongXacDinh_UpdateApprove(List<CaKhongXacDinh> obj)
        {
            try
            {
                return _chamCongRepo.CaKhongXacDinh_UpdateApprove(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool CaKhongXacDinh_Del(CaKhongXacDinh obj)
        {
            try
            {
                return _chamCongRepo.CaKhongXacDinh_Del(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<TongHopCong> TongHopCong_View(string fromDate, string toDate, int userid, int kp, int loainv)
        {
            try
            {
                var o = new TongHopCong_Filter()
                {
                    TuNgay = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"),
                    DenNgay = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"),
                    KhoaPhong = kp,
                    UserID = userid,
                    LoaiNV = loainv
                };

                return _chamCongRepo.TongHopCong_View(StaticParams.connectionStringWiseEyeWebOn, o);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool TongHopCong_CapNhatChiSo(int userid, string date, string column, string val, ref string errorMessage)
        {
            try
            {
                var dateF = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                return _chamCongRepo.TongHopCong_CapNhatChiSo(StaticParams.connectionStringWiseEyeWebOn, userid, dateF, column, val) > 0;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return false;
        }
        public DataTable TongHopCong_Excel(TongHopCong_Filter obj)
        {
            try
            {
                return _chamCongRepo.TongHopCong_Excel(StaticParams.connectionStringWiseEyeWebOn, obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion LÀM THÊM TÍNH TIỀN

        #region Khóa số liệu

        public int KhoaSoLieu_TrangThai()
        {
            try
            {
                return _chamCongRepo.KhoaSoLieu_TrangThai(StaticParams.connectionStringWiseEyeWebOn);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool KhoaSoLieu_CapNhat(int status)
        {
            try
            {
                return _chamCongRepo.KhoaSoLieu_CapNhat(StaticParams.connectionStringWiseEyeWebOn, status);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion Khóa số liệu

        public DataTable DS_KhaiBaoAn(int userid, string date)
        {
            try
            {
                return _chamCongRepo.DS_KhaiBaoAn(StaticParams.connectionStringWiseEyeWebOn, userid, date);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool XacNhanHuy(KhaiBaoAn obj, ref string errorMessage)
        {
            try
            {
                return _chamCongRepo.XacNhanHuy(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        public bool XacNhan(KhaiBaoAn obj, ref string errorMessage)
        {
            try
            {
                return _chamCongRepo.XacNhan(StaticParams.connectionStringWiseEyeWebOn, obj) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        public DiaDiemAn Get_NoiAn(int user)
        {
            return _chamCongRepo.Get_NoiAn(StaticParams.connectionStringWiseEyeWebOn, user);
        }
        public List<SelectListItem> DSKhoaPhong(int userenrollnumber)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = _chamCongRepo.DSKhoaPhong(StaticParams.connectionStringWiseEyeWebOn, userenrollnumber);

            if (userenrollnumber == -1)
            {
                items.Add(new SelectListItem() { Value = "-1", Text = "-- Tất cả --" });

                foreach (var item in depts)
                {
                    items.Add(new SelectListItem() { Value = item.STT.ToString(), Text = item.KhoaP });
                }
            }
            else
            {
                foreach (var item in depts)
                {
                    items.Add(new SelectListItem() { Value = item.STT.ToString(), Text = item.KhoaP });
                }
            }

            return items;
        }
        public List<SelectListItem> DSLoaiNhanVien()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var depts = _chamCongRepo.DSLoaiNhanVien(StaticParams.connectionStringWiseEyeWebOn);

            items.Add(new SelectListItem() { Value = "-1", Text = "-- Tất cả --" });

            foreach (var item in depts)
            {
                items.Add(new SelectListItem() { Value = item.STT.ToString(), Text = item.KhoaP });
            }

            return items;
        }
        public IEnumerable<HCNS_NhanVien> Table_VanTayThucTe(HCNS_NhanVien objsearch)
        {
            try
            {
                IEnumerable<HCNS_NhanVien> lst = _chamCongRepo.Table_VanTayThucTe(StaticParams.connectionStringWiseEyeWebOn, objsearch, "1");
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataTable Excel_VanTayThucTe(HCNS_NhanVien oSearch)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã NV", typeof(string));
            dataTable.Columns.Add("Họ và tên", typeof(string));
            dataTable.Columns.Add("Khoa/Phòng", typeof(string));
            dataTable.Columns.Add("Thời gian chấm vân tay", typeof(string));

            try
            {
                var data = _chamCongRepo.Table_VanTayThucTe(StaticParams.connectionStringWiseEyeWebOn, oSearch, "2");

                foreach (var item in data)
                {
                    dataTable.Rows.Add(
                        item.UserEnrollNumber,
                        item.UserFullName,
                        item.Ten_KhoaPhong,
                        item.TimeStr);
                }

                return dataTable;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public TinhPhep Get_Phep(string curDate, int user)
        {
            return _chamCongRepo.Get_Phep(StaticParams.connectionStringWiseEyeWebOn, curDate, user);
        }

        public bool TongHopCong_TongHopSoLieu(string ngaybd, string ngaykt, ref string error)
        {
            try
            {
                ngaybd = DateTime.ParseExact(ngaybd, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                ngaykt = DateTime.ParseExact(ngaykt, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");

                return _chamCongRepo.TongHopCong_TongHopSoLieu(StaticParams.connectionStringWiseEyeWebOn, ngaybd, ngaykt) > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return false;
        }
        public bool ChuyenKyLuong(string ngaybd, string ngaykt, ref string error)
        {
            try
            {
                ngaybd = DateTime.ParseExact(ngaybd, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                ngaykt = DateTime.ParseExact(ngaykt, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");

                return _chamCongRepo.ChuyenKyLuong(StaticParams.connectionStringWiseEyeWebOn, ngaybd, ngaykt) > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return false;
        }
        public IEnumerable<LamViecNgayLe> Table_LamViecNgayLe(LamViecNgayLe objsearch)
        {
            try
            {
                IEnumerable<LamViecNgayLe> lst = _chamCongRepo.Table_LamViecNgayLe(StaticParams.connectionStringWiseEyeWebOn, objsearch);
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Push_DeXuatLamViecNgayLe(string dataString, string lydo, string loaica, int type, string approve)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserEnrollNumber", typeof(string));
            dt.Columns.Add("HDATE", typeof(string));
            dt.Columns.Add("DS", typeof(string));
            try
            {
                var banghi = dataString.Split(',');
                for (int i = 0; i < banghi.Length; i++)
                {
                    var giatri = banghi[i].ToString().Split(':');
                    dt.Rows.Add(giatri[0].ToString(),
                        DateTime.ParseExact(giatri[1].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),
                        giatri[2].ToString()
                    );
                }
                var user = HttpContext.Current.User.Identity.Name;
                return _chamCongRepo.Push_DeXuatLamViecNgayle(StaticParams.connectionStringWiseEyeWebOn, dt, lydo, loaica, user, type, approve) == "0" ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public DataTable Excel_LamViecNgayLe(LamViecNgayLe oSearch)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("STT", typeof(string));
            dataTable.Columns.Add("Mã NV", typeof(string));
            dataTable.Columns.Add("Họ và tên", typeof(string));
            dataTable.Columns.Add("Ngày", typeof(string));
            dataTable.Columns.Add("Ca làm việc", typeof(string));
            dataTable.Columns.Add("Lý do", typeof(string));
            dataTable.Columns.Add("Loại", typeof(string));

            var counter = 0;

            try
            {
                var data = _chamCongRepo.Table_LamViecNgayLe(StaticParams.connectionStringWiseEyeWebOn, oSearch);

                foreach (var item in data)
                {
                    counter++;
                    dataTable.Rows.Add(
                        item.STT,
                        item.MaNV,
                        item.HoTen,
                        item.Ngay,
                        item.CaLV,
                        item.LyDo,
                        item.TrangThaiDL
                        );
                }

                return dataTable;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }
}