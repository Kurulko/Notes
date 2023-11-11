using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Notes.Models.Context;
using Microsoft.AspNetCore.Http;
using Notes.ViewModels;
using Notes.Commons;
using Notes.Commons.Extensions;
using Notes.Models.Database.NotesModels;
using Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;
using Notes.Models.Database.AdminModels;

namespace Notes.Repositories.AdminRepositories.UserRepositories;

public class UserModelsRepository : BaseUserRepository, IUserModelsRepository
{
    public UserModelsRepository(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : base(userManager, httpContextAccessor)
    { }


    IndexViewModel<T> GetFilteredData<T>(IEnumerable<T>? models, string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber)
        where T : NoteModel
    {
        int countOfAllModels = models.CountOrDefault();

        attribute = attribute ?? nameof(NoteModel.Id);
        orderBy = orderBy ?? OrderBy.Ascending;
        pageNumber = pageNumber ?? 1;
        pageSize = pageSize ?? countOfAllModels;

        return models.GetModelsOrEmpty().OrderBy(attribute, orderBy.Value).Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value).ToIndexViewModel(countOfAllModels, pageSize, pageNumber);
    }

    async Task<User> GetUsedUserIfUserIdIsNull(string? userId)
        => await (userId is null ? GetUsedUserAsync() : GetUserByIdAsync(userId));

    async Task<IndexViewModel<T>> GetUserModelsAsync<T>(string? userId, string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, Expression<Func<User, IEnumerable<T>?>> include)
        where T : NoteModel
    {
        dbAdminModels = dbAdminModels.Include(include);
        User user = await GetUsedUserIfUserIdIsNull(userId);
        return GetFilteredData(include.Compile()(user), attribute, orderBy, pageSize, pageNumber);
    }

    async Task<IndexViewModel<T>> GetUserModelsAsync<T, T2>(string? userId, string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, Expression<Func<User, IEnumerable<T>?>> include, Expression<Func<T, T2>> thenInclude)
        where T : NoteModel
    {
        dbAdminModels = dbAdminModels.Include(include)!.ThenInclude(thenInclude);
        User user = await GetUsedUserIfUserIdIsNull(userId);
        return GetFilteredData(include.Compile()(user), attribute, orderBy, pageSize, pageNumber);
    }

    public async Task<IndexViewModel<NoteItem>> GetUserNoteItemsAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
        => await GetUserModelsAsync(userId, attribute, orderBy, pageSize, pageNumber, u => u.NoteItems, n => n.Category);

    public async Task<IndexViewModel<Category>> GetUserCategoriesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
         => await GetUserModelsAsync(userId, attribute, orderBy, pageSize, pageNumber, u => u.Categories);
}