using LTL.Manager.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LTL.Manager.Infrastructure.Persistence.Configurations;

public class LoadConfiguration : IEntityTypeConfiguration<Load>
{
  public void Configure(EntityTypeBuilder<Load> builder)
  {
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id).ValueGeneratedOnAdd();

    builder.Property(x => x.LoadId).IsRequired();
    builder.Property(x => x.Description).HasMaxLength(512);
    builder.Property(x => x.Weight).IsRequired();
    builder.Property(x => x.Length).IsRequired();
    builder.Property(x => x.Width).IsRequired();
    builder.Property(x => x.Height).IsRequired();
    
    builder.Property(x => x.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
    builder.Property(x => x.LastUpdated).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
    builder.HasOne(x => x.Order)
      .WithMany(o => o.Loads)
      .HasForeignKey(x => x.OrderId)
      .HasPrincipalKey(o => o.OrderId);
  }
}
