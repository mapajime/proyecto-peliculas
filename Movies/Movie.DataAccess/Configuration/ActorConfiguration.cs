using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Entities;

namespace Movies.DataAccess.Configuration
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.Property(p => p.DateOfBirth);
            builder.Property(P => P.FirstName).HasMaxLength(50).IsRequired();
            builder.HasOne(p => p.Gender).WithMany().HasForeignKey(a => a.GenderId);
            builder.Property(p => p.Height).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(50).IsRequired();
            builder.HasOne(p => p.Nacionality).WithMany().HasForeignKey(a => a.NationalityId);
            builder.Property(p => p.NetWorth);
            builder.Property(p => p.NumberOfChildren);
            builder.Property(p => p.Weight);
            builder.HasMany(p => p.Movies).WithMany(m => m.Cast);
            builder.ToTable("Actors", "movie");
        }
    }
}