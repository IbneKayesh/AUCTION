using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auction.Dal
{
    public class User_DAL
    {
        public void User(T_USER u)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            using (auctionDbContext db = new auctionDbContext())
            {
                T_USER _u = db.T_USER.Find(u.USER_TEXT);
                if (_u == null)
                {
                    u.ACT = 1;
                    u.CDT = DateTime.Now;
                    u.CDU = Usr;
                    db.T_USER.Add(u);
                    db.SaveChanges();
                }
            }
        }

        public List<T_USER> UserList(string USER_TEXT)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.T_USER.Where(u => u.USER_TEXT == USER_TEXT).ToList();
                return _data;
            }
        }
        public List<V_MENU> MenuList(string USER_TEXT)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"select u.user_text, u.act, p.mnup_name, p.mnup_icon, c.mnuc_text,
                    c.mnuc_name, c.mnuc_icon, c.mnuc_note
                    from auction.t_mnup p join auction.t_mnuc c on p.mnup_text = c.mnuc_mnup
                    left outer join auction.t_usmc u on c.mnuc_text = u.mnuc_text and u.user_text='{0}'
                    where p.act = 1 and c.act = 1
                    order by p.mnup_text,c.mnuc_text", USER_TEXT);
                var _data = db.Database.SqlQuery<V_MENU>(sql: Sql).ToList();
                return _data;
            }
        }

        public string UserName(string USER_TEXT)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"select user_name from auction.t_user where user_text='{0}'", USER_TEXT);
                var _data = db.Database.SqlQuery<string>(sql: Sql).FirstOrDefault();
                return _data;
            }

        }

        public bool UserMenu(string USER_TEXT, int MENU_TEXT)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            bool result = false;
            using (auctionDbContext db = new auctionDbContext())
            {
                T_USER exist = db.T_USER.Find(USER_TEXT);
                if (exist == null || exist.ACT == 0)
                {
                    return result;
                }

                T_USMC _data = db.T_USMC.Find(USER_TEXT, MENU_TEXT);
                if (_data != null)
                {
                    if (_data.ACT == 1)
                    {
                        _data.ACT = 0;
                    }
                    else
                    {
                        _data.ACT = 1;
                    }
                    _data.UDT = DateTime.Now;
                    _data.CDU = Usr;
                    db.Entry(_data).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return result = true;
                }
                else
                {
                    T_USMC _n_um = new T_USMC();
                    _n_um.USER_TEXT = USER_TEXT;
                    _n_um.MNUC_TEXT = MENU_TEXT;
                    _n_um.ACT = 1;
                    _n_um.CDT = DateTime.Now;
                    _n_um.CDU = Usr;
                    db.T_USMC.Add(_n_um);
                    db.SaveChanges();
                    return result = true;
                }
            }
        }

        public bool UserLogin(V_USER_LOGIN L)
        {
            try
            {

                bool result = false;
                using (auctionDbContext db = new auctionDbContext())
                {
                    result = db.T_USER.Where(s => s.USER_TEXT == L.USER_TEXT && s.USER_PASS == L.USER_PASS && s.ACT == 1).Any();
                }
                return result;
            }
            catch (Exception ex)
            {
                string Mess = ex.Message;
                return false;
                    
            }
        }
        public List<MNUP_MNUC> SessionMenuList(string USER_TEXT)
        {
            string Sql = String.Format(@"select p.mnup_text,p.mnup_name,p.mnup_icon,
            c.mnuc_text,c.mnuc_name,c.mnuc_cont,c.mnuc_actn,c.mnuc_icon
             from auction.t_mnup p
            join auction.t_mnuc c on p.mnup_text=c.mnuc_mnup and c.act=1
            join auction.t_usmc u on c.mnuc_text=u.mnuc_text and u.act=1
            where u.user_text='{0}' and p.act=1 order by p.mnup_text,c.mnuc_text", USER_TEXT);
            using (auctionDbContext db = new auctionDbContext())
            {
                try
                {
                    var _data = db.Database.SqlQuery<MNUP_MNUC>(sql: Sql).ToList();
                    return _data;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public List<V_AMSP_USER> WhCoList(string USER_TEXT, string AMSP_FLAG)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"select u.wcrt_user,u.wcrt_actv,w.oid,w.amsp_name
                    from auction.t_amsp w
                    left outer join auction.t_wcrt u on w.oid = u.wcrt_amsp and u.wcrt_user='{0}'
                    where w.amsp_actv = 1
                    and w.amsp_flag='{1}' 
                    order by u.wcrt_amsp", USER_TEXT, AMSP_FLAG);
                var _data = db.Database.SqlQuery<V_AMSP_USER>(sql: Sql).ToList();
                return _data;
            }
        }

        public bool UserWhCoAction(V_AMSP_USER UM)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            bool result = false;
            using (auctionDbContext db = new auctionDbContext())
            {
                T_USER exist = db.T_USER.Find(UM.WCRT_USER);
                if (exist == null || exist.ACT == 0)
                {
                    return result;
                }

                T_WCRT _data = db.T_WCRT.Where(p => p.WCRT_AMSP == UM.AMSP_NAME && p.WCRT_USER == UM.WCRT_USER).FirstOrDefault();
                if (_data != null)
                {
                    if (_data.WCRT_ACTV == "1")
                    {
                        _data.WCRT_ACTV = "0";
                    }
                    else
                    {
                        _data.WCRT_ACTV = "1";
                    }
                    _data.UDT = DateTime.Now;
                    _data.EDAT = DateTime.Now;
                    _data.EUSER = Usr;
                    _data.VER = _data.VER + 1;
                    db.Entry(_data).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return result = true;
                }
                else
                {
                    CommonData cd = new CommonData();
                    int SEQN = cd.Table_Sequence("wcrt_seqn", "auction.t_wcrt");
                    T_WCRT _n = new T_WCRT();
                    _n.OID = UM.WCRT_USER + "x" + SEQN.ToString();
                    _n.WCRT_AMSP = UM.AMSP_NAME;
                    _n.WCRT_USER = UM.WCRT_USER;
                    _n.WCRT_ACTV = "1";
                    _n.IUSER = Usr;
                    _n.IDAT = DateTime.Now;
                    _n.UDT = DateTime.Now.Date;
                    _n.VER = 1;
                    _n.WCRT_SEQN = SEQN.ToString();
                    _n.WCRT_AMRR = "0";
                    db.T_WCRT.Add(_n);
                    db.SaveChanges();
                    return result = true;
                }
            }
        }

        public List<T_MAIL> EMailRecipient(string USER_TEXT)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                var _data = db.T_MAIL.Where(x => x.MAIL_USER == USER_TEXT).ToList();
                return _data;
            }
        }

        public bool UserMailRecipient(string AT, string USER_TEXT, string MAIL_TYPE, string MAIL_INBX)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();
            using (auctionDbContext db = new auctionDbContext())
            {
                if (AT == "NEW")
                {
                    T_MAIL m = new T_MAIL();
                    m.MAIL_USER = USER_TEXT;
                    m.MAIL_TYPE = MAIL_TYPE;
                    m.MAIL_INBX = MAIL_INBX;
                    m.ACT = 1;
                    m.CDU = Usr;
                    m.CDT = DateTime.Now;
                    m.VAR = 1;
                    db.T_MAIL.Add(m);
                }
                else if (AT == "TC")
                {
                    T_MAIL m = db.T_MAIL.Find(USER_TEXT, MAIL_TYPE);
                    m.MAIL_TYPE = MAIL_TYPE;
                    m.MAIL_INBX = MAIL_INBX;
                    m.UDU = Usr;
                    m.UDT = DateTime.Now;
                    m.VAR = m.VAR + 1;
                    db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                }
                else if (AT == "OO")
                {
                    T_MAIL m = db.T_MAIL.Find(USER_TEXT, MAIL_TYPE);
                    if (m.ACT == 1)
                    {
                        m.ACT = 0;
                    }
                    else
                    {
                        m.ACT = 1;
                    }
                    m.UDU = Usr;
                    m.UDT = DateTime.Now;
                    m.VAR = m.VAR + 1;
                    db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    return false;
                }
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public bool ChangePassword(V_USER_PASS_CHANGE ucp)
        {
            bool result = false;
            using (auctionDbContext db = new auctionDbContext())
            {
                T_USER user = db.T_USER.Where(u => u.ACT == 1 && u.USER_TEXT == ucp.USER_TEXT && u.USER_PASS == ucp.USER_PASS).FirstOrDefault();
                if (user != null)
                {
                    user.USER_PASS = ucp.CONF_PASS;
                    user.UDU = ucp.USER_TEXT;
                    user.UDT = DateTime.Now;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return result;
                }
            }
        }
    }
}