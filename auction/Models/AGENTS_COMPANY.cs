using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class AGENTS_COMPANY:ExtColumns
    {
        [DisplayName("Agent Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 AGENT_ID { get; set; }


        [DisplayName("Company Id")]
        [Required(ErrorMessage = "{0} is Required")]
        public Int64 COMPANY_ID { get; set; }
    }
}