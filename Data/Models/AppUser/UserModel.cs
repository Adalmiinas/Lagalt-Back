using Lagalt;

namespace lagaltApp
{
  public class UserModel
  {

    public int Id { get; set; }
    public bool IsPrivate { get; set; } = false;
    public string Username { get; set; }
    public string CareerTitle { get; set; }


    public string Password { get; set; }

    public string Email { get; set; }

    public string Portfolio { get; set; }

    public string Description { get; set; }

    public List<ProjectUserModel> ProjectUsers { get; set; } = new();
    public List<SkillModel> Skills { get; set; } = new();

    public PhotoModel Photo { get; set; } = new();

    public List<UserInWaitingListModel> UsersInWaitingLists { get; set; } = new();

    //WIP 
    public List<AppliedProjectHistoryModel> AppliedProjectHistories { get; set; } = new();
    public List<ClickedProjectHistoryModel> ClickedProjectHistories { get; set; } = new();
    public List<SearchWordModel> SearchWords { get; set; } = new();

  }

}
