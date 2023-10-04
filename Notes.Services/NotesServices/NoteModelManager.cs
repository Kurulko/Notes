using Microsoft.EntityFrameworkCore;
using Notes.Interfaces.Repositories;
using Notes.Interfaces.Repositories.NotesRepositories;
using Notes.Interfaces.Services.NotesServices;
using Notes.Models;
using Notes.Models.Context;
using Notes.Models.Database.NotesModels;
using Notes.Services;

namespace Notes.Services.NotesServices;

public abstract class NoteModelManager<T> : DbModelManager<T, long>, INoteModelService<T> where T : NoteModel 
{
    protected readonly INoteModelRepository<T> noteModelRepository;
    public NoteModelManager(INoteModelRepository<T> noteModelRepository) : base(noteModelRepository)
        => this.noteModelRepository = noteModelRepository;
}
