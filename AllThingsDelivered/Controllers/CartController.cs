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
            Cart thisCart = new Cart
            {
                cartContent = db.CartContents.Where(x => x.CustomerID == customerID),
                customCartContent = db.CustomCartContents.Where(x => x.CustomerID == customerID)
            };
            return View(thisCart);
        }

        [HttpPost]
        public ActionResult Index(int id, string type)
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
            
            //remove item from cart
            if (type == "CartContent")
            {
                CartContent cartContent = new CartContent { CartContentID = id };
                db.CartContents.Attach(cartContent);
                db.CartContents.Remove(cartContent);
                db.SaveChanges();
            }
            else if (type == "CustomCartContent")
            {
                CustomCartContent customCartContent = new CustomCartContent { CustomCartContentsID = id };
                db.CustomCartContents.Attach(customCartContent);
                db.CustomCartContents.Remove(customCartContent);
                db.SaveChanges();
            }
            
            Cart thisCart = new Cart
            {
                cartContent = db.CartContents.Where(x => x.CustomerID == customerID),
                customCartContent = db.CustomCartContents.Where(x => x.CustomerID == customerID)
            };
            return View(thisCart);
        }
    }
}