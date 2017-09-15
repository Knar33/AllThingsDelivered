﻿using AllThingsDelivered.Models;
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

        public ActionResult Addresses()
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

            List<SelectListItem> StateList = new List<SelectListItem>();
            StateList.Add(new SelectListItem { Text = "Select A State", Value = "" });
            foreach (State state in db.States)
            {
                StateList.Add(new SelectListItem { Text = state.StateName, Value = state.StateName });
            }

            List<SelectListItem> CountryList = new List<SelectListItem>();
            CountryList.Add(new SelectListItem { Text = "Select A Country", Value = "" });
            foreach (Country country in db.Countries)
            {
                CountryList.Add(new SelectListItem { Text = country.CountryName, Value = country.CountryName });
            }

            AddAddress model = new AddAddress
            {
                stateList = StateList,
                countryList = CountryList,
                customer = db.Customers.Single(x => x.CustomerID == customerID)
            };

            return View(model);
        }

        //user delete address
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAddress(int addressID, int customerID)
        {
            db.CustomerAddresses.Remove(db.CustomerAddresses.SingleOrDefault(x => (x.CustomerID == customerID && x.AddressID == addressID)));

            Address address = db.Addresses.Single(x => x.AddressID == addressID);
            address.Deleted = true;
            db.SaveChanges();

            return RedirectToAction("Addresses");
        }

        //user add address
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAddress(AddAddress model)
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
                Address address = new Address { Line1 = model.line1, Line2 = model.line2, City = model.city, State = model.state, ZipCode = model.postalcode, Country = model.country, Deleted = false, AddressType = "Customer" };
                db.Addresses.Add(address);

                CustomerAddress customerAddress = new CustomerAddress { DefaultAddr = false };
                customerAddress.CustomerID = customerID;
                customerAddress.AddressID = address.AddressID;
                db.CustomerAddresses.Add(customerAddress);
                db.SaveChanges();
            }
            return RedirectToAction("Addresses", model);
        }

        //user set address as default
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DefaultAddress(int addressID, int customerID)
        {
            foreach (CustomerAddress addr in db.CustomerAddresses.Where(x => x.CustomerID == customerID))
            {
                addr.DefaultAddr = false;
            }
            db.CustomerAddresses.SingleOrDefault(x => (x.CustomerID == customerID && x.AddressID == addressID)).DefaultAddr = true;
            db.SaveChanges();

            return RedirectToAction("Addresses");
        }
    }
}