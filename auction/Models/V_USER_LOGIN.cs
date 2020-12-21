using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class V_USER_LOGIN
    {
        [Required]
        public string USER_TEXT { get; set; }
        [Required]
        public string USER_PASS { get; set; }
    }
    public class V_USER_PASS_CHANGE
    {
        public string USER_TEXT { get; set; }
        [Required(ErrorMessage ="Old password is required")]
        public string USER_PASS { get; set; }
        [Required(ErrorMessage = "New password is required")]
        public string NEW_PASS { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        public string CONF_PASS { get; set; }
    }
}