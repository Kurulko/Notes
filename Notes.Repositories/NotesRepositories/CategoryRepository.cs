using Notes.Interfaces.Repositories.NotesRepositories;
using Notes.Interfaces.Services.AdminServices.UserServices;
using Notes.Models.Context;
using Notes.Models.Database.NotesModels;

namespace Notes.Repositories.NotesRepositories;

public class CategoryRepository : NoteModelRepository<Category>, ICategoryRepository
{
    public CategoryRepository(NotesContext db, IUserService userService) : base(db, userService)
    {
    }
}
