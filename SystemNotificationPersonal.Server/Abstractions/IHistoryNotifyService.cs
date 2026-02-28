using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.Server.Abstractions
{
    public interface IHistoryNotifyService
    {
        Task<Guid> AddAsync(HistoryNotifyEntity history);
        Task<List<HistoryNotifyEntity>> GetAsync();
        Task<List<HistoryNotifyEntity>> GetByDateAsync(DateTime date);
        Task<List<HistoryNotifyEntity>> GetByTypeAlarmAsync(string alarmType);
    }
}