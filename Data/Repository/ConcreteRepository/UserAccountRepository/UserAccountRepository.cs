using AutoMapper;
using Lagalt;
using lagaltApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace lagalt
{

  public class RegisterUserRepository : IUserAccountRepository
  {
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public RegisterUserRepository(DataContext dataContext, IMapper mapper)
    {
      _mapper = mapper;

      _dataContext = dataContext;
    }

    public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
    {
      var IsUser = await _dataContext.Users.
  Include(p => p.ProjectUsers).
  ThenInclude(u => u.Project)
  .Include(s => s.Skills)
  .Include(w => w.UsersInWaitingLists)
  .Include(c => c.ClickedProjectHistories).ThenInclude(p => p.Project)
  .Include(a => a.AppliedProjectHistories)
  .Include(sw => sw.SearchWords).FirstOrDefaultAsync(u => u.KeyCloakId == loginDto.KeyCloakId);

      if (IsUser == null)
      {
        return new OkObjectResult("User confirmed to not exists");
      }
      else
      {
        var existingUser = _mapper.Map<UserDto>(IsUser);
        return existingUser;
      }


    }

    public async Task<ActionResult<UserDto>> LoginDevAsync(LoginDevDto loginDev)
    {
      var IsUser = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == loginDev.Username);
      if (IsUser == null)
      {
        return new OkObjectResult("User confirmed to not exists");
      }
      else
      {
        var existingUser = _mapper.Map<UserDto>(IsUser);
        return existingUser;
      }
    }

    public async Task<ActionResult<UserDto>> RegisterAsync(RegisterAppUserDto registerAppUserDto)
    {

      var IsUser = await _dataContext.Users.
      Include(p => p.ProjectUsers).
      ThenInclude(u => u.Project)
      .Include(s => s.Skills)
      .Include(w => w.UsersInWaitingLists)
      .Include(c => c.ClickedProjectHistories).ThenInclude(p => p.Project)
      .Include(a => a.AppliedProjectHistories)
      .Include(sw => sw.SearchWords).FirstOrDefaultAsync(u => u.KeyCloakId == registerAppUserDto.KeycloakId);


      if (IsUser != null)
      {
        return new OkObjectResult("User already exists.");
      }
      else
      {
        var newUser = _mapper.Map<UserModel>(registerAppUserDto);
        _dataContext.Users.Add(newUser);
        await _dataContext.SaveChangesAsync();
        return _mapper.Map<UserDto>(newUser);
      }


    }
  }
}