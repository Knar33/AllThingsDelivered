using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllThingsDelivered.Models
{
    public class Cart
    {
        public IEnumerable<CartContent> cartContent { get; set; }
        public IEnumerable<CustomCartContent> customCartContent { get; set; }
    }
}