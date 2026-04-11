using Serilog;
using System.Diagnostics;
using System.Text;
using SystemNotificationPersonal.Core.Models;

namespace SystemNotificationPersonal.ConfigClientGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _settings = new();
            _defaultSettings = new();
            _settings.ReadConfig();
            if (_settings.FirstStart)
            {
                _settings.CreateConfig();
            }
            SetupToolTip();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("D:\\documents\\logsSNP\\logConfigClientGUI.txt")
                .CreateLogger();
        }

        private AppSettingClient _settings;
        private AppSettingClient _defaultSettings;
        private ToolTip? toolTip;

        private void Form1_Load(object sender, EventArgs e)
        {
            label8.Text = "Текущее значение " + _settings.AddressServer;
            label9.Text = "Текущее значение " + _settings.PathExe;
            label10.Text = "Текущее значение " + _settings.Theme;
            label12.Text = "Текущее значение " + _settings.Header;
            label13.Text = "Текущее значение " + _settings.TimeBeforeOffPC;
            if (label8.Text.Length >= 80)
            {
                label8.Text = SetShortText(label8.Text, 80);
                toolTip?.SetToolTip(label8,
                    "Текущее значение " + _settings.AddressServer);
            }
            if (label9.Text.Length >= 80)
            {
                label9.Text = SetShortText(label9.Text, 80);
                toolTip?.SetToolTip(label9,
                    "Текущее значение " + _settings.PathExe);
            }
            if (label10.Text.Length >= 80)
            {
                label10.Text = SetShortText(label10.Text, 80);
                toolTip?.SetToolTip(label10,
                    "Текущее значение " + _settings.Theme);
            }
            if (label12.Text.Length >= 80)
            {
                label12.Text = SetShortText(label12.Text, 80);
                toolTip?.SetToolTip(label12,
                    "Текущее значение " + _settings.Header);
            }
            if (label13.Text.Length >= 80)
            {
                label13.Text = SetShortText(label13.Text, 80);
                toolTip?.SetToolTip(label13,
                    "Текущее значение " + _settings.TimeBeforeOffPC);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != string.Empty) _settings.AddressServer = textBox1.Text;
                if (textBox2.Text != string.Empty) _settings.PathExe = textBox2.Text;
                if (textBox3.Text != string.Empty) _settings.Theme = textBox3.Text;
                if (textBox5.Text != string.Empty) _settings.Header = textBox5.Text;
                if (textBox6.Text != string.Empty) _settings.TimeBeforeOffPC = Convert.ToInt32(textBox6.Text);
                _settings.CreateConfig();
                label8.Text = "Текущее значение " + _settings.AddressServer;
                label9.Text = "Текущее значение " + _settings.PathExe;
                label10.Text = "Текущее значение " + _settings.Theme;
                label12.Text = "Текущее значение " + _settings.Header;
                label13.Text = "Текущее значение " + _settings.TimeBeforeOffPC;
                MessageBox.Show("Конфигурация успешно обновлена");
                Log.Information("Конфигурация обновлена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Error(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_settings.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _settings.Copy(_defaultSettings);
            _settings.CreateConfig();
            label8.Text = "Текущее значение " + _settings.AddressServer;
            label9.Text = "Текущее значение " + _settings.PathExe;
            label10.Text = "Текущее значение " + _settings.Theme;
            label12.Text = "Текущее значение " + _settings.Header;
            label13.Text = "Текущее значение " + _settings.TimeBeforeOffPC;
            MessageBox.Show("Конфигурация успешно сброшена");
            Log.Information("Конфигурация сброшена");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string appPath = Directory.GetCurrentDirectory() + "\\SystemNotificationPeopleBackTask.exe";
                var processInfo = new ProcessStartInfo
                {
                    FileName = appPath,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };
                Process.Start(processInfo);
                Log.Information("Фоновая задача запущена");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Information(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string appPath = Directory.GetCurrentDirectory() + "\\SystemNotificationPeopleBackTask.exe";
                var processInfo = new ProcessStartInfo
                {
                    FileName = appPath,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal,
                    Verb = "runas"
                };
                Process.Start(processInfo);
                Log.Information("Фоновая задача запущена с правами администраторра");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Log.Information(ex.Message);
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
            pictureBox5.Image = SystemIcons.Question.ToBitmap();
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
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

            toolTip.SetToolTip(pictureBox2,
               "Полный путь к EXE-файлу главного приложения.\n" +
               "Определяет какое приложение будет запускаться у пользователя при старте оповещения.\n\n" +
               "Подробнее в документации.");

            toolTip.SetToolTip(pictureBox3,
               "Визуальное оформление интерфейса.\n" +
               "Влияет на внешний вид всех элементов управления.\n\n" +
               "Подробнее в документации.");

            toolTip.SetToolTip(pictureBox4,
               "Текст в заголовке окна главного приложения.\n" +
               "Отображается в верху главного приложения и информирует пользователя\n\n" +
               "Подробнее в документации.");

            toolTip.SetToolTip(pictureBox5,
               "Таймер обратного отсчета перед автоматическим завершением работы системы.\n" +
               "Указывается в секундах.\n\n" +
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
