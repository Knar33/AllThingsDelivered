using AllThingsDelivered.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllThingsDelivered.Controllers
{
    public class ProfileController : Controller
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

        // GET: Profile
        public ActionResult Index()
        {
            int customerID = 0;
            if (User.Identity.IsAuthenticated)
            {
                /*
                //claim based authentication
                var claimsIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
                claimsIdentity.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Role);
                */
                customerID = db.AspNetUsers.Single(x => x.UserName == User.Identity.Name).Customers.First().CustomerID;
            }
            else
            {
                TempData["SignIn"] = "You must be signed in to do that";
                return RedirectToAction("SignIn", "Account");
            }
            return View(db.Customers.Single(x => x.CustomerID == customerID));
        }

        //user edit profile information
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string firstname, string lastname, string phone, string email)
        {
            return View();
        }

        //user delete address
        public ActionResult DeleteAddress(int AddressID)
        {
            return View();
        }

        //user add address
        public ActionResult AddAddress(string line1, string line2, string city, string state, string zipcode, string country)
        {
            return View();
        }

        //user set address as default
        public ActionResult DefaultAddress(int AddressID)
        {
        return View();
        }
    }
}