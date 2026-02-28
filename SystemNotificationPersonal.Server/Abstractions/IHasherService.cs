namespace SystemNotificationPersonal.Server.Abstractions
{
    public interface IHasherService
    {
        string GetHash(string text);
    }
}