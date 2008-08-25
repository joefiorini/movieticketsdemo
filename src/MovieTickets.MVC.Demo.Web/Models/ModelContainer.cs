using System.Collections.Generic;

namespace MovieTickets.MVC.Demo.Web.Models
{
    public class ModelContainer
    {
        public Movie Movie { get; set; }
        public List<Movie> Movies { get; set; }

        public static ModelContainer Create(List<Movie> movies)
        {
            return new ModelContainer {Movies = movies};
        }

        public static ModelContainer Create(Movie movie)
        {
            return new ModelContainer {Movie = movie};
        }
    }
}