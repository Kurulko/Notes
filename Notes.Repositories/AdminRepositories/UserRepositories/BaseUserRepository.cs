using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;
using Notes.Models.Context;
using Notes.Models.Database.AdminModels;
using System.Security.Claims;

namespace Notes.Repositories.AdminRepositories.UserRepositories;

public class BaseUserRepository : AdminModelRepository<User>, IBaseUserRepository
{
    protected readonly IHttpContextAccessor httpContextAccessor;
    protected readonly UserManager<User> userManager;

    public BaseUserRepository(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : base(userManager.Users)
    {
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;
    }

    public override async Task<User> AddModelAsync(User model)
    {
        User? existingUser = await GetModelByIdAsync(model.Id);
        if (existingUser is null)
        {
            await userManager.CreateAsync(model);
            return model;
        }
        return existingUser;
    }

    public override async Task UpdateModelAsync(User model)
    {
        User? existingUser = await GetModelByIdAsync(model.Id);

        if (existingUser is not null)
        {
            if(existingUser.UserName != model.UserName)
                await userManager.SetUserNameAsync(existingUser, model.UserName);

            if (existingUser.Email != model.Email)
                await userManager.SetEmailAsync(existingUser, model.Email);

            if (existingUser.UsedUserId != model.UsedUserId)
            {
                existingUser.UsedUserId = model.UsedUserId;
                await userManager.UpdateAsync(existingUser);
            }
        }
    }

    public override async Task DeleteModelAsync(string key)
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
        => (await GetAllModelsAsync()).SingleOrDefault(u => u.UserName.ToLower() == name.ToLower());

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