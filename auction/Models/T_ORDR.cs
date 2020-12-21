using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class T_ORDR
    {
        [Key]
        public string ORDR_TEXT { get; set; }
        public string ORDR_NAME { get; set; }
        public string ORDR_FMWH { get; set; }
        public string ORDR_TOWH { get; set; }
        public string ORDR_ORTP { get; set; }
        public string ORDR_RISR { get; set; }
        public Nullable<DateTime> ORDR_DATE { get; set; }
        public Nullable<DateTime> ORDR_RDAT { get; set; }
        public Nullable<DateTime> ORDR_DDAT { get; set; }
        public string ORDR_STAS { get; set; }
        public string ORDR_ITEM { get; set; }
        public string ORDR_UNIT { get; set; }
        public Nullable<int> ORDR_QNTY { get; set; }
        public Nullable<decimal> ORDR_RATE { get; set; }
        public string ORDR_PRLM { get; set; }
        public string ORDR_NOTE { get; set; }
        public string ORDR_CMNT { get; set; }
        public string ORDR_SBMT { get; set; }
        public Nullable<int> ORDR_SEQN { get; set; }
        public Nullable<int> ACT { get; set; }
        public Nullable<DateTime> CDT { get; set; }
        public Nullable<DateTime> UDT { get; set; }
        public string CDU { get; set; }
        public string UDU { get; set; }
        public Nullable<int> VAR { get; set; }

    }

    public class V_ORDR
    {
        [Key]
        public string ORDR_TEXT { get; set; }
        public string ORDR_NAME { get; set; }
        public string ORDR_FMWH { get; set; }
        public string ORDR_TOWH { get; set; }
        public string ORDR_ORTP { get; set; }
        public string ORDR_RISR { get; set; }
        public Nullable<DateTime> ORDR_DATE { get; set; }
        public Nullable<DateTime> ORDR_RDAT { get; set; }
        public Nullable<DateTime> ORDR_DDAT { get; set; }
        public string ORDR_STAS { get; set; }
        public string ORDR_ITEM { get; set; }
        public string ORDR_UNIT { get; set; }
        public Nullable<int> ORDR_QNTY { get; set; }
        public Nullable<decimal> ORDR_RATE { get; set; }
        public string ORDR_PRLM { get; set; }
        public string ORDR_NOTE { get; set; }
        public string ORDR_CMNT { get; set; }
        public string ORDR_SBMT { get; set; }
        public Nullable<int> ORDR_SEQN { get; set; }
        public Nullable<int> ACT { get; set; }
        public Nullable<DateTime> CDT { get; set; }
        public Nullable<DateTime> UDT { get; set; }
        public string CDU { get; set; }
        public string UDU { get; set; }
        public Nullable<int> VAR { get; set; }

    }
}