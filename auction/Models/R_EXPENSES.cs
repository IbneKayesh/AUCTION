using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class R_EXPENSES
    {
        public DateTime FROM_DATE { get; set; }
        public DateTime TO_DATE { get; set; }
        public string ORDR_FMWH { get; set; }
        public string ORDR_TOWH { get; set; }
    }
    public class R_Unit_EC_Summary_C
    {
        public string WH_ID { get; set; }
        public string WH_NAME { get; set; }
        public string CO_ID { get; set; }
        public string CO_NAME { get; set; }
        public decimal EXP_AMNT { get; set; }
        public decimal CON_AMNT { get; set; }
    }

    public class R_Unit_EC_D_Summary_C
    {
        public string ORDR_TEXT { get; set; }
        public DateTime ORDR_DATE { get; set; }
        public string WH_NAME { get; set; }
        public string CO_NAME { get; set; }
        public string ORTP_NAME { get; set; }
        public string MOLD_NAME { get; set; }
        public decimal EXP_AMNT { get; set; }
        public decimal CON_AMNT { get; set; }
    }
    public class R_Unit_EC_ORAM_ORMC_C
    {
        public string ORDR_TEXT { get; set; }
        public string MCLT_NAME { get; set; }
        public decimal ORMC_HOUR { get; set; }
        public decimal ORMC_COST { get; set; }
        public string ORMC_DESC { get; set; }
        public string ORDR_REFR { get; set; }
    }

    public class R_TimeConsumeC
    {
        public string ORDR_TEXT { get; set; }
        public string WH_NAME { get; set; }
        public string CO_NAME { get; set; }
        public string ORTP_NAME { get; set; }
        public string MOLD_NAME { get; set; }
        public DateTime CDT { get; set; }
        public Nullable<DateTime> ORDR_RDAT { get; set; }
        public Nullable<DateTime> ORDR_DDAT { get; set; }
        public string ORDR_STAS { get; set; }
    }
    public class R_TimeConsumeDC
    {
        public string ORDR_TEXT { get; set; }
        public string WH_NAME { get; set; }
        public string CO_NAME { get; set; }
        public string ORTP_NAME { get; set; }
        public string MOLD_NAME { get; set; }
        public DateTime CDT { get; set; }
        public Nullable<DateTime> ORDR_RDAT { get; set; }
        public string OR_C { get; set; }
        public Nullable<DateTime> ORDR_DDAT { get; set; }
        public string RD_C { get; set; }
        public string ORDR_STAS { get; set; }
    }
    public class R_ExpConsumeC
    {
        public string MCLT_NAME { get; set; }
        public string MCLT_DESC { get; set; }
        public decimal MCLT_HOUR { get; set; }
    }

    public class R_BILL_PENDING_C
    {
        public string ORDR_TEXT { get; set; }
        public string WH_NAME { get; set; }
        public string CO_NAME { get; set; }
        public string ORTP_NAME { get; set; }
        public string MOLD_NAME { get; set; }
        public string ORDR_RISR { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ORDR_DATE { get; set; }
        public Nullable<DateTime> ORDR_DDAT { get; set; }
        public string ORDR_PRLM { get; set; }
        public string ORDR_CMNT { get; set; }
    }


}