using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_AMIM
    {
        [Key]
        public string OID { get; set; }
        public string AMIM_TEXT { get; set; }
        public string AMIM_NAME { get; set; }
        public string AMIM_AMSU { get; set; }
        public string AMIM_ACTV { get; set; }
    }
}