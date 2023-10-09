using Notes.Models.Database.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces.Repositories.AdminRepositories;

public interface IAdminModelRepository<T> : IDbModelRepository<T, string> where T : IAdminModel
{
}
