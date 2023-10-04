using Notes.Interfaces.Maps.NotesMaps;
using Notes.ViewModels.Database.NotesModels;

namespace WebApi.Controllers.CRUDControllers.NotesControllers;

public class CategoriesController : NotesModelsController<CategoryViewModel>
{
    public CategoriesController(ICategoryMap map, ILogger<CategoriesController> logger) : base(map, logger) { }
}
