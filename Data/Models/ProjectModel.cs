using lagalt.Data.Models;

namespace lagaltApp
{
  public class ProjectModel
  {
    public int Id { get; set; }
    //manually config this
    //  public UserModel User { get; set; }

    //USER ID 
    //PROJECT 
    public int OwnerId { get; set; }
    // public ProjectOwnerModel ProjectOwner { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string GitRepositoryUrl { get; set; }

    public ProjectImageModel projectImage { get; set; }
    public IndustryModel Industry { get; set; }
    public List<TagModel> Tags { get; set; }
    public List<ChatModel> Chats { get; set; }
    public List<UserModel> UsersMembers { get; set; }

    public List<MessageBoardModel> MessageBoards { get; set; }
    public List<SkillModel> Skills { get; set; }
  }
}