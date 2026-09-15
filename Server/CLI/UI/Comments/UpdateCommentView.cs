using Entities;
using RepositoryContracts;

namespace CLI.UI.Comments;

public class UpdateCommentView
{
    private readonly ICommentRepository commentRepository;

    public UpdateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task UpdateAsync()
    {
        Console.Write("Comment ID: ");
        string? idInput = Console.ReadLine();

        if (idInput is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        int id = int.Parse(idInput);

        Comment comment = await commentRepository.GetSingleAsync(id);

        Console.Write("New body: ");
        string? body = Console.ReadLine();

        if (body is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        comment.Body = body;

        await commentRepository.UpdateAsync(comment);

        Console.WriteLine("Comment updated");
    }
}