using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class V_ORDR_ITEM
    {
        public string AMIM_TEXT { get; set; }
        public string AMIM_NAME { get; set; }
        public string AMSU_NAME { get; set; }
        public decimal ORAM_QTY { get; set; }
        public decimal ORAM_RATE { get; set; }
        public string ORAM_DESC { get; set; }
    }
}