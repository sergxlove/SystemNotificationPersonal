using System.Text.Json;
using SystemNotificationPersonal.Core.Models;

namespace SystemNotificationPeronal.Tests.UnitTests
{
    public class AppsettingStartappTests
    {
        private string _testDirectory;
        private string _testConfigPath;

        [SetUp]
        public void SetUp()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            _testDirectory = Path.Combine(currentDirectory, "TestTemp", "StartAppTests_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(_testDirectory);
            _testConfigPath = Path.Combine(_testDirectory, "appsettingsStartApp.json");
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
            var config = new AppSettingStartApp();
            Assert.Multiple(() =>
            {
                Assert.That(config.AddressServer, Is.EqualTo("localhost:5005"));
                Assert.That(config.PathAppsettings, Does.Contain("appsettingsStartApp.json"));
                Assert.That(config.FirstStart, Is.False);
            });
        }

        [Test]
        public void Constructor_Parameterized_ShouldSetProvidedValues()
        {
            string expectedAddress = "192.168.1.100:8080";
            string expectedPath = @"D:\test\startapp.json";
            bool expectedFirstStart = true;
            var config = new AppSettingStartApp(expectedAddress, expectedPath, expectedFirstStart);
            Assert.Multiple(() =>
            {
                Assert.That(config.AddressServer, Is.EqualTo(expectedAddress));
                Assert.That(config.PathAppsettings, Is.EqualTo(expectedPath));
                Assert.That(config.FirstStart, Is.EqualTo(expectedFirstStart));
            });
        }

        [Test]
        public void Constructor_Parameterized_WithEmptyAddress_ShouldSetEmptyString()
        {
            string expectedAddress = "";
            string expectedPath = @"D:\test\config.json";
            bool expectedFirstStart = false;
            var config = new AppSettingStartApp(expectedAddress, expectedPath, expectedFirstStart);
            Assert.Multiple(() =>
            {
                Assert.That(config.AddressServer, Is.EqualTo(""));
                Assert.That(config.PathAppsettings, Is.EqualTo(expectedPath));
                Assert.That(config.FirstStart, Is.EqualTo(expectedFirstStart));
            });
        }

        [Test]
        public void CreateConfig_ShouldCreateJsonFile()
        {
            var config = new AppSettingStartApp();
            config.PathAppsettings = _testConfigPath;
            config.CreateConfig();
            Assert.That(File.Exists(_testConfigPath), Is.True);
            string fileContent = File.ReadAllText(_testConfigPath);
            Assert.That(fileContent, Is.Not.Empty);
            Assert.Multiple(() =>
            {
                Assert.That(fileContent, Does.Contain("AddressServer"));
                Assert.That(fileContent, Does.Contain("PathAppsettings"));
                Assert.That(fileContent, Does.Contain("FirstStart"));
            });
        }

        [Test]
        public void CreateConfig_ShouldCreateValidJsonWithCorrectProperties()
        {
            var config = new AppSettingStartApp
            {
                PathAppsettings = _testConfigPath,
                AddressServer = "10.0.0.1:9000",
                FirstStart = true
            };
            config.CreateConfig();
            string jsonContent = File.ReadAllText(_testConfigPath);
            var deserializedConfig = JsonSerializer.Deserialize<AppSettingStartApp>(jsonContent);
            Assert.That(deserializedConfig, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(deserializedConfig.AddressServer, Is.EqualTo("10.0.0.1:9000"));
                Assert.That(deserializedConfig.FirstStart, Is.True);
            });
        }

        [Test]
        public void CreateConfig_ShouldCreateFormattedJsonWithIndentation()
        {
            var config = new AppSettingStartApp();
            config.PathAppsettings = _testConfigPath;
            config.CreateConfig();
            string content = File.ReadAllText(_testConfigPath);
            Assert.That(content, Does.Contain(Environment.NewLine));
            Assert.That(content.Length, Is.GreaterThan(50));
        }

