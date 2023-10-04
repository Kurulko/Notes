using Notes.Interfaces.Repositories.NotesRepositories;
using Notes.Interfaces.Services.NotesServices;
using Notes.Models.Database.NotesModels;

namespace Notes.Services.NotesServices;

public class NoteItemManager : NoteModelManager<NoteItem>, INoteItemService
{
    public NoteItemManager(INoteItemRepository noteItemRepository) : base(noteItemRepository) { }
}
