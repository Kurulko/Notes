using Microsoft.AspNetCore.Mvc;
using Notes.Interfaces.Maps.NotesMaps;
using Notes.ViewModels.Database.NotesModels;

namespace WebApi.Controllers.CRUDControllers.NotesControllers;

[Route($"{pathApi}/notes-items")]
public class NotesItemsController : NotesModelsController<NoteItemViewModel>
{
    public NotesItemsController(INoteItemMap map, ILogger<NotesItemsController> logger) : base(map, logger)
    {
    }
}
