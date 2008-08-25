using System.Web.Mvc;

namespace MovieTickets.MVC.Demo.Web.Helpers
{
    public static class AppHelper
    {
        public static SelectList SelectListFor(string[] array)
        {
            return new SelectList(array);
        }
    }
}