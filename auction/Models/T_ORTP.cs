using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_ORTP
    {
        [Key]
        public string ORTP_TEXT { get; set; }
        public string ORTP_NAME { get; set; }
    }
}