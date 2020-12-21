using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace auction.Dal
{
    public class Admin_DAL
    {
        CommonData _c = new CommonData();
        public bool PRO_SMRY(R_EXPENSES R)
        {
            Oracle_Date od = _c.Oracle_Date_Format(R.FROM_DATE, R.TO_DATE);
            using (auctionDbContext db = new auctionDbContext())
            {
                string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
                //   var PARAM_A = new OracleParameter("USERID", OracleDbType.Varchar2, UserId, ParameterDirection.Input);
                //string Sql = string.Format(@"BEGIN auction.pro_smry(:USERID,:COUNIT); END;");
                //  var r = db.Database.ExecuteSqlCommand(Sql, Usr, R.ORDR_TOWH);
                bool result = false;
                string Sql = string.Format(@"declare
                            SMRY_NSMT_A NUMBER(10);
                            SMRY_NRCV_A NUMBER(10);
                            SMRY_IWRK_A NUMBER(10);
                            SMRY_NBIL_A NUMBER(10);
                            SMRY_NRTP_A NUMBER(10);
                            SMRY_CMPT_A NUMBER(10);
                            begin
                            SELECT COUNT(ORDR_TEXT) INTO SMRY_NSMT_A FROM auction.T_ORDR WHERE ORDR_STAS='P' AND ORDR_SBMT='N' AND ACT='0'
                            AND ORDR_TOWH='{0}' AND TO_DATE(UDT,'DD-MM-YY') BETWEEN TO_DATE('{1}','DD-MM-YYYY') AND TO_DATE('{2}','DD-MM-YYYY');
                            SELECT COUNT(ORDR_TEXT) INTO SMRY_NRCV_A FROM auction.T_ORDR WHERE ORDR_STAS='P' AND ORDR_SBMT='Y' AND ACT='0'
                            AND ORDR_TOWH='{3}' AND TO_DATE(UDT,'DD-MM-YY') BETWEEN TO_DATE('{4}','DD-MM-YYYY') AND TO_DATE('{5}','DD-MM-YYYY');
                            SELECT COUNT(ORDR_TEXT) INTO SMRY_IWRK_A FROM auction.T_ORDR WHERE ORDR_STAS='W' AND ORDR_SBMT='Y' AND ACT='0'
                            AND ORDR_TOWH='{6}' AND TO_DATE(UDT,'DD-MM-YY') BETWEEN TO_DATE('{7}','DD-MM-YYYY') AND TO_DATE('{8}','DD-MM-YYYY');
                            SELECT COUNT(ORDR_TEXT) INTO SMRY_NBIL_A FROM auction.T_ORDR WHERE ORDR_STAS='D' AND ORDR_SBMT='Y' AND ACT='0' 
                            AND ORDR_TOWH='{9}' AND TO_DATE(UDT,'DD-MM-YY') BETWEEN TO_DATE('{10}','DD-MM-YYYY') AND TO_DATE('{11}','DD-MM-YYYY');
                            SELECT COUNT(ORDR_TEXT) INTO SMRY_NRTP_A FROM auction.T_ORDR WHERE ORDR_STAS='D' AND ORDR_SBMT='Y' AND ACT='1'
                            AND ORDR_TOWH='{12}' AND TO_DATE(UDT,'DD-MM-YY') BETWEEN TO_DATE('{13}','DD-MM-YYYY') AND TO_DATE('{14}','DD-MM-YYYY');
                            SELECT COUNT(ORDR_TEXT) INTO SMRY_CMPT_A FROM auction.T_ORDR WHERE ORDR_STAS='D' AND ORDR_SBMT='Y' AND ACT='2' 
                            AND ORDR_TOWH='{15}' AND TO_DATE(UDT,'DD-MM-YY') BETWEEN TO_DATE('{16}','DD-MM-YYYY') AND TO_DATE('{17}','DD-MM-YYYY');
                            UPDATE T_SMRY SET SMRY_NSMT=SMRY_NSMT_A,SMRY_NRCV=SMRY_NRCV_A,SMRY_IWRK=SMRY_IWRK_A,SMRY_NBIL=SMRY_NBIL_A,SMRY_NRTP=SMRY_NRTP_A,SMRY_CMPT=SMRY_CMPT_A, UDT=SYSDATE, UDU='{18}' WHERE SMRY_AMSP='{19}';END commit;",
                              R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE,
                              R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE,
                              R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE,
                              R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE,
                              R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE,
                              R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE,
                              Usr, R.ORDR_TOWH
                            );

                try
                {
                    var r = db.Database.ExecuteSqlCommand(sql: Sql);
                    result = true;
                }
                catch (Exception)
                {
                    return false;
                }

                decimal exp_amt = 0m;
                decimal con_amt = 0m;
                //left outer
                Sql = string.Format(@"select sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0))
                    from auction.t_ordr o
                    join auction.t_ormc e on o.ordr_text = e.ordr_text
                    where o.act ='2' and o.ordr_stas = 'D' and o.ordr_sbmt = 'Y' and o.ordr_towh='{0}'
                    and to_date(o.udt,'DD-MM-YY') between to_date('{1}','DD-MM-YYYY')
                    and to_date('{2}','DD-MM-YYYY')
                    group by o.ordr_towh", R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE);
                try
                {
                    var r = db.Database.SqlQuery<decimal>(sql: Sql).ToList();
                    if (r != null)
                    {
                        exp_amt = r.FirstOrDefault();
                        result = true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }

                Sql = string.Format(@"select sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0))
                    from auction.t_ordr o
                    join auction.t_oram i on o.ordr_text = i.ordr_text
                    where o.act ='2' and o.ordr_stas = 'D' and o.ordr_sbmt = 'Y' and o.ordr_towh='{0}'
                    and to_date(o.udt,'DD-MM-YY') between to_date('{1}','DD-MM-YYYY')
                    and to_date('{2}','DD-MM-YYYY')
                    group by o.ordr_towh", R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE);
                try
                {
                    var r = db.Database.SqlQuery<decimal>(sql: Sql).ToList();
                    if (r != null)
                    {
                        con_amt = r.FirstOrDefault();
                        result = true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                if (exp_amt >= 0 && con_amt >= 0)
                {
                    Sql = string.Format(@"update t_smry  set smry_ormc='{0}', smry_oram='{1}',smry_fmdt= to_date('{2}','DD-MM-YYYY'),smry_todt=to_date('{3}','DD-MM-YYYY') where smry_amsp='{4}'", exp_amt, con_amt, od.FROM_DATE, od.TO_DATE, R.ORDR_TOWH);
                    db.Database.ExecuteSqlCommand(sql: Sql);
                    try
                    {
                        var r2 = db.Database.ExecuteSqlCommand(sql: Sql);
                        result = true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                return result;

            }
        }
        public List<T_SMRY> T_SMRY_List()
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"select w.amsp_name smry_amsp, s.smry_nsmt, s.smry_nrcv, s.smry_iwrk, s.smry_nbil, s.smry_nrtp,
                    s.smry_cmpt,s.smry_ormc,s.smry_oram,s.smry_fmdt,s.smry_todt, s.udt, s.udu
                    from t_smry s
                    join t_wcrt u on s.smry_amsp=u.wcrt_amsp and u.wcrt_actv='1'
                    join t_amsp w on s.smry_amsp=w.oid
                    where u.wcrt_user='{0}'", Usr);
                var _data = db.Database.SqlQuery<T_SMRY>(sql: Sql).ToList();
                return _data;
            }
        }
        public bool PRO_SMRY_W(R_EXPENSES R)
        {
            Oracle_Date od = _c.Oracle_Date_Format(R.FROM_DATE, R.TO_DATE);
            using (auctionDbContext db = new auctionDbContext())
            {
                string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
                bool result = false;
                string Sql = string.Format(@"declare
                            SMRY_NSMT_A NUMBER(10);
                            SMRY_NRCV_A NUMBER(10);
                            SMRY_IWRK_A NUMBER(10);
                            SMRY_NBIL_A NUMBER(10);
                            SMRY_NRTP_A NUMBER(10);
                            SMRY_CMPT_A NUMBER(10);
                            begin
                            SELECT COUNT(ORDR_TEXT) INTO SMRY_NSMT_A FROM auction.T_ORDR WHERE ORDR_STAS='P' AND ACT='0'
                            AND ORDR_FMWH='{0}' AND TO_DATE(CDT,'DD-MM-YY') BETWEEN TO_DATE('{1}','DD-MM-YYYY') AND TO_DATE('{2}','DD-MM-YYYY');
                            SELECT COUNT(ORDR_TEXT) INTO SMRY_NRCV_A FROM auction.T_ORDR WHERE ORDR_STAS='R' AND ACT='0'
                            AND ORDR_FMWH='{0}' AND TO_DATE(UDT,'DD-MM-YY') BETWEEN TO_DATE('{1}','DD-MM-YYYY') AND TO_DATE('{2}','DD-MM-YYYY');
                            SELECT COUNT(ORDR_TEXT) INTO SMRY_IWRK_A FROM auction.T_ORDR WHERE ORDR_STAS='W' AND ORDR_SBMT='Y' AND ACT='0'
                            AND ORDR_FMWH='{6}' AND TO_DATE(UDT,'DD-MM-YY') BETWEEN TO_DATE('{7}','DD-MM-YYYY') AND TO_DATE('{8}','DD-MM-YYYY');
                            SELECT COUNT(ORDR_TEXT) INTO SMRY_NBIL_A FROM auction.T_ORDR WHERE ORDR_STAS='D' AND ORDR_SBMT='Y' AND ACT='0' 
                            AND ORDR_FMWH='{9}' AND TO_DATE(UDT,'DD-MM-YY') BETWEEN TO_DATE('{10}','DD-MM-YYYY') AND TO_DATE('{11}','DD-MM-YYYY');
                            SELECT COUNT(ORDR_TEXT) INTO SMRY_NRTP_A FROM auction.T_ORDR WHERE ORDR_STAS='D' AND ORDR_SBMT='Y' AND ACT in ('1','9')
                            AND ORDR_FMWH='{12}' AND TO_DATE(UDT,'DD-MM-YY') BETWEEN TO_DATE('{13}','DD-MM-YYYY') AND TO_DATE('{14}','DD-MM-YYYY');
                            SELECT COUNT(ORDR_TEXT) INTO SMRY_CMPT_A FROM auction.T_ORDR WHERE ORDR_STAS='D' AND ORDR_SBMT='Y' AND ACT='2' 
                            AND ORDR_FMWH='{15}' AND TO_DATE(UDT,'DD-MM-YY') BETWEEN TO_DATE('{16}','DD-MM-YYYY') AND TO_DATE('{17}','DD-MM-YYYY');
                            UPDATE T_SMRY SET SMRY_NSMT=SMRY_NSMT_A,SMRY_NRCV=SMRY_NRCV_A,SMRY_IWRK=SMRY_IWRK_A,SMRY_NBIL=SMRY_NBIL_A,SMRY_NRTP=SMRY_NRTP_A,SMRY_CMPT=SMRY_CMPT_A, UDT=SYSDATE, UDU='{18}' WHERE SMRY_AMSP='{19}';END commit;",
                              R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE,
                              R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE,
                              R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE,
                              R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE,
                              R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE,
                              R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE,
                              Usr, R.ORDR_FMWH
                            );

                try
                {
                    var r = db.Database.ExecuteSqlCommand(sql: Sql);
                    result = true;
                }
                catch (Exception)
                {
                    return false;
                }
                Sql = string.Format(@"update t_smry set smry_fmdt= to_date('{0}','DD-MM-YYYY'),smry_todt=to_date('{1}','DD-MM-YYYY') where smry_amsp='{2}'", od.FROM_DATE, od.TO_DATE, R.ORDR_FMWH);
                db.Database.ExecuteSqlCommand(sql: Sql);
                return result;

            }
        }
        public T_SMRY T_SMRY_WH(string ORDR_FMWH)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.T_SMRY.Where(x => x.SMRY_AMSP == ORDR_FMWH).ToList();
                if (_data != null)
                {
                    return _data.FirstOrDefault();
                }
                else
                {
                    return new T_SMRY();
                }
            }
        }
    }
}