using Notes.Interfaces.Primaries.NotesPrimaries;
using Notes.Models.Database.NotesModels;

namespace Notes.Interfaces.Repositories.NotesRepositories;

public interface ICategoryRepository : INoteModelRepository<Category>, ICategoryPrimary<Category>
{
}
