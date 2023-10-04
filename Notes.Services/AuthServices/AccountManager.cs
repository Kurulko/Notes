using Microsoft.AspNetCore.Identity;
using Notes.ViewModels.Account;
using Notes.Interfaces.Services.AuthServices;
using Notes.Interfaces.Repositories.AuthRepositories;

namespace Notes.Services.AuthServices;

public class AccountManager : IAccountService
{
    readonly IAccountRepository accountRepository;
    public AccountManager(IAccountRepository accountRepository)
        => (this.accountRepository) = (accountRepository);

    public async Task<IEnumerable<string>> LoginUserAsync(LoginModel model)
        => await accountRepository.LoginUserAsync(model);

    public async Task LogoutUserAsync()
        => await accountRepository.LogoutUserAsync();

    public async Task<IEnumerable<string>> RegisterUserAsync(RegisterModel model)
        => await accountRepository.RegisterUserAsync(model);
}