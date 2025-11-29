using LTL.Manager.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace LTL.Manager.Infrastructure;

public class LtlMngrContext : DbContext
{
  internal DbSet<User> Users { get; set; }
  
  public LtlMngrContext(DbContextOptions<LtlMngrContext> options) : base(options)
  {
  }
  public LtlMngrContext() {}
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    
  }
}
