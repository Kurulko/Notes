using Notes.Models;
using Notes.Models.Base.NotesModels;
using Notes.Models.Database.NotesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.ViewModels.Database.NotesModels;

public class CategoryViewModel : NoteViewModel, ICategoryBase
{
    public string Name { get; set; } = null!;

    public static explicit operator Category(CategoryViewModel categoryViewModel)
        => new()
        {
            Id = categoryViewModel.Id,
            Name = categoryViewModel.Name,
        };

    public static explicit operator CategoryViewModel(Category category)
        => new()
        {
            Id = category.Id,
            Name = category.Name,
        };
}

