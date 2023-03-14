using lagalt;
using Microsoft.AspNetCore.Mvc;

namespace Lagalt
{
  public interface IAppUserRepository
  {
    Task<List<ProjectUserDto>> UserAdminProjectsAsync(int id);
    Task<List<ProjectUserDto>> UserProjectsAsync(int id);

    Task<ActionResult<UserDto>> GetUserAsync(int id);
    Task<ActionResult<UserDto>> UpdateUserAsync(int id, UpdateAppUserDto updateAppUser);
  }
}