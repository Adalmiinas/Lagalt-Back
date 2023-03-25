using System.ComponentModel.DataAnnotations;

namespace lagaltApp
{
  public class MessageBoardModel
  {
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    [StringLength(250)]
    public string Body { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [Required]
    public string Username { get; set; }
    public int UserId { get; set; }
    public UserModel UserModel { get; set; }

    //if doesnt work create another table for message
    public int ProjectId { get; set; }
    public ProjectModel Project { get; set; }
  }
}