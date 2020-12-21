using auction.Dal;
using auction.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Data;
using ClosedXML.Excel;

namespace auction.Controllers
{
    public class ReportsController : Controller
    {
        Report_DAL _d = new Report_DAL();
        Common_DAL _c = new Common_DAL();
        Email_Sender _es = new Email_Sender();
        [jAuth(MenuId = 401)]
        public ActionResult Expenses()
        {
            DropDownFor_Expenses();
            return View();
        }
        [jAuth(MenuId = 401)]
        public ActionResult R_Unit_EC_Summary_Exl(DateTime FROM_DATE, DateTime TO_DATE, string ORDR_FMWH, string ORDR_TOWH)
        {
            R_EXPENSES r = new R_EXPENSES();
            r.FROM_DATE = FROM_DATE;
            r.TO_DATE = TO_DATE;
            r.ORDR_FMWH = ORDR_FMWH;
            r.ORDR_TOWH = ORDR_TOWH;

            string SheetName = FROM_DATE.Year + "" + FROM_DATE.Month.ToString().PadLeft(2, '0') + "" + FROM_DATE.Day.ToString().PadLeft(2, '0') + "-" + TO_DATE.Year + "" + TO_DATE.Month.ToString().PadLeft(2, '0') + "" + TO_DATE.Day.ToString().PadLeft(2, '0');

            List<R_Unit_EC_Summary_C> _data = new List<R_Unit_EC_Summary_C>();

            double DayRange = (r.TO_DATE - r.FROM_DATE).TotalDays;
            if (DayRange >= 61)
            {
                return null;
            }
            else
            {
                _data = _d.R_Unit_EC_Summary_DAL(r);
                DataTable dt = new DataTable();
                dt.Columns.Add("Wh");
                dt.Columns.Add("Co");
                dt.Columns.Add("Exp");
                dt.Columns.Add("Con");
                foreach (var item in _data)
                {
                    dt.Rows.Add(item.WH_NAME, item.CO_NAME, item.EXP_AMNT, item.CON_AMNT);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, SheetName);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Excel1.xlsx");
                        // creates byte array from stream
                        TempData["Output"] = stream.ToArray();

                        // returns successful state
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }
        [jAuth(MenuId = 401)]
        public ActionResult R_Unit_EC_Summary(DateTime FROM_DATE, DateTime TO_DATE, string ORDR_FMWH, string ORDR_TOWH)
        {
            R_EXPENSES r = new R_EXPENSES();
            r.FROM_DATE = FROM_DATE;
            r.TO_DATE = TO_DATE;
            r.ORDR_FMWH = ORDR_FMWH;
            r.ORDR_TOWH = ORDR_TOWH;

            List<R_Unit_EC_Summary_C> _data = new List<R_Unit_EC_Summary_C>();

            double DayRange = (TO_DATE - FROM_DATE).TotalDays;
            if (DayRange >= 61)
            {
                return PartialView(_data);
            }
            else
            {
                _data = _d.R_Unit_EC_Summary_DAL(r);
                return PartialView(_data);
            }
        }


        [jAuth(MenuId = 401)]
        public ActionResult R_Unit_EC_D_Summary(DateTime FROM_DATE, DateTime TO_DATE, string ORDR_FMWH, string ORDR_TOWH)
        {
            R_EXPENSES r = new R_EXPENSES();
            r.FROM_DATE = FROM_DATE;
            r.TO_DATE = TO_DATE;
            r.ORDR_FMWH = ORDR_FMWH;
            r.ORDR_TOWH = ORDR_TOWH;
            List<R_Unit_EC_D_Summary_C> _data = new List<R_Unit_EC_D_Summary_C>();
            double DayRange = (TO_DATE - FROM_DATE).TotalDays;
            if (DayRange >= 61)
            {
                return PartialView(_data);
            }
            else
            {
                _data = _d.R_Unit_EC_D_Summary_DAL(r);
                return PartialView(_data);
            }
        }
        [jAuth(MenuId = 401)]
        public ActionResult R_Unit_EC_D_Summary_Exl(DateTime FROM_DATE, DateTime TO_DATE, string ORDR_FMWH, string ORDR_TOWH)
        {
            R_EXPENSES r = new R_EXPENSES();
            r.FROM_DATE = FROM_DATE;
            r.TO_DATE = TO_DATE;
            r.ORDR_FMWH = ORDR_FMWH;
            r.ORDR_TOWH = ORDR_TOWH;

            string SheetName = FROM_DATE.Year + "" + FROM_DATE.Month.ToString().PadLeft(2, '0') + "" + FROM_DATE.Day.ToString().PadLeft(2, '0') + "-" + TO_DATE.Year + "" + TO_DATE.Month.ToString().PadLeft(2, '0') + "" + TO_DATE.Day.ToString().PadLeft(2, '0');

            List<R_Unit_EC_D_Summary_C> _data = new List<R_Unit_EC_D_Summary_C>();

            double DayRange = (r.TO_DATE - r.FROM_DATE).TotalDays;
            if (DayRange >= 61)
            {
                return null;
            }
            else
            {
                _data = _d.R_Unit_EC_D_Summary_DAL(r);
                DataTable dt = new DataTable();
                dt.Columns.Add("order");
                dt.Columns.Add("date");
                dt.Columns.Add("wh");
                dt.Columns.Add("co");
                dt.Columns.Add("type");
                dt.Columns.Add("work");
                dt.Columns.Add("exp");
                dt.Columns.Add("con");
                foreach (var item in _data)
                {
                    dt.Rows.Add(item.ORDR_TEXT, item.ORDR_DATE, item.WH_NAME, item.CO_NAME, item.ORTP_NAME, item.MOLD_NAME, item.EXP_AMNT, item.CON_AMNT);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, SheetName);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        TempData["Output"] = stream.ToArray();
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }


        [jAuth(MenuId = 401)]
        public ActionResult R_Unit_EC_Summary_Email(DateTime FROM_DATE, DateTime TO_DATE, string ORDR_FMWH)
        {
            R_EXPENSES r = new R_EXPENSES();
            r.FROM_DATE = FROM_DATE;
            r.TO_DATE = TO_DATE;
            r.ORDR_FMWH = ORDR_FMWH;

            List<R_Unit_EC_Summary_C> _data = new List<R_Unit_EC_Summary_C>();
            double DayRange = (TO_DATE - FROM_DATE).TotalDays;
            if (DayRange >= 61)
            {
                return PartialView("R_Unit_EC_Summary", _data);
            }
            else
            {
                _d.R_Unit_EC_Summary_Email_DAL(r);
                _es.R_Unit_EC_Summary_Email_Sent(_Data: _data, _R: r);
                return PartialView("R_Unit_EC_Summary", _data);
            }
        }
        [jAuth(MenuId = 401)]
        public ActionResult R_Unit_EC_ORAM_ORMC(string id)
        {
            List<R_Unit_EC_ORAM_ORMC_C> _data = _d.R_Unit_EC_ORAM_ORMC_DAL(id);
            if (_data == null) { _data = new List<R_Unit_EC_ORAM_ORMC_C>(); }
            return PartialView(_data);
        }

        [jAuth(MenuId = 401)]
        public ActionResult DownloadExcelFile()
        {
            string fileName = "ReportFile_" + DateTime.Now.Year + "" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "" + DateTime.Now.Day.ToString().PadLeft(2, '0') + ".xlsx";
            // retrieve byte array here
            var array = TempData["Output"] as byte[];
            if (array != null)
            {
                return File(array, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            else
            {
                return new EmptyResult();
            }
        }


        [jAuth(MenuId=401)]
        public ActionResult R_BILL_PENDING(DateTime FROM_DATE, DateTime TO_DATE, string ORDR_FMWH, string ORDR_TOWH, string OT)
        {
            R_EXPENSES r = new R_EXPENSES();
            r.FROM_DATE = FROM_DATE;
            r.TO_DATE = TO_DATE;
            r.ORDR_FMWH = ORDR_FMWH;
            r.ORDR_TOWH = ORDR_TOWH;
            List<R_BILL_PENDING_C> _data = new List<R_BILL_PENDING_C>();
            double DayRange = (TO_DATE - FROM_DATE).TotalDays;
            if (DayRange >= 61)
            {
                return PartialView(_data);
            }
            else
            {
                _data = _d.R_BILL_PENDING_DAL(r, OT:OT);
                return PartialView(_data);
            }
        }
        [jAuth(MenuId = 401)]
        public ActionResult R_BILL_PENDING_Exl(DateTime FROM_DATE, DateTime TO_DATE, string ORDR_FMWH, string ORDR_TOWH, string OT)
        {
            R_EXPENSES r = new R_EXPENSES();
            r.FROM_DATE = FROM_DATE;
            r.TO_DATE = TO_DATE;
            r.ORDR_FMWH = ORDR_FMWH;
            r.ORDR_TOWH = ORDR_TOWH;
            List<R_BILL_PENDING_C> _data = new List<R_BILL_PENDING_C>();
            double DayRange = (TO_DATE - FROM_DATE).TotalDays;
            if (DayRange >= 61)
            {
                return null;
            }
            else
            {
                string Pref = "pRate-";
                if (OT == "B")
                {
                    Pref = "pBill";
                }
                string SheetName = Pref + FROM_DATE.Year + "" + FROM_DATE.Month.ToString().PadLeft(2, '0') + "" + FROM_DATE.Day.ToString().PadLeft(2, '0') + "-" + TO_DATE.Year + "" + TO_DATE.Month.ToString().PadLeft(2, '0') + "" + TO_DATE.Day.ToString().PadLeft(2, '0');

                _data = _d.R_BILL_PENDING_DAL(r, OT: OT);
                DataTable dt = new DataTable();
                dt.Columns.Add("ot");
                dt.Columns.Add("wh");
                dt.Columns.Add("co");
                dt.Columns.Add("type");
                dt.Columns.Add("work");
                dt.Columns.Add("raiser");
                dt.Columns.Add("odate");
                dt.Columns.Add("ddate");
                dt.Columns.Add("problem");
                dt.Columns.Add("remarks");
                foreach (var item in _data)
                {
                    dt.Rows.Add(item.ORDR_TEXT, item.WH_NAME,item.CO_NAME,item.ORTP_NAME,item.MOLD_NAME,item.ORDR_RISR,item.ORDR_DATE,item.ORDR_DDAT,item.ORDR_PRLM,item.ORDR_CMNT);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, SheetName);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        TempData["Output"] = stream.ToArray();
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }



        [jAuth(MenuId = 402)]
        public ActionResult TimeConsume()
        {
            DropDownFor_TimeConsume();
            return View();
        }
        [jAuth(MenuId = 402)]
        public ActionResult R_TimeConsumeD(DateTime FROM_DATE, DateTime TO_DATE, string ORDR_FMWH, string ORDR_TOWH)
        {
            R_EXPENSES r = new R_EXPENSES();
            r.FROM_DATE = FROM_DATE;
            r.TO_DATE = TO_DATE;
            r.ORDR_FMWH = ORDR_FMWH;
            r.ORDR_TOWH = ORDR_TOWH;

            List<R_TimeConsumeC> _data = new List<R_TimeConsumeC>();

            double DayRange = (TO_DATE - FROM_DATE).TotalDays;
            if (DayRange >= 31)
            {
                return PartialView(_data);
            }
            else
            {
                _data = _d.R_TimeConsumeD_DAL(r);
                List<R_TimeConsumeDC> R_TimeConsumeDC = new List<R_TimeConsumeDC>();
                foreach (var item in _data)
                {
                    DateTime dd = item.ORDR_DDAT ?? DateTime.Now;
                    DateTime rd = item.ORDR_RDAT ?? DateTime.Now;
                    DateTime cd = item.CDT;
                    TimeSpan span_rd = (rd - cd);
                    string s_rd = String.Format("{0} d, {1} h, {2} m, {3} s",
                        span_rd.Days, span_rd.Hours, span_rd.Minutes, span_rd.Seconds);

                    TimeSpan span_dd = (dd - rd);
                    string s_dd = String.Format("{0} d, {1} h, {2} m, {3} s",
                        span_dd.Days, span_dd.Hours, span_dd.Minutes, span_dd.Seconds);

                    R_TimeConsumeDC row = new R_TimeConsumeDC();
                    row.ORDR_TEXT = item.ORDR_TEXT;
                    row.WH_NAME = item.WH_NAME;
                    row.CO_NAME = item.CO_NAME;
                    row.ORTP_NAME = item.ORTP_NAME;
                    row.MOLD_NAME = item.MOLD_NAME;
                    row.CDT = item.CDT;
                    row.ORDR_RDAT = item.ORDR_RDAT;
                    row.OR_C = s_rd;
                    row.ORDR_DDAT = item.ORDR_DDAT;
                    row.RD_C = s_dd;
                    row.ORDR_STAS = item.ORDR_STAS;
                    R_TimeConsumeDC.Add(row);
                }

                return PartialView(R_TimeConsumeDC);
            }
        }

        [jAuth(MenuId= 403)]
        public ActionResult ExpConsume()
        {
            DropDownFor_ExpConsume();
            return View();
        }
        [jAuth(MenuId = 403)]
        public ActionResult R_ExpConsume(DateTime FROM_DATE, DateTime TO_DATE, string ORDR_FMWH, string MCTP_TEXT)
        {
            R_EXPENSES r = new R_EXPENSES();
            r.FROM_DATE = FROM_DATE;
            r.TO_DATE = TO_DATE;
            r.ORDR_FMWH = ORDR_FMWH;
            r.ORDR_TOWH = MCTP_TEXT; // MCTP_TEXT as ORDR_TOWH

            List<R_ExpConsumeC> _data = new List<R_ExpConsumeC>();
            double DayRange = (TO_DATE - FROM_DATE).TotalDays;
            if (DayRange >= 31)
            {
                return PartialView(_data);
            }
            else
            {
                _data = _d.R_ExpConsume_DAL(r);
                return PartialView(_data);
            }
        }
        
        [AllowAnonymous]
        public void DropDownFor_Expenses()
        {
            var d1 = _c.DropDownList_T_AMSP_WH();
            ViewBag.ORDR_FMWH = d1;
            var d2 = _c.DropDownList_T_AMSP_CO();
            ViewBag.ORDR_TOWH = d2;
        }
        [AllowAnonymous]
        public void DropDownFor_TimeConsume()
        {
            var d1 = _c.DropDownList_T_AMSP_WH();
            ViewBag.ORDR_FMWH = d1;
            var d2 = _c.DropDownList_T_AMSP_CO();
            ViewBag.ORDR_TOWH = d2;
        }
        [AllowAnonymous]
        public void DropDownFor_ExpConsume()
        {
            var d1 = _c.DropDownList_T_AMSP_WH();
            ViewBag.ORDR_FMWH = d1;
            var d2 = _c.DropDownList_T_MCTP();
            ViewBag.MCTP_TEXT = d2;
        }
    }
}