using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ETicaret.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Home",
                "",
                new { controller = "Home", action = "Index" },
                new [] { "ETicaret.Web.Contollers" });

            routes.MapRoute(
                "Blog",
                "blog",
                new { controller = "Blog", action = "Index" },
                new [] { "ETicaret.Web.Contollers" });

            routes.MapRoute(
                "Cart",
                "cart",
                new { controller = "ShoppingCart", action = "Index" },
                new [] { "ETicaret.Web.Contollers" });

            routes.MapRoute(
                "Checkout",
                "checkout",
                new { controller = "Checkout", action = "BillingPage" },
                new [] { "ETicaret.Web.Contollers" });

            routes.MapRoute(
                "GenericUrl",
                "{id}",
                new { controller = "Home", action = "CheckRoute", id = "" },
                new [] { "ETicaret.Web.Contollers" });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new [] { "ETicaret.Web.Contollers" }
            );
        }
    }
}
