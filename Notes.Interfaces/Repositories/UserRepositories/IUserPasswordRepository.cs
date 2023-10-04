using Notes.Interfaces.Primaries.UserPrimaries;
using Notes.Models.Database;
using Notes.ViewModels;
using Notes.ViewModels.Account;

namespace Notes.Interfaces.Repositories.UserRepositories;

public interface IUserPasswordRepository : IUserPasswordPrimary<User>
{
}