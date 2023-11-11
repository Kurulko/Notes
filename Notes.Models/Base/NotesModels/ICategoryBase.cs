using Notes.Models.Database.NotesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models.Base.NotesModels;

public interface ICategoryBase : INoteModelBase
{
    string Name { get; set; }
}
