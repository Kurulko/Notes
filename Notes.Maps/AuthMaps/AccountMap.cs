using Microsoft.AspNetCore.Identity;
using Notes.ViewModels.Account;
using Notes.Interfaces.Maps.AuthMaps;
using Notes.Interfaces.Services.AuthServices;

namespace Notes.Maps.AuthMaps;

public class AccountMap : IAccountMap
{
    readonly IAccountService accountService;
    public AccountMap(IAccountService accountService)
        => (this.accountService) = (accountService);

    public async Task<IEnumerable<string>> LoginUserAsync(LoginModel model)
        => await accountService.LoginUserAsync(model);

    public async Task LogoutUserAsync()
        => await accountService.LogoutUserAsync();

    public async Task<IEnumerable<string>> RegisterUserAsync(RegisterModel model)
        => await accountService.RegisterUserAsync(model);
}