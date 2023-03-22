using lagalt.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lagalt
{

  /// <summary>
  /// Control login and register
  /// </summary>
  [Authorize]
  public class AccountController : BaseApiController
  {
    private readonly DataContext _dataContext;
    private readonly IUserAccountRepository _userAccountRepository;

    /// <summary>
    /// Inject datacontext and account interface
    /// </summary>
    /// <param name="dataContext"></param>
    /// <param name="userAccountRepository"></param>
    public AccountController(DataContext dataContext, IUserAccountRepository userAccountRepository)
    {
      _userAccountRepository = userAccountRepository;

      _dataContext = dataContext;
    }

    /// <summary>
    /// Register user 
    /// </summary>
    /// <param name="registerAppUserDto"></param>
    /// <returns></returns>
    /// 
   
    [HttpPost("register")]
    public async Task<ActionResult<RegisterAppUserDto>> Register([FromBody] RegisterAppUserDto registerAppUserDto)
    {
      var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.KeyCloakId == registerAppUserDto.KeycloakId);
      var IsTaken = await _userAccountRepository.RegisterAsync(registerAppUserDto);
      return IsTaken;
    }

    //change to use keycloak

    /// <summary>
    /// Login user with correct login data
    /// </summary>
    /// <param name="loginDto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
    {

      var IsAuhtorised = await _userAccountRepository.LoginAsync(loginDto);
      if (IsAuhtorised == null)
      {
        return Unauthorized("Invalid Password / Username");
      }
      return IsAuhtorised;
    }
  }
}