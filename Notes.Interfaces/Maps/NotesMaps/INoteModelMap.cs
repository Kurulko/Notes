using Notes.Interfaces.Primaries.NotesPrimaries;
using Notes.Interfaces.Services;
using Notes.ViewModels.Database.NotesModels;

namespace Notes.Interfaces.Maps.NotesMaps;

public interface INoteModelMap<T> : IDbModelMap<T, long>, INoteModelPrimary<T> where T : NoteViewModel
{
}
