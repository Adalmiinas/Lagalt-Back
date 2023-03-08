using AutoMapper;
using lagaltApp;
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

    public async Task<UserDto> LoginAsync(LoginDto loginDto)
    {
      var IsUser = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

      if (IsUser == null)
      {
        throw new Exception();
      }
      else
      {
        var existingUser = _mapper.Map<UserDto>(IsUser);

        return existingUser;
      }
    }

    public async Task<RegisterAppUserDto> RegisterAsync(RegisterAppUserDto registerAppUserDto)
    {
      var IsUser = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == registerAppUserDto.Username);
      if (IsUser != null)
      {
        throw new Exception();
      }
      else
      {
        var newUser = _mapper.Map<UserModel>(registerAppUserDto);
        _dataContext.Users.Add(newUser);
        await _dataContext.SaveChangesAsync();
        return new RegisterAppUserDto
        {
          Username = newUser.Username,
          Password = "Created user sucess"
        };
      }
    }
  }
}