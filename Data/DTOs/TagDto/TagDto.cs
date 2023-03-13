using lagalt;

namespace Lagalt
{
  public class TagDto
  {
    public int Id { get; set; }

    public string TagName { get; set; }

   public List <ProjectDto> Project { get; set; }
  }
}