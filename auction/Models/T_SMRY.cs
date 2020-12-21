using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_SMRY
    {
        [Key]
        public string SMRY_AMSP { get; set; }
        public int SMRY_NSMT { get; set; }
        public int SMRY_NRCV { get; set; }
        public int SMRY_IWRK { get; set; }
        public int SMRY_NBIL { get; set; }
        public int SMRY_NRTP { get; set; }
        public int SMRY_CMPT { get; set; }
        public decimal SMRY_ORMC { get; set; }
        public decimal SMRY_ORAM { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<DateTime> SMRY_FMDT { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<DateTime> SMRY_TODT { get; set; }
        public DateTime UDT { get; set; }
        public string UDU { get; set; }
    }
}