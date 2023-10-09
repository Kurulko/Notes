using Microsoft.AspNetCore.Identity;
using Notes.Models.Base.AdminModels;
using Notes.Models.Database.NotesModels;

namespace Notes.Models.Database.AdminModels;

public class User : IdentityUser, IAdminModel, IUserBase
{
    public DateTime Registered { get; set; }
    public string? UsedUserId { get; set; }

    public IEnumerable<NoteItem>? NoteItems { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }
}
