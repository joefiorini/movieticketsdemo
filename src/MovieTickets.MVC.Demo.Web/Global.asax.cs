using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MovieTickets.MVC.Demo.Web
{
    public class GlobalApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Note: Change the URL to "{controller}.mvc/{action}/{id}" to enable
            //       automatic support on IIS6 and IIS7 classic mode

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }, // Parameter defaults
                new { controller = @"[^\.]*" }                          // Parameter constraints
            );

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}