using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllThingsDelivered.Models
{
    public class EditInfo
    {
        public Customer customer { get; set; }

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
    }

    public class DeleteAddress
    {
        public int AddressID { get; set; }
    }

    public class AddAddress
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
    }
}