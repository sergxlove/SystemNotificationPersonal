using System.Text.Json;
using System.Text.Json.Serialization;

namespace SystemNotificationPersonal.Core.Models
{
    public class AppSettingClient
    {
        public string PathAppsettings { get; set; }
        public string PathExe { get; set; }
        public string AddressServer { get; set; }
        public string Theme { get; set; }
        public string CodeHash { get; set; }
        public string Header { get; set; }
        public int TimeBeforeOffPC { get; set; }
        public int VariableExit { get; set; }
        public bool FirstStart { get; set; }

        private JsonSerializerOptions OptionsJson = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public AppSettingClient()
        {
            PathAppsettings = Directory.GetCurrentDirectory() + "\\appsettingsClient.json";
            PathExe = Directory.GetCurrentDirectory() + "\\SystemNotificationPeopleGUI.exe";
            AddressServer = "localhost:5005";
            Theme = "light";
            CodeHash = string.Empty;
            Header = "Пожалуйста, покиньте помещение";
            TimeBeforeOffPC = 180;
            VariableExit = 1;
            FirstStart = false;
        }

        public AppSettingClient(string pathAppsettings, string pathExe, string addressServer,
            string theme, string codeHash, string header, int timeBeforeOffPC,
            int variableExit, bool firstStart)
        {
            PathAppsettings = pathAppsettings;
            PathExe = pathExe;
            AddressServer = addressServer;
            Theme = theme;
            CodeHash = codeHash;
            Header = header;
            TimeBeforeOffPC = timeBeforeOffPC;
            VariableExit = variableExit;
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
            AppSettingClient? config = JsonSerializer.Deserialize<AppSettingClient>(json);
            if (config is null) return;
            PathAppsettings = config.PathAppsettings;
            PathExe = config.PathExe;
            AddressServer = config.AddressServer;
            Theme = config.Theme;
            CodeHash = config.CodeHash;
            Header = config.Header;
            TimeBeforeOffPC = config.TimeBeforeOffPC;
            VariableExit = config.VariableExit;
            FirstStart = config.FirstStart;
        }

        public void Copy(AppSettingClient newSettings)
        {
            PathAppsettings = newSettings.PathAppsettings;
            PathExe = newSettings.PathExe;
            AddressServer = newSettings.AddressServer;
            Theme = newSettings.Theme;
            CodeHash = newSettings.CodeHash;
            Header = newSettings.Header;
            TimeBeforeOffPC = newSettings.TimeBeforeOffPC;
            VariableExit = newSettings.VariableExit;
            FirstStart = newSettings.FirstStart;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, OptionsJson);
        }
    }
}
