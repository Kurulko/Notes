using Notes.Interfaces.Primaries.AdminPrimaries.RolePrimaries;
using Notes.Interfaces.Services;
using Notes.Models;
using Notes.ViewModels.Database.AdminModels;

namespace Notes.Interfaces.Maps.AdminMaps.RoleMaps;

public interface IRoleMap : IAdminModelMap<RoleViewModel>, IRolePrimary<RoleViewModel>
{
}