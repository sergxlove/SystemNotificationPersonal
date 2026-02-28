using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;
using SystemNotificationPersonal.Server.Abstractions;

namespace SystemNotificationPersonal.Server.Services
{
    public class HistoryNotifyService : IHistoryNotifyService
    {
        private readonly IHistoryNotifyRepository _repository;
        public HistoryNotifyService(IHistoryNotifyRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> AddAsync(HistoryNotifyEntity history)
        {
            return await _repository.AddAsync(history);
        }
        public async Task<List<HistoryNotifyEntity>> GetAsync()
        {
            return await _repository.GetAsync();
        }
        public async Task<List<HistoryNotifyEntity>> GetByDateAsync(DateTime date)
        {
            return await _repository.GetByDateAsync(date);
        }
        public async Task<List<HistoryNotifyEntity>> GetByTypeAlarmAsync(string alarmType)
        {
            return await _repository.GetByTypeAlarmAsync(alarmType);
        }
    }
}
