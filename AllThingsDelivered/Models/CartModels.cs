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
        [Display(Name = "Where can the item be found (If you're not sure, you can leave this blank)")]
        public string itemLocation { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "Item")]
        public string content { get; set; }
    }
}