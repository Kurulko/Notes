using Microsoft.AspNetCore.Identity;
using Notes.Commons;
using Notes.Interfaces.Maps.UserMaps;
using Notes.Interfaces.Services.UserServices;
using Notes.Models.Database;
using Notes.Models.Database.NotesModels;
using Notes.ViewModels;
using Notes.ViewModels.Account;
using Notes.ViewModels.Database;
using Notes.ViewModels.Database.NotesModels;
using System.Security.Claims;

namespace Notes.Maps.UserMaps;

public class UserMap : DbModelMap<UserViewModel, User, string>, IUserMap
{
    readonly IUserService userService;
    public UserMap(IUserService userService) : base(userService)
        => this.userService = userService;

    public async Task AddRoleToUserAsync(ModelWithUserId<string> model)
        => await userService.AddRoleToUserAsync(model);

    public async Task AddUserPasswordAsync(ModelWithUserId<string> model)
        => await userService.AddUserPasswordAsync(model);

    public async Task ChangeUsedUserIdAsync(string usedUserId)
        => await userService.ChangeUsedUserIdAsync(usedUserId);

    public async Task ChangeUserPasswordAsync(ChangePassword model)
        => await userService.ChangeUserPasswordAsync(model);

    public async Task<UserViewModel> CreateUserAsync()
        => ConvertToViewModel(await userService.CreateUserAsync());

    public async Task<IdentityResult> CreateUserAsync(UserViewModel user, string password)
        => await userService.CreateUserAsync(ConvertFromViewModel(user), password);

    public async Task DeleteRoleFromUserAsync(ModelWithUserId<string> model)
        => await userService.DeleteRoleFromUserAsync(model);

    public async Task DropUsedUserIdAsync()
        => await userService.DropUsedUserIdAsync();

    public async Task<string?> GetCurrentUserNameAsync()
        => await userService.GetCurrentUserNameAsync();

    public async Task<IEnumerable<string>> GetRolesAsync(string? userId)
        => await userService.GetRolesAsync(userId);

    public async Task<UserViewModel> GetUsedUserAsync()
        => ConvertToViewModel(await userService.GetUsedUserAsync());

    public async Task<string> GetUsedUserIdAsync()
        => await userService.GetUsedUserIdAsync();

    public async Task<UserViewModel?> GetUserByClaimsAsync(ClaimsPrincipal claims)
        => ConvertToNullableViewModel(await userService.GetUserByClaimsAsync(claims));

    public async Task<UserViewModel?> GetUserByNameAsync(string name)
        => ConvertToNullableViewModel(await userService.GetUserByNameAsync(name));

    public async Task<string?> GetUserIdByUserNameAsync(string userName)
        => await userService.GetUserIdByUserNameAsync(userName);

    IndexViewModel<TViewModel> FromUserModelsToUserViewModels<TModel, TViewModel>(IndexViewModel<TModel> indexUserModels, Func<TModel, TViewModel> convert)
        where TModel : NoteModel
        where TViewModel : NoteViewModel
    {
        var userViewModels = indexUserModels.Models.Select(userModel => convert(userModel));
        return new IndexViewModel<TViewModel>(userViewModels, indexUserModels.PageViewModel);
    }

    public async Task<IndexViewModel<NoteItemViewModel>> GetUserNoteItemsAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
    {
        var indexNoteItems = await userService.GetUserNoteItemsAsync(attribute, orderBy, pageSize, pageNumber, userId);
        return FromUserModelsToUserViewModels(indexNoteItems, noteItem => (NoteItemViewModel)noteItem);
    }


    public async Task<IEnumerable<UserViewModel>> GetUsersByRoleAsync(string roleName)
        => ConvertToViewModels(await userService.GetUsersByRoleAsync(roleName));

    public async Task<bool> HasUserPasswordAsync(string userId)
        => await userService.HasUserPasswordAsync(userId);

    public async Task<bool> IsImpersonating()
        => await userService.IsImpersonating();

    protected override User ConvertFromViewModel(UserViewModel viewModel)
        => (User)viewModel;
    protected override UserViewModel ConvertToViewModel(User model)
        => (UserViewModel)model;
}