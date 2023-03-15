namespace Lagalt
{
  public class UpdateMessageBoardDto
  {
    public int messageboardId { get; set; }
    // public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    //if doesnt work create another table for messages
    public int ProjectId { get; set; }
  }
}