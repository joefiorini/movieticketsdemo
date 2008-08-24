using System;
using System.Web.Routing;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using Ruby.Builtins;

namespace IronRubyMvcLibrary
{
    public class DlrInteropRouteCollection
    {
        RouteCollection routes;

        public DlrInteropRouteCollection(RouteCollection routes)
        {
            this.routes = routes;
        }

        public void Add(Route route)
        {
            this.routes.Add(route);
        }

        public void MapRoute(string url)
        {
            MapRoute(url, new Hashtable(), new Hashtable());
        }

        public void MapRoute(string url, IDictionary defaults)
        {
            MapRoute(url, defaults, new Hashtable());
        }

        public void MapRoute(string url, IDictionary defaults, IDictionary constraints)
        {
            var route = new Route(url, new MvcRouteHandler());
            route.Defaults = defaults.ToRouteDictionary();
            route.Constraints = constraints.ToRouteDictionary();
            routes.Add(route);
        }

        public void MapRoute(string name, string url, IDictionary defaults)
        {
            var route = new Route(url, new MvcRouteHandler())
                            {
                                Defaults = defaults.ToRouteDictionary(),
                            };
            routes.Add(name, route);
            
        }

        public void MapRoute(string name, string url, IDictionary defaults, IDictionary constraints)
        {
            var route = new Route(url, new MvcRouteHandler())
                            {
                                Defaults = defaults.ToRouteDictionary(),
                                Constraints = constraints.ToRouteDictionary()
                            };
            routes.Add(name, route);
        }
    }

    public static class RouteValueDictionaryHelpers
    {
        public static RouteValueDictionary ToRouteDictionary(this IDictionary dictionary)
        {
            var rvd = new RouteValueDictionary();
            foreach (var key in dictionary.Keys) {
                rvd.Add(key.ToString(), (dictionary[key] ?? "").ToString());
            }
            return rvd;
        }
    }
}
