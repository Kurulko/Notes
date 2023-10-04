using Microsoft.AspNetCore.Identity;
using Notes.Interfaces.Primaries.UserPrimaries;
using Notes.Interfaces.Repositories.UserRepositories;
using Notes.Interfaces.Services;
using Notes.Models;
using Notes.ViewModels.Database;
using Notes.ViewModels.Database.NotesModels;

namespace Notes.Interfaces.Maps.UserMaps;

public interface IUserMap : IDbModelMap<UserViewModel, string>, IUserPrimary<UserViewModel, NoteItemViewModel>
{
}