using System.ComponentModel.DataAnnotations;

namespace lagaltApp
{
  public class TagModel
  {
    public int Id { get; set; }
    [Required]
    public string TagName { get; set; }

    public List<ProjectModel> Project { get; set; } = new();
  }
}