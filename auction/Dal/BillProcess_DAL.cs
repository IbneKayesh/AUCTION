using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace auction.Dal
{
    public class BillProcess_DAL
    {
        CommonData _c = new CommonData();
        public bool SaveBillModel(ORMC_ORAM oo)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();

            bool result = true;
            using (auctionDbContext db = new auctionDbContext())
            {
                try
                {
                    foreach (var m in oo.T_ORMC)
                    {
                        m.ORMC_COST = 0;
                        m.ACT = 1;
                        m.CDT = DateTime.Now;
                        m.CDU = Usr;
                        m.VAR = 1;
                        db.T_ORMC.Add(m);
                    }
                    foreach (var i in oo.T_ORAM)
                    {
                        i.ORAM_RATE = 0;
                        i.ACT = 1;
                        i.CDT = DateTime.Now;
                        i.CDU = Usr;
                        i.VAR = 1;
                        db.T_ORAM.Add(i);
                    }
                    db.SaveChanges();
                    string Sql = string.Format(@"update auction.t_ordr set act='1' where ordr_text='{0}'", oo.ORDR_TEXT);
                    db.Database.ExecuteSqlCommand(sql: Sql);
                    result = true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return result;
        }
        public bool SaveBillModelOnly(ORMC oo)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();

            bool result = true;
            using (auctionDbContext db = new auctionDbContext())
            {
                try
                {
                    foreach (var m in oo.T_ORMC)
                    {
                        m.ORMC_COST = 0;
                        m.ACT = 1;
                        m.CDT = DateTime.Now;
                        m.CDU = Usr;
                        m.VAR = 1;
                        db.T_ORMC.Add(m);
                    }
                    db.SaveChanges();
                    string Sql = string.Empty;
                    if (oo.ORDR_XY == "on")
                    {
                        Sql= string.Format(@"update auction.t_ordr set act='9',udu='{1}' where ordr_text='{0}'", oo.ORDR_TEXT, Usr);
                    }
                    else
                    {
                        Sql = string.Format(@"update auction.t_ordr set act='1',udu='{1}' where ordr_text='{0}'", oo.ORDR_TEXT, Usr);
                    }

                    db.Database.ExecuteSqlCommand(sql: Sql);
                    result = true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return result;
        }
        public List<V_ORDR_EXP> OrderBillM_List(string ORDR_VAL)
        {
            string Sql = string.Format(@"select m.mclt_name, o.ormc_hour, o.ormc_cost,o.ormc_desc
                        from auction.t_ormc o join auction.t_mclt m on o.mclt_text = m.mclt_text
                        where o.ordr_text='{0}'", ORDR_VAL);
            using (auctionDbContext db = new auctionDbContext())
            {
                try
                {
                    var _data = db.Database.SqlQuery<V_ORDR_EXP>(sql: Sql).ToList();
                    return _data;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public List<V_ORDR_ITEM> OrderBillI_List(string ORDR_VAL)
        {
            string Sql = string.Format(@"select i.amim_text, i.amim_name, u.amsu_name, o.oram_qty, 
                            o.oram_rate,o.oram_desc
                            from auction.t_oram o join auction.t_amim i on o.amim_text = i.oid
                            join auction.t_amsu u on i.amim_amsu = u.oid
                            where o.ordr_text = '{0}'", ORDR_VAL);
            using (auctionDbContext db = new auctionDbContext())
            {
                try
                {
                    var _data = db.Database.SqlQuery<V_ORDR_ITEM>(sql: Sql).ToList();
                    return _data;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        //ECR   ECIQR   ER
        public int StartProcess(string ORDR_TEXT, string CO_TEXT, string CT)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            string Sql_Exp = string.Format(@"update (
                                        select t1.mclt_text ormc, t1.ormc_cost orrt, t2.mclt_text mclt,
                                        t2.mclt_cost mcrt, t1.udu, t1.udt
                                        from auction.t_ormc t1, auction.t_mclt t2, auction.t_ordr t3
                                        where t1.mclt_text = t2.mclt_text
                                        and t3.ordr_text=t1.ORDR_TEXT and t3.act in(1,9)
                                        and t1.ordr_text = '{0}'
                                        )
                                        set orrt = mcrt, udu='{1}', udt=sysdate", ORDR_TEXT, Usr);

            string Sql_Con = string.Empty;

            string Sql_Con_L_Add = string.Empty;
            string Sql_Con_L_Update = string.Empty;

            string Sql_Ordr = string.Format(@"update auction.t_ordr set act='2',udu='{1}',udt=sysdate where ordr_text='{0}'", ORDR_TEXT, Usr);

            if (CT == "ECR") //update chain rate
            {
                if (string.IsNullOrWhiteSpace(CO_TEXT)) //without CO
                {
                    Sql_Con_L_Add = string.Format(@"declare 
                    fmwh varchar2(50);
                    towh varchar2(50);
                    begin
                    select o.ordr_fmwh ,o.ordr_towh into fmwh, towh from auction.t_ordr o where o.ordr_text = '{0}';
                    merge into auction.t_imtm_rate tgt using (
                    select v.imtm_fmwh,v.imtm_towh,v.imtd_amim,v.rate from auction.vw_imtm_rate v
                    join auction.t_oram i on v.imtd_amim=i.amim_text and i.ordr_text='{1}'
                    where v.imtm_fmwh=fmwh and v.imtm_towh=towh
                    )src   on (tgt.imtm_fmwh = src.imtm_fmwh and tgt.imtm_towh=src.imtm_towh and tgt.imtd_amim=src.imtd_amim) 
                    when matched then
                    update  set tgt.imtd_rate=src.rate
                    when not matched then
                    insert (tgt.imtm_fmwh,tgt.imtm_towh,tgt.imtd_amim,tgt.imtd_rate) values(src.imtm_fmwh,src.imtm_towh,src.imtd_amim,src.rate);END commit;", ORDR_TEXT, ORDR_TEXT);
                    Sql_Con_L_Update = string.Format(@"merge into auction.t_oram tgt using(
                    select o.ordr_text,v.imtd_amim,v.imtd_rate,v.imtm_text from auction.t_imtm_rate v
                    join auction.t_ordr o on v.imtm_fmwh=o.ordr_fmwh and v.imtm_towh=o.ordr_towh
                    where o.ordr_text = '{0}'
                    and o.ordr_sbmt = 'Y'
                    and o.act in(1,9)
                    and o.ordr_stas = 'D'
                    ) src on (tgt.ordr_text=src.ordr_text and tgt.amim_text=src.imtd_amim)
                    when matched then update set tgt.oram_rate=src.imtd_rate,tgt.oram_refr=src.imtm_text", ORDR_TEXT);
                }
                else //with CO
                {
                    Sql_Con = string.Format(@"update(
                        select o.ordr_text, o.ordr_fmwh, o.ordr_towh, a.amim_text, b.amim_amsu,
                        a.oram_qty, a.oram_rate,d.imtd_rate,a.oram_refr,m.imtm_text, a.udt
                        from auction.t_ordr o join auction.t_oram a on o.ordr_text = a.ordr_text
                        join auction.t_amim b on a.amim_text = b.oid
                        join im.t_imtd d
                        on a.amim_text = d.imtd_amim
                        and b.amim_amsu = d.imtd_amsu
                        join im.t_imtm m
                        on d.imtd_imtm = m.oid
                        and m.imtm_towh = o.ordr_towh
                        and m.imtm_fmwh = o.ordr_fmwh
                        and m.imtm_auctionm = 'auctionMxxxxxxxxxxx35005xxxxxxxxCO000010xxxx'
                        and m.imtm_posf = 'Y'
                        and m.imtm_cnlf = 'N'
                        and m.imtm_text = '{1}'
                        where o.ordr_text = '{0}'
                        and o.ordr_sbmt = 'Y'
                        and o.act in(1,9)
                        and o.ordr_stas = 'D'
                        )set oram_rate=imtd_rate,oram_refr=imtm_text,udt=sysdate", ORDR_TEXT, CO_TEXT);
                }
            }
            else if (CT == "ECIQR") //insert chain item qty rate , Without CO
            {
                Sql_Con = string.Format(@"insert into auction.t_oram (ordr_text,amim_text,oram_qty,oram_desc,oram_rate,oram_refr,act,cdu)
                select o.ordr_text ,d.imtd_amim,d.imtd_qnty, d.imtd_note,d.imtd_rate,'{2}' ,'1' act,'{3}'
                from im.t_imtm m 
                join im.t_imtd d on m.oid = d.imtd_imtm
                join auction.t_ordr o on m.imtm_fmwh=o.ordr_fmwh and m.imtm_towh=o.ordr_towh
                where m.imtm_auctionm = 'auctionMxxxxxxxxxxx35005xxxxxxxxCO000010xxxx'
                and m.imtm_posf = 'Y'
                and m.imtm_cnlf = 'N'
                and m.imtm_text = '{1}'
                and o.ordr_text='{0}'
                and o.ordr_sbmt='Y'
                and o.ordr_stas='D'
                and o.act in(1,9)", ORDR_TEXT, CO_TEXT, CO_TEXT, Usr);
            }
            else if (CT == "ER")
            {
                Sql_Con = string.Empty;
                using (auctionDbContext DB = new auctionDbContext())
                {
                    if (string.IsNullOrWhiteSpace(Sql_Con))
                    {
                        // string ChkItem = string.Format(@"select count(ordr_text) ordr_text from auction.t_oram where ordr_text='{0}'", ORDR_TEXT);
                        var ChkItem = DB.T_ORAM.Where(s => s.ORDR_TEXT == ORDR_TEXT).Count();
                        if (ChkItem > 0)
                        {
                            return 4;
                        }
                        var ChkItem2 = DB.T_ORDR.Where(s => s.ORDR_TEXT == ORDR_TEXT && s.ACT==9).Count();
                        if (ChkItem2 > 0)
                        {
                            return 4;
                        }
                    }
                }
            }
            else
            {
                return 0;
            }

            using (auctionDbContext db = new auctionDbContext())
            {
                try
                {
                    int i = db.Database.ExecuteSqlCommand(sql: Sql_Exp);
                    if (i == 0)
                    {
                        return 0;
                    }

                    if (!string.IsNullOrWhiteSpace(Sql_Con))
                    {
                        i = db.Database.ExecuteSqlCommand(sql: Sql_Con);
                        if (i == 0)
                        {
                            return 1;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(Sql_Con_L_Add))
                    {
                        i = db.Database.ExecuteSqlCommand(sql: Sql_Con_L_Add);
                        if (i == 0)
                        {
                            return 1;
                        }
                        else
                        {
                            i = db.Database.ExecuteSqlCommand(sql: Sql_Con_L_Update);
                            if (i == 0)
                            {
                                return 1;
                            }
                        }
                    }


                    i = db.Database.ExecuteSqlCommand(sql: Sql_Ordr); //master update
                    if (i == 0)
                    {
                        return 2;
                    }
                    return 3;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
        }
        public int WhClosingMonth(V_CLSM clsm)
        {
            //session user
            string Usr = "78278";// System.Web.HttpContext.Current.Session["UserId"].ToString();

            //create a list for wh related active co unit
            List<T_PSMA> T_PSMA_LIST = new List<T_PSMA>();
            using (auctionDbContext db = new auctionDbContext())
            {
                T_PSMA_LIST = db.T_PSMA.Where(w => w.PSMA_AMSP == clsm.CLSM_FMWH && w.AMSP_ACTV == "1").ToList();
            }

            //create unique Id for all the data
            string Id = string.Empty;
            //create a general Sql_List
            List<string> Sql_List = new List<string>();
            //create a Sql Query
            string Sql = string.Empty;

            //catch WH and APPLY ALL expense
            foreach (var item in clsm.T_CLSM_V)
            {
                if (string.IsNullOrWhiteSpace(Id))
                {
                    Id = item.CLSM_YEAR + "" + item.CLSM_MONT;
                }
                #region T_CLSD
                //catch WH name with grid WH name APPLY ALL Expense
                if (item.CLSM_TYPE=="AL" && item.CLSM_FMWH == clsm.CLSM_FMWH)
                {
                    //for WH APPLY ALL expense also
                    Sql = string.Format(@"Insert into auction.T_CLSD(CLSD_CLST, CLSD_CLSM_TEXT,CLSD_CLSM_AMSP, CLSD_AMNT, CLSD_DESC, ACT, CDT, CDU, VAR) Values ('{0}', '{1}','{2}','{3}', '', 1, sysdate, '{4}', 1)", item.CLSD_CLST, Id, item.CLSM_FMWH, item.CLSD_AMNT, Usr);
              //----Sql_List.Add(Sql);
                    //apply for all the related CO unit expense 
                    foreach (var psma in T_PSMA_LIST)
                    {
                        Sql = string.Format(@"Insert into auction.T_CLSD(CLSD_CLST, CLSD_CLSM_TEXT,CLSD_CLSM_AMSP, CLSD_AMNT, CLSD_DESC, ACT, CDT, CDU, VAR) Values ('{0}', '{1}','{2}','{3}', '', 1, sysdate, '{4}', 1)", item.CLSD_CLST, Id, psma.AMSP_PSMA, 0, Usr);
                        //----Sql_List.Add(Sql);
                    }
                }
                else if (item.CLSM_TYPE == "EX")
                {
                    //for WH  Exception expense also
                    Sql = string.Format(@"Insert into auction.T_CLSD(CLSD_CLST, CLSD_CLSM_TEXT,CLSD_CLSM_AMSP, CLSD_AMNT, CLSD_DESC, ACT, CDT, CDU, VAR) Values ('{0}', '{1}','{2}','{3}', '', 1, sysdate, '{4}', 1)", item.CLSD_CLST, Id, item.CLSM_FMWH, item.CLSD_AMNT, Usr);
                    //----Sql_List.Add(Sql);
                }
                else
                {
                    //do nothing
                }
                #endregion
            }

            T_CLSM_V t_CLSM_V = clsm.T_CLSM_V.FirstOrDefault();
            
            #region T_CLSM
            //for WH
            Sql = string.Format(@"Insert into auction.T_CLSM(CLSM_TEXT, CLSM_AMSP, CLSM_ORMC, CLSM_ORAM, CLSM_CYCL, CLSM_MONT, CLSM_YEAR, ACT, CDT, CDU, VAR) Values ('{0}', '{1}', 0, 0, '{2}', '{3}', '{4}', 1, sysdate, '{5}', 1)", Id, clsm.CLSM_FMWH, t_CLSM_V.CLSM_CYCL, t_CLSM_V.CLSM_MONT, t_CLSM_V.CLSM_YEAR, Usr);
            //----Sql_List.Add(Sql);
            //for CO
            foreach (T_PSMA v in T_PSMA_LIST)
            {
                Sql = string.Format(@"Insert into auction.T_CLSM(CLSM_TEXT, CLSM_AMSP, CLSM_ORMC, CLSM_ORAM, CLSM_CYCL, CLSM_MONT, CLSM_YEAR, ACT, CDT, CDU, VAR) Values ('{0}', '{1}', 0, 0, '{2}', '{3}', '{4}', 1, sysdate, '{5}', 1)", Id, v.AMSP_PSMA, t_CLSM_V.CLSM_CYCL, t_CLSM_V.CLSM_MONT, t_CLSM_V.CLSM_YEAR, Usr);
                //----Sql_List.Add(Sql);
            }
            #endregion


            Month_Cycle MC = _c._Month_Cycle(t_CLSM_V.CLSM_CYCL, Convert.ToInt32(t_CLSM_V.CLSM_YEAR), t_CLSM_V.CLSM_MONT);

            List<V_CLSM_ORMC_ORAM> v_ORBL_ORMC_ORAM = new List<V_CLSM_ORMC_ORAM>();
            using (auctionDbContext db = new auctionDbContext())
            {
                Sql = string.Format(@"select c.oid,sum (nvl(e.ormc_hour, 0) * nvl(e.ormc_cost, 0)) ormc,
                    sum(nvl(i.oram_qty, 0) * nvl(i.oram_rate, 0)) oram
                    from auction.t_ordr o
                    join auction.t_psma n on o.ordr_fmwh=n.psma_amsp and o.ordr_towh=n.amsp_psma and n.amsp_actv='1'
                    left outer join auction.t_ormc e on o.ordr_text = e.ordr_text
                    left outer join auction.t_oram i on o.ordr_text = i.ordr_text
                    join auction.t_amsp w on o.ordr_fmwh = w.oid and w.amsp_actv='1'
                    join auction.t_amsp c on o.ordr_towh = c.oid and w.amsp_actv='1'
                    where o.act = 2
                    and o.ordr_stas = 'D'
                    and o.ordr_sbmt = 'Y'
                    and to_date(o.udt, 'DD-MM-YY') between to_date('{0}', 'DD-MM-YYYY')
                    and to_date('{1}', 'DD-MM-YYYY')
                    and n.psma_amsp='{2}'
                    group by c.oid",MC.FROM_D,MC.TO_D,clsm.CLSM_FMWH);
                v_ORBL_ORMC_ORAM = db.Database.SqlQuery<V_CLSM_ORMC_ORAM>(sql: Sql).ToList();
            }
            var SubTotal= (from p in v_ORBL_ORMC_ORAM
                             group p by 1 into g
                             select new
                             {
                                 ORMC = g.Sum(x => x.ORMC),
                                 ORAM = g.Sum(x => x.ORAM)                                 
                             }).ToList();
            var GrandTotal = SubTotal.Sum(s => s.ORAM + s.ORMC);


            //create a general Sql_List2 
            List<string> Sql_List2 = new List<string>();
            //for CO Unit update
            if (v_ORBL_ORMC_ORAM != null)
            {
                foreach (var item in v_ORBL_ORMC_ORAM)
                {
                    Sql = string.Format(@"update auction.t_clsm set clsm_ormc='{0}',clsm_oram='{1}' where clsm_text='{2}' and clsm_amsp='{3}'",
                        item.ORMC,item.ORAM,Id,item.OID);
                 //--   Sql_List2.Add(Sql);
                }
            }
            //for WH update
            Sql = string.Format(@"update auction.t_clsm set clsm_ormc='{0}',clsm_oram='{1}' where clsm_text='{2}' and clsm_amsp='{3}'",
                  SubTotal.FirstOrDefault().ORMC, SubTotal.FirstOrDefault().ORAM, Id, clsm.CLSM_FMWH);
            Sql_List2.Add(Sql);






            //int Mx = 100 - orbl.ORBL_MRGP;
            //

            //List<string> Sql_ORBL = new List<string>();

            //string Sql_Insert = string.Format(@"insert into t_orbl (orbl_text,orbl_amsp,orbl_powr,orbl_mrgp,orbl_mrga,orbl_ormc,orbl_oram,orbl_cycl,orbl_mont,orbl_year,act,cdt,cdu,var)
            //    values ('{0}','{1}','{2}','{3}','0','0','0','{4}','{5}','{6}','1',sysdate,'{7}','1')",
            //    Id, orbl.ORDR_FMWH, orbl.ORBL_POWR, orbl.ORBL_MRGP, orbl.ORBL_CYCL, orbl.ORDR_CYCL, dt.Year, Usr);
            ////  Sql_ORBL.Add(Sql_Insert);

            //string Sql_ORMC_ORAM = string.Format(@"select c.oid, sum (nvl (e.ormc_hour, 0) * nvl (e.ormc_cost, 0)) ormc,
            //        sum (nvl (i.oram_qty, 0) * nvl (i.oram_rate, 0)) oram --into orm_amnt,ora_amnt
            //        from auction.t_ordr o left outer join auction.t_ormc e on o.ordr_text = e.ordr_text
            //        left outer join auction.t_oram i on o.ordr_text = i.ordr_text
            //        join auction.t_amsp w on o.ordr_fmwh = w.oid
            //        join auction.t_amsp c on o.ordr_towh = c.oid
            //        where o.act = 2
            //        and o.ordr_stas = 'D'
            //        and o.ordr_sbmt = 'Y'
            //        and to_date(o.udt,'DD-MM-YY') between to_date('{0}','DD-MM-YYYY')
            //        and to_date('{1}','DD-MM-YYYY')
            //        and w.oid='{2}'
            //        group by c.oid", MC.FROM_D, MC.TO_D, orbl.ORDR_FMWH);

            //List<T_PSMA> t_PSMA = new List<T_PSMA>();
            //List<V_ORBL_ORMC_ORAM> v_ORBL_ORMC_ORAM = new List<V_ORBL_ORMC_ORAM>();
            //using (auctionDbContext db = new auctionDbContext())
            //{
            //    t_PSMA = db.T_PSMA.Where(w => w.PSMA_AMSP == orbl.ORDR_FMWH && w.AMSP_ACTV == "1").ToList();
            //    v_ORBL_ORMC_ORAM = db.Database.SqlQuery<V_ORBL_ORMC_ORAM>(sql: Sql_ORMC_ORAM).ToList();
            //}
            //foreach (var item in t_PSMA)
            //{
            //    Sql_Insert = string.Format(@"insert into t_orbl (orbl_text,orbl_amsp,orbl_powr,orbl_mrgp,orbl_mrga,orbl_ormc,orbl_oram,orbl_cycl,orbl_mont,orbl_year,act,cdt,cdu,var)
            //    values ('{0}','{1}','{2}','{3}','0','0','0','{4}','{5}','{6}','1',sysdate,'{7}','1')",
            //                Id, item.AMSP_PSMA, "0", orbl.ORBL_MRGP, orbl.ORBL_CYCL, orbl.ORDR_CYCL, dt.Year, Usr);
            //    //Sql_ORBL.Add(Sql_Insert);
            //}

            //foreach (var item in v_ORBL_ORMC_ORAM)
            //{
            //    Sql_Insert = string.Format(@"update auction.t_orbl set orbl_ormc='{0}',orbl_oram='{1}' where orbl_amsp='{2}' and orbl_mont='{3}' and orbl_year='{4}'", item.ORMC, item.ORAM, item.OID, orbl.ORDR_CYCL, dt.Year);
            //    // Sql_ORBL.Add(Sql_Insert);
            //}

            //Sql_Insert = string.Format(@"declare 
            //            orm_amnt number(10,2);
            //            ora_amnt number(10,2);
            //            begin      
            //    select sum (d.orbl_ormc), sum (d.orbl_oram) into orm_amnt,ora_amnt
            //    from auction.t_orbl d
            //    join auction.t_psma w  on d.orbl_amsp=w.amsp_psma 
            //    where d.orbl_mont = '{0}' and d.orbl_year = '{1}'
            //    and w.psma_amsp='{2}'
            //    group by d.orbl_text;
            //    update auction.t_orbl set orbl_ormc=orm_amnt,orbl_oram=ora_amnt,udt=sysdate where orbl_amsp='{3}' and orbl_text='{4}';
            //    end commit;", orbl.ORDR_CYCL, dt.Year, orbl.ORDR_FMWH, orbl.ORDR_FMWH, Id);
            ////  Sql_ORBL.Add(Sql_Insert);

            ////string Sql_Update = string.Format(@"declare 
            ////            orm_amnt number(10,2);
            ////            ora_amnt number(10,2);
            ////            begin      
            ////            select sum (nvl (e.ormc_hour, 0) * nvl (e.ormc_cost, 0)),
            ////            sum (nvl (i.oram_qty, 0) * nvl (i.oram_rate, 0)) into orm_amnt,ora_amnt
            ////            from auction.t_ordr o left outer join auction.t_ormc e on o.ordr_text = e.ordr_text
            ////            left outer join auction.t_oram i on o.ordr_text = i.ordr_text
            ////            join auction.t_amsp w on o.ordr_fmwh = w.oid
            ////            join auction.t_amsp c on o.ordr_towh = c.oid
            ////            where o.act = 2
            ////            and o.ordr_stas = 'D'
            ////            and o.ordr_sbmt = 'Y'
            ////            and to_date(o.udt,'DD-MM-YY') between to_date('{0}','DD-MM-YYYY')
            ////            and to_date('{1}','DD-MM-YYYY')
            ////            and w.oid='{2}'
            ////            group by w.oid;
            ////            update auction.t_orbl set orbl_ormc=orm_amnt,orbl_oram=ora_amnt,udt=sysdate where orbl_amsp='{3}' and orbl_text='{4}';
            ////            commit;
            ////            update auction.t_ordr o set o.act=3 where o.act = 2 and o.ordr_stas = 'D' and o.ordr_sbmt = 'Y' and to_date(o.udt,'DD-MM-YY') between to_date('{5}','DD-MM-YYYY')
            ////            and to_date('{6}','DD-MM-YYYY');END commit;", MC.FROM_D, MC.TO_D, orbl.ORDR_FMWH, orbl.ORDR_FMWH, Id, MC.FROM_D, MC.TO_D);

            //Sql_Insert = string.Format(@"declare 
            //        total_amnt number(10,2);
            //        total_d number(10,2);
            //        begin
            //        select sum(orbl_ormc+orbl_oram) into total_amnt from auction.t_orbl where orbl_amsp='{0}' and orbl_text='{1}';
            //        select sum(total_amnt/{2})*{3} into total_d from dual;
            //        update  auction.t_orbl set orbl_mrga=total_d where orbl_amsp='{4}' and orbl_text='{5}';
            //        end commit;", orbl.ORDR_FMWH, Id, Mx, orbl.ORBL_MRGP, orbl.ORDR_FMWH, Id);
            ////  Sql_ORBL.Add(Sql_Insert);

            //foreach (var item in v_ORBL_ORMC_ORAM)
            //{
            //    Sql_Insert = string.Format(@"declare 
            //        total_amnt number(10,2);
            //        total_d number(10,2);
            //        begin
            //        select sum(orbl_ormc+orbl_oram) into total_amnt from auction.t_orbl where orbl_amsp='{0}' and orbl_text='{1}';
            //        select sum(total_amnt/{2})*{3} into total_d from dual;
            //        update  auction.t_orbl set orbl_mrga=total_d where orbl_amsp='{4}' and orbl_text='{5}';
            //        end commit;", item.OID, Id, Mx, orbl.ORBL_MRGP, item.OID, Id);
            //    //   Sql_ORBL.Add(Sql_Insert);
            //}

            //foreach (var item in v_ORBL_ORMC_ORAM)
            //{
            //    Sql_Insert = string.Format(@"declare 
            //        total_amnt number(10,2);
            //        total_power number(10,2);
            //        sub_amnt number(10,2);
            //        total_d number(10,2);
            //        begin
            //        select sum (orbl_ormc + orbl_oram), orbl_powr into total_amnt,total_power
            //        from auction.t_orbl where orbl_amsp='{0}' and orbl_text='{1}' group by orbl_powr;
            //        select sum (orbl_ormc + orbl_oram) sub_amnt from auction.t_orbl where orbl_amsp='{2}' and orbl_text='{1}';
            //        select sum(total_amnt/sub_amnt)*total_power into total_d from dual;
            //        update  auction.t_orbl set orbl_mrga=total_d where orbl_amsp='{4}' and orbl_text='{5}';
            //        end commit;", orbl.ORDR_FMWH, Id, item.OID, Id);
            //    //   Sql_ORBL.Add(Sql_Insert);
            //}

            using (auctionDbContext db = new auctionDbContext())
            {
                try
                {
                    foreach (string sql in Sql_List)
                    {
                        int i = db.Database.ExecuteSqlCommand(sql: sql);
                        if (i == 0)
                        {
                            return 0;
                        }
                    }
                    foreach (string sql in Sql_List2)
                    {
                        int i = db.Database.ExecuteSqlCommand(sql: sql);
                        if (i == 0)
                        {
                            return 0;
                        }
                    }
                }
                catch (Exception m)
                {
                    return 2;
                }

            }
            return 1;
        }

        public List<T_CLSM> ORBL_List(string ORBL_AMSP, string ORDR_CYCL)
        {
            DateTime dt = DateTime.Now;
            string Sql = string.Format(@"select o.orbl_text,w.amsp_name orbl_amsp,o.orbl_powr,o.orbl_mrgp,o.orbl_mrga,o.orbl_ormc,o.orbl_oram,o.orbl_cycl,o.orbl_mont,o.orbl_year
            from auction.t_orbl o
            join auction.t_amsp w on o.orbl_amsp=w.oid
            join auction.t_psma a on o.orbl_amsp=a.amsp_psma
            where a.psma_amsp= '{0}'
            and o.orbl_mont='{1}'
            and o.orbl_year='{2}'
            and a.amsp_actv='1'", ORBL_AMSP, ORDR_CYCL, dt.Year);
            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.Database.SqlQuery<T_CLSM>(sql: Sql).ToList();
                if (_data != null)
                {
                    return _data;
                }
                else
                {
                    return new List<T_CLSM>();
                }
            }
        }
    }
}
