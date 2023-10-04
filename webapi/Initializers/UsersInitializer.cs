using Microsoft.AspNetCore.Identity;
using Notes.Commons;
using Notes.Interfaces.Repositories.UserRepositories;
using Notes.Models.Database;

namespace WebApi.Initializers;

public class UsersInitializer
{
    public static async Task AdminInitializeAsync(IUserRepository userRepository, string name, string password)
    {
        if (await userRepository.GetUserByNameAsync(name) is null)
        {
            User admin = new() { UserName = name, Registered = DateTime.Now};
            IdentityResult result = await userRepository.CreateUserAsync(admin, password);
            if (result.Succeeded)
                await userRepository.AddRoleToUserAsync(new(admin.Id, Roles.Admin));
        }
    }
}