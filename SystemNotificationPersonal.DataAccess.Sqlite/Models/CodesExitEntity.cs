namespace SystemNotificationPersonal.DataAccess.Sqlite.Models
{
    public class CodesExitEntity
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public string Code { get; set; } = string.Empty;
    }
}
