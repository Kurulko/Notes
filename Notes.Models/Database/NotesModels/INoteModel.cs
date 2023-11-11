using Notes.Models.Base.NotesModels;
using Notes.Models.Database.AdminModels;

namespace Notes.Models.Database.NotesModels;

public abstract class NoteModel : IEntityBase, INoteModelBase
{
    public long Id { get; set; }

    public string? UserId { get; set; }
    public User? User { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }
}
