using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
 
    public class v_wkod_master
    {
        [Required]
        public string ORDR_FMWH { get; set; }
        [Required]
        public string ORDR_TOWH { get; set; }
        public string ORDR_RISR { get; set; }
        public DateTime ORDR_DATE { get; set; }
        [Required]
        public List<v_wkod_item> V_WKOD_ITEM { get; set; }
    }
    public class v_wkod_item
    {
        [Required]
        public string ORDR_ORTP { get; set; }
        [Required]
        public string ORDR_ITEM { get; set; }
        [Required]
        public string ORDR_UNIT { get; set; }
        public string ORDR_QNTY { get; set; }
        public decimal ORDR_RATE { get; set; }
        [Required]
        public string ORDR_PRLM { get; set; }
        public string ORDR_NOTE { get; set; }

    }

    public class v_ordr_edit
    {
        [Required]
        public string ORDR_TEXT { get; set; }
        [Required]
        public string ORDR_TOWH { get; set; }
        public string ORDR_ITEM { get; set; }
        [Required]
        public string ORDR_ORTP { get; set; }
        [Required]
        public string ORDR_RISR { get; set; }
        [Required]
        public string ORDR_PRLM { get; set; }
        public string ORDR_NOTE { get; set; }
        public string ORDR_CMNT { get; set; }
    }

    public class v_ordr_received
    {
        [Required]
        public string ORDR_TEXT { get; set; }
        [Required]
        public string ORDR_CMNT { get; set; }
    }
}