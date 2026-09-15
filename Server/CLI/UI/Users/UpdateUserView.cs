using Entities;
using RepositoryContracts;

namespace CLI.UI.Users;

public class UpdateUserView
{
    private readonly IUserRepository userRepository;

    public UpdateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task UpdateAsync()
    {
        Console.Write("User ID: ");
        string? idInput = Console.ReadLine();

        if (idInput is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        int id = int.Parse(idInput);

        User user = await userRepository.GetSingleAsync(id);

        Console.Write("New username: ");
        string? username = Console.ReadLine();

        Console.Write("New password: ");
        string? password = Console.ReadLine();

        Console.Write("New email: ");
        string? email = Console.ReadLine();

        if (username is null || password is null || email is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        user.Username = username;
        user.Password = password;
        user.Email = email;

        await userRepository.UpdateAsync(user);

        Console.WriteLine("User updated");
    }
}