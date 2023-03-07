namespace lagaltApp
{
  public class TagModel
  {
    public int Id { get; set; }

    public string TagName { get; set; }

    public int ProjectId { get; set; }
    public ProjectModel Project { get; set; }
  }
}