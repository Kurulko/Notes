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
    protected readonly DbSet<T> dbAdminModels;
    public AdminModelRepository(NotesContext db)
        => dbAdminModels = db.Set<T>();

    public abstract Task<T> AddModelAsync(T model);
    public abstract Task DeleteModelAsync(string key);
    public abstract Task<IEnumerable<T>> GetAllModelsAsync();
    public abstract Task<T?> GetModelByIdAsync(string key);
    public abstract Task UpdateModelAsync(T model);
}
