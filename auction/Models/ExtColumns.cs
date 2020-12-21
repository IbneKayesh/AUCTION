using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class ExtColumns
    {
        
        [DisplayName("Device Name")]
        [StringLength(50, ErrorMessage = "{0} length between {2} and {1} char", MinimumLength = 0)]
        public string PC_NAME { get; set; } = Environment.MachineName;


        [DisplayName("Create by")]
        [StringLength(50, ErrorMessage = "{0} length between {2} and {1} char", MinimumLength = 0)]
        public string CDU { get; set; } //= System.Web.HttpContext.Current.Session["UserId"].ToString();


        [DisplayName("Update by")]
        [StringLength(50, ErrorMessage = "{0} length between {2} and {1} char", MinimumLength = 0)]
        public string UDU { get; set; } //= System.Web.HttpContext.Current.Session["UserId"].ToString();


        [DisplayName("Create date")]
        public DateTime CDT { get; set; } = DateTime.Now;


        [DisplayName("Update date")]
        public Nullable<DateTime> UDT { get; set; } = DateTime.Now;

        [DisplayName("IsActive")]
        [Range(minimum: 0, maximum: 1, ErrorMessage = "{0} length between {2} and {1}")]
        public int ACT { get; set; } = 1;

        [DisplayName("Data Version")]
        [Range(minimum: 1, maximum: 9999999999, ErrorMessage = "{0} length between {2} and {1}")]
        public int VAR { get; set; } = 1;
    }
}