using auction.Dal;
using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Controllers
{
    public class AdminController : Controller
    {
        Admin_DAL _d = new Admin_DAL();
        Common_DAL _c = new Common_DAL();
        [jAuth(MenuId = 451)]
        public ActionResult Order_Top_Sheet()
        {
            DropDownFor_Order_Top_Sheet();
            var T_SMRY_List = _d.T_SMRY_List();
            return View(T_SMRY_List);
        }
        [jAuth(MenuId = 451)]
        public ActionResult Order_Top_Sheet_P(DateTime FROM_DATE, DateTime TO_DATE, string ORDR_FMWH, string ORDR_TOWH)
        {
            R_EXPENSES r = new R_EXPENSES();
            r.FROM_DATE = FROM_DATE;
            r.TO_DATE = TO_DATE;
            r.ORDR_FMWH = ORDR_FMWH;
            r.ORDR_TOWH = ORDR_TOWH;            

            double DayRange = (TO_DATE - FROM_DATE).TotalDays;
            if (DayRange >= 31)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                _d.PRO_SMRY(r);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
        }

        [jAuth(MenuId = 452)]
        public ActionResult Order_Top_Sheet_WH()
        {
            DropDownFor_Order_Top_Sheet_WH();
            return View();
        }
        [jAuth(MenuId = 452)]
        public JsonResult Order_Data_WH(string wh)
        {
            var _D = _d.T_SMRY_WH(ORDR_FMWH:wh);
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "Name", "Data" });
            chartData.Add(new object[] { "Pending", (double)_D.SMRY_NSMT});
            chartData.Add(new object[] { "In Working", (double)_D.SMRY_IWRK });
            chartData.Add(new object[] { "Delivered", (double)_D.SMRY_NBIL });
            chartData.Add(new object[] { "In Process", (double)_D.SMRY_NRTP });
            chartData.Add(new object[] { "Completed", (double)_D.SMRY_CMPT });
            chartData.Add(new object[] { "Returned", (double)_D.SMRY_NRCV });
            return Json(chartData, JsonRequestBehavior.AllowGet);
        }
        [jAuth(MenuId = 452)]
        public ActionResult Order_Top_Sheet_W(DateTime FROM_DATE, DateTime TO_DATE, string ORDR_FMWH)
        {
            R_EXPENSES r = new R_EXPENSES();
            r.FROM_DATE = FROM_DATE;
            r.TO_DATE = TO_DATE;
            r.ORDR_FMWH = ORDR_FMWH;
            r.ORDR_TOWH = "";
            double DayRange = (TO_DATE - FROM_DATE).TotalDays;
            if (DayRange >= 31)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                _d.PRO_SMRY_W(r);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public void DropDownFor_Order_Top_Sheet()
        {
            var d1 = _c.DropDownList_T_AMSP_CO();
            ViewBag.ORDR_TOWH = d1;
            var d2 = _c.DropDownList_T_AMSP_WH();
            ViewBag.ORDR_FMWH = d2;
        }

        [AllowAnonymous]
        public void DropDownFor_Order_Top_Sheet_WH()
        {
            var d2 = _c.DropDownList_T_AMSP_WH();
            ViewBag.ORDR_FMWH = d2;
        }
    }
}