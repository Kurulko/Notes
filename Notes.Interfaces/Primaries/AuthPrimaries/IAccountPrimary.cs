using Notes.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces.Primaries.AuthPrimaries;

public interface IAccountPrimary
{
    Task<TokenModel> LoginUserAsync(LoginModel model);
    Task<TokenModel> RegisterUserAsync(RegisterModel model);
    Task<TokenModel> GetTokenAsync();
    Task LogoutUserAsync();
}