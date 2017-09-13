using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllThingsDelivered.Models
{
    public class AddToCart
    {
        public int id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ItemName { get; set; }
        
        [MaxLength(2000)]
        public string ItemDescription { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal Price { get; set; }

        [Required]
        public int RestaurantID { get; set; }
        
        [MaxLength(1000)]
        public string Customize { get; set; }
    }
}