using Notes.Interfaces.Maps.NotesMaps;
using Notes.Interfaces.Services.NotesServices;
using Notes.Models.Database.NotesModels;
using Notes.ViewModels.Database.NotesModels;

namespace Notes.Maps.NotesMaps;

public class CategoryMap : NoteModelMap<Category, CategoryViewModel>, ICategoryMap
{
    public CategoryMap(ICategoryService categoryService) : base(categoryService) { }

    protected override Category ConvertFromViewModel(CategoryViewModel viewModel)
        => (Category)viewModel!;

    protected override CategoryViewModel ConvertToViewModel(Category model)
        => (CategoryViewModel)model!;

}
