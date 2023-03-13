using Microsoft.AspNetCore.Mvc;

namespace Lagalt
{
  public interface IAppUserRepository
  {
    Task<List<ProjectUserDto>> UserAdminProjectsAsync(int id);
    Task<List<ProjectUserDto>> UserProjectsAsync(int id);
  }
}