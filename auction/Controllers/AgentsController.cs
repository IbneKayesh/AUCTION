using auction.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Controllers
{
    public class AgentsController : Controller
    {
        Accounts_DAL _d = new Accounts_DAL();
        public ActionResult Index()
        {
            var _data = _d.AGENTS_NEW_LIST(0);
            return View(_data);
        }
        public ActionResult Agents()
        {
            var _data = _d.AGENTS_NEW_LIST(1);
            return View(_data);
        }
    }
}