using SystemNotificationPersonal.ConfigServerConsole.Interfaces;
using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.ConfigServerConsole.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddAsync(UsersEntity user, CancellationToken token)
        {
            return await _repository.AddAsync(user, token);
        }
        public async Task<bool> CheckAsync(string login, CancellationToken token)
        {
            return await _repository.CheckAsync(login, token);
        }
        public async Task<int> DeleteAsync(string login, CancellationToken token)
        {
            return await _repository.DeleteAsync(login, token);
        }
        public async Task<int> UpdatePasswordAsync(UsersEntity user, CancellationToken token)
        {
            return await _repository.UpdatePasswordAsync(user, token);
        }
        public async Task<int> UpdateRoleAsync(UsersEntity user, CancellationToken token)
        {
            return await _repository.UpdateRoleAsync(user, token);
        }
        public async Task<bool> VerifyAsync(UsersEntity user, CancellationToken token)
        {
            return await _repository.VerifyAsync(user, token);
        }
    }
}
