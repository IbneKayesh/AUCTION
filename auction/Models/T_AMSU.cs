using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_AMSU
    {

        [Key]
        public string OID { get; set; }
        public string AMSU_TEXT { get; set; }
        public string AMSU_NAME { get; set; }
    }
}