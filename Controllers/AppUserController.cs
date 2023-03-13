using Lagalt;
using Microsoft.AspNetCore.Mvc;

namespace lagalt.Controllers
{
  public class AppUserController : BaseApiController
  {
    private readonly IAppUserRepository _appUserRepository;
    public AppUserController(IAppUserRepository appUserRepository)
    {
      _appUserRepository = appUserRepository;
    }

    //get all

    [HttpGet("User/{id}/AdminProjects")]
    public async Task<ActionResult<List<ProjectUserDto>>> AppUserAdminProjects(int id)
    {
      try
      {
        var project = await _appUserRepository.UserAdminProjectsAsync(id);
        return Ok(project);
      }
      catch (Exception ex)
      {

        throw new Exception("User does not match existing user. Check current id", ex);
      }
    }
    [HttpGet("User/{id}/Projects")]
    public async Task<ActionResult<List<ProjectUserDto>>> AppUserProjects(int id)
    {
      try
      {
        var projects = await _appUserRepository.UserAdminProjectsAsync(id);
        return Ok(projects);
      }
      catch (Exception ex)
      {
        throw new Exception("User does not match existing user. Check current id", ex);
      }
    }
  }
}