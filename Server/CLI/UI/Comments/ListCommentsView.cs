using Entities;
using RepositoryContracts;

namespace CLI.UI.Comments;

public class ListCommentsView
{
    private readonly ICommentRepository commentRepository;

    public ListCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public void Show()
    {
        IQueryable<Comment> comments = commentRepository.GetManyAsync();

        foreach (Comment comment in comments)
        {
            Console.WriteLine(
                $"[{comment.Id}] {comment.Body} - User ID: {comment.UserId} - Post ID: {comment.PostId}");
        }
    }
}