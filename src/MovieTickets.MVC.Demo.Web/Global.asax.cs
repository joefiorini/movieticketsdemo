using System;
using System.Web;
using System.Web.Routing;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using IronRubyMvcLibrary.Routing;
using MovieTickets.MVC.Demo.Web.Models;

namespace MovieTickets.MVC.Demo.Web
{
    public class GlobalApplication : HttpApplication
    {
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            RouteTable.Routes.LoadFromRuby();
        }

        protected void Application_Start()
        {
            IConfigurationSource config = new XmlConfigurationSource(Server.MapPath("/bin/ARConfig.xml"));
            ActiveRecordStarter.Initialize(config, typeof(Movie));
        }
    }
}