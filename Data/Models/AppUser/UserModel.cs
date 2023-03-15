using Lagalt;

namespace lagaltApp
{
  public class UserModel
  {

    public int Id { get; set; } = new();
    public string Username { get; set; }
    public string CareerTitle { get; set; }


    public string Password { get; set; }

    public string Email { get; set; }

    public string Portfolio { get; set; }

    public string Description { get; set; }


    public List<SearchWordModel> SearchWords { get; set; }
    public List<ProjectUserModel> ProjectUsers { get; set; }
    public List<SkillModel> Skills { get; set; }

    public PhotoModel Photo { get; set; }

    public List<UserInWaitingListModel> UsersInWaitingLists { get; set; } = new();

    //WIP 
    // public List<AppliedProjectHistoryModel> AppliedProjectHistories { get; set; }
    // public List<ClickedProjectHistoryModel> ClickedProjectHistories { get; set; }

  }

}
