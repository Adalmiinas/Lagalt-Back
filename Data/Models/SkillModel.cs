using System.ComponentModel.DataAnnotations;

namespace lagaltApp
{
  public class SkillModel
  {
    public int Id { get; set; }

    [Required]
    public string SkillName { get; set; }

    public List<UserModel> User { get; set; } = new();
    public List<ProjectModel> Project { get; set; } = new();


  }
}