using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Notes.ViewModels;
using Notes.ViewModels.Auth;
using Notes.Commons;
using Notes.Models.Database.NotesModels;
using Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;
using Notes.Models.Database.AdminModels;

namespace Notes.Repositories.AdminRepositories.UserRepositories;

public class UserRepository : IUserRepository
{
    readonly IBaseUserRepository baseUserRepository;
    readonly IUserRolesRepository userRolesRepository;
    readonly IUserPasswordRepository userPasswordRepository;
    readonly IUsedUserRepository usedUserRepository;
    readonly IUserModelsRepository userModelsRepository;
    public UserRepository(IBaseUserRepository baseUserRepository, IUserRolesRepository userRolesRepository, IUserPasswordRepository userPasswordRepository, IUsedUserRepository usedUserRepository, IUserModelsRepository userModelsRepository)
    {
        this.baseUserRepository = baseUserRepository;
        this.userRolesRepository = userRolesRepository;
        this.userPasswordRepository = userPasswordRepository;
        this.usedUserRepository = usedUserRepository;
        this.userModelsRepository = userModelsRepository;
    }

    public async Task<User> AddModelAsync(User model)
        => await baseUserRepository.AddModelAsync(model);

    public async Task AddRoleToUserAsync(ModelWithUserId<string> model)
        => await userRolesRepository.AddRoleToUserAsync(model);

    public async Task AddUserPasswordAsync(ModelWithUserId<string> model)
        => await userPasswordRepository.AddUserPasswordAsync(model);

    public async Task ChangeUsedUserIdAsync(string usedUserId)
        => await usedUserRepository.ChangeUsedUserIdAsync(usedUserId);

    public async Task ChangeUserPasswordAsync(ChangePassword model)
        => await userPasswordRepository.ChangeUserPasswordAsync(model);

    public async Task DeleteModelAsync(string key)
        => await baseUserRepository.DeleteModelAsync(key);

    public async Task DeleteRoleFromUserAsync(ModelWithUserId<string> model)
        => await userRolesRepository.DeleteRoleFromUserAsync(model);

    public async Task DropUsedUserIdAsync()
        => await usedUserRepository.DropUsedUserIdAsync();

    public async Task<IEnumerable<User>> GetAllModelsAsync()
        => await baseUserRepository.GetAllModelsAsync();

    public async Task<User?> GetModelByIdAsync(string key)
        => await baseUserRepository.GetModelByIdAsync(key);

    public async Task<IEnumerable<string>> GetRolesAsync(string? userId)
        => await userRolesRepository.GetRolesAsync(userId);

    public async Task<User> GetUsedUserAsync()
        => await usedUserRepository.GetUsedUserAsync();

    public async Task<string> GetUsedUserIdAsync()
        => await usedUserRepository.GetUsedUserIdAsync();

    public async Task<string> GetUsedUserNameAsync()
        => await usedUserRepository.GetUsedUserNameAsync();

    public async Task<User?> GetUserByClaimsAsync(ClaimsPrincipal claims)
        => await baseUserRepository.GetUserByClaimsAsync(claims);

    public async Task<User?> GetUserByNameAsync(string name)
        => await baseUserRepository.GetUserByNameAsync(name);

    public async Task<string?> GetUserIdByUserNameAsync(string userName)
        => await baseUserRepository.GetUserIdByUserNameAsync(userName);

    public async Task<bool> HasUserPasswordAsync(string userId)
        => await userPasswordRepository.HasUserPasswordAsync(userId);

    public async Task<bool> IsImpersonating()
        => await usedUserRepository.IsImpersonating();

    public async Task UpdateModelAsync(User model)
        => await baseUserRepository.UpdateModelAsync(model);

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(string roleName)
        => await userRolesRepository.GetUsersByRoleAsync(roleName);

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
        => await baseUserRepository.CreateUserAsync(user, password);

    public async Task<IndexViewModel<NoteItem>> GetUserNoteItemsAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
        => await userModelsRepository.GetUserNoteItemsAsync(attribute, orderBy, pageSize, pageNumber, userId);

    public async Task<IndexViewModel<Category>> GetUserCategoriesAsync(string? attribute, OrderBy? orderBy, int? pageSize, int? pageNumber, string? userId = null)
        => await userModelsRepository.GetUserCategoriesAsync(attribute, orderBy, pageSize, pageNumber, userId);
}