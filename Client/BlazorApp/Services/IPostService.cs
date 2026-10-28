using Entities;

namespace BlazorApp.Services;

public interface IPostService
{
    // CREATE - POST /Posts
    Task<Post> AddPostAsync(Post request);

    // GET SINGLE - GET /Posts/1
    Task<Post> GetPostAsync(int id);

    // GET MANY - GET /Posts
    Task<List<Post>> GetPostsAsync(
        string? title = null,
        int? userId = null,
        string? username = null);

    // UPDATE - PUT /Posts/1
    Task UpdatePostAsync(int id, Post request);

    // DELETE - DELETE /Posts/1
    Task DeletePostAsync(int id);
}