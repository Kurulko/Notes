using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Notes.Interfaces.Repositories.UserRepositories;
using Notes.Models.Context;
using Notes.Models.Database;

namespace Notes.Repositories.UserRepositories;

public class UsedUserRepository : BaseUserRepository, IUsedUserRepository
{
    public UsedUserRepository(UserManager<User> userManager, NotesContext db, IHttpContextAccessor httpContextAccessor) : base(userManager, db, httpContextAccessor)
    { }

    public async Task<string?> GetCurrentUserNameAsync()
        => (await GetUsedUserAsync())?.UserName;

    public async Task ChangeUsedUserIdAsync(string usedUserId)
    {
        User? user = await GetCurrentUserAsync();
        if (user is not null && !string.IsNullOrEmpty(usedUserId))
        {
            user.UsedUserId = usedUserId;
            await UpdateModelAsync(user);
        }
    }

    public async Task DropUsedUserIdAsync()
    {
        User? user = await GetCurrentUserAsync();
        if (user is not null)
        {
            user.UsedUserId = null;
            await UpdateModelAsync(user);
        }
    }

    public new async Task<User> GetUsedUserAsync()
        => await base.GetUsedUserAsync();

    public new async Task<string> GetUsedUserIdAsync()
        => await base.GetUsedUserIdAsync();

    public async Task<bool> IsImpersonating()
        => !string.IsNullOrEmpty((await GetCurrentUserAsync())?.UsedUserId);
}