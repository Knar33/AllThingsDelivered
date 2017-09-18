using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllThingsDelivered.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.BGUrl = "'/Content/Images/bg.jpg'";
            ViewBag.BGRepeat = "no-repeat";
            ViewBag.BGSize = "cover";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.BGUrl = "'/Content/Images/bg2.jpg'";
            ViewBag.BGRepeat = "no-repeat";
            ViewBag.BGSize = "cover";
            return View();
        }
    }
}