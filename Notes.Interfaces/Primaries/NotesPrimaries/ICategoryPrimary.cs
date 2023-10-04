using Notes.Models.Base.NotesModels;
using Notes.Models.Database.NotesModels;

namespace Notes.Interfaces.Primaries.NotesPrimaries;

public interface ICategoryPrimary<T> : INoteModelPrimary<T> where T : ICategoryBase
{
}
