using System.Text.Json;
using lagaltApp;
using Microsoft.EntityFrameworkCore;

namespace lagalt
{
  public class Seed
  {
    // / <summary>
    // / READ JSON FILE TO SEED DATA TO DB
    // / </summary>
    // / <param name="context"></param>
    // / <returns></returns>
    public static async Task SeedUsers(DataContext context)
    {
      if (await context.Projects.AnyAsync()) return;

      Console.WriteLine("Viisiting seeding");
      var userData = await File.ReadAllTextAsync("Data/MigrationData/UserSeed.json");
      var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

      var users = JsonSerializer.Deserialize<List<UserModel>>(userData);

      foreach (var characterData in users)
      {

        context.Users.Add(characterData);
      }

      await context.SaveChangesAsync();
    }
  }
}