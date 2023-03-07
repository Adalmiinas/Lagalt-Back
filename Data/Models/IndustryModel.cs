namespace lagaltApp
{
  public class IndustryModel
  {
    public int Id { get; set; }
    public string IndustryName { get; set; }

    public List<ProjectModel> Projects { get; set; }
  }
}