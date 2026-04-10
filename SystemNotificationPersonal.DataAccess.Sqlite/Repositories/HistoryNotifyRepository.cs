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

        public async Task<Guid> AddAsync(HistoryNotifyEntity history, CancellationToken token)
        {
            await _context.HistoryNotify.AddAsync(history, token);
            await _context.SaveChangesAsync(token);
            return history.Id;
        }

        public async Task<List<HistoryNotifyEntity>> GetByDateAsync(DateTime date, CancellationToken token)
        {
            return await _context.HistoryNotify
                .AsNoTracking()
                .Where(a => a.DateNotify.Date == date.Date)
                .ToListAsync(token);
        }

        public async Task<List<HistoryNotifyEntity>> GetByTypeAlarmAsync(string alarmType, CancellationToken token)
        {
            return await _context.HistoryNotify
                .AsNoTracking()
                .Where(a => a.TypeAlarm == alarmType)
                .ToListAsync(token);
        }

        public async Task<List<HistoryNotifyEntity>> GetAsync(CancellationToken token)
        {
            return await _context.HistoryNotify
                .AsNoTracking()
                .ToListAsync(token);
        }

    }
}
