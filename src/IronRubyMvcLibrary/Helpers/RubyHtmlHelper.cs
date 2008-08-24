namespace IronRubyMvcLibrary.Helpers {
    using System.Web.Mvc;
    using System.Web.Routing;
    using Ruby.Builtins;
    using System.Linq;
    using Microsoft.Scripting.Utils;

    /// <summary>
    /// Note, it looks like the interop is not calling base methods.
    /// </summary>
    public class RubyHtmlHelper : HtmlHelper {
        public RubyHtmlHelper(ViewContext context, IViewDataContainer viewDataContainer) : base(context, viewDataContainer) { 
        }

        public string ActionLink(string linkText, string actionName, Hash values) {
            return base.ActionLink(linkText, actionName, values.ToRouteDictionary());
        }

        public new string ActionLink(string linkText, string actionName, string controllerName) {
            return base.ActionLink(linkText, actionName, controllerName);
        }

        public new string ActionLink(string linkText, string actionName) {
            return base.ActionLink(linkText, actionName);
        }

        public string ActionLink(string linkText, Hash values) {
            return base.RouteLink(linkText, values.ToRouteDictionary());
        }

        public new string TextBox(string name) {
            //Yeah, I know this is sooo wrong, but still.
            name = name.Replace("_", "");
            return base.TextBox(name);
        }

        public new string TextBox(string name, object value) {
            //Yeah, I know this is sooo wrong, but still.
            name = name.Replace("_", "");
            return base.TextBox(name, value.ToString());
        }

        public new string Hidden(string name, object value) {
            return base.Hidden(name, value.ToString());
        }
    }
}
