using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class VW_ORDR_SENDER_EMAIL
    {
        public string ORDR_TEXT { get; set; }
        public string ORDR_PRLM { get; set; }
        public string ORDR_RISR { get; set; }
        public string ORTP_NAME { get; set; }
        public string MOLD_NAME { get; set; }
        public string USER_MAIL { get; set; }
        public string MAIL_TYPE { get; set; }
        public string MAIL_INBX { get; set; }
        public string CO_UNIT { get; set; }
        public string WH_UNIT { get; set; }
        public string CO_ID { get; set; }
        public string WH_ID { get; set; }
    }
}