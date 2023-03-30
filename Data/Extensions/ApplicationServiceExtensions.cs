
using System.Text.Json;
using System.Text.Json.Serialization;
using Lagalt;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
namespace lagalt.Data.Extensions
{

  public static class ApplicationServicesExtensions
  {

    /// <summary>
    /// THIS FILE IS FRO SERVICES CONFIGURATION 
    /// add automapper
    ///create openapi configs
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
      
    
   
      services.AddDbContext<DataContext>(option =>
      {

        option.UseSqlServer(config.GetConnectionString("Db"));

      });

      //ADD INTERFACE REPO / CONCRETE REPO
      services.AddControllers().AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.ReferenceHandler = null;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
      });


      //swagger for documentation
      //services.AddSwaggerGen(c =>
      //{
      //  c.SwaggerDoc("v1", new OpenApiInfo
      //  {
      //    Version = "v1",
      //    Title = "CUAPP",
      //    Description = "Web apis for creating projects",

      //  });
      //  c.ExampleFilters();
      //  var xmlFile = "./bin/LagalDocument.xml";
      //  var xmlPath = Path.Combine(xmlFile);
      //  c.IncludeXmlComments(xmlPath);
      //});
      //services.AddSwaggerExamplesFromAssemblyOf<Program>();
      services.AddCors();
      services.AddScoped<IUserAccountRepository, RegisterUserRepository>();
      services.AddScoped<IProjectRepository, ProjectRepository>();
      services.AddScoped<IAppUserRepository, AppUserRepository>();
      services.AddScoped<IProjectUserRepository, ProjectUserRepository>();
      services.AddScoped<IMessageBoardRepository, MessageBoardRepository>();

      //automapper
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddAutoMapper(typeof(AppDomain));

      return services;
    }
  }
}
