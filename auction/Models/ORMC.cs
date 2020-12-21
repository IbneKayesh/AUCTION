using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class ORMC
    {
        [Required]
        public string ORDR_TEXT { get; set; }
        [Required]
        public List<T_ORMC> T_ORMC { get; set; }
        public string ORDR_XY { get; set; }
    }
}