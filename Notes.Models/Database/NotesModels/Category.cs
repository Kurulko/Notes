using Notes.Models.Base.NotesModels;

namespace Notes.Models.Database.NotesModels;

public class Category : NoteModel, ICategoryBase
{
    public string Name { get; set; } = null!;

    public IEnumerable<NoteItem>? NoteItems { get; set; }
}
