using AllThingsDelivered.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllThingsDelivered.Controllers
{
    public class CustomItemController : Controller
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

        // GET: CustomItem
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

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(CustomCartIndex model)
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

                db.CustomCartContents.Add(new CustomCartContent { ItemLocation = model.itemLocation, Content = model.content, CustomerID = customerID });
                db.SaveChanges();

                ViewBag.message = "You have successfully added a custom item to your cart!";
            }
            return View();
        }
    }
}