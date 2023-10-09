using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Notes.Commons.Settings;
using System.Text;
using Notes.Interfaces.Repositories.AuthRepositories;
using Notes.Repositories.AuthRepositories;
using Notes.Services.AuthServices;
using Notes.Interfaces.Services.AuthServices;
using Notes.Interfaces.Maps.AuthMaps;
using Notes.Maps.AuthMaps;
using Notes.Interfaces.Repositories.NotesRepositories;
using Notes.Repositories.NotesRepositories;
using Notes.Services.NotesServices;
using Notes.Interfaces.Services.NotesServices;
using Notes.Interfaces.Maps.NotesMaps;
using Notes.Maps.NotesMaps;
using Notes.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Notes.Models.Database;
using Notes.Interfaces.Repositories;
using Notes.Interfaces.Services;
using Notes.Interfaces.Maps;
using Notes.ViewModels.Database;
using Notes.Models.Database.NotesModels;
using Notes.ViewModels.Database.NotesModels;
using Notes.ViewModels.Database.AdminModels;
using Notes.Models.Database.AdminModels;
using Notes.Interfaces.Repositories.AdminRepositories.RoleRepositories;
using Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;
using Notes.Interfaces.Services.AdminServices.UserServices;
using Notes.Interfaces.Services.AdminServices.RoleServices;
using Notes.Interfaces.Maps.AdminMaps.UserMaps;
using Notes.Interfaces.Maps.AdminMaps.RoleMaps;
using Notes.Maps.AdminMaps.UserMaps;
using Notes.Maps.AdminMaps.RoleMaps;
using Notes.Repositories.AdminRepositories.RoleRepositories;
using Notes.Repositories.AdminRepositories.UserRepositories;
using Notes.Services.AdminServices.UserServices;
using Notes.Services.AdminServices.RoleServices;

namespace WebApi.Providers;

public static class ServiceProviders
{
    public static void AddMSSQLServer(this IServiceCollection services, IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<NotesContext>(opts =>
        {
            opts.UseSqlServer(connection);
            opts.EnableSensitiveDataLogging();
        });
    }

    public static void AddIdenityModels(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<NotesContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
        services.AddSingleton(jwtSettings);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
            options.TokenValidationParameters = (TokenValidationParameters)jwtSettings
        );

        string[] originSettings = configuration.GetSection("OriginSettings").Get<string[]>()!;

        services.AddCors(options =>
            options.AddDefaultPolicy(builder =>
                builder.WithOrigins(originSettings)
                       .AllowAnyHeader()
                       .AllowAnyMethod()
        ));
    }

    public static void AddUserServices(this IServiceCollection services)
    {
        services.AddScoped<IBaseUserRepository, BaseUserRepository>();
        services.AddScoped<IUserRolesRepository, UserRolesRepository>();
        services.AddScoped<IUserPasswordRepository, UserPasswordRepository>();
        services.AddScoped<IUsedUserRepository, UsedUserRepository>();
        services.AddScoped<IUserModelsRepository, UserModelsRepository>();

        services.AddServices<IUserRepository, UserRepository, 
            IUserService, UserManager, IUserMap, UserMap, User, UserViewModel, string>();
    }

    public static void AddAccountServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtRepository, JwtRepository>()
            .AddScoped<IJwtService, JwtManager>()
            .AddScoped<IJwtMap, JwtMap>();

        services.AddServices<IRoleRepository, RoleRepository,
            IRoleService, RoleManager, IRoleMap, RoleMap, Role, RoleViewModel, string>();

        services.AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<IAccountService, AccountManager>()
            .AddScoped<IAccountMap, AccountMap>();
    }

    public static void AddNotesModelServices(this IServiceCollection services)
    {
        services.AddDbModelServices<ICategoryRepository, CategoryRepository,
            ICategoryService, CategoryManager, ICategoryMap, CategoryMap, Category, CategoryViewModel>();

        services.AddDbModelServices<INoteItemRepository, NoteItemRepository,
            INoteItemService, NoteItemManager, INoteItemMap, NoteItemMap, NoteItem, NoteItemViewModel>();
    }

    static void AddDbModelServices<R1, R2, S1, S2, M1, M2, T1, T2>(this IServiceCollection services) 
        where R1 : class, INoteModelRepository<T1>
        where R2 : class, R1
        where S1 : class, INoteModelService<T1>
        where S2 : class, S1
        where M1 : class, INoteModelMap<T2>
        where M2 : class, M1
        where T1 : NoteModel
        where T2 : NoteViewModel
    {
        services.AddServices<R1, R2, S1, S2, M1, M2, T1, T2, long>();
    }

    static void AddServices<R1, R2, S1, S2, M1, M2, T1, T2, K>(this IServiceCollection services) 
        where R1 : class, IDbModelRepository<T1, K>
        where R2 : class, R1
        where S1 : class, IDbModelService<T1, K>
        where S2 : class, S1
        where M1 : class, IDbModelMap<T2, K>
        where M2 : class, M1
        where T1 : IEntityBase
        where T2 : IDbViewModel
    {
        services.AddScoped<R1, R2>()
            .AddScoped<S1, S2>()
            .AddScoped<M1, M2>();
    }
}