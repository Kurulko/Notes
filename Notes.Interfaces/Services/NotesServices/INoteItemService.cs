using Notes.Interfaces.Primaries.NotesPrimaries;
using Notes.Models.Database.NotesModels;

namespace Notes.Interfaces.Services.NotesServices;

public interface INoteItemService : INoteItemPrimary<NoteItem>, INoteModelService<NoteItem>
{
}
