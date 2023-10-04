using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Interfaces.Maps.RoleMaps;
using Notes.ViewModels.Database;

namespace WebApi.Controllers.CRUDControllers;

public class RolesController : AdminDbModelsController<RoleViewModel, string>
{
    readonly IRoleMap roleMap;
    public RolesController(IRoleMap roleMap, ILogger<RolesController> logger) : base(roleMap, logger)
        => this.roleMap = roleMap;

    [HttpGet("role-by-default")]
    public async Task<RoleViewModel> CreateRole()
        => await roleMap.CreateRole();

    [HttpGet("by-name/{name}")]
    public async Task<RoleViewModel?> GetRoleByNameAsync(string name)
        => await roleMap.GetRoleByNameAsync(name);
}