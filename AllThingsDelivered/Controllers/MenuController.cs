using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AllThingsDelivered.Models;

namespace AllThingsDelivered.Store.Controllers
{
    public class MenuController : Controller
    {
        AllThingsDeliveredDBEntities db = new AllThingsDeliveredDBEntities();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Menu
        public ActionResult Index(int id)
        {
            return View(db.Restaurants.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int id, string ItemName, string ItemDescription, int Quantity, decimal Price, int RestaurantID, string Customize)
        {
            int customerID = 0;
            if (User.Identity.IsAuthenticated)
            {
                customerID = db.AspNetUsers.Single(x => x.UserName == User.Identity.Name).Customers.First().CustomerID;
            }
            else
            {
                TempData["SignIn"] = "You must be signed in to do that";
                return RedirectToAction("SignIn", "Account");
            }

            db.CartContents.Add(new CartContent { CustomerID = customerID, RestaurantID = RestaurantID, ItemName = ItemName, ItemDescription = ItemDescription, Quantity = Quantity, Customize = Customize, Price = Price });
            db.SaveChanges();
            return View(db.Restaurants.Find(RestaurantID));
        }
    }
}