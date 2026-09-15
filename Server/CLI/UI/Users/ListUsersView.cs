using Entities;
using RepositoryContracts;

namespace CLI.UI.Users;

public class ListUsersView
{
    private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void Show()
    {
        IQueryable<User> users = userRepository.GetManyAsync();

        foreach (User user in users)
        {
            Console.WriteLine($"[{user.Id}] {user.Username} - {user.Email}");
        }
    }
}