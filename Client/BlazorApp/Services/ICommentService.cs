using Entities;

namespace BlazorApp.Services;

public interface ICommentService
{
    // CREATE - POST /Comments
    Task<Comment> AddCommentAsync(Comment request);

    // GET SINGLE - GET /Comments/1
    Task<Comment> GetCommentAsync(int id);

    // GET MANY - GET /Comments
    Task<List<Comment>> GetCommentsAsync(
        int? userId = null,
        string? username = null,
        int? postId = null);

    // UPDATE - PUT /Comments/1
    Task UpdateCommentAsync(int id, Comment request);

    // DELETE - DELETE /Comments/1
    Task DeleteCommentAsync(int id);
}