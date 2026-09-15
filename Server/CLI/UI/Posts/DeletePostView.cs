using RepositoryContracts;

namespace CLI.UI.Posts;

public class DeletePostView
{
    private readonly IPostRepository postRepository;

    public DeletePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task DeleteAsync()
    {
        Console.Write("Post ID: ");
        string? idInput = Console.ReadLine();

        if (idInput is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        int id = int.Parse(idInput);

        await postRepository.DeleteAsync(id);

        Console.WriteLine("Post deleted");
    }
}