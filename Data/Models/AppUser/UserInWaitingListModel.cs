using lagaltApp;

namespace Lagalt
{
  public class UserInWaitingListModel
  {
    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public int? Id { get; set; }
    public bool PendingStatus { get; set; } = true;
 
    public string MotivationLetter { get; set; }
    public int UserId { get; set; }
    public UserModel User { get; set; } = new();
    public int? WaitListId { get; set; }
    public WaitListModel WaitList { get; set; }


  }
}