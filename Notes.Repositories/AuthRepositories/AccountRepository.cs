using Microsoft.AspNetCore.Identity;
using Notes.ViewModels.Auth;
using Notes.Commons;
using Notes.Interfaces.Repositories.AuthRepositories;
using Notes.Models.Database.AdminModels;
using Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;
using Microsoft.AspNetCore.Http;
using Notes.ViewModels.Database.AdminModels;
using Notes.Interfaces.Maps.AuthMaps;
using System.Data;
using Microsoft.Win32;

namespace Notes.Repositories.AuthRepositories;

public class AccountRepository : IAccountRepository
{
    readonly SignInManager<User> signInManager;
    readonly IUserRepository userRepository;
    readonly IHttpContextAccessor httpContextAccessor;
    readonly IJwtMap jwtMap;

    public AccountRepository(SignInManager<User> signInManager, IUserRepository userRepository, IJwtMap jwtMap, IHttpContextAccessor httpContextAccessor)
        => (this.signInManager, this.userRepository, this.httpContextAccessor, this.jwtMap) = (signInManager, userRepository, httpContextAccessor, jwtMap);

    public async Task<TokenModel> LoginUserAsync(LoginModel login)
    {
        var res = await signInManager.PasswordSignInAsync(login.Name, login.Password, login.RememberMe, false);

        if (!res.Succeeded)
            throw new Exception("Password or/and login invalid");

        User user = (await userRepository.GetUserByNameAsync(login.Name))!;
        string[] roles = (await userRepository.GetRolesAsync(user.Id)).ToArray();
        return jwtMap.GenerateJwtToken((UserViewModel)login, roles);
    }

    public async Task<TokenModel> RegisterUserAsync(RegisterModel register)
    {
        User user = (User)register;
        IdentityResult result = await userRepository.CreateUserAsync(user, register.Password);
        if (result.Succeeded)
        {
            user.Registered = DateTime.Now;
            await signInManager.SignInAsync(user, register.RememberMe);
            string userRole = Roles.User;
            await userRepository.AddRoleToUserAsync(new(user.Id, userRole));

            return jwtMap.GenerateJwtToken((UserViewModel)register, userRole);
        }
        else
            throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
    }

    public async Task LogoutUserAsync()
        => await signInManager.SignOutAsync();

    public async Task<TokenModel> GetTokenAsync()
    {
        var claims = httpContextAccessor.HttpContext!.User;
        User user =  (await userRepository.GetUserByClaimsAsync(claims))!;
        string[] roles = (await userRepository.GetRolesAsync(user.Id)).ToArray();
        return jwtMap.GenerateJwtToken((UserViewModel)user, roles);
    }
}
