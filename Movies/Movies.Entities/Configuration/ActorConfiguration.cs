using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movies.Entities.Configuration
{
    internal class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.Property(p => p.DateOfBirth);
            builder.Property(p => p.CreatedAt);
            builder.Property(P => P.FirtsName).HasMaxLength(50).IsRequired();
            builder.HasOne(p => p.Gender);
            builder.Property(p => p.Height).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.ModifiedAt);
            builder.HasOne(p => p.Nacionality);
            builder.Property(p => p.NetWorth);
            builder.Property(p => p.NumberOfChildren);
            builder.Property(p => p.Weight);
            builder.HasKey(p => p.Id);
            builder.ToTable("Actor", "movie");
        }
    }
}