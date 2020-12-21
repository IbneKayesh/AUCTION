using auction.Dal;
using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Controllers
{
    public class SetupController : Controller
    {
        Setup_DAL _d = new Setup_DAL();
        Common_DAL _c = new Common_DAL();
        [jAuth(MenuId = 202)]
        public ActionResult ConsumptionItems()
        {
            DropDownFor_ConsumptionItems();
            return View();
        }
        [jAuth(MenuId = 202)]
        public ActionResult AddConItems(string amim_text, string ORDR_FMWH)
        {
            bool result = _d.Add_Con_Item(amim_text, ORDR_FMWH);
            string msg = string.Empty;
            if (result)
            {
                msg = "Item added successfully";
            }
            else
            {
                msg = "Item added failed";
            }
            return Json(new { messages = msg }, JsonRequestBehavior.AllowGet);
        }
        [jAuth(MenuId = 202)]
        public ActionResult ConItems(string amim_text)
        {
            List<T_AMIM> _amim = _d.T_AMIM_LIST(amim_text);
            if (_amim == null)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = _amim.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [jAuth(MenuId = 202)]
        public ActionResult Add_Con_Items_Auto(string ORDR_FMWH)
        {
            bool result = _d.Add_Con_Item_Auto(ORDR_FMWH);
            string msg = string.Empty;
            if (result)
            {
                msg = "Missing items auto added successfully";
            }
            else
            {
                msg = "No missing items found";
            }
            return Json(new { messages = msg }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public void DropDownFor_ConsumptionItems()
        {
            var d1 = _c.DropDownList_T_AMSP_WH();
            ViewBag.ORDR_FMWH = d1;
        }
    }
}