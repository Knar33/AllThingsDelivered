using System;
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
            int customerID = 1;
            return View(db.CartContents.Where(x => x.CustomerID == customerID));
        }

        [HttpPost]
        public ActionResult Index(int id)
        {
            int customerID = 1;
            CartContent cartContent = new CartContent { CartContentID = id };
            db.CartContents.Attach(cartContent);
            db.CartContents.Remove(cartContent);
            db.SaveChanges();
            return View(db.CartContents.Where(x => x.CustomerID == customerID));
        }
    }
}