using Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;
using Notes.Models.Database.AdminModels;
using Notes.ViewModels;
using Notes.ViewModels.Account;

namespace Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;

public interface IUserPasswordRepository : IUserPasswordPrimary<User>
{
}