using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.Interfaces.Repositories.RoleRepositories;
using Notes.Interfaces.Services.RoleServices;
using Notes.Models.Database;
using Notes.Services;

namespace Notes.Services.RoleServices;

public class RoleManager : DbModelManager<Role, string>, IRoleService
{
    readonly IRoleRepository roleRepository;
    public RoleManager(IRoleRepository roleRepository) : base(roleRepository)
        => this.roleRepository = roleRepository;

    public async Task<Role> CreateRole()
        => await roleRepository.CreateRole();

    public async Task<Role?> GetRoleByNameAsync(string name)
        => await roleRepository.GetRoleByNameAsync(name);
}