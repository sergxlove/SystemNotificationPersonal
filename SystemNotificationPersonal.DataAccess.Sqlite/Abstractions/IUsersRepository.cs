using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.DataAccess.Sqlite.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> AddAsync(UsersEntity user, CancellationToken token);
        Task<bool> CheckAsync(string login, CancellationToken token);
        Task<int> DeleteAsync(string login, CancellationToken token);
        Task<int> UpdatePasswordAsync(UsersEntity user, CancellationToken token);
        Task<int> UpdateRoleAsync(UsersEntity user, CancellationToken token);
        Task<bool> VerifyAsync(UsersEntity user, CancellationToken token);
    }
}