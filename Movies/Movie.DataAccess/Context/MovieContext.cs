using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Configuration;
using Movies.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.DataAccess.Context
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGender> MovieGenders { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            var added = ChangeTracker.Entries()
                            .Where(e => e.State == EntityState.Added)
                            .Select(e => e.Entity).ToArray();

            foreach (var e in added)
            {
                if (e is EntityBase)
                {
                    var entity = (EntityBase)e;
                    entity.Id = Guid.NewGuid();
                    entity.CreatedAt = DateTime.Now;
                    entity.ModifiedAt = DateTime.Now;
                }
            }

            // modificados
            var modified = ChangeTracker.Entries()
                .Where(m => m.State == EntityState.Modified)
                .Select(m => m.Entity).ToArray();

            foreach (var m in modified)
            {
                if (m is EntityBase)
                {
                    var entity = (EntityBase)m;
                    entity.ModifiedAt = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
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