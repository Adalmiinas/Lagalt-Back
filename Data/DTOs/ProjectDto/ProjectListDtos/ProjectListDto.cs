namespace Lagalt
{
  public class ProjectListDto
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string GitRepositoryUrl { get; set; }

    public List<ProjectListUserDto> ProjectUsers { get; set; }
    public IndustryNameDto Industry { get; set; }
    public List<TagNameDto> Tags { get; set; }
    public List<ProjectSkillListDto> Skills { get; set; }
    // public List<WaitListDto> waii { get; set; }
  }
}