using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.ConfigServerConsole.Interfaces
{
    public interface IUsersService
    {
        Task<Guid> AddAsync(UsersEntity user);
        Task<int> DeleteAsync(string login);
        Task<int> UpdateAsync(UsersEntity user);
        Task<bool> VerifyAsync(UsersEntity user);
        Task<bool> CheckAsync(string login);
    }
}
