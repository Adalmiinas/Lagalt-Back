using lagalt.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Lagalt
{
  public class ProjectUserController : BaseApiController
  {
    private readonly IProjectUserRepository _projectUserRepository;
    public ProjectUserController(IProjectUserRepository projectUserRepository)
    {
      _projectUserRepository = projectUserRepository;
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
        return await _projectUserRepository.AddUserToWaitListAsync(userId, applyProject);
      }
      catch (Exception)
      {

        throw new Exception("BAD REQUEST");
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
        return await _projectUserRepository.AddOrRemoveUserFromProjectListAsync(ownerId, usersInWaitingList);
      }
      catch (Exception ex)
      {

        throw new Exception("Bad request CANNOT UPDATE USER LIST STATE", ex);

      }
    }

    /// <summary>
    /// Remove user from project, only owner cannot be removed. project id and user id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="removeProjectUser"></param>
    /// <returns></returns>
    [HttpDelete("/project")]
    public async Task<IActionResult> RemoveUserFromProject([FromHeader] int userId, [FromBody] RemoveProjectUserDto removeProjectUser)
    {
      try
      {
        return await _projectUserRepository.RemoveProjectUserFromProject(userId, removeProjectUser);
      }
      catch (Exception ex)
      {

        throw new Exception("Problem happened removing character", ex);
      }
    }
  }
}