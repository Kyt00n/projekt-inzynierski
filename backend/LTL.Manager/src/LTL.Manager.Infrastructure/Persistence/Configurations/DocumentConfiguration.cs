using LTL.Manager.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LTL.Manager.Infrastructure.Persistence.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
  public void Configure(EntityTypeBuilder<Document> builder)
  {
    
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id).ValueGeneratedOnAdd();

    builder.Property(x => x.DocumentId).IsRequired();
    builder.Property(x => x.FileName).HasMaxLength(256);
    
    builder.Property(x => x.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
    builder.Property(x => x.LastUpdated).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
    
    builder.HasOne(d => d.Order)
      .WithMany(o => o.Documents)
      .HasForeignKey(d => d.OrderId)
      .HasPrincipalKey(o => o.OrderId);
  }
}
