using System.ComponentModel.DataAnnotations;
using Lagalt;
using Microsoft.EntityFrameworkCore;

namespace lagaltApp
{
  public class UserModel
  {
    public int Id { get; set; }
    public bool IsPrivate { get; set; } = false;


    public string KeyCloakId { get; set; }

    [Required]
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CareerTitle { get; set; }
    public string Photo { get; set; }


    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string Portfolio { get; set; }
    [StringLength(200)]
    public string Description { get; set; }

    public List<ProjectUserModel> ProjectUsers { get; set; } = new();
    public List<SkillModel> Skills { get; set; } = new();



    public List<UserInWaitingListModel> UsersInWaitingLists { get; set; } = new();

    //WIP 
    public List<AppliedProjectHistoryModel> AppliedProjectHistories { get; set; } = new();
    public List<ClickedProjectHistoryModel> ClickedProjectHistories { get; set; } = new();
    public List<SearchWordModel> SearchWords { get; set; } = new();

  }

}
