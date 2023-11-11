using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.Interfaces.Repositories.AdminRepositories;
using Notes.Models.Context;
using Notes.Models.Database.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Repositories.AdminRepositories;

public abstract class AdminModelRepository<T> : IAdminModelRepository<T> where T : class, IAdminModel
{
    protected IQueryable<T> dbAdminModels;
    public AdminModelRepository(IQueryable<T> dbAdminModels)
        => this.dbAdminModels = dbAdminModels;

    public abstract Task<T> AddModelAsync(T model);
    public abstract Task DeleteModelAsync(string key);
    public abstract Task UpdateModelAsync(T model);

    public virtual async Task<IEnumerable<T>> GetAllModelsAsync()
        => await dbAdminModels.ToListAsync();

    public virtual async Task<T?> GetModelByIdAsync(string key)
        => (await GetAllModelsAsync()).SingleOrDefault(m => m.Id == key);
}
