using auction.Dal;
using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Controllers
{
    public class ExpensesController : Controller
    {
        Expenses_DAL _d = new Expenses_DAL();
        Common_DAL _c = new Common_DAL();
        CommonData cd = new CommonData();
        [jAuth(MenuId = 201)]
        public ActionResult ManPower()
        {
            DropDownFor_ManPower();
            return View();
        }
        [jAuth(MenuId = 201)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManPower(T_MCLT _mclt)
        {
            DropDownFor_ManPower();
            _mclt.MCLT_MCTP = "1111"; //1111 is Man Power;
            int SEQN = cd.Table_Sequence("mclt_seqn", "auction.t_mclt");
            string Mclt_Text = cd.Mclt_Text(SEQN);
            _mclt.MCLT_TEXT = Mclt_Text;
            _mclt.MCLT_SEQN = SEQN;
            if (ModelState.IsValid)
            {
                _mclt.MCLT_YEAR = DateTime.Now.Date;
                bool result = _d.SaveExpenses(_mclt);
                if (result)
                {
                    TempData["MSG"] = "Swal.fire('success','Man power saved sucessfully : " + _mclt.MCLT_NAME + "','success')";
                    return Redirect("ManPower");
                }
            }
            return View(_mclt);
        }
        [jAuth(MenuId = 201)]
        public ActionResult ManPowerList(string MCLT_FMWH, string MCLT_TEXT)
        {
            List<V_MCLT_MANPOWER> d = _d.ManPowerList(MCLT_FMWH, MCLT_TEXT).ToList();
            if (d == null)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = d.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [jAuth(MenuId = 201)]
        public ActionResult ManPowerAction(string MCLT_TEXT)
        {
            return View();

        }
        [AllowAnonymous]
        public void DropDownFor_ManPower()
        {
            var d1 = _c.DropDownList_T_AMSP_WH();
            ViewBag.MCLT_FMWH = d1;
        }

    }
}