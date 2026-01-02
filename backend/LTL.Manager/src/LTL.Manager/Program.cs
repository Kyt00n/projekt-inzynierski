using LTL.Manager.Application;
using LTL.Manager.Infrastructure;
using LTL.Manager.WebApi;
using Serilog;

//const string serviceName = "LTL.Manager.API";
var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var configuration = builder.Configuration;

configuration.SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: false)
  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
  .AddEnvironmentVariables();
  

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
builder.Services.ConfigureWebApiServices(configuration);
builder.Services.ConfigureInfrastructureServices(configuration);
builder.Services.ConfigureApplicationServices();

builder.WebHost.UseUrls("http://192.168.100.11:5000");
var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();

// Enable Swagger middleware
if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("Swagger:Enabled", false))
{
  app.UseSwagger();
  app.UseSwaggerUI(options =>
  {
    options.RoutePrefix = string.Empty; // serve at root
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "LTL.Manager API v1");
  });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
// app.UseFastEndpoints();

app.Run();
