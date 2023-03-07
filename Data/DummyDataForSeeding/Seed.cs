using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt.Data.DummyDataForSeeding
{
  public class Seed
  {
    /// <summary>
    /// READ JSON FILE TO SEED DATA TO DB
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    // public static async Task SeedCharacters(DataContext context)
    // {
    //   if (await context.Characters.AnyAsync()) return;

    //   var userData = await File.ReadAllTextAsync("Data/MigrationStart/Seeding.json");

    //   var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    //   var users = JsonSerializer.Deserialize<List<Character>>(userData);

    //   foreach (var characterData in users)
    //   {

    //     context.Characters.Add(characterData);
    //   }

    //   await context.SaveChangesAsync();
    // }
  }
}