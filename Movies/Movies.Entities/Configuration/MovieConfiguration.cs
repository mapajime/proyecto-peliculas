using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movies.Entities.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Argument).HasMaxLength(2048).IsRequired();
            builder.HasMany(p => p.Cast);
            builder.HasOne(p => p.CountryOfOrigin);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.Director).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Duration);
            builder.HasOne(p => p.Gender);
            builder.HasOne(p => p.Language);
            builder.Property(p => p.ModifiedAt).IsRequired();
            builder.Property(p => p.ReleaseDate).IsRequired();
            builder.Property(p => p.Slogan).HasMaxLength(150);
            builder.HasKey(p => p.Id);
            builder.ToTable("Movie", "movie");
        }
    }
}