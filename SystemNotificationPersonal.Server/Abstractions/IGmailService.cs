namespace SystemNotificationPersonal.Server.Abstractions
{
    public interface IGmailService
    {
        Task<bool> SendEmailAsync(string email, string subject, string htmlBody, string textBody = "");
    }
}