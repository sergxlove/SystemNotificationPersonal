using System.Text.Json;
using SystemNotificationPersonal.Core.Models;

namespace SystemNotificationPeronal.Tests.UnitTests
{
    public class AppsettingServerTests
    {
        private string _testDirectory;
        private string _testConfigPath;

        [SetUp]
        public void SetUp()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            _testDirectory = Path.Combine(currentDirectory, "TestTemp", "ServerTests_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(_testDirectory);
            _testConfigPath = Path.Combine(_testDirectory, "appsettingsServer.json");
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
            var config = new AppSettingServer();
            Assert.Multiple(() =>
            {
                Assert.That(config.Port, Is.EqualTo(5005));
                Assert.That(config.IPAddressCors, Is.EqualTo("192.168."));
                Assert.That(config.PathAppsettings, Does.Contain("appsettingsServer.json"));
                Assert.That(config.ConnectionString, Does.Contain("data.db"));
                Assert.That(config.Protocol, Is.EqualTo("http"));
                Assert.That(config.PathExe, Does.Contain("SystemNotificationPeople.exe"));
                Assert.That(config.FirstStart, Is.False);
            });
        }

        [Test]
        public void Constructor_Parameterized_ShouldSetProvidedValues()
        {
            int expectedPort = 8080;
            string expectedIPAddressCors = "10.0.0.";
            string expectedPathAppsettings = @"D:\test\server.json";
            string expectedConnectionString = "Data Source=D:\\test\\data.db";
            string expectedProtocol = "https";
            string expectedPathExe = @"D:\test\server.exe";
            bool expectedFirstStart = true;
            var config = new AppSettingServer(
                expectedPort, expectedIPAddressCors, expectedPathAppsettings,
                expectedConnectionString, expectedProtocol, expectedPathExe, expectedFirstStart);
            Assert.Multiple(() =>
            {
                Assert.That(config.Port, Is.EqualTo(expectedPort));
                Assert.That(config.IPAddressCors, Is.EqualTo(expectedIPAddressCors));
                Assert.That(config.PathAppsettings, Is.EqualTo(expectedPathAppsettings));
                Assert.That(config.ConnectionString, Is.EqualTo(expectedConnectionString));
                Assert.That(config.Protocol, Is.EqualTo(expectedProtocol));
                Assert.That(config.PathExe, Is.EqualTo(expectedPathExe));
                Assert.That(config.FirstStart, Is.EqualTo(expectedFirstStart));
            });
        }

        [Test]
        public void CreateConfig_ShouldCreateJsonFile()
        {
            var config = new AppSettingServer();
            config.PathAppsettings = _testConfigPath;
            config.CreateConfig();
            Assert.That(File.Exists(_testConfigPath), Is.True);
            string fileContent = File.ReadAllText(_testConfigPath);
            Assert.That(fileContent, Is.Not.Empty);
            Assert.That(fileContent, Does.Contain("Port"));
            Assert.That(fileContent, Does.Contain("IPAddressCors"));
            Assert.That(fileContent, Does.Contain("ConnectionString"));
        }

        [Test]
        public void CreateConfig_ShouldCreateValidJsonWithCorrectProperties()
        {
            var config = new AppSettingServer
            {
                PathAppsettings = _testConfigPath,
                Port = 9090,
                Protocol = "https",
                ConnectionString = "Data Source=test.db",
                IPAddressCors = "192.168.1.",
                FirstStart = true
            };
            config.CreateConfig();
            string jsonContent = File.ReadAllText(_testConfigPath);
            var deserializedConfig = JsonSerializer.Deserialize<AppSettingServer>(jsonContent);
            Assert.That(deserializedConfig, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(deserializedConfig.Port, Is.EqualTo(9090));
                Assert.That(deserializedConfig.Protocol, Is.EqualTo("https"));
                Assert.That(deserializedConfig.ConnectionString, Is.EqualTo("Data Source=test.db"));
                Assert.That(deserializedConfig.IPAddressCors, Is.EqualTo("192.168.1."));
                Assert.That(deserializedConfig.FirstStart, Is.True);
            });
        }

        [Test]
        public void CreateConfig_ShouldCreateFormattedJsonWithIndentation()
        {
            var config = new AppSettingServer();
            config.PathAppsettings = _testConfigPath;
            config.CreateConfig();
            string content = File.ReadAllText(_testConfigPath);
            Assert.That(content, Does.Contain(Environment.NewLine));
            Assert.That(content.Length, Is.GreaterThan(100));
        }

        [Test]
        public void ReadConfig_WhenFileDoesNotExist_ShouldCreateConfig()
        {
            var config = new AppSettingServer();
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
            var originalConfig = new AppSettingServer(
                7777, "10.0.0.", _testConfigPath,
                "Data Source=test.db", "https", @"D:\app.exe", true);
            originalConfig.CreateConfig();
            var loadedConfig = new AppSettingServer();
            loadedConfig.PathAppsettings = _testConfigPath;
            loadedConfig.ReadConfig();
            Assert.Multiple(() =>
            {
                Assert.That(loadedConfig.Port, Is.EqualTo(originalConfig.Port));
                Assert.That(loadedConfig.IPAddressCors, Is.EqualTo(originalConfig.IPAddressCors));
                Assert.That(loadedConfig.PathAppsettings, Is.EqualTo(originalConfig.PathAppsettings));
                Assert.That(loadedConfig.ConnectionString, Is.EqualTo(originalConfig.ConnectionString));
                Assert.That(loadedConfig.Protocol, Is.EqualTo(originalConfig.Protocol));
                Assert.That(loadedConfig.PathExe, Is.EqualTo(originalConfig.PathExe));
                Assert.That(loadedConfig.FirstStart, Is.EqualTo(originalConfig.FirstStart));
            });
        }

        [Test]
        public void Copy_ShouldCopyAllPropertiesFromSource()
        {
            var sourceConfig = new AppSettingServer(
                8888, "172.16.0.", @"C:\source\server.json",
                "Data Source=C:\\source\\data.db", "http", @"C:\source\server.exe", true);
            var targetConfig = new AppSettingServer();
            targetConfig.Copy(sourceConfig);
            Assert.Multiple(() =>
            {
                Assert.That(targetConfig.Port, Is.EqualTo(sourceConfig.Port));
                Assert.That(targetConfig.IPAddressCors, Is.EqualTo(sourceConfig.IPAddressCors));
                Assert.That(targetConfig.PathAppsettings, Is.EqualTo(sourceConfig.PathAppsettings));
                Assert.That(targetConfig.ConnectionString, Is.EqualTo(sourceConfig.ConnectionString));
                Assert.That(targetConfig.Protocol, Is.EqualTo(sourceConfig.Protocol));
                Assert.That(targetConfig.PathExe, Is.EqualTo(sourceConfig.PathExe));
                Assert.That(targetConfig.FirstStart, Is.EqualTo(sourceConfig.FirstStart));
            });
        }

        [Test]
        public void Copy_ShouldCreateIndependentCopy()
        {
            var sourceConfig = new AppSettingServer();
            var targetConfig = new AppSettingServer();
            targetConfig.Copy(sourceConfig);
            sourceConfig.Port = 9999;
            sourceConfig.Protocol = "https";
            Assert.Multiple(() =>
            {
                Assert.That(targetConfig.Port, Is.Not.EqualTo(sourceConfig.Port));
                Assert.That(targetConfig.Protocol, Is.Not.EqualTo(sourceConfig.Protocol));
                Assert.That(targetConfig.Port, Is.EqualTo(5005));
            });
        }

        [Test]
        public void ToString_ShouldReturnValidJsonRepresentation()
        {
            var config = new AppSettingServer(
                1234, "192.169.", _testConfigPath,
                "Data Source=test.db", "ftp", @"D:\test.exe", true);
            string jsonString = config.ToString();
            Assert.That(jsonString, Is.Not.Empty);
            Assert.Multiple(() =>
            {
                Assert.That(jsonString, Does.Contain("Port"));
                Assert.That(jsonString, Does.Contain("1234"));
                Assert.That(jsonString, Does.Contain("IPAddressCors"));
                Assert.That(jsonString, Does.Contain("192.169."));
                Assert.That(jsonString, Does.Contain("Protocol"));
                Assert.That(jsonString, Does.Contain("ftp"));
            });
            var deserializedConfig = JsonSerializer.Deserialize<AppSettingServer>(jsonString);
            Assert.That(deserializedConfig, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(deserializedConfig.Port, Is.EqualTo(config.Port));
                Assert.That(deserializedConfig.Protocol, Is.EqualTo(config.Protocol));
            });
        }

        [TestCase(80)]
        [TestCase(443)]
        [TestCase(3000)]
        [TestCase(8080)]
        [TestCase(27015)]
        public void Port_ShouldAcceptVariousValues(int port)
        {
            var config = new AppSettingServer();
            config.Port = port;
            Assert.That(config.Port, Is.EqualTo(port));
        }

        [TestCase("http")]
        [TestCase("https")]
        [TestCase("ftp")]
        [TestCase("ws")]
        [TestCase("wss")]
        public void Protocol_ShouldAcceptVariousValues(string protocol)
        {
            var config = new AppSettingServer();
            config.Protocol = protocol;
            Assert.That(config.Protocol, Is.EqualTo(protocol));
        }

        [TestCase("192.168.")]
        [TestCase("10.0.0.")]
        [TestCase("172.16.")]
        [TestCase("*")]
        public void IPAddressCors_ShouldAcceptVariousValues(string ipAddress)
        {
            var config = new AppSettingServer();
            config.IPAddressCors = ipAddress;
            Assert.That(config.IPAddressCors, Is.EqualTo(ipAddress));
        }

        [Test]
        public void MultipleReadWriteOperations_ShouldPreserveDataIntegrity()
        {
            var originalConfig = new AppSettingServer(
                5432, "192.168.1.", _testConfigPath,
                "Data Source=database.db", "https", @"D:\app.exe", true);
            originalConfig.CreateConfig();
            var loadedConfig1 = new AppSettingServer();
            loadedConfig1.PathAppsettings = _testConfigPath;
            loadedConfig1.ReadConfig();
            var loadedConfig2 = new AppSettingServer();
            loadedConfig2.PathAppsettings = _testConfigPath;
            loadedConfig2.ReadConfig();
            Assert.Multiple(() =>
            {
                Assert.That(loadedConfig1.Port, Is.EqualTo(loadedConfig2.Port));
                Assert.That(loadedConfig1.IPAddressCors, Is.EqualTo(loadedConfig2.IPAddressCors));
                Assert.That(loadedConfig1.ConnectionString, Is.EqualTo(loadedConfig2.ConnectionString));
                Assert.That(loadedConfig1.Protocol, Is.EqualTo(loadedConfig2.Protocol));
                Assert.That(loadedConfig1.FirstStart, Is.EqualTo(loadedConfig2.FirstStart));
            });
        }

        [Test]
        public void SaveAndLoad_ShouldPreserveAllData()
        {
            var originalConfig = new AppSettingServer(
                9999, "10.10.10.", _testConfigPath,
                "Data Source=fulltest.db", "wss", @"D:\fulltest.exe", true);
            originalConfig.CreateConfig();
            var loadedConfig = new AppSettingServer();
            loadedConfig.PathAppsettings = _testConfigPath;
            loadedConfig.ReadConfig();
            Assert.Multiple(() =>
            {
                Assert.That(loadedConfig.Port, Is.EqualTo(originalConfig.Port));
                Assert.That(loadedConfig.IPAddressCors, Is.EqualTo(originalConfig.IPAddressCors));
                Assert.That(loadedConfig.PathAppsettings, Is.EqualTo(originalConfig.PathAppsettings));
                Assert.That(loadedConfig.ConnectionString, Is.EqualTo(originalConfig.ConnectionString));
                Assert.That(loadedConfig.Protocol, Is.EqualTo(originalConfig.Protocol));
                Assert.That(loadedConfig.PathExe, Is.EqualTo(originalConfig.PathExe));
                Assert.That(loadedConfig.FirstStart, Is.EqualTo(originalConfig.FirstStart));
            });
        }

        [Test]
        public void ReadConfig_WhenConfigIsNull_ShouldNotCrash()
        {
            var config = new AppSettingServer();
            config.PathAppsettings = _testConfigPath;
            File.WriteAllText(_testConfigPath, "null");
            Assert.That(() => config.ReadConfig(), Throws.Nothing);
        }
    }
}
