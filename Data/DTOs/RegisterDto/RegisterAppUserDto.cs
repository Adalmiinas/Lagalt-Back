using System.ComponentModel.DataAnnotations;
namespace lagalt
{
  public class RegisterAppUserDto
  {
    public string KeycloakId { get; set; }
    public string Username { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
  }
}