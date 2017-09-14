using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllThingsDelivered.Models
{
    public class Checkout
    {
        public Customer customer { get; set; }

        public decimal orderprice { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }
        
        [MaxLength(1000)]
        [Display(Name = "Special Order Instructions")]
        public string customize { get; set; }
    }
}