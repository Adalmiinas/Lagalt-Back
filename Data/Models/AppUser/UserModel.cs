using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using lagalt.Data.Models;

namespace lagaltApp
{
  public class UserModel
  {

    public int Id { get; set; }

    [Required]
    public string Username { get; set; }
    public string CareerTitle { get; set; }


    public string Password { get; set; }

    public string Email { get; set; }

    public string Portfolio { get; set; }

    public string Description { get; set; }


    public List<ProjectOwnerModel> ProjectOwner { get; set; }
    public List<SearchWordModel> SearchWords { get; set; } = new();


    public List<ProjectModel> Projects { get; set; } = new();
    public List<SkillModel> Skills { get; set; } = new();

    public PhotoModel Photo { get; set; }

  }

}
