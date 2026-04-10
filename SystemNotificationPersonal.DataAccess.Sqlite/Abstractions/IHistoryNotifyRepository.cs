using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.DataAccess.Sqlite.Abstractions
{
    public interface IHistoryNotifyRepository
    {
        Task<Guid> AddAsync(HistoryNotifyEntity history, CancellationToken token);
        Task<List<HistoryNotifyEntity>> GetAsync(CancellationToken token);
        Task<List<HistoryNotifyEntity>> GetByDateAsync(DateTime date, CancellationToken token);
        Task<List<HistoryNotifyEntity>> GetByTypeAlarmAsync(string alarmType, CancellationToken token);
    }
}