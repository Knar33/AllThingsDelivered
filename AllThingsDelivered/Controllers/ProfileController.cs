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
            EditInfo customerInfo = new EditInfo { customer = db.Customers.Single(x => x.CustomerID == customerID) };
            return View(customerInfo);
        }

        //user edit profile information
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EditInfo model)
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

            if (ModelState.IsValid)
            {
                db.Customers.Single(x => x.CustomerID == customerID).FirstName = model.firstname;
                db.Customers.Single(x => x.CustomerID == customerID).LastName = model.lastname;
                db.Customers.Single(x => x.CustomerID == customerID).PhoneNumber = model.phone;
                //make sure the email address isn't already taken
                if (User.Identity.Name != model.email)
                {
                    if ((db.AspNetUsers.SingleOrDefault(x => x.UserName == model.email) == null) && (db.AspNetUsers.SingleOrDefault(x => x.Email == model.email) == null))
                    {
                        db.AspNetUsers.Single(x => x.UserName == User.Identity.Name).Email = model.email;
                        db.AspNetUsers.Single(x => x.UserName == User.Identity.Name).UserName = model.email;
                        db.AspNetUsers.Single(x => x.UserName == User.Identity.Name).EmailConfirmed = false;
                    }
                    else
                    {
                        ViewBag.error = "That email address is already taken by another user.";
                        EditInfo failedInfo = new EditInfo { customer = db.Customers.Single(x => x.CustomerID == customerID) };
                        return View(failedInfo);
                    }
                }
                db.SaveChanges();
                ViewBag.Success = "You have successfully updated your profile inormation";
            }
            EditInfo customerInfo = new EditInfo { customer = db.Customers.Single(x => x.CustomerID == customerID) };
            return View(customerInfo);
        }

        //user delete address
        public ActionResult DeleteAddress(DeleteAddress model)
        {
            return View();
        }

        //user add address
        public ActionResult AddAddress(AddAddress model)
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