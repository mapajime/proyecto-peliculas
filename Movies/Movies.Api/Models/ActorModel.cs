using System;

namespace Movies.Api.Models
{
    public class ActorModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Guid NacionalityId { get; set; }
        public float? Weight { get; set; }
        public int Height { get; set; }
        public float? NetWorth { get; set; }
        public Guid GenderId { get; set; }
        public int? NumberOfChildren { get; set; }
    }
}