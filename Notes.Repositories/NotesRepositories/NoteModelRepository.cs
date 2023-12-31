﻿using Microsoft.EntityFrameworkCore;
using Notes.Interfaces.Repositories.NotesRepositories;
using Notes.Interfaces.Services.AdminServices.UserServices;
using Notes.Models.Context;
using Notes.Models.Database.NotesModels;

namespace Notes.Repositories.NotesRepositories;

public abstract class NoteModelRepository<T> : INoteModelRepository<T> where T : NoteModel 
{
    protected readonly IUserService userService;
    protected readonly NotesContext db;
    protected readonly DbSet<T> dbSet;
    public NoteModelRepository(NotesContext db, IUserService userService)
    {
        this.db = db;
        this.userService = userService;
        dbSet = db.Set<T>();
    }

    protected virtual IQueryable<T> GetAllModels()
        => dbSet;

    public virtual async Task<T> AddModelAsync(T model)
    {
        var existingModel = await GetModelByIdAsync(model.Id);
        if (existingModel is null)
        {
            model.UserId = await userService.GetUsedUserIdAsync();
            
            await dbSet.AddAsync(model);
            await SaveChangesAsync();
            return model;
        }
        return existingModel;
    }

    public virtual async Task DeleteModelAsync(long key)
    {
        T? model = await GetModelByIdAsync(key);
        if (model is not null)
        {
            dbSet.Remove(model);
            await SaveChangesAsync();
        }
    }

    public virtual async Task<IEnumerable<T>> GetAllModelsAsync()
        => await GetAllModels().ToListAsync();

    public virtual async Task<T?> GetModelByIdAsync(long key)
        => await GetAllModels().SingleOrDefaultAsync(m => m.Id == key);

    public virtual async Task UpdateModelAsync(T model)
    {
        model.UserId = await userService.GetUsedUserIdAsync();
        
        dbSet.Update(model);
        await SaveChangesAsync();
    }

    async Task SaveChangesAsync()
        => await db.SaveChangesAsync();
}
