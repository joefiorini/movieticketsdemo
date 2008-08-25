using System.Web;
using System.Web.Routing;

namespace MovieTickets.MVC.Demo.Web.RouteConstraints
{
    public class IntegerRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value))
            {
                int result;
                return int.TryParse(value.ToString(), out result);
            }

            return true;
        }
    }
}