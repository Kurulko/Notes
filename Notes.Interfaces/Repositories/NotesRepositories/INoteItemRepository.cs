using Notes.Interfaces.Primaries.NotesPrimaries;
using Notes.Models.Database.NotesModels;

namespace Notes.Interfaces.Repositories.NotesRepositories;

public interface INoteItemRepository : INoteModelRepository<NoteItem>, INoteItemPrimary<NoteItem>
{
}
