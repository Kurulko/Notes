using Notes.Models.Base.NotesModels;

namespace Notes.Models.Database.NotesModels;

public class NoteItem : NoteModel, INoteItemBase
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public long CategoryId { get; set; }
    public Category? Category { get; set; }
}
