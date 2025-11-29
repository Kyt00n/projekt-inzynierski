using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LTL.Manager.WebApi;

public static class WebApi
{
 public static void ConfigureWebApiServices(this IServiceCollection services)
 {
   services.AddControllers();
   //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
 }
}
