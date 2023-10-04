using Notes.Interfaces.Primary;
using Notes.Models.Base;
using Notes.Models.Database;

namespace Notes.Interfaces.Primaries.RolePrimaries;

public interface IRolePrimary<T> : IDbModelPrimary<T, string> where T : IRoleBase
{
    Task<T?> GetRoleByNameAsync(string name);
    Task<T> CreateRole();
}