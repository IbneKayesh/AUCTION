using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Dal
{
    public class Mold_DAL
    {
        public T_MOLD _CreateLike(string _Data)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                T_MOLD _Like = db.T_MOLD.Find(_Data);
                if (_Like != null)
                {
                    return _Like;
                }
                return null;
            }
        }

        public bool CreateLikeMoldDelivery(T_MOLD m)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            bool result = false;
            using (auctionDbContext db = new auctionDbContext())
            {
                T_MOLD _m = new T_MOLD();
                _m.MOLD_TEXT = m.MOLD_TEXT;
                _m.MOLD_NAME = m.MOLD_NAME;
                _m.MOLD_AMSU = m.MOLD_AMSU;
                _m.MOLD_AMSP = m.MOLD_AMSP;
                _m.MOLD_TAAG = m.MOLD_TAAG;
                _m.MOLD_DESC = m.MOLD_DESC;
                _m.MOLD_CVTY = m.MOLD_CVTY;
                _m.MOLD_WTRL = m.MOLD_WTRL;
                _m.MOLD_HYDL = m.MOLD_HYDL;
                _m.MOLD_SIZE = m.MOLD_SIZE;
                _m.MOLD_MFGD = DateTime.Now.Date;
                _m.MOLD_STUS = m.MOLD_STUS;
                _m.MOLD_SEQN = m.MOLD_SEQN;
                _m.ACT = 1;
                _m.CDT = DateTime.Now;
                _m.CDU =Usr;
                _m.VAR = 1;
                T_ORDR O = db.T_ORDR.Find(m.CDU); //cdu contain order number
                if (O != null)
                {
                    O.ORDR_STAS = "D";
                    O.ORDR_CMNT = m.UDU; //udu contain order comment
                    O.ORDR_DDAT = DateTime.Now;
                    O.UDT = DateTime.Now;
                    O.UDU = Usr;
                    O.VAR = O.VAR + 1;
                    db.Entry(O).State = System.Data.Entity.EntityState.Modified;
                    db.T_MOLD.Add(_m);
                    db.SaveChanges();
                    return true;
                }
            }
            return result;
        }
    }
}