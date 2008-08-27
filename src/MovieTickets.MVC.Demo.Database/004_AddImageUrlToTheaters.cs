using MovieTickets.MVC.Demo.Database;
using RikMigrations;

[assembly: Migration(typeof(AddImageUrlToTheaters), 4)]
namespace MovieTickets.MVC.Demo.Database
{
    public class AddImageUrlToTheaters : IMigration
    {
        public void Up(DbProvider db)
        {
            var t = db.AlterTable("Theaters");
            t.AddColumn<string>("ImageUrl", 1024);
            t.Save();
        }

        public void Down(DbProvider db)
        {
            var t = db.AlterTable("Theaters");
            t.DropColumn("ImageUrl");
            t.Save();
        }
    }
}