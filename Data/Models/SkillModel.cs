namespace lagaltApp
{
  public class SkillModel
  {
    public int Id { get; set; }
    
    public string SkillName { get; set; }

    public int UserId { get; set; }
    public UserModel User { get; set; }


    public int ProjectId { get; set; }
    public ProjectModel Project { get; set; }


  }
}