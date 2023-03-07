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

    [Required]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }

    public string Portfolio { get; set; }

    public string Description { get; set; }


    public int OwnerId { get; set; }
    public List<ProjectOwnerModel> ProjectOwner { get; set; }
    // public List<SearchWordModel> SearchWords { get; set; }

    
     public List<ProjectModel> Projects { get; set; }
     public List<SkillModel> Skills { get; set; }
      public PhotoModel Photo { get; set; }

  }

}
