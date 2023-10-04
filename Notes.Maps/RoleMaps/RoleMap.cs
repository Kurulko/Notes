using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Notes.Interfaces.Maps.RoleMaps;
using Notes.Interfaces.Maps.UserMaps;
using Notes.Interfaces.Primaries.RolePrimaries;
using Notes.Interfaces.Repositories.RoleRepositories;
using Notes.Interfaces.Services;
using Notes.Interfaces.Services.RoleServices;
using Notes.Interfaces.Services.UserServices;
using Notes.Models.Database;
using Notes.ViewModels.Database;

namespace Notes.Maps.RoleMaps;

public class RoleMap : DbModelMap<RoleViewModel, Role, string>, IRoleMap
{
    readonly IRoleService roleService;
    public RoleMap(IRoleService roleService) : base(roleService)
        => this.roleService = roleService;

    public async Task<RoleViewModel> CreateRole()
        => ConvertToViewModel(await roleService.CreateRole());

    public async Task<RoleViewModel?> GetRoleByNameAsync(string name)
        => ConvertToNullableViewModel(await roleService.GetRoleByNameAsync(name));

    protected override Role ConvertFromViewModel(RoleViewModel viewModel)
        => (Role)viewModel;
    protected override RoleViewModel ConvertToViewModel(Role model)
        => (RoleViewModel)model;
}