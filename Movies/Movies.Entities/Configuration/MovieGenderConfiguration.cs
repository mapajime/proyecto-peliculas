using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movies.Entities.Configuration
{
    public class MovieGenderConfiguration : IEntityTypeConfiguration<MovieGender>
    {
        public void Configure(EntityTypeBuilder<MovieGender> builder)
        {
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.ModifiedAt);
            builder.Property(p => p.Description).HasMaxLength(50);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.HasKey(p => p.Id);
            builder.ToTable("MovieGender", "movie");
        }
    }
}