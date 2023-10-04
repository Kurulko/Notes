using Microsoft.AspNetCore.Identity;
using Notes.Models.Base;
using Notes.Models.Database.NotesModels;

namespace Notes.Models.Database;

public class User : IdentityUser, IEntityBase, IUserBase
{
    public DateTime Registered { get; set; }
    public string? UsedUserId { get; set; }

    public IEnumerable<NoteItem>? NoteItems { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }
}
