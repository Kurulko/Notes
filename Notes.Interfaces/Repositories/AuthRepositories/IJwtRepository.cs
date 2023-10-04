using Notes.Interfaces.Primaries.AuthPrimaries;
using Notes.Models.Database;
using System.Security.Claims;

namespace Notes.Interfaces.Repositories.AuthRepositories;

public interface IJwtRepository : IJwtPrimary<User>
{
}