using Entities;
using RepositoryContracts;

namespace CLI.UI.Posts;

public class ViewPostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public ViewPostView(
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.Write("Post ID: ");
        string? input = Console.ReadLine();

        if (input is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        int postId = int.Parse(input);

        Post post = await postRepository.GetSingleAsync(postId);

        Console.WriteLine();
        Console.WriteLine($"Title: {post.Title}");
        Console.WriteLine($"Body: {post.Body}");
        Console.WriteLine("Comments:");

        IQueryable<Comment> comments = commentRepository.GetManyAsync();

        foreach (Comment comment in comments)
        {
            if (comment.PostId == postId)
            {
                Console.WriteLine($"- {comment.Body}");
            }
        }
    }
}