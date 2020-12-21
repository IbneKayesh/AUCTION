using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auction.Dal
{
    public class Accounts_DAL
    {
        public bool SaveRegistrations(AGENTS_NEW agent)
        {
            string Sql = string.Format(@"INSERT INTO KAYESH.AGENTS
                   (AGENT_LOGIN_ID, AGENT_PASSWORD, AGENT_PROPRIETOR_NAME, AGENT_PICTURES, 
                    AGENT_NAME, AGENT_CONTACT, AGENT_WEBSITE, REGISTRATION_NUMBER, AGENT_MEMBER, 
                    CONTACT_PERSON, CONTACT_PERSON_CONTACT, EMAIL_ADDRESS, AGENT_ADDRESS,
                    AGENT_CAPACITY, EMAIL_NOTIFICATIONS, AGENT_STATUS,
                    PC_NAME, CDU, CDT, ACT)VALUES
                   ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',
                    '{13}','{14}','{15}','{16}','{17}',  sysdate, 1)",
                   agent.AGENT_LOGIN_ID, agent.AGENT_PASSWORD, agent.AGENT_PROPRIETOR_NAME, agent.AGENT_PICTURES,
                    agent.AGENT_NAME, agent.AGENT_CONTACT, agent.AGENT_WEBSITE, agent.REGISTRATION_NUMBER, agent.AGENT_MEMBER,
                    agent.CONTACT_PERSON, agent.CONTACT_PERSON_CONTACT, agent.EMAIL_ADDRESS, agent.AGENT_ADDRESS,
                    agent.AGENT_CAPACITY, agent.EMAIL_NOTIFICATIONS, agent.AGENT_STATUS,
                    agent.PC_NAME, agent.CDU);
            try
            {
                using (auctionDbContext db = new auctionDbContext())
                {
                    db.Database.ExecuteSqlCommand(sql: Sql);
                    Sql = string.Format("SELECT AGENT_ID FROM AGENTS WHERE AGENT_LOGIN_ID='{0}'", agent.AGENT_LOGIN_ID);
                    List<string> Sql_List = new List<string>();
                    var _data = db.Database.SqlQuery<Int64>(sql: Sql).ToList();
                    if (_data != null)
                    {
                        foreach (var item in agent.AGENT_COMPANY_T)
                        {
                            Sql = string.Format(@"INSERT INTO AGENTS_COMPANY_TEMP (AGENT_ID,COMPANY_ID) VALUES ('{0}','{1}')", _data.FirstOrDefault(), item.COMPANY_ID);
                            Sql_List.Add(Sql);
                        }
                        foreach (var item in Sql_List)
                        {
                            db.Database.ExecuteSqlCommand(sql: item);
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex) { return false; }
        }


        public List<AGENTS> AGENTS_NEW_LIST(int status)
        {
            List<AGENTS> agent_list = new List<AGENTS>();

            string Sql = string.Format(@"SELECT * FROM AGENTS WHERE AGENT_STATUS={0}", status);
            using (auctionDbContext db = new auctionDbContext())
            {
                agent_list = db.Database.SqlQuery<AGENTS>(sql: Sql).ToList();
            }
            return agent_list;
        }
    }
}