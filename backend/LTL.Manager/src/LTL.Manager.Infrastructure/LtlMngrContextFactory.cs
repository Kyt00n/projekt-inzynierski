using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LTL.Manager.Infrastructure;

public class LtlMngrContextFactory : IDesignTimeDbContextFactory<LtlMngrContext>
{
  public LtlMngrContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<LtlMngrContext>();
    optionsBuilder.UseSqlServer("Server=.;Database=LtlMngrDb;Trusted_Connection=True;",
      sqlOptions => { sqlOptions.EnableRetryOnFailure().SqlServerConfigure(); });

    return new LtlMngrContext(optionsBuilder.Options);
  }
}
