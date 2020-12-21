using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Dal
{
    public class Expenses_DAL
    {
        public bool SaveExpenses(T_MCLT mclt)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            using (auctionDbContext db = new auctionDbContext())
            {
                try
                {
                    mclt.ACT = 1;
                    mclt.CDT = DateTime.Now;
                    mclt.CDU =Usr;
                    mclt.VAR = 1;
                    db.T_MCLT.Add(mclt);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception Ex)
                {
                    return false;
                }
            }
        }

        public List<V_MCLT_MANPOWER> ManPowerList(string MCLT_FMWH, string MCLT_TEXT)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"select m.mclt_text, m.mclt_name, m.mclt_desc, t.mctp_name, w.amsp_name,
                    m.mclt_year, m.mclt_prce, m.mclt_cost, m.act
                    from auction.t_mclt m join auction.t_mctp t on m.mclt_mctp = t.mctp_text
                    join auction.t_amsp w on m.mclt_fmwh = w.oid
                    where w.oid='{0}' and m.mclt_name='{1}' and m.mclt_mctp'1111'", MCLT_FMWH, MCLT_TEXT);
                var _data = db.Database.SqlQuery<V_MCLT_MANPOWER>(sql: Sql).ToList();
                return _data;
            }
        }
    }
}