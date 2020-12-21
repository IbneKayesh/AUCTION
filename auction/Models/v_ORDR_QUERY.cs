using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class v_ORDR_QUERY
    {
        public Nullable<DateTime> fromDate { get; set; }
        public Nullable<DateTime> toDate { get; set; }
        public string ORDR_TEXT { get; set; }
        public string ORDR_NAME { get; set; }
        [Required]
        public string ORDR_FMWH { get; set; }
        [Required]
        public string ORDR_TOWH { get; set; }
        public string ORDR_ORTP { get; set; }
        public string ORDR_STAS { get; set; }
        [Required]
        public string QUERY_NAME { get; set; }
    }
}