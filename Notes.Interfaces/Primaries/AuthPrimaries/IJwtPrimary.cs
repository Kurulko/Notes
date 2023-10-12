using Notes.Models.Base.AdminModels;
using Notes.Models.Database;
using Notes.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces.Primaries.AuthPrimaries;

public interface IJwtPrimary<T> where T : IUserBase
{
    TokenModel GenerateJwtToken(T user, params string[] roles);
    ClaimsPrincipal GetPrincipalFromToken(string token);
}