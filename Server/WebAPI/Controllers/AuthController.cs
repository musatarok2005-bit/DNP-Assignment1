using ApiContracts;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public AuthController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    // LOGIN - POST /auth/login
    [HttpPost("login")]
    public ActionResult<UserDto> Login(LoginRequest request)
    {
        // Find user by username
        var users = userRepository.GetManyAsync();
        var user = users.FirstOrDefault(u => u.Username == request.Username);
        
        // Check username and password
        if (user == null || user.Password != request.Password)
        {
            return Unauthorized("Incorrect username or password");
        }

        // Return user information without password
        UserDto userDto = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(userDto);
    }
}