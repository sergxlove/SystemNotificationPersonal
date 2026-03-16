using SystemNotificationPersonal.Core.Models;

namespace SystemNotificationPersonal.StartappConsole
{
    public class DataCore
    {
        public DataCore(AppSettingStartApp settings)
        {
            Settings = settings;
        }
        public AppSettingStartApp Settings { get; set; }
    }
}
