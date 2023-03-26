using Lagalt;

namespace lagalt
{
  public class ProjectDto
  {
    public int Id { get; set; }
    public string Title { get; set; }

    public string Status { get; set; }
    public string Description { get; set; }
    public string GitRepositoryUrl { get; set; }
    public ProjectImageDto projectImage { get; set; }
    public List<ProjectListUserDto> ProjectUsers { get; set; } = new();
    public IndustryNameDto Industry { get; set; }
    public List<TagNameDto> Tags { get; set; }
    public List<SkillNameDto> Skills { get; set; }
    public List<MessageBoardDto> messageBoards { get; set; } = new();

    public int? WaitListId { get; set; }
    public WaitListDto WaitList { get; set; }


  }
}