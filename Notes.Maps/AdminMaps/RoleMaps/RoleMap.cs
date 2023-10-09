using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Notes.Interfaces.Maps.AdminMaps.RoleMaps;
using Notes.Interfaces.Services;
using Notes.Interfaces.Services.AdminServices.RoleServices;
using Notes.Models.Database.AdminModels;
using Notes.ViewModels.Database.AdminModels;

namespace Notes.Maps.AdminMaps.RoleMaps;

public class RoleMap : AdminModelMap<RoleViewModel, Role>, IRoleMap
{
    readonly IRoleService roleService;
    public RoleMap(IRoleService roleService) : base(roleService)
        => this.roleService = roleService;

    public async Task<RoleViewModel?> GetRoleByNameAsync(string name)
        => ConvertToNullableViewModel(await roleService.GetRoleByNameAsync(name));

    protected override Role ConvertFromViewModel(RoleViewModel viewModel)
        => (Role)viewModel;
    protected override RoleViewModel ConvertToViewModel(Role model)
        => (RoleViewModel)model;
}