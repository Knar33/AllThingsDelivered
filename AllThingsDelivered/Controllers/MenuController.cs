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
        public ActionResult Index(AddToCart model)
        {
            if (ModelState.IsValid)
            {
                int customerID;
                if (User.Identity.IsAuthenticated)
                {
                    customerID = db.AspNetUsers.Single(x => x.UserName == User.Identity.Name).Customers.First().CustomerID;
                }
                else
                {
                    TempData["SignIn"] = "You must be signed in to do that";
                    return RedirectToAction("SignIn", "Account");
                }

                db.CartContents.Add(new CartContent { CustomerID = customerID, RestaurantID = model.RestaurantID, ItemName = model.ItemName, ItemDescription = model.ItemDescription, Quantity = model.Quantity, Customize = model.Customize, Price = model.Price });
                db.SaveChanges();
            }
            ViewBag.message = "You have successfully added a custom item to your cart!";

            return View(db.Restaurants.Find(model.RestaurantID));
        }
    }
}