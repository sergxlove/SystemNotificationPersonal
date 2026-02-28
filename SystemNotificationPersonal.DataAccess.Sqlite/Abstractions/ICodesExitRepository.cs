namespace SystemNotificationPersonal.DataAccess.Sqlite.Abstractions
{
    public interface ICodesExitRepository
    {
        Task<string> GenerateAsync();
        Task<string> GetCodeAsync(DateOnly date);
    }
}