using Notes.Interfaces.Primaries.UserPrimaries;
using Notes.Models.Database;
using Notes.Models.Database.NotesModels;

namespace Notes.Interfaces.Repositories.UserRepositories;

public interface IUserRepository : IBaseUserRepository, IUserRolesRepository, IUserPasswordRepository, IUsedUserRepository, IUserModelsRepository, IUserPrimary<User, NoteItem>
{
}