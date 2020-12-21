using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Dal
{
    public class Common_DAL
    {
        public SelectList DropDownList_REGISTRATION_COMPANY()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"SELECT C.COMPANY_ID,(C.COMPANY_SHORT_NAME ||' - '|| C.COMPANY_NAME)COMPANY_NAME
                    FROM COMPANY C
                    WHERE C.ACT=1");
                List<COMPANY_V> amsp = db.Database.SqlQuery<COMPANY_V>(sql: Sql).ToList();
                return new SelectList(amsp, "COMPANY_ID", "COMPANY_NAME");
            }
        }

        public SelectList DropDownList_BOOKING_TYPE()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"SELECT BOOKING_TYPE_ID,BOOKING_TYPE_NAME FROM BOOKING_TYPE WHERE ACT=1");
                List<BOOKING_TYPE_V> amsp = db.Database.SqlQuery<BOOKING_TYPE_V>(sql: Sql).ToList();
                return new SelectList(amsp, "BOOKING_TYPE_ID", "BOOKING_TYPE_NAME");
            }
        }

        public SelectList DropDownList_CARRIERS()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"SELECT CARRIER_ID,CARRIER_NAME FROM CARRIERS WHERE ACT=1");
                List<CARRIERS_V> amsp = db.Database.SqlQuery<CARRIERS_V>(sql: Sql).ToList();
                return new SelectList(amsp, "CARRIER_ID", "CARRIER_NAME");
            }
        }

        public SelectList DropDownList_CONTAINER_TYPE()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"SELECT CONTAINER_TYPE_ID, CONTAINER_TYPE_NAME FROM CONTAINER_TYPE WHERE ACT=1");
                List<CONTAINER_TYPE_V> amsp = db.Database.SqlQuery<CONTAINER_TYPE_V>(sql: Sql).ToList();
                return new SelectList(amsp, "CONTAINER_TYPE_ID", "CONTAINER_TYPE_NAME");
            }
        }


        public SelectList DropDownList_CONTAINERS()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                //string Sql = string.Format(@"SELECT CONTAINER_ID, CONTAINER_NAME FROM CONTAINERS WHERE ACT=1 AND CONTAINER_TYPE_ID=10");
                string Sql = string.Format(@"SELECT CONTAINER_ID, CONTAINER_NAME FROM CONTAINERS WHERE ACT=1");
                List<CONTAINERS_V> amsp = db.Database.SqlQuery<CONTAINERS_V>(sql: Sql).ToList();
                return new SelectList(amsp, "CONTAINER_ID", "CONTAINER_NAME");
            }
        }

        public SelectList DropDownList_CURRENCY()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"SELECT CURRENCY_ID, CURRENCY_NAME FROM CURRENCY WHERE ACT=1");
                List<CURRENCY_V> amsp = db.Database.SqlQuery<CURRENCY_V>(sql: Sql).ToList();
                return new SelectList(amsp, "CURRENCY_ID", "CURRENCY_NAME");
            }
        }

        public SelectList DropDownList_ITEM_UNIT()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"SELECT ITEM_UNIT_ID,ITEM_UNIT_NAME FROM ITEM_UNIT WHERE ACT=1");
                List<ITEM_UNIT_V> amsp = db.Database.SqlQuery<ITEM_UNIT_V>(sql: Sql).ToList();
                return new SelectList(amsp, "ITEM_UNIT_ID", "ITEM_UNIT_NAME");
            }
        }


        public SelectList DropDownList_ITEMS()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"SELECT ITEM_ID,ITEM_NAME FROM ITEMS WHERE ACT=1");
                List<ITEMS_V> amsp = db.Database.SqlQuery<ITEMS_V>(sql: Sql).ToList();
                return new SelectList(amsp, "ITEM_ID", "ITEM_NAME");
            }
        }

        public SelectList DropDownList_PAYMENT_MODE()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"SELECT PAYMENT_MODE_ID,PAYMENT_MODE_NAME FROM PAYMENT_MODE WHERE ACT=1");
                List<PAYMENT_MODE_V> amsp = db.Database.SqlQuery<PAYMENT_MODE_V>(sql: Sql).ToList();
                return new SelectList(amsp, "PAYMENT_MODE_ID", "PAYMENT_MODE_NAME");
            }
        }

        public SelectList DropDownList_POD()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"SELECT POD_ID,POD_NAME FROM POD WHERE ACT=1");
                List<POD_V> amsp = db.Database.SqlQuery<POD_V>(sql: Sql).ToList();
                return new SelectList(amsp, "POD_ID", "POD_NAME");
            }
        }

        public SelectList DropDownList_POL()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"SELECT POL_ID,POL_NAME FROM POL WHERE ACT=1");
                List<POL_V> amsp = db.Database.SqlQuery<POL_V>(sql: Sql).ToList();
                return new SelectList(amsp, "POL_ID", "POL_NAME");
            }
        }
        public SelectList DropDownList_SHIPPING_TYPE()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"SELECT SHIPPING_TYPE_ID,SHIPPING_TYPE_NAME FROM SHIPPING_TYPE WHERE ACT=1");
                List<SHIPPING_TYPE_V> amsp = db.Database.SqlQuery<SHIPPING_TYPE_V>(sql: Sql).ToList();
                return new SelectList(amsp, "SHIPPING_TYPE_ID", "SHIPPING_TYPE_NAME");
            }
        }



        public SelectList AgentCompany()
        {
            var Data = System.Web.HttpContext.Current.Session["UserId"];
            if (Data != null)
            {
                using (auctionDbContext db = new auctionDbContext())
                {
                    string Sql = string.Format(@"SELECT C.COMPANY_ID,(C.COMPANY_SHORT_NAME ||' - '|| C.COMPANY_NAME)COMPANY_NAME, NVL(AC.ACT,0) ACT
                    FROM COMPANY C
                    LEFT OUTER JOIN AGENTS_COMPANY AC ON C.COMPANY_ID = AC.COMPANY_ID AND AC.AGENT_ID='{0}'
                    WHERE C.ACT=1", Data);
                    List<COMPANY_V> amsp = db.Database.SqlQuery<COMPANY_V>(sql: Sql).ToList();
                    return new SelectList(amsp, "COMPANY_ID", "COMPANY_NAME");
                }
            }
            else
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }


        public SelectList DropDownList_T_AMSP_WH()
        {
            var Data = System.Web.HttpContext.Current.Session["UserId"];
            if (Data != null)
            {
                using (auctionDbContext db = new auctionDbContext())
                {
                    string Sql = string.Format(@"select w.oid, w.amsp_name
                    from auction.t_amsp w join auction.t_wcrt p
                    on w.oid = p.wcrt_amsp and p.wcrt_actv = 1 and p.wcrt_user = '{0}'
                    where w.amsp_flag = 4 and w.amsp_actv = 1", Data);
                    List<V_ASMP> amsp = db.Database.SqlQuery<V_ASMP>(sql: Sql).ToList();
                    return new SelectList(amsp, "OID", "AMSP_NAME");
                }
            }
            else
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public SelectList DropDownList_T_AMSP_WH_For_Dev()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"select w.oid, w.amsp_name
                    from auction.t_amsp w 
                    where w.amsp_flag = 4 and w.amsp_actv = 1");
                List<V_ASMP> amsp = db.Database.SqlQuery<V_ASMP>(sql: Sql).ToList();
                return new SelectList(amsp, "OID", "AMSP_NAME");
            }
        }


        public SelectList DropDownList_T_AMSP_CO()
        {
            var Data = System.Web.HttpContext.Current.Session["UserId"];
            if (Data != null)
            {
                using (auctionDbContext db = new auctionDbContext())
                {
                    string Sql = string.Format(@"select w.oid, w.amsp_name
                    from auction.t_amsp w join auction.t_wcrt p
                    on w.oid = p.wcrt_amsp and p.wcrt_actv = 1 and p.wcrt_user = '{0}'
                    where w.amsp_flag = 2 and w.amsp_actv = 1", Data);
                    List<V_ASMP> amsp = db.Database.SqlQuery<V_ASMP>(sql: Sql).ToList();
                    return new SelectList(amsp, "OID", "AMSP_NAME");
                }
            }
            else
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
        public SelectList DropDownList_T_AMSP_CO_For_Dev()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"select w.oid, w.amsp_name
                    from auction.t_amsp w 
                    where w.amsp_flag = 2 and w.amsp_actv = 1");
                List<V_ASMP> amsp = db.Database.SqlQuery<V_ASMP>(sql: Sql).ToList();
                return new SelectList(amsp, "OID", "AMSP_NAME");
            }
        }
        public SelectList DropDownList_T_MCTP()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                return new SelectList(db.T_MCTP.ToList(), "MCTP_TEXT", "MCTP_NAME");
            }
        }
        public SelectList DropDownList_T_ORTP()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                return new SelectList(db.T_ORTP.ToList(), "ORTP_TEXT", "ORTP_NAME");
            }
        }
        public SelectList DropDownList_T_AMIM()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                return new SelectList(db.T_AMIM.Where(i => i.AMIM_ACTV == "1").ToList(), "OID", "AMIM_NAME");
            }
        }
        public SelectList DropDownList_T_MOLD(string MOLD_AMSP)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                return new SelectList(db.T_MOLD.Where(s => s.MOLD_AMSP == MOLD_AMSP && s.ACT == 1).ToList(), "MOLD_TEXT", "MOLD_NAME");
            }
        }
        public SelectList DropDownList_BILL_TYPE()
        {
            SelectList BillType = new SelectList(
              new List<SelectListItem>
              {
                new SelectListItem { Selected = true, Text = "Pending", Value ="1"},
                new SelectListItem { Selected = false, Text = "Completed", Value = "2"},
              }, "Value", "Text", 1);
            return BillType;
        }
        public SelectList DropDownList_MAIL_TYPE()
        {
            SelectList BillType = new SelectList(
              new List<SelectListItem>
              {
                new SelectListItem { Selected = true, Text = "Work Worder(WORK_ORDR)", Value ="WORK_ORDR"},
                new SelectListItem { Selected = false, Text = "Wh Unit Expenes Consumption(WHEC_SMRY)", Value = "WHEC_SMRY"},
              }, "Value", "Text", 1);
            return BillType;
        }
        public SelectList DropDownList_MAIL_INBX()
        {
            SelectList BillType = new SelectList(
              new List<SelectListItem>
              {
                new SelectListItem { Selected = true, Text = "TO", Value ="TO"},
                new SelectListItem { Selected = false, Text = "CC", Value = "CC"},
              }, "Value", "Text", 1);
            return BillType;
        }
        public SelectList DropDownList_ORDR_STATUS()
        {
            SelectList StatusType = new SelectList(
              new List<SelectListItem>
              {
                new SelectListItem { Selected = false, Text = "Pending", Value ="P"},
                new SelectListItem { Selected = true, Text = "Working", Value = "W"},
                new SelectListItem { Selected = false, Text = "Delivered", Value = "D"},
                new SelectListItem { Selected = false, Text = "Returned", Value = "R"},
                new SelectListItem { Selected = false, Text = "Cancelled", Value = "C"},
              }, "Value", "Text", 1);
            return StatusType;
        }
        public SelectList DropDownList_T_ORDR_FOR_BILL(string ORDR_FMWH, string ORDR_TOWH)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                return new SelectList(db.T_ORDR.Where(t => t.ORDR_FMWH == ORDR_FMWH
                && t.ORDR_TOWH == ORDR_TOWH
                && t.ORDR_SBMT == "Y"
                && t.ORDR_STAS == "D"
                && t.ACT == 0).ToList(), "ORDR_TEXT", "ORDR_TEXT");
            }
        }
        public SelectList DropDownList_T_ORDR_BILL_RATE_PROCCESS(string ORDR_FMWH, string ORDR_TOWH, DateTime fromDate, DateTime toDate, int State)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                if (State == 1) //pending,9 is also pending force with CO
                {
                    var ordr = db.T_ORDR.Where(t => t.ORDR_FMWH == ORDR_FMWH
                      && t.ORDR_TOWH == ORDR_TOWH
                      && t.ORDR_DATE >= fromDate && t.ORDR_DATE <= toDate
                      && t.ORDR_SBMT == "Y"
                      && t.ORDR_STAS == "D"
                      && (t.ACT == State || t.ACT == 9)).ToList();
                    return new SelectList(ordr, "ORDR_TEXT", "ORDR_TEXT");
                }
                else //completed
                {
                    var ordr = db.T_ORDR.Where(t => t.ORDR_FMWH == ORDR_FMWH
                && t.ORDR_TOWH == ORDR_TOWH
                && t.ORDR_DATE >= fromDate && t.ORDR_DATE <= toDate
                && t.ORDR_SBMT == "Y"
                && t.ORDR_STAS == "D"
                && t.ACT == State).ToList();
                    return new SelectList(ordr, "ORDR_TEXT", "ORDR_TEXT");
                }

            }
        }
        public SelectList DropDownList_T_MCLT(string MCTP_TEXT, string MCLT_FMWH)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                if (MCTP_TEXT == "1111") //for man power combine staff id and name
                {
                    IEnumerable<SelectListItem> _manList = from s in db.T_MCLT.Where(t => t.MCLT_MCTP == MCTP_TEXT && t.MCLT_FMWH == MCLT_FMWH && t.ACT == 1).ToList()
                                                           select new SelectListItem
                                                           {
                                                               Value = s.MCLT_TEXT,
                                                               Text = "(" + s.MCLT_NAME + ") " + s.MCLT_DESC
                                                           };
                    return new SelectList(_manList, "Value", "Text");
                }
                else
                {
                    return new SelectList(db.T_MCLT.Where(t => t.MCLT_MCTP == MCTP_TEXT && t.MCLT_FMWH == MCLT_FMWH && t.ACT == 1).ToList(), "MCLT_TEXT", "MCLT_NAME");
                }
            }
        }
        public T_AMSU T_MOLD_MOLD_UNIT(string MOLD_TEXT)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string OID = db.T_MOLD.Where(m => m.MOLD_TEXT == MOLD_TEXT).FirstOrDefault().MOLD_AMSU;
                T_AMSU AMSU = new T_AMSU();
                var t_amsu = db.T_AMSU.Find(OID);
                if (t_amsu == null)
                    return null;
                AMSU.OID = t_amsu.OID;
                AMSU.AMSU_TEXT = t_amsu.AMSU_TEXT;
                AMSU.AMSU_NAME = t_amsu.AMSU_NAME;
                return AMSU;
            }
        }
        public string Order_Header_Data(string ORDR_TEXT)
        {
            string Sql = string.Format(@"select ('OT#'||o.ordr_text||', Date#'|| o.ordr_date ||', Raiser#'|| o.ordr_risr||', Work#'|| m.mold_name||', Problem#'|| o.ordr_prlm||', Note#'||
                o.ordr_note ||', DNote#'|| o.ordr_cmnt)detail
                from auction.t_ordr o join auction.t_mold m on o.ordr_item = m.mold_text
                where o.ordr_text='{0}'", ORDR_TEXT);
            string result = string.Empty;
            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.Database.SqlQuery<string>(sql: Sql).ToList();
                if (_data != null)
                {
                    result = _data.FirstOrDefault();
                    return result;
                }
                return result;
            }
        }
        public SelectList DropDownList_Month()
        {
            SelectList BillType = new SelectList(
              new List<SelectListItem>
              {
                new SelectListItem { Selected = true, Text = "Jan", Value ="01"},
                new SelectListItem { Selected = false, Text = "Feb", Value = "02"},
                new SelectListItem { Selected = false, Text = "Mar", Value = "03"},
                new SelectListItem { Selected = false, Text = "Apr", Value = "04"},
                new SelectListItem { Selected = false, Text = "May", Value = "05"},
                new SelectListItem { Selected = false, Text = "Jun", Value = "06"},
                new SelectListItem { Selected = false, Text = "Jul", Value = "07"},
                new SelectListItem { Selected = false, Text = "Aug", Value = "08"},
                new SelectListItem { Selected = false, Text = "Sep", Value = "09"},
                new SelectListItem { Selected = false, Text = "Oct", Value = "10"},
                new SelectListItem { Selected = false, Text = "Nov", Value = "11"},
                new SelectListItem { Selected = false, Text = "Dec", Value = "12"},
              }, "Value", "Text", 1);
            return BillType;
        }
        public SelectList DropDownList_Year()
        {
            int dt = Convert.ToInt32(DateTime.Now.Year) - 2;
            List<SelectListItem> yearlist = new List<SelectListItem>();
            for (int i = dt + 2; i >= dt; i--)
            {
                yearlist.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            return new SelectList(yearlist, "Value", "Text", 1);
        }
        public SelectList DropDownList_BillCycle()
        {
            SelectList BillType = new SelectList(
              new List<SelectListItem>
              {
                new SelectListItem { Selected = true, Text = "26-25", Value ="26-25"},
              }, "Value", "Text", 1);
            return BillType;
        }

        public SelectList DropDownList_T_CLST()
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"select clst_text,clst_name from auction.t_clst where act='1'");
                List<T_CLST> amsp = db.Database.SqlQuery<T_CLST>(sql: Sql).ToList();
                return new SelectList(amsp, "CLST_TEXT", "CLST_NAME");
            }
        }
        public SelectList DropDownList_CLSM_CLSD_TYPE()
        {
            SelectList StatusType = new SelectList(
              new List<SelectListItem>
              {
                new SelectListItem { Selected = false, Text = "Apply All", Value ="AL"},
                new SelectListItem { Selected = true, Text = "Exception", Value = "EX"},
              }, "Value", "Text", 1);
            return StatusType;
        }

    }

}