namespace lagaltApp
{
  public class ProjectUserModel
  {
    public int Id { get; set; }

    public int UserId { get; set; }
    public UserModel User { get; set; }
    public int ProjectId { get; set; }
    public ProjectModel Project { get; set; }

    public bool IsOwner { get; set; }
  }
}