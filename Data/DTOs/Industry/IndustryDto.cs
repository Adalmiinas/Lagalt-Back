using lagalt;

namespace Lagalt
{
  public class IndustryDto
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public List<ProjectDto> Projects { get; set; }
  }
}