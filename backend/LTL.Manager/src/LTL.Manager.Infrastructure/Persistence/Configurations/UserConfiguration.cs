using LTL.Manager.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LTL.Manager.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id).ValueGeneratedOnAdd();
    
    builder.Property(x => x.UserId).IsRequired();
    
    builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
    
    builder.Property(x => x.Surname).HasMaxLength(100).IsRequired();
    
    builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
    
    builder.Property(x => x.PasswordHash).IsRequired();
    
    builder.Property(x => x.IsActive).IsRequired();
    
    builder.Property(x => x.Status).IsRequired();
    
    builder.Property(x => x.CreatedOn).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
    builder.Property(x => x.LastUpdated).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
    
    builder.HasAlternateKey(x => x.UserId);
    builder.HasMany(x=> x.Orders)
      .WithOne(o=> o.Driver)
      .HasForeignKey(o=> o.UserId)
      .HasPrincipalKey(x => x.UserId)
      .OnDelete(DeleteBehavior.SetNull);
  }
}
