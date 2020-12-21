using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_CLSM
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public string CLSM_TEXT { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required]
        public string CLSM_AMSP { get; set; }
        public decimal CLSM_POWR { get; set; }
        public decimal CLSM_ORMC { get; set; }
        public decimal CLSM_ORAM { get; set; }
        public string CLSM_CYCL { get; set; }
        public string CLSM_MONT { get; set; }
        public string CLSM_YEAR { get; set; }

    }
}