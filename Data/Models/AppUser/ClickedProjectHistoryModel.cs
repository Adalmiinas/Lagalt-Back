namespace lagaltApp
{
  /// <summary>
  /// 
  /// </summary>
  public class ClickedProjectHistoryModel
  {
    public int Id { get; set; }

    public int ProjectId { get; set; }
    public ProjectModel Project { get; set; }
  }
}