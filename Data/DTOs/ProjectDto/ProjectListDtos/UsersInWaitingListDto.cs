using System.Text.Json.Serialization;

namespace Lagalt
{
  public class UserInWaitingListDto
  {
    [JsonIgnore]
    public int ProjectId { get; set; }

    public int UserId { get; set; }
    //false
    public bool PendingStatus { get; set; }
    //id we want to accept


    [JsonIgnore]
    public int? WaitListId { get; set; }
    // public WaitListModel WaitList { get; set; }
  }
}