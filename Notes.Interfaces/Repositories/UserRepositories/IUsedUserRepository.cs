using Notes.Interfaces.Primaries.UserPrimaries;
using Notes.Models.Database;

namespace Notes.Interfaces.Repositories.UserRepositories;

public interface IUsedUserRepository : IUsedUserPrimary<User>
{
}