        [Test]
        public void ReadConfig_WhenFileDoesNotExist_ShouldCreateConfig()
        {
            var config = new AppSettingStartApp();
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
            var originalConfig = new AppSettingStartApp(
                "192.168.1.50:7070",
                _testConfigPath,
                true);
            originalConfig.CreateConfig();
            var loadedConfig = new AppSettingStartApp();
            loadedConfig.PathAppsettings = _testConfigPath;
            loadedConfig.ReadConfig();
            Assert.Multiple(() =>
            {
                Assert.That(loadedConfig.AddressServer, Is.EqualTo(originalConfig.AddressServer));
                Assert.That(loadedConfig.PathAppsettings, Is.EqualTo(originalConfig.PathAppsettings));
                Assert.That(loadedConfig.FirstStart, Is.EqualTo(originalConfig.FirstStart));
            });
        }

        [Test]
        public void ReadConfig_WhenFileExists_ShouldOverwriteCurrentValues()
        {
            var fileConfig = new AppSettingStartApp("10.10.10.10:1234", _testConfigPath, true);
            fileConfig.CreateConfig();
            var config = new AppSettingStartApp("old.address:9999", "old_path.json", false);
            config.PathAppsettings = _testConfigPath;
            config.ReadConfig();
            Assert.Multiple(() =>
            {
                Assert.That(config.AddressServer, Is.EqualTo("10.10.10.10:1234"));
                Assert.That(config.FirstStart, Is.True);
            });
        }

        [Test]
        public void ReadConfig_WhenConfigIsNull_ShouldNotCrash()
        { 
            var config = new AppSettingStartApp();
            config.PathAppsettings = _testConfigPath;
            File.WriteAllText(_testConfigPath, "null");
            Assert.That(() => config.ReadConfig(), Throws.Nothing);
        }

        [Test]
        public void Copy_ShouldCopyAllPropertiesFromSource()
        {
            var sourceConfig = new AppSettingStartApp(
                "source.address:1234",
                @"C:\source\config.json",
                true);
            var targetConfig = new AppSettingStartApp();
            targetConfig.Copy(sourceConfig);
            Assert.Multiple(() =>
            {
                Assert.That(targetConfig.AddressServer, Is.EqualTo(sourceConfig.AddressServer));
                Assert.That(targetConfig.PathAppsettings, Is.EqualTo(sourceConfig.PathAppsettings));
                Assert.That(targetConfig.FirstStart, Is.EqualTo(sourceConfig.FirstStart));
            });
        }

        [Test]
        public void Copy_ShouldCreateIndependentCopy()
        {
            var sourceConfig = new AppSettingStartApp("source:1111", "source.json", false);
            var targetConfig = new AppSettingStartApp();
            targetConfig.Copy(sourceConfig);
            sourceConfig.AddressServer = "changed:9999";
            sourceConfig.FirstStart = true;
            Assert.Multiple(() =>
            {
                Assert.That(targetConfig.AddressServer, Is.EqualTo("source:1111"));
                Assert.That(targetConfig.FirstStart, Is.EqualTo(false));
            });
        }

        [Test]
        public void ToString_ShouldReturnValidJsonRepresentation()
        {
            var config = new AppSettingStartApp(
                "test.server.com:5000",
                _testConfigPath,
                true);
            string jsonString = config.ToString();
            Assert.That(jsonString, Is.Not.Empty);
            Assert.Multiple(() =>
            {
                Assert.That(jsonString, Does.Contain("AddressServer"));
                Assert.That(jsonString, Does.Contain("test.server.com:5000"));
                Assert.That(jsonString, Does.Contain("FirstStart"));
                Assert.That(jsonString, Does.Contain("true"));
            });
            var deserializedConfig = JsonSerializer.Deserialize<AppSettingStartApp>(jsonString);
            Assert.That(deserializedConfig, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(deserializedConfig.AddressServer, Is.EqualTo(config.AddressServer));
                Assert.That(deserializedConfig.FirstStart, Is.EqualTo(config.FirstStart));
            });
        }

        [TestCase("localhost:5005")]
        [TestCase("192.168.1.100:8080")]
        [TestCase("10.0.0.1:3000")]
        [TestCase("myserver.com:443")]
        [TestCase("127.0.0.1:9000")]
        public void AddressServer_ShouldAcceptVariousValues(string address)
        {
            var config = new AppSettingStartApp();
            config.AddressServer = address;
            Assert.That(config.AddressServer, Is.EqualTo(address));
        }

        [TestCase(@"C:\config.json")]
        [TestCase(@"D:\temp\appsettings.json")]
        [TestCase(@"/home/user/config.json")]
        public void PathAppsettings_ShouldAcceptVariousPaths(string path)
        {
            var config = new AppSettingStartApp();
            config.PathAppsettings = path;
            Assert.That(config.PathAppsettings, Is.EqualTo(path));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void FirstStart_ShouldAcceptBothValues(bool firstStart)
        {
            var config = new AppSettingStartApp();
            config.FirstStart = firstStart;
            Assert.That(config.FirstStart, Is.EqualTo(firstStart));
        }

        [Test]
        public void MultipleReadWriteOperations_ShouldPreserveDataIntegrity()
        {
            var originalConfig = new AppSettingStartApp(
                "persistent.server:7777",
                _testConfigPath,
                true);
            originalConfig.CreateConfig();
            var loadedConfig1 = new AppSettingStartApp();
            loadedConfig1.PathAppsettings = _testConfigPath;
            loadedConfig1.ReadConfig();
            var loadedConfig2 = new AppSettingStartApp();
            loadedConfig2.PathAppsettings = _testConfigPath;
            loadedConfig2.ReadConfig();
            Assert.Multiple(() =>
            {
                Assert.That(loadedConfig1.AddressServer, Is.EqualTo(loadedConfig2.AddressServer));
                Assert.That(loadedConfig1.PathAppsettings, Is.EqualTo(loadedConfig2.PathAppsettings));
                Assert.That(loadedConfig1.FirstStart, Is.EqualTo(loadedConfig2.FirstStart));
            });
        }

        [Test]
        public void SaveAndLoad_ShouldPreserveAllData()
        {
            var originalConfig = new AppSettingStartApp(
                "save.load.com:12345",
                _testConfigPath,
                false);
            originalConfig.CreateConfig();
            var loadedConfig = new AppSettingStartApp();
            loadedConfig.PathAppsettings = _testConfigPath;
            loadedConfig.ReadConfig();
            Assert.Multiple(() =>
            {
                Assert.That(loadedConfig.AddressServer, Is.EqualTo(originalConfig.AddressServer));
                Assert.That(loadedConfig.PathAppsettings, Is.EqualTo(originalConfig.PathAppsettings));
                Assert.That(loadedConfig.FirstStart, Is.EqualTo(originalConfig.FirstStart));
            });
        }

        [Test]
        public void ChainedOperations_ShouldWorkCorrectly()
        {
            var config1 = new AppSettingStartApp("first:1111", _testConfigPath, false);
            config1.CreateConfig(); 
            var config2 = new AppSettingStartApp();
            config2.PathAppsettings = _testConfigPath;
            config2.ReadConfig(); 
            var config3 = new AppSettingStartApp();
            config3.Copy(config2); 
            config3.AddressServer = "modified:9999";
            config3.CreateConfig(); 
            var finalConfig = new AppSettingStartApp();
            finalConfig.PathAppsettings = _testConfigPath;
            finalConfig.ReadConfig();
            Assert.Multiple(() =>
            {
                Assert.That(finalConfig.AddressServer, Is.EqualTo("modified:9999"));
                Assert.That(finalConfig.FirstStart, Is.EqualTo(false));
            });
        }

        [Test]
        public void Properties_CanBeSetToEmptyString()
        {
            var config = new AppSettingStartApp();
            config.AddressServer = "";
            config.PathAppsettings = "";
            Assert.Multiple(() =>
            {
                Assert.That(config.AddressServer, Is.EqualTo(""));
                Assert.That(config.PathAppsettings, Is.EqualTo(""));
            });
        }

        [Test]
        public void CreateConfig_WithEmptyAddress_ShouldCreateValidJson()
        {
            var config = new AppSettingStartApp("", _testConfigPath, false);
            config.CreateConfig();
            string content = File.ReadAllText(_testConfigPath);
            var deserialized = JsonSerializer.Deserialize<AppSettingStartApp>(content);
            Assert.That(deserialized, Is.Not.Null);
            Assert.That(deserialized.AddressServer, Is.EqualTo(""));
        }
    }
}
