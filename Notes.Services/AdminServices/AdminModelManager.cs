using Notes.Interfaces.Repositories.AdminRepositories;
using Notes.Models.Database.AdminModels;

namespace Notes.Services.AdminServices;

public abstract class AdminModelManager<T> : DbModelManager<T, string> where T : IAdminModel
{
    protected AdminModelManager(IAdminModelRepository<T> adminModelRepository) : base(adminModelRepository)
    {
    }
}
