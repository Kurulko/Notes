using Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;
using Notes.Models.Database.AdminModels;
using Notes.Models.Database.NotesModels;

namespace Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;

public interface IUserRepository : IBaseUserRepository, IUserRolesRepository, IUserPasswordRepository, IUsedUserRepository, IUserModelsRepository, IUserPrimary<User, NoteItem, Category>
{
}