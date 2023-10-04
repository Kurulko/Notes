using Notes.Interfaces.Primaries.NotesPrimaries;
using Notes.Models.Database.NotesModels;

namespace Notes.Interfaces.Repositories.NotesRepositories;

public interface INoteModelRepository<T> : IDbModelRepository<T, long>, INoteModelPrimary<T>
    where T : NoteModel
{
}
