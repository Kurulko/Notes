using Microsoft.IdentityModel.Tokens;
using Notes.Commons;
using Notes.Interfaces.Maps.AuthMaps;
using Notes.Interfaces.Services.AuthServices;
using Notes.Models.Database.AdminModels;
using Notes.ViewModels.Auth;
using Notes.ViewModels.Database.AdminModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Notes.Maps.AuthMaps;

public class JwtMap : IJwtMap
{
    readonly IJwtService jwtService;
    public JwtMap(IJwtService jwtService)
        => this.jwtService = jwtService;

    public TokenModel GenerateJwtToken(UserViewModel user, params string[] roles)
        => jwtService.GenerateJwtToken((User)user, roles);

    public ClaimsPrincipal GetPrincipalFromToken(string token)
        => jwtService.GetPrincipalFromToken(token);
}