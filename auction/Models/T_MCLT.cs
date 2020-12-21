using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_MCLT
    {
        [Key]
        public string MCLT_TEXT { get; set; }
        [Required]
        public string MCLT_NAME { get; set; }
        public string MCLT_MCTP { get; set; }
        [Required]
        public string MCLT_FMWH { get; set; }
        [Required]
        public string MCLT_DESC { get; set; }
        public string MCLT_MODL { get; set; }
        public string MCLT_BRND { get; set; }
        public string MCLT_HIGT { get; set; }
        public string MCLT_WIGT { get; set; }
        public string MCLT_CPTY { get; set; }
        public string MCLT_ORGN { get; set; }
        public Nullable<DateTime> MCLT_YEAR { get; set; }
        public Nullable<decimal> MCLT_PRCE { get; set; }
        [Required]
        public Nullable<decimal> MCLT_COST { get; set; }
        public Nullable<int> MCLT_SEQN { get; set; }
        public Nullable<int> ACT { get; set; }
        public Nullable<DateTime> CDT { get; set; }
        public Nullable<DateTime> UDT { get; set; }
        public string CDU { get; set; }
        public string UDU { get; set; }
        public Nullable<int> VAR { get; set; }

    }
}