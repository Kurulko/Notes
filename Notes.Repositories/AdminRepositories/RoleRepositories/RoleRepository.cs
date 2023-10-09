using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.Interfaces.Repositories.AdminRepositories.RoleRepositories;
using Notes.Models.Context;
using Notes.Models.Database.AdminModels;

namespace Notes.Repositories.AdminRepositories.RoleRepositories;

public class RoleRepository : AdminModelRepository<Role>, IRoleRepository
{
    readonly RoleManager<Role> roleManager;
    public RoleRepository(RoleManager<Role> roleManager, NotesContext db) : base(db)
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

    public override async Task<IEnumerable<Role>> GetAllModelsAsync()
        => await dbAdminModels.ToListAsync();

    public override async Task<Role?> GetModelByIdAsync(string key)
        => await dbAdminModels.SingleOrDefaultAsync(u => u.Id == key);

    public async Task<Role?> GetRoleByNameAsync(string name)
        => await roleManager.FindByNameAsync(name);

    public override async Task UpdateModelAsync(Role model)
        => await roleManager.UpdateAsync(model);
}