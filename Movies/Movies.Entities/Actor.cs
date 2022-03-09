using System;
using System.Collections.Generic;

namespace Movies.Entities
{
    public class Actor : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public float? Weight { get; set; }
        public int Height { get; set; }
        public float? NetWorth { get; set; }
        public int? NumberOfChildren { get; set; }
        public Guid NationalityId { get; set; }
        public virtual Country Nacionality { get; set; }
        public Guid GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}