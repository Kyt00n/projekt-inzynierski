using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LTL.Manager.WebApi;

public static class WebApi
{
 public static void ConfigureWebApiServices(this IServiceCollection services, IConfiguration configuration)
 {
   services.AddControllers();
   services.AddEndpointsApiExplorer();
   services.AddSwaggerGen(options =>
   {
     options.SwaggerDoc("v1", new OpenApiInfo { Title = "LTL.Manager API", Version = "v1" });

     var bearerScheme = new OpenApiSecurityScheme
     {
       Name = "Authorization",
       Description = "Enter JWT token",
       Type = SecuritySchemeType.Http,
       Scheme = JwtBearerDefaults.AuthenticationScheme,
       BearerFormat = "JWT",
       In = ParameterLocation.Header,
     };

     options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, bearerScheme);
     var securityRequirement = new OpenApiSecurityRequirement
     {
       {
         new OpenApiSecurityScheme()
         {
            Reference = new OpenApiReference()
            {
              Type = ReferenceType.SecurityScheme,
              Id = JwtBearerDefaults.AuthenticationScheme
            }
         },[]
       }
     };
      options.AddSecurityRequirement(securityRequirement);

     // Include XML comments if present
     try
     {
       var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
       var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile ?? string.Empty);
       if (File.Exists(xmlPath))
         options.IncludeXmlComments(xmlPath);
     }
     catch (Exception ex)
     {
        Console.WriteLine($"Could not include XML comments: {ex.Message}");
     }
   });
   services.AddAuthorization();
   services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
       options.RequireHttpsMetadata = false;
       options.TokenValidationParameters = new TokenValidationParameters()
       {
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
         ValidIssuer = configuration["Jwt:Issuer"],
         ValidAudience = configuration["Jwt:Audience"],
         ClockSkew = TimeSpan.Zero,
       };
     });
 }
}
