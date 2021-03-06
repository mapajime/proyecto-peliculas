using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Entities;

namespace Movies.DataAccess.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Argument).HasMaxLength(2048).IsRequired();
            builder.HasMany(p => p.Cast);
            builder.HasOne(p => p.CountryOfOrigin).WithMany().HasForeignKey(c => c.CountryId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(p => p.Director).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Duration).IsRequired();
            builder.HasOne(p => p.Gender).WithMany().HasForeignKey(g => g.MovieGenderId);
            builder.HasOne(p => p.Language).WithMany().HasForeignKey(l => l.LanguageId);
            builder.Property(p => p.ReleaseDate).IsRequired();
            builder.Property(p => p.Slogan).HasMaxLength(150);
            builder.ToTable("Movies", "movie");
        }
    }
}