using Notes.Models.Base.AdminModels;
using Notes.Models.Database;

namespace Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;

public interface IUsedUserPrimary<T> where T : IUserBase
{
    Task<bool> IsImpersonating();

    Task<T> GetUsedUserAsync();
    Task<string> GetUsedUserIdAsync();
    Task<string> GetUsedUserNameAsync();

    Task ChangeUsedUserIdAsync(string usedUserId);
    Task DropUsedUserIdAsync();
}