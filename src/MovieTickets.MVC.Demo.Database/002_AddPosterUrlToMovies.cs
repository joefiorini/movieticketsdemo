using System;
using RikMigrations;

[assembly: Migration(typeof(MovieTickets.MVC.Demo.Database.AddPosterUrlToMovies), 2)]
namespace MovieTickets.MVC.Demo.Database
{
    public class AddPosterUrlToMovies : IMigration
    {
        public void Up(DbProvider db)
        {
            Table t = db.AlterTable("Movies");
            t.AddColumn<string>("PosterThumbnailUrl", 1024);
            t.Save();
        }

        public void Down(DbProvider db)
        {
            Table t = db.AlterTable("Movies");
            t.DropColumn("PosterThumbnailUrl");
            t.Save();
        }
    }
}