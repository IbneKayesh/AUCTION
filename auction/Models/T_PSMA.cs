using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_PSMA
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public string AMSP_PSMA { get; set; }
        [Key]
        [Column(Order = 1)]
        [Required]
        public string PSMA_AMSP { get; set; }
        public string AMSP_ACTV { get; set; }
    }
}