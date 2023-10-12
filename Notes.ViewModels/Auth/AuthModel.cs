using Notes.Models.Database.AdminModels;
using Notes.ViewModels.Database.AdminModels;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Notes.ViewModels.Auth;

public class AuthModel
{
    [Required(ErrorMessage = "Enter your name!")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Enter your password!")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "{0} must be at least {1} characters long")]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember me?")]
    [JsonPropertyName("rememberme")]
    public bool RememberMe { get; set; }

    public static explicit operator User(AuthModel accountModel)
        => new() { UserName = accountModel.Name };

    public static explicit operator UserViewModel(AuthModel accountModel)
        => new() { UserName = accountModel.Name };
}