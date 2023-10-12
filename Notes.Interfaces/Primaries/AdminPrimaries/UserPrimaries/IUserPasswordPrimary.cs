using Notes.Models.Base.AdminModels;
using Notes.ViewModels;
using Notes.ViewModels.Auth;

namespace Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;

public interface IUserPasswordPrimary<T> where T : IUserBase
{
    Task ChangeUserPasswordAsync(ChangePassword model);
    Task AddUserPasswordAsync(ModelWithUserId<string> model);
    Task<bool> HasUserPasswordAsync(string userId);
}