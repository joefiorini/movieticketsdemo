using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MovieTickets.MVC.Demo.Web.Models;

namespace MovieTickets.MVC.Demo.Web.Controllers
{
    public class TheatersController : Controller
    {
        public ActionResult Show(string zip)
        {
            List<Theater> theaters = Theater.FindByZipCode(zip).ToList();
            ViewData.Add("zip_code", zip);
            return View("ZipCode", ModelContainer.Create(theaters));
        }

        
    }
}


