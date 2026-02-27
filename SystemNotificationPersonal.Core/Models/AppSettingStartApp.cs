using System.Text.Json;
using System.Text.Json.Serialization;

namespace SystemNotification.Core.Models
{
    public class AppSettingStartApp
    {
        public string AddressServer { get; set; }
        public string PathAppsettings { get; set; }
        public bool FirstStart { get; set; }

        private JsonSerializerOptions OptionsJson = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public AppSettingStartApp()
        {
            AddressServer = "localhost:5005";
            PathAppsettings = Directory.GetCurrentDirectory() + "\\appsettingsStartApp.json";
            FirstStart = false;
        }

        public AppSettingStartApp(string addressServer, string pathAppsettings, bool firstStart)
        {
            AddressServer = addressServer;
            PathAppsettings = pathAppsettings;
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
            AppSettingStartApp? config = JsonSerializer.Deserialize<AppSettingStartApp>(json);
            if (config is null) return;
            AddressServer = config.AddressServer;
            PathAppsettings = config.PathAppsettings;
            FirstStart = config.FirstStart;
        }

        public void Copy(AppSettingStartApp newSetting)
        {
            AddressServer = newSetting.AddressServer;
            PathAppsettings = newSetting.PathAppsettings;
            FirstStart = newSetting.FirstStart;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, OptionsJson);
        }
    }
}
