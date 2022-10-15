using System;
using System.App.Entities.HCNS;
using System.App.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HCNS.Controllers
{
    public class DaoTaoChungChiController : Controller
    {
        private readonly IDaoTaoChungChi_Services _daoTaoChungChiServices;

        public DaoTaoChungChiController(IDaoTaoChungChi_Services daoTaoChungChiServices)
        {
            _daoTaoChungChiServices = daoTaoChungChiServices;
        }

        // GET: HCNS/DaoTaoChungChi
        public ActionResult Index(SearchDaoTao search = null)
        {
            DaoTaoChungChiVM model = new DaoTaoChungChiVM();
            var data = new List<DaoTaoChungChi>();
            model.DaoTaoChungChiLists = data;
            return View(model);
        }
        //public async Task<ActionResult> Index(SearchDaoTao search = null)
        //{
        //    DaoTaoChungChiVM model = new DaoTaoChungChiVM();
        //    model.DaoTaoChungChiLists = await _daoTaoChungChi.SearchDaoTaoAsync(search);
        //    return View(model);
        //}

        [HttpPost]
        public async Task<JsonResult> GetNhanVienInfo(string manv)
        {
            HCNS_NhanVien data = new HCNS_NhanVien();
            data = await _daoTaoChungChiServices.TimThongTinNhanVienAsync(new HCNS_NhanVien() { UserFullCode = manv });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
       public PartialViewResult AddDaoTao()
        {
            return PartialView("_AddDaoTao");
        }

    }
}