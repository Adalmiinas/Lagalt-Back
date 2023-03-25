using System.ComponentModel.DataAnnotations;

namespace lagaltApp
{
  public class IndustryModel
  {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public List<ProjectModel> Projects { get; set; } = new();
  }
}