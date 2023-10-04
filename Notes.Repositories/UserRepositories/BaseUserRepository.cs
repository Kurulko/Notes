﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Notes.Interfaces.Repositories.UserRepositories;
using Notes.Models.Context;
using Notes.Models.Database;
using System.Security.Claims;

namespace Notes.Repositories.UserRepositories;

public class BaseUserRepository : IBaseUserRepository
{
    protected readonly IHttpContextAccessor httpContextAccessor;
    protected readonly UserManager<User> userManager;
    protected readonly NotesContext db;
    public BaseUserRepository(UserManager<User> userManager, NotesContext db, IHttpContextAccessor httpContextAccessor)
    {
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;
        this.db = db;

        getModels = db.Users;
    }

    protected IQueryable<User> getModels;

    public Task<User> CreateUserAsync()
        => Task.FromResult(new User());

    public async Task<User> AddModelAsync(User model)
    {
        User? existingUser = await GetModelByIdAsync(model.Id);
        if (existingUser is null)
        {
            await userManager.CreateAsync(model);
            return model;
        }
        return existingUser;
    }

    object ReturnDbNullIfValueIsNull(object? value)
        => value ?? DBNull.Value;
    public async Task UpdateModelAsync(User model)
    {
        string updateSql = "UPDATE AspNetUsers " +
            "SET UserName = @UserName, NormalizedUserName = @NormalizedUserName, Email = @Email,  UsedUserId = @UsedUserId " +
            "WHERE Id = @Id";

        SqlParameter[] sqlParameters = {
            new SqlParameter("@UserName", model.UserName),
            new SqlParameter("@NormalizedUserName", model.UserName!.ToUpper()),
            new SqlParameter("@Email", ReturnDbNullIfValueIsNull(model.Email)),
            new SqlParameter("@UsedUserId", ReturnDbNullIfValueIsNull(model.UsedUserId)),
            new SqlParameter("@Id", model.Id)
        };

        await db.Database.ExecuteSqlRawAsync(updateSql, sqlParameters);
    }

    public async Task<IEnumerable<User>> GetAllModelsAsync()
        => await getModels.AsNoTracking().ToListAsync();

    public async Task<User?> GetModelByIdAsync(string key)
        => await userManager.FindByIdAsync(key);
        //=> await getModels.AsNoTracking().SingleOrDefaultAsync(u => u.Id == key);

    public async Task DeleteModelAsync(string key)
    {
        User? user = await GetModelByIdAsync(key);
        if (user is not null)
            await userManager.DeleteAsync(user);
    }


    public async Task<User?> GetUserByClaimsAsync(ClaimsPrincipal claims)
        => await GetUserByNameAsync(claims.Identity!.Name!);

    public async Task<string?> GetUserIdByUserNameAsync(string userName)
        => (await GetUserByNameAsync(userName))?.Id;

    public async Task<User?> GetUserByNameAsync(string name)
        => await getModels.SingleOrDefaultAsync(u => u.UserName == name);

    protected async Task<User> GetUserByIdAsync(string userId)
    {
        User? user = await GetModelByIdAsync(userId);
        if (user is null)
            throw new ArgumentException($"The user with id '{userId}' doesn't exist");
        return user!;
    }

    protected async Task<User> GetUsedUserAsync()
    {
        string usedUserId = await GetUsedUserIdAsync();
        User usedUser = (await GetModelByIdAsync(usedUserId))!;
        return usedUser;
    }

    protected async Task<string> GetUsedUserIdAsync()
    {
        User currentUser = await GetCurrentUserAsync()!;
        return currentUser.UsedUserId ?? currentUser.Id;
    }

    protected async Task<User> GetCurrentUserAsync()
    {
        var claims = httpContextAccessor.HttpContext!.User;
        return (await GetUserByClaimsAsync(claims))!;
    }

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
        => await userManager.CreateAsync(user, password);
}