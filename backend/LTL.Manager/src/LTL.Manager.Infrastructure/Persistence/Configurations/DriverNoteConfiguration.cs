using LTL.Manager.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LTL.Manager.Infrastructure.Persistence.Configurations;

public class DriverNoteConfiguration : IEntityTypeConfiguration<DriverNote>
{
  public void Configure(EntityTypeBuilder<DriverNote> builder)
  {
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id).ValueGeneratedOnAdd();
    
    builder.Property(x => x.DriverNoteId).IsRequired();
    builder.HasAlternateKey(x => x.DriverNoteId);
    
    builder.Property(x => x.UserId).IsRequired();
    
    builder.Property(x => x.Note).IsRequired().HasMaxLength(1000);
    
    builder.Property(x => x.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
    builder.Property(x => x.LastUpdated).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");

    builder.HasOne(dn => dn.Order)
      .WithMany(o => o.DriverNotes)
      .HasForeignKey(dn => dn.OrderId)
      .HasPrincipalKey(o => o.OrderId);
  }
}
