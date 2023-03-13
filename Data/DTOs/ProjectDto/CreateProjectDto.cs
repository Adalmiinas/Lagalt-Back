using lagalt;

namespace Lagalt
{
  public class CreateProjectDto
  {
    // public int Id { get; set; }
    // public int OwnerId { get; set; }
    // public UserNameDto Owner { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string GitRepositoryUrl { get; set; }

    public IndustryNameDto IndustryName { get; set; }
    public List<TagNameDto> TagNames { get; set; }
    public List<SkillNameDto> SkillNames { get; set; }
  }
}