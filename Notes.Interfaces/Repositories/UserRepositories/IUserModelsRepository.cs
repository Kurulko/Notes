using Notes.Commons;
using Notes.Interfaces.Primaries.UserPrimaries;
using Notes.Models.Database;
using Notes.Models.Database.NotesModels;
using Notes.ViewModels;

namespace Notes.Interfaces.Repositories.UserRepositories;

public interface IUserModelsRepository : IUserModelsPrimary<User, NoteItem>
{
}