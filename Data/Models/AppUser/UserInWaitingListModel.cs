using lagaltApp;

namespace Lagalt
{
  public class UserInWaitingListModel
  {
    public int? Id { get; set; }
    public bool PendingStatus { get; set; }

    public int? UserId { get; set; } 
    public UserModel User { get; set; } = new();
    public int? WaitListId { get; set; }
    public WaitListModel WaitList { get; set; }


  }
}