using Entities;
using RepositoryContracts;

namespace CLI.UI.Posts;

public class UpdatePostView
{
    private readonly IPostRepository postRepository;

    public UpdatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task UpdateAsync()
    {
        Console.Write("Post ID: ");
        string? idInput = Console.ReadLine();

        if (idInput is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        int id = int.Parse(idInput);

        Post post = await postRepository.GetSingleAsync(id);

        Console.Write("New title: ");
        string? title = Console.ReadLine();

        Console.Write("New body: ");
        string? body = Console.ReadLine();

        if (title is null || body is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        post.Title = title;
        post.Body = body;

        await postRepository.UpdateAsync(post);

        Console.WriteLine("Post updated");
    }
}