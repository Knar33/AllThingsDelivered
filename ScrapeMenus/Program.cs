using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllThingsDelivered.Models;
using System.Web;

namespace ScrapeMenus
{
    class Program
    {
        AllThingsDeliveredDBEntities db = new AllThingsDeliveredDBEntities();

        //Type override and hit tab
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        static void Main(string[] args)
        {

        }
    }
}
