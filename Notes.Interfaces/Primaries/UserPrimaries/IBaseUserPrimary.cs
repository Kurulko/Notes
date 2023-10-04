using Microsoft.AspNetCore.Identity;
using Notes.Interfaces.Primary;
using Notes.Models.Base;
using Notes.Models.Database;
using System.Security.Claims;

namespace Notes.Interfaces.Primaries.UserPrimaries;

public interface IBaseUserPrimary<T> : IDbModelPrimary<T, string> where T : IUserBase
{
    Task<T> CreateUserAsync();
    Task<IdentityResult> CreateUserAsync(T user, string password);

    Task<string?> GetUserIdByUserNameAsync(string userName);
    Task<T?> GetUserByClaimsAsync(ClaimsPrincipal claims);
    Task<T?> GetUserByNameAsync(string name);
}