using Notes.Interfaces.Primaries.UserPrimaries;
using Notes.Models.Database;
using Notes.ViewModels;

namespace Notes.Interfaces.Repositories.UserRepositories;

public interface IUserRolesRepository : IUserRolesPrimary<User>
{
}