using System.Text.Json;
using System.Text.Json.Serialization;

namespace SystemNotificationPersonal.Core.Models
{
    public class AppSettingServer
    {
        public int Port { get; set; }
        public string IPAddressCors { get; set; }
        public string PathAppsettings { get; set; }
        public string ConnectionString { get; set; }
        public string Protocol { get; set; }
        public string PathExe { get; set; }
        public bool FirstStart { get; set; }

        private JsonSerializerOptions OptionsJson = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public AppSettingServer()
        {
            Port = 5005;
            IPAddressCors = "192.168.";
            PathAppsettings = "D:\\documents\\logsSNP" + "\\appsettingsServer.json";
            ConnectionString = "Data Source=" + "D:\\documents\\logsSNP" + "\\data.db";
            Protocol = "http";
            PathExe = Directory.GetCurrentDirectory() + "\\SystemNotificationPeople.exe";
            FirstStart = false;
        }

        public AppSettingServer(int port, string iPAddressCors, string pathAppsettings,
            string connectionString, string protocol, string pathExe, bool firstStart)
        {
            Port = port;
            IPAddressCors = iPAddressCors;
            PathAppsettings = pathAppsettings;
            ConnectionString = connectionString;
            Protocol = protocol;
            PathExe = pathExe;
            FirstStart = firstStart;
        }

        public void CreateConfig()
        {
            string json = JsonSerializer.Serialize(this, OptionsJson);
            File.WriteAllText(PathAppsettings, json);
        }

        public void ReadConfig()
        {
            if (!File.Exists(PathAppsettings))
            {
                CreateConfig();
                return;
            }
            string json = File.ReadAllText(PathAppsettings);
            AppSettingServer? config = JsonSerializer.Deserialize<AppSettingServer>(json);
            if (config is null) return;
            Port = config.Port;
            IPAddressCors = config.IPAddressCors;
            PathAppsettings = config.PathAppsettings;
            ConnectionString = config.ConnectionString;
            Protocol = config.Protocol;
            PathExe = config.PathExe;
            FirstStart = config.FirstStart;
        }

        public void Copy(AppSettingServer newSetting)
        {
            Port = newSetting.Port;
            IPAddressCors = newSetting.IPAddressCors;
            PathAppsettings = newSetting.PathAppsettings;
            ConnectionString = newSetting.ConnectionString;
            Protocol = newSetting.Protocol;
            PathExe = newSetting.PathExe;
            FirstStart = newSetting.FirstStart;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, OptionsJson);
        }
    }
}
