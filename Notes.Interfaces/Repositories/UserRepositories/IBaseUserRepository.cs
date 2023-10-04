using Microsoft.AspNetCore.Identity;
using Notes.Interfaces.Primaries.UserPrimaries;
using Notes.Models.Database;
using System.Security.Claims;

namespace Notes.Interfaces.Repositories.UserRepositories;

public interface IBaseUserRepository : IDbModelRepository<User, string>, IBaseUserPrimary<User>
{
}