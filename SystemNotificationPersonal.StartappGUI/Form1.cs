using Serilog;
using System.Net;
using System.Text;
using System.Text.Json;
using SystemNotificationPersonal.Core.Models;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.StartappGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("D:\\projects\\projects\\SystemNotificationPersonal\\SystemNotificationPersonal.StartappGUI\\log.txt")
                .CreateLogger();
            _settings = new();
            if (!File.Exists(_settings.PathAppsettings))
            {
                MessageBox.Show("Используя данное программное обеспечение, вы принимаете все условия лицензии. См LISENCE.txt");
            }
            _settings.ReadConfig();
            if (_settings.FirstStart)
            {
                _settings.CreateConfig();
            }
            textBox4.Text = "1";
            textBox3.Text = _settings.AddressServer;
            Log.Information("Приложение запущено");
            SetupToolTip();
        }

        private bool _isNotify = false;
        private AppSettingStartApp _settings;
        private ToolTip? toolTip;

        private async void button1_Click(object sender, EventArgs e)
        {
            if (_isNotify)
            {
                string apiUrl = $"http://{_settings.AddressServer}/stop";
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(apiUrl);
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                string responseBody = await response.Content.ReadAsStringAsync();
                                MessageBox.Show($"Успешно!");
                                _isNotify = false;
                                button1.Text = "Запустить оповещение";
                                Log.Information("Оповещение остановлено");
                                break;
                            default:
                                MessageBox.Show($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                                Log.Error($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                    Log.Error($"Произошла ошибка: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    string login = textBox1.Text;
                    string password = textBox2.Text;
                    string adressServer = textBox3.Text;
                    int variableExit = Convert.ToInt32(textBox4.Text);
                    string apiUrl = $"http://{adressServer}/start";
                    if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Логин и пароль не может иметь пустые значения");
                        Log.Warning("Неправильный ввод логина или пароля");
                        return;
                    }
                    if (string.IsNullOrEmpty(adressServer))
                    {
                        MessageBox.Show("Адрес сервера не может иметь пустые значения");
                        Log.Warning("Неправильный ввод адреса сервера");
                        return;
                    }
                    if (variableExit <= 0 || variableExit >= 6)
                    {
                        MessageBox.Show("Тип маршрута может быть только от 1 до 5");
                        Log.Warning("Неправильный ввод типа маршрута");
                        return;
                    }
                    UsersEntity user = new()
                    {
                        Login = login,
                        Password = password
                    };
                    LoginRequest req = new LoginRequest()
                    {
                        Id = user.Id,
                        Login = user.Login,
                        Password = user.Password,
                        VariableExit = variableExit,
                    };
                    using (var httpClient = new HttpClient())
                    {
                        string json = JsonSerializer.Serialize(req);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(apiUrl, content);
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                string responseBody = await response.Content.ReadAsStringAsync();
                                MessageBox.Show(responseBody);
                                button1.Text = "Остановить оповещение";
                                _isNotify = true;
                                Log.Information($"Оповещение запущено от {req.Login}");
                                break;
                            default:
                                MessageBox.Show($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                                Log.Error($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                    Log.Error($"Произошла ошибка: {ex.Message}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string adressServer = textBox3.Text;
            if (string.IsNullOrEmpty(adressServer))
            {
                MessageBox.Show("Адрес сервера не может иметь пустые значения");
                return;
            }
            _settings.AddressServer = adressServer;
            _settings.CreateConfig();
        }

        private void SetupToolTip()
        {
            pictureBox1.Image = SystemIcons.Question.ToBitmap();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            toolTip = new ToolTip
            {
                ToolTipTitle = "Подробное объяснение",
                ForeColor = Color.Black,
                BackColor = Color.LightYellow,
                IsBalloon = true,
                AutoPopDelay = 5000,
                InitialDelay = 100,
                ReshowDelay = 500,
                UseAnimation = true,
                UseFading = true
            };
            toolTip.SetToolTip(pictureBox1,
                "URL или IP-адрес сервера для подключения клиента.\n" +
                "Определяет куда приложение будет отправлять запросы или откуда получать данные.\n\n" +
                "Подробнее в документации.");
        }
    }
}
