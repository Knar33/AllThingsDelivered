using AllThingsDelivered.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllThingsDelivered.Controllers
{
    public class CheckoutController : Controller
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

        // GET: Checkout
        public ActionResult Index()
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
            Checkout model = new Checkout
            {
                customer = db.Customers.Single(x => x.CustomerID == customerID)
            };
            return View(model);
        }

        // GET: Checkout
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(Checkout model)
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
            Checkout outModel = new Checkout
            {
                orderprice = model.orderprice,
                customer = db.Customers.Single(x => x.CustomerID == customerID)
            };
            return View(outModel);
        }

        // POST: Checkout
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Finalize(Checkout model)
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

            DateTime timePlaced = DateTime.Now;

            db.Orders.Add(new Order
            {
                AddressID = Convert.ToInt32(model.address),
                CustomerID = customerID,
                TimePlaced = timePlaced,
                OrderMethod = "Internet",
                Active = true,
                Updating = false,
                Updated = false,
                OrderPrice = model.orderprice,
                Customize = model.customize
            });
            db.SaveChanges();
            //move everything from cart to order
            //delete cart items

            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}