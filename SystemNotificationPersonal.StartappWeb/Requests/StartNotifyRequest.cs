namespace SystemNotificationPersonal.StartappWeb.Requests
{
    public class StartNotifyRequest
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string AddressServer { get; set; } = string.Empty;
        public int VariableExit { get; set; }
    }
}
