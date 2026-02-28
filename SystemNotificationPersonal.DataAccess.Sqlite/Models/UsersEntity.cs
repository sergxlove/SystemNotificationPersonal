namespace SystemNotificationPersonal.DataAccess.Sqlite.Models
{
    public class UsersEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
