namespace lagalt
{

  /// <summary>
  /// Handle user registeration
  /// </summary>
  public interface IUserAccountRepository
  {
    Task<RegisterAppUserDto> RegisterAsync(RegisterAppUserDto registerAppUserDto);

    Task<UserDto> LoginAsync(LoginDto loginDto);
  }
}