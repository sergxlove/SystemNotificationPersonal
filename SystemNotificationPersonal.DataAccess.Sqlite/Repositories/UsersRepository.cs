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

        public async Task<Guid> AddAsync(UsersEntity user)
        {
            await _context.UsersTable.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> UpdateAsync(UsersEntity user)
        {
            return await _context.UsersTable
                .AsNoTracking()
                .Where(a => a.Id == user.Id)
                .ExecuteUpdateAsync(a => a.SetProperty(a => a.Password, user.Password));
        }

        public async Task<int> DeleteAsync(string login)
        {
            return await _context.UsersTable
                .AsNoTracking()
                .Where(a => a.Login == login)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> VerifyAsync(UsersEntity user)
        {
            var currentUser = await _context.UsersTable
                .FirstOrDefaultAsync(a => a.Login == user.Login);
            if (currentUser is null) return false;
            if (currentUser.Password == user.Password) return true;
            return false;
        }

        public async Task<bool> CheckAsync(string login)
        {
            var currentUser = await _context.UsersTable
                .FirstOrDefaultAsync(a => a.Login == login);
            if (currentUser is null) return false;
            return true;
        }
    }
}
