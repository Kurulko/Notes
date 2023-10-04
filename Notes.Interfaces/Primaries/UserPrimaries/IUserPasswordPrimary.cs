using Notes.Models.Base;
using Notes.ViewModels;
using Notes.ViewModels.Account;

namespace Notes.Interfaces.Primaries.UserPrimaries;

public interface IUserPasswordPrimary<T> where T : IUserBase
{
    Task ChangeUserPasswordAsync(ChangePassword model);
    Task AddUserPasswordAsync(ModelWithUserId<string> model);
    Task<bool> HasUserPasswordAsync(string userId);
}