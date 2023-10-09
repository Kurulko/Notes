using Notes.Interfaces.Primary;
using Notes.Models.Base.AdminModels;
using Notes.Models.Database;

namespace Notes.Interfaces.Primaries.AdminPrimaries.RolePrimaries;

public interface IRolePrimary<T> : IAdminModelPrimary<T> where T : IRoleBase
{
    Task<T?> GetRoleByNameAsync(string name);
}