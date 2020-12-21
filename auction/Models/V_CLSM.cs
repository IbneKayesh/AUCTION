using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class V_CLSM
    {
        public string CLSM_FMWH { get; set; }
        [Required]
        public List<T_CLSM_V> T_CLSM_V { get; set; }
    }

    public class T_CLSM_V
    {
        public string CLSM_FMWH { get; set; }
        public string CLSM_TYPE { get; set; }
        public string CLSD_CLST { get; set; }
        public decimal CLSD_AMNT { get; set; }
        public string CLSM_CYCL { get; set; }
        public string CLSM_MONT { get; set; }
        public string CLSM_YEAR { get; set; }
    }
    public class V_CLSM_ORMC_ORAM
    {
        public string OID { get; set; }
        public decimal ORMC { get; set; }
        public decimal ORAM { get; set; }
    }
    
}