using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class V_MENU
    {
        public string USER_TEXT { get; set; }
        public Nullable<int> ACT { get; set; }
        public string MNUP_NAME { get; set; }
        public string MNUP_ICON { get; set; }
        public int MNUC_TEXT { get; set; }
        public string MNUC_NAME { get; set; }
        public string MNUC_ICON { get; set; }
        public string MNUC_NOTE { get; set; }
    }
}