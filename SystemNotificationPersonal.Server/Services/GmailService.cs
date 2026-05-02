using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using SystemNotificationPersonal.Server.Abstractions;

namespace SystemNotificationPersonal.Server.Services
{
    public class GmailService : IGmailService
    {
        private const string _mailFrom = "";
        private const string _mailFromPassword = "";
        private const string _server = "smtp.gmail.com";
        private readonly MimeMessage _email;

        public GmailService()
        {
            _email = new MimeMessage();
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string htmlBody,
            string textBody = "")
        {
            try
            {
                _email.From.Add(new MailboxAddress("", _mailFrom));
                _email.To.Add(new MailboxAddress("", email));
                _email.Subject = subject;
                BodyBuilder body = new BodyBuilder();
                body.HtmlBody = htmlBody;
                body.TextBody = textBody;
                _email.Body = body.ToMessageBody();
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_server, 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailFrom, _mailFromPassword);
                await smtp.SendAsync(_email);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
