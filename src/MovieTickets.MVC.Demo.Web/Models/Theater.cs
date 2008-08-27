using Castle.ActiveRecord;

namespace MovieTickets.MVC.Demo.Web.Models
{
    [ActiveRecord("Theaters")]
    public class Theater : ActiveRecordBase<Theater>
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Property]
        public string Name { get; set; }

        [Property]
        public string Address { get; set; }

        [Property]
        public string City { get; set; }

        [Property]
        public string State { get; set; }

        [Property]
        public string Zip { get; set; }

        [Property]
        public string ImageUrl { get; set; }

        public static Theater[] FindByZipCode(string zip)
        {
            return FindAllByProperty("Zip", zip);
        }
    }
}