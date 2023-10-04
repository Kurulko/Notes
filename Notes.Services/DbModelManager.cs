using Notes.Interfaces.Repositories;
using Notes.Interfaces.Services;
using Notes.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services;

public abstract class DbModelManager<T, K> : IDbModelService<T, K> where T : IEntityBase
{
    readonly IDbModelRepository<T, K> dbModelRepository;
    public DbModelManager(IDbModelRepository<T, K> dbModelRepository)
        => this.dbModelRepository = dbModelRepository;

    public async Task<T> AddModelAsync(T model)
        => await dbModelRepository.AddModelAsync(model);

    public async Task DeleteModelAsync(K key)
        => await dbModelRepository.DeleteModelAsync(key);

    public async Task<IEnumerable<T>> GetAllModelsAsync()
        => await dbModelRepository.GetAllModelsAsync();

    public async Task<T?> GetModelByIdAsync(K key)
        => await dbModelRepository.GetModelByIdAsync(key);

    public async Task UpdateModelAsync(T model)
        => await dbModelRepository.UpdateModelAsync(model);
}
