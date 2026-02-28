using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;
using SystemNotificationPersonal.Server.Abstractions;

namespace SystemNotificationPersonal.Server.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddAsync(UsersEntity user)
        {
            return await _repository.AddAsync(user);
        }

        public async Task<int> UpdateAsync(UsersEntity user)
        {
            return await _repository.UpdateAsync(user);
        }

        public async Task<int> DeleteAsync(string login)
        {
            return await _repository.DeleteAsync(login);
        }

        public async Task<bool> VerifyAsync(UsersEntity user)
        {
            return await _repository.VerifyAsync(user);
        }

        public async Task<bool> CheckAsync(string login)
        {
            return await _repository.CheckAsync(login);
        }
    }
}
