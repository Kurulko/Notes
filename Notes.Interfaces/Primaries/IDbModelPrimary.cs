using Notes.Commons.Extensions;
using Notes.Commons;
using Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Models.Base;

namespace Notes.Interfaces.Primary;

public interface IDbModelPrimary<T, K> where T : IModelBase
{
    Task<IEnumerable<T>> GetAllModelsAsync();

    private IEnumerable<T> GetSortedModels(IEnumerable<T> models, string? attribute, OrderBy? orderBy)
        => attribute is null || orderBy is null ? models : models.OrderBy(attribute, orderBy.Value);

    async Task<IEnumerable<T>> GetModelsAsync(string? attribute, OrderBy? orderBy)
    {
        var models = await GetAllModelsAsync();
        return GetSortedModels(models, attribute, orderBy);
    }

    private IndexViewModel<T> GetPaddingModels(IEnumerable<T> models, int? pageSize, int? pageNumber)
    {
        var paddingModels = pageSize is null || pageNumber is null ? models : models.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
        return paddingModels.ToIndexViewModel(models.Count(), pageSize, pageNumber);
    }

    async Task<IndexViewModel<T>> GetModelsAsync(int? pageSize, int? pageNumber)
    {
        var models = await GetAllModelsAsync();
        return GetPaddingModels(models, pageSize, pageNumber);
    }

    async Task<IndexViewModel<T>> GetModelsAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber)
    {
        var models = await GetAllModelsAsync();
        return GetPaddingModels(GetSortedModels(models, attribute, orderBy), pageSize, pageNumber);
    }

    Task<T?> GetModelByIdAsync(K key);
    Task UpdateModelAsync(T model);
    Task<T> AddModelAsync(T model);
    Task DeleteModelAsync(K key);
}
