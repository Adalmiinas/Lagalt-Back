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
        var applyingUser = _mapper.Map<ProjectUserModel>(ApplyingUser);
        applyingUser.IsOwner = false;
        Owner.Project.ProjectUsers.Add(_mapper.Map<ProjectUserModel>(applyingUser));
      }
      _dataContext.UsersInWaitingLists.Remove(WaitList);
      await _dataContext.SaveChangesAsync();
      return new NoContentResult();
    }


    /// <summary>
    /// id = user id
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="applyProject"></param>
    /// <returns></returns>
    public async Task<IActionResult> AddUserToWaitListAsync(int Id, ApplyProjectDto applyProject)
    {
      //find user 
      //find 
      var exisitingUser = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
      if (exisitingUser == null) return new BadRequestObjectResult("Incorrect user id");
      var existingWaitList = await _dataContext.Projects.Include(pu => pu.ProjectUsers).Include(p => p.WaitList).ThenInclude(uw => uw.UserWaitingLists).FirstOrDefaultAsync(p => p.Id == applyProject.ProjectId);
      if (existingWaitList == null) return new BadRequestObjectResult("wait list does not exist");
      //find if user is already in list 
      var isUserInWaitingListProject = existingWaitList.WaitList.UserWaitingLists.FirstOrDefault(p => p.UserId == exisitingUser.Id);
      var isUserInProject = existingWaitList.ProjectUsers.FirstOrDefault(p => p.UserId == exisitingUser.Id);

      if (isUserInProject != null) return new BadRequestObjectResult("User is already in the project");

      if (isUserInWaitingListProject != null) return new BadRequestObjectResult("User already in the waiting list");

      var userInWaiting = new UserInWaitingListModel()
      {
        PendingStatus = true,
        WaitListId = existingWaitList.Id,
        WaitList = existingWaitList.WaitList,
        UserId = exisitingUser.Id,
        User = exisitingUser,
        MotivationLetter = applyProject.MotivationLetter
      };

      _dataContext.UsersInWaitingLists.Add(userInWaiting);
      await _dataContext.SaveChangesAsync();
      return new OkResult();
    }
    

    /// <summary>
    /// Remove user from project
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="removeProjectUser"></param>
    /// <returns></returns>
    public async Task<IActionResult> RemoveProjectUserFromProject(int userId, RemoveProjectUserDto removeProjectUser)
    {
      //1. find project id 
      //2. include users 
      //find user id in project and remove 
      var existingProject = await _dataContext.Projects
      .Include(p => p.ProjectUsers)
      .ThenInclude(u => u.User)
      .FirstOrDefaultAsync(p => p.Id == removeProjectUser.ProjectId);
      if (existingProject == null) return new BadRequestObjectResult("Incorrect project id");

      //find user to be deleted
      var user = existingProject.ProjectUsers.FirstOrDefault(pu => pu.UserId == removeProjectUser.UserId);
      var deleterUser = existingProject.ProjectUsers.FirstOrDefault(pu => pu.UserId == userId);

      if (deleterUser == null) return new BadRequestObjectResult("You cannot remove others...not even in the project...");
      if (user == null) return new BadRequestObjectResult("User not part of the project");


      if (user.IsOwner && user.UserId == userId)
      {
        // User is owner and trying to remove themselves
        return new BadRequestObjectResult("Owners cannot remove themselves from the project");
      }

      //user can delete themself but not others
      if (user.UserId == userId)
      {
        existingProject.ProjectUsers.Remove(user);
        await _dataContext.SaveChangesAsync();
        return new NoContentResult();
      }
      //user with owner status can delete all but themself
      if (deleterUser.IsOwner)
      {
        existingProject.ProjectUsers.Remove(user);
        await _dataContext.SaveChangesAsync();
        return new NoContentResult();
      }
      else
      {
        return new BadRequestObjectResult("Something went wrong deleting selected user from project");
      }
      //self delete ok or user is owner but to be deleted user is not owne

    }

  }
}