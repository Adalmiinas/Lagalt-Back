using Microsoft.AspNetCore.Mvc;

namespace Lagalt
{
  public interface IProjectUserRepository
  {
    //Delete character from userproject list of the project
    Task<IActionResult> RemoveProjectUserFromProject(int projectId, RemoveProjectUserDto removeProjectUser);

    Task<IActionResult> AddOrRemoveUserFromProjectListAsync(int ownerId, UserInWaitingListDto userInWaitingList);

    Task<IActionResult> AddUserToWaitListAsync(int Id, ApplyProjectDto applyProject);
  }
}