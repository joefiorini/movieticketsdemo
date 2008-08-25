using System;
using System.IO;
using System.Web;
using Castle.ActiveRecord;

namespace MovieTickets.MVC.Demo.Web.Models
{
    [ActiveRecord("Movies")]
    public class Movie : ActiveRecordBase<Movie>
    {
        private string _thumbnailUrl = null;

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
        public string PosterThumbnailUrl
        {
            get
            {
                if(!File.Exists(HttpContext.Current.Server.MapPath(_thumbnailUrl))) _thumbnailUrl = "/images/default_poster.png";
                return _thumbnailUrl;
            } 
            set{ _thumbnailUrl = value;}
        }

        public static string[] Ratings
        {
            get
            {
                return new[]{"G", "PG", "PG13", "R", "NC17"};
            }
        }
    }
}