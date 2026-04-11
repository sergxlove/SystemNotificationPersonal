using Serilog;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using SystemNotificationPersonal.Core.Models;

namespace SystemNotificationPersonal.GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += (sender, e) => SetForegroundWindow(this.Handle);
            _appSetting = new AppSettingClient();
            _appSetting.ReadConfig();
            if (_appSetting.FirstStart)
            {
                _appSetting.CreateConfig();
            }
            WindowState = FormWindowState.Normal;
            _originalSize = Size;
            _positionTimer = labelTime.Location;
            _positionButtonMinimize = buttonMinimize.Location;
            WindowState = FormWindowState.Maximized;
            _pathDirectoryPhoto = Directory.GetCurrentDirectory();
            switch (_appSetting.VariableExit)
            {
                case 1:
                    _pathDirectoryPhoto += "\\Images\\image1.png";
                    break;
                case 2:
                    _pathDirectoryPhoto += "\\Images\\image2.png";
                    break;
                case 3:
                    _pathDirectoryPhoto += "\\Images\\image3.png";
                    break;
                case 4:
                    _pathDirectoryPhoto += "\\Images\\image4.png";
                    break;
                case 5:
                    _pathDirectoryPhoto += "\\Images\\image5.png";
                    break;
                default:
                    _pathDirectoryPhoto += "\\Images\\image1.png";
                    break;
            }
            LoadImage(_pathDirectoryPhoto);
            InitializeTimer();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("D:\\documents\\logsSNP\\logGUI.txt")
                .CreateLogger();
            Log.Information("Приложение успешно запущено");
        }

        private AppSettingClient _appSetting;
        private int _initialSeconds = 180;
        //private bool _isOff = true;
        private bool _isCompactMode = false;
        private Size _originalSize;
        private Point _positionTimer;
        private Point _positionButtonMinimize;
        private string _pathDirectoryPhoto;
        private System.Windows.Forms.Timer? blinkTimer;
        private int brightness = 0;
        private bool increasing = true;
        private const int step = 5;

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_appSetting.Theme == "dark") SetDarkTheme();
            labelHeader.Text = _appSetting.Header;
            _initialSeconds = _appSetting.TimeBeforeOffPC;
            label1.Visible = false;
            timer1.Start();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (_isOff)
        //    {
        //        timer1.Stop();
        //        labelTime.Visible = false;
        //        labelForOffPC.Text = "Компьютер не будет выключен";
        //        buttonNoOffPC.Text = "Выключить мой компьютер";
        //        _isOff = false;
        //    }
        //    else
        //    {
        //        timer1.Start();
        //        labelTime.Visible = true;
        //        labelForOffPC.Text = "Компьютер будет выключен через:";
        //        buttonNoOffPC.Text = "Не выключать мой компьютер";
        //        _isOff = true;
        //    }
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            _initialSeconds--;
            UpdateDisplay();
            if (_initialSeconds < 0)
            {
                timer1.Stop();
                ShutdownComputer();
            }
        }

        private void UpdateDisplay()
        {
            labelTime.Text = TimeSpan.FromSeconds(_initialSeconds).ToString(@"mm\:ss");
        }

        private void ShutdownComputer()
        {
            Log.Information("Выключение клмпбютера");
            //Process.Start("shutdown", "/s /f /t 0");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_isCompactMode)
            {
                panel1.Visible = true;
                panel2.Visible = true;
                panel3.Visible = true;
                panel4.Visible = true;
                Size = _originalSize;
                pictureBoxSchemaExit.Visible = true;
                labelForOffPC.Visible = true;
                labelHeader.Visible = true;
                labelForWarning.Visible = true;
                labelForCode.Visible = true;
                textBoxCodeForExit.Visible = true;
                buttonExit.Visible = true;
                labelTime.Location = _positionTimer;
                buttonMinimize.Location = _positionButtonMinimize;
                _isCompactMode = false;
                WindowState = FormWindowState.Maximized;
                label1.Visible = false;
            }
            else
            {
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                pictureBoxSchemaExit.Visible = false;
                labelForOffPC.Visible = false;
                labelHeader.Visible = false;
                labelForWarning.Visible = false;
                labelForCode.Visible = false;
                textBoxCodeForExit.Visible = false;
                buttonExit.Visible = false;
                WindowState = FormWindowState.Normal;
                Size = new Size(500, 200);
                labelTime.Location = new Point(20, 20);
                buttonMinimize.Location = new Point(150, 20);
                _isCompactMode = true;
                label1.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string code = textBoxCodeForExit.Text;
            string codeHash = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(code);
            SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(bytes);
            codeHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            _appSetting.ReadConfig();
            if (codeHash == _appSetting.CodeHash)
            {
                Log.Information($"Приложение закрыто с помощью кода: {code}");
                this.Close();
            }
            else
            {
                Log.Warning($"Ошибка закрытия приложения с помощью кода: {code}");
                textBoxCodeForExit.ForeColor = Color.Red;
            }
        }

        private void SetDarkTheme()
        {
            this.BackColor = Color.FromArgb(69, 69, 69);
            labelHeader.ForeColor = Color.White;
            labelTime.ForeColor = Color.White;
            labelForCode.ForeColor = Color.White;
            labelForWarning.ForeColor = Color.White;
            labelForOffPC.ForeColor = Color.White;
            textBoxCodeForExit.BackColor = Color.FromArgb(69, 69, 69);
            textBoxCodeForExit.ForeColor = Color.White;
            buttonExit.BackColor = Color.FromArgb(69, 69, 69);
            buttonExit.ForeColor = Color.White;
            buttonMinimize.BackColor = Color.FromArgb(69, 69, 69);
            buttonMinimize.ForeColor = Color.White;
        }

        private void LoadImage(string pathFile)
        {
            try
            {
                if (File.Exists(pathFile))
                {
                    pictureBoxSchemaExit.Image?.Dispose();
                    pictureBoxSchemaExit.Image = Image.FromFile(pathFile);
                }
            }
            catch { }
        }

        private void InitializeTimer()
        {
            blinkTimer = new System.Windows.Forms.Timer
            {
                Interval = 30
            };
            blinkTimer.Tick += BlinkTimer_Tick!;
            blinkTimer.Start();
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            if (increasing)
            {
                brightness += step;
                if (brightness >= 220)
                {
                    brightness = 220;
                    increasing = false;
                }
            }
            else
            {
                brightness -= step;
                if (brightness <= 0)
                {
                    brightness = 0;
                    increasing = true;
                }
            }

            panel1.BackColor = Color.FromArgb(255, brightness, brightness);
            panel2.BackColor = Color.FromArgb(255, brightness, brightness);
            panel3.BackColor = Color.FromArgb(255, brightness, brightness);
            panel4.BackColor = Color.FromArgb(255, brightness, brightness);

        }
    }
}
