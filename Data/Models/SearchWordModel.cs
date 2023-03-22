namespace lagaltApp
{
  public class SearchWordModel
  {
    public int Id { get; set; }
    public string Word { get; set; }
    
    public List<UserModel> User { get; set; }
  }
}