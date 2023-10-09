using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notes.Interfaces.Maps.AuthMaps;
using Notes.Models.Database;
using Notes.ViewModels.Account;
using Notes.ViewModels.Database.AdminModels;

namespace WebApi.Controllers.AuthControllers;

[AllowAnonymous]
public class AccountController : ApiController
{
    readonly IAccountMap accMap;
    readonly IJwtMap jwtMap;
    public AccountController(IAccountMap accMap, IJwtMap jwtMap, ILogger<AccountController> logger) : base(logger)
        => (this.accMap, this.jwtMap) = (accMap, jwtMap);


    [AllowAnonymous]
    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register([FromBody] RegisterModel register)
        => await ReturnOkTokenIfEverithingIsGood(async () => await accMap.RegisterUserAsync(register), register);


    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
        => await ReturnOkTokenIfEverithingIsGood(async () => await accMap.LoginUserAsync(login), login);


    [Authorize]
    [HttpPost(nameof(Logout))]
    public async Task<IActionResult> Logout()
        => await ReturnOkIfEverithingIsGood(accMap.LogoutUserAsync);

    async Task<IActionResult> ReturnOkTokenIfEverithingIsGood(Func<Task<IEnumerable<string>>> action, AccountModel accountModel)
        => await ReturnOkIfEverithingIsGood(async () =>
        {
            IEnumerable<string> roles = await action();
            var tokenInfo = jwtMap.GenerateJwtToken((UserViewModel)accountModel, roles);
            return new { tokenInfo.token, tokenInfo.expirationDays };
        });
}