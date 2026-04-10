using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.Server.Abstractions
{
    public interface ICodesService
    {
        Task<string> GenerateAsync(CancellationToken token);
        Task<string> GetCodeAsync(DateOnly date, CancellationToken token);
        Task<List<CodesExitEntity>> GetCodesAllAsync(CancellationToken token);
    }
}