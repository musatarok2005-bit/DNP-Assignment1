using ApiContracts;

namespace BlazorApp.Services;

public interface IUserService
{
    // CREATE - POST /Users
    Task<UserDto> AddUserAsync(CreateUserDto request);

    // GET SINGLE - GET /Users/1
    Task<UserDto> GetUserAsync(int id);

    // GET MANY - GET /Users
    Task<List<UserDto>> GetUsersAsync(string? username = null);

    // UPDATE - PUT /Users/1
    Task UpdateUserAsync(int id, CreateUserDto request);

    // DELETE - DELETE /Users/1
    Task DeleteUserAsync(int id);
}