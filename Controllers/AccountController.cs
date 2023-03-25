using lagalt.Controllers;
using Lagalt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace lagalt
{

  /// <summary>
  /// Control login and register
  /// </summary>

  public class AccountController : BaseApiController
  {
    private readonly DataContext _dataContext;
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly MemoryCache _memoryCache;


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

      var isUser =await _dataContext.Users.AnyAsync(u => u.KeyCloakId == registerAppUserDto.KeycloakId);
      if (isUser)
      {
        return new BadRequestObjectResult("User already exists no need to register, prevent this totally later phases");
      }

      // var cacheKey = "registerDto:" + registerAppUserDto.KeycloakId;
      // var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(2));

      // // Check if the request is a duplicate
      // if (_memoryCache.TryGetValue(cacheKey, out _))
      // {
      //   return BadRequest("Duplicate request detected");
      // }

      // // Store the request in the cache
      // _memoryCache.Set(cacheKey, DateTime.UtcNow, cacheEntryOptions);
      var IsTaken = await _userAccountRepository.RegisterAsync(registerAppUserDto);
      return new OkObjectResult(IsTaken);
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
        return Unauthorized("Invalid Keycloak Id");
      }
      return Ok(IsAuhtorised);
    }

    [HttpPost("loginDev")]
    public async Task<ActionResult<UserDto>> LoginDev([FromBody] LoginDevDto loginDev)
    {

      var IsAuhtorised = await _userAccountRepository.LoginDevAsync(loginDev);
      if (IsAuhtorised == null)
      {
        return Unauthorized("Invalid Password / Username");
      }
      return IsAuhtorised;
    }
  }
}