using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.Server.Abstractions
{
    public interface IHistoryNotifyService
    {
        Task<Guid> AddAsync(HistoryNotifyEntity history, CancellationToken token);
        Task<List<HistoryNotifyEntity>> GetAsync(CancellationToken token);
        Task<List<HistoryNotifyEntity>> GetByDateAsync(DateTime date, CancellationToken token);
        Task<List<HistoryNotifyEntity>> GetByTypeAlarmAsync(string alarmType, CancellationToken token);
    }
}