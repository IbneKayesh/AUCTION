using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_MCTP
    {
        [Key]
        public string MCTP_TEXT { get; set; }
        public string MCTP_NAME { get; set; }
    }
}