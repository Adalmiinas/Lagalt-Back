using lagalt;

namespace Lagalt
{
  public class ProjectUserDto
  {
    public int Id { get; set; }

    public int UserId { get; set; }
    public UserDto User { get; set; }
    public int ProjectId { get; set; }
    public ProjectDto Project { get; set; }

    public bool IsOwner { get; set; }
  }
}