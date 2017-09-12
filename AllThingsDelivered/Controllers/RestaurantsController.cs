using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllThingsDelivered.Controllers
{
    public class RestaurantsController : Controller
    {
        Models.AllThingsDeliveredDBEntities db = new Models.AllThingsDeliveredDBEntities();

        //Type override and hit tab
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Restaurants
        public ActionResult Index()
        {
            var restaurants = db.Restaurants;
            return View(restaurants);
        }
    }
}