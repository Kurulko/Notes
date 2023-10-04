using Notes.Commons;
using Notes.ViewModels;
using Notes.Interfaces.Repositories;
using Notes.Models.Database;
using Notes.Interfaces.Primary;

namespace Notes.Interfaces.Services;

public interface IDbModelService<T, K> : IDbModelPrimary<T, K> where T : IEntityBase
{
}