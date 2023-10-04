using Microsoft.AspNetCore.Identity;
using Notes.Interfaces.Repositories.UserRepositories;
using Notes.Models.Context;
using Microsoft.AspNetCore.Http;
using Notes.ViewModels;
using Notes.ViewModels.Account;
using Notes.Models.Database;

namespace Notes.Repositories.UserRepositories;

public class UserPasswordRepository : BaseUserRepository, IUserPasswordRepository
{
    public UserPasswordRepository(UserManager<User> userManager, NotesContext db, IHttpContextAccessor httpContextAccessor) : base(userManager, db, httpContextAccessor)
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