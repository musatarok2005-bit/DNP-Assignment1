using Entities;
using RepositoryContracts;

namespace CLI.UI.Posts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public void Show()
    {
        IQueryable<Post> posts = postRepository.GetManyAsync();

        foreach (Post post in posts)
        {
            Console.WriteLine($"[{post.Id}] {post.Title}");
        }
    }
}