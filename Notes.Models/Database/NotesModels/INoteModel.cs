using Notes.Models.Base.NotesModels;

namespace Notes.Models.Database.NotesModels;

public abstract class NoteModel : IEntityBase, INoteModelBase
{
    public long Id { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }
}
