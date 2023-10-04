using Notes.Commons;
using Notes.Models.Base;
using Notes.Models.Base.NotesModels;
using Notes.Models.Database.NotesModels;
using Notes.ViewModels;

namespace Notes.Interfaces.Primaries.UserPrimaries;

public interface IUserModelsPrimary<T, K1> 
    where T : IUserBase
    where K1 : INoteItemBase
{
    Task<IndexViewModel<K1>> GetUserNoteItemsAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null);
}