using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models.Base.AdminModels;

public interface IRoleBase : IAdminModelBase
{
    string Name { get; set; }
}
