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
            builder.Property(P => P.FirtsName).HasMaxLength(50).IsRequired();
            builder.HasOne(p => p.Gender);
            builder.Property(p => p.Height).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(50).IsRequired();
            builder.HasOne(p => p.Nacionality);
            builder.Property(p => p.NetWorth);
            builder.Property(p => p.NumberOfChildren);
            builder.Property(p => p.Weight);
            builder.ToTable("Actors", "movie");
        }
    }
}