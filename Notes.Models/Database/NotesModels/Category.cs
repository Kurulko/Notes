using Notes.Models.Base.NotesModels;

namespace Notes.Models.Database.NotesModels;

public class Category : NoteModel, ICategoryBase
{
    public string Name { get; set; } = null!;

    public long? NoteItemId { get; set; }
    public NoteItem? NoteItem { get; set; }

    public long? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public IEnumerable<Category>? Categories { get; set; }
}
