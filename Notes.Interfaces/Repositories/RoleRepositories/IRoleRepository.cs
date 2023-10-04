using Notes.Interfaces.Primaries.RolePrimaries;
using Notes.Models.Database;

namespace Notes.Interfaces.Repositories.RoleRepositories;

public interface IRoleRepository : IDbModelRepository<Role, string>, IRolePrimary<Role>
{
}