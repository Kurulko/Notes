using Notes.Commons;
using Notes.Models.Base.AdminModels;
using Notes.Models.Base.NotesModels;
using Notes.Models.Database.NotesModels;
using Notes.ViewModels;

namespace Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;

public interface IUserModelsPrimary<T, K1, K2>
    where T : IUserBase
    where K1 : INoteItemBase
    where K2 : ICategoryBase
{
    Task<IndexViewModel<K1>> GetUserNoteItemsAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null);
    Task<IndexViewModel<K2>> GetUserCategoriesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null);
}