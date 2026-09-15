using Entities;
using RepositoryContracts;

namespace CLI.UI.Comments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task CreateAsync()
    {
        Console.Write("Body: ");
        string? body = Console.ReadLine();

        Console.Write("User ID: ");
        string? userIdInput = Console.ReadLine();

        Console.Write("Post ID: ");
        string? postIdInput = Console.ReadLine();

        if (body is null || userIdInput is null || postIdInput is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        int userId = int.Parse(userIdInput);
        int postId = int.Parse(postIdInput);

        Comment comment = new Comment
        {
            Body = body,
            UserId = userId,
            PostId = postId,
            Created = DateTime.Now
        };

        Comment created = await commentRepository.AddAsync(comment);

        Console.WriteLine($"Comment created with ID: {created.Id}");
    }
}