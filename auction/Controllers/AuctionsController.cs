using auction.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Controllers
{
    public class AuctionsController : Controller
    {
        Common_DAL _c = new Common_DAL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult New_Auctions()
        {
            return View();
        }
        public void DropDownFor_New_Auctions()
        {
            var d1 = _c.DropDownList_REGISTRATION_COMPANY();
            ViewBag.ORDR_FMWH = d1;

            var d2 = _c.DropDownList_BOOKING_TYPE();
            var d3 = _c.DropDownList_CARRIERS();
            var d4= _c.DropDownList_CONTAINER_TYPE();
            var d5 = _c.DropDownList_ITEMS();
            var d6 = _c.DropDownList_PAYMENT_MODE();
            var d7 = _c.DropDownList_POD();
            var d8 = _c.DropDownList_POL();
            var d9 = _c.DropDownList_SHIPPING_TYPE();

        }
    }
}