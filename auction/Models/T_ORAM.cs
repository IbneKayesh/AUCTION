using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_ORAM
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public string ORDR_TEXT { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required]
        public string AMIM_TEXT { get; set; }
        [Required]
        public decimal ORAM_QTY { get; set; }
        public string ORAM_DESC { get; set; }
        public decimal ORAM_RATE { get; set; }
        public Nullable<int> ACT { get; set; }
        public Nullable<DateTime> CDT { get; set; }
        public Nullable<DateTime> UDT { get; set; }
        public string CDU { get; set; }
        public string UDU { get; set; }
        public Nullable<int> VAR { get; set; }
    }
}