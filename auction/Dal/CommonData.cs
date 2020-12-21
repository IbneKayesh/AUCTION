using auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auction.Dal
{
    public class CommonData
    {
        public int Table_Sequence(string ColumnName, string TableName)
        {
            int last_number = 0;
            string Sql = string.Format(@"select nvl(max(to_number({0})),0){1} from {2}", ColumnName, ColumnName, TableName);
            using (auctionDbContext db = new auctionDbContext())
            {
                last_number = db.Database.SqlQuery<int>(Sql).ToList().FirstOrDefault();
            }
            return last_number + 1;
        }
        public string Ordr_Name(int No)
        {
            string Id = "ON" + No.ToString().PadLeft(10, '0');
            return Id;
        }
        public string Ordr_Text(int No, string OrderName)
        {
            string Id = "OT" + No.ToString().PadLeft(3, '0');
            return Id + OrderName;
        }
        public string Mold_Text(int No)
        {
            return "MLD" + No.ToString().PadLeft(7, '0');
        }
        public string Mclt_Text(int No)
        {
            return "MCLT" + No.ToString().PadLeft(6, '0');
        }

        public Month_Cycle _Month_Cycle(string CLSM_CYCL, int Year, string Mont)
        {
            string TO = CLSM_CYCL.Substring(3, 2)+"-" + Mont + "-" + Year;

            string FROM = string.Empty;
            if (Mont == "01")
            {
                Mont = "12";
                Year = Year - 1;
            }
            else
            {
                Mont =  (Convert.ToInt32(Mont) - 1).ToString().PadLeft(2,'0');
            }

            FROM = CLSM_CYCL.Substring(0, 2) + "-" + Mont + "-" + Year;
                        
            Month_Cycle mc = new Month_Cycle();
            mc.FROM_D = FROM;
            mc.TO_D = TO;
            return mc;
        }

        public Oracle_Date Oracle_Date_Format(DateTime _fromDate, DateTime _toDate)
        {
            Oracle_Date new_date = new Oracle_Date();
            DateTime dt = DateTime.Now;

            string fy = _fromDate.Date.Year.ToString();
            if (fy == "1") {
                fy = dt.Date.Year.ToString();
            }
            string fd = _fromDate.Date.Day.ToString().PadLeft(2, '0');
            string fm = _fromDate.Date.Month.ToString().PadLeft(2, '0');
            new_date.FROM_DATE = fd + "-" + fm + "-" + fy;

            string ty = _toDate.Date.Year.ToString();
            if (ty == "1") {
                ty = dt.Date.Year.ToString();
            }
            string td = _toDate.Date.Day.ToString().PadLeft(2, '0');
            string tm = _toDate.Date.Month.ToString().PadLeft(2, '0');
            new_date.TO_DATE = td + "-" + tm + "-" + ty;
            return new_date;
        }
    }

    public class Oracle_Date
    {
        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }
    }

    public class Month_Cycle
    {
        public string FROM_D { get; set; }
        public string TO_D { get; set; }
    }
}