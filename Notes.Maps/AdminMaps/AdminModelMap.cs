using Notes.Interfaces.Services;
using Notes.Interfaces.Services.AdminServices;
using Notes.Models.Database.AdminModels;
using Notes.ViewModels.Database.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Maps.AdminMaps;

public abstract class AdminModelMap<TView, T> : DbModelMap<TView, T, string>
    where T : IAdminModel
    where TView : IAdminViewModel
{
    protected AdminModelMap(IAdminModelService<T> adminModelService) : base(adminModelService) { }
}
