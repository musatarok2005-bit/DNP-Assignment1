
using System.Net.Http.Json;
using System.Text.Json;
using Entities;

namespace BlazorApp.Services;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient client;

    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }

    // CREATE - POST /Comments
    public async Task<Comment> AddCommentAsync(Comment request)
    {
        HttpResponseMessage httpResponse =
            await client.PostAsJsonAsync("comments", request);

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<Comment>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    // GET SINGLE - GET /Comments/1
    public async Task<Comment> GetCommentAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await client.GetAsync($"comments/{id}");

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<Comment>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    // GET MANY - GET /Comments
    public async Task<List<Comment>> GetCommentsAsync(
        int? userId = null,
        string? username = null,
        int? postId = null)
    {
        string url = "comments";
        List<string> filters = new List<string>();

        if (userId.HasValue)
        {
            filters.Add($"userId={userId.Value}");
        }

        if (!string.IsNullOrEmpty(username))
        {
            filters.Add($"username={Uri.EscapeDataString(username)}");
        }

        if (postId.HasValue)
        {
            filters.Add($"postId={postId.Value}");
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

        return JsonSerializer.Deserialize<List<Comment>>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    // UPDATE - PUT /Comments/1
    public async Task UpdateCommentAsync(int id, Comment request)
    {
        HttpResponseMessage httpResponse =
            await client.PutAsJsonAsync($"comments/{id}", request);

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
    }

    // DELETE - DELETE /Comments/1
    public async Task DeleteCommentAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await client.DeleteAsync($"comments/{id}");

        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
    }
}
