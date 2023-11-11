using Microsoft.AspNetCore.Identity;
using Notes.Models.Context;
using Microsoft.AspNetCore.Http;
using Notes.ViewModels;
using Notes.ViewModels.Auth;
using Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;
using Notes.Models.Database.AdminModels;

namespace Notes.Repositories.AdminRepositories.UserRepositories;

public class UserPasswordRepository : BaseUserRepository, IUserPasswordRepository
{
    public UserPasswordRepository(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : base(userManager, httpContextAccessor)
    { }

    public async Task AddUserPasswordAsync(ModelWithUserId<string> model)
    {
        string newPassword = model.Model;
        User user = await GetUserByIdAsync(model.UserId);
        IdentityResult res = await userManager.AddPasswordAsync(user, newPassword);
        if (!res.Succeeded)
            throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
    }

    public async Task ChangeUserPasswordAsync(ChangePassword model)
    {
        ChangePassword password = model;
        User user = await GetUsedUserAsync();
        IdentityResult res = await userManager.ChangePasswordAsync(user, password.OldPassword!, password.NewPassword);
        if (!res.Succeeded)
            throw new Exception(string.Join("; ", res.Errors.Select(e => e.Description)));
    }

    public async Task<bool> HasUserPasswordAsync(string userId)
    {
        User user = await GetUserByIdAsync(userId);
        return await userManager.HasPasswordAsync(user);
    }
}