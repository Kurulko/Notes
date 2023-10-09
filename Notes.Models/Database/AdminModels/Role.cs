using Microsoft.AspNetCore.Identity;
using Notes.Models.Base.AdminModels;

namespace Notes.Models.Database.AdminModels;

public class Role : IdentityRole, IAdminModel, IRoleBase
{
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }
}
