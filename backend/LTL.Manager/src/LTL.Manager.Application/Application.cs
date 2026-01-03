using LTL.Manager.Application.Infrastructure;
using LTL.Manager.Application.Interfaces;
using LTL.Manager.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LTL.Manager.Application;

public static class Application
{
  public static void ConfigureApplicationServices(this IServiceCollection services)
  {
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<ITripService, TripService>();
  }
}
