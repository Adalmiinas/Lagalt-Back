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

    //Delete character from userproject list of the project
    Task<IActionResult> RemoveCharacterFromProject(int projectId, int userId);

    Task<IActionResult> AddOrRemoveUserFromProjectListAsync(int ownerId, UserInWaitingListDto userInWaitingList);

    Task<IActionResult> AddUserToWaitListAsync(int Id, ApplyProjectDto applyProject);
  }
}