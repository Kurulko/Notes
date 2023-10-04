using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Notes.Interfaces.Maps;
using Notes.Interfaces.Repositories;
using Notes.Interfaces.Services;
using Notes.Models.Database;
using Notes.ViewModels.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Maps;

public abstract class DbModelMap<TView, T, K> : IDbModelMap<TView, K> where T : IEntityBase where TView : IDbViewModel
{
    readonly IDbModelService<T, K> dbModelService;
    public DbModelMap(IDbModelService<T, K> dbModelService)
        => this.dbModelService = dbModelService;

    protected abstract TView ConvertToViewModel(T model);
    protected abstract T ConvertFromViewModel(TView viewModel);

    protected IEnumerable<TView> ConvertToViewModels(IEnumerable<T> models)
        => models.Select(ConvertToViewModel);
    protected IEnumerable<T> ConvertFromViewModels(IEnumerable<TView> viewModels)
        => viewModels.Select(ConvertFromViewModel);


    public async Task<TView> AddModelAsync(TView model)
        => ConvertToViewModel(await dbModelService.AddModelAsync(ConvertFromViewModel(model)));

    public async Task DeleteModelAsync(K key)
        => await dbModelService.DeleteModelAsync(key);

    public async Task<IEnumerable<TView>> GetAllModelsAsync()
        => ConvertToViewModels(await dbModelService.GetAllModelsAsync());

    public async Task<TView?> GetModelByIdAsync(K key)
        => ConvertToNullableViewModel(await dbModelService.GetModelByIdAsync(key));

    public async Task UpdateModelAsync(TView model)
        => await dbModelService.UpdateModelAsync(ConvertFromViewModel(model));

    protected TView? ConvertToNullableViewModel(T? model)
    {
        if (model is null)
            return default;
        return ConvertToViewModel(model);
    }
}
