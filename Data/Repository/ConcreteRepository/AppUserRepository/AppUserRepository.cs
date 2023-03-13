using System.ComponentModel;
using AutoMapper;
using lagalt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lagalt
{
  public class AppUserRepository : IAppUserRepository
  {
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    public AppUserRepository(DataContext dataContext, IMapper mapper)
    {
      _mapper = mapper;
      _dataContext = dataContext;
    }
    public async Task<List<ProjectUserDto>> UserAdminProjectsAsync(int id)
    {
      var isUserId = await _dataContext.ProjectUsers.FindAsync(id);
      if (isUserId == null)
      {
        throw new Exception("Bad id");
      }

      var ProjectOwnerProjects = await _dataContext.ProjectUsers
      .Include(pu => pu.Project)
      .ThenInclude(p => p.Industry)
      .Include(t => t.Project.Tags)
      .Include(t => t.Project.Skills)
      .Where(pu => pu.IsOwner == true && pu.UserId == id).ToListAsync();

      return _mapper.Map<List<ProjectUserDto>>(ProjectOwnerProjects);
    }
    public async Task<List<ProjectUserDto>> UserProjectsAsync(int id)
    {

      //check user id validity
      var isUserId = await _dataContext.ProjectUsers.FindAsync(id);
      if (isUserId == null)
      {
        throw new Exception("Bad id");
      }

      var ProjectOwnerProjects = await _dataContext.ProjectUsers
     .Include(pu => pu.Project)
     .ThenInclude(p => p.Industry)
     .Include(t => t.Project.Tags)
     .Include(t => t.Project.Skills)
     .Where(pu => pu.UserId == id).ToListAsync();

      return _mapper.Map<List<ProjectUserDto>>(ProjectOwnerProjects);
    }
  }
}