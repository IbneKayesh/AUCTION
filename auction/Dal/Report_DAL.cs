using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auction.Dal
{
    public class Report_DAL
    {
        CommonData _c = new CommonData();
        public List<R_Unit_EC_Summary_C> R_Unit_EC_Summary_DAL(R_EXPENSES R)
        {
            Oracle_Date od = _c.Oracle_Date_Format(R.FROM_DATE, R.TO_DATE);
            string Sql = string.Empty;
            if (string.IsNullOrWhiteSpace(R.ORDR_TOWH))
            {
                Sql = string.Format(@"select  wh_name,co_name,sum(exp_amnt)exp_amnt,sum(con_amnt)con_amnt from (
                                select w.amsp_name wh_name,c.amsp_name co_name,
                                sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0)) exp_amnt,
                                0 con_amnt
                                from auction.t_ordr o
                                left outer  join auction.t_ormc e on o.ordr_text = e.ordr_text
                                join auction.t_amsp w on o.ordr_fmwh = w.oid
                                join auction.t_amsp c on o.ordr_towh = c.oid
                                where o.act ='2' and o.ordr_stas = 'D' and o.ordr_sbmt = 'Y' and o.ordr_fmwh='{0}'
                                and to_date(o.udt,'DD-MM-YY') between to_date('{1}','DD-MM-YYYY')
                                and to_date('{2}','DD-MM-YYYY')
                                group by w.amsp_name, c.amsp_name
                                union all
                                select w.amsp_name wh_name,c.amsp_name co_name,
                                0 exp_amnt,
                                sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0)) con_amnt
                                from auction.t_ordr o
                                left outer  join auction.t_oram i on o.ordr_text = i.ordr_text
                                join auction.t_amsp w on o.ordr_fmwh = w.oid
                                join auction.t_amsp c on o.ordr_towh = c.oid
                                where o.act ='2' and o.ordr_stas = 'D' and o.ordr_sbmt = 'Y' and o.ordr_fmwh='{3}'
                                and to_date(o.udt,'DD-MM-YY') between to_date('{4}','DD-MM-YYYY')
                                and to_date('{5}','DD-MM-YYYY')
                                group by w.amsp_name, c.amsp_name
                                ) group by  wh_name,co_name", R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE
                                , R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE);


                //Sql = string.Format(@"select w.amsp_name wh_name,c.amsp_name co_name,
                //    sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0)) exp_amnt,
                //    sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0)) con_amnt
                //    from auction.t_ordr o
                //    left outer  join auction.t_ormc e on o.ordr_text = e.ordr_text
                //    left outer  join auction.t_oram i on o.ordr_text = i.ordr_text
                //    join auction.t_amsp w on o.ordr_fmwh = w.oid
                //    join auction.t_amsp c on o.ordr_towh = c.oid
                //    where o.act ='2' and o.ordr_stas = 'D' and o.ordr_sbmt = 'Y' and o.ordr_fmwh='{0}'
                //    and to_date(o.udt,'DD-MM-YY') between to_date('{1}','DD-MM-YYYY')
                //    and to_date('{2}','DD-MM-YYYY')
                //    group by w.amsp_name, c.amsp_name", R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE);
            }
            else
            {

                Sql = string.Format(@"select  wh_name,co_name,sum(exp_amnt)exp_amnt,sum(con_amnt)con_amnt from (
                    select w.amsp_name wh_name,c.amsp_name co_name,
                    sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0)) exp_amnt,
                    0 con_amnt
                    from auction.t_ordr o
                    left outer  join auction.t_ormc e on o.ordr_text = e.ordr_text
                    join auction.t_amsp w on o.ordr_fmwh = w.oid
                    join auction.t_amsp c on o.ordr_towh = c.oid
                    where o.act ='2' and o.ordr_stas = 'D' and o.ordr_sbmt = 'Y' and o.ordr_fmwh='{0}'  and o.ordr_towh='{1}'
                    and to_date(o.udt,'DD-MM-YY') between to_date('{2}','DD-MM-YYYY')
                    and to_date('{3}','DD-MM-YYYY')
                    group by w.amsp_name, c.amsp_name
                    union all
                    select w.amsp_name wh_name,c.amsp_name co_name,
                    0 exp_amnt,
                    sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0)) con_amnt
                    from auction.t_ordr o
                    left outer  join auction.t_oram i on o.ordr_text = i.ordr_text
                    join auction.t_amsp w on o.ordr_fmwh = w.oid
                    join auction.t_amsp c on o.ordr_towh = c.oid
                    where o.act ='2' and o.ordr_stas = 'D' and o.ordr_sbmt = 'Y' and o.ordr_fmwh='{4}'  and o.ordr_towh='{5}'
                    and to_date(o.udt,'DD-MM-YY') between to_date('{6}','DD-MM-YYYY')
                    and to_date('{7}','DD-MM-YYYY')
                    group by w.amsp_name, c.amsp_name
                    ) group by  wh_name,co_name", R.ORDR_FMWH, R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE
                    , R.ORDR_FMWH, R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE);

                //Sql = string.Format(@"select w.amsp_name wh_name,c.amsp_name co_name,
                //    sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0)) exp_amnt,
                //    sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0)) con_amnt
                //    from auction.t_ordr o
                //    left outer  join auction.t_ormc e on o.ordr_text = e.ordr_text
                //    left outer  join auction.t_oram i on o.ordr_text = i.ordr_text
                //    join auction.t_amsp w on o.ordr_fmwh = w.oid
                //    join auction.t_amsp c on o.ordr_towh = c.oid
                //    where o.act ='2' and o.ordr_stas = 'D' and o.ordr_sbmt = 'Y' and o.ordr_fmwh='{0}' and o.ordr_towh='{1}'
                //    and to_date(o.udt,'DD-MM-YY') between to_date('{2}','DD-MM-YYYY')
                //    and to_date('{3}','DD-MM-YYYY')
                //    group by w.amsp_name, c.amsp_name", R.ORDR_FMWH, R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE);


            }

            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.Database.SqlQuery<R_Unit_EC_Summary_C>(sql: Sql).ToList();
                return _data;
            }
        }
        public List<R_Unit_EC_D_Summary_C> R_Unit_EC_D_Summary_DAL(R_EXPENSES R)
        {
            Oracle_Date od = _c.Oracle_Date_Format(R.FROM_DATE, R.TO_DATE);
            string Sql = string.Empty;
            if (string.IsNullOrWhiteSpace(R.ORDR_TOWH))
            {
                Sql = string.Format(@"select  ordr_text,ordr_date, wh_name,co_name,ortp_name,mold_name,sum(exp_amnt)exp_amnt,sum(con_amnt)con_amnt
                            from (
                            select o.ordr_text,o.ordr_date,w.amsp_name wh_name,c.amsp_name co_name,t.ortp_name,m.mold_name,
                            sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0)) exp_amnt,0 con_amnt-- sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0)) con_amnt
                            from auction.t_ordr o
                            left outer join auction.t_ormc e on o.ordr_text = e.ordr_text
                            --left outer join auction.t_oram i on o.ordr_text = i.ordr_text
                            join auction.t_amsp w on o.ordr_fmwh = w.oid
                            join auction.t_amsp c on o.ordr_towh = c.oid
                            join auction.t_mold m on o.ordr_item=m.mold_text
                            join auction.t_ortp t on o.ordr_ortp=t.ortp_text
                            where o.act ='2' and o.ordr_fmwh='{0}'
                            and to_date(o.udt,'DD-MM-YY') between to_date('{1}','DD-MM-YYYY')
                            and to_date('{2}','DD-MM-YYYY')
                            group by o.ordr_text,o.ordr_date,w.amsp_name,c.amsp_name,t.ortp_name,m.mold_name
                            union all
                            select o.ordr_text,o.ordr_date,w.amsp_name wh_name,c.amsp_name co_name,t.ortp_name,m.mold_name,
                            0 exp_amnt,--sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0)) exp_amnt,
                            sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0)) con_amnt
                            from auction.t_ordr o
                            --left outer join auction.t_ormc e on o.ordr_text = e.ordr_text
                            left outer join auction.t_oram i on o.ordr_text = i.ordr_text
                            join auction.t_amsp w on o.ordr_fmwh = w.oid
                            join auction.t_amsp c on o.ordr_towh = c.oid
                            join auction.t_mold m on o.ordr_item=m.mold_text
                            join auction.t_ortp t on o.ordr_ortp=t.ortp_text
                            where o.act ='2' and o.ordr_fmwh='{3}'
                            and to_date(o.udt,'DD-MM-YY') between to_date('{4}','DD-MM-YYYY')
                            and to_date('{5}','DD-MM-YYYY')
                            group by o.ordr_text,o.ordr_date,w.amsp_name,c.amsp_name,t.ortp_name,m.mold_name
                            ) group by ordr_text,ordr_date,wh_name,co_name,ortp_name,mold_name
                            order by co_name,ordr_text", R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE,
                            R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE);



                // Sql = string.Format(@"select o.ordr_text,o.ordr_date,w.amsp_name wh_name,c.amsp_name co_name,t.ortp_name,m.mold_name,
                // sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0)) exp_amnt, sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0)) con_amnt
                // from auction.t_ordr o
                //left outer join auction.t_ormc e on o.ordr_text = e.ordr_text
                //left outer join auction.t_oram i on o.ordr_text = i.ordr_text
                // join auction.t_amsp w on o.ordr_fmwh = w.oid
                // join auction.t_amsp c on o.ordr_towh = c.oid
                // join auction.t_mold m on o.ordr_item=m.mold_text
                // join auction.t_ortp t on o.ordr_ortp=t.ortp_text
                // where o.act in (2,3) and o.ordr_fmwh='{0}'
                // and to_date(o.udt,'DD-MM-YY') between to_date('{1}','DD-MM-YYYY')
                // and to_date('{2}','DD-MM-YYYY')
                // group by o.ordr_text,o.ordr_date,w.amsp_name,c.amsp_name,t.ortp_name,m.mold_name
                // order by c.amsp_name,o.ordr_text", R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE);
            }
            else
            {
                Sql = string.Format(@"select  ordr_text,ordr_date, wh_name,co_name,ortp_name,mold_name,sum(exp_amnt)exp_amnt,sum(con_amnt)con_amnt
                            from (
                            select o.ordr_text,o.ordr_date,w.amsp_name wh_name,c.amsp_name co_name,t.ortp_name,m.mold_name,
                            sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0)) exp_amnt,0 con_amnt-- sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0)) con_amnt
                            from auction.t_ordr o
                            left outer join auction.t_ormc e on o.ordr_text = e.ordr_text
                            --left outer join auction.t_oram i on o.ordr_text = i.ordr_text
                            join auction.t_amsp w on o.ordr_fmwh = w.oid
                            join auction.t_amsp c on o.ordr_towh = c.oid
                            join auction.t_mold m on o.ordr_item=m.mold_text
                            join auction.t_ortp t on o.ordr_ortp=t.ortp_text
                            where o.act ='2' and o.ordr_fmwh='{0}' and o.ordr_towh='{1}'
                            and to_date(o.udt,'DD-MM-YY') between to_date('{2}','DD-MM-YYYY')
                            and to_date('{3}','DD-MM-YYYY')
                            group by o.ordr_text,o.ordr_date,w.amsp_name,c.amsp_name,t.ortp_name,m.mold_name
                            union all
                            select o.ordr_text,o.ordr_date,w.amsp_name wh_name,c.amsp_name co_name,t.ortp_name,m.mold_name,
                            0 exp_amnt,--sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0)) exp_amnt,
                            sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0)) con_amnt
                            from auction.t_ordr o
                            --left outer join auction.t_ormc e on o.ordr_text = e.ordr_text
                            left outer join auction.t_oram i on o.ordr_text = i.ordr_text
                            join auction.t_amsp w on o.ordr_fmwh = w.oid
                            join auction.t_amsp c on o.ordr_towh = c.oid
                            join auction.t_mold m on o.ordr_item=m.mold_text
                            join auction.t_ortp t on o.ordr_ortp=t.ortp_text
                            where o.act ='2' and o.ordr_fmwh='{4}' and o.ordr_towh='{5}'
                            and to_date(o.udt,'DD-MM-YY') between to_date('{6}','DD-MM-YYYY')
                            and to_date('{7}','DD-MM-YYYY')
                            group by o.ordr_text,o.ordr_date,w.amsp_name,c.amsp_name,t.ortp_name,m.mold_name
                            ) group by ordr_text,ordr_date,wh_name,co_name,ortp_name,mold_name
                            order by co_name,ordr_text", R.ORDR_FMWH, R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE,
                            R.ORDR_FMWH, R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE);



                // Sql = string.Format(@"select o.ordr_text,o.ordr_date,w.amsp_name wh_name,c.amsp_name co_name,t.ortp_name,m.mold_name,
                // sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0)) exp_amnt, sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0)) con_amnt
                // from auction.t_ordr o
                //left outer join auction.t_ormc e on o.ordr_text = e.ordr_text
                //left outer join auction.t_oram i on o.ordr_text = i.ordr_text
                // join auction.t_amsp w on o.ordr_fmwh = w.oid
                // join auction.t_amsp c on o.ordr_towh = c.oid
                // join auction.t_mold m on o.ordr_item=m.mold_text
                // join auction.t_ortp t on o.ordr_ortp=t.ortp_text
                // where o.act in (2,3) and o.ordr_fmwh='{0}' and o.ordr_towh='{1}'
                // and to_date(o.udt,'DD-MM-YY') between to_date('{2}','DD-MM-YYYY')
                // and to_date('{3}','DD-MM-YYYY')
                // group by o.ordr_text,o.ordr_date,w.amsp_name,c.amsp_name,t.ortp_name,m.mold_name
                // order by o.ordr_text", R.ORDR_FMWH, R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE);
            }

            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.Database.SqlQuery<R_Unit_EC_D_Summary_C>(sql: Sql).ToList();
                return _data;
            }
        }
        public List<R_Unit_EC_Summary_C> R_Unit_EC_Summary_Email_DAL(R_EXPENSES R)
        {
            Oracle_Date od = _c.Oracle_Date_Format(R.FROM_DATE, R.TO_DATE);

            //string Sql = string.Format(@"select w.amsp_name wh_name,c.oid co_id, c.amsp_name co_name,
            //        sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0)) exp_amnt,
            //        sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0)) con_amnt
            //        from auction.t_ordr o
            //        left outer  join auction.t_ormc e on o.ordr_text = e.ordr_text
            //        left outer  join auction.t_oram i on o.ordr_text = i.ordr_text
            //        join auction.t_amsp w on o.ordr_fmwh = w.oid
            //        join auction.t_amsp c on o.ordr_towh = c.oid
            //        where  o.ordr_stas = 'D' and o.ordr_sbmt = 'Y' and o.act ='2' and o.ordr_fmwh='{0}'
            //        and to_date(o.udt,'DD-MM-YY') between to_date('{1}','DD-MM-YYYY')
            //        and to_date('{2}','DD-MM-YYYY')
            //        group by w.amsp_name,c.oid,c.amsp_name", R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE);

            string Sql = string.Format(@"select  wh_name,co_name,sum(exp_amnt)exp_amnt,sum(con_amnt)con_amnt from (
                                select w.amsp_name wh_name,c.amsp_name co_name,
                                sum (nvl(e.ormc_hour,0) * nvl(e.ormc_cost,0)) exp_amnt,
                                0 con_amnt
                                from auction.t_ordr o
                                left outer  join auction.t_ormc e on o.ordr_text = e.ordr_text
                                join auction.t_amsp w on o.ordr_fmwh = w.oid
                                join auction.t_amsp c on o.ordr_towh = c.oid
                                where o.act ='2' and o.ordr_stas = 'D' and o.ordr_sbmt = 'Y' and o.ordr_fmwh='{0}'
                                and to_date(o.udt,'DD-MM-YY') between to_date('{1}','DD-MM-YYYY')
                                and to_date('{2}','DD-MM-YYYY')
                                group by w.amsp_name, c.amsp_name
                                union all
                                select w.amsp_name wh_name,c.amsp_name co_name,
                                0 exp_amnt,
                                sum (nvl(i.oram_qty,0) * nvl(i.oram_rate,0)) con_amnt
                                from auction.t_ordr o
                                left outer  join auction.t_oram i on o.ordr_text = i.ordr_text
                                join auction.t_amsp w on o.ordr_fmwh = w.oid
                                join auction.t_amsp c on o.ordr_towh = c.oid
                                where o.act ='2' and o.ordr_stas = 'D' and o.ordr_sbmt = 'Y' and o.ordr_fmwh='{3}'
                                and to_date(o.udt,'DD-MM-YY') between to_date('{4}','DD-MM-YYYY')
                                and to_date('{5}','DD-MM-YYYY')
                                group by w.amsp_name, c.amsp_name
                                ) group by  wh_name,co_name", R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE
                                , R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE);

            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.Database.SqlQuery<R_Unit_EC_Summary_C>(sql: Sql).ToList();
                return _data;
            }
        }
        public List<R_Unit_EC_ORAM_ORMC_C> R_Unit_EC_ORAM_ORMC_DAL(string ORDR_TEXT)
        {
            string Sql = string.Format(@"select m.ordr_text,(mc.mclt_name ||'#'|| mc.mclt_desc)mclt_name, m.ormc_hour, m.ormc_cost, m.ormc_desc,
                '' ORDR_REFR
                from auction.t_ormc m join auction.t_mclt mc on m.mclt_text = mc.mclt_text
                where m.ordr_text='{0}'
                union
                select i.ordr_text, ic.amim_name, i.oram_qty, i.oram_rate, i.oram_desc,
                i.oram_refr
                from auction.t_oram i join auction.t_amim ic on i.amim_text = ic.oid
                where i.ordr_text='{1}'", ORDR_TEXT, ORDR_TEXT);
            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.Database.SqlQuery<R_Unit_EC_ORAM_ORMC_C>(sql: Sql).ToList();
                return _data;
            }
        }
        public List<R_TimeConsumeC> R_TimeConsumeD_DAL(R_EXPENSES R)
        {
            Oracle_Date od = _c.Oracle_Date_Format(R.FROM_DATE, R.TO_DATE);
            string Sql = string.Empty;
            if (string.IsNullOrWhiteSpace(R.ORDR_TOWH))
            {
                Sql = string.Format(@"select o.ordr_text,w.amsp_name wh_name,c.amsp_name co_name,t.ortp_name,m.mold_name,o.cdt,o.ordr_rdat,o.ordr_ddat,o.ordr_stas               
                from auction.t_ordr o
                left outer join auction.t_ormc e on o.ordr_text = e.ordr_text
                left outer join auction.t_oram i on o.ordr_text = i.ordr_text
                join auction.t_amsp w on o.ordr_fmwh = w.oid
                join auction.t_amsp c on o.ordr_towh = c.oid
                join auction.t_mold m on o.ordr_item=m.mold_text
                join auction.t_ortp t on o.ordr_ortp=t.ortp_text
                where o.ordr_fmwh='{0}'
                and to_date(o.udt,'DD-MM-YY') between to_date('{1}','DD-MM-YYYY')
                and to_date('{2}','DD-MM-YYYY')
                order by c.amsp_name,o.ordr_text", R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE);
            }
            else
            {
                Sql = string.Format(@"select o.ordr_text,w.amsp_name wh_name,c.amsp_name co_name,t.ortp_name,m.mold_name,o.cdt,o.ordr_rdat,o.ordr_ddat,o.ordr_stas               
                from auction.t_ordr o
                left outer join auction.t_ormc e on o.ordr_text = e.ordr_text
                left outer join auction.t_oram i on o.ordr_text = i.ordr_text
                join auction.t_amsp w on o.ordr_fmwh = w.oid
                join auction.t_amsp c on o.ordr_towh = c.oid
                join auction.t_mold m on o.ordr_item=m.mold_text
                join auction.t_ortp t on o.ordr_ortp=t.ortp_text
                where o.ordr_fmwh='{0}' and o.ordr_towh='{1}'
                and to_date(o.udt,'DD-MM-YY') between to_date('{2}','DD-MM-YYYY')
                and to_date('{3}','DD-MM-YYYY')
                order by c.amsp_name,o.ordr_text", R.ORDR_FMWH, R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE);
            }
            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.Database.SqlQuery<R_TimeConsumeC>(sql: Sql).ToList();
                return _data;
            }
        }
        public List<R_ExpConsumeC> R_ExpConsume_DAL(R_EXPENSES R)
        {
            Oracle_Date od = _c.Oracle_Date_Format(R.FROM_DATE, R.TO_DATE);
            string Sql = string.Format(@"select m.mclt_name, m.mclt_desc, sum(nvl(p.ormc_hour,0)) mclt_hour
                from auction.t_mclt m
                left outer join auction.t_ormc p on m.mclt_text=p.mclt_text
                and to_date(p.cdt,'DD-MM-YY') between to_date('{0}','DD-MM-YYYY') and to_date('{1}','DD-MM-YYYY')
                where m.mclt_fmwh='{2}'
                and m.mclt_mctp='{3}'
                and m.act=1
                group by  m.mclt_name, m.mclt_desc
                order by  mclt_hour desc", od.FROM_DATE, od.TO_DATE, R.ORDR_FMWH, R.ORDR_TOWH);
            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.Database.SqlQuery<R_ExpConsumeC>(sql: Sql).ToList();
                return _data;
            }
        }

        public List<R_BILL_PENDING_C> R_BILL_PENDING_DAL(R_EXPENSES R, string OT)
        {
            Oracle_Date od = _c.Oracle_Date_Format(R.FROM_DATE, R.TO_DATE);
            string Sql = string.Empty;
            if (string.IsNullOrWhiteSpace(R.ORDR_TOWH) && OT == "B")
            {
                Sql = string.Format(@"SELECT O.ORDR_TEXT, W.AMSP_NAME WH_NAME, C.AMSP_NAME CO_NAME, T.ORTP_NAME,
                M.MOLD_NAME, O.ORDR_RISR, O.ORDR_DATE, O.ORDR_DDAT, O.ORDR_PRLM,O.ORDR_CMNT
                FROM auction.T_ORDR O JOIN auction.T_AMSP W ON O.ORDR_FMWH = W.OID
                JOIN auction.T_AMSP C ON O.ORDR_TOWH = C.OID
                JOIN auction.T_ORTP T ON O.ORDR_ORTP = T.ORTP_TEXT
                JOIN auction.T_MOLD M ON O.ORDR_ITEM = M.MOLD_TEXT
                WHERE O.ORDR_STAS = 'D' AND O.ACT = '0' AND O.ORDR_SBMT = 'Y'
                AND O.ORDR_FMWH='{0}'
                AND TO_DATE(O.UDT,'DD-MM-YY') BETWEEN TO_DATE('{1}','DD-MM-YYYY') AND TO_DATE('{2}','DD-MM-YYYY')
                ORDER BY W.AMSP_NAME,C.AMSP_NAME,T.ORTP_NAME",R.ORDR_FMWH,od.FROM_DATE,od.TO_DATE);

            }
            else if (string.IsNullOrWhiteSpace(R.ORDR_TOWH) && OT == "R")
            {
                Sql = string.Format(@"SELECT O.ORDR_TEXT, W.AMSP_NAME WH_NAME, C.AMSP_NAME CO_NAME, T.ORTP_NAME,
                M.MOLD_NAME, O.ORDR_RISR, O.ORDR_DATE, O.ORDR_DDAT, O.ORDR_PRLM,O.ORDR_CMNT
                FROM auction.T_ORDR O JOIN auction.T_AMSP W ON O.ORDR_FMWH = W.OID
                JOIN auction.T_AMSP C ON O.ORDR_TOWH = C.OID
                JOIN auction.T_ORTP T ON O.ORDR_ORTP = T.ORTP_TEXT
                JOIN auction.T_MOLD M ON O.ORDR_ITEM = M.MOLD_TEXT
                WHERE O.ORDR_STAS = 'D' AND O.ACT in(1,9) AND O.ORDR_SBMT = 'Y'
                AND O.ORDR_FMWH='{0}'
                AND TO_DATE(O.UDT,'DD-MM-YY') BETWEEN TO_DATE('{1}','DD-MM-YYYY') AND TO_DATE('{2}','DD-MM-YYYY')
                ORDER BY W.AMSP_NAME,C.AMSP_NAME,T.ORTP_NAME", R.ORDR_FMWH, od.FROM_DATE, od.TO_DATE);
            }
            else if (OT == "B")
            {
                Sql = string.Format(@"SELECT O.ORDR_TEXT, W.AMSP_NAME WH_NAME, C.AMSP_NAME CO_NAME, T.ORTP_NAME,
                M.MOLD_NAME, O.ORDR_RISR, O.ORDR_DATE, O.ORDR_DDAT, O.ORDR_PRLM,O.ORDR_CMNT
                FROM auction.T_ORDR O JOIN auction.T_AMSP W ON O.ORDR_FMWH = W.OID
                JOIN auction.T_AMSP C ON O.ORDR_TOWH = C.OID
                JOIN auction.T_ORTP T ON O.ORDR_ORTP = T.ORTP_TEXT
                JOIN auction.T_MOLD M ON O.ORDR_ITEM = M.MOLD_TEXT
                WHERE O.ORDR_STAS = 'D' AND O.ACT = '0' AND O.ORDR_SBMT = 'Y'
                AND O.ORDR_FMWH='{0}' AND O.ORDR_TOWH='{1}'
                AND TO_DATE(O.UDT,'DD-MM-YY') BETWEEN TO_DATE('{2}','DD-MM-YYYY') AND TO_DATE('{3}','DD-MM-YYYY')
                ORDER BY W.AMSP_NAME,C.AMSP_NAME,T.ORTP_NAME", R.ORDR_FMWH,R.ORDR_TOWH ,od.FROM_DATE, od.TO_DATE);
            }
            else if (OT == "R")
            {
                Sql = string.Format(@"SELECT O.ORDR_TEXT, W.AMSP_NAME WH_NAME, C.AMSP_NAME CO_NAME, T.ORTP_NAME,
                M.MOLD_NAME, O.ORDR_RISR, O.ORDR_DATE, O.ORDR_DDAT, O.ORDR_PRLM,O.ORDR_CMNT
                FROM auction.T_ORDR O JOIN auction.T_AMSP W ON O.ORDR_FMWH = W.OID
                JOIN auction.T_AMSP C ON O.ORDR_TOWH = C.OID
                JOIN auction.T_ORTP T ON O.ORDR_ORTP = T.ORTP_TEXT
                JOIN auction.T_MOLD M ON O.ORDR_ITEM = M.MOLD_TEXT
                WHERE O.ORDR_STAS = 'D' AND O.ACT in(1,9) AND O.ORDR_SBMT = 'Y'
                AND O.ORDR_FMWH='{0}' AND O.ORDR_TOWH='{1}'
                AND TO_DATE(O.UDT,'DD-MM-YY') BETWEEN TO_DATE('{2}','DD-MM-YYYY') AND TO_DATE('{3}','DD-MM-YYYY')
                ORDER BY W.AMSP_NAME,C.AMSP_NAME,T.ORTP_NAME", R.ORDR_FMWH,R.ORDR_TOWH, od.FROM_DATE, od.TO_DATE);
            }
            else
            {
                    return new List<R_BILL_PENDING_C>();
            }

            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.Database.SqlQuery<R_BILL_PENDING_C>(sql: Sql).ToList();
                return _data;
            }
        }
    }
}