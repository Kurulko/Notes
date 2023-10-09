using Microsoft.AspNetCore.Identity;
using Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;
using Notes.Models.Database.AdminModels;
using Notes.Models.Database.NotesModels;

namespace Notes.Interfaces.Services.AdminServices.UserServices;

public interface IUserService : IUserPrimary<User, NoteItem>, IAdminModelService<User>
{
}