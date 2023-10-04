using Notes.Models.Base.NotesModels;
using Notes.Models.Database.NotesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.ViewModels.Database.NotesModels;

public class NoteItemViewModel : NoteViewModel, INoteItemBase
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public static explicit operator NoteItem(NoteItemViewModel noteItemViewModel)
        => new()
        {
            Id = noteItemViewModel.Id,
            Title = noteItemViewModel.Title,
            Description = noteItemViewModel.Description,
        };

    public static explicit operator NoteItemViewModel(NoteItem noteItem)
        => new()
        {
            Id = noteItem.Id,
            Title = noteItem.Title,
            Description = noteItem.Description,
        };
}
