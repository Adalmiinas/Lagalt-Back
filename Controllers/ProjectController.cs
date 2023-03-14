using Lagalt;
using Microsoft.AspNetCore.Mvc;

namespace lagalt.Controllers
{
  /// <summary>
  /// Project controller to handle project related commands
  /// </summary>
  public class ProjectController : BaseApiController
  {
    private readonly IProjectRepository _projectRepository;

    /// <summary>
    /// Inject project interface
    /// </summary>
    /// <param name="projectRepository"></param>
    public ProjectController(IProjectRepository projectRepository)
    {
      _projectRepository = projectRepository;
    }

    /// <summary>
    /// Get list of projects
    /// </summary>
    /// <returns></returns>
    [HttpGet("List")]
    public async Task<ActionResult<List<ProjectListDto>>> Projects()
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

    /// <summary>
    /// Get projects by name 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("Names")]
    public async Task<ActionResult<List<ProjectListDto>>> ProjectNames(string name)
    {
      try
      {
        return await _projectRepository.GetProjectsAsync(name);
      }
      catch (Exception)
      {

        throw new Exception("Problem occured while fetching projects");
      }
    }
    /// <summary>
    /// Get project by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProjectAsync(int id)
    {
      try
      {
        return await _projectRepository.GetProjectAsync(id);
      }
      catch (Exception ex)
      {
        throw new Exception("Problems fetching project with id", ex);
      }
    }

    /// <summary>
    /// Create project, feed current user id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="createProjectDto"></param>
    /// <returns></returns>
    [HttpPost("")]
    public async Task<IActionResult> CreateProjectAsync([FromHeader] int id, [FromBody] CreateProjectDto createProjectDto)
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

    /// <summary>
    /// Update project details, only owner can update, id has to match owner
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateProjectDetails"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Remove user from project, only owner can remove
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Accept or remove user from waiting list only owner can do this
    /// </summary>
    /// <param name="ownerId"></param>
    /// <param name="usersInWaitingList"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Add User to waiting list > takes user id and project id in body 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="applyProject"></param>
    /// <returns></returns>
    [HttpPost("User/WaitList")]
    public async Task<IActionResult> AddUserToWaitListAsync(int userId, [FromBody] ApplyProjectDto applyProject)
    {
      try
      {
        return await _projectRepository.AddUserToWaitListAsync(userId, applyProject);
      }
      catch (Exception)
      {

        throw new Exception("BAD REQUEST");
      }
    }
  }
}