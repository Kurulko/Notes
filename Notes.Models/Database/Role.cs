using Microsoft.AspNetCore.Identity;
using Notes.Models.Base;

namespace Notes.Models.Database;

public class Role : IdentityRole, IEntityBase, IRoleBase
{
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }
}
