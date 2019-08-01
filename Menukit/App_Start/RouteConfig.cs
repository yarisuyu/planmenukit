using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Menukit
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Ingredients", action = "List", page = 1 }
            );

            routes.MapRoute(
                name: "",
                url: "Page{page}",
                defaults: new { controller = "Ingredients", action = "List", page = @"\d+" }
            );

            routes.MapRoute(
                name: "",
                url: "{category}",
                defaults: new { controller = "Ingredients", action = "List", page = @"\d+" }
            );

            routes.MapRoute(
                name: "",
                url: "{category}/Page{page}",
                defaults: new { controller = "Ingredients", action = "List" },
                    new { page = @"\d+" }
            );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
