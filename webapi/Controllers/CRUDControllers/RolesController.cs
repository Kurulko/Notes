using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Interfaces.Maps.AdminMaps.RoleMaps;
using Notes.ViewModels.Database.AdminModels;

namespace WebApi.Controllers.CRUDControllers;

public class RolesController : AdminDbModelsController<RoleViewModel, string>
{
    readonly IRoleMap roleMap;
    public RolesController(IRoleMap roleMap, ILogger<RolesController> logger) : base(roleMap, logger)
        => this.roleMap = roleMap;

    [HttpGet("by-name/{name}")]
    public async Task<RoleViewModel?> GetRoleByNameAsync(string name)
        => await roleMap.GetRoleByNameAsync(name);
}