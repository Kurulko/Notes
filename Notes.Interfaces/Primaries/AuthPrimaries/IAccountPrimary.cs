using Notes.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces.Primaries.AuthPrimaries;

public interface IAccountPrimary
{
    Task<IEnumerable<string>> LoginUserAsync(LoginModel model);
    Task<IEnumerable<string>> RegisterUserAsync(RegisterModel model);
    Task LogoutUserAsync();
}