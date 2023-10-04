using Notes.Interfaces.Maps.NotesMaps;
using Notes.ViewModels.Database.NotesModels;

namespace WebApi.Controllers.CRUDControllers.NotesControllers;

public abstract class NotesModelsController<T> : DbModelsController<T, long> where T : NoteViewModel
{
    public NotesModelsController(INoteModelMap<T> service, ILogger<NotesModelsController<T>> logger) : base(service, logger) { }
}