using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllThingsDelivered.Models
{
    public class RegisterModel
    {
        public List<SelectListItem> stateList { get; set; }

        public List<SelectListItem> countryList { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string username { get; set; }

        [Required]
        [MinLength(4)]
        public string password1 { get; set; }

        [Required]
        [MinLength(4)]
        [System.ComponentModel.DataAnnotations.Compare("password1")]
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

        [Required]
        [MaxLength(100)]
        [Display(Name = "Address Line 1")]
        public string line1 { get; set; }
        
        [MaxLength(100)]
        [Display(Name = "Address Line 2")]
        public string line2 { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "State")]
        public string state { get; set; }

        [Required]
        [MaxLength(25)]
        [Display(Name = "Postal Code")]
        public string postalcode { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Country")]
        public string country { get; set; }

        [Required]
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }

        [Required]
        [Display(Name = "CVV")]
        public string CreditCardVerificationValue { get; set; }

        [Required]
        [Display(Name = "Expiration")]
        public int CreditCardExpirationMonth { get; set; }

        [Required]
        public int CreditCardExpirationYear { get; set; }

        [Required]
        public string CreditCardHolderName { get; set; }
    }

    public class ForgotPassword
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string email { get; set; }
    }

    public class SignIn
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string username { get; set; }

        [Required]
        [MinLength(4)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Remember Me")]
        public bool rememberMe { get; set; }

        public string returnUrl { get; set; }
    }

    public class ResetPassword
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string email { get; set; }
        public string token { get; set; }
        public string password { get; set; }
    }
}