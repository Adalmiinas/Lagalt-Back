using Lagalt;
using Microsoft.AspNetCore.Mvc;

namespace lagalt.Controllers
{
  public class ProjectController : BaseApiController
  {
    private readonly IProjectRepository _projectRepository;
    public ProjectController(IProjectRepository projectRepository)
    {
      _projectRepository = projectRepository;
    }

    [HttpGet("List")]
    public async Task<List<ProjectListDto>> Projects()
    {
      try
      {
        return await _projectRepository.GetProjectsAsync();
      }
      catch (Exception)
      {

        throw new Exception("Couldn't fetch the projects");
      }
    }
    [HttpGet("Names")]
    public async Task<List<ProjectListDto>> ProjectNames(string name)
    {
      try
      {
        return await _projectRepository.GetProjectsAsync(name);
      }
      catch (Exception)
      {

        throw new Exception("Couldn't fetch the projects");
      }
    }
    [HttpGet("{id}")]
    public async Task<ProjectDto> GetProjectAsync(int id)
    {
      try
      {
        return await _projectRepository.GetProjectAsync(id);
      }
      catch (Exception ex)
      {

        throw new Exception("Id was not found", ex);
      }
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateProjectAsync(int id, [FromBody] CreateProjectDto createProjectDto)
    {
      try
      {
        return await _projectRepository.CreateProjectAsync(id, createProjectDto);
      }
      catch (Exception ex)
      {

        throw new Exception("Couldn't create project", ex);
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProjectAsync(int id, [FromBody] UpdateProjectDetailsDto updateProjectDetails)
    {
      try
      {
        return await _projectRepository.UpdateProjectAsync(id, updateProjectDetails);
      }
      catch (Exception ex)
      {

        throw new Exception("Couldnt update the project ", ex);
      }
    }

    [HttpDelete("{Id}/projectUser")]
    public async Task<IActionResult> RemoveUserFromProject(int Id, [FromBody] int userId)
    {
      try
      {
        return await _projectRepository.RemoveCharacterFromProject(Id, userId);
      }
      catch (Exception ex)
      {

        throw new Exception("Problem happened removing character", ex);
      }
    }

    [HttpPatch("owner/{ownerId}/waitlist")]
    public async Task<IActionResult> AcceptOrRemoveUserFromProject(int ownerId, [FromBody] UserInWaitingListDto usersInWaitingList)
    {
      try
      {
        return await _projectRepository.AddOrRemoveUserFromProjectListAsync(ownerId, usersInWaitingList);
      }
      catch (Exception ex)
      {

        throw new Exception("Bad request CANNOT UPDATE USER LIST STATE", ex);

      }
    }

    [HttpPost("User/WaitList")]

    public async Task<IActionResult> AddUserToWaitListAsync(int userId, [FromBody] UserInWaitingListDto waitList)
    {
      try
      {
        return await _projectRepository.AddUserToWaitListAsync(userId, waitList);
      }
      catch (Exception)
      {

        throw new Exception("BAD REQUEST");
      }
    }
  }
}