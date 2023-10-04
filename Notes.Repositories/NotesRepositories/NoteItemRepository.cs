using Notes.Interfaces.Repositories.NotesRepositories;
using Notes.Models.Context;
using Notes.Models.Database.NotesModels;

namespace Notes.Repositories.NotesRepositories;

public class NoteItemRepository : NoteModelRepository<NoteItem>, INoteItemRepository
{
    public NoteItemRepository(NotesContext db) : base(db)
    {
    }
}
