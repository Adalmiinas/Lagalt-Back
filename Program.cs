using lagalt;
using lagalt.Data.Extensions;
using Lagalt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);
// var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddMemoryCache();
var service = builder.Services;
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(builder =>
  {

    builder.WithOrigins("https://lagalt-projects.netlify.app/", "http://localhost:3000", "lagaltkeycloak.azurewebsites.net")
        .AllowAnyHeader()
        .AllowAnyMethod();
  });
});

var host = builder.Host;
// services.AddKeycloakAuthentication(configuration);
builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
// builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

.AddJwtBearer(options =>
{
  var keyUri = builder.Configuration["KeyUri"];
  var ValidIssuer = builder.Configuration["ValidIssuer"];
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidIssuer = ValidIssuer,
    ValidAudience = "account",
    IssuerSigningKeyResolver = (token, SecurityToken, kid, parameters) =>
    {
      var client = new HttpClient();
      var keyuri = keyUri;
      //retrieve kes from kc instance to verify token
      var response = client.GetAsync(keyuri).Result;
      var responseString = response.Content.ReadAsStringAsync().Result;
      var keys = new JsonWebKeySet(responseString);
      return keys.Keys;
    },

  };

});
var app = builder.Build();
app.UseCors(builder =>
{
  builder.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseHttpsRedirection();
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
