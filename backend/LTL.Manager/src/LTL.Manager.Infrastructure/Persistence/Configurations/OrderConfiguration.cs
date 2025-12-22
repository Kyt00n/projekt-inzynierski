using LTL.Manager.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LTL.Manager.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id).ValueGeneratedOnAdd();
    
    builder.Property(x => x.OrderId).IsRequired();
    builder.HasAlternateKey(x => x.OrderId);
    builder.Property(x => x.Status).IsRequired();
    
    builder.Property(x => x.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
    builder.Property(x => x.LastUpdated).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");

    builder.Property(x => x.DeliveryLocation).IsRequired().HasMaxLength(256);
    builder.Property(x => x.PickupLocation).IsRequired().HasMaxLength(256);
    
    builder.HasMany(o => o.Loads)
      .WithOne(l => l.Order)
      .HasForeignKey(l => l.OrderId)
      .OnDelete(DeleteBehavior.Cascade);
    
    builder.HasMany(o => o.Documents)
      .WithOne(d => d.Order)
      .HasForeignKey(d => d.OrderId)
      .OnDelete(DeleteBehavior.Cascade);
    
    builder.HasOne(x=> x.Driver)
      .WithMany(u=> u.Orders)
      .HasForeignKey(x=> x.UserId)
      .HasPrincipalKey(u=> u.UserId)
      .OnDelete(DeleteBehavior.SetNull);
    
    builder.HasOne(x=> x.Trip)
      .WithMany(t=> t.Orders)
      .HasForeignKey(x=> x.TripId)
      .HasPrincipalKey(t=> t.TripId)
      .OnDelete(DeleteBehavior.SetNull);
  }
}
