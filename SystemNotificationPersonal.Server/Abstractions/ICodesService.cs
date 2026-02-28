namespace SystemNotificationPersonal.Server.Abstractions
{
    public interface ICodesService
    {
        Task<string> GenerateAsync();
        Task<string> GetCodeAsync(DateOnly date);
    }
}