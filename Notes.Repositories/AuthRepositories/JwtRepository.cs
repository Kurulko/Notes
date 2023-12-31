﻿using Microsoft.IdentityModel.Tokens;
using Notes.Commons.Settings;
using Notes.Interfaces.Repositories.AuthRepositories;
using Notes.Models.Database.AdminModels;
using Notes.ViewModels.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Notes.Repositories.AuthRepositories;

public class JwtRepository : IJwtRepository
{
    readonly JwtSettings jwtSettings;
    public JwtRepository(JwtSettings jwtSettings)
        => this.jwtSettings = jwtSettings;

    public TokenModel GenerateJwtToken(User user, params string[] roles)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

        var rolesClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName!),
            }.Union(rolesClaims)),
            Expires = DateTime.UtcNow.AddDays(jwtSettings.ExpirationDays),
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return new TokenModel() { Token = tokenHandler.WriteToken(token) , ExpirationDays = jwtSettings.ExpirationDays , Roles = roles };
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        var validationParameters = (TokenValidationParameters)jwtSettings;

        try
        {
            return tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
        }
        catch
        {
            return null!;
        }
    }
}