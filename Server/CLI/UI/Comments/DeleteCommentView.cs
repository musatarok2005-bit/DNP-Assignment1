using RepositoryContracts;

namespace CLI.UI.Comments;

public class DeleteCommentView
{
    private readonly ICommentRepository commentRepository;

    public DeleteCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task DeleteAsync()
    {
        Console.Write("Comment ID: ");
        string? idInput = Console.ReadLine();

        if (idInput is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        int id = int.Parse(idInput);

        await commentRepository.DeleteAsync(id);

        Console.WriteLine("Comment deleted");
    }
}