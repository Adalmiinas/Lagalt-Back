namespace lagaltApp
{
  public class SkillModel
  {
    public int Id { get; set; }
    public string SkillName { get; set; }

    public List <UserModel> User { get; set; }
    public List <ProjectModel> Project { get; set; }


  }
}