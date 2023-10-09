using Notes.Commons;
using Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;
using Notes.Models.Database.AdminModels;
using Notes.Models.Database.NotesModels;
using Notes.ViewModels;

namespace Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;

public interface IUserModelsRepository : IUserModelsPrimary<User, NoteItem>
{
}