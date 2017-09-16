using AllThingsDelivered.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Braintree;
using System.Configuration;

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
            return RedirectToAction("Index", "Cart");
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
            
            string merchantId = ConfigurationManager.AppSettings["Braintree.MerchantID"];
            var environment = Braintree.Environment.SANDBOX;
            string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
            string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
            BraintreeGateway braintreeGateway = new BraintreeGateway(environment, merchantId, publicKey, privateKey);

            var clientToken = braintreeGateway.ClientToken.generate();
            ViewBag.ClientToken = clientToken;

            Braintree.Customer customer = braintreeGateway.Customer.Find(db.AspNetUsers.Single(x => x.UserName == User.Identity.Name).Customers.First().BrainTreeID);
            PaymentMethods methods = new PaymentMethods
            {
                primaryMethod = customer.DefaultPaymentMethod,
                methods = customer.PaymentMethods
            };

            Checkout outModel = new Checkout
            {
                payment = methods,
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

            Order newOrder = new Order
            {
                AddressID = Convert.ToInt32(model.address),
                CustomerID = customerID,
                TimePlaced = timePlaced,
                Active = true,
                OrderPrice = model.orderprice,
                Customize = model.customize,
                PaymentToken = model.paymentMethod
            };
            db.SaveChanges();

            Models.Customer customer = db.Customers.Single(x => x.CustomerID == customerID);

            foreach (CartContent content in customer.CartContents)
            {
                newOrder.OrderContents.Add(new OrderContent
                {
                    OrderID = newOrder.OrderID,
                    ItemName = content.ItemName,
                    ItemDescription = content.ItemDescription,
                    Quantity = content.Quantity,
                    Price = content.Price,
                    Customize = content.Customize,
                    RestaurantName = content.RestaurantName,
                    Phone = content.Phone,
                    Line1 = content.Line1,
                    Line2 = content.Line2,
                    City = content.City,
                    State = content.State,
                    ZipCode = content.ZipCode,
                    Country = content.Country
                });
            }

            foreach (CustomCartContent customContent in customer.CustomCartContents)
            {
                newOrder.CustomOrderContents.Add(new CustomOrderContent
                {
                    OrderID = newOrder.OrderID,
                    ItemLocation = customContent.ItemLocation,
                    Content = customContent.Content
                });
            }
            db.Orders.Add(newOrder);

            db.CustomCartContents.RemoveRange(customer.CustomCartContents.Where(x => x.CustomerID == customerID));
            db.CartContents.RemoveRange(customer.CartContents.Where(x => x.CustomerID == customerID));
            db.SaveChanges();

            //move everything from cart to order
            //delete cart items
            TempData["ID"] = newOrder.CustomerID;

            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}