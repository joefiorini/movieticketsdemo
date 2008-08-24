using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace IronRubyMvcLibrary {
    public class RubyControllerFactory : IControllerFactory {

        public IController CreateController(RequestContext context, string controllerName) {
            // for now limit controller name to alphanumeric characters
            if (!Regex.IsMatch(controllerName, @"^(\w)+$")) {
                return null;
            }
            return new RubyController { ControllerName = controllerName };
        }

        public void DisposeController(IController controller) {
            return;
        }

    }
}
