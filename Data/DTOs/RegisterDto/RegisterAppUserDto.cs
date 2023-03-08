using System.ComponentModel.DataAnnotations;
namespace lagalt
{
  public class RegisterAppUserDto
  {
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
  }
}