using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_CLST
    {
        [Key]
        public string CLST_TEXT { get; set; }
        public string CLST_NAME { get; set; }
    }
}