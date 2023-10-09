using Microsoft.AspNetCore.Identity;
using Notes.Interfaces.Primary;
using Notes.Models.Base.AdminModels;
using Notes.Models.Database;
using System.Security.Claims;

namespace Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;

public interface IBaseUserPrimary<T> : IAdminModelPrimary<T> where T : IUserBase
{
    Task<IdentityResult> CreateUserAsync(T user, string password);

    Task<string?> GetUserIdByUserNameAsync(string userName);
    Task<T?> GetUserByClaimsAsync(ClaimsPrincipal claims);
    Task<T?> GetUserByNameAsync(string name);
}