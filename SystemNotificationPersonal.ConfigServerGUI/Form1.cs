using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics;
using System.Text;
using SystemNotificationPersonal.Core.Models;
using SystemNotificationPersonal.DataAccess.Sqlite;
using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;
using SystemNotificationPersonal.DataAccess.Sqlite.Repositories;

namespace SystemNotificationPersonal.ConfigServerGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _settings = new();
            _settings.ReadConfig();
            if (_settings.FirstStart)
            {
                _settings.CreateConfig();
            }
            _defautSetting = new();
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddDbContext<SystemNotificationDbContext>(options =>
                options.UseSqlite(_settings.ConnectionString));
            _serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            SetupToolTip();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("")
                .CreateLogger();
        }

        private AppSettingServer _settings;
        private AppSettingServer _defautSetting;
        private ServiceCollection _serviceCollection;
        private ServiceProvider _serviceProvider;
        private ToolTip? toolTip;

        private void button3_Click(object sender, EventArgs e)
        {
            _settings.Copy(_defautSetting);
            label8.Text = "Текущее значение " + _settings.Port;
            label3.Text = "Текущее значение " + _settings.ConnectionString;
            label5.Text = "Текущее значение " + _settings.IPAddressCors;
            label14.Text = "Текущее значение " + _settings.Protocol;
            MessageBox.Show("Конфигурация успешно сброшена");
            Log.Information("Конфигурация сброшена до стандартных");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_settings.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty) _settings.Port = Convert.ToInt32(textBox1.Text);
            if (textBox2.Text != string.Empty) _settings.ConnectionString = "Data Source=" + textBox2.Text;
            if (textBox4.Text != string.Empty) _settings.IPAddressCors = textBox3.Text;
            if (textBox10.Text != string.Empty) _settings.Protocol = textBox10.Text;
            _settings.CreateConfig();
            label8.Text = "Текущее значение " + _settings.Port;
            label3.Text = "Текущее значение " + _settings.ConnectionString;
            label5.Text = "Текущее значение " + _settings.IPAddressCors;
            label14.Text = "Текущее значение " + _settings.Protocol;
            MessageBox.Show("Конфигурация успешно обновлена");
            Log.Information($"Конфигурация была обновлена, значения: {_settings.Port}," +
                $" {_settings.ConnectionString}, {_settings.IPAddressCors}, {_settings.Protocol}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label8.Text = "Текущее значение " + _settings.Port;
            label3.Text = "Текущее значение " + _settings.ConnectionString;
            label5.Text = "Текущее значение " + _settings.IPAddressCors;
            label14.Text = "Текущее значение " + _settings.Protocol;
            if (label8.Text.Length >= 80)
            {
                label8.Text = SetShortText(label8.Text, 80);
                toolTip?.SetToolTip(label8,
                    "Текущее значение " + _settings.Port);
            }
            if (label3.Text.Length >= 80)
            {
                label3.Text = SetShortText(label3.Text, 80);
                toolTip?.SetToolTip(label3,
                    "Текущее значение " + _settings.ConnectionString);
            }
            if (label5.Text.Length >= 80)
            {
                label5.Text = SetShortText(label5.Text, 80);
                toolTip?.SetToolTip(label5,
                    "Текущее значение " + _settings.IPAddressCors);
            }
            if (label14.Text.Length >= 80)
            {
                label14.Text = SetShortText(label14.Text, 80);
                toolTip?.SetToolTip(label14,
                    "Текущее значение " + _settings.Protocol);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text == string.Empty || textBox5.Text == string.Empty)
                {
                    MessageBox.Show("Необходимо ввсети логин и пароль");
                    Log.Warning("Ошибка ввода логина или пароля");
                    return;
                }
                string login = textBox4.Text;
                string password = textBox5.Text;
                var userRepo = _serviceProvider.GetService<IUsersRepository>();
                UsersEntity users = new UsersEntity()
                {
                    Id = Guid.NewGuid(),
                    Login = login,
                    Password = password
                };
                await userRepo!.AddAsync(users);
                MessageBox.Show("Профиль успешно добавлен");
                Log.Information($"Профиль добвлен: {users.Login}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                Log.Error(ex.Message);
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text == string.Empty || textBox7.Text == string.Empty
                    || textBox8.Text == string.Empty)
                {
                    MessageBox.Show("Необходимо ввсети логин и пароли");
                    Log.Warning("Ошибка ввода логина или пароля");
                    return;
                }
                string login = textBox6.Text;
                string oldPassword = textBox7.Text;
                string newPassword = textBox8.Text;
                var userRepo = _serviceProvider.GetService<IUsersRepository>();
                UsersEntity users = new UsersEntity()
                {
                    Id = Guid.NewGuid(),
                    Login = login,
                    Password = oldPassword
                };
                if (!await userRepo!.VerifyAsync(users))
                {
                    MessageBox.Show("Необходимо ввсети логин и пароли");
                    Log.Warning("Ошибка ввода логина или пароля");
                    return;
                }
                users.Password = newPassword;
                await userRepo!.UpdatePasswordAsync(users);
                MessageBox.Show($"Пароль профиля {login} успешно обновлен");
                Log.Information($"Пароль профиля {login} обновлен");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                Log.Error($"{ex.Message}");
            }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox9.Text == string.Empty)
                {
                    MessageBox.Show("Необходимо ввсети логин");
                    return;
                }
                string login = textBox9.Text;
                var userRepo = _serviceProvider.GetService<IUsersRepository>();
                await userRepo!.DeleteAsync(login);
                MessageBox.Show($"Профиль {login} удален");
                Log.Information($"Профиль {login} удален");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                Log.Error(ex.Message );
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string appPath = _settings.PathExe;
                var processInfo = new ProcessStartInfo
                {
                    FileName = appPath,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };
                Process.Start(processInfo);
                Log.Information("Сервер запущен");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Log.Error(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string appPath = _settings.PathExe;
                var processInfo = new ProcessStartInfo
                {
                    FileName = appPath,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal,
                    Verb = "runas"
                };
                Process.Start(processInfo);
                Log.Information("Сервер запущен с правами администратора");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Log.Error(ex.Message);
            }
        }

        private void SetupToolTip()
        {
            pictureBox1.Image = SystemIcons.Question.ToBitmap();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Image = SystemIcons.Question.ToBitmap();
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.Image = SystemIcons.Question.ToBitmap();
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.Image = SystemIcons.Question.ToBitmap();
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
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
                "Номер сетевого порта для подключения к серверу.\n" +
                "Определяет точку входа для сетевых соединений.\n\n" +
                "Подробнее в документации.");

            toolTip.SetToolTip(pictureBox2,
               "Локальный путь к файлу базы данных.\n" +
               "Указывает расположение и параметры подключения к СУБД.\n\n" +
               "Подробнее в документации.");

            toolTip.SetToolTip(pictureBox3,
               "Пулы IP-адресов разрешенные для подключения.\n" +
               "Определяет какие клиентские адреса могут обращаться к системе.\n\n" +
               "Подробнее в документации.");

            toolTip.SetToolTip(pictureBox4,
               "Правила обмена данными между клиентом и сервером.\n" +
               "Определяет формат, структуру и порядок передачи сообщений.\n\n" +
               "Подробнее в документации.");
        }

        private string SetShortText(string text, int count)
        {
            StringBuilder sb = new();
            for (int i = 0; i < count; i++)
            {
                sb.Append(text[i]);
            }
            sb.Append("...");
            return sb.ToString();
        }
    }
}
