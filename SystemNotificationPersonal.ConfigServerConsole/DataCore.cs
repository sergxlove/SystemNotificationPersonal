using System.Diagnostics;
using SystemNotificationPersonal.Core.Models;

namespace SystemNotificationPersonal.ConfigServerConsole
{
    public class DataCore
    {
        public DataCore(AppSettingServer settings)
        {
            Settings = settings;
        }
        public AppSettingServer Settings { get; set; }
        public Process? StartingProcess { get; set; }
    }
}
