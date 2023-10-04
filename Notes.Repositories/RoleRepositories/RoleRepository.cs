using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.Interfaces.Repositories.RoleRepositories;
using Notes.Models.Context;
using Notes.Models.Database;

namespace Notes.Repositories.RoleRepositories;

public class RoleRepository : IRoleRepository
{
    readonly RoleManager<Role> roleManager;
    readonly DbSet<Role> dbRoles;
    public RoleRepository(RoleManager<Role> roleManager, NotesContext db)
        => (this.roleManager, dbRoles) = (roleManager, db.Roles);

    public async Task<Role> AddModelAsync(Role model)
    {
        await roleManager.CreateAsync(model);
        return model;
    }

    public Task<Role> CreateRole()
        => Task.FromResult(new Role());

    public async Task DeleteModelAsync(string key)
    {
        Role? role = await GetModelByIdAsync(key);
        if (role is not null)
            await roleManager.DeleteAsync(role);
    }

    public async Task<IEnumerable<Role>> GetAllModelsAsync()
        => await dbRoles.ToListAsync();

    public async Task<Role?> GetModelByIdAsync(string key)
        => await dbRoles.SingleOrDefaultAsync(u => u.Id == key);

    public async Task<Role?> GetRoleByNameAsync(string name)
        => await roleManager.FindByNameAsync(name);

    public async Task UpdateModelAsync(Role model)
        => await roleManager.UpdateAsync(model);
}