using AllThingsDelivered.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
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
            EditInfo editedInfo = new EditInfo { customer = db.Customers.Single(x => x.CustomerID == customerID) };
            return View(editedInfo);
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
                var customer = db.Customers.Single(x => x.CustomerID == customerID);
                customer.FirstName = model.firstname;
                customer.LastName = model.lastname;
                customer.PhoneNumber = model.phone;
                db.SaveChanges();
                ViewBag.Success = "You have successfully updated your profile inormation";
            }
            EditInfo editedInfo = new EditInfo { customer = db.Customers.Single(x => x.CustomerID == customerID) };
            return View(editedInfo);
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