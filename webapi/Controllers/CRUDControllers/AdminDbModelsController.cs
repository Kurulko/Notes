using Microsoft.AspNetCore.Authorization;
using Notes.Commons;
using Notes.Interfaces.Maps;
using Notes.ViewModels.Database;
using WebApi.Controllers.AuthControllers;

namespace WebApi.Controllers.CRUDControllers;

[Authorize(Roles = Roles.Admin)]
public class AdminDbModelsController<TModel, TKey> : DbModelsController<TModel, TKey> where TModel : IDbViewModel
{
    public AdminDbModelsController(IDbModelMap<TModel, TKey> service, ILogger<AdminDbModelsController<TModel, TKey>> logger) : base(service, logger) { }
}