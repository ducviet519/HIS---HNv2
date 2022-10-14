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
        //private readonly DaoTaoChungChi_Interface _daoTaoChungChi;

        //public DaoTaoChungChiController(DaoTaoChungChi_Interface daoTaoChungChi)
        //{
        //    _daoTaoChungChi = daoTaoChungChi;
        //}
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

       public ActionResult AddDaoTao()
        {
            return PartialView("_AddDaoTao");
        }

    }
}