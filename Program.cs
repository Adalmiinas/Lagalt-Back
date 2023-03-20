using System.Security.Claims;
using Keycloak.AuthServices.Authentication;
using lagalt;
using lagalt.Data.Extensions;
using Lagalt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var service = builder.Services;
var host = builder.Host;
// services.AddKeycloakAuthentication(configuration);
// service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
// .AddJwtBearer(options =>
// {
//   options.TokenValidationParameters = new TokenValidationParameters
//   {
//     IssuerSigningKeyResolver = (token, SecurityToken, kid, parameters) => 
//     {

//     }
//   };
// });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", (ClaimsPrincipal user) =>
{
  app.Logger.LogInformation(user.Identity.Name);
}).RequireAuthorization();

//use our custom middleware
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//try to read a json file and populate database with 10 movies, characters and fransises 
using var scope = app.Services.CreateAsyncScope();
var services = scope.ServiceProvider;
try
{
  var context = services.GetRequiredService<DataContext>();
  await context.Database.MigrateAsync();
  await Seed.SeedUsers(context);
  await Seed.FixDummyUser(context);
}
catch (Exception ex)
{
  var logger = services.GetService<ILogger<Program>>();
  logger.LogError(ex, "An Error occured during migration");
}

app.Run();
