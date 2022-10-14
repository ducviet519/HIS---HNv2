using System;
using System.App.Entities.Common;
using System.App.Entities.HCNS;
using System.App.Security;
using System.App.Services.Interfaces;
using System.Web.Mvc;

namespace HCNS.Controllers
{
    public class BCCCController : Controller
    {
        private readonly NhanVien_Interface _nhanvienService;
        private readonly IBCCC _bcService;

        public BCCCController(NhanVien_Interface nv, IBCCC bc)
        {
            _nhanvienService = nv;
            _bcService = bc;
        }

        #region 1. Bằng cấp

        [CustomAuthorize(StaticParams.HCNS_BangCap, StaticParams.IT)]
        public ActionResult BangCap()
        {
            ViewBag.LoaiBangCap = _bcService.DS_LoaiBangCap();

            return View();
        }

        public PartialViewResult TableData_BangCap(string text = "", string loai = "")
        {
            var oSearch = new BangCap()
            {
                UserFullName = text,
                SType = loai
            };
            return PartialView("_TableData_BangCap", _bcService.DS_BangCap(oSearch));
        }

        public PartialViewResult Add_BangCap()
        {
            ViewBag.LoaiBangCap = _bcService.DS_LoaiBangCap();

            return PartialView("_Add_BangCap");
        }

        public PartialViewResult Edit_BangCap(int id)
        {
            var info = _bcService.ThongTin_BangCap(id);
            ViewBag.LoaiBangCap = _bcService.DS_LoaiBangCap();
            return PartialView("_Edit_BangCap", info);
        }

        public ActionResult Remove_BangCap(int id)
        {
            var val = _bcService.Delete_BangCap(new System.App.Entities.HCNS.BangCap() { ID = id });

            return Json(new { result = val }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_BangCap(BangCap obj)
        {
            if (obj.ID == 0)
            {
                var val = _bcService.Add_BangCap(obj);
                return Json(new { result = val }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var val = _bcService.Update_BangCap(obj);
                return Json(new { result = val }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion 1. Bằng cấp

        #region 2. Chứng chỉ

        [CustomAuthorize(StaticParams.HCNS_ChungChi,StaticParams.IT)]
        public ActionResult ChungChi()
        {
            ViewBag.LoaiBangCap = _bcService.DS_LoaiBangCap();
            ViewBag.DS_KhoaPhong = _nhanvienService.DanhSachKhoaPhongHC();
            return View();
        }

        public PartialViewResult TableData_ChungChi(string text = "", string kp = "")
        {
            var oSearch = new ChungChi()
            {
                UserFullName = text,
                KhoaPhong = String.IsNullOrEmpty(kp) ? -1 : int.Parse(kp)
            };
            return PartialView("_TableData_ChungChi", _bcService.DS_ChungChi(oSearch));
        }

        public PartialViewResult Add_ChungChi()
        {
            ViewBag.LoaiBangCap = _bcService.DS_LoaiBangCap();

            return PartialView("_Add_ChungChi");
        }

        public PartialViewResult Edit_ChungChi(int id)
        {
            var info = _bcService.ThongTin_ChungChi(id);
            return PartialView("_Edit_ChungChi", info);
        }

        public ActionResult Remove_ChungChi(int id)
        {
            var val = _bcService.Delete_ChungChi(new System.App.Entities.HCNS.ChungChi() { ID = id });

            return Json(new { result = val }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_ChungChi(ChungChi obj)
        {
            if (obj.ID == 0)
            {
                var val = _bcService.Add_ChungChi(obj);
                return Json(new { result = val }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var val = _bcService.Update_ChungChi(obj);
                return Json(new { result = val }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion 2. Chứng chỉ

        #region 3. Chứng chỉ hành nghề

        [CustomAuthorize(StaticParams.HCNS_ChungChiHanhNghe, StaticParams.IT)]
        public ActionResult ChungChiHanhNghe()
        {
            ViewBag.DS_KhoaPhong = _nhanvienService.DanhSachKhoaPhongHC();
            return View();
        }

        public PartialViewResult TableData_ChungChiHanhNghe(string text = "", string kp = "")
        {
            var oSearch = new ChungChiHanhNghe()
            {
                UserFullName = text,
                KhoaPhong = String.IsNullOrEmpty(kp) ? -1 : int.Parse(kp)
            };
            return PartialView("_TableData_ChungChiHanhNghe", _bcService.DS_ChungChiHanhNghe(oSearch));
        }

        public PartialViewResult Edit_ChungChiHanhNghe(int id)
        {
            var info = _bcService.ThongTin_ChungChiHanhNghe(id);
            return PartialView("_Edit_ChungChiHanhNghe", info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Push_ChungChiHanhNghe(ChungChiHanhNghe obj)
        {
            var val = _bcService.Update_ChungChiHanhNghe(obj);
            return Json(new { result = val }, JsonRequestBehavior.AllowGet);
        }

        #endregion 3. Chứng chỉ hành nghề
    }
}