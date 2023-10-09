using Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;
using Notes.Models.Database.AdminModels;

namespace Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;

public interface IUsedUserRepository : IUsedUserPrimary<User>
{
}