namespace Lagalt
{
  public class ProjectListUserDto
  {

    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }

    public bool IsOwner { get; set; }

  }
}