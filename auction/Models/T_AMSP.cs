using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_AMSP
    {
        [Key]
        public string OID { get; set; }
        public string AMSP_TEXT { get; set; }
        public string AMSP_NAME { get; set; }
        public int AMSP_FLAG { get; set; }
        public string AMSP_ACTV { get; set; }
    }
}