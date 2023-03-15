using AutoMapper;
using lagalt;
using lagaltApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lagalt
{
  public class ProjectUserRepository : IProjectUserRepository
  {
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    public ProjectUserRepository(DataContext dataContext, IMapper mapper)
    {
      _mapper = mapper;
      _dataContext = dataContext;
    }

    /// <summary>
    /// ADD OR REMOVE USER FROM PROJECT LIST
    /// </summary>
    /// <param name="ownerId"></param>
    /// <param name="userPendingRequest"></param>
    /// <returns></returns>
    public async Task<IActionResult> AddOrRemoveUserFromProjectListAsync(int ownerId, UserPendingRequestDto userPendingRequest)
    {

      var Owner = await _dataContext.ProjectUsers.Include(pu => pu.Project).ThenInclude(p => p.WaitList).Include(p => p.User).FirstOrDefaultAsync(pu => pu.UserId == ownerId && pu.IsOwner == true && pu.ProjectId == userPendingRequest.ProjectId);
      var ApplyingUser = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userPendingRequest.UserId);
      var exisitingProject = await _dataContext.Projects.Include(p => p.WaitList).FirstOrDefaultAsync(p => p.Id == userPendingRequest.ProjectId);
      // var ChangeStatus = exisitingProject.WaitList.UserWaitingLists.FirstOrDefault(p => p.UserId == ApplyingUser.Id);
      var WaitList = await _dataContext.UsersInWaitingLists.FirstOrDefaultAsync(uw => uw.UserId == userPendingRequest.UserId);
      var IsDuplicate = await _dataContext.ProjectUsers.Include(pu => pu.Project).FirstOrDefaultAsync(p => p.UserId == userPendingRequest.UserId && p.ProjectId == userPendingRequest.ProjectId && p.IsOwner == false);
      //  var ApplyingUserDto = _mapper.Map<ProjectUserDto>(ApplyingUser);

      if (ApplyingUser == null) return new BadRequestObjectResult("Invalid User id, user does not exist");

      if (WaitList == null) return new BadRequestObjectResult("User  not is  in the list");

      if (IsDuplicate != null) return new BadRequestObjectResult("User is already part of the project");

      if (Owner == null) return new BadRequestObjectResult("Invalid Owner Id / Invalid Project");

      if (userPendingRequest.PendingStatus == true) return new BadRequestObjectResult("Nothing was changed...false patching");

      if (userPendingRequest.PendingStatus == false)
      {
        Owner.Project.ProjectUsers.Add(_mapper.Map<ProjectUserModel>(ApplyingUser));
      }
      _dataContext.UsersInWaitingLists.Remove(WaitList);
      await _dataContext.SaveChangesAsync();
      return new NoContentResult();
    }

    public async Task<IActionResult> AddUserToWaitListAsync(int Id, ApplyProjectDto applyProject)
    {
      //find user 
      //find 
      var exisitingUser = await _dataContext.Users.FindAsync(Id);
      var existingWaitList = await _dataContext.Projects.Include(p => p.WaitList).ThenInclude(uw => uw.UserWaitingLists).FirstOrDefaultAsync(p => p.Id == applyProject.ProjectId);
      // var existingProjectWaitLIst = await _dataContext.WaitLists.FirstOrDefaultAsync(uw => uw.Id == existingWaitList.WaitList.Id);
      var wl = await _dataContext.WaitLists.Include(wl => wl.UserWaitingLists).FirstOrDefaultAsync(wl => wl.Id == existingWaitList.WaitListId);
      var userInWaiting = new UserInWaitingListModel()
      {
        PendingStatus = true,
        WaitListId = existingWaitList.Id,
        WaitList = existingWaitList.WaitList,
        UserId = exisitingUser.Id,
        User = exisitingUser,
        MotivationLetter = applyProject.MotivationLetter
      };
      if (exisitingUser == null) return new BadRequestObjectResult("Incorrect user id");
      if (existingWaitList == null) return new BadRequestObjectResult("User does not exist in list");
      _dataContext.UsersInWaitingLists.Add(userInWaiting);
      await _dataContext.SaveChangesAsync();
      return new OkResult();
    }

    public async Task<IActionResult> RemoveProjectUserFromProject(int userId, RemoveProjectUserDto removeProjectUser)
    {
      //1. find project id 
      //2. include users 
      //find user id in project and remove 
      var existingProject = await _dataContext.Projects
      .Include(p => p.ProjectUsers)
      .FirstOrDefaultAsync(p => p.Id == removeProjectUser.ProjectId);
      if (existingProject == null) return new BadRequestObjectResult("Incorrect project id");

      var user = existingProject.ProjectUsers.FirstOrDefault(pu => pu.Id == userId);
      var exisitingUser = existingProject.ProjectUsers.FirstOrDefault(pu => pu.UserId == removeProjectUser.UserId && pu.IsOwner == false);
      if (exisitingUser == null || user.IsOwner != true && user.UserId != userId) throw new Exception("You do not meet the right to remove user / User does not exist");
      if (user.IsOwner == false && user.UserId != userId) throw new Exception("You cannot remove others / User does not exist");

      existingProject.ProjectUsers.Remove(exisitingUser);
      await _dataContext.SaveChangesAsync();
      return new NoContentResult();
    }

  }
}