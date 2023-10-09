using Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;
using Notes.Models.Database.AdminModels;
using Notes.ViewModels;

namespace Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;

public interface IUserRolesRepository : IUserRolesPrimary<User>
{
}