using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Notes.Commons;
using Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;
using Notes.Interfaces.Services.AdminServices.UserServices;
using Notes.Models.Database.AdminModels;
using Notes.Models.Database.NotesModels;
using Notes.ViewModels;
using Notes.ViewModels.Auth;

namespace Notes.Services.AdminServices.UserServices;

public class UserManager : AdminModelManager<User>, IUserService
{
    readonly IUserRepository userRepository;
    public UserManager(IUserRepository userRepository) : base(userRepository)
        => this.userRepository = userRepository;

    public async Task AddRoleToUserAsync(ModelWithUserId<string> model)
        => await userRepository.AddRoleToUserAsync(model);

    public async Task AddUserPasswordAsync(ModelWithUserId<string> model)
        => await userRepository.AddUserPasswordAsync(model);

    public async Task ChangeUsedUserIdAsync(string usedUserId)
        => await userRepository.ChangeUsedUserIdAsync(usedUserId);

    public async Task ChangeUserPasswordAsync(ChangePassword model)
        => await userRepository.ChangeUserPasswordAsync(model);

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
        => await userRepository.CreateUserAsync(user, password);

    public async Task DeleteRoleFromUserAsync(ModelWithUserId<string> model)
        => await userRepository.DeleteRoleFromUserAsync(model);

    public async Task DropUsedUserIdAsync()
        => await userRepository.DropUsedUserIdAsync();

    public async Task<string?> GetCurrentUserNameAsync()
        => await userRepository.GetCurrentUserNameAsync();

    public async Task<IEnumerable<string>> GetRolesAsync(string? userId)
        => await userRepository.GetRolesAsync(userId);

    public async Task<User> GetUsedUserAsync()
        => await userRepository.GetUsedUserAsync();

    public async Task<string> GetUsedUserIdAsync()
        => await userRepository.GetUsedUserIdAsync();

    public async Task<User?> GetUserByClaimsAsync(ClaimsPrincipal claims)
        => await userRepository.GetUserByClaimsAsync(claims);

    public async Task<User?> GetUserByNameAsync(string name)
        => await userRepository.GetUserByNameAsync(name);

    public async Task<string?> GetUserIdByUserNameAsync(string userName)
        => await userRepository.GetUserIdByUserNameAsync(userName);

    public async Task<IndexViewModel<NoteItem>> GetUserNoteItemsAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
        => await userRepository.GetUserNoteItemsAsync(attribute, orderBy, pageSize, pageNumber, userId);

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName)
        => await userRepository.GetUsersByRoleAsync(roleName);

    public async Task<bool> HasUserPasswordAsync(string userId)
        => await userRepository.HasUserPasswordAsync(userId);

    public async Task<bool> IsImpersonating()
        => await userRepository.IsImpersonating();
}