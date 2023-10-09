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
    public UserModelsRepository(UserManager<User> userManager, NotesContext db, IHttpContextAccessor httpContextAccessor) : base(userManager, db, httpContextAccessor)
    { }


    IndexViewModel<T> GetFilteredData<T>(IEnumerable<T> models, string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber)
        where T : NoteModel
    {
        int countOfAllModels = models.Count();

        attribute = attribute ?? nameof(NoteModel.Id);
        orderBy = orderBy ?? OrderBy.Ascending;
        pageNumber = pageNumber ?? 1;
        pageSize = pageSize ?? countOfAllModels;

        return models.OrderBy(attribute, orderBy.Value).Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value).ToIndexViewModel(countOfAllModels, pageSize, pageNumber);
    }

    async Task<User> GetUsedUserIfUserIdIsNull(string? userId)
        => await (userId is null ? GetUsedUserAsync() : GetUserByIdAsync(userId));

    async Task<IndexViewModel<T>> GetUserModelsByLanguageAsync<T>(Expression<Func<User, IEnumerable<T>>> include, string? userId, string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber)
        where T : NoteModel
    {
        userManager.Users.Include(include);
        //getModels = getModels.Include(include);
        User user = await GetUsedUserIfUserIdIsNull(userId);
        return GetFilteredData(include.Compile()(user), attribute, orderBy, pageSize, pageNumber);
    }

    public async Task<IndexViewModel<NoteItem>> GetUserNoteItemsAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
       => await GetUserModelsByLanguageAsync(u => u.NoteItems!, userId, attribute, orderBy, pageSize, pageNumber);
}