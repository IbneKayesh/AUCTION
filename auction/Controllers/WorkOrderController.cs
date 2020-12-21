using auction.Dal;
using auction.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Controllers
{
    public class WorkOrderController : Controller
    {
        WorkOrder_DAL _d = new WorkOrder_DAL();
        Common_DAL _c = new Common_DAL();
        Mold_DAL _md = new Mold_DAL();
        CommonData cd = new CommonData();
        Email_Sender _es = new Email_Sender();
        #region Work_Order_Demand
        [jAuth(MenuId = 101)]
        public ActionResult Index()
        {
            DropDownFor_Index();
            return View();
        }
        [jAuth(MenuId = 101)]
        public ActionResult find_order(DateTime? fromDate, DateTime? toDate, string ORDR_TEXT, string ORDR_NAME, string ORDR_FMWH, string ORDR_TOWH, string ORDR_ORTP, string ORDR_STAT, string QUERY_NAME)
        {
            DateTime fd = fromDate ?? DateTime.Now.Date;
            DateTime td = toDate ?? DateTime.Now.Date;
            double DayRange = (td - fd).TotalDays;
            if (DayRange >= 61)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }

            v_ORDR_QUERY _q = new v_ORDR_QUERY();
            _q.fromDate = fromDate;
            _q.toDate = toDate;
            _q.ORDR_TEXT = ORDR_TEXT;
            _q.ORDR_NAME = ORDR_NAME;
            _q.ORDR_FMWH = ORDR_FMWH;
            _q.ORDR_TOWH = ORDR_TOWH;
            _q.ORDR_ORTP = ORDR_ORTP;
            _q.ORDR_STAS = ORDR_STAT;
            _q.QUERY_NAME = QUERY_NAME;
            if (string.IsNullOrWhiteSpace(QUERY_NAME))
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            List<V_ORDR> d = _d.OrderList(_q).ToList();
            if (d == null)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = d.ToList() }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult NewWorkOrder()
        {
            DropDownFor_NewWorkOrder();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewWorkOrder(v_wkod_master wm)
        {
            DropDownFor_NewWorkOrder();
            if (ModelState.IsValid)
            {
                List<T_ORDR> t_list = new List<T_ORDR>();
                CommonData cd = new CommonData();
                int SEQN = cd.Table_Sequence("ordr_seqn", "auction.t_ordr");
                int sl = 1;
                string OrdrName = cd.Ordr_Name(SEQN);
                foreach (var item in wm.V_WKOD_ITEM)
                {
                    //string Sql = string.Format(@"Insert into T_ORDR(ORDR_TEXT, ORDR_NAME, ORDR_BSNS, ORDR_TYPE, ORDR_RISR, ORDR_DATE, ORDR_STAS, ORDR_ITEM, ORDR_UNIT, ORDR_QNTY, ORDR_RATE, ORDR_PRLM,ORDR_NOTE,ORDR_CMNT ,ORDR_SBMT, ORDR_SEQN, ACT, CDT, VAR) Values('{0}', '{1}', '{2}', '{3}', '{4}', TO_DATE('{5}', 'MM/DD/YYYY HH:MI:SS AM'), 'P', '{6}', '{7}', 1, {8}, '{9}','{10}','{11}', 'N', {12}, 1, TO_DATE('{13}', 'MM/DD/YYYY HH:MI:SS AM'), 1)",
                    //    "T01", "N01", wm.ORDR_BSNS, wm.ORDR_TYPE, wm.ORDR_RISR, wm.ORDR_DATE, item.ORDR_ITEM, item.ORDR_UNIT, item.ORDR_RATE, item.ORDR_PRLM, item.ORDR_NOTE, wm.ORDR_CMNT, 1, DateTime.Now);
                    //o_list.Add(Sql);
                    string OrdrText = cd.Ordr_Text(sl, OrdrName);
                    sl = sl + 1;
                    T_ORDR o = new T_ORDR();
                    o.ORDR_TEXT = OrdrText;
                    o.ORDR_NAME = OrdrName;
                    o.ORDR_FMWH = wm.ORDR_FMWH;
                    o.ORDR_TOWH = wm.ORDR_TOWH;
                    o.ORDR_ORTP = item.ORDR_ORTP;
                    o.ORDR_RISR = wm.ORDR_RISR;
                    string dt = wm.ORDR_DATE.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    o.ORDR_DATE = DateTime.ParseExact(dt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    o.ORDR_STAS = "P";
                    o.ORDR_ITEM = item.ORDR_ITEM;
                    o.ORDR_UNIT = item.ORDR_UNIT;
                    o.ORDR_QNTY = 1;
                    o.ORDR_RATE = 0;
                    o.ORDR_PRLM = item.ORDR_PRLM;
                    o.ORDR_NOTE = item.ORDR_NOTE;
                    o.ORDR_SBMT = "N";
                    o.ORDR_SEQN = SEQN;
                    o.ACT = 0;
                    t_list.Add(o);
                }
                bool result = _d.SaveOrderModel(o_list: t_list);
                TempData["MSG"] = "Swal.fire('success','Save Success ON: " + OrdrName + "','success')";
                return RedirectToAction("NewWorkOrder");
            }
            TempData["MSG"] = "Swal.fire('error', 'invalid order data', 'error')";
            return View(wm);
        }
        [jAuth(MenuId = 102)]
        public ActionResult OrderEdit(string ORDR_TEXT)
        {
            v_ordr_edit edit = _d._edit(ORDR_TEXT);
            if (edit != null)
            {
                DropDownFor_OrderEdit();
                return View(edit);
            }
            TempData["MSG"] = "Swal.fire('error', 'unable to edit order data', 'error')";
            return Redirect("Index");
        }
        [jAuth(MenuId = 102)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderEdit(v_ordr_edit _edit)
        {
            DropDownFor_OrderEdit();
            if (ModelState.IsValid)
            {
                bool _data = _d._editSave(_edit: _edit);
                if (_data)
                {
                    TempData["MSG"] = "Swal.fire('Success', 'changes data saved', 'success')";
                    return Redirect("Index");
                }
            }
            TempData["MSG"] = "Swal.fire('error', 'invalid order data', 'error')";
            return Redirect("Index");
        }
        [jAuth(MenuId = 102)]
        [HttpPost]
        public ActionResult OrderSubmit(string ORDR_TEXT)
        {
            bool result = _d.OrderSubmit(ORDR_TEXT: ORDR_TEXT);
            if (result)
            {
                _es.WorkOrderSubmit(ORDR_TEXT);
                return Json(new { success = true, responseText = "Order submitted successfully" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "Order already submitted" }, JsonRequestBehavior.AllowGet);
        }

        #endregion





        #region Work_Order_Status_Tool_Room

        [jAuth(MenuId = 103)]
        public ActionResult WorkOrders_tr()
        {
            DropDownFor_WorkOrders_tr();
            return View();
        }
        [jAuth(MenuId = 103)]
        public ActionResult find_order_tr(DateTime? fromDate, DateTime? toDate, string ORDR_TEXT, string ORDR_NAME, string ORDR_FMWH, string ORDR_TOWH, string ORDR_ORTP, string ORDR_STAT, string QUERY_NAME)
        {
            DateTime fd = fromDate ?? DateTime.Now.Date;
            DateTime td = toDate ?? DateTime.Now.Date;
            double DayRange = (td - fd).TotalDays;
            if (DayRange >= 61)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }

            v_ORDR_QUERY _q = new v_ORDR_QUERY();
            _q.fromDate = fromDate;
            _q.toDate = toDate;
            _q.ORDR_TEXT = ORDR_TEXT;
            _q.ORDR_NAME = ORDR_NAME;
            _q.ORDR_FMWH = ORDR_FMWH;
            _q.ORDR_TOWH = ORDR_TOWH;
            _q.ORDR_ORTP = ORDR_ORTP;
            _q.ORDR_STAS = ORDR_STAT;
            _q.QUERY_NAME = QUERY_NAME;
            if (string.IsNullOrWhiteSpace(QUERY_NAME))
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            List<V_ORDR> d = _d.Order_List_Tr(_q).ToList();
            if (d == null)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = d.ToList() }, JsonRequestBehavior.AllowGet);
        }
        [jAuth(MenuId = 103)]
        public ActionResult Receive(string ORDR_TEXT)
        {
            v_ordr_received edit = _d._receive(ORDR_TEXT);
            if (edit != null)
            {
                return PartialView(edit);
            }
            string msg = string.Format(@"<strong style='color:red;'>{0} : work order already received</strong>", ORDR_TEXT);
            return Content(msg);
        }
        //[jAuth(MenuId = 103)]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Receive(v_ordr_received _edit)
        //{
        //    string msg = string.Empty;
        //    if (ModelState.IsValid)
        //    {
        //        bool _data = _d.__receiveSave(_edit: _edit);
        //        if (_data)
        //        {
        //            _es.WorkOrderTR(_edit.ORDR_TEXT, "RECEIVE");
        //            TempData["MSG"] = "Swal.fire('success','order received sucessfully : " + _edit.ORDR_TEXT + "','success')";
        //            return RedirectToAction("WorkOrders_tr");
        //        }
        //    }
        //    TempData["MSG"] = "Swal.fire('failed','order received failed : " + _edit.ORDR_TEXT + "','error')";
        //    return RedirectToAction("WorkOrders_tr");
        //}
        [jAuth(MenuId = 103)]
        [HttpGet]
        public ActionResult OrderReceive(string order, string comnt)
        {
            if (!string.IsNullOrWhiteSpace(comnt) && !string.IsNullOrWhiteSpace(order))
            {
                v_ordr_received _edit = new v_ordr_received();
                _edit.ORDR_TEXT = order;
                _edit.ORDR_CMNT = comnt;
                bool _data = _d.__receiveSave(_edit: _edit);
                if (_data)
                {
                    _es.WorkOrderTR(order, "RECEIVE");
                    return Json(new { data = "order received : " + order }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "order already received : " + order }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { data = "order received failed : " + order }, JsonRequestBehavior.AllowGet);
            }
        }

        [jAuth(MenuId = 103)]
        public ActionResult Return(string ORDR_TEXT)
        {
            v_ordr_received edit = _d._return(ORDR_TEXT);
            if (edit != null)
            {
                return PartialView(edit);
            }
            string msg = string.Format(@"<strong style='color:red;'>{0} : work order not received yet or deliverd</strong>", ORDR_TEXT);
            return Content(msg);
        }
        [jAuth(MenuId = 103)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(v_ordr_received _edit)
        {
            string msg = string.Empty;
            if (ModelState.IsValid)
            {
                bool _data = _d._returnSave(_edit: _edit);
                if (_data)
                {
                    _es.WorkOrderTR(_edit.ORDR_TEXT, "RETURN");
                    TempData["MSG"] = "Swal.fire('success','order returned sucessfully : " + _edit.ORDR_TEXT + "','success')";
                    return RedirectToAction("WorkOrders_tr");
                }
            }
            TempData["MSG"] = "Swal.fire('failed','order returned failed : " + _edit.ORDR_TEXT + "','error')";
            return RedirectToAction("WorkOrders_tr");
        }
        [jAuth(MenuId = 103)]
        public ActionResult Delivery(string ORDR_TEXT)
        {
            v_ordr_received edit = _d._return(ORDR_TEXT);
            if (edit != null)
            {
                return PartialView(edit);
            }
            string msg = string.Format(@"<strong style='color:red;'>{0} : work order not received yet or returned</strong>", ORDR_TEXT);
            return Content(msg);
        }
        [jAuth(MenuId = 103)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delivery(v_ordr_received _edit)
        {
            string msg = string.Empty;
            if (ModelState.IsValid)
            {
                string _data = _d._DeliverySave(_edit: _edit);
                if (_data == "1")
                {
                    _es.WorkOrderTR(_edit.ORDR_TEXT, "DELIVERY");
                    TempData["MSG"] = "Swal.fire('success','order delivered sucessfully : " + _edit.ORDR_TEXT + "','success')";
                    return RedirectToAction("WorkOrders_tr");
                }
                else if (_data == "0")
                {
                    TempData["MSG"] = "Swal.fire('failed','order delivered failed : " + _edit.ORDR_TEXT + "','error')";
                    return RedirectToAction("WorkOrders_tr");
                }
                else //create new mold with new delivery
                {
                    Mold_DAL _m = new Mold_DAL();
                    var _v_data = _m._CreateLike(_data);
                    if (_v_data != null)
                    {
                        _v_data.CDU = _edit.ORDR_TEXT;
                        _v_data.UDU = _edit.ORDR_CMNT;
                        return View("CreateLikeMold", _v_data);
                    }
                }
            }
            TempData["MSG"] = "Swal.fire('failed','order delivered failed : " + _edit.ORDR_TEXT + "','error')";
            return RedirectToAction("WorkOrders_tr");
        }

        [jAuth(MenuId = 103)]
        [HttpPost]
        public ActionResult OrderDelivery(string order, string comnt)
        {
            if (!string.IsNullOrWhiteSpace(comnt) && !string.IsNullOrWhiteSpace(order))
            {
                v_ordr_received _edit = new v_ordr_received();
                _edit.ORDR_TEXT = order;
                _edit.ORDR_CMNT = comnt;
                string _data = _d._DeliverySave(_edit: _edit);
                if (_data == "1")
                {
                    _es.WorkOrderTR(_edit.ORDR_TEXT, "DELIVERY");
                    return Json(new { success = true, msg = "order delivered succesfully : " + order }, JsonRequestBehavior.AllowGet);
                }
                else if (_data == "0")
                {
                    return Json(new { success = false, msg = "order delivered failed : " + order }, JsonRequestBehavior.AllowGet);
                }
                else //create new mold with new delivery
                {
                    return Json(new { success = false, msg = "order delivered failed, try using Submit N: " + order }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, msg = "order delivered failed : " + order }, JsonRequestBehavior.AllowGet);
            }
        }


        [jAuth(MenuId = 103)]
        public ActionResult CreateLikeMold(T_MOLD M)
        {
            int SEQN = cd.Table_Sequence("mold_seqn", "auction.t_mold");
            string Mold_Text = cd.Mold_Text(SEQN);
            M.MOLD_TEXT = Mold_Text;
            M.MOLD_SEQN = SEQN;
            if (ModelState.IsValid)
            {
                bool result = _md.CreateLikeMoldDelivery(m: M);
                if (result == true)
                {
                    _es.WorkOrderTR(M.CDU, "DELIVERY");
                    TempData["MSG"] = "Swal.fire('success','order delivered sucessfully : " + M.CDU + "','success')";
                    return RedirectToAction("WorkOrders_tr");
                }
            }
            TempData["MSG"] = "Swal.fire('error','order delivered falied : " + M.CDU + "','error')";
            return View(M);
        }
        #endregion


        [AllowAnonymous]
        public void DropDownFor_Index()
        {
            var d1 = _c.DropDownList_T_AMSP_WH();
            ViewBag.ORDR_FMWH = d1;
            var d2 = _c.DropDownList_T_AMSP_CO();
            ViewBag.ORDR_TOWH = d2;
            var d3 = _c.DropDownList_T_ORTP();
            ViewBag.ORDR_ORTP = d3;
            var d4 = _c.DropDownList_ORDR_STATUS();
            ViewBag.ORDR_STAS = d4;
        }
        [AllowAnonymous]
        public void DropDownFor_NewWorkOrder()
        {
            var d1 = _c.DropDownList_T_AMSP_WH();
            ViewBag.ORDR_FMWH = d1;
            var d2 = _c.DropDownList_T_AMSP_CO();
            ViewBag.ORDR_TOWH = d2;
            var d3 = _c.DropDownList_T_ORTP();
            ViewBag.ORDR_ORTP = d3;
        }
        [AllowAnonymous]
        public void DropDownFor_WorkOrders_tr()
        {
            var d1 = _c.DropDownList_T_AMSP_WH();
            ViewBag.ORDR_FMWH = d1;
            var d2 = _c.DropDownList_T_AMSP_CO();
            ViewBag.ORDR_TOWH = d2;
            var d3 = _c.DropDownList_T_ORTP();
            ViewBag.ORDR_ORTP = d3;
            var d4 = _c.DropDownList_ORDR_STATUS();
            ViewBag.ORDR_STAS = d4;
        }
        [AllowAnonymous]
        public void DropDownFor_OrderEdit()
        {
            var d2 = _c.DropDownList_T_AMSP_CO();
            ViewBag.ORDR_TOWH = d2;
            var d3 = _c.DropDownList_T_ORTP();
            ViewBag.ORDR_ORTP = d3;
        }
        [AllowAnonymous]
        public ActionResult DropDownFor_MoldList(string MOLD_AMSP)
        {
            var mold = _c.DropDownList_T_MOLD(MOLD_AMSP);
            return Json(mold, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult Hidden_Mold_Unit(string MOLD_TEXT)
        {
            if (!string.IsNullOrWhiteSpace(MOLD_TEXT))
            {
                var mold = _c.T_MOLD_MOLD_UNIT(MOLD_TEXT);
                return Json(new { AMSU_OID = mold.OID, AMSU_NAME = mold.AMSU_NAME }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

    }
};