using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using IronRubyMvcLibrary.Routing;
using MovieTickets.MVC.Demo.Database;
using MovieTickets.MVC.Demo.Web.Models;
using RikMigrations;
using RikMigrations.Providers;

namespace MovieTickets.MVC.Demo.Web
{
    public class GlobalApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteTable.Routes.LoadFromRuby();

            IConfigurationSource config = new XmlConfigurationSource(Server.MapPath("/bin/ARConfig.xml"));
            ActiveRecordStarter.Initialize(config, typeof(Movie));
        }
    }
}