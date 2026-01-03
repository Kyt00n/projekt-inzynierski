using System.Security.Claims;
using System.Text;
using LTL.Manager.Application.Interfaces;
using LTL.Manager.Domain.Responses.UserResponses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace LTL.Manager.Infrastructure.Security;

internal sealed class TokenProvider(IConfiguration configuration) : ITokenProvider
{
  public string Create(GetUserInternalResponse user)
  {
    string secretKey = configuration["Jwt:Secret"];
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var tokenDescriptor = new SecurityTokenDescriptor()
    {
      Subject = new ClaimsIdentity(
      [
        new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("is_active", user.IsActive.ToString()),
        new Claim("role", user.IsAdmin ? "Admin" : "User"),
      ]),
      Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationMinutes")),
      SigningCredentials = credentials,
      Issuer = configuration["Jwt:Issuer"],
      Audience = configuration["Jwt:Audience"],
    };
    var tokenHandler = new JsonWebTokenHandler();
    string token = tokenHandler.CreateToken(tokenDescriptor);
    return token;
  }
}
