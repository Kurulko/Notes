﻿using Microsoft.AspNetCore.Identity;
using Notes.Commons;
using Notes.Interfaces.Repositories.AdminRepositories.RoleRepositories;
using Notes.Models.Database.AdminModels;

namespace WebApi.Initializers;

public class RolesInitializer
{
    public static async Task InitializeAsync(IRoleRepository roleRepository)
    {
        string[] rolesStr = { Roles.Admin, Roles.User };
        foreach (string roleStr in rolesStr)
        {
            Role? role = await roleRepository.GetRoleByNameAsync(roleStr);
            if (role is null)
                await roleRepository.AddModelAsync(new Role() { Name = roleStr });
        }
    }
}