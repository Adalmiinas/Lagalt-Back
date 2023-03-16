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

      var userData = await File.ReadAllTextAsync("Data/MigrationData/UserSeed.json");
      var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

      var users = JsonSerializer.Deserialize<List<UserModel>>(userData);

      foreach (var characterData in users)
      {

        context.Users.Add(characterData);
      }

      await context.SaveChangesAsync();
    }
    public static async Task FixDummyUser(DataContext context)
    {
      var isNull = await context.Users.Where(u => u.Username == null).ToListAsync();
      if (isNull == null) return;

      var rand = new Random();
      string[] usernames = { "CHingCONG", "Butter", "FJORD", "Maazzkar", "Fjerla", "Carla", "Voidman" };
      string[] emails = { "hellowlrd", "chicken", "mayonese", "Maaadasdzzkar", "Fjerd", "Carla", "mildy" };
      string[] passwords = { "test123", "nugget123", "123", "mul123", "mal123", "melttu", "test69" };

      foreach (var userdetails in isNull)
      {
        var findUser = await context.Users.FindAsync(userdetails.Id);
        var user = new UserModel
        {
          Id = findUser.Id,
          Username = usernames[rand.Next(0, usernames.Length)],
          Password = passwords[rand.Next(0, passwords.Length)],
          CareerTitle = "Pro gamer" + rand.Next(1, 30),
          Email = usernames[rand.Next(0, usernames.Length)] + "@" + emails[rand.Next(0, emails.Length)] + ".Com",
          Portfolio = "Lol",
          Description = "Hey im weeb"

        };
        findUser.Id = findUser.Id;
        findUser.Username = user.Username;
        findUser.Password = user.Password;
        findUser.CareerTitle = user.CareerTitle;
        findUser.Email = user.Email;
        findUser.Portfolio = user.Portfolio;
        findUser.Description = user.Description;
        context.Entry(findUser).State = EntityState.Modified;
      }

      await context.SaveChangesAsync();
    }
  }
}