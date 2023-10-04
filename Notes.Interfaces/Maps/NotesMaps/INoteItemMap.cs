using Notes.Interfaces.Primaries.NotesPrimaries;
using Notes.ViewModels.Database.NotesModels;

namespace Notes.Interfaces.Maps.NotesMaps;

public interface INoteItemMap : INoteModelMap<NoteItemViewModel>, INoteItemPrimary<NoteItemViewModel>
{
}
