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
builder.Services.ConfigureWebApiServices();



var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
// app.UseFastEndpoints();

app.Run();

