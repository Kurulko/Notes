
using Notes.Interfaces.Primaries.RolePrimaries;
using Notes.Interfaces.Repositories.RoleRepositories;
using Notes.Interfaces.Services;
using Notes.Models;
using Notes.ViewModels.Database;

namespace Notes.Interfaces.Maps.RoleMaps;

public interface IRoleMap : IDbModelMap<RoleViewModel, string>, IRolePrimary<RoleViewModel>
{
}