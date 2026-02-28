using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
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

        public async Task<string> GetCodeAsync(DateOnly date)
        {
            return await _repository.GetCodeAsync(date);
        }

        public async Task<string> GenerateAsync()
        {
            return await _repository.GenerateAsync();
        }
    }
}
