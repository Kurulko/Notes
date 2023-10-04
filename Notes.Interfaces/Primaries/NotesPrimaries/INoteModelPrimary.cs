using Notes.Interfaces.Primary;
using Notes.Models.Base.NotesModels;
using Notes.Models.Database.NotesModels;

namespace Notes.Interfaces.Primaries.NotesPrimaries;

public interface INoteModelPrimary<T> : IDbModelPrimary<T, long> where T : INoteModelBase
{
}
