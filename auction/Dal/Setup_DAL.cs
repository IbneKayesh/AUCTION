using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auction.Dal
{
    public class Setup_DAL
    {
        public bool Add_Con_Item(string amim_text, string ORDR_FMWH)
        {
            string Sql = string.Format(@"merge into auction.t_amim tgt
                        using (select i.oid, amim_text, amim_name, amim_amig, amim_amic, amim_amit,
                            amim_amsu, amim_amlu, amim_amuf, amim_actv, amim_slmn, amim_slmx,
                            amim_slrd, amim_sleo, amim_seqn, i.iuser, i.euser, i.idat, i.edat,
                            i.ver, i.udt, amim_impt, amim_flag, amim_spcf, amim_glid
                            from cm.t_amim i join cm.t_amwi w
                            on i.oid = w.amwi_amim
                            and w.amwi_actv = '1'
                            and w.amwi_amsp = '{1}'
                            where amim_text = '{0}' and amim_actv = '1') src
                        on (tgt.oid = src.oid)
                        when matched then update set
                        tgt.amim_name=src.amim_name,
                                    tgt.amim_amig=src.amim_amig,
                                    tgt.amim_amic=src.amim_amic,
                                    tgt.amim_amit=src.amim_amit,
                                    tgt.amim_amsu=src.amim_amsu,
                                    tgt.amim_amlu=src.amim_amlu,
                                    tgt.amim_amuf=src.amim_amuf,
                                    tgt.amim_actv=src.amim_actv,
                                    tgt.amim_slmn=src.amim_slmn,
                                    tgt.amim_slmx=src.amim_slmx,
                                    tgt.amim_slrd=src.amim_slrd,
                                    tgt.amim_sleo=src.amim_sleo,
                                    tgt.amim_seqn=src.amim_seqn,
                                    tgt.iuser=src.iuser,
                                    tgt.euser=src.euser,
                                    tgt.idat=src.idat,
                                    tgt.edat=src.edat,
                                    tgt.ver=src.ver,
                                    tgt.udt=src.udt,
                                    tgt.amim_impt=src.amim_impt,
                                    tgt.amim_flag=src.amim_flag,
                                    tgt.amim_spcf=src.amim_spcf,
                                    tgt.amim_glid=src.amim_glid
                        when not matched then 
                        insert (
                                        tgt.oid,
                                        tgt.amim_text,
                                        tgt.amim_name,
                                        tgt.amim_amig,
                                        tgt.amim_amic,
                                        tgt.amim_amit,
                                        tgt.amim_amsu,
                                        tgt.amim_amlu,
                                        tgt.amim_amuf,
                                        tgt.amim_actv,
                                        tgt.amim_slmn,
                                        tgt.amim_slmx,
                                        tgt.amim_slrd,
                                        tgt.amim_sleo,
                                        tgt.amim_seqn,
                                        tgt.iuser,
                                        tgt.euser,
                                        tgt.idat,
                                        tgt.edat,
                                        tgt.ver,
                                        tgt.udt,
                                        tgt.amim_impt,
                                        tgt.amim_flag,
                                        tgt.amim_spcf,
                                        tgt.amim_glid
                                       )
                                values (
                                        src.oid,
                                        src.amim_text,
                                        src.amim_name,
                                        src.amim_amig,
                                        src.amim_amic,
                                        src.amim_amit,
                                        src.amim_amsu,
                                        src.amim_amlu,
                                        src.amim_amuf,
                                        src.amim_actv,
                                        src.amim_slmn,
                                        src.amim_slmx,
                                        src.amim_slrd,
                                        src.amim_sleo,
                                        src.amim_seqn,
                                        src.iuser,
                                        src.euser,
                                        src.idat,
                                        src.edat,
                                        src.ver,
                                        src.udt,
                                        src.amim_impt,
                                        src.amim_flag,
                                        src.amim_spcf,
                                        src.amim_glid
                                        )", amim_text, ORDR_FMWH);

            using (auctionDbContext db = new auctionDbContext())
            {
                try
                {
                    int i = db.Database.ExecuteSqlCommand(sql: Sql);
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception) { return false; }
            }
        }

        public List<T_AMIM> T_AMIM_LIST(string amim_text)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"select oid,amim_text,amim_name,amim_amsu,amim_actv from auction.t_amim where amim_text='{0}'", amim_text);
                var _data = db.Database.SqlQuery<T_AMIM>(sql: Sql).ToList();
                return _data;
            }
        }

        public bool Add_Con_Item_Auto (string ORDR_FMWH)
        {
            string Sql = string.Format(@"insert into auction.t_amim select * from cm.t_amim where oid in (
                select distinct(amim_text) from auction.t_oram i 
                join auction.t_ordr o on i.ordr_text=o.ordr_text and o.ordr_fmwh='{0}'
                where i.cdt between ADD_MONTHS(sysdate,-2) and sysdate
                and amim_text not in (select oid from auction.t_amim)
                )", ORDR_FMWH);
            using (auctionDbContext db = new auctionDbContext())
            {
                try
                {
                    int i = db.Database.ExecuteSqlCommand(sql: Sql);
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception) { return false; }
            }
        }
    }
}