using Notes.Models.Base.NotesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.ViewModels.Database.NotesModels;

public class NoteViewModel : IDbViewModel, INoteModelBase
{
    public long Id { get; set; }
}
