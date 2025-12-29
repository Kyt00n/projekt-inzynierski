using Microsoft.EntityFrameworkCore;

namespace LTL.Manager.Infrastructure;

public class LtlMngrContextScopedFactory : IDbContextFactory<LtlMngrContext>
{
  private readonly IDbContextFactory<LtlMngrContext> _instance;

  public LtlMngrContextScopedFactory(IDbContextFactory<LtlMngrContext> instance)
  {
    _instance = instance;
  }

  public LtlMngrContext CreateDbContext()
  {
    return _instance.CreateDbContext();
  }
}
