namespace SystemNotificationPersonal.StartappConsole.Models
{
    public class Request
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int VariableExit { get; set; }
    }
}
