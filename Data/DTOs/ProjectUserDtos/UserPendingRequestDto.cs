namespace Lagalt
{
  public class UserPendingRequestDto
  {
    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public bool? PendingStatus { get; set; }

  }
}