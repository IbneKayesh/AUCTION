using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_EMAL
    {
        [Key]
        public string EMAL_USER { get; set; }
        public string EMAL_PASS { get; set; }
        public string EMAL_SRVR { get; set; }
        public string EMAL_PORT { get; set; }
        public string EMAL_SMTP { get; set; }
        public string EMAL_DSPL { get; set; }
    }
}