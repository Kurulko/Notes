using Notes.Interfaces.Repositories.AdminRepositories.RoleRepositories;
using Notes.Interfaces.Repositories.AdminRepositories.UserRepositories;
using WebApi.Initializers;

namespace Notes.WebApi.Providers;

public static class InitializerProvider
{
    public static async Task InitializeDataAsync(this WebApplication app, ConfigurationManager config)
    {
        using (IServiceScope serviceScope = app.Services.CreateScope())
        {
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var roleRepository = serviceProvider.GetRequiredService<IRoleRepository>();
            await RolesInitializer.InitializeAsync(roleRepository);

            string adminName = config.GetValue<string>("Admin:Name")!;
            string adminPassword = config.GetValue<string>("Admin:Password")!;
            var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            await UsersInitializer.AdminInitializeAsync(userRepository, adminName, adminPassword);
        }
    }
}
