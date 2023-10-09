using Microsoft.AspNetCore.Identity;
using Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;
using Notes.Models.Database.AdminModels;
using System.Security.Claims;

namespace Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;

public interface IBaseUserRepository : IAdminModelRepository<User>, IBaseUserPrimary<User>
{
}