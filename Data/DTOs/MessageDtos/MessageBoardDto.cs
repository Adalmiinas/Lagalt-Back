using System.Text.Json.Serialization;

namespace Lagalt
{
  public class MessageBoardDto
  {
    public int Id { get; set; }
    public string Title { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;
    public string Body { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }

    //if doesnt work create another table for messages
    public int ProjectId { get; set; }

  }
}