using Notes.Commons;
using Notes.ViewModels;
using Notes.Commons.Extensions;
using Notes.Models.Database;
using Notes.Interfaces.Primary;

namespace Notes.Interfaces.Repositories;

public interface IDbModelRepository<T, K> : IDbModelPrimary<T, K> where T : IEntityBase
{
}