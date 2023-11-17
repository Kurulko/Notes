using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Interfaces.Services.AuthServices;
using Notes.ViewModels;
using Notes.Commons.Extensions;
using Notes.ViewModels.Auth;
using Notes.Commons;
using Notes.Models.Database.NotesModels;
using Notes.Interfaces.Maps.AuthMaps;
using Notes.ViewModels.Database.NotesModels;
using Notes.Interfaces.Maps.AdminMaps.UserMaps;
using Notes.ViewModels.Database.AdminModels;
using Notes.Models.Database.AdminModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace WebApi.Controllers.CRUDControllers;

public class UsersController : AdminDbModelsController<UserViewModel, string>
{
    readonly IUserMap userMap;
    readonly IJwtMap jwtMap;
    readonly SignInManager<User> signInManager;
    readonly IHttpContextAccessor httpContextAccessor;

    public UsersController(IUserMap userMap, IJwtMap jwtMap, ILogger<UsersController> logger, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor) : base(userMap, logger)
        => (this.userMap, this.jwtMap, this.signInManager, this.httpContextAccessor) = (userMap, jwtMap, signInManager, httpContextAccessor);

    #region User

    [AllowAnonymous]
    public override async Task<IActionResult> UpdateModelAsync(UserViewModel model)
        => await ReturnOkIfEverithingIsGood(async () =>
        {
            bool isItCurrentUser = await IsItCurrentUser(model.Id);

            await CheckAccessForUser(model.Id);
            await userMap.UpdateModelAsync(model);

            UserViewModel usedUser;
            if (isItCurrentUser)
            {
                usedUser = model;
                await signInManager.SignInAsync((User)model, isPersistent: false);
            }
            else
            {
                usedUser = (await GetUserByCurrentNameAsync())!;
            }

            string[] roles = (await userMap.GetRolesAsync(usedUser.Id)).ToArray();
            return jwtMap.GenerateJwtToken(usedUser, roles);
        });


    [AllowAnonymous]
    [HttpGet("userid-by-name/{userName}")]
    public async Task<string?> GetUserIdByUserNameAsync(string userName)
        => await userMap.GetUserIdByUserNameAsync(userName);

    [AllowAnonymous]
    public override async Task<UserViewModel?> GetModelByIdAsync(string key)
        => await CheckAccessForUser(key, () => base.GetModelByIdAsync(key));

    [AllowAnonymous]
    [HttpGet("current")]
    public virtual async Task<UserViewModel?> GetUserByClaimsAsync()
        => await CheckAccess(userMap.GetUsedUserAsync);


    [HttpGet("name/{userName}")]
    public virtual async Task<UserViewModel?> GetUserByNameAsync(string userName)
        => await userMap.GetUserByNameAsync(userName);

    #endregion

    #region UsedUser

    [AllowAnonymous]
    [HttpGet("used-username")]
    public async Task<string?> GetUsedUserNameAsync()
    => await CheckAccess(userMap.GetUsedUserNameAsync);

    [AllowAnonymous]
    [HttpGet("used-userid")]
    public async Task<string?> GetUsedUserIdAsync()
        => await CheckAccess(userMap.GetUsedUserIdAsync);

    [AllowAnonymous]
    [HttpGet("is-impersonating")]
    public async Task<bool> IsImpersonating()
        => await userMap.IsImpersonating();
    //=> await CheckAccess(userMap.IsImpersonating);

    [HttpGet("usedUser")]
    public virtual async Task<UserViewModel> GetUsedUserAsync()
        => await userMap.GetUsedUserAsync();

    [HttpPut("change-used-userId")]
    public async Task ChangeUsedUserIdAsync([FromBody] string usedUserId)
        => await ReturnOkIfEverithingIsGood(async () => await userMap.ChangeUsedUserIdAsync(usedUserId));


    [HttpDelete("drop-used-userId")]
    public async Task DropUsedUserIdAsync()
        => await ReturnOkIfEverithingIsGood(userMap.DropUsedUserIdAsync);

    #endregion

    const string pathToUserNoteItems = "user-noteitems";
    const string pathToUserCategories = "user-categories";
    const string pathToUnnecessaryUserId = "{userId?}";

    #region UserModels

    [AllowAnonymous]
    [HttpGet($"{pathToUserNoteItems}/{pathToUnnecessaryUserId}")]
    public virtual async Task<IndexViewModel<NoteItemViewModel>?> GetUserNoteItemsAsync([FromQuery] string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, string? userId = null)
        => await CheckAccess(() => userMap.GetUserNoteItemsAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber, userId));

    [AllowAnonymous]
    [HttpGet($"{pathToUserCategories}/{pathToUnnecessaryUserId}")]
    public virtual async Task<IndexViewModel<CategoryViewModel>?> GetUserCategoriesAsync([FromQuery] string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, string? userId = null)
        => await CheckAccess(() => userMap.GetUserCategoriesAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber, userId));
    
    #endregion


    #region Password

    [AllowAnonymous]
    [HttpGet("{userId}/password")]
    public async Task<bool> HasPassword(string userId)
        => await CheckAccessForUser(userId, () => userMap.HasUserPasswordAsync(userId));

    [AllowAnonymous]
    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword(ChangePassword model)
        => await ReturnOkIfEverithingIsGood(async () =>
        {
            CheckAccess();
            await userMap.ChangeUserPasswordAsync(model);
        });

    [HttpPost("password")]
    public async Task<IActionResult> CreatePassword(ModelWithUserId<string> m)
        => await ReturnOkIfEverithingIsGood(async () => await userMap.AddUserPasswordAsync(m));

    #endregion

    #region Roles

    [AllowAnonymous]
    [HttpGet("user-roles/{userId?}")]
    public async Task<IEnumerable<string>> GetRoles(string? userId)
        => await (userId is null ?
         CheckAccess(() => userMap.GetRolesAsync(userId)) :
         CheckAccessForUser(userId, () => userMap.GetRolesAsync(userId)));

    [HttpPost("{userId}/role")]
    public async Task<IActionResult> AddRole(string userId, [FromBody] string roleName)
        => await ReturnOkIfEverithingIsGood(async () => await userMap.AddRoleToUserAsync(new(userId, roleName)));

    [HttpDelete("{userId}/{roleName}")]
    public async Task<IActionResult> DeleteRole(string userId, string roleName)
        => await ReturnOkIfEverithingIsGood(async () => await userMap.DeleteRoleFromUserAsync(new(userId, roleName)));

    [HttpGet("users-by-role/{roleName}")]
    public async Task<IEnumerable<UserViewModel>> GetUsersByRoleAsync(string roleName)
        => await userMap.GetUsersByRoleAsync(roleName);

    #endregion

    async Task<bool> IsItCurrentUser(string userId)
    {
        string? currentUserId = await GetUserIdByCurrentNameAsync();
        return currentUserId == userId;
    }

    async Task<string?> GetUserIdByCurrentNameAsync()
        => await userMap.GetUserIdByUserNameAsync(UserName!);

    async Task<UserViewModel?> GetUserByCurrentNameAsync()
        =>  await userMap.GetUserByNameAsync(UserName!);

    string? UserName => UserIdentity?.Name;

    async Task CheckAccessForUser(string userId)
    {
        string? _userId = await GetUserIdByCurrentNameAsync();
        if (!(IsAdmin() || _userId == userId))
            AccessDenied();
    }
    async Task<T> CheckAccessForUser<T>(string userId, Func<Task<T>> actionAsync)
    {
        await CheckAccessForUser(userId);
        return await actionAsync();
    }

    bool IsAdmin()
        => User.IsInRole(Roles.Admin);

    void CheckAccess()
    {
        if (!UserIdentity?.IsAuthenticated ?? false)
            AccessDenied();
    }

    async Task<T> CheckAccess<T>(Func<Task<T>> actionAsync)
    {
        CheckAccess();
        return await actionAsync();
    }

    void AccessDenied()
        => throw new UnauthorizedAccessException("Access to this source is denied!");

    IIdentity? UserIdentity => httpContextAccessor.HttpContext?.User.Identity;
}