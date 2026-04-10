using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;
using SystemNotificationPersonal.Server.Abstractions;

namespace SystemNotificationPersonal.Server.Services
{
    public class CodesService : ICodesService
    {
        private readonly ICodesExitRepository _repository;

        public CodesService(ICodesExitRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> GenerateAsync(CancellationToken token)
        {
            return await _repository.GenerateAsync(token);
        }
        public async Task<string> GetCodeAsync(DateOnly date, CancellationToken token)
        {
            return await _repository.GetCodeAsync(date, token);
        }
        public async Task<List<CodesExitEntity>> GetCodesAllAsync(CancellationToken token)
        {
            return await _repository.GetCodesAllAsync(token);
        }
    }
}
