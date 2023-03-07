namespace lagaltApp
{
  public class ProjectImageModel
  {

    public int Id { get; set; }
    public string Url { get; set; }
    
    public int ProjectModelId { get; set; }
    public ProjectModel Project { get; set; }
  }
}