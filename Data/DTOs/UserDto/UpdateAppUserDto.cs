using lagalt;
using lagaltApp;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lagalt
{
  public class UpdateAppUserDto
  {

    public string CareerTitle { get; set; }

    public string Portfolio { get; set; }

    public string Description { get; set; }

    public string PhotoUrl { get; set; }


    // public List<SearchWordModel> SearchWords { get; set; } = new();


    // public List<ProjectDto> ProjectUsers { get; set; }
    public List<SkillDto> Skills { get; set; } = new();


    public List<AppliedProjectHistoryDto> AppliedProjectHistories { get; set; } = new();
    public List<ClickedProjectHistoryDto> ClickedProjectHistories { get; set; } = new();
    public List<SearchWordDto> SearchWords { get; set; } = new();


  }
}