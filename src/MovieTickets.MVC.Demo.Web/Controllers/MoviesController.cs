using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieTickets.MVC.Demo.Web.Models;

namespace MovieTickets.MVC.Demo.Web.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Index()
        {
            Movie[] movies = Movie.FindAll();

            var container = ModelContainer.Create(movies.ToList());

            return View(container);
        }

        public ActionResult Show(int id)
        {
            var movie = Movie.Find(id);

            var container = ModelContainer.Create(movie);

            return View(container);
        }
    }
}
