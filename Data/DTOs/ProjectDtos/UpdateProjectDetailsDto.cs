namespace Lagalt
{
  public class UpdateProjectDetailsDto
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string GitRepositoryUrl { get; set; }
    public ProjectImageDto ProjectImage { get; set; } = new();

    public IndustryNameDto Industry { get; set; } = new();
    public List<TagNameDto> TagNames { get; set; } = new();
    public List<SkillNameDto> SkillNames { get; set; } = new();
  }
}