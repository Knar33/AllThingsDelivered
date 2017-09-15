using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllThingsDelivered.Models
{
    public class EditInfo
    {
        [Required]
        [MaxLength(100)]
        public string firstname { get; set; }

        [Required]
        [MaxLength(100)]
        public string lastname { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(15)]
        public string phone { get; set; }

        public Customer customer { get; set; }
    }

    public class DeleteAddress
    {
        public int AddressID { get; set; }
    }

    public class AddAddress
    {
        public Customer customer { get; set; }

        public List<SelectListItem> stateList { get; set; }

        public List<SelectListItem> countryList { get; set; }

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
    }
}