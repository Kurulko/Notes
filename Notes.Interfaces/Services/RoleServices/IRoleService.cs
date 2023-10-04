
using Notes.Interfaces.Primaries.RolePrimaries;
using Notes.Interfaces.Repositories.RoleRepositories;
using Notes.Models.Database;

namespace Notes.Interfaces.Services.RoleServices;

public interface IRoleService : IRolePrimary<Role>, IDbModelService<Role, string>
{
}