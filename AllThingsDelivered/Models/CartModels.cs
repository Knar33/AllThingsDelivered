using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AllThingsDelivered.Models
{
    public class Cart
    {
        public IEnumerable<CartContent> cartContent { get; set; }
        public IEnumerable<CustomCartContent> customCartContent { get; set; }
    }

    public class CartIndex
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string type { get; set; }
    }

    public class CustomCartIndex
    {
        [Required]
        [MaxLength(1000)]
        public string itemLocation { get; set; }

        [Required]
        [MaxLength(1000)]
        public string content { get; set; }
    }
}