using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace auction.Dal
{
    public class Email_Sender
    {
        public void WorkOrderSubmit(string ORDR_TEXT)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"select o.ordr_text, o.ordr_prlm,o.ordr_risr, t.ortp_name, d.mold_name,u.user_mail, m.mail_type, m.mail_inbx,
                    c.amsp_name co_unit, w.amsp_name wh_unit, c.oid co_id, w.oid wh_id
                    from auction.t_user u join auction.t_mail m on u.user_text = m.mail_user
                    join auction.t_wcrt wp on u.user_text = wp.wcrt_user
                    join auction.t_ordr o on wp.wcrt_amsp = o.ordr_towh
                    join auction.t_ortp t on o.ordr_ortp = t.ortp_text
                    join auction.t_mold d on o.ordr_item = d.mold_text
                    join auction.t_amsp c on o.ordr_towh = c.oid
                    join auction.t_amsp w on o.ordr_fmwh = w.oid
                    join auction.t_emas s on m.mail_type=s.emas_mail and s.act='1'
                    where u.act = '1' and u.user_mail is not null and m.act = '1'
                    and wp.wcrt_actv = '1'
                    and s.emas_text='ORDER'
                    and o.ordr_text='{0}'", ORDR_TEXT);
                // --and m.mail_type = 'work_ordr' 
                List<VW_ORDR_SENDER_EMAIL> mail_users = db.Database.SqlQuery<VW_ORDR_SENDER_EMAIL>(sql: Sql).ToList();
                if(mail_users.Count >0)
                {
                    List<string> to = new List<string>();
                    List<string> cc = new List<string>();
                    foreach (var item in mail_users.Where(t => t.MAIL_INBX == "TO"))
                    {
                        to.Add(item.USER_MAIL);
                    }
                    foreach (var item in mail_users.Where(t => t.MAIL_INBX == "CC"))
                    {
                        cc.Add(item.USER_MAIL);
                    }
                    foreach (var item in mail_users.Where(t => t.MAIL_INBX == "XX"))
                    {
                        cc.Add(item.USER_MAIL);
                    }
                    VW_ORDR_SENDER_EMAIL _data = mail_users.FirstOrDefault();
                    string body = string.Format(@"<table border='1'><tr><td align='center'>From</td><td align='center'>To</td></tr><tr><td>{0}</td><td>{1}</td></tr><tr><td colspan='2' align='center'>{2}</td></tr><tr><td align='center'>Type</td><td>{3}</td></tr><tr><td align='center'>Work</td><td>{4}</td></tr><tr><td align='center'>Problem</td><td>{5}</td></tr><tr><td colspan='2' align='center'>{6}</td></tr><tr><td colspan='2'  align='center'>{7}</td></tr></table>", _data.CO_UNIT, _data.WH_UNIT, _data.ORDR_TEXT, _data.ORTP_NAME, _data.MOLD_NAME, _data.ORDR_PRLM, _data.ORDR_RISR, System.DateTime.Now);

                    SendEmail(TO_LIST: to, CC_LIST: cc, emailSubject: "New WO : " + ORDR_TEXT, emailBody: body, emailDisplay: "Work Order - auction");
                }
               
            }
        }

        public void WorkOrderTR(string ORDR_TEXT, string WO_TEXT)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                string Sql = string.Format(@"select o.ordr_text, o.ordr_prlm, o.ordr_risr, t.ortp_name, d.mold_name,u.user_mail, m.mail_type, m.mail_inbx,
                    c.amsp_name co_unit, w.amsp_name wh_unit, c.oid co_id, w.oid wh_id
                    from auction.t_user u join auction.t_mail m on u.user_text = m.mail_user
                    join auction.t_wcrt wp on u.user_text = wp.wcrt_user
                    join auction.t_ordr o on wp.wcrt_amsp = o.ordr_towh
                    join auction.t_ortp t on o.ordr_ortp = t.ortp_text
                    join auction.t_mold d on o.ordr_item = d.mold_text
                    join auction.t_amsp c on o.ordr_towh = c.oid
                    join auction.t_amsp w on o.ordr_fmwh = w.oid
                    join auction.t_emas s on m.mail_type=s.emas_mail and s.act='1'
                    where u.act = '1' and u.user_mail is not null and m.act = '1'
                    and wp.wcrt_actv = '1'
                    and s.emas_text='{1}' 
                    and o.ordr_text='{0}'", ORDR_TEXT, WO_TEXT);
                //--and m.mail_type = 'work_ordr'
                List<VW_ORDR_SENDER_EMAIL> mail_users = db.Database.SqlQuery<VW_ORDR_SENDER_EMAIL>(sql: Sql).ToList();
                if (mail_users.Count>0)
                {
                    List<string> to = new List<string>();
                    List<string> cc = new List<string>();
                    foreach (var item in mail_users.Where(t => t.MAIL_INBX == "CC"))
                    {
                        to.Add(item.USER_MAIL);
                    }
                    foreach (var item in mail_users.Where(t => t.MAIL_INBX == "TO"))
                    {
                        cc.Add(item.USER_MAIL);
                    }
                    foreach (var item in mail_users.Where(t => t.MAIL_INBX == "XX"))
                    {
                        cc.Add(item.USER_MAIL);
                    }
                    VW_ORDR_SENDER_EMAIL _data = mail_users.FirstOrDefault();
                    string body = string.Format(@"<table border='1'><tr><td align='center'>From</td><td align='center'>To</td></tr><tr><td>{1}</td><td>{0}</td></tr><tr><td colspan='2' align='center'>{2}</td></tr><tr><td align='center'>Type</td><td>{3}</td></tr><tr><td align='center'>Work</td><td>{4}</td></tr><tr><td align='center'>Problem</td><td>{5}</td></tr><tr><td colspan='2' align='center'>{6}</td></tr><tr><td colspan='2'  align='center'>{7}</td></tr></table>", _data.CO_UNIT, _data.WH_UNIT, _data.ORDR_TEXT, _data.ORTP_NAME, _data.MOLD_NAME, _data.ORDR_PRLM, _data.ORDR_RISR, System.DateTime.Now);

                    SendEmail(TO_LIST: to, CC_LIST: cc, emailSubject: WO_TEXT + " WO : " + ORDR_TEXT, emailBody: body, emailDisplay: "Work Order - auction");
                }

            }
        }

        public void R_Unit_EC_Summary_Email_Sent(List<R_Unit_EC_Summary_C> _Data, R_EXPENSES _R)
        {
            string Usr = System.Web.HttpContext.Current.Session["UserId"].ToString();

            foreach (R_Unit_EC_Summary_C EC_SUM in _Data)
            {
                string Sql = string.Format(@"select u.user_mail, m.mail_type, m.mail_inbx
                        from auction.t_user u join auction.t_mail m on u.user_text = m.mail_user
                        join auction.t_wcrt wp on u.user_text = wp.wcrt_user
                        where u.act = 1 and u.user_mail is not null and m.act = 1
                        and wp.wcrt_actv = '1'
                        and m.mail_type = 'WHEC_SMRY'
                        and wp.wcrt_amsp='{0}'", EC_SUM.CO_ID);
                string Body = string.Format(@"<table border='1'><tr><td>From date</td><td>{0}</td></tr><tr><td>To date</td><td>{1}</td></tr><tr><td>Wh</td><td>{2}</td></tr><tr><td>Unit</td><td>{3}</td></tr><tr><td>Expense</td><td>{4}</td></tr><tr><td>Consumption</td><td>{5}</td></tr><tr><td>Sent date</td><td>{6}</td></tr><tr><td>Sent by</td><td>{7}</td></tr></table>",
                    _R.FROM_DATE.ToString("dddd, dd MMMM yyyy"),
                    _R.TO_DATE.ToString("dddd, dd MMMM yyyy"),
                    EC_SUM.WH_NAME,
                    EC_SUM.CO_NAME,
                    EC_SUM.EXP_AMNT,
                    EC_SUM.CON_AMNT,
                    DateTime.Now,
                    Usr);
                List<string> to = new List<string>();
                List<string> cc = new List<string>();
                using (auctionDbContext db = new auctionDbContext())
                {
                    List<V_EMAIL> mail_users = db.Database.SqlQuery<V_EMAIL>(sql: Sql).ToList();
                    foreach (var item in mail_users.Where(t => t.MAIL_INBX == "TO"))
                    {
                        to.Add(item.USER_MAIL);
                    }
                    foreach (var item in mail_users.Where(t => t.MAIL_INBX == "CC"))
                    {
                        cc.Add(item.USER_MAIL);
                    }
                    foreach (var item in mail_users.Where(t => t.MAIL_INBX == "XX"))
                    {
                        cc.Add(item.USER_MAIL);
                    }
                }
                string Subj = string.Format(@"EC Summary : {0} to {1}", _R.FROM_DATE.ToString("dddd, dd MMMM yyyy"), _R.TO_DATE.ToString("dddd, dd MMMM yyyy"));
                SendEmail(TO_LIST: to, CC_LIST: cc, emailSubject: Subj, emailBody: Body, emailDisplay: "EC Summary - auction");
            }
        }

        public bool SendEmail(List<string> TO_LIST, List<string> CC_LIST, string emailSubject, string emailBody, string emailDisplay)
        {
            using (auctionDbContext db = new auctionDbContext())
            {
                T_EMAL emal = db.T_EMAL.Where(s => s.EMAL_USER == "rsms").FirstOrDefault();

                string Server_ID = emal.EMAL_SRVR;
                int Server_Port = Convert.ToInt32(emal.EMAL_PORT);
                string Sender_Display_Name = emailDisplay;
                string Sender_Account = emal.EMAL_SMTP;
                string Sender_Credential = emal.EMAL_PASS;
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(Sender_Account, Sender_Display_Name);
                foreach (var item in TO_LIST)
                {
                    mail.To.Add(item);
                }
                foreach (var item in CC_LIST)
                {
                    mail.CC.Add(item);
                }

                mail.Subject = emailSubject;
                mail.Body = emailBody;
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Host = Server_ID;
                    smtp.Port = Server_Port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(Sender_Account, Sender_Credential);
                    smtp.EnableSsl = true;
                    smtp.Timeout = 600000;
                }
                try
                {
                    NEVER_EAT_POISON_Disable_CertificateValidation();
                    smtp.Send(mail);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        static void NEVER_EAT_POISON_Disable_CertificateValidation()
        {
            // Disabling certificate validation can expose you to a man-in-the-middle attack
            // which may allow your encrypted message to be read by an attacker
            // https://stackoverflow.com/a/14907718/740639
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                )
                {
                    return true;
                };
        }
    }
}