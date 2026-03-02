namespace SystemNotificationPersonal.StartappGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelHeader = new Panel();
            label1 = new Label();
            pictureBoxLogo = new PictureBox();
            panelServer = new Panel();
            label5 = new Label();
            textBox3 = new TextBox();
            button2 = new Button();
            pictureBox1 = new PictureBox();
            panelLogin = new Panel();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            textBox2 = new TextBox();
            panelRoute = new Panel();
            label6 = new Label();
            textBox4 = new TextBox();
            panelButtons = new Panel();
            button1 = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            panelMain = new Panel();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            panelServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelLogin.SuspendLayout();
            panelRoute.SuspendLayout();
            panelButtons.SuspendLayout();
            statusStrip1.SuspendLayout();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(52, 73, 94);
            panelHeader.Controls.Add(label1);
            panelHeader.Controls.Add(pictureBoxLogo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(749, 80);
            panelHeader.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.White;
            label1.Location = new Point(90, 18);
            label1.Name = "label1";
            label1.Size = new Size(442, 54);
            label1.TabIndex = 1;
            label1.Text = "Система оповещения";
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.Location = new Point(20, 15);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(50, 50);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 0;
            pictureBoxLogo.TabStop = false;
            // 
            // panelServer
            // 
            panelServer.BackColor = Color.White;
            panelServer.BorderStyle = BorderStyle.FixedSingle;
            panelServer.Controls.Add(label5);
            panelServer.Controls.Add(textBox3);
            panelServer.Controls.Add(button2);
            panelServer.Controls.Add(pictureBox1);
            panelServer.Location = new Point(20, 100);
            panelServer.Name = "panelServer";
            panelServer.Size = new Size(710, 100);
            panelServer.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label5.ForeColor = Color.FromArgb(52, 73, 94);
            label5.Location = new Point(15, 15);
            label5.Name = "label5";
            label5.Size = new Size(162, 28);
            label5.TabIndex = 0;
            label5.Text = "Адрес сервера:";
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox3.Location = new Point(15, 46);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(350, 34);
            textBox3.TabIndex = 1;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(52, 152, 219);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button2.ForeColor = Color.White;
            button2.Location = new Point(375, 46);
            button2.Name = "button2";
            button2.Size = new Size(280, 34);
            button2.TabIndex = 2;
            button2.Text = "Запомнить адрес сервера";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(665, 46);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(30, 34);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // panelLogin
            // 
            panelLogin.BackColor = Color.White;
            panelLogin.BorderStyle = BorderStyle.FixedSingle;
            panelLogin.Controls.Add(label2);
            panelLogin.Controls.Add(textBox1);
            panelLogin.Controls.Add(label3);
            panelLogin.Controls.Add(textBox2);
            panelLogin.Location = new Point(20, 210);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(710, 130);
            panelLogin.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.ForeColor = Color.FromArgb(52, 73, 94);
            label2.Location = new Point(15, 15);
            label2.Name = "label2";
            label2.Size = new Size(77, 28);
            label2.TabIndex = 0;
            label2.Text = "Логин:";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox1.Location = new Point(15, 46);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(350, 34);
            textBox1.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label3.ForeColor = Color.FromArgb(52, 73, 94);
            label3.Location = new Point(375, 15);
            label3.Name = "label3";
            label3.Size = new Size(90, 28);
            label3.TabIndex = 2;
            label3.Text = "Пароль:";
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox2.Location = new Point(375, 46);
            textBox2.Name = "textBox2";
            textBox2.PasswordChar = '*';
            textBox2.Size = new Size(320, 34);
            textBox2.TabIndex = 3;
            // 
            // panelRoute
            // 
            panelRoute.BackColor = Color.White;
            panelRoute.BorderStyle = BorderStyle.FixedSingle;
            panelRoute.Controls.Add(label6);
            panelRoute.Controls.Add(textBox4);
            panelRoute.Location = new Point(20, 350);
            panelRoute.Name = "panelRoute";
            panelRoute.Size = new Size(710, 80);
            panelRoute.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label6.ForeColor = Color.FromArgb(52, 73, 94);
            label6.Location = new Point(15, 25);
            label6.Name = "label6";
            label6.Size = new Size(158, 28);
            label6.TabIndex = 0;
            label6.Text = "Тип маршрута:";
            // 
            // textBox4
            // 
            textBox4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox4.Location = new Point(173, 22);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 34);
            textBox4.TabIndex = 1;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.FromArgb(236, 240, 241);
            panelButtons.Controls.Add(button1);
            panelButtons.Location = new Point(20, 440);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(710, 100);
            panelButtons.TabIndex = 4;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(46, 204, 113);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button1.ForeColor = Color.White;
            button1.Location = new Point(200, 25);
            button1.Name = "button1";
            button1.Size = new Size(310, 50);
            button1.TabIndex = 0;
            button1.Text = "Запустить оповещение";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(52, 73, 94);
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 629);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(749, 29);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("Segoe UI", 10F);
            toolStripStatusLabel1.ForeColor = Color.White;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(237, 23);
            toolStripStatusLabel1.Text = "Готов к работе | Версия 1.0.0";
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(236, 240, 241);
            panelMain.Controls.Add(panelServer);
            panelMain.Controls.Add(panelLogin);
            panelMain.Controls.Add(panelRoute);
            panelMain.Controls.Add(panelButtons);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 80);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(749, 549);
            panelMain.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(749, 658);
            Controls.Add(panelMain);
            Controls.Add(panelHeader);
            Controls.Add(statusStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Система оповещения сотрудников";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            panelServer.ResumeLayout(false);
            panelServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            panelRoute.ResumeLayout(false);
            panelRoute.PerformLayout();
            panelButtons.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panelMain.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelHeader;
        private Label label1;
        private PictureBox pictureBoxLogo;
        private Panel panelServer;
        private Label label5;
        private TextBox textBox3;
        private Button button2;
        private PictureBox pictureBox1;
        private Panel panelLogin;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private TextBox textBox2;
        private Panel panelRoute;
        private Label label6;
        private TextBox textBox4;
        private Panel panelButtons;
        private Button button1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panelMain;
    }
}
