using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieTickets.MVC.Demo.Database;
using RikMigrations;
using RikMigrations.Providers;

namespace MovieTickets.MVC.Demo.Web.Controllers
{
    public class MigrationsController : Controller
    {
        public ActionResult Index()
        {
            // Add action logic here
            try
            {
                DbProvider provider = new MssqlProvider
                                          {
                                              ConnectionString =
                                                  ConfigurationManager.ConnectionStrings["DefaultConnectionString"].
                                                  ConnectionString
                                          };
                MigrationManager.UpgradeMax(typeof(CreateMovies).Assembly, provider);
            }
            catch (Exception)
            {
                ControllerContext.HttpContext.Response.StatusCode = 404;
                throw;
            }

            return new EmptyResult();

        }
    }
}
