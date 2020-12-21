using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_MOLD
    {
        [Key]
        public string MOLD_TEXT { get; set; }
        [Required]
        public string MOLD_NAME { get; set; }
        [Required]
        public string MOLD_AMSU { get; set; }
        [Required]
        public string MOLD_AMSP { get; set; }
        public string MOLD_TAAG { get; set; }
        public string MOLD_DESC { get; set; }
        public string MOLD_CVTY { get; set; }
        public string MOLD_WTRL { get; set; }
        public string MOLD_HYDL { get; set; }
        public string MOLD_SIZE { get; set; }
        public Nullable<DateTime> MOLD_MFGD { get; set; }
        public string MOLD_STUS { get; set; }
        public string MOLD_IMAG { get; set; }
        public Nullable<int> MOLD_SEQN { get; set; }
        public Nullable<int> ACT { get; set; }
        public Nullable<DateTime> CDT { get; set; }
        public Nullable<DateTime> UDT { get; set; }
        public string CDU { get; set; }
        public string UDU { get; set; }
        public Nullable<int> VAR { get; set; }

    }
}