
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepo;
    private readonly IUserRepository userRepo;

    public CommentsController(
        ICommentRepository commentRepo,
        IUserRepository userRepo)
    {
        this.commentRepo = commentRepo;
        this.userRepo = userRepo;
    }

    // CREATE - POST /Comments
    [HttpPost]
    public async Task<ActionResult<Comment>> AddComment(
        [FromBody] Comment request)
    {
        Comment created = await commentRepo.AddAsync(request);

        return Created($"/comments/{created.Id}", created);
    }

    // GET SINGLE - GET /Comments/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(int id)
    {
        Comment comment = await commentRepo.GetSingleAsync(id);

        return Ok(comment);
    }

    // GET MANY - GET /Comments
    [HttpGet]
    public ActionResult<IEnumerable<Comment>> GetComments(
        [FromQuery] int? userId,
        [FromQuery] string? username,
        [FromQuery] int? postId)
    {
        IQueryable<Comment> comments = commentRepo.GetManyAsync();

        if (userId.HasValue)
        {
            comments = comments.Where(c =>
                c.UserId == userId.Value);
        }

        if (!string.IsNullOrEmpty(username))
        {
            List<int> userIds = userRepo.GetManyAsync()
                .Where(u => u.Username.Contains(username))
                .Select(u => u.Id)
                .ToList();

            comments = comments.Where(c =>
                userIds.Contains(c.UserId));
        }

        if (postId.HasValue)
        {
            comments = comments.Where(c =>
                c.PostId == postId.Value);
        }

        return Ok(comments.ToList());
    }

    // UPDATE - PUT /Comments/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(
        int id, [FromBody] Comment request)
    {
        Comment comment = await commentRepo.GetSingleAsync(id);

        comment.Body = request.Body;
        comment.UserId = request.UserId;
        comment.PostId = request.PostId;
        comment.Likes = request.Likes;
        comment.Dislikes = request.Dislikes;
        comment.Created = request.Created;

        await commentRepo.UpdateAsync(comment);

        return NoContent();
    }

    // DELETE - DELETE /Comments/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        await commentRepo.DeleteAsync(id);

        return NoContent();
    }
}
