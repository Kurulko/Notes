using Notes.Models.Base.AdminModels;
using Notes.Models.Database.AdminModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.ViewModels.Database.AdminModels;

public class RoleViewModel : IAdminViewModel, IRoleBase
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;

    public static explicit operator Role(RoleViewModel roleViewModel)
        => new()
        {
            Id = roleViewModel.Id,
            Name = roleViewModel.Name,
        };

    public static explicit operator RoleViewModel(Role role)
        => new()
        {
            Id = role.Id,
            Name = role.Name,
        };
}
