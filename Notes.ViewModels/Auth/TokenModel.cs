using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.ViewModels.Auth;

public class TokenModel
{
    public string Token { get; set; } = null!;
    public int ExpirationDays { get; set; }
    public string[] Roles { get; set; } = null!;
}
