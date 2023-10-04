using Microsoft.AspNetCore.Mvc;
using Notes.Interfaces.Maps;
using Notes.ViewModels;
using Notes.ViewModels.Database;
using Notes.Commons.Extensions;

namespace WebApi.Controllers.CRUDControllers;

public abstract class DbModelsController<T, K> : ApiController where T : IDbViewModel
{
    protected readonly IDbModelMap<T, K> map;
    public DbModelsController(IDbModelMap<T, K> map, ILogger<DbModelsController<T, K>> logger) : base(logger)
        => this.map = map;

    [HttpGet]
    public virtual async Task<IndexViewModel<T>> GetModelsAsync([FromQuery] string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        => await map.GetModelsAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber);

    [HttpGet("{key}")]
    public virtual async Task<T?> GetModelByIdAsync(K key)
        => await map.GetModelByIdAsync(key);

    [HttpPost]
    public virtual async Task<T> AddModelAsync(T model)
        => await map.AddModelAsync(model);

    [HttpPut]
    public virtual async Task<IActionResult> UpdateModelAsync(T model)
        => await ReturnOkIfEverithingIsGood(async () => await map.UpdateModelAsync(model));

    [HttpDelete("{key}")]
    public virtual async Task<IActionResult> DeleteModelAsync(K key)
        => await ReturnOkIfEverithingIsGood(async () => await map.DeleteModelAsync(key));
}