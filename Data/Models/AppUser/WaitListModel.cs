using lagaltApp;

namespace Lagalt
{
  public class WaitListModel
  {
    public int? Id { get; set; }
    public List<UserInWaitingListModel> UserWaitingLists { get; set; } = new();

  }
}