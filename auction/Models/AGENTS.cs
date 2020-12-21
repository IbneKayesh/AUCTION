using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace auction.Models
{
    public class AGENTS:ExtColumns
    {
        public Nullable<Int64> AGENT_ID { get; set; }

        [DisplayName("Login Id")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 5)]
        public string AGENT_LOGIN_ID { get; set; }


        [DisplayName("Password")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 5)]
        public string AGENT_PASSWORD { get; set; }


        [DisplayName("Proprietor Name")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 0)]
        public string AGENT_PROPRIETOR_NAME { get; set; }


        [DisplayName("Proprietor Picture")]
        public string AGENT_PICTURES { get; set; }


        [DisplayName("Agent Name")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 5)]
        public string AGENT_NAME { get; set; }


        [DisplayName("Agent Contact")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 0)]
        public string AGENT_CONTACT { get; set; }


        [DisplayName("Website")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 0)]
        public string AGENT_WEBSITE { get; set; }


        [DisplayName("Registration number")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 0)]
        public string REGISTRATION_NUMBER { get; set; }


        [DisplayName("Member No")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 0)]
        public string AGENT_MEMBER { get; set; }


        [DisplayName("Agent Contact Person")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 3)]
        public string CONTACT_PERSON { get; set; }


        [DisplayName("Agent Contact Person Contact Number")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 3)]
        public string CONTACT_PERSON_CONTACT { get; set; }


        [DisplayName("Agent Email Address")]
        [DataType(DataType.EmailAddress)]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 0)]
        public string EMAIL_ADDRESS { get; set; }


        [DisplayName("Agent Address")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 200, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 3)]
        public string AGENT_ADDRESS { get; set; }


        [DisplayName("Capacity")]
        [Required(ErrorMessage = "Capacity is Required")]
        [Range(0.01, 9999999999999999.99, ErrorMessage = "Please enter a valid number")]
        public string AGENT_CAPACITY { get; set; }


        [DisplayName("Email Notifications")]
        [Required(ErrorMessage = "{0} is Required")]
        [Range(0, 1, ErrorMessage = "Please enter a valid number")]
        public int EMAIL_NOTIFICATIONS { get; set; }


        [DisplayName("Agent Status")]
        [Required(ErrorMessage = "{0} is Required")]
        [Range(0, 10, ErrorMessage = "Please enter a valid number")]
        public int AGENT_STATUS { get; set; }
    }

    public class AGENTS_NEW : ExtColumns
    {

        [DisplayName("Login Id")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 5)]
        public string AGENT_LOGIN_ID { get; set; }


        [DisplayName("Password")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 5)]
        public string AGENT_PASSWORD { get; set; }


        [DisplayName("Proprietor Name")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 0)]
        public string AGENT_PROPRIETOR_NAME { get; set; }


        [DisplayName("Proprietor Picture")]
        public string AGENT_PICTURES { get; set; }


        [DisplayName("Agent Name")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 5)]
        public string AGENT_NAME { get; set; }


        [DisplayName("Agent Contact")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 0)]
        public string AGENT_CONTACT { get; set; }


        [DisplayName("Website")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 0)]
        public string AGENT_WEBSITE { get; set; }


        [DisplayName("Registration number")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 0)]
        public string REGISTRATION_NUMBER { get; set; }


        [DisplayName("Member No")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 0)]
        public string AGENT_MEMBER { get; set; }


        [DisplayName("Agent Contact Person")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 3)]
        public string CONTACT_PERSON { get; set; }


        [DisplayName("Agent Contact Person Contact Number")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 3)]
        public string CONTACT_PERSON_CONTACT { get; set; }


        [DisplayName("Agent Email Address")]
        [DataType(DataType.EmailAddress)]
        [StringLength(maximumLength: 50, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 0)]
        public string EMAIL_ADDRESS { get; set; }


        [DisplayName("Agent Address")]
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(maximumLength: 200, ErrorMessage = "The {0} length must between {2} and {1} char", MinimumLength = 3)]
        public string AGENT_ADDRESS { get; set; }


        [DisplayName("Capacity")]
        [Required(ErrorMessage = "Capacity is Required")]
        [Range(0.01, 9999999999999999.99, ErrorMessage = "Please enter a valid number")]
        public decimal AGENT_CAPACITY { get; set; }


        [DisplayName("Email Notifications")]
        [Required(ErrorMessage = "{0} is Required")]
        [Range(0, 1, ErrorMessage = "Please enter a valid number")]
        public int EMAIL_NOTIFICATIONS { get; set; }


        [DisplayName("Agent Status")]
        [Required(ErrorMessage = "{0} is Required")]
        [Range(0, 10, ErrorMessage = "Please enter a valid number")]
        public int AGENT_STATUS { get; set; }

        public List<agent_company_t> AGENT_COMPANY_T { get; set; }
    }

    public class agent_company_t
    {
        [Required(ErrorMessage = "{0} is Required")]
        public string COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }
    }
}