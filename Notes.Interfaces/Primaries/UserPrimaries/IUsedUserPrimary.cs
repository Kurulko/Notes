using Notes.Models.Base;
using Notes.Models.Database;

namespace Notes.Interfaces.Primaries.UserPrimaries;

public interface IUsedUserPrimary<T> where T : IUserBase
{
    Task<bool> IsImpersonating();

    Task<string?> GetCurrentUserNameAsync();

    Task<T> GetUsedUserAsync();
    Task<string> GetUsedUserIdAsync();

    Task ChangeUsedUserIdAsync(string usedUserId);
    Task DropUsedUserIdAsync();
}