using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
            return View(GetMovie(id));
        }

        public ActionResult Edit(int id)
        {
            return View(GetMovie(id));
        }

        public ActionResult Update(int id)
        {
            var container = GetMovie(id);
            BindingHelperExtensions.UpdateFrom(container.Movie, Request.Form);
            container.Movie.Save();
            return new RedirectToRouteResult("movie", new RouteValueDictionary(new {Id = id}));
        }

        public ActionResult New()
        {
            var container = ModelContainer.Create();
            return View(container);
        }

        public ActionResult Create()
        {
            var container = ModelContainer.Create();
            BindingHelperExtensions.UpdateFrom(container.Movie, Request.Form);
            container.Movie.Save();

            return new RedirectToRouteResult("movie", new RouteValueDictionary(new {container.Movie.Id}));
        }

        public ActionResult Delete(int id)
        {
            var movie = Movie.Find(id);
            movie.Delete();
            return new RedirectToRouteResult("movies", null);
        }

        private ModelContainer GetMovie(int id)
        {
            var movie = Movie.Find(id);
            return ModelContainer.Create(movie);
        }
    }
}
