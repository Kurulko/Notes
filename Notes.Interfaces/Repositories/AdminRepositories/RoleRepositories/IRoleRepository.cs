using Notes.Interfaces.Primaries.AdminPrimaries.RolePrimaries;
using Notes.Models.Database.AdminModels;

namespace Notes.Interfaces.Repositories.AdminRepositories.RoleRepositories;

public interface IRoleRepository : IAdminModelRepository<Role>, IRolePrimary<Role>
{
}