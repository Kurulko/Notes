using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Notes.Interfaces.Repositories.AdminRepositories.RoleRepositories;
using Notes.Models.Context;
using Notes.Models.Database.AdminModels;

namespace Notes.Repositories.AdminRepositories.RoleRepositories;

public class RoleRepository : AdminModelRepository<Role>, IRoleRepository
{
    readonly RoleManager<Role> roleManager;
    public RoleRepository(RoleManager<Role> roleManager) : base(roleManager.Roles)
        => this.roleManager = roleManager;

    public override async Task<Role> AddModelAsync(Role model)
    {
        await roleManager.CreateAsync(model);
        return model;
    }

    public override async Task DeleteModelAsync(string key)
    {
        Role? role = await GetModelByIdAsync(key);
        if (role is not null)
            await roleManager.DeleteAsync(role);
    }

    public async Task<Role?> GetRoleByNameAsync(string name)
        => (await GetAllModelsAsync()).SingleOrDefault(m => m.Name.ToLower() == name.ToLower());

    public override async Task UpdateModelAsync(Role model)
        => await roleManager.UpdateAsync(model);
}