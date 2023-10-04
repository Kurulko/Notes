using Notes.Interfaces.Repositories.NotesRepositories;
using Notes.Interfaces.Services.NotesServices;
using Notes.Models.Database.NotesModels;

namespace Notes.Services.NotesServices;

public class CategoryManager : NoteModelManager<Category>, ICategoryService
{
    public CategoryManager(ICategoryRepository categoryRepository) : base(categoryRepository) { }
}
