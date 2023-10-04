using Microsoft.AspNetCore.Identity;
using Notes.Interfaces.Primaries.UserPrimaries;
using Notes.Interfaces.Repositories.UserRepositories;
using Notes.Models.Database;
using Notes.Models.Database.NotesModels;

namespace Notes.Interfaces.Services.UserServices;

public interface IUserService : IUserPrimary<User, NoteItem>, IDbModelService<User, string>
{
}