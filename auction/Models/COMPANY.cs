using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class COMPANY : ExtColumns
    {
        public Int64 COMPANY_ID { get; set; }
        public string COMPANY_SHORT_NAME { get; set; }
        public string COMPANY_NAME { get; set; }
        public string COMPANY_VAT { get; set; }
        public string TIN_NUMBER { get; set; }
        public string IRS_NUMBER { get; set; }
        public string COMPANY_ADDRESS { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string CONTACT_PERSON_NAME { get; set; }
        public string CONTACT_PERSON_CONTACT_NO { get; set; }
        public string VAT_IMAGE { get; set; }
        public string TIN_IMAGE { get; set; }

    }
}