using MovieTickets.MVC.Demo.Database;
using RikMigrations;

[assembly: Migration(typeof(CreateTheaters), 3)]
namespace MovieTickets.MVC.Demo.Database
{
    public class CreateTheaters : IMigration
    {
        public void Up(DbProvider db)
        {
            var t = db.AddTable("Theaters");
            t.AddColumn<int>("Id").PrimaryKey().AutoGenerate();
            t.AddColumn<string>("Name", 100);
            t.AddColumn<string>("Address", 200);
            t.AddColumn<string>("City", 100);
            t.AddColumn<string>("State", 20);
            t.AddColumn<string>("Zip", 9);
            t.Save();
        }

        public void Down(DbProvider db)
        {
            db.DropTable("Theaters");
        }
    }
}