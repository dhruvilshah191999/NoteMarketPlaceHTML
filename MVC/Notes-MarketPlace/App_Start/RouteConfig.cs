﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Notes_MarketPlace
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Auth",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Auth", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Profile",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Profile", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Notes",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Notes", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
