namespace Lagalt
{
  public class WaitListDto
  {
    public int? Id { get; set; }

    public List<UserInWaitingListDto> UserWaitingLists { get; set; }
  }
}