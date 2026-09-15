using Entities;
using RepositoryContracts;

namespace CLI.UI.Users;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task CreateAsync()
    {
        Console.Write("Username: ");
        string? username = Console.ReadLine();

        Console.Write("Password: ");
        string? password = Console.ReadLine();

        Console.Write("Email: ");
        string? email = Console.ReadLine();

        if (username is null || password is null || email is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        User user = new User
        {
            Username = username,
            Password = password,
            Email = email
        };

        User created = await userRepository.AddAsync(user);

        Console.WriteLine($"User created with ID: {created.Id}");
    }
}