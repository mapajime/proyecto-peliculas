using System;

namespace Movies.Entities
{
    public class Actor : EntityBase
    {
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual Country Nacionality { get; set; }
        public float Weight { get; set; }
        public int Height { get; set; }
        public float NetWorth { get; set; }
        public virtual Gender Gender { get; set; }
        public int NumberOfChildren { get; set; }
    }
}