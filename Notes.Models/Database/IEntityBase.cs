using Notes.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models.Database;

public interface IEntityBase : IModelBase
{
    [Column("DateCreated")]
    public DateTime Created { get; set; }

    [Column("DateUpdated")]
    public DateTime? Updated { get; set; }

    [Column("DateDeleted")]
    public DateTime? Deleted { get; set; }
}
