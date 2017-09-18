﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AllThingsDelivered.Models;

namespace AllThingsDelivered.Controllers
{
    public class CartController : Controller
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
            Cart thisCart = new Cart
            {
                cartContent = db.CartContents.Where(x => x.CustomerID == customerID),
                customCartContent = db.CustomCartContents.Where(x => x.CustomerID == customerID)
            };
            return View(thisCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CartIndex model)
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

                //remove item from cart
                if (model.type == "CartContent")
                {
                    if (db.CartContents.SingleOrDefault(x => x.CartContentID == model.id) != null)
                    {
                        db.CartContents.Remove(db.CartContents.SingleOrDefault(x => x.CartContentID == model.id));
                        db.SaveChanges();
                    }
                }
                else if (model.type == "CustomCartContent")
                {
                    db.CustomCartContents.Remove(db.CustomCartContents.SingleOrDefault(x => x.CustomCartContentsID == model.id));
                    db.SaveChanges();
                }

                Cart thisCart = new Cart
                {
                    cartContent = db.CartContents.Where(x => x.CustomerID == customerID),
                    customCartContent = db.CustomCartContents.Where(x => x.CustomerID == customerID)
                };
                return View(thisCart);
            }
            return View();
        }
    }
}