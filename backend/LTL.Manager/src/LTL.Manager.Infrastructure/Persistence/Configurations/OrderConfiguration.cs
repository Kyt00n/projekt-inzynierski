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
    
    builder.HasAlternateKey(x => x.OrderId);
    
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
