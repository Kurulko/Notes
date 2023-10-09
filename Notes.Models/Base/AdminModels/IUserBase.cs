using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models.Base.AdminModels;

public interface IUserBase : IAdminModelBase
{
    string UserName { get; set; }
    string? UsedUserId { get; set; }
    string? Email { get; set; }
    DateTime Registered { get; set; }
}
