using Lagalt;
using Microsoft.AspNetCore.Mvc;

namespace lagalt
{
  public interface IProjectRepository
  {
    Task<ActionResult<List<ProjectListDto>>> GetProjectsAsync();
    Task<ActionResult<ProjectDto>> GetProjectAsync(int id);
    Task<ActionResult<List<ProjectListDto>>> GetProjectsAsync(string name);

    Task<IActionResult> CreateProjectAsync(int id, CreateProjectDto createProjectDto);

    //update just project data
    Task<IActionResult> UpdateProjectAsync(int id, UpdateProjectDetailsDto updateProjectDto);


  }
}