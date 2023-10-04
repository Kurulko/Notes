using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models.Base;

public interface IRoleBase : IModelBase
{
    string Id { get; set; }
    string Name { get; set; }
}
