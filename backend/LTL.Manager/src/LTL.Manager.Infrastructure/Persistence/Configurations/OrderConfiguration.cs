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
    
    builder.Property(x => x.Status).IsRequired();
    
    builder.Property(x => x.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
    builder.Property(x => x.LastUpdated).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");

    builder.Property(x => x.DeliveryLocation).IsRequired().HasMaxLength(256);
    builder.Property(x => x.PickupLocation).IsRequired().HasMaxLength(256);
    builder.HasAlternateKey(x => x.OrderId);
    
    builder.OwnsMany(x => x.Loads, lb =>
    {
      lb.HasKey(l => l.Id);
      lb.Property(l => l.Id).ValueGeneratedOnAdd();
      lb.Property(l => l.LoadId).IsRequired();
      lb.Property(l => l.Description).HasMaxLength(512);
      lb.Property(l => l.Weight).IsRequired();
      lb.Property(l => l.Length).IsRequired();
      lb.Property(l => l.Width).IsRequired();
      lb.Property(l => l.Height).IsRequired();
    });
    
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
