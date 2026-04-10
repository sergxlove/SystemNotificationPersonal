using Microsoft.EntityFrameworkCore;
using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.DataAccess.Sqlite.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly SystemNotificationDbContext _context;

        public UsersRepository(SystemNotificationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(UsersEntity user, CancellationToken token)
        {
            await _context.UsersTable.AddAsync(user, token);
            await _context.SaveChangesAsync(token);
            return user.Id;
        }

        public async Task<int> UpdatePasswordAsync(UsersEntity user, CancellationToken token)
        {
            return await _context.UsersTable
                .AsNoTracking()
                .Where(a => a.Id == user.Id)
                .ExecuteUpdateAsync(a => a.SetProperty(a => a.Password, user.Password), token);
        }

        public async Task<int> UpdateRoleAsync(UsersEntity user, CancellationToken token)
        {
            return await _context.UsersTable
                .AsNoTracking()
                .Where(a => a.Id == user.Id)
                .ExecuteUpdateAsync(a => a.SetProperty(a => a.Role, user.Role), token);
        }

        public async Task<int> DeleteAsync(string login, CancellationToken token)
        {
            return await _context.UsersTable
                .AsNoTracking()
                .Where(a => a.Login == login)
                .ExecuteDeleteAsync(token);
        }

        public async Task<bool> VerifyAsync(UsersEntity user, CancellationToken token)
        {
            var currentUser = await _context.UsersTable
                .FirstOrDefaultAsync(a => a.Login == user.Login, token);
            if (currentUser is null) return false;
            if (currentUser.Password == user.Password) return true;
            return false;
        }

        public async Task<bool> CheckAsync(string login, CancellationToken token)
        {
            var currentUser = await _context.UsersTable
                .FirstOrDefaultAsync(a => a.Login == login, token);
            if (currentUser is null) return false;
            return true;
        }
    }
}
