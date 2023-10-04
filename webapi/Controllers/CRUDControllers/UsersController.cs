using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Interfaces.Maps.UserMaps;
using Notes.Interfaces.Services.AuthServices;
using Notes.ViewModels;
using Notes.ViewModels.Database;
using Notes.Commons.Extensions;
using Notes.ViewModels.Account;
using Notes.Commons;
using Notes.Models.Database.NotesModels;
using Notes.Interfaces.Maps.AuthMaps;
using Notes.ViewModels.Database.NotesModels;

namespace WebApi.Controllers.CRUDControllers;

public class UsersController : AdminDbModelsController<UserViewModel, string>
{
    readonly IUserMap userMap;
    readonly IJwtMap jwtMap;
    public UsersController(IUserMap userMap, IJwtMap jwtMap, ILogger<UsersController> logger) : base(userMap, logger)
        => (this.userMap, this.jwtMap) = (userMap, jwtMap);

    #region User

    [AllowAnonymous]
    public override async Task<IActionResult> UpdateModelAsync(UserViewModel model)
        => await ReturnOkIfEverithingIsGood(async () =>
        {
            string userId = model.Id;
            await CheckAccessForUser(userId);
            await userMap.UpdateModelAsync(model);
            IEnumerable<string> roles = await userMap.GetRolesAsync(userId);
            var tokenInfo = jwtMap.GenerateJwtToken(model, roles);
            return new { tokenInfo.token, tokenInfo.expirationDays };
        });

    [HttpGet("user-by-default")]
    public async Task<UserViewModel> CreateUserAsync()
        => await userMap.CreateUserAsync();

    [AllowAnonymous]
    [HttpGet("userid-by-name/{userName}")]
    public async Task<string?> GetUserIdByUserNameAsync(string userName)
        => await userMap.GetUserIdByUserNameAsync(userName);

    [AllowAnonymous]
    [HttpGet("current-username")]
    public async Task<string?> GetCurrentUserNameAsync()
        => await CheckAccess(userMap.GetCurrentUserNameAsync);

    [AllowAnonymous]
    [HttpGet("is-impersonating")]
    public async Task<bool> IsImpersonating()
        => await CheckAccess(userMap.IsImpersonating);

    [AllowAnonymous]
    public override async Task<UserViewModel?> GetModelByIdAsync(string key)
        => await CheckAccessForUser(key, () => base.GetModelByIdAsync(key));

    [AllowAnonymous]
    [HttpGet("current")]
    public virtual async Task<UserViewModel?> GetUserByClaimsAsync()
        => await CheckAccess(() => userMap.GetUsedUserAsync());


    [HttpGet("name")]
    public virtual async Task<UserViewModel?> GetUserByNameAsync(string name)
        => await userMap.GetUserByNameAsync(name);

    #endregion

    #region UsedUser

    [HttpGet("usedUser")]
    public virtual async Task<UserViewModel> GetUsedUserAsync()
        => await userMap.GetUsedUserAsync();

    [HttpPut("change-used-userId")]
    public async Task ChangeUsedUserIdAsync([FromForm] string usedUserId)
        => await ReturnOkIfEverithingIsGood(async () => await userMap.ChangeUsedUserIdAsync(usedUserId));

    [HttpDelete("drop-used-userId")]
    public async Task DropUsedUserIdAsync()
        => await ReturnOkIfEverithingIsGood(userMap.DropUsedUserIdAsync);

    #endregion

    const string pathToUserNoteItems = "user-noteitems";
    const string pathToUnnecessaryUserId = "{userId?}";

    #region UserModels

    [AllowAnonymous]
    [HttpGet($"{pathToUserNoteItems}/{pathToUnnecessaryUserId}")]
    public virtual async Task<IndexViewModel<NoteItemViewModel>?> GetUserNoteItemsAsync([FromQuery] string? attribute, [FromQuery] string? orderBy, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, string? userId = null)
        => await CheckAccess(() => userMap.GetUserNoteItemsAsync(attribute, orderBy?.ParseToOrderBy(), pageSize, pageNumber, userId));
    
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

    async Task CheckAccessForUser(string userId)
    {
        string? userName = User.Identity?.Name;
        string? _userId = await userMap.GetUserIdByUserNameAsync(userName!);
        if (!(User.IsInRole(Roles.Admin) || _userId == userId))
            AccessDenied();
    }
    async Task<T> CheckAccessForUser<T>(string userId, Func<Task<T>> actionAsync)
    {
        await CheckAccessForUser(userId);
        return await actionAsync();
    }

    void CheckAccess()
    {
        if (!User.Identity?.IsAuthenticated ?? false)
            AccessDenied();
    }
    async Task<T> CheckAccess<T>(Func<Task<T>> actionAsync)
    {
        CheckAccess();
        return await actionAsync();
    }

    void AccessDenied()
        => throw new UnauthorizedAccessException("Access to this source is denied!");
}