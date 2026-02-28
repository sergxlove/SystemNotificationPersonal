using Microsoft.EntityFrameworkCore;
using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.DataAccess.Sqlite.Repositories
{
    public class HistoryNotifyRepository : IHistoryNotifyRepository
    {
        private readonly SystemNotificationDbContext _context;

        public HistoryNotifyRepository(SystemNotificationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(HistoryNotifyEntity history)
        {
            await _context.HistoryNotify.AddAsync(history);
            await _context.SaveChangesAsync();
            return history.Id;
        }

        public async Task<List<HistoryNotifyEntity>> GetByDateAsync(DateTime date)
        {
            return await _context.HistoryNotify
                .AsNoTracking()
                .Where(a => a.DateNotify.Date == date.Date)
                .ToListAsync();
        }

        public async Task<List<HistoryNotifyEntity>> GetByTypeAlarmAsync(string alarmType)
        {
            return await _context.HistoryNotify
                .AsNoTracking()
                .Where(a => a.TypeAlarm == alarmType)
                .ToListAsync();
        }

        public async Task<List<HistoryNotifyEntity>> GetAsync()
        {
            return await _context.HistoryNotify
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
