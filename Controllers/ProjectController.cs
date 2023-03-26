using Lagalt;
using Microsoft.AspNetCore.Authorization;
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
    /// 
    [AllowAnonymous]
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
    /// 

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
    [HttpPost("create")]
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
    [HttpPut("update")]
    public async Task<IActionResult> UpdateProjectAsync([FromHeader] int id, [FromBody] UpdateProjectDetailsDto updateProjectDetails)
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
    [HttpPatch("update")]
    public async Task<IActionResult> PatchUserStatusAsync([FromHeader] int id, [FromBody] PatchProjectStatusDto patchProjectStatus)
    {
      try
      {
        return await _projectRepository.PatchProjectStatusAsync(id, patchProjectStatus);
      }
      catch (Exception ex)
      {

        throw new Exception("Couldnt update the project ", ex);
      }
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteProjectAsync([FromHeader] int userId, [FromBody] DeleteProjectDto deleteProject)
    {
      try
      {
        return await _projectRepository.DeleteProjectAsync(userId, deleteProject);
      }
      catch (Exception ex)
      {

        throw new Exception("Couldnt update the project ", ex);
      }
    }



  }
}