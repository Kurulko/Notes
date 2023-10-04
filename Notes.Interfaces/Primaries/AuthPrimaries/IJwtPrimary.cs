using Notes.Models.Base;
using Notes.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces.Primaries.AuthPrimaries;

public interface IJwtPrimary<T> where T : IUserBase
{
    (string token, int expirationDays) GenerateJwtToken(T user, IEnumerable<string> roles);
    ClaimsPrincipal GetPrincipalFromToken(string token);
}