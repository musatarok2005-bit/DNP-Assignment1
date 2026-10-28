
using System.Net.Http.Json;
using System.Text.Json;
using Entities;

namespace BlazorApp.Services;

public class HttpPostService : IPostService
{
    private readonly HttpClient client;

    public HttpPostService(HttpClient client)
    {
        this.client = client;
    }

    // CREATE - POST /Posts
    public async Task<Post> AddPostAsync(Post request)
    {
        HttpResponseMessage httpResponse =
            await client.PostAsJsonAsync("posts", request);

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<Post>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    // GET SINGLE - GET /Posts/1
    public async Task<Post> GetPostAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await client.GetAsync($"posts/{id}");

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<Post>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    // GET MANY - GET /Posts
    public async Task<List<Post>> GetPostsAsync(
        string? title = null,
        int? userId = null,
        string? username = null)
    {
        string url = "posts";
        List<string> filters = new List<string>();

        if (!string.IsNullOrEmpty(title))
        {
            filters.Add($"title={Uri.EscapeDataString(title)}");
        }

        if (userId.HasValue)
        {
            filters.Add($"userId={userId.Value}");
        }

        if (!string.IsNullOrEmpty(username))
        {
            filters.Add($"username={Uri.EscapeDataString(username)}");
        }

        if (filters.Count > 0)
        {
            url += "?" + string.Join("&", filters);
        }

        HttpResponseMessage httpResponse = await client.GetAsync(url);

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<List<Post>>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    // UPDATE - PUT /Posts/1
    public async Task UpdatePostAsync(int id, Post request)
    {
        HttpResponseMessage httpResponse =
            await client.PutAsJsonAsync($"posts/{id}", request);

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
    }

    // DELETE - DELETE /Posts/1
    public async Task DeletePostAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await client.DeleteAsync($"posts/{id}");

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
    }
}
