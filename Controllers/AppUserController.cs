using Lagalt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lagalt.Controllers
{
  /// <summary>
  /// Handles Login and register
  /// </summary>
  public class AppUserController : BaseApiController
  {
    private readonly IAppUserRepository _appUserRepository;

    /// <summary>
    /// Inject app user Repository
    /// </summary>
    /// <param name="appUserRepository"></param>
    public AppUserController(IAppUserRepository appUserRepository)
    {
      _appUserRepository = appUserRepository;
    }



    /// <summary>
    /// Get projects where user is owner 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Get projects where user is part of
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    [HttpPut("User/{id}/Update")]
    public async Task<ActionResult<UserDto>> UpdateUserInformation(int id, [FromBody] UpdateAppUserDto updateAppUser)
    {
      try
      {
        return await _appUserRepository.UpdateUserAsync(id, updateAppUser);
      }
      catch (Exception)
      {

        throw new Exception("Couldnt update character");
      }
    }
    [HttpGet("User/{id}")]
    public async Task<ActionResult> GetUserAsync(int id)
    {
      try
      {
        return await _appUserRepository.GetUserAsync(id);
      }
      catch (Exception)
      {

        throw new Exception("cant fetch user");
      }
    }
  }
}