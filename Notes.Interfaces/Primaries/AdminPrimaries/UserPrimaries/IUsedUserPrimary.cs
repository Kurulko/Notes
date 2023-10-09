using Notes.Models.Base.AdminModels;
using Notes.Models.Database;

namespace Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;

public interface IUsedUserPrimary<T> where T : IUserBase
{
    Task<bool> IsImpersonating();

    Task<string?> GetCurrentUserNameAsync();

    Task<T> GetUsedUserAsync();
    Task<string> GetUsedUserIdAsync();

    Task ChangeUsedUserIdAsync(string usedUserId);
    Task DropUsedUserIdAsync();
}