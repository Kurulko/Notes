using Notes.Interfaces.Primary;
using Notes.Models.Base.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces.Primaries.AdminPrimaries;

public interface IAdminModelPrimary<T> : IDbModelPrimary<T, string> where T : IAdminModelBase
{
}
