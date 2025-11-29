using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace LTL.Manager.Infrastructure;

public static class EntityFrameworkExtenstion
{
  internal static void SqlServerConfigure(this SqlServerDbContextOptionsBuilder builder)
  {
    builder.MigrationsHistoryTable("__EFMigrationsHistory");
  }

  internal static IServiceCollection InitializeEntityFramework(this IServiceCollection services,
    string connectionString)
  {
    services.AddPooledDbContextFactory<LtlMngrContext>(options =>
      options.UseSqlServer(connectionString, sqlOptions =>
        {
          sqlOptions.EnableRetryOnFailure().SqlServerConfigure();
        }
      )
    );
    return services;
  }

}
