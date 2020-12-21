using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_WCRT
    {
        [Key]
        public string OID { get; set; }
        public string WCRT_AMSP { get; set; }
        public string WCRT_USER { get; set; }
        public string WCRT_ACTV { get; set; }
        public string IUSER { get; set; }
        public string EUSER { get; set; }
        public Nullable<DateTime> IDAT { get; set; }
        public Nullable<DateTime> EDAT { get; set; }
        public Nullable<int> VER { get; set; }
        public Nullable<DateTime> UDT { get; set; }
        public string WCRT_SEQN { get; set; }
        public string WCRT_AMRR { get; set; }
    }
}