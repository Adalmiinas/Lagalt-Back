using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Lagalt
{
  public static class IdentityServiceExtensions
  {
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidIssuer = "https://lagaltkeycloak.azurewebsites.net/auth/realms/Lagalt",
    ValidAudience = "account",
    IssuerSigningKeyResolver = (token, SecurityToken, kid, parameters) =>
    {
      var client = new HttpClient();
      var keyuri = "https://lagaltkeycloak.azurewebsites.net/auth/realms/Lagalt/protocol/openid-connect/certs";
      //retrieve kes from kc instance to verify token
      var response = client.GetAsync(keyuri).Result;
      var responseString = response.Content.ReadAsStringAsync().Result;
      var keys = new JsonWebKeySet(responseString);
      return keys.Keys;
    },

  };

});
      return services;
    }
  }
}