using System.Diagnostics;
using SystemNotificationPersonal.Core.Models;

namespace SystemNotificationPersonal.ConfigClientConsole
{
    public class DataCore
    {
        public DataCore(AppSettingClient settings)
        {
            Settings = settings;
        }
        public AppSettingClient Settings { get; set; }

        public Process? StartingProcess { get; set; }
    }
}
