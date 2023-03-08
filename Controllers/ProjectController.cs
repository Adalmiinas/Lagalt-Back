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

    [HttpGet("Projects")]
    public async Task<List<ProjectDto>> Projects()
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
    [HttpGet("{id}")]
    public async Task<ProjectDto> ProjectId(int id)
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
  }
}