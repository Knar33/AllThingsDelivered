using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllThingsDelivered.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string username { get; set; }

        [Required]
        [MinLength(4)]
        public string password1 { get; set; }

        [Required]
        [MinLength(4)]
        [Compare("password1")]
        public string password2 { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "First Name")]
        public string firstname { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Last Name")]
        public string lastname { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(15)]
        [Display(Name = "Phone Number")]
        public string phone { get; set; }
    }

    public class forgotpassword
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string email { get; set; }
    }

    public class signin
    {

    }

    public class resetpassword
    {

    }
}