using Notes.Models.Database.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces.Services.AdminServices;

public interface IAdminModelService<T> : IDbModelService<T, string> where T : IAdminModel
{
}
