using auction.Dal;
using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Controllers
{
    public class BillProcessController : Controller
    {
        BillProcess_DAL _d = new BillProcess_DAL();
        Common_DAL _c = new Common_DAL();

        [jAuth(MenuId = 151)]
        public ActionResult CreateBill()
        {
            DropDownFor_CreateBill();
            return View();
        }
        [jAuth(MenuId = 151)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBill(ORMC_ORAM _OO)
        {
            if (ModelState.IsValid)
            {
                bool result = _d.SaveBillModel(oo: _OO);
                TempData["MSG"] = "Swal.fire('success','Bill saved success','success')";
                return RedirectToAction("CreateBill");
            }
            string Msg = string.Empty;
            if (_OO.T_ORAM == null || _OO.T_ORMC == null || _OO.T_ORAM.Count == 0 || _OO.T_ORMC.Count == 0)
            {
                Msg = "Swal.fire('error','enter at least one E and C item','error')";
            }
            else
            {
                Msg = "Swal.fire('error','Bill save failed','error')";
            }
            DropDownFor_CreateBill();
            TempData["MSG"] = Msg;
            return View(_OO);
        }


        [jAuth(MenuId = 153)]
        public ActionResult RateProcessing()
        {
            DropDownFor_RateProcessing();
            return View();
        }
        [jAuth(MenuId = 153)]
        public ActionResult OrderBillM(string ORDR_VAL)
        {
            List<V_ORDR_EXP> d = _d.OrderBillM_List(ORDR_VAL).ToList();
            if (d == null)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = d.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [jAuth(MenuId = 153)]
        public ActionResult OrderBillI(string ORDR_VAL)
        {
            List<V_ORDR_ITEM> d = _d.OrderBillI_List(ORDR_VAL).ToList();
            if (d == null)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = d.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [jAuth(MenuId = 153)]
        public ActionResult StartProcess(string ORDR_TEXT, string CO_TEXT, string CT)
        {
            string msg = string.Empty;
            int result = _d.StartProcess(ORDR_TEXT: ORDR_TEXT, CO_TEXT: CO_TEXT, CT: CT);
            if (result == 0)
            {
                msg = "E rate proccessing failed";
            }
            else if (result == 1)
            {
                msg = "C rate proccessing failed";
            }
            else if (result == 2)
            {
                msg = "Error while proccessing order";
            }
            else if (result == 3)
            {
                msg = "Proccess executed successfully";
            }
            else if (result == 4)
            {
                msg = "Execution failed without CO Number";
            }
            else
            {
                msg = "error in command";
            }
            return Json(new { messages = msg }, JsonRequestBehavior.AllowGet);
        }

        [jAuth(MenuId = 152)]
        public ActionResult CreateBillOnly()
        {
            DropDownFor_CreateBill();
            return View();
        }
        [jAuth(MenuId = 152)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBillOnly(ORMC _OO)
        {
            if (ModelState.IsValid)
            {
                bool result = _d.SaveBillModelOnly(oo: _OO);
                TempData["MSG"] = "Swal.fire('success','Bill saved success','success')";
                return RedirectToAction("CreateBillOnly");
            }
            string Msg = string.Empty;
            if (_OO.T_ORMC == null || _OO.T_ORMC.Count == 0)
            {
                Msg = "Swal.fire('error','enter at least one E item','error')";
            }
            else
            {
                Msg = "Swal.fire('error','Bill save failed','error')";
            }
            DropDownFor_CreateBill();
            TempData["MSG"] = Msg;
            return View(_OO);
        }


        public ActionResult WhClosing()
        {
            DropDownFor_WhClosing();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WhClosing(V_CLSM clsm)
        {
            DropDownFor_WhClosing();
            if (ModelState.IsValid)
            {
                int result =_d.WhClosingMonth(clsm:clsm);
                if (result == 0)
                {
                    TempData["MSG"] = "Swal.fire('error','Wh Closing save failed due to data validation error','error')";
                }
                else if(result == 2)
                {
                    TempData["MSG"] = "Swal.fire('error','No data found/Error in command','error')";
                }
                else
                {
                    TempData["MSG"] = "Swal.fire('success','Wh Closing save success','success')";
                }                
                return RedirectToAction("WhClosing");
            }
            TempData["MSG"] = "Swal.fire('error','Wh Closing save failed','error')";
            return View(clsm);
        }
        public ActionResult WhClosing_List(string ORDR_FMWH, string ORDR_CYCL)
        {
            List<T_CLSM> d = _d.ORBL_List(ORDR_FMWH, ORDR_CYCL).ToList();
            if (d == null)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = d.ToList() }, JsonRequestBehavior.AllowGet);
        }



        [AllowAnonymous]
        public ActionResult Order_Header(string ORDR_TEXT)
        {
            string _data = _c.Order_Header_Data(ORDR_TEXT: ORDR_TEXT);
            return Json(new { data = _data }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public void DropDownFor_CreateBill()
        {
            var d1 = _c.DropDownList_T_AMSP_WH();
            ViewBag.ORDR_FMWH = d1;
            var d2 = _c.DropDownList_T_AMSP_CO();
            ViewBag.ORDR_TOWH = d2;
            var d3 = _c.DropDownList_T_MCTP();
            ViewBag.MCTP_TEXT = d3;
        }
        [AllowAnonymous]
        public void DropDownFor_RateProcessing()
        {
            var d1 = _c.DropDownList_T_AMSP_WH();
            ViewBag.ORDR_FMWH = d1;
            var d2 = _c.DropDownList_T_AMSP_CO();
            ViewBag.ORDR_TOWH = d2;
            var d3 = _c.DropDownList_BILL_TYPE();
            ViewBag.BILL_TYPE = d3;
        }
        [AllowAnonymous]
        public ActionResult DropDownFor_scmItems()
        {
            var amim = _c.DropDownList_T_AMIM();
            return Json(amim, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult DropDownFor_DlvOrdrs(string ORDR_FMWH, string ORDR_TOWH)
        {
            var ordr = _c.DropDownList_T_ORDR_FOR_BILL(ORDR_FMWH, ORDR_TOWH);
            return Json(ordr, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult DropDownFor_ExpHead(string MCTP_TEXT, string MCLT_FMWH)
        {
            var exphd = _c.DropDownList_T_MCLT(MCTP_TEXT, MCLT_FMWH);
            return Json(exphd, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult DropDownFor_BillProces(string ORDR_FMWH, string ORDR_TOWH, DateTime fromDate, DateTime toDate, int BILL_TYPE)
        {
            var ordr = _c.DropDownList_T_ORDR_BILL_RATE_PROCCESS(ORDR_FMWH, ORDR_TOWH, fromDate, toDate, State: BILL_TYPE);
            return Json(ordr, JsonRequestBehavior.AllowGet);
        }
        public void DropDownFor_WhClosing()
        {
            var d1 = _c.DropDownList_T_AMSP_WH_For_Dev();
            ViewBag.CLSM_FMWH = d1;
            var d2 = _c.DropDownList_BillCycle();
            ViewBag.CLSM_CYCL = d2;
            var d3 = _c.DropDownList_Month();
            ViewBag.CLSM_MONT = d3;
            var d4 = _c.DropDownList_Year();
            ViewBag.CLSM_YEAR = d4;
            var d5 = _c.DropDownList_T_CLST();
            ViewBag.CLSD_CLST = d5;
            var d6 = _c.DropDownList_T_AMSP_CO_For_Dev();
            ViewBag.CLSM_TOWH = d6;
            var d7 = _c.DropDownList_CLSM_CLSD_TYPE();
            ViewBag.CLSM_TYPE=d7;
        }
    }
}