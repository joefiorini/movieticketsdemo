using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ruby.Builtins;

namespace IronRubyMvcLibrary.Helpers {
    public class RubyUrlHelper : UrlHelper {
        public RubyUrlHelper(ViewContext viewContext) : base(viewContext) { 
        }

        public new string Action(string actionName) {
            return base.Action(actionName);
        }

        public new string Action(string actionName, Hash values) {
            return base.Action(actionName, values.ToRouteDictionary());
        }

        public new string Action(string actionName, string controllerName) {
            return base.Action(actionName, controllerName);
        }

        public string Action(string actionName, string controllerName, Hash values) {
            return base.Action(actionName, controllerName, values.ToRouteDictionary());
        }

        public string Action(Hash values) {
            return base.RouteUrl(values.ToRouteDictionary());
        }

        public string e(object s) {
            return base.Encode(s.ToString());
        }

        public string e(string s) {
            return base.Encode(s);
        }
    }
}
