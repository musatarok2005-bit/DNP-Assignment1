using CLI.UI.Comments;
using CLI.UI.Posts;
using CLI.UI.Users;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    private readonly CreateUserView createUserView;
    private readonly CreatePostView createPostView;
    private readonly CreateCommentView createCommentView;
    private readonly ListPostsView listPostsView;
    private readonly ViewPostView viewPostView;
    private readonly ListUsersView listUsersView;
    private readonly UpdateUserView updateUserView;
    private readonly DeleteUserView deleteUserView;
    private readonly UpdatePostView updatePostView;
    private readonly DeletePostView deletePostView;

    private readonly ListCommentsView listCommentsView;
    private readonly UpdateCommentView updateCommentView;
    private readonly DeleteCommentView deleteCommentView;

    public CliApp(
        IUserRepository userRepository,
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;

        createUserView = new CreateUserView(userRepository);
        createPostView = new CreatePostView(postRepository);
        createCommentView = new CreateCommentView(commentRepository);

        listPostsView = new ListPostsView(postRepository);
        viewPostView = new ViewPostView(postRepository, commentRepository);
        listUsersView = new ListUsersView(userRepository);

        updateUserView = new UpdateUserView(userRepository);
        deleteUserView = new DeleteUserView(userRepository);

        updatePostView = new UpdatePostView(postRepository);
        deletePostView = new DeletePostView(postRepository);

        listCommentsView = new ListCommentsView(commentRepository);
        updateCommentView = new UpdateCommentView(commentRepository);
        deleteCommentView = new DeleteCommentView(commentRepository);
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to the forum!");
            Console.WriteLine("1. Create user");
            Console.WriteLine("2. Create post");
            Console.WriteLine("3. Create comment");
            Console.WriteLine("4. View posts");
            Console.WriteLine("5. View specific post");
            Console.WriteLine("6. View all users");
            Console.WriteLine("7. Update user");
            Console.WriteLine("8. Delete user");
            Console.WriteLine("9. Update post");
            Console.WriteLine("10. Delete post");
            Console.WriteLine("11. View all comments");
            Console.WriteLine("12. Update comment");
            Console.WriteLine("13. Delete comment");
            Console.WriteLine("0. Exit");

            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                await createUserView.CreateAsync();
            }

            if (choice == "2")
            {
                await createPostView.CreateAsync();
            }

            if (choice == "3")
            {
                await createCommentView.CreateAsync();
            }

            if (choice == "4")
            {
                listPostsView.Show();
            }

            if (choice == "5")
            {
                await viewPostView.ShowAsync();
            }

            if (choice == "6")
            {
                listUsersView.Show();
            }

            if (choice == "7")
            {
                await updateUserView.UpdateAsync();
            }

            if (choice == "8")
            {
                await deleteUserView.DeleteAsync();
            }

            if (choice == "9")
            {
                await updatePostView.UpdateAsync();
            }

            if (choice == "10")
            {
                await deletePostView.DeleteAsync();
            }

            if (choice == "11")
            {
                listCommentsView.Show();
            }

            if (choice == "12")
            {
                await updateCommentView.UpdateAsync();
            }

            if (choice == "13")
            {
                await deleteCommentView.DeleteAsync();
            }

            if (choice == "0")
            {
                break;
            }
        }
    }
}