using Notes.Interfaces.Primaries.AuthPrimaries;
using Notes.Models.Database.AdminModels;
using System.Security.Claims;

namespace Notes.Interfaces.Services.AuthServices;

public interface IJwtService : IJwtPrimary<User>
{
}