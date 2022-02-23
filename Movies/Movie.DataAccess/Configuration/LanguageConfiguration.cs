using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Entities;

namespace Movies.DataAccess.Configuration
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.ModifiedAt);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.HasKey(p => p.Id);
            builder.ToTable("Languaage", "movie");
        }
    }
}