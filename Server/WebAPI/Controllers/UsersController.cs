
using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    // CREATE - POST /Users
    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser(
        [FromBody] CreateUserDto request)
    {
        User user = new User
        {
            Username = request.Username,
            Password = request.Password,
            Email = request.Email
        };

        User created = await userRepo.AddAsync(user);

        UserDto dto = new UserDto
        {
            Id = created.Id,
            Username = created.Username,
            Email = created.Email
        };

        return Created($"/users/{dto.Id}", dto);
    }

    // GET SINGLE - GET /Users/1
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        User user = await userRepo.GetSingleAsync(id);

        UserDto dto = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(dto);
    }

    // GET MANY - GET /Users
    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> GetUsers(
        [FromQuery] string? username)
    {
        IQueryable<User> users = userRepo.GetManyAsync();

        if (!string.IsNullOrEmpty(username))
        {
            users = users.Where(u =>
                u.Username.Contains(username));
        }

        List<UserDto> dtos = users.Select(u => new UserDto
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email
        }).ToList();

        return Ok(dtos);
    }

    // UPDATE - PUT /Users/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(
        int id, [FromBody] CreateUserDto request)
    {
        User user = await userRepo.GetSingleAsync(id);

        user.Username = request.Username;
        user.Password = request.Password;
        user.Email = request.Email;

        await userRepo.UpdateAsync(user);

        return NoContent();
    }

    // DELETE - DELETE /Users/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userRepo.DeleteAsync(id);

        return NoContent();
    }
}
