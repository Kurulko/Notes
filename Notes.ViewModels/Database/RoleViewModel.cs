using Notes.Models.Base;
using Notes.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.ViewModels.Database;

public class RoleViewModel : IDbViewModel, IRoleBase
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
