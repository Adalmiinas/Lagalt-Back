namespace lagaltApp
{
  public class MessageBoardModel
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public string Username { get; set; }
    public int UserId { get; set; }
    public UserModel UserModel { get; set; }

    //if doesnt work create another table for message
    public int ProjectId { get; set; }
    public ProjectModel Project { get; set; }
  }
}