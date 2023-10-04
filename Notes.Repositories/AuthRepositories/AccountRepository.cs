using Microsoft.AspNetCore.Identity;
using Notes.Interfaces.Repositories.UserRepositories;
using Notes.ViewModels.Account;
using Notes.Commons;
using Notes.Models.Database;
using Notes.Interfaces.Repositories.AuthRepositories;

namespace Notes.Repositories.AuthRepositories;

public class AccountRepository : IAccountRepository
{
    readonly SignInManager<User> signInManager;
    readonly IUserRepository userRepository;

    public AccountRepository(SignInManager<User> signInManager, IUserRepository userRepository)
        => (this.signInManager, this.userRepository) = (signInManager, userRepository);

    public async Task<IEnumerable<string>> LoginUserAsync(LoginModel login)
    {
        var res = await signInManager.PasswordSignInAsync(login.Name, login.Password, login.RememberMe, false);

        if (!res.Succeeded)
            throw new Exception("Password or/and login invalid");

        User user = (await userRepository.GetUserByNameAsync(login.Name))!;
        return await userRepository.GetRolesAsync(user.Id);
    }

    public async Task<IEnumerable<string>> RegisterUserAsync(RegisterModel register)
    {
        User user = (User)register;
        IdentityResult result = await userRepository.CreateUserAsync(user, register.Password);
        if (result.Succeeded)
        {
            user.Registered = DateTime.Now;
            await signInManager.SignInAsync(user, register.RememberMe);
            string userRole = Roles.User;
            await userRepository.AddRoleToUserAsync(new(user.Id, userRole));
            return new string[] { userRole };
        }
        else
            throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
    }

    public async Task LogoutUserAsync()
        => await signInManager.SignOutAsync();
}