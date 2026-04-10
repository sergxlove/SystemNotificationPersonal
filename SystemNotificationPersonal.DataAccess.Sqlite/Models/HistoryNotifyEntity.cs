namespace SystemNotificationPersonal.DataAccess.Sqlite.Models
{
    public class HistoryNotifyEntity
    {
        public Guid Id { get; set; } 
        public Guid IdCode { get; set; }
        public Guid IdUser { get; set; } 
        public string TypeAlarm { get; set; } = string.Empty;
        public DateTime DateNotify { get; set; } 
    }
}
