using Microsoft.IdentityModel.Tokens;
using Notes.Commons;
using Notes.Interfaces.Repositories.AuthRepositories;
using Notes.Interfaces.Services.AuthServices;
using Notes.Models.Database;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Notes.Services.AuthServices;

public class JwtManager : IJwtService
{
    readonly IJwtRepository jwtRepository;
    public JwtManager(IJwtRepository jwtRepository)
        => this.jwtRepository = jwtRepository;

    public (string token, int expirationDays) GenerateJwtToken(User user, IEnumerable<string> roles)
        => jwtRepository.GenerateJwtToken(user, roles);

    public ClaimsPrincipal GetPrincipalFromToken(string token)
        => jwtRepository.GetPrincipalFromToken(token);
}