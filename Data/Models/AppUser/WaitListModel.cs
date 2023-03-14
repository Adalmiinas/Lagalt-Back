namespace Lagalt
{

  /// <summary>
  /// 
  /// </summary>
  public class WaitListModel
  {
    public int? Id { get; set; }
    public List<UserInWaitingListModel> UserWaitingLists { get; set; } = new();




  }
}