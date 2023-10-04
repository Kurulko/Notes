using Notes.Interfaces.Maps.NotesMaps;
using Notes.Interfaces.Services.NotesServices;
using Notes.Models.Database.NotesModels;
using Notes.ViewModels.Database.NotesModels;

namespace Notes.Maps.NotesMaps;

public class NoteItemMap : NoteModelMap<NoteItem, NoteItemViewModel>, INoteItemMap
{
    public NoteItemMap(INoteItemService noteItemService) : base(noteItemService) { }

    protected override NoteItem ConvertFromViewModel(NoteItemViewModel viewModel)
        => (NoteItem)viewModel;

    protected override NoteItemViewModel ConvertToViewModel(NoteItem model)
        => (NoteItemViewModel)model;
}
