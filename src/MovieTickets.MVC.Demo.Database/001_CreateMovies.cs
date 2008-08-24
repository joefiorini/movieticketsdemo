using System;
using MovieTickets.MVC.Demo.Database;
using RikMigrations;

[assembly: Migration(typeof(MovieTickets.MVC.Demo.Database.CreateMovies), 1)]
namespace MovieTickets.MVC.Demo.Database
{
    public class CreateMovies : IMigration
    {
        public void Up(DbProvider db)
        {
            Table t = db.AddTable("Movies");
            t.AddColumn<int>("Id").PrimaryKey().AutoGenerate();
            t.AddColumn<string>("Name", 100);
            t.AddColumn<DateTime>("ReleaseDate");
            t.AddColumn<decimal>("Runtime");
            t.AddColumn<string>("Rating", 5);
            t.AddColumn<string>("RatingDescription", 250);
            t.AddColumn<string>("Description", 5000);
            t.Save();
        }

        public void Down(DbProvider db)
        {
            db.DropTable("Movies");
        }
    }
}