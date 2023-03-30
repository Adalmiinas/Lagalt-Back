using lagalt.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace lagalt
{

  /// <summary>
  /// Control login and register
  /// just authorize login and register 
  /// </summary>

  [Authorize]
  public class AccountController : BaseApiController
  {
    private readonly DataContext _dataContext;
    private readonly IUserAccountRepository _userAccountRepository;

    /// <summary>
    /// Register and check if user tries to duplicate the code
    /// </summary>
    /// <param name="dataContext"></param>
    /// <param name="userAccountRepository"></param>

    public AccountController(DataContext dataContext, IUserAccountRepository userAccountRepository)
    {


      _dataContext = dataContext;
      _userAccountRepository = userAccountRepository;


    }

    /// <summary>
    /// Register user 
    /// </summary>
    /// <param name="registerAppUserDto"></param>
    /// <returns></returns>
    /// 

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterAppUserDto registerAppUserDto)
    {

      var isUser = await _dataContext.Users.AnyAsync(u => u.KeyCloakId == registerAppUserDto.KeycloakId);
      if (isUser)
      {
        return new BadRequestObjectResult("User already exists no need to register, prevent this totally later phases");
      }
      var IsTaken = await _userAccountRepository.RegisterAsync(registerAppUserDto);
      return new OkObjectResult(IsTaken);
    }


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
        return Unauthorized("Invalid Keycloak Id");
      }
      return Ok(IsAuhtorised);
    }


  }
}