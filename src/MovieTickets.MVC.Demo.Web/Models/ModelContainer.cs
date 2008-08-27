using System.Collections.Generic;

namespace MovieTickets.MVC.Demo.Web.Models
{
    public class ModelContainer
    {
        public Movie Movie { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Theater> Theaters { get; set; }
        public string[] MovieRatings { get { return Movie.Ratings; } }

        public static ModelContainer Create(List<Movie> movies)
        {
            return new ModelContainer {Movies = movies};
        }

        public static ModelContainer Create(List<Theater> theaters)
        {
            return new ModelContainer {Theaters = theaters};
        }

        public static ModelContainer Create(Movie movie)
        {
            return new ModelContainer {Movie = movie};
        }

        public static ModelContainer Create()
        {
            return new ModelContainer
                       {
                           Movies = new List<Movie>(),
                           Movie = new Movie()
                       };
        }
    }
}