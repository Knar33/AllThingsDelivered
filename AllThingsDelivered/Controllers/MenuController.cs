using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AllThingsDelivered.Models;

namespace AllThingsDelivered.Store.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index(string id)
        {
            return View();
        }
    }
}