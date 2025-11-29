using LTL.Manager.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Validation;

namespace LTL.Manager.Infrastructure;

public static class Infrastructure
{
  public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
  {
    var infrastructureSettings = configuration.GetSection(nameof(InfrastructureSettings)).Get<InfrastructureSettings>()!;
    services.AddSingleton(infrastructureSettings);
    Requires.NotNull(infrastructureSettings, nameof(infrastructureSettings));
    services.AddMemoryCache();
    services.InitializeEntityFramework(infrastructureSettings.ConnectionString);
  }
}
