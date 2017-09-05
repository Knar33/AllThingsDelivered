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
        public async Task<ActionResult> Index(string id)
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

            var menuUrl = string.Format("https://api.foursquare.com/v2/venues/{0}/menu?client_id={1}&client_secret={2}&v=20170825",
                id,
                ConfigurationManager.AppSettings["FourSquare.ClientId"],
                ConfigurationManager.AppSettings["FourSquare.ClientSecret"]);
            var menuResponse = await client.GetAsync(menuUrl);
            var typedResponse = JsonConvert.DeserializeObject<Menu>(await menuResponse.Content.ReadAsStringAsync());

            return View(typedResponse);
        }
    }
}