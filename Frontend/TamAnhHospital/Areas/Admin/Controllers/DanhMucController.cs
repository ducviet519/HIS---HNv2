using System.App.Entities;
using System.App.Entities.Common;
using System.App.Security;
using System.App.Services;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly IDanhMuc_Service danhmucService;

        public DanhMucController(IDanhMuc_Service danhmuc)
        {
            danhmucService = danhmuc;
        }
        // GET: Admin/DanhMuc
        public ActionResult LoaiVang()
        {
            return View();
        }
        [CustomAuthorize(StaticParams.DM_CaLamViec)]
        [HttpGet]
        public JsonResult DS_LoaiVang_Search(DanhMuc_LoaiVang_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_LoaiVang_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_LoaiVang()
        {
            return PartialView("_Create_LoaiVang");
        }
        [HttpPost]
        public JsonResult AddNew_LoaiVang(DanhMuc_LoaiVang_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_LoaiVang(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_LoaiVang(DanhMuc_LoaiVang_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_LoaiVang(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_LoaiVang(DanhMuc_LoaiVang_Search obj)
        {
            var info = danhmucService.Get_LoaiVang(obj);
            return PartialView("_Update_LoaiVang", info);
        }
        [HttpPost]
        public JsonResult Edit_LoaiVang(DanhMuc_LoaiVang_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_LoaiVang(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinh)]
        public ActionResult NganHang()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_NganHang_Search(DanhMuc_NganHang_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_NganHang_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_NganHang(DanhMuc_NganHang_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_NganHang(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_NganHang()
        {
            return PartialView("_Create_NganHang");
        }
        [HttpPost]
        public JsonResult AddNew_NganHang(DanhMuc_NganHang_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_NganHang(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinhBV)]
        public ActionResult HopDong()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_HopDong_Search(DanhMuc_HopDong_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_HopDong_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_HopDong(DanhMuc_HopDong_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_HopDong(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_HopDong()
        {
            return PartialView("_Create_HopDong");
        }
        [HttpPost]
        public JsonResult AddNew_HopDong(DanhMuc_HopDong_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_HopDong(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_HopDong(DanhMuc_HopDong_Search obj)
        {
            var info = danhmucService.Get_HopDong(obj);
            ViewBag.ID = info.ID;
            return PartialView("_Update_HopDong", info);
        }
        [HttpPost]
        public JsonResult Edit_HopDong(DanhMuc_HopDong_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_HopDong(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinhBV)]
        public ActionResult KhoaPhong()
        {
            return View();
        }
        public PartialViewResult Create_KhoaPhong()
        {
            ViewBag.NHOM = danhmucService.DS_Ca();
            return PartialView("_Create_KhoaPhong");
        }
        [HttpGet]
        public JsonResult DS_KhoaPhong_Search(DanhMuc_KhoaPhong_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_KhoaPhong_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_KhoaPhong(DanhMuc_KhoaPhong_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_KhoaPhong(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddNew_KhoaPhong(DanhMuc_KhoaPhong_Search obj)
        {            
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_KhoaPhong(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_KhoaPhong(DanhMuc_KhoaPhong_Search obj)
        {
            ViewBag.NHOM = danhmucService.DS_Ca();
            var info = danhmucService.Get_KhoaPhong(obj);
            return PartialView("_Update_KhoaPhong", info);
        }
        [HttpPost]
        public JsonResult Edit_KhoaPhong(DanhMuc_KhoaPhong_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_KhoaPhong(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinh)]
        public ActionResult ThanhPho()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DS_ThanhPho_Search(DanhMuc_ThanhPho_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_ThanhPho_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_ThanhPho()
        {
            return PartialView("_Create_ThanhPho");
        }
        [HttpPost]
        public JsonResult AddNew_ThanhPho(DanhMuc_ThanhPho_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_ThanhPho(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_ThanhPho(DanhMuc_ThanhPho_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_ThanhPho(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_ThanhPho(DanhMuc_ThanhPho_Search obj)
        {
            var info = danhmucService.Get_ThanhPho(obj);
            return PartialView("_Update_ThanhPho", info);
        }
        [HttpPost]
        public JsonResult Edit_ThanhPho(DanhMuc_ThanhPho_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_ThanhPho(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinh)]
        public ActionResult QuanHuyen()
        {
            ViewBag.THANHPHO = danhmucService.DS_ThanhPho();
            return View();
        }

        [HttpGet]
        public JsonResult DS_QuanHuyen_Search(DanhMuc_QuanHuyen_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_QuanHuyen_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_QuanHuyen()
        {
            ViewBag.THANHPHO = danhmucService.DS_ThanhPho();
            return PartialView("_Create_QuanHuyen");
        }
        [HttpPost]
        public JsonResult AddNew_QuanHuyen(DanhMuc_QuanHuyen_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_QuanHuyen(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_QuanHuyen(DanhMuc_QuanHuyen_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_QuanHuyen(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_QuanHuyen(DanhMuc_QuanHuyen_Search obj)
        {
            var info = danhmucService.Get_QuanHuyen(obj);
            ViewBag.THANHPHO = danhmucService.DS_ThanhPho(info.City);
            return PartialView("_Update_QuanHuyen", info);
        }
        [HttpPost]
        public JsonResult Edit_QuanHuyen(DanhMuc_QuanHuyen_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_QuanHuyen(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinh)]
        public ActionResult BenhVien()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_BenhVien_Search(DanhMuc_BenhVien_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_BenhVien_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_BenhVien(DanhMuc_BenhVien_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_BenhVien(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_BenhVien()
        {
            return PartialView("_Create_BenhVien");
        }
        [HttpPost]
        public JsonResult AddNew_BenhVien(DanhMuc_BenhVien_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_BenhVien(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinh)]
        public ActionResult QuocGia()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_QuocGia_Search(DanhMuc_QuocGia_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_QuocGia_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_QuocGia(DanhMuc_QuocGia_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_QuocGia(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_QuocGia()
        {
            return PartialView("_Create_QuocGia");
        }
        [HttpPost]
        public JsonResult AddNew_QuocGia(DanhMuc_QuocGia_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_QuocGia(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinh)]
        public ActionResult NoiCapCMT()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_NoiCapCMT_Search(DanhMuc_NoiCapCMT_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_NoiCapCMT_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_NoiCapCMT(DanhMuc_NoiCapCMT_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_NoiCapCMT(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_NoiCapCMT()
        {
            return PartialView("_Create_NoiCapCMT");
        }
        [HttpPost]
        public JsonResult AddNew_NoiCapCMT(DanhMuc_NoiCapCMT_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_NoiCapCMT(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinh)]
        public ActionResult DanToc()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_DanToc_Search(DanhMuc_DanToc_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_DanToc_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_DanToc(DanhMuc_DanToc_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_DanToc(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_DanToc()
        {
            return PartialView("_Create_DanToc");
        }
        [HttpPost]
        public JsonResult AddNew_DanToc(DanhMuc_DanToc_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_DanToc(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_DanToc(DanhMuc_DanToc_Search obj)
        {
            var info = danhmucService.Get_DanToc(obj);
            ViewBag.ID = info.ID;
            return PartialView("_Update_DanToc", info);
        }
        [HttpPost]
        public JsonResult Edit_DanToc(DanhMuc_DanToc_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_DanToc(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinh)]
        public ActionResult TonGiao()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_TonGiao_Search(DanhMuc_TonGiao_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_TonGiao_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_TonGiao(DanhMuc_TonGiao_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_TonGiao(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_TonGiao()
        {
            return PartialView("_Create_TonGiao");
        }
        [HttpPost]
        public JsonResult AddNew_TonGiao(DanhMuc_TonGiao_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_TonGiao(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinhBV)]
        public ActionResult PhongHop()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DS_PhongHop_Search(DanhMuc_PhongHop_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_PhongHop_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_PhongHop()
        {
            return PartialView("_Create_PhongHop");
        }
        [HttpPost]
        public JsonResult AddNew_PhongHop(DanhMuc_PhongHop_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_PhongHop(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_PhongHop(DanhMuc_PhongHop_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_PhongHop(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_PhongHop(DanhMuc_PhongHop_Search obj)
        {
            var info = danhmucService.Get_PhongHop(obj);
            return PartialView("_Update_PhongHop", info);
        }
        [HttpPost]
        public JsonResult Edit_PhongHop(DanhMuc_PhongHop_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_PhongHop(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UnDelete_PhongHop(DanhMuc_PhongHop_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.UnDelete_PhongHop(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinhBV)]
        public ActionResult KhoaPhongCC()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DS_KhoaPhongCC_Search(DanhMuc_KhoaPhongCC_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_KhoaPhongCC_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_KhoaPhongCC()
        {
            ViewBag.QUANHE = danhmucService.DS_QuanHe();
            return PartialView("_Create_KhoaPhongCC");
        }
        [HttpPost]
        public JsonResult AddNew_KhoaPhongCC(DanhMuc_KhoaPhongCC_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_KhoaPhongCC(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_KhoaPhongCC(DanhMuc_KhoaPhongCC_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_KhoaPhongCC(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_KhoaPhongCC(DanhMuc_KhoaPhongCC_Search obj)
        {
            var info = danhmucService.Get_KhoaPhongCC(obj);
            ViewBag.QUANHE = danhmucService.DS_QuanHe(info.RelationID);
            return PartialView("_Update_KhoaPhongCC", info);
        }
        [HttpPost]
        public JsonResult Edit_KhoaPhongCC(DanhMuc_KhoaPhongCC_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_KhoaPhongCC(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinh)]
        public ActionResult PhuongXa()
        {
            ViewBag.THANHPHO = danhmucService.DS_ThanhPho();
            ViewBag.QUANHUYEN = danhmucService.DS_QuanHuyen();
            return View();
        }

        [HttpGet]
        public JsonResult DS_PhuongXa_Search(DanhMuc_PhuongXa_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_PhuongXa_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_PhuongXa()
        {
            ViewBag.THANHPHO = danhmucService.DS_ThanhPho();
            ViewBag.QUANHUYEN = danhmucService.DS_QuanHuyen();
            return PartialView("_Create_PhuongXa");
        }
        [HttpPost]
        public JsonResult AddNew_PhuongXa(DanhMuc_PhuongXa_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_PhuongXa(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_PhuongXa(DanhMuc_PhuongXa_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_PhuongXa(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_PhuongXa(DanhMuc_PhuongXa_Search obj)
        {
            var info = danhmucService.Get_PhuongXa(obj);
            ViewBag.THANHPHO = danhmucService.DS_ThanhPho(info.CityID);
            ViewBag.QUANHUYEN = danhmucService.DS_QuanHuyen(info.Dis);
            return PartialView("_Update_PhuongXa", info);
        }
        [HttpPost]
        public JsonResult Edit_PhuongXa(DanhMuc_PhuongXa_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_PhuongXa(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinhBV)]
        public ActionResult Tang()
        {
            ViewBag.TANG = danhmucService.DS_Tang();
            return View();
        }
        [HttpGet]
        public JsonResult DS_Tang_Search(DanhMuc_Tang_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_Tang_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_Tang(DanhMuc_Tang_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_Tang(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_Tang()
        {
            return PartialView("_Create_Tang");
        }
        [HttpPost]
        public JsonResult AddNew_Tang(DanhMuc_Tang_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_Tang(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_CaLamViec)]
        public ActionResult CaLamViec()
        {
            ViewBag.Auth = danhmucService.GetPermission();
            return View();
        }

        [HttpGet]
        public JsonResult DS_CaLamViec_Search(DanhMuc_CaLamViec_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_CaLamViec_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_CaLamViec()
        {
            ViewBag.LoaiCa = danhmucService.DS_LoaiCa();
            return PartialView("_Create_CaLamViec");
        }
        [HttpPost]
        public JsonResult AddNew_CaLamViec(DanhMuc_CaLamViec_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_CaLamViec(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_CaLamViec(DanhMuc_CaLamViec_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_CaLamViec(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_CaLamViec(DanhMuc_CaLamViec_Search obj)
        {
            var info = danhmucService.Get_CaLamViec(obj);
            ViewBag.LoaiCa = danhmucService.DS_LoaiCa(info.WKOTLevel);
            return PartialView("_Update_CaLamViec", info);
        }
        [HttpPost]
        public JsonResult Edit_CaLamViec(DanhMuc_CaLamViec_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_CaLamViec(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_CaLamViec)]
        public ActionResult LichLamViec()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DS_LichLamViec_Search(DanhMuc_LichLamViec_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_LichLamViec_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_LichLamViec()
        {
            ViewBag.INOUTID = danhmucService.DS_InOutID();
            return PartialView("_Create_LichLamViec");
        }
        [HttpPost]
        public JsonResult AddNew_LichLamViec(DanhMuc_LichLamViec_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_LichLamViec(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_LichLamViec(DanhMuc_LichLamViec_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_LichLamViec(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_LichLamViec(DanhMuc_LichLamViec_Search obj)
        {
            var info = danhmucService.Get_LichLamViec(obj);
            ViewBag.INOUTID = danhmucService.DS_InOutID(info.InOutID);
            return PartialView("_Update_LichLamViec", info);
        }
        [HttpPost]
        public JsonResult Edit_LichLamViec(DanhMuc_LichLamViec_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_LichLamViec(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_CaLamViec)]
        public ActionResult LichTrinh()
        {
            ViewBag.SCHEDULES = danhmucService.DS_Schedule();
            ViewBag.SHIFTS = danhmucService.DS_Shift();
            return View();
        }

        [HttpGet]
        public JsonResult DS_LichTrinh_Search(DanhMuc_LichTrinh_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_LichTrinh_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_LichTrinh()
        {
            ViewBag.SCHEDULES = danhmucService.DS_Schedule();
            ViewBag.SHIFTS = danhmucService.DS_Shift();
            return PartialView("_Create_LichTrinh");
        }
        [HttpPost]
        public JsonResult AddNew_LichTrinh(DanhMuc_LichTrinh_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_LichTrinh(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_LichTrinh(DanhMuc_LichTrinh_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_LichTrinh(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_LichTrinh(DanhMuc_LichTrinh_Search obj)
        {
            var info = danhmucService.Get_LichTrinh(obj);
            ViewBag.SCHEDULE = danhmucService.DS_Schedule(info.SchID);
            ViewBag.SHIFT = danhmucService.DS_Shift(info.ShiftID);
            return PartialView("_Update_LichTrinh", info);
        }
        [HttpPost]
        public JsonResult Edit_LichTrinh(DanhMuc_LichTrinh_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_LichTrinh(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinhBV)]
        public ActionResult NoiAn()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_NoiAn_Search(DanhMuc_NoiAn_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_NoiAn_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_NoiAn(DanhMuc_NoiAn_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_NoiAn(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_NoiAn()
        {
            return PartialView("_Create_NoiAn");
        }
        [HttpPost]
        public JsonResult AddNew_NoiAn(DanhMuc_NoiAn_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_NoiAn(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinhBV)]
        public ActionResult NgayNghi()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_NgayNghi_Search(DanhMuc_NgayNghi_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_NgayNghi_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_NgayNghi(DanhMuc_NgayNghi_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_NgayNghi(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_NgayNghi()
        {
            return PartialView("_Create_NgayNghi");
        }
        [HttpPost]
        public JsonResult AddNew_NgayNghi(DanhMuc_NgayNghi_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_NgayNghi(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_CaLamViec)]
        public ActionResult LoaiOT()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_LoaiOT_Search(DanhMuc_LoaiOT_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_LoaiOT_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_LoaiOT(DanhMuc_LoaiOT_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_LoaiOT(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_LoaiOT()
        {
            return PartialView("_Create_LoaiOT");
        }
        [HttpPost]
        public JsonResult AddNew_LoaiOT(DanhMuc_LoaiOT_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_LoaiOT(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinhBV)]
        public ActionResult KyNang()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_KyNang_Search(DanhMuc_KyNang_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_KyNang_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_KyNang(DanhMuc_KyNang_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_KyNang(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_KyNang()
        {
            return PartialView("_Create_KyNang");
        }
        [HttpPost]
        public JsonResult AddNew_KyNang(DanhMuc_KyNang_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_KyNang(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_CaLamViec)]
        public ActionResult LoaiYeuCauSuaCong()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_LoaiYeuCauSuaCong_Search(DanhMuc_LoaiYeuCauSuaCong_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_LoaiYeuCauSuaCong_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_LoaiYeuCauSuaCong(DanhMuc_LoaiYeuCauSuaCong_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_LoaiYeuCauSuaCong(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_LoaiYeuCauSuaCong()
        {
            return PartialView("_Create_LoaiYeuCauSuaCong");
        }
        [HttpPost]
        public JsonResult AddNew_LoaiYeuCauSuaCong(DanhMuc_LoaiYeuCauSuaCong_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_LoaiYeuCauSuaCong(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinhBV)]
        public ActionResult DoiTuongCC()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DS_DoiTuongCC_Search(DanhMuc_DoiTuongCC_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_DoiTuongCC_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_DoiTuongCC()
        {
            return PartialView("_Create_DoiTuongCC");
        }
        [HttpPost]
        public JsonResult AddNew_DoiTuongCC(DanhMuc_DoiTuongCC_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_DoiTuongCC(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_DoiTuongCC(DanhMuc_DoiTuongCC_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_DoiTuongCC(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_DoiTuongCC(DanhMuc_DoiTuongCC_Search obj)
        {
            var info = danhmucService.Get_DoiTuongCC(obj);
            return PartialView("_Update_DoiTuongCC", info);
        }
        [HttpPost]
        public JsonResult Edit_DoiTuongCC(DanhMuc_DoiTuongCC_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_DoiTuongCC(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_CaLamViec)]
        public ActionResult LoaiLamThem()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DS_LoaiLamThem_Search(DanhMuc_LoaiLamThem_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_LoaiLamThem_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_LoaiLamThem()
        {
            return PartialView("_Create_LoaiLamThem");
        }
        [HttpPost]
        public JsonResult AddNew_LoaiLamThem(DanhMuc_LoaiLamThem_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_LoaiLamThem(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_LoaiLamThem(DanhMuc_LoaiLamThem_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_LoaiLamThem(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_LoaiLamThem(DanhMuc_LoaiLamThem_Search obj)
        {
            var info = danhmucService.Get_LoaiLamThem(obj);
            return PartialView("_Update_LoaiLamThem", info);
        }
        [HttpPost]
        public JsonResult Edit_LoaiLamThem(DanhMuc_LoaiLamThem_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_LoaiLamThem(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinhBV)]
        public ActionResult LoaiNhanVien()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DS_LoaiNhanVien_Search(DanhMuc_LoaiNhanVien_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_LoaiNhanVien_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_LoaiNhanVien()
        {
            return PartialView("_Create_LoaiNhanVien");
        }
        [HttpPost]
        public JsonResult AddNew_LoaiNhanVien(DanhMuc_LoaiNhanVien_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_LoaiNhanVien(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_LoaiNhanVien(DanhMuc_LoaiNhanVien_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_LoaiNhanVien(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_LoaiNhanVien(DanhMuc_LoaiNhanVien_Search obj)
        {
            var info = danhmucService.Get_LoaiNhanVien(obj);
            return PartialView("_Update_LoaiNhanVien", info);
        }
        [HttpPost]
        public JsonResult Edit_LoaiNhanVien(DanhMuc_LoaiNhanVien_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_LoaiNhanVien(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_CaLamViec)]
        public ActionResult LoaiLoiCC()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DS_LoaiLoiCC_Search(DanhMuc_LoaiLoiCC_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_LoaiLoiCC_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_LoaiLoiCC()
        {
            return PartialView("_Create_LoaiLoiCC");
        }
        [HttpPost]
        public JsonResult AddNew_LoaiLoiCC(DanhMuc_LoaiLoiCC_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_LoaiLoiCC(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_LoaiLoiCC(DanhMuc_LoaiLoiCC_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_LoaiLoiCC(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_LoaiLoiCC(DanhMuc_LoaiLoiCC_Search obj)
        {
            var info = danhmucService.Get_LoaiLoiCC(obj);
            return PartialView("_Update_LoaiLoiCC", info);
        }
        [HttpPost]
        public JsonResult Edit_LoaiLoiCC(DanhMuc_LoaiLoiCC_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_LoaiLoiCC(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinh)]
        public ActionResult BangCap()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_BangCap_Search(DanhMuc_BangCap_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_BangCap_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_BangCap(DanhMuc_BangCap_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_BangCap(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_BangCap()
        {
            return PartialView("_Create_BangCap");
        }
        [HttpPost]
        public JsonResult AddNew_BangCap(DanhMuc_BangCap_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_BangCap(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinhBV)]
        public ActionResult ThietBiPhongHop()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_ThietBiPhongHop_Search(DanhMuc_ThietBiPhongHop_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_ThietBiPhongHop_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_ThietBiPhongHop(DanhMuc_ThietBiPhongHop_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_ThietBiPhongHop(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_ThietBiPhongHop()
        {
            return PartialView("_Create_ThietBiPhongHop");
        }
        [HttpPost]
        public JsonResult AddNew_ThietBiPhongHop(DanhMuc_ThietBiPhongHop_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_ThietBiPhongHop(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinh)]
        public ActionResult TTHonNhan()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_TTHonNhan_Search(DanhMuc_TTHonNhan_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_TTHonNhan_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_TTHonNhan(DanhMuc_TTHonNhan_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_TTHonNhan(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_TTHonNhan()
        {
            return PartialView("_Create_TTHonNhan");
        }
        [HttpPost]
        public JsonResult AddNew_TTHonNhan(DanhMuc_TTHonNhan_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_TTHonNhan(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_ThietBi)]
        public ActionResult ThietBi()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DS_ThietBi_Search(DanhMuc_ThietBi_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_ThietBi_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_ThietBi()
        {
            return PartialView("_Create_ThietBi");
        }
        [HttpPost]
        public JsonResult AddNew_ThietBi(DanhMuc_ThietBi_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_ThietBi(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_ThietBi(DanhMuc_ThietBi_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_ThietBi(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_ThietBi(DanhMuc_ThietBi_Search obj)
        {
            var info = danhmucService.Get_ThietBi(obj);
            return PartialView("_Update_ThietBi", info);
        }
        [HttpPost]
        public JsonResult Edit_ThietBi(DanhMuc_ThietBi_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_ThietBi(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_PhanQuyen)]
        public ActionResult DS_Quyen()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_Quyen_Search(DanhMuc_Quyen_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_Quyen_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_Quyen()
        {
            return PartialView("_Create_Quyen");
        }
        [HttpPost]
        public JsonResult AddNew_Quyen(DanhMuc_Quyen_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_Quyen(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_Quyen(DanhMuc_Quyen_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_Quyen(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_Quyen(DanhMuc_Quyen_Search obj)
        {
            var info = danhmucService.Get_Quyen(obj);
            return PartialView("_Update_Quyen", info);
        }
        [HttpPost]
        public JsonResult Edit_Quyen(DanhMuc_Quyen_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_Quyen(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_PhanQuyen)]
        public ActionResult DS_Nhom()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_Nhom_Search(DanhMuc_Nhom_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_Nhom_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_Nhom()
        {
            ViewBag.QUYEN = danhmucService.DS_Quyen();
            return PartialView("_Create_Nhom");
        }
        [HttpPost]
        public JsonResult AddNew_Nhom(DanhMuc_Nhom_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_Nhom(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_Nhom(DanhMuc_Nhom_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_Nhom(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_Nhom(DanhMuc_Nhom_Search obj)
        {
            ViewBag.QUYEN = danhmucService.DS_Quyen();
            var info = danhmucService.Get_Nhom(obj);
            return PartialView("_Update_Nhom", info);
        }
        [HttpPost]
        public JsonResult Edit_Nhom(DanhMuc_Nhom_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_Nhom(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_PhanQuyen)]
        public ActionResult DS_Quyen_NguoiDung()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_Quyen_NguoiDung_Search(DanhMuc_Quyen_NguoiDung_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_Quyen_NguoiDung_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_Quyen_NguoiDung()
        {
            ViewBag.NHOM = danhmucService.DS_Nhom();
            return PartialView("_Create_Quyen_NguoiDung");
        }
        [HttpPost]
        public JsonResult AddNew_Quyen_NguoiDung(DanhMuc_Quyen_NguoiDung_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_Quyen_NguoiDung(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_Quyen_NguoiDung(DanhMuc_Quyen_NguoiDung_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_Quyen_NguoiDung(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_Quyen_NguoiDung(DanhMuc_Quyen_NguoiDung_Search obj)
        {
            ViewBag.NHOM = danhmucService.DS_Nhom();
            var info = danhmucService.Get_Quyen_NguoiDung(obj);
            return PartialView("_Update_Quyen_NguoiDung", info);
        }
        [HttpPost]
        public JsonResult Edit_Quyen_NguoiDung(DanhMuc_Quyen_NguoiDung_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_Quyen_NguoiDung(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_PhanQuyen)]
        public ActionResult DS_Quyen_KhoaPhong()
        {
            ViewBag.DEPTS = danhmucService.DS_Depts();
            return View();
        }
        [HttpGet]
        public JsonResult DS_Quyen_KhoaPhong_Search(DanhMuc_Quyen_KhoaPhong_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_Quyen_KhoaPhong_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_Quyen_KhoaPhong()
        {
            ViewBag.DEPTS = danhmucService.DS_Depts();
            ViewBag.NHOM = danhmucService.DS_Nhom();
            return PartialView("_Create_Quyen_KhoaPhong");
        }
        [HttpPost]
        public JsonResult AddNew_Quyen_KhoaPhong(DanhMuc_Quyen_KhoaPhong_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_Quyen_KhoaPhong(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_Quyen_KhoaPhong(DanhMuc_Quyen_KhoaPhong_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_Quyen_KhoaPhong(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_Quyen_KhoaPhong(DanhMuc_Quyen_KhoaPhong_Search obj)
        {            
            ViewBag.NHOM = danhmucService.DS_Nhom();
            var info = danhmucService.Get_Quyen_KhoaPhong(obj);
            ViewBag.DEPTS = danhmucService.DS_Depts(info.Dept);
            return PartialView("_Update_Quyen_KhoaPhong", info);
        }
        [HttpPost]
        public JsonResult Edit_Quyen_KhoaPhong(DanhMuc_Quyen_KhoaPhong_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_Quyen_KhoaPhong(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        #region Phân quyền chấm công
        [CustomAuthorize(StaticParams.DM_PhanQuyen)]
        public ActionResult PhanQuyenChamCong()
        {
            ViewBag.Departments = danhmucService.DSKhoaPhong();
            return View();
        }

        [HttpGet]
        public JsonResult QuyenChamCong_table(QuyenChamCong objSearch)
        {
            return Json(new { data = danhmucService.QuyenChamCong_table(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult XoaQuyen(QuyenChamCong obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.XoaQuyen(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ThemQuyen(QuyenChamCong obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.ThemQuyen(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [CustomAuthorize(StaticParams.DM_PhanQuyen)]
        public ActionResult DS_Config()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DS_Config_Search(DanhMuc_Config_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_Config_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_Config()
        {
            return PartialView("_Create_Config");
        }
        [HttpPost]
        public JsonResult AddNew_Config(DanhMuc_Config_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_Config(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_Config(DanhMuc_Config_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_Config(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_Config(DanhMuc_Config_Search obj)
        {
            var info = danhmucService.Get_Config(obj);
            return PartialView("_Update_Config", info);
        }
        [HttpPost]
        public JsonResult Edit_Config(DanhMuc_Config_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_Config(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(StaticParams.DM_HanhChinh)]
        public ActionResult ViTriTuyenDung()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DSViTriTuyenDung_Search(DanhMuc_ViTriTuyenDung_Search objSearch)
        {
            return Json(new { data = danhmucService.DS_ViTriTuyenDung_Search(objSearch) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Delete_ViTriTuyenDung(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UnDelete_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.UnDelete_ViTriTuyenDung(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Create_ViTriTuyenDung()
        {
            return PartialView("_Create_ViTriTuyenDung");
        }
        [HttpPost]
        public JsonResult AddNew_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.AddNew_ViTriTuyenDung(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Update_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj)
        {
            var info = danhmucService.Get_ViTriTuyenDung(obj);
            return PartialView("_Update_ViTriTuyenDung", info);
        }
        [HttpPost]
        public JsonResult Edit_ViTriTuyenDung(DanhMuc_ViTriTuyenDung_Search obj)
        {
            string _errorMessage = "";
            return Json(new { result = danhmucService.Edit_ViTriTuyenDung(obj, ref _errorMessage), message = _errorMessage }, JsonRequestBehavior.AllowGet);
        }
    }
}