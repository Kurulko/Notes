using Notes.Interfaces.Primary;
using Notes.Models.Database.NotesModels;
using Notes.Interfaces.Repositories;
using Notes.Interfaces.Primaries.NotesPrimaries;

namespace Notes.Interfaces.Services.NotesServices;

public interface INoteModelService<T> : INoteModelPrimary<T>, IDbModelService<T, long> where T : NoteModel
{
}
