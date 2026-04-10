using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.DataAccess.Sqlite.Abstractions
{
    public interface ICodesExitRepository
    {
        Task<string> GenerateAsync(CancellationToken token);
        Task<string> GetCodeAsync(DateOnly date, CancellationToken token);
        Task<List<CodesExitEntity>> GetCodesAllAsync(CancellationToken token);
    }
}