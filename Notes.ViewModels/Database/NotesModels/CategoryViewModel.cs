﻿using Notes.Models;
using Notes.Models.Base.NotesModels;
using Notes.Models.Database.NotesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Notes.ViewModels.Database.NotesModels;

public class CategoryViewModel : NoteViewModel, ICategoryBase
{
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public IEnumerable<NoteItemViewModel>? NoteItems { get; set; }


    public static explicit operator Category?(CategoryViewModel? categoryViewModel)
        => categoryViewModel is null ? null : new()
        {
            Id = categoryViewModel.Id,
            Name = categoryViewModel.Name,
            NoteItems = categoryViewModel.NoteItems?.Select(nt => (NoteItem)nt!)
        };

    public static explicit operator CategoryViewModel?(Category? category)
        => category is null ? null : new()
        {
            Id = category.Id,
            Name = category.Name,
            NoteItems = category.NoteItems?.Select(nt => (NoteItemViewModel)nt!)
        };
}

