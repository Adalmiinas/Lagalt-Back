using System.ComponentModel.DataAnnotations;
using Lagalt;

namespace lagaltApp
{
  public class ProjectModel
  {
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [StringLength(300)]
    public string Description { get; set; }
    public string GitRepositoryUrl { get; set; }

    public ProjectImageModel projectImage { get; set; } = new();
    public IndustryModel Industry { get; set; } = new();
    public List<TagModel> Tags { get; set; } = new();
    public List<ChatModel> Chats { get; set; } = new();

    public List<ProjectUserModel> ProjectUsers { get; set; } = new();
    public List<MessageBoardModel> MessageBoards { get; set; } = new();
    public List<SkillModel> Skills { get; set; } = new();

    //project can have one waiting list 
    //many users can be in waiting list

    public int? WaitListId { get; set; }
    public WaitListModel WaitList { get; set; } = new();
  }
}