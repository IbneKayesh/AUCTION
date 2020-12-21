using auction.Dal;
using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Controllers
{
    public class AccountsController : Controller
    {
        Accounts_DAL _d = new Accounts_DAL();
        Common_DAL _c = new Common_DAL();
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            DropDownFor_Registration();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(AGENTS_NEW ag)
        {
            string Msg = "Swal.fire('success','Registration success','success')";
            ag.AGENT_STATUS = 0;
            ag.AGENT_PICTURES = "pic.jpg";


            if (ag.AGENT_COMPANY_T == null || ag.AGENT_COMPANY_T.Count < 1)
            {
                Msg = "Swal.fire('error','select at least one company','error')";
            }
            else if (ModelState.IsValid)
            {
                bool result = _d.SaveRegistrations(agent: ag);
                TempData["MSG"] = Msg;
                return RedirectToAction("Registration");
            }
            DropDownFor_Registration();
            TempData["MSG"] = Msg;
            return View(ag);
        }

        private void DropDownFor_Registration()
        {
            var d1 = _c.DropDownList_REGISTRATION_COMPANY();
            ViewBag.COMPANY = d1;
        }
    }
}
