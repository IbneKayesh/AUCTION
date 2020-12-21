using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Dal
{
    public class WorkOrder_DAL
    {
        Common_DAL _c = new Common_DAL();
        //by WH/CO-> OT, by WH/CO-> ON, by Date/WH/CO-> Work Type (WT),  by Date/WH/CO-> Status (ST)
        public List<V_ORDR> OrderList(v_ORDR_QUERY _Q)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                if (_Q.QUERY_NAME == "OT")
                {
                    #region OT
                    var _data = (from o in db.T_ORDR.Where(o => o.ORDR_TEXT == _Q.ORDR_TEXT
                                         && o.ORDR_TOWH == _Q.ORDR_TOWH && o.ORDR_FMWH == _Q.ORDR_FMWH)
                                 join m in db.T_MOLD on o.ORDR_ITEM equals m.MOLD_TEXT
                                 join u in db.T_AMSU on o.ORDR_UNIT equals u.OID
                                 join f in db.T_AMSP on o.ORDR_FMWH equals f.OID
                                 join t in db.T_AMSP on o.ORDR_TOWH equals t.OID
                                 join y in db.T_ORTP on o.ORDR_ORTP equals y.ORTP_TEXT
                                 select new V_ORDR
                                 {
                                     ORDR_TEXT = o.ORDR_TEXT,
                                     ORDR_NAME = o.ORDR_NAME,
                                     ORDR_FMWH = f.AMSP_NAME,
                                     ORDR_TOWH = t.AMSP_NAME,
                                     ORDR_ORTP = y.ORTP_NAME,
                                     ORDR_RISR = o.ORDR_RISR,
                                     ORDR_DATE = o.ORDR_DATE,
                                     ORDR_RDAT = o.ORDR_RDAT,
                                     ORDR_DDAT = o.ORDR_DDAT,
                                     ORDR_STAS = o.ORDR_STAS,
                                     ORDR_ITEM = m.MOLD_NAME,
                                     ORDR_UNIT = u.AMSU_NAME,
                                     ORDR_RATE = o.ORDR_RATE,
                                     ORDR_QNTY = o.ORDR_QNTY,
                                     ORDR_PRLM = o.ORDR_PRLM,
                                     ORDR_NOTE = o.ORDR_NOTE,
                                     ORDR_CMNT = o.ORDR_CMNT,
                                     ORDR_SBMT = o.ORDR_SBMT,
                                     ORDR_SEQN = o.ORDR_SEQN,
                                     ACT = o.ACT,
                                     CDT = o.CDT,
                                     UDT = o.UDT,
                                     CDU = o.CDU,
                                     UDU = o.UDU,
                                     VAR = o.VAR
                                 }).ToList();
                    return _data;
                    #endregion
                }
                else if (_Q.QUERY_NAME == "ON")
                {
                    #region ON
                    var _data = (from o in db.T_ORDR.Where(o => o.ORDR_NAME == _Q.ORDR_NAME
                                      && o.ORDR_TOWH == _Q.ORDR_TOWH && o.ORDR_FMWH == _Q.ORDR_FMWH)
                                 join m in db.T_MOLD on o.ORDR_ITEM equals m.MOLD_TEXT
                                 join u in db.T_AMSU on o.ORDR_UNIT equals u.OID
                                 join f in db.T_AMSP on o.ORDR_FMWH equals f.OID
                                 join t in db.T_AMSP on o.ORDR_TOWH equals t.OID
                                 join y in db.T_ORTP on o.ORDR_ORTP equals y.ORTP_TEXT
                                 select new V_ORDR
                                 {
                                     ORDR_TEXT = o.ORDR_TEXT,
                                     ORDR_NAME = o.ORDR_NAME,
                                     ORDR_FMWH = f.AMSP_NAME,
                                     ORDR_TOWH = t.AMSP_NAME,
                                     ORDR_ORTP = y.ORTP_NAME,
                                     ORDR_RISR = o.ORDR_RISR,
                                     ORDR_DATE = o.ORDR_DATE,
                                     ORDR_RDAT = o.ORDR_RDAT,
                                     ORDR_DDAT = o.ORDR_DDAT,
                                     ORDR_STAS = o.ORDR_STAS,
                                     ORDR_ITEM = m.MOLD_NAME,
                                     ORDR_UNIT = u.AMSU_NAME,
                                     ORDR_RATE = o.ORDR_RATE,
                                     ORDR_QNTY = o.ORDR_QNTY,
                                     ORDR_PRLM = o.ORDR_PRLM,
                                     ORDR_NOTE = o.ORDR_NOTE,
                                     ORDR_CMNT = o.ORDR_CMNT,
                                     ORDR_SBMT = o.ORDR_SBMT,
                                     ORDR_SEQN = o.ORDR_SEQN,
                                     ACT = o.ACT,
                                     CDT = o.CDT,
                                     UDT = o.UDT,
                                     CDU = o.CDU,
                                     UDU = o.UDU,
                                     VAR = o.VAR
                                 }).ToList();
                    return _data;
                    #endregion
                }
                else if (_Q.QUERY_NAME == "WT")
                {
                    #region WT
                    var _data = (from o in db.T_ORDR.Where(o => o.ORDR_DATE >= _Q.fromDate && o.ORDR_DATE <= _Q.toDate
                       && o.ORDR_TOWH == _Q.ORDR_TOWH && o.ORDR_FMWH == _Q.ORDR_FMWH
                       && o.ORDR_ORTP == _Q.ORDR_ORTP)
                                 join m in db.T_MOLD on o.ORDR_ITEM equals m.MOLD_TEXT
                                 join u in db.T_AMSU on o.ORDR_UNIT equals u.OID
                                 join f in db.T_AMSP on o.ORDR_FMWH equals f.OID
                                 join t in db.T_AMSP on o.ORDR_TOWH equals t.OID
                                 join y in db.T_ORTP on o.ORDR_ORTP equals y.ORTP_TEXT
                                 select new V_ORDR
                                 {
                                     ORDR_TEXT = o.ORDR_TEXT,
                                     ORDR_NAME = o.ORDR_NAME,
                                     ORDR_FMWH = f.AMSP_NAME,
                                     ORDR_TOWH = t.AMSP_NAME,
                                     ORDR_ORTP = y.ORTP_NAME,
                                     ORDR_RISR = o.ORDR_RISR,
                                     ORDR_DATE = o.ORDR_DATE,
                                     ORDR_RDAT = o.ORDR_RDAT,
                                     ORDR_DDAT = o.ORDR_DDAT,
                                     ORDR_STAS = o.ORDR_STAS,
                                     ORDR_ITEM = m.MOLD_NAME,
                                     ORDR_UNIT = u.AMSU_NAME,
                                     ORDR_RATE = o.ORDR_RATE,
                                     ORDR_QNTY = o.ORDR_QNTY,
                                     ORDR_PRLM = o.ORDR_PRLM,
                                     ORDR_NOTE = o.ORDR_NOTE,
                                     ORDR_CMNT = o.ORDR_CMNT,
                                     ORDR_SBMT = o.ORDR_SBMT,
                                     ORDR_SEQN = o.ORDR_SEQN,
                                     ACT = o.ACT,
                                     CDT = o.CDT,
                                     UDT = o.UDT,
                                     CDU = o.CDU,
                                     UDU = o.UDU,
                                     VAR = o.VAR
                                 }).ToList();
                    return _data;
                    #endregion
                }
                else if (_Q.QUERY_NAME == "ST")
                {
                    #region ST
                    var _data = (from o in db.T_ORDR.Where(o => o.ORDR_DATE >= _Q.fromDate && o.ORDR_DATE <= _Q.toDate
                     && o.ORDR_TOWH == _Q.ORDR_TOWH && o.ORDR_FMWH == _Q.ORDR_FMWH
                     && o.ORDR_STAS == _Q.ORDR_STAS)
                                 join m in db.T_MOLD on o.ORDR_ITEM equals m.MOLD_TEXT
                                 join u in db.T_AMSU on o.ORDR_UNIT equals u.OID
                                 join f in db.T_AMSP on o.ORDR_FMWH equals f.OID
                                 join t in db.T_AMSP on o.ORDR_TOWH equals t.OID
                                 join y in db.T_ORTP on o.ORDR_ORTP equals y.ORTP_TEXT
                                 select new V_ORDR
                                 {
                                     ORDR_TEXT = o.ORDR_TEXT,
                                     ORDR_NAME = o.ORDR_NAME,
                                     ORDR_FMWH = f.AMSP_NAME,
                                     ORDR_TOWH = t.AMSP_NAME,
                                     ORDR_ORTP = y.ORTP_NAME,
                                     ORDR_RISR = o.ORDR_RISR,
                                     ORDR_DATE = o.ORDR_DATE,
                                     ORDR_RDAT = o.ORDR_RDAT,
                                     ORDR_DDAT = o.ORDR_DDAT,
                                     ORDR_STAS = o.ORDR_STAS,
                                     ORDR_ITEM = m.MOLD_NAME,
                                     ORDR_UNIT = u.AMSU_NAME,
                                     ORDR_RATE = o.ORDR_RATE,
                                     ORDR_QNTY = o.ORDR_QNTY,
                                     ORDR_PRLM = o.ORDR_PRLM,
                                     ORDR_NOTE = o.ORDR_NOTE,
                                     ORDR_CMNT = o.ORDR_CMNT,
                                     ORDR_SBMT = o.ORDR_SBMT,
                                     ORDR_SEQN = o.ORDR_SEQN,
                                     ACT = o.ACT,
                                     CDT = o.CDT,
                                     UDT = o.UDT,
                                     CDU = o.CDU,
                                     UDU = o.UDU,
                                     VAR = o.VAR
                                 }).ToList();
                    return _data;
                    #endregion
                }
                else
                {
                    return null;
                }
            }


            //using (auctionDbContext db = new auctionDbContext())
            //{
            //    try
            //    {
            //        var _data = db.Database.SqlQuery<T_ORDR>(sql: Sql).ToList();
            //        return _data;
            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }
            //}
        }
        public bool SaveOrderModel(List<T_ORDR> o_list)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            bool result = true;
            foreach (var o in o_list)
            {
                using (auctionDbContext db = new auctionDbContext())
                {
                    try
                    {
                        o.CDT = DateTime.Now;
                        o.VAR = 1;
                        o.CDU = Usr;
                        db.T_ORDR.Add(o);
                        db.SaveChanges();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return result;
        }

        public v_ordr_edit _edit(string ORDR_TEXT)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = (from o in db.T_ORDR.Where(o => o.ORDR_TEXT == ORDR_TEXT && o.ORDR_SBMT == "N" && o.ORDR_STAS == "P" && o.ACT == 0)
                             join m in db.T_MOLD on o.ORDR_ITEM equals m.MOLD_TEXT
                             select new v_ordr_edit
                             {
                                 ORDR_TEXT = o.ORDR_TEXT,
                                 ORDR_ITEM = m.MOLD_NAME,
                                 ORDR_ORTP = o.ORDR_ORTP,
                                 ORDR_RISR = o.ORDR_RISR,
                                 ORDR_PRLM = o.ORDR_PRLM,
                                 ORDR_NOTE = o.ORDR_NOTE
                             }).FirstOrDefault();
                if (_data != null)
                {
                    return _data;
                }
            }
            return null;
        }
        public bool _editSave(v_ordr_edit _edit)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            bool result = false;
            using (auctionDbContext db = new auctionDbContext())
            {
                T_ORDR O = db.T_ORDR.Find(_edit.ORDR_TEXT);
                if (O != null)
                {
                    O.ORDR_TOWH = _edit.ORDR_TOWH;
                    O.ORDR_ORTP = _edit.ORDR_ORTP;
                    O.ORDR_RISR = _edit.ORDR_RISR;
                    O.ORDR_PRLM = _edit.ORDR_PRLM;
                    O.ORDR_NOTE = _edit.ORDR_NOTE;
                    O.CDU = Usr;
                    O.UDT = DateTime.Now;
                    O.VAR = O.VAR + 1;
                    db.Entry(O).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            return result;
        }

        public bool OrderSubmit(string ORDR_TEXT)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            bool result = false;
            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.T_ORDR.Where(x => x.ORDR_TEXT == ORDR_TEXT && x.ORDR_STAS == "P" && x.ORDR_SBMT == "N" && x.ACT == 0).FirstOrDefault();
                if (_data != null)
                {
                    _data.ORDR_SBMT = "Y";
                    _data.UDT = DateTime.Now;
                    _data.UDU = Usr;
                    _data.VAR = _data.VAR + 1;
                    db.Entry(_data).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            return result;
        }

        //by WH/CO-> OT, by WH/CO-> ON, by Date/WH/CO-> Work Type (WT),  by Date/WH/CO-> Status (ST)
        public List<V_ORDR> Order_List_Tr(v_ORDR_QUERY _Q)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                if (_Q.QUERY_NAME == "OT")
                {
                    #region OT
                    var _data = (from o in db.T_ORDR.Where(o => o.ORDR_TEXT == _Q.ORDR_TEXT && o.ORDR_SBMT == "Y"
                                         && o.ORDR_TOWH == _Q.ORDR_TOWH && o.ORDR_FMWH == _Q.ORDR_FMWH)
                                 join m in db.T_MOLD on o.ORDR_ITEM equals m.MOLD_TEXT
                                 join u in db.T_AMSU on o.ORDR_UNIT equals u.OID
                                 join f in db.T_AMSP on o.ORDR_FMWH equals f.OID
                                 join t in db.T_AMSP on o.ORDR_TOWH equals t.OID
                                 join y in db.T_ORTP on o.ORDR_ORTP equals y.ORTP_TEXT
                                 select new V_ORDR
                                 {
                                     ORDR_TEXT = o.ORDR_TEXT,
                                     ORDR_NAME = o.ORDR_NAME,
                                     ORDR_FMWH = f.AMSP_NAME,
                                     ORDR_TOWH = t.AMSP_NAME,
                                     ORDR_ORTP = y.ORTP_NAME,
                                     ORDR_RISR = o.ORDR_RISR,
                                     ORDR_DATE = o.ORDR_DATE,
                                     ORDR_RDAT = o.ORDR_RDAT,
                                     ORDR_DDAT = o.ORDR_DDAT,
                                     ORDR_STAS = o.ORDR_STAS,
                                     ORDR_ITEM = m.MOLD_NAME,
                                     ORDR_UNIT = u.AMSU_NAME,
                                     ORDR_RATE = o.ORDR_RATE,
                                     ORDR_QNTY = o.ORDR_QNTY,
                                     ORDR_PRLM = o.ORDR_PRLM,
                                     ORDR_NOTE = o.ORDR_NOTE,
                                     ORDR_CMNT = o.ORDR_CMNT,
                                     ORDR_SBMT = o.ORDR_SBMT,
                                     ORDR_SEQN = o.ORDR_SEQN,
                                     ACT = o.ACT,
                                     CDT = o.CDT,
                                     UDT = o.UDT,
                                     CDU = o.CDU,
                                     UDU = o.UDU,
                                     VAR = o.VAR
                                 }).ToList();
                    return _data;
                    #endregion

                }
                else if (_Q.QUERY_NAME == "ON")
                {
                    #region ON
                    var _data = (from o in db.T_ORDR.Where(o => o.ORDR_NAME == _Q.ORDR_NAME && o.ORDR_SBMT == "Y"
                                      && o.ORDR_TOWH == _Q.ORDR_TOWH && o.ORDR_FMWH == _Q.ORDR_FMWH)
                                 join m in db.T_MOLD on o.ORDR_ITEM equals m.MOLD_TEXT
                                 join u in db.T_AMSU on o.ORDR_UNIT equals u.OID
                                 join f in db.T_AMSP on o.ORDR_FMWH equals f.OID
                                 join t in db.T_AMSP on o.ORDR_TOWH equals t.OID
                                 join y in db.T_ORTP on o.ORDR_ORTP equals y.ORTP_TEXT
                                 select new V_ORDR
                                 {
                                     ORDR_TEXT = o.ORDR_TEXT,
                                     ORDR_NAME = o.ORDR_NAME,
                                     ORDR_FMWH = f.AMSP_NAME,
                                     ORDR_TOWH = t.AMSP_NAME,
                                     ORDR_ORTP = y.ORTP_NAME,
                                     ORDR_RISR = o.ORDR_RISR,
                                     ORDR_DATE = o.ORDR_DATE,
                                     ORDR_RDAT = o.ORDR_RDAT,
                                     ORDR_DDAT = o.ORDR_DDAT,
                                     ORDR_STAS = o.ORDR_STAS,
                                     ORDR_ITEM = m.MOLD_NAME,
                                     ORDR_UNIT = u.AMSU_NAME,
                                     ORDR_RATE = o.ORDR_RATE,
                                     ORDR_QNTY = o.ORDR_QNTY,
                                     ORDR_PRLM = o.ORDR_PRLM,
                                     ORDR_NOTE = o.ORDR_NOTE,
                                     ORDR_CMNT = o.ORDR_CMNT,
                                     ORDR_SBMT = o.ORDR_SBMT,
                                     ORDR_SEQN = o.ORDR_SEQN,
                                     ACT = o.ACT,
                                     CDT = o.CDT,
                                     UDT = o.UDT,
                                     CDU = o.CDU,
                                     UDU = o.UDU,
                                     VAR = o.VAR
                                 }).ToList();
                    return _data;
                    #endregion
                }
                else if (_Q.QUERY_NAME == "WT")
                {
                    if (string.IsNullOrWhiteSpace(_Q.ORDR_TOWH))
                    {
                        var all_CO = _c.DropDownList_T_AMSP_CO();
                        List<string> all_CO_ID = new List<string>();
                        foreach (var item in all_CO)
                        {
                            all_CO_ID.Add(item.Value);
                        }
                        #region WT_ALL_PERMITTED_CO
                        var _data = (from o in db.T_ORDR.Where(o => o.ORDR_DATE >= _Q.fromDate && o.ORDR_DATE <= _Q.toDate
                       && o.ORDR_FMWH == _Q.ORDR_FMWH && all_CO_ID.Contains(o.ORDR_TOWH) //&& o.ORDR_TOWH == _Q.ORDR_TOWH
                       && o.ORDR_ORTP == _Q.ORDR_ORTP && o.ORDR_SBMT == "Y")
                                     join m in db.T_MOLD on o.ORDR_ITEM equals m.MOLD_TEXT
                                     join u in db.T_AMSU on o.ORDR_UNIT equals u.OID
                                     join f in db.T_AMSP on o.ORDR_FMWH equals f.OID
                                     join t in db.T_AMSP on o.ORDR_TOWH equals t.OID
                                     join y in db.T_ORTP on o.ORDR_ORTP equals y.ORTP_TEXT
                                     select new V_ORDR
                                     {
                                         ORDR_TEXT = o.ORDR_TEXT,
                                         ORDR_NAME = o.ORDR_NAME,
                                         ORDR_FMWH = f.AMSP_NAME,
                                         ORDR_TOWH = t.AMSP_NAME,
                                         ORDR_ORTP = y.ORTP_NAME,
                                         ORDR_RISR = o.ORDR_RISR,
                                         ORDR_DATE = o.ORDR_DATE,
                                         ORDR_RDAT = o.ORDR_RDAT,
                                         ORDR_DDAT = o.ORDR_DDAT,
                                         ORDR_STAS = o.ORDR_STAS,
                                         ORDR_ITEM = m.MOLD_NAME,
                                         ORDR_UNIT = u.AMSU_NAME,
                                         ORDR_RATE = o.ORDR_RATE,
                                         ORDR_QNTY = o.ORDR_QNTY,
                                         ORDR_PRLM = o.ORDR_PRLM,
                                         ORDR_NOTE = o.ORDR_NOTE,
                                         ORDR_CMNT = o.ORDR_CMNT,
                                         ORDR_SBMT = o.ORDR_SBMT,
                                         ORDR_SEQN = o.ORDR_SEQN,
                                         ACT = o.ACT,
                                         CDT = o.CDT,
                                         UDT = o.UDT,
                                         CDU = o.CDU,
                                         UDU = o.UDU,
                                         VAR = o.VAR
                                     }).ToList();
                        return _data;
                        #endregion
                    }
                    else
                    {
                        #region WT_SELECTED
                        var _data = (from o in db.T_ORDR.Where(o => o.ORDR_DATE >= _Q.fromDate && o.ORDR_DATE <= _Q.toDate
                       && o.ORDR_TOWH == _Q.ORDR_TOWH && o.ORDR_FMWH == _Q.ORDR_FMWH
                       && o.ORDR_ORTP == _Q.ORDR_ORTP && o.ORDR_SBMT == "Y")
                                     join m in db.T_MOLD on o.ORDR_ITEM equals m.MOLD_TEXT
                                     join u in db.T_AMSU on o.ORDR_UNIT equals u.OID
                                     join f in db.T_AMSP on o.ORDR_FMWH equals f.OID
                                     join t in db.T_AMSP on o.ORDR_TOWH equals t.OID
                                     join y in db.T_ORTP on o.ORDR_ORTP equals y.ORTP_TEXT
                                     select new V_ORDR
                                     {
                                         ORDR_TEXT = o.ORDR_TEXT,
                                         ORDR_NAME = o.ORDR_NAME,
                                         ORDR_FMWH = f.AMSP_NAME,
                                         ORDR_TOWH = t.AMSP_NAME,
                                         ORDR_ORTP = y.ORTP_NAME,
                                         ORDR_RISR = o.ORDR_RISR,
                                         ORDR_DATE = o.ORDR_DATE,
                                         ORDR_RDAT = o.ORDR_RDAT,
                                         ORDR_DDAT = o.ORDR_DDAT,
                                         ORDR_STAS = o.ORDR_STAS,
                                         ORDR_ITEM = m.MOLD_NAME,
                                         ORDR_UNIT = u.AMSU_NAME,
                                         ORDR_RATE = o.ORDR_RATE,
                                         ORDR_QNTY = o.ORDR_QNTY,
                                         ORDR_PRLM = o.ORDR_PRLM,
                                         ORDR_NOTE = o.ORDR_NOTE,
                                         ORDR_CMNT = o.ORDR_CMNT,
                                         ORDR_SBMT = o.ORDR_SBMT,
                                         ORDR_SEQN = o.ORDR_SEQN,
                                         ACT = o.ACT,
                                         CDT = o.CDT,
                                         UDT = o.UDT,
                                         CDU = o.CDU,
                                         UDU = o.UDU,
                                         VAR = o.VAR
                                     }).ToList();
                        return _data;
                        #endregion
                    }
                }
                else if (_Q.QUERY_NAME == "ST")
                {
                    if (string.IsNullOrWhiteSpace(_Q.ORDR_TOWH))
                    {
                        var all_CO = _c.DropDownList_T_AMSP_CO();
                       // List<V_ASMP> all_CO_V = new List<V_ASMP>();
                        List<string> all_CO_ID = new List<string>();
                        foreach (var item in all_CO)
                        {
                            all_CO_ID.Add(item.Value);
                        }
                        #region ST_ALL_PERMITTED_CO
                        var _data = (from o in db.T_ORDR.Where(o => o.ORDR_DATE >= _Q.fromDate && o.ORDR_DATE <= _Q.toDate
                         && o.ORDR_FMWH == _Q.ORDR_FMWH && all_CO_ID.Contains(o.ORDR_TOWH)
                         && o.ORDR_STAS == _Q.ORDR_STAS && o.ORDR_SBMT == "Y")
                                     join m in db.T_MOLD on o.ORDR_ITEM equals m.MOLD_TEXT
                                     join u in db.T_AMSU on o.ORDR_UNIT equals u.OID
                                     join f in db.T_AMSP on o.ORDR_FMWH equals f.OID
                                     join t in db.T_AMSP on o.ORDR_TOWH equals t.OID
                                     join y in db.T_ORTP on o.ORDR_ORTP equals y.ORTP_TEXT
                                     select new V_ORDR
                                     {
                                         ORDR_TEXT = o.ORDR_TEXT,
                                         ORDR_NAME = o.ORDR_NAME,
                                         ORDR_FMWH = f.AMSP_NAME,
                                         ORDR_TOWH = t.AMSP_NAME,
                                         ORDR_ORTP = y.ORTP_NAME,
                                         ORDR_RISR = o.ORDR_RISR,
                                         ORDR_DATE = o.ORDR_DATE,
                                         ORDR_RDAT = o.ORDR_RDAT,
                                         ORDR_DDAT = o.ORDR_DDAT,
                                         ORDR_STAS = o.ORDR_STAS,
                                         ORDR_ITEM = m.MOLD_NAME,
                                         ORDR_UNIT = u.AMSU_NAME,
                                         ORDR_RATE = o.ORDR_RATE,
                                         ORDR_QNTY = o.ORDR_QNTY,
                                         ORDR_PRLM = o.ORDR_PRLM,
                                         ORDR_NOTE = o.ORDR_NOTE,
                                         ORDR_CMNT = o.ORDR_CMNT,
                                         ORDR_SBMT = o.ORDR_SBMT,
                                         ORDR_SEQN = o.ORDR_SEQN,
                                         ACT = o.ACT,
                                         CDT = o.CDT,
                                         UDT = o.UDT,
                                         CDU = o.CDU,
                                         UDU = o.UDU,
                                         VAR = o.VAR
                                     }).ToList();
                        return _data;
                        #endregion
                    }
                    else
                    {
                        #region ST_SELECTED
                        var _data = (from o in db.T_ORDR.Where(o => o.ORDR_DATE >= _Q.fromDate && o.ORDR_DATE <= _Q.toDate
                         && o.ORDR_TOWH == _Q.ORDR_TOWH && o.ORDR_FMWH == _Q.ORDR_FMWH
                         && o.ORDR_STAS == _Q.ORDR_STAS && o.ORDR_SBMT == "Y")
                                     join m in db.T_MOLD on o.ORDR_ITEM equals m.MOLD_TEXT
                                     join u in db.T_AMSU on o.ORDR_UNIT equals u.OID
                                     join f in db.T_AMSP on o.ORDR_FMWH equals f.OID
                                     join t in db.T_AMSP on o.ORDR_TOWH equals t.OID
                                     join y in db.T_ORTP on o.ORDR_ORTP equals y.ORTP_TEXT
                                     select new V_ORDR
                                     {
                                         ORDR_TEXT = o.ORDR_TEXT,
                                         ORDR_NAME = o.ORDR_NAME,
                                         ORDR_FMWH = f.AMSP_NAME,
                                         ORDR_TOWH = t.AMSP_NAME,
                                         ORDR_ORTP = y.ORTP_NAME,
                                         ORDR_RISR = o.ORDR_RISR,
                                         ORDR_DATE = o.ORDR_DATE,
                                         ORDR_RDAT = o.ORDR_RDAT,
                                         ORDR_DDAT = o.ORDR_DDAT,
                                         ORDR_STAS = o.ORDR_STAS,
                                         ORDR_ITEM = m.MOLD_NAME,
                                         ORDR_UNIT = u.AMSU_NAME,
                                         ORDR_RATE = o.ORDR_RATE,
                                         ORDR_QNTY = o.ORDR_QNTY,
                                         ORDR_PRLM = o.ORDR_PRLM,
                                         ORDR_NOTE = o.ORDR_NOTE,
                                         ORDR_CMNT = o.ORDR_CMNT,
                                         ORDR_SBMT = o.ORDR_SBMT,
                                         ORDR_SEQN = o.ORDR_SEQN,
                                         ACT = o.ACT,
                                         CDT = o.CDT,
                                         UDT = o.UDT,
                                         CDU = o.CDU,
                                         UDU = o.UDU,
                                         VAR = o.VAR
                                     }).ToList();
                        return _data;
                        #endregion
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public v_ordr_received _receive(string ORDR_TEXT)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = (from o in db.T_ORDR.Where(o => o.ORDR_TEXT == ORDR_TEXT && o.ORDR_SBMT == "Y" && o.ORDR_STAS == "P" && o.ACT == 0)
                             select new v_ordr_received
                             {
                                 ORDR_TEXT = o.ORDR_TEXT,
                                 ORDR_CMNT = o.ORDR_CMNT
                             }).FirstOrDefault();
                if (_data != null)
                {
                    return _data;
                }
            }
            return null;
        }
        public bool __receiveSave(v_ordr_received _edit)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            bool result = false;
            using (auctionDbContext db = new auctionDbContext())
            {
                T_ORDR O = db.T_ORDR.Find(_edit.ORDR_TEXT);
                if (O != null && O.ORDR_STAS=="P")
                {
                    O.ORDR_STAS = "W";
                    O.ORDR_CMNT = _edit.ORDR_CMNT;
                    O.ORDR_RDAT = DateTime.Now;
                    O.UDT = DateTime.Now;
                    O.VAR = O.VAR + 1;
                    O.UDU = Usr;
                    db.Entry(O).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            return result;
        }


        public v_ordr_received _return(string ORDR_TEXT)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = (from o in db.T_ORDR.Where(o => o.ORDR_TEXT == ORDR_TEXT && o.ORDR_SBMT == "Y" && o.ORDR_STAS == "W" && o.ACT == 0)
                             select new v_ordr_received
                             {
                                 ORDR_TEXT = o.ORDR_TEXT,
                                 ORDR_CMNT = o.ORDR_CMNT
                             }).FirstOrDefault();
                if (_data != null)
                {
                    return _data;
                }
            }
            return null;
        }
        public bool _returnSave(v_ordr_received _edit)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            bool result = false;
            using (auctionDbContext db = new auctionDbContext())
            {
                T_ORDR O = db.T_ORDR.Find(_edit.ORDR_TEXT);
                if (O != null)
                {
                    O.ORDR_STAS = "R";
                    O.ORDR_CMNT = _edit.ORDR_CMNT;
                    O.ORDR_DDAT = DateTime.Now;
                    O.UDT = DateTime.Now;
                    O.VAR = O.VAR + 1;
                    O.UDU = Usr;
                    db.Entry(O).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            return result;
        }

        public string _DeliverySave(v_ordr_received _edit)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            using (auctionDbContext db = new auctionDbContext())
            {
                T_ORDR O = db.T_ORDR.Find(_edit.ORDR_TEXT);
                if (O != null && O.ORDR_STAS=="W")
                {
                    if (O.ORDR_ORTP == "1002") //new item 
                    {
                        return O.ORDR_ITEM;
                    }
                    else
                    {
                        O.ORDR_STAS = "D";
                        O.ORDR_CMNT = _edit.ORDR_CMNT;
                        O.ORDR_DDAT = DateTime.Now;
                        O.UDT = DateTime.Now;
                        O.VAR = O.VAR + 1;
                        O.UDU = Usr;
                        db.Entry(O).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return "1";
                    }
                }
            }
            return "0";
        }
    }
}
