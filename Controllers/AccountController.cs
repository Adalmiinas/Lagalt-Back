using lagalt.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace lagalt
{
  public class AccountController : BaseApiController
  {
    private readonly DataContext _dataContext;
    private readonly IUserAccountRepository _userAccountRepository;
    public AccountController(DataContext dataContext, IUserAccountRepository userAccountRepository)
    {
      _userAccountRepository = userAccountRepository;

      _dataContext = dataContext;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterAppUserDto>> Register(RegisterAppUserDto registerAppUserDto)
    {
      try
      {
        return await _userAccountRepository.RegisterAsync(registerAppUserDto);
      }
      catch (Exception ex)
      {

        throw new Exception("Username is already taken try again", ex);
      }

    }

    //change to use keycloak
    [HttpPost("login")]
    public async Task<UserDto> Login([FromBody] LoginDto loginDto)
    {
      try
      {
        return await _userAccountRepository.LoginAsync(loginDto);
      }
      catch (Exception ex)
      {

        throw new Exception("Incorrect Login password or Username", ex);
      }

    }
  }
}