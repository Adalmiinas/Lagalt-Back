using Lagalt;
using Microsoft.EntityFrameworkCore;

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
      });

      //swagger for documentation
      // services.AddSwaggerGen(c =>
      // {
      //   c.SwaggerDoc("v1", new OpenApiInfo
      //   {
      //     Version = "v1",
      //     Title = "CHARACTER MOVIE API",
      //     Description = "Web api application to add characters , movies and fransises to sql database",

      //   });
      //   c.ExampleFilters();
      //   var xmlFile = "./bin/MovieCharacterApi.xml";
      //   var xmlPath = Path.Combine(xmlFile);
      //   c.IncludeXmlComments(xmlPath);
      // });
      // services.AddSwaggerExamplesFromAssemblyOf<Program>();
      services.AddCors();
      // services.AddScoped<ICharacterRepository, CharacterRepository>();
      // services.AddScoped<IMovieRepository, MovieRepository>();
      services.AddScoped<IUserAccountRepository, RegisterUserRepository>();
      services.AddScoped<IProjectRepository, ProjectRepository>();
      services.AddScoped<IAppUserRepository, AppUserRepository>();

      //automapper
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddAutoMapper(typeof(AppDomain));

      return services;
    }
  }
}
