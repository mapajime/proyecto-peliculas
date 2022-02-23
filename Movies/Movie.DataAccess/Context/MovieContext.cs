using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Configuration;
using Movies.Entities;

namespace Movies.DataAccess.Context
{
    public class MovieContext : DbContext
    {
        private readonly string _connectionString;

        public MovieContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGender> MovieGenders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  @"Server=.;Database=Movie;User Id=sa;Password=Yupi2021*;"
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActorConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            modelBuilder.ApplyConfiguration(new MovieGenderConfiguration());
        }
    }
}