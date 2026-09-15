using Entities;
using RepositoryContracts;

namespace CLI.UI.Posts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task CreateAsync()
    {
        Console.Write("Title: ");
        string? title = Console.ReadLine();

        Console.Write("Body: ");
        string? body = Console.ReadLine();

        Console.Write("User ID: ");
        string? userIdInput = Console.ReadLine();

        if (title is null || body is null || userIdInput is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        int userId = int.Parse(userIdInput);

        Post post = new Post
        {
            Title = title,
            Body = body,
            UserId = userId,
            Created = DateTime.Now
        };

        Post created = await postRepository.AddAsync(post);

        Console.WriteLine($"Post created with ID: {created.Id}");
    }
}