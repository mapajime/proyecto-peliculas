﻿using System;
using System.Collections.Generic;

namespace Movies.Entities
{
    public class Movie : EntityBase
    {
        public string Name { get; set; }
        public virtual MovieGender Gender { get; set; }
        public TimeSpan Duration { get; set; }
        public virtual ICollection<Actor> Cast { get; set; }
        public string Argument { get; set; }
        public string Slogan { get; set; }
        public string Director { get; set; }
        public virtual Language Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual Country CountryOfOrigin { get; set; }
    }
}