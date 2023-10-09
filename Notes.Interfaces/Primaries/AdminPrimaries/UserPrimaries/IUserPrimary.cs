using Notes.Models.Base.AdminModels;
using Notes.Models.Base.NotesModels;

namespace Notes.Interfaces.Primaries.AdminPrimaries.UserPrimaries;

public interface IUserPrimary<T, K1> : IBaseUserPrimary<T>, IUserRolesPrimary<T>, IUserPasswordPrimary<T>, IUsedUserPrimary<T>, IUserModelsPrimary<T, K1>
    where T : IUserBase
    where K1 : INoteItemBase
{
}