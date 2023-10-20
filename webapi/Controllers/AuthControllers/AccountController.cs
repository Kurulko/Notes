using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Notes.Interfaces.Maps.AuthMaps;
using Notes.Models.Database;
using Notes.ViewModels.Auth;
using Notes.ViewModels.Database.AdminModels;

namespace WebApi.Controllers.AuthControllers;

[AllowAnonymous]
public class AccountController : ApiController
{
    readonly IAccountMap accMap;
    public AccountController(IAccountMap accMap, ILogger<AccountController> logger) : base(logger)
        => (this.accMap) = (accMap);


    [AllowAnonymous]
    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register([FromBody] RegisterModel register)
        => await ReturnOkIfEverithingIsGood(async () => await accMap.RegisterUserAsync(register));

    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
        => await ReturnOkIfEverithingIsGood(async () => await accMap.LoginUserAsync(login));

    [HttpGet("Token")]
    public async Task<IActionResult> GetTokenAsync()
        => await ReturnOkIfEverithingIsGood((Func<Task<TokenModel?>>)((User.Identity?.IsAuthenticated ?? false) ? accMap.GetTokenAsync! : () => Task.FromResult(default(TokenModel))));


    [Authorize]
    [HttpPost(nameof(Logout))]
    public async Task<IActionResult> Logout()
        => await ReturnOkIfEverithingIsGood(accMap.LogoutUserAsync);
}