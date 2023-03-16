namespace lagaltApp
{
  public class ChatModel
  {
    public int Id { get; set; }

    public List<ChatMessageModel> ChatMessages { get; set; } = new();

    public int ProjectId { get; set; }
    public ProjectModel ProjectModel { get; set; }
  }
}