using Notes.Interfaces.Maps.NotesMaps;
using Notes.ViewModels.Database.NotesModels;

namespace WebApi.Controllers.CRUDControllers.NotesControllers;

public class NotesItemsController : NotesModelsController<NoteItemViewModel>
{
    public NotesItemsController(INoteItemMap map, ILogger<NotesItemsController> logger) : base(map, logger)
    {
    }
}
