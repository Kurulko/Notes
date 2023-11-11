using Notes.Models.Base.NotesModels;
using Notes.Models.Database.AdminModels;
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

    public long CategoryId { get; set; }
    public CategoryViewModel? Category { get; set; }


    public static explicit operator NoteItem?(NoteItemViewModel? noteItemViewModel)
        => noteItemViewModel is null ? null : new()
        {
            Id = noteItemViewModel.Id,
            Title = noteItemViewModel.Title,
            Description = noteItemViewModel.Description,
            Category = (Category?)noteItemViewModel.Category,
            CategoryId = noteItemViewModel.CategoryId,
        };

    public static explicit operator NoteItemViewModel?(NoteItem? noteItem)
        => noteItem is null ? null : new()
        {
            Id = noteItem.Id,
            Title = noteItem.Title,
            Description = noteItem.Description,
            Category = (CategoryViewModel?)noteItem.Category,
            CategoryId = noteItem.CategoryId,
        };
}
