using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models.Base.NotesModels;

public interface INoteModelBase : IModelBase
{
    long Id { get; set; }
}
