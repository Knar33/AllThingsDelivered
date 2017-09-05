using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AllThingsDelivered
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "towns",
                url: "Restaurants",
                defaults: new { controller = "Restaurants", action = "Index" }
            );

            routes.MapRoute(
                name: "restaurants",
                url: "Restaurants/{town}",
                defaults: new { controller = "Restaurants", action = "List", town = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "menus",
                url: "Menu/{id}",
                defaults: new { controller = "Menu", action = "Index", id = UrlParameter.Optional }
            );

            //will it only stop at this route if a suitable action is found?
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
