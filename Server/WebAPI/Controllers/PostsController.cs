
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository postRepo;
    private readonly IUserRepository userRepo;

    public PostsController(
        IPostRepository postRepo,
        IUserRepository userRepo)
    {
        this.postRepo = postRepo;
        this.userRepo = userRepo;
    }

    // CREATE - POST /Posts
    [HttpPost]
    public async Task<ActionResult<Post>> AddPost(
        [FromBody] Post request)
    {
        Post created = await postRepo.AddAsync(request);

        return Created($"/posts/{created.Id}", created);
    }

    // GET SINGLE - GET /Posts/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        Post post = await postRepo.GetSingleAsync(id);

        return Ok(post);
    }

    // GET MANY - GET /Posts
    [HttpGet]
    public ActionResult<IEnumerable<Post>> GetPosts(
        [FromQuery] string? title,
        [FromQuery] int? userId,
        [FromQuery] string? username)
    {
        IQueryable<Post> posts = postRepo.GetManyAsync();

        if (!string.IsNullOrEmpty(title))
        {
            posts = posts.Where(p =>
                p.Title.Contains(title));
        }

        if (userId.HasValue)
        {
            posts = posts.Where(p =>
                p.UserId == userId.Value);
        }

        if (!string.IsNullOrEmpty(username))
        {
            List<int> userIds = userRepo.GetManyAsync()
                .Where(u => u.Username.Contains(username))
                .Select(u => u.Id)
                .ToList();

            posts = posts.Where(p =>
                userIds.Contains(p.UserId));
        }

        return Ok(posts.ToList());
    }

    // UPDATE - PUT /Posts/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(
        int id, [FromBody] Post request)
    {
        Post post = await postRepo.GetSingleAsync(id);

        post.Title = request.Title;
        post.Body = request.Body;
        post.UserId = request.UserId;
        post.Likes = request.Likes;
        post.Dislikes = request.Dislikes;
        post.Created = request.Created;

        await postRepo.UpdateAsync(post);

        return NoContent();
    }

    // DELETE - DELETE /Posts/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        await postRepo.DeleteAsync(id);

        return NoContent();
    }
}
