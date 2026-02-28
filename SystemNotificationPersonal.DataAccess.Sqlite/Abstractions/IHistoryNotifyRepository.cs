using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.DataAccess.Sqlite.Abstractions
{
    public interface IHistoryNotifyRepository
    {
        Task<Guid> AddAsync(HistoryNotifyEntity history);
        Task<List<HistoryNotifyEntity>> GetAsync();
        Task<List<HistoryNotifyEntity>> GetByDateAsync(DateTime date);
        Task<List<HistoryNotifyEntity>> GetByTypeAlarmAsync(string alarmType);
    }
}