using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_USER
    {
        [Key]
        public string USER_TEXT { get; set; }
        [Required]
        public string USER_NAME { get; set; }
        [Required]
        public string USER_PASS { get; set; }
        public string USER_MAIL { get; set; }
        public string USER_MOBL { get; set; }
        public string USER_IMAG { get; set; }
        public Nullable<int> ACT { get; set; }
        public Nullable<DateTime> CDT { get; set; }
        public Nullable<DateTime> UDT { get; set; }
        public string CDU { get; set; }
        public string UDU { get; set; }
        public Nullable<int> VAR { get; set; }
    }
}