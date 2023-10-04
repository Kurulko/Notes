using Microsoft.EntityFrameworkCore;
using Notes.Interfaces.Maps;
using Notes.Interfaces.Maps.NotesMaps;
using Notes.Interfaces.Repositories;
using Notes.Interfaces.Services.NotesServices;
using Notes.Models;
using Notes.Models.Context;
using Notes.Models.Database.NotesModels;
using Notes.ViewModels.Database.NotesModels;

namespace Notes.Maps.NotesMaps;


public abstract class NoteModelMap<T, TView> : DbModelMap<TView, T, long>, INoteModelMap<TView> 
    where T : NoteModel 
    where TView : NoteViewModel
{
    protected readonly INoteModelService<T> noteModelService;
    public NoteModelMap(INoteModelService<T> noteModelService) : base(noteModelService)
        => this.noteModelService = noteModelService;
}
