
using System.Net.Http.Json;
using System.Text.Json;
using ApiContracts;

namespace BlazorApp.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient client;

    public HttpUserService(HttpClient client)
    {
        this.client = client;
    }

    // CREATE - POST /Users
    public async Task<UserDto> AddUserAsync(CreateUserDto request)
    {
        HttpResponseMessage httpResponse =
            await client.PostAsJsonAsync("users", request);

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<UserDto>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    // GET SINGLE - GET /Users/1
    public async Task<UserDto> GetUserAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await client.GetAsync($"users/{id}");

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<UserDto>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    // GET MANY - GET /Users
    public async Task<List<UserDto>> GetUsersAsync(string? username = null)
    {
        string url = "users";

        if (!string.IsNullOrEmpty(username))
        {
            url += $"?username={Uri.EscapeDataString(username)}";
        }

        HttpResponseMessage httpResponse = await client.GetAsync(url);

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<List<UserDto>>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    // UPDATE - PUT /Users/1
    public async Task UpdateUserAsync(int id, CreateUserDto request)
    {
        HttpResponseMessage httpResponse =
            await client.PutAsJsonAsync($"users/{id}", request);

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
    }

    // DELETE - DELETE /Users/1
    public async Task DeleteUserAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await client.DeleteAsync($"users/{id}");

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
    }
}
