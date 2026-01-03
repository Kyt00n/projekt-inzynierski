using LTL.Manager.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LTL.Manager.Infrastructure.Persistence.Configurations;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
  public void Configure(EntityTypeBuilder<Trip> builder)
  {
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id).ValueGeneratedOnAdd();
    
    builder.Property(x => x.TripId).IsRequired();
    builder.HasAlternateKey(x => x.TripId);
    builder.Property(x => x.Status).IsRequired();
    
    builder.Property(x => x.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
    builder.Property(x => x.LastUpdated).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");

    builder.Property(x => x.DeliveryLocation).IsRequired().HasMaxLength(256);
    
    builder.HasMany(t => t.Orders)
      .WithOne(o => o.Trip)
      .HasForeignKey(o => o.TripId)
      .OnDelete(DeleteBehavior.SetNull);
  }
}
