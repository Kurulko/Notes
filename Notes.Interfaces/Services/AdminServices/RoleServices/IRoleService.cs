using Notes.Interfaces.Primaries.AdminPrimaries.RolePrimaries;
using Notes.Models.Database.AdminModels;

namespace Notes.Interfaces.Services.AdminServices.RoleServices;

public interface IRoleService : IRolePrimary<Role>, IAdminModelService<Role>
{
}