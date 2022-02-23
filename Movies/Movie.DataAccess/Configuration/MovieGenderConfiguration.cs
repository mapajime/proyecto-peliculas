using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Entities;

namespace Movies.DataAccess.Configuration
{
    public class MovieGenderConfiguration : IEntityTypeConfiguration<MovieGender>
    {
        public void Configure(EntityTypeBuilder<MovieGender> builder)
        {
            builder.Property(p => p.Description).HasMaxLength(50);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.ToTable("MovieGender", "movie");
        }
    }
}