using Notes.ViewModels.Database.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces.Maps.AdminMaps;

public interface IAdminModelMap<T> : IDbModelMap<T, string> where T : IAdminViewModel
{
}
