using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AllThingsDelivered.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AllThingsDelivered.Controllers
{
    public class ScrapeController : Controller
    {
        Models.AllThingsDeliveredDBEntities db = new Models.AllThingsDeliveredDBEntities();

        //Type override and hit tab
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Scrape
        public async Task<ActionResult> Index()
        {
            //delete everything from restaurants tables
            db.Database.ExecuteSqlCommand("DELETE FROM RestaurantItems");
            db.Database.ExecuteSqlCommand("DELETE FROM RestaurantCategories");
            db.Database.ExecuteSqlCommand("DELETE FROM Restaurants");
            db.Database.ExecuteSqlCommand("DELETE FROM Addresses WHERE AddressType='Restaurant'");

            string[] cities = new string[4] { "Chicago", "New York", "Houston", "Boston" };

            foreach (string city in cities)
            {
                //API call to Foursquare
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

                for (int i = 0; i < 10; i++)
                {
                    var cityString = string.Format("https://api.foursquare.com/v2/venues/search?client_id={0}&client_secret={1}&v=20170825&near={2}&limit=500&categoryId=4d4b7105d754a06374d81259&intent=browse&offset={3}",
                        ConfigurationManager.AppSettings["FourSquare.ClientId"],
                        ConfigurationManager.AppSettings["FourSquare.ClientSecret"],
                        city,
                        (i*50));
                    var cityUrl = await client.GetAsync(cityString);
                    var cityResponse = JsonConvert.DeserializeObject<Venue>(await cityUrl.Content.ReadAsStringAsync());

                    if (cityResponse.response.venues.Count() > 0)
                    {
                        foreach (Venues venue in cityResponse.response.venues)
                        {
                            if (venue.hasMenu)
                            {
                                //add this venues info to restaurants table.
                                Restaurant thisRestaurant = new Restaurant();
                                thisRestaurant.RestaurantName = venue.name;
                                thisRestaurant.Phone = venue.contact.phone;
                                thisRestaurant.FSID = venue.id;
                                thisRestaurant.Address = new Address { Deleted = false, AddressType = "Restaurant", Line1 = venue.location.address, City = venue.location.city, Country = venue.location.Country, State = venue.location.state, ZipCode = venue.location.postalCode };

                                var menuString = string.Format("https://api.foursquare.com/v2/venues/{0}/menu?client_id={1}&client_secret={2}&v=20170825",
                                    venue.id,
                                    ConfigurationManager.AppSettings["FourSquare.ClientId"],
                                    ConfigurationManager.AppSettings["FourSquare.ClientSecret"]);
                                var menuUrl = await client.GetAsync(menuString);
                                var menuResponse = JsonConvert.DeserializeObject<Menu>(await menuUrl.Content.ReadAsStringAsync());

                                foreach (SectionItems Section in menuResponse.response.menu.menus.items[0].entries.items)
                                {
                                    RestaurantCategory thisRestaurantCategory = new RestaurantCategory { CategoryName = Section.name, CategoryDescription = " " };

                                    foreach (ItemItems Item in Section.entries.items)
                                    {

                                        thisRestaurantCategory.RestaurantItems.Add(new RestaurantItem { ItemName = Item.name, ItemDescription = (Item.description == null ? "" : Item.description) });
                                    }
                                    thisRestaurant.RestaurantCategories.Add(thisRestaurantCategory);
                                }

                                db.Restaurants.Add(thisRestaurant);
                                try
                                {
                                    db.SaveChanges();
                                }
                                catch (Exception exception)
                                {
                                    Console.WriteLine(exception);
                                }

                                //second API call to get this restaurants menu bound to menu object
                                //add each category to RestaurantCategories table
                                //add each item to RestaurantItems table
                            }
                        }
                    }
                }
            }
            return View();
        }
    }
}