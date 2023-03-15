using lagalt;

namespace Lagalt
{
  public class ProjectUserWithoutUserDataDto
  {
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Title { get; set; }

    public bool IsOwner { get; set; }
  }
}