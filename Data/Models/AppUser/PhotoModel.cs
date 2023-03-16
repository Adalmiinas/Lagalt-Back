namespace lagaltApp
{
  /// <summary>
  /// 
  /// </summary>
  public class PhotoModel
  {
    public int Id { get; set; }
    public string Url { get; set; }

    public int UserId { get; set; }
    public UserModel User { get; set; }
  }
}