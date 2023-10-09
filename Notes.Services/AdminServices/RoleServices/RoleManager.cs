using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.Interfaces.Repositories.AdminRepositories.RoleRepositories;
using Notes.Interfaces.Services.AdminServices.RoleServices;
using Notes.Models.Database.AdminModels;

namespace Notes.Services.AdminServices.RoleServices;

public class RoleManager : AdminModelManager<Role>, IRoleService
{
    readonly IRoleRepository roleRepository;
    public RoleManager(IRoleRepository roleRepository) : base(roleRepository)
        => this.roleRepository = roleRepository;

    public async Task<Role?> GetRoleByNameAsync(string name)
        => await roleRepository.GetRoleByNameAsync(name);
}