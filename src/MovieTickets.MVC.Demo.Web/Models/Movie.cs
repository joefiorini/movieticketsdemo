using System;
using Castle.ActiveRecord;

namespace MovieTickets.MVC.Demo.Web.Models
{
    [ActiveRecord("Movies")]
    public class Movie : ActiveRecordBase<Movie>
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Property]
        public string Name { get; set; }

        [Property]
        public DateTime ReleaseDate { get; set; }

        [Property]
        public Decimal Runtime { get; set; }

        [Property]
        public string Rating { get; set; }

        [Property]
        public string RatingDescription { get; set; }

        [Property]
        public string Description { get; set; }

        [Property]
        public string PosterThumbnailUrl { get; set; }

    }
}