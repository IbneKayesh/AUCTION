using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Controllers
{
    public class MoldController : Controller
    {
        public ActionResult NewMold()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewMold(T_MOLD _mold)
        {
            if (ModelState.IsValid)
            {
            }
            return View();
        }

    }
}