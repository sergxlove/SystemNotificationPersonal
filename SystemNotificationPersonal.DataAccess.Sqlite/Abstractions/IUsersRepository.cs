using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.DataAccess.Sqlite.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> AddAsync(UsersEntity user);
        Task<bool> CheckAsync(string login);
        Task<int> DeleteAsync(string login);
        Task<int> UpdateAsync(UsersEntity user);
        Task<bool> VerifyAsync(UsersEntity user);
    }
}