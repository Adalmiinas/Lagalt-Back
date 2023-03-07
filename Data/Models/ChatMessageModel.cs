namespace lagaltApp
{
  public class ChatMessageModel
  {
    public int Id { get; set; }
    public string MessageContent { get; set; }

    public int UserId { get; set; }
    public UserModel User { get; set; }

    public int ChatId { get; set; }
    public ChatModel Chat { get; set; }
  }
}