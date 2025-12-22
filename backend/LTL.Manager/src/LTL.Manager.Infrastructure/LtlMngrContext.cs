using LTL.Manager.Infrastructure.Persistence.Configurations;
using LTL.Manager.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace LTL.Manager.Infrastructure;

public class LtlMngrContext : DbContext
{
  internal DbSet<User> Users { get; set; }
  internal DbSet<Order> Orders { get; set; }
  internal DbSet<Trip> Trips { get; set; }
  
  
  public LtlMngrContext(DbContextOptions<LtlMngrContext> options) : base(options)
  {
  }
  public LtlMngrContext() {}
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new UserConfiguration());
    modelBuilder.ApplyConfiguration(new OrderConfiguration());
    modelBuilder.ApplyConfiguration(new LoadConfiguration());
    modelBuilder.ApplyConfiguration(new DocumentConfiguration());
  }
}
