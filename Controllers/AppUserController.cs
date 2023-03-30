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
        var projects = await _appUserRepository.UserProjectsAsync(id);
        return Ok(projects);
      }
      catch (Exception ex)
      {
        throw new Exception("User does not match existing user. Check current id", ex);
      }
    }

    /// <summary>
    /// Update user details
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateAppUser"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Patch user status public / private
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patchUserStatus"></param>
    /// <returns></returns>
    [HttpPatch("User/{id}")]
    public async Task<ActionResult<UserDto>> PatchUserStatus(int id, [FromBody] PatchUserStatusDto patchUserStatus)
    {
      try
      {
        return await _appUserRepository.PatchUserStatusAsync(id, patchUserStatus);
      }
      catch (Exception)
      {

        throw new Exception("Couldnt update character");
      }
    }

    /// <summary>
    /// Patch view history > user clicks projects save it
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patchUserHistory"></param>
    /// <returns></returns>
    [HttpPatch("User/{id}/viewHistory")]
    public async Task<ActionResult<UserDto>> PatchViewHistory(int id, [FromBody] PatchUserHistoryDto patchUserHistory)
    {
      try
      {
        return await _appUserRepository.PatchUserHistoryAsync(id, patchUserHistory);
      }
      catch (Exception)
      {

        throw new Exception("Couldnt update character");
      }
    }

    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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