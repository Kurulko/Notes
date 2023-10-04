using Notes.Models.Base;
using Notes.Models.Base.NotesModels;

namespace Notes.Interfaces.Primaries.UserPrimaries;

public interface IUserPrimary<T, K1> : IBaseUserPrimary<T>, IUserRolesPrimary<T>, IUserPasswordPrimary<T>, IUsedUserPrimary<T>, IUserModelsPrimary<T, K1>
    where T : IUserBase
    where K1 : INoteItemBase
{
}