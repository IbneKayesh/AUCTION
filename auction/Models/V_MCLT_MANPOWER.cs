using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class V_MCLT_MANPOWER
    {
        [Required]
        public string MCLT_TEXT { get; set; }
        [Required]
        public string MCLT_NAME { get; set; }
        [Required]
        public string MCLT_DESC { get; set; }
        public string MCTP_NAME { get; set; }
        public string AMSP_NAME { get; set; }
        public Nullable<DateTime> MCTP_YEAR { get; set; }
        public Nullable<decimal> MCLT_PRCE { get; set; }
        [Required]
        public Nullable<decimal> MCLT_COST { get; set; }
        public Nullable<int> ACT { get; set; }
    }
}