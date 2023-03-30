using AutoMapper;
using lagalt;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Lagalt
{
  public class TestAuthentication
  {
    private readonly DataContext _context;
    private readonly IUserAccountRepository _userRepository;
    private readonly AccountController _controller;
    private readonly IMapper _mapper;

    public TestAuthentication()
    {
      var options = new DbContextOptionsBuilder<DataContext>()
          .UseInMemoryDatabase(databaseName: "Db")
          .Options;
      _context = new DataContext(options);

      _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));
      _userRepository = new RegisterUserRepository(_context, _mapper);
      _controller = new AccountController(_context, _userRepository);
    }

    [Fact]
    public async Task Register_WithValidRegisterDto_ReturnsOkResult()
    {
      // Arrange
      var registerDto = new RegisterAppUserDto
      {
        KeycloakId = "abc123",
      };

      // Act
      var result = await _controller.Register(registerDto);

      // Assert
      Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task Register_WithDuplicateRegisterDto_ReturnsBadRequestResult()
    {
      // Arrange
      var registerDto = new RegisterAppUserDto
      {
        KeycloakId = "abc123",
      };

      // Act
      var result1 = await _controller.Register(registerDto);
      var result2 = await _controller.Register(registerDto);

      // Assert
      Assert.IsType<BadRequestObjectResult>(result2.Result);
    }
  }
}