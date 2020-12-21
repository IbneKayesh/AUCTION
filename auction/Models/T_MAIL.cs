using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_MAIL
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public string MAIL_USER { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required]
        public string MAIL_TYPE { get; set; }
        [Required]
        public string MAIL_INBX { get; set; }
        public Nullable<int> ACT { get; set; }
        public Nullable<DateTime> CDT { get; set; }
        public Nullable<DateTime> UDT { get; set; }
        public string CDU { get; set; }
        public string UDU { get; set; }
        public Nullable<int> VAR { get; set; }
    }
}