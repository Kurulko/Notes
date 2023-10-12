using Notes.Models.Base.AdminModels;
using Notes.Models.Database.AdminModels;
using Notes.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.ViewModels.Database.AdminModels;

public class UserViewModel : IAdminViewModel, IUserBase
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string? UsedUserId { get; set; }
    public string? Email { get; set; }
    public DateTime Registered { get; set; }

    public static explicit operator User(UserViewModel userViewModel)
        => new()
        {
            Id = userViewModel.Id,
            UsedUserId = userViewModel.UsedUserId,
            Email = userViewModel.Email,
            UserName = userViewModel.UserName,
            Registered = userViewModel.Registered,
        };

    public static explicit operator UserViewModel(User user)
        => new()
        {
            Id = user.Id,
            UsedUserId = user.UsedUserId,
            Email = user.Email,
            UserName = user.UserName,
            Registered = user.Registered,
        };
}
