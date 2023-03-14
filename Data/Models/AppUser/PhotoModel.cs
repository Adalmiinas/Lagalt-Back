namespace lagaltApp
{
  /// <summary>
  /// 
  /// </summary>
  public class PhotoModel
  {
    public int Id { get; set; }
    public string Url { get; set; }

    public int UserModelId { get; set; }
    public UserModel UserModel { get; set; }
  }
}