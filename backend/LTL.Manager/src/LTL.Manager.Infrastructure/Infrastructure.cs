using System.Reflection;
using AutoMapper;
using LTL.Manager.Application.Infrastructure;
using LTL.Manager.Application.Interfaces;
using LTL.Manager.Infrastructure.Configuration;
using LTL.Manager.Infrastructure.Mapper;
using LTL.Manager.Infrastructure.Persistence.Repositories;
using LTL.Manager.Infrastructure.Security;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
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
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IOrderRepository, OrderRepository>();
    services.AddSingleton<IPasswordHasher, PasswordHasher>();
    services.AddSingleton<ITokenProvider, TokenProvider>();
    // var mapperConfig = new MapperConfiguration(mc =>
    // {
    //   mc.AddProfile(new OrderProfile());
    //   mc.AddProfile(new OrderProfile());
    // }, null);
    // IMapper mapper = mapperConfig.CreateMapper();
    // services.AddSingleton(mapper);
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

  }
}
