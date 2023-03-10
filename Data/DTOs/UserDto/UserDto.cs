namespace lagalt
{
  public class UserDto
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string CareerTitle { get; set; }

    public string Email { get; set; }

    public string Portfolio { get; set; }

    public string Description { get; set; }


    // public List<SearchWordModel> SearchWords { get; set; } = new();


    public List<ProjectDto> ProjectUsers { get; set; }
    public List<SkillDto> Skills { get; set; } = new();

    // public PhotoModel Photo { get; set; }


  }
}