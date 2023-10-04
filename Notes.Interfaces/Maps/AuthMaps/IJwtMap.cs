using Notes.Interfaces.Primaries.AuthPrimaries;
using Notes.ViewModels.Database;
using System.Security.Claims;

namespace Notes.Interfaces.Maps.AuthMaps;

public interface IJwtMap : IJwtPrimary<UserViewModel>
{
}