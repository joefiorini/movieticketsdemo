using System;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;
using Microsoft.Scripting.Hosting;
using Ruby.Runtime;
using Ruby;
using System.IO;
using Microsoft.Scripting;
using System.Web.Hosting;

namespace IronRubyMvcLibrary.Routing
{
    public static class RubyRouteLoader
    {
        public static void LoadFromRuby(this RouteCollection routes)
        {
            LoadFromRuby(routes, HostingEnvironment.VirtualPathProvider);
        }

        public static void LoadFromRuby(this RouteCollection routes, VirtualPathProvider vpp)
        {
            LoadFromRuby(routes, vpp, "~/routes.rb");
        }

        public static void LoadFromRuby(this RouteCollection routes, VirtualPathProvider vpp, string virtualPath)
        {
            if (LoadFromCache(routes))
            {
                routes.Clear();
                VirtualFile file = vpp.GetFile(virtualPath);
                using (var reader = new StreamReader(file.Open()))
                {
                    LoadFromRuby(routes, reader);
                    MarkRoutesCollectionLoaded(routes);
                }
            }
        }

        public static bool LoadFromCache(this RouteCollection routes)
        {
            var loaded = HttpContext.Current.Cache["routes_loaded"];
            return loaded == null;
        }

        private static void MarkRoutesCollectionLoaded(RouteCollection routes)
        {
            HttpContext.Current.Cache.Insert("routes_loaded", true, new CacheDependency(HttpContext.Current.Server.MapPath("~/routes.rb")));
        }

        public static void LoadFromRuby(this RouteCollection routes, TextReader reader)
        {
            var routeCollection = new DlrInteropRouteCollection(routes);

            ScriptRuntime runtime = IronRuby.CreateRuntime();
            ScriptEngine rubyengine = IronRuby.GetEngine(runtime);
            RubyExecutionContext ctx = IronRuby.GetExecutionContext(runtime);

            ctx.DefineReadOnlyGlobalVariable("routes", routeCollection);

            string header = @"require 'System.Web.Abstractions, Version=0.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
require 'System.Web.Routing, Version=0.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
require 'System.Web.Mvc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
require 'MovieTickets.MVC.Demo.Web, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
";
            ScriptSource headerSource = rubyengine.CreateScriptSourceFromString(header);
            headerSource.Execute();

            string routesScript = reader.ReadToEnd();
            ScriptSource source = rubyengine.CreateScriptSourceFromString(routesScript);
            source.Execute();
        }
    }
}
