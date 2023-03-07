namespace lagaltApp
{
  public class SearchWordModel
  {
    public int Id { get; set; }
    public string Word { get; set; }

    public int userId { get; set; }
    public UserModel User { get; set; }
  }
}