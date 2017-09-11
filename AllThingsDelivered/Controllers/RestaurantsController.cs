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
        Cities City = new Cities();

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
            return View(City);
        }

        public ActionResult List(string city)
        {
            var restaurants = db.Restaurants.Where(x => x.Address.City == city);
            return View(restaurants);
        }
    }
}