using Microsoft.AspNetCore.Identity;
using Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;
using Notes.Interfaces.Services;
using Notes.Models;
using Notes.ViewModels.Database.AdminModels;
using Notes.ViewModels.Database.NotesModels;

namespace Notes.Interfaces.Maps.AdminMaps.UserMaps;

public interface IUserMap : IAdminModelMap<UserViewModel>, IUserPrimary<UserViewModel, NoteItemViewModel, CategoryViewModel>
{
}