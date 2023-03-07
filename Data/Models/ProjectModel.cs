using lagalt.Data.Models;

namespace lagaltApp
{
  public class ProjectModel
  {
    public int Id { get; set; }
    public List<ProjectOwnerModel> ProjectOwner { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string GitRepositoryUrl { get; set; }

    public ProjectImageModel projectImage { get; set; }
    public IndustryModel Industry { get; set; }
    public List<TagModel> Tags { get; set; } = new();
    public List<ChatModel> Chats { get; set; } = new();
    public List<UserModel> UsersMembers { get; set; } = new();

    public List<MessageBoardModel> MessageBoards { get; set; } = new();
    public List<SkillModel> Skills { get; set; } = new();
  }
}