using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auction.Models
{
    public class T_CLSD
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public string CLSD_CLST { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required]
        public string CLSD_CLSM_TEXT { get; set; }
        [Key]
        [Column(Order = 3)]
        [Required]
        public string CLSD_CLSM_AMSP { get; set; }
        public decimal CLSD_AMNT { get; set; }
        public string CLSD_DESC { get; set; }
    }
}