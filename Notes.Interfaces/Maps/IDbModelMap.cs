using Notes.Commons.Extensions;
using Notes.Commons;
using Notes.Interfaces.Services;
using Notes.Models;
using Notes.ViewModels;
using Notes.ViewModels.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Interfaces.Primary;
using Notes.Models.Database;

namespace Notes.Interfaces.Maps;

public interface IDbModelMap<T, K> : IDbModelPrimary<T, K> where T : IDbViewModel
{
}
