using System.Security.Cryptography;
using System.Text;
using SystemNotificationPersonal.Server.Abstractions;

namespace SystemNotificationPersonal.Server.Services
{
    public class HasherService : IHasherService
    {
        public string GetHash(string text)
        {
            string textHash = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(bytes);
            textHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return textHash;
        }
    }
}
