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
        public async Task<Guid> AddAsync(HistoryNotifyEntity history, CancellationToken token)
        {
            return await _repository.AddAsync(history, token);
        }
        public async Task<List<HistoryNotifyEntity>> GetAsync(CancellationToken token)
        {
            return await _repository.GetAsync(token);
        }
        public async Task<List<HistoryNotifyEntity>> GetByDateAsync(DateTime date, CancellationToken token)
        {
            return await _repository.GetByDateAsync(date, token);
        }
        public async Task<List<HistoryNotifyEntity>> GetByTypeAlarmAsync(string alarmType, CancellationToken token)
        {
            return await _repository.GetByTypeAlarmAsync(alarmType, token);
        }
    }
}
