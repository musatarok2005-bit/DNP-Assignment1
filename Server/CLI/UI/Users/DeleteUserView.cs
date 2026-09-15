using RepositoryContracts;

namespace CLI.UI.Users;

public class DeleteUserView
{
    private readonly IUserRepository userRepository;

    public DeleteUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task DeleteAsync()
    {
        Console.Write("User ID: ");
        string? idInput = Console.ReadLine();

        if (idInput is null)
        {
            Console.WriteLine("Invalid input");
            return;
        }

        int id = int.Parse(idInput);

        await userRepository.DeleteAsync(id);

        Console.WriteLine("User deleted");
    }
}