using System.Text.Json;
using SystemNotificationPersonal.Core.Models;

namespace SystemNotificationPeronal.Tests.UnitTests
{
    public class AppsettingClientTests
    {
        private string _testDirectory;
        private string _testConfigPath;

        [SetUp]
        public void SetUp()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            _testDirectory = Path.Combine(currentDirectory, "TestTemp", "SNP_Tests_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(_testDirectory);
            _testConfigPath = Path.Combine(_testDirectory, "appsettingsClient.json");
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }
        }

        [Test]
        public void Constructor_Default_ShouldSetDefaultValues()
        {
            var config = new AppSettingClient();
            Assert.Multiple(() =>
            {
                Assert.That(config.PathAppsettings, Does.Contain("appsettingsClient.json"));
                Assert.That(config.PathExe, Does.Contain("SystemNotificationPeopleGUI.exe"));
                Assert.That(config.AddressServer, Is.EqualTo("localhost:5005"));
                Assert.That(config.Theme, Is.EqualTo("light"));
                Assert.That(config.CodeHash, Is.Empty);
                Assert.That(config.Header, Is.EqualTo("Пожалуйста, покиньте помещение"));
                Assert.That(config.TimeBeforeOffPC, Is.EqualTo(180));
                Assert.That(config.VariableExit, Is.EqualTo(1));
                Assert.That(config.FirstStart, Is.False);
            });
        }

        [Test]
        public void Constructor_Parameterized_ShouldSetProvidedValues()
        {
            string expectedPath = @"C:\test\config.json";
            string expectedExe = @"C:\test\app.exe";
            string expectedAddress = "192.168.1.1:8080";
            string expectedTheme = "dark";
            string expectedCodeHash = "ABC123";
            string expectedHeader = "Test Header";
            int expectedTime = 300;
            int expectedVariable = 2;
            bool expectedFirstStart = true;
            var config = new AppSettingClient(
                expectedPath, expectedExe, expectedAddress, expectedTheme,
                expectedCodeHash, expectedHeader, expectedTime, expectedVariable, expectedFirstStart);
            Assert.Multiple(() =>
            {
                Assert.That(config.PathAppsettings, Is.EqualTo(expectedPath));
                Assert.That(config.PathExe, Is.EqualTo(expectedExe));
                Assert.That(config.AddressServer, Is.EqualTo(expectedAddress));
                Assert.That(config.Theme, Is.EqualTo(expectedTheme));
                Assert.That(config.CodeHash, Is.EqualTo(expectedCodeHash));
                Assert.That(config.Header, Is.EqualTo(expectedHeader));
                Assert.That(config.TimeBeforeOffPC, Is.EqualTo(expectedTime));
                Assert.That(config.VariableExit, Is.EqualTo(expectedVariable));
                Assert.That(config.FirstStart, Is.EqualTo(expectedFirstStart));
            });
        }

        [Test]
        public void CreateConfig_ShouldCreateJsonFile()
        {
            var config = new AppSettingClient();
            config.PathAppsettings = _testConfigPath;
            config.CreateConfig();
            Assert.That(File.Exists(_testConfigPath), Is.True);
            string fileContent = File.ReadAllText(_testConfigPath);
            Assert.Multiple(() =>
            {
                Assert.That(fileContent, Is.Not.Empty);
                Assert.That(fileContent, Does.Contain("PathAppsettings"));
                Assert.That(fileContent, Does.Contain("SystemNotificationPeopleGUI.exe"));
            });
        }

        [Test]
        public void CreateConfig_ShouldCreateValidJsonWithCorrectProperties()
        {
            var config = new AppSettingClient
            {
                PathAppsettings = _testConfigPath,
                AddressServer = "test.server.com",
                Theme = "dark",
                TimeBeforeOffPC = 120
            };
            config.CreateConfig();
            string jsonContent = File.ReadAllText(_testConfigPath);
            var deserializedConfig = JsonSerializer.Deserialize<AppSettingClient>(jsonContent);
            Assert.That(deserializedConfig, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(deserializedConfig.AddressServer, Is.EqualTo("test.server.com"));
                Assert.That(deserializedConfig.Theme, Is.EqualTo("dark"));
                Assert.That(deserializedConfig.TimeBeforeOffPC, Is.EqualTo(120));
            });
        }

        [Test]
        public void ReadConfig_WhenFileDoesNotExist_ShouldCreateConfig()
        {
            var config = new AppSettingClient();
            config.PathAppsettings = _testConfigPath;
            if (File.Exists(_testConfigPath))
                File.Delete(_testConfigPath);
            config.ReadConfig();
            Assert.Multiple(() =>
            {
                Assert.That(File.Exists(_testConfigPath), Is.True);
                Assert.That(config.PathAppsettings, Is.EqualTo(_testConfigPath));
            });
        }

        [Test]
        public void ReadConfig_WhenFileExists_ShouldLoadConfiguration()
        {
            var originalConfig = new AppSettingClient(
                _testConfigPath, @"D:\test\app.exe", "10.0.0.1:9000",
                "dark", "XYZ789", "Custom Header", 240, 3, true);
            originalConfig.CreateConfig();
            var loadedConfig = new AppSettingClient();
            loadedConfig.PathAppsettings = _testConfigPath;
            loadedConfig.ReadConfig();
            Assert.Multiple(() =>
            {
                Assert.That(loadedConfig.PathAppsettings, Is.EqualTo(originalConfig.PathAppsettings));
                Assert.That(loadedConfig.PathExe, Is.EqualTo(originalConfig.PathExe));
                Assert.That(loadedConfig.AddressServer, Is.EqualTo(originalConfig.AddressServer));
                Assert.That(loadedConfig.Theme, Is.EqualTo(originalConfig.Theme));
                Assert.That(loadedConfig.CodeHash, Is.EqualTo(originalConfig.CodeHash));
                Assert.That(loadedConfig.Header, Is.EqualTo(originalConfig.Header));
                Assert.That(loadedConfig.TimeBeforeOffPC, Is.EqualTo(originalConfig.TimeBeforeOffPC));
                Assert.That(loadedConfig.VariableExit, Is.EqualTo(originalConfig.VariableExit));
                Assert.That(loadedConfig.FirstStart, Is.EqualTo(originalConfig.FirstStart));
            });
        }

        [Test]
        public void Copy_ShouldCopyAllPropertiesFromSource()
        {
            var sourceConfig = new AppSettingClient(
                @"C:\source\config.json", @"C:\source\app.exe", "source.server.com",
                "dark", "SOURCE123", "Source Header", 500, 5, true);
            var targetConfig = new AppSettingClient();
            targetConfig.Copy(sourceConfig);
            Assert.Multiple(() =>
            {
                Assert.That(targetConfig.PathAppsettings, Is.EqualTo(sourceConfig.PathAppsettings));
                Assert.That(targetConfig.PathExe, Is.EqualTo(sourceConfig.PathExe));
                Assert.That(targetConfig.AddressServer, Is.EqualTo(sourceConfig.AddressServer));
                Assert.That(targetConfig.Theme, Is.EqualTo(sourceConfig.Theme));
                Assert.That(targetConfig.CodeHash, Is.EqualTo(sourceConfig.CodeHash));
                Assert.That(targetConfig.Header, Is.EqualTo(sourceConfig.Header));
                Assert.That(targetConfig.TimeBeforeOffPC, Is.EqualTo(sourceConfig.TimeBeforeOffPC));
                Assert.That(targetConfig.VariableExit, Is.EqualTo(sourceConfig.VariableExit));
                Assert.That(targetConfig.FirstStart, Is.EqualTo(sourceConfig.FirstStart));
            });
        }

        [Test]
        public void ToString_ShouldReturnValidJsonRepresentation()
        {
            var config = new AppSettingClient(
                _testConfigPath, @"D:\test.exe", "test.server.com",
                "dark", "HASH123", "Test Header", 150, 2, true);
            string jsonString = config.ToString();
            Assert.That(jsonString, Is.Not.Empty);
            Assert.That(jsonString, Does.Contain("PathAppsettings"));
            Assert.That(jsonString, Does.Contain("test.server.com"));
            var deserializedConfig = JsonSerializer.Deserialize<AppSettingClient>(jsonString);
            Assert.That(deserializedConfig, Is.Not.Null);
            Assert.That(deserializedConfig.AddressServer, Is.EqualTo(config.AddressServer));
        }

        [TestCase("light")]
        [TestCase("dark")]
        [TestCase("system")]
        public void ThemeProperty_ShouldAcceptVariousValues(string theme)
        {
            var config = new AppSettingClient();
            config.Theme = theme;
            Assert.That(config.Theme, Is.EqualTo(theme));
        }

        [TestCase(0)]
        [TestCase(30)]
        [TestCase(1800)]
        [TestCase(3600)]
        public void TimeBeforeOffPC_ShouldAcceptVariousValues(int timeSeconds)
        {
            var config = new AppSettingClient();
            config.TimeBeforeOffPC = timeSeconds;
            Assert.That(config.TimeBeforeOffPC, Is.EqualTo(timeSeconds));
        }

        [Test]
        public void CreateConfig_ShouldCreateFormattedJsonWithIndentation()
        {
            var config = new AppSettingClient();
            config.PathAppsettings = _testConfigPath;
            config.CreateConfig();
            string content = File.ReadAllText(_testConfigPath);
            Assert.That(content, Does.Contain(Environment.NewLine));
            Assert.That(content.Length, Is.GreaterThan(100));
        }

        [Test]
        public void MultipleReadWriteOperations_ShouldPreserveDataIntegrity()
        {
            var originalConfig = new AppSettingClient(
                _testConfigPath, @"D:\app.exe", "192.168.1.100:5000",
                "dark", "MULTI123", "Multiple Test", 999, 7, true);
            originalConfig.CreateConfig();
            var loadedConfig1 = new AppSettingClient();
            loadedConfig1.PathAppsettings = _testConfigPath;
            loadedConfig1.ReadConfig();
            var loadedConfig2 = new AppSettingClient();
            loadedConfig2.PathAppsettings = _testConfigPath;
            loadedConfig2.ReadConfig();
            Assert.Multiple(() =>
            {
                Assert.That(loadedConfig1.AddressServer, Is.EqualTo(loadedConfig2.AddressServer));
                Assert.That(loadedConfig1.TimeBeforeOffPC, Is.EqualTo(loadedConfig2.TimeBeforeOffPC));
            });
        }
    }
}
