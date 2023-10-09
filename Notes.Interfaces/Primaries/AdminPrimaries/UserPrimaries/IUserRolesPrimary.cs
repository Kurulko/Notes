using Notes.Models.Base.AdminModels;
using Notes.Models.Database;
using Notes.ViewModels;

namespace Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;

public interface IUserRolesPrimary<T> where T : IUserBase
{
    Task<IEnumerable<string>> GetRolesAsync(string? userId);
    Task<IEnumerable<T>> GetUsersByRoleAsync(string roleName);
    Task AddRoleToUserAsync(ModelWithUserId<string> model);
    Task DeleteRoleFromUserAsync(ModelWithUserId<string> model);
}