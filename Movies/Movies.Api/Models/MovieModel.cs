using System;

namespace Movies.Api.Models
{
    public class MovieModel : ModelBase
    {
        public string Name { get; set; }
        public Guid GenderId { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid CastId { get; set; }
        public string Argument { get; set; }
        public string Slogan { get; set; }
        public string Director { get; set; }
        public Guid LanguageId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid CountryOfOriginId { get; set; }
    }
}