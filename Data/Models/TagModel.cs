namespace lagaltApp
{
  public class TagModel
  {
    public int Id { get; set; }

    public string TagName { get; set; }

    public List<ProjectModel> Project { get; set; } = new();
  }
}