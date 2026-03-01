namespace SystemNotificationPersonal.GUI
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
            components = new System.ComponentModel.Container();
            pictureBoxSchemaExit = new PictureBox();
            labelForOffPC = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            labelTime = new Label();
            labelHeader = new Label();
            labelForWarning = new Label();
            buttonMinimize = new Button();
            labelForCode = new Label();
            textBoxCodeForExit = new TextBox();
            buttonExit = new Button();
            label1 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSchemaExit).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxSchemaExit
            // 
            pictureBoxSchemaExit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxSchemaExit.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBoxSchemaExit.Location = new Point(194, 135);
            pictureBoxSchemaExit.Name = "pictureBoxSchemaExit";
            pictureBoxSchemaExit.Size = new Size(1014, 463);
            pictureBoxSchemaExit.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxSchemaExit.TabIndex = 0;
            pictureBoxSchemaExit.TabStop = false;
            // 
            // labelForOffPC
            // 
            labelForOffPC.Anchor = AnchorStyles.Bottom;
            labelForOffPC.AutoSize = true;
            labelForOffPC.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelForOffPC.Location = new Point(209, 627);
            labelForOffPC.Name = "labelForOffPC";
            labelForOffPC.Size = new Size(473, 38);
            labelForOffPC.TabIndex = 2;
            labelForOffPC.Text = "Компьютер будет выключен через:";
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // labelTime
            // 
            labelTime.Anchor = AnchorStyles.Bottom;
            labelTime.AutoSize = true;
            labelTime.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelTime.Location = new Point(705, 627);
            labelTime.Name = "labelTime";
            labelTime.Size = new Size(98, 38);
            labelTime.TabIndex = 3;
            labelTime.Text = "Время";
            // 
            // labelHeader
            // 
            labelHeader.Anchor = AnchorStyles.Top;
            labelHeader.AutoSize = true;
            labelHeader.Font = new Font("Segoe UI", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelHeader.Location = new Point(338, 55);
            labelHeader.Name = "labelHeader";
            labelHeader.Size = new Size(780, 62);
            labelHeader.TabIndex = 4;
            labelHeader.Text = "Пожалуйста, покиньте помещение";
            labelHeader.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelForWarning
            // 
            labelForWarning.Anchor = AnchorStyles.Bottom;
            labelForWarning.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 204);
            labelForWarning.Location = new Point(209, 680);
            labelForWarning.Name = "labelForWarning";
            labelForWarning.Size = new Size(696, 67);
            labelForWarning.TabIndex = 5;
            labelForWarning.Text = "Если у вас открыты какие-либо приложения, пожалуйста, закройте их, в противном случае вы можете потерять свои данные\r\n";
            // 
            // buttonMinimize
            // 
            buttonMinimize.Anchor = AnchorStyles.Bottom;
            buttonMinimize.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonMinimize.Location = new Point(936, 716);
            buttonMinimize.Name = "buttonMinimize";
            buttonMinimize.Size = new Size(272, 83);
            buttonMinimize.TabIndex = 6;
            buttonMinimize.Text = "Свернуть\r\n";
            buttonMinimize.UseVisualStyleBackColor = true;
            buttonMinimize.Click += button2_Click;
            // 
            // labelForCode
            // 
            labelForCode.Anchor = AnchorStyles.Bottom;
            labelForCode.AutoSize = true;
            labelForCode.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 204);
            labelForCode.Location = new Point(209, 771);
            labelForCode.Name = "labelForCode";
            labelForCode.Size = new Size(419, 28);
            labelForCode.TabIndex = 7;
            labelForCode.Text = "Введите код, что бы закрыть приложение:\r\n";
            // 
            // textBoxCodeForExit
            // 
            textBoxCodeForExit.Anchor = AnchorStyles.Bottom;
            textBoxCodeForExit.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxCodeForExit.Location = new Point(656, 765);
            textBoxCodeForExit.Name = "textBoxCodeForExit";
            textBoxCodeForExit.Size = new Size(218, 34);
            textBoxCodeForExit.TabIndex = 8;
            // 
            // buttonExit
            // 
            buttonExit.Anchor = AnchorStyles.Bottom;
            buttonExit.Location = new Point(880, 765);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(43, 34);
            buttonExit.TabIndex = 9;
            buttonExit.Text = "X";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(106, 117);
            label1.Name = "label1";
            label1.Size = new Size(242, 31);
            label1.TabIndex = 10;
            label1.Text = "Покиньте помещение";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(142, 868);
            panel1.TabIndex = 11;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel2.Location = new Point(1240, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(142, 868);
            panel2.TabIndex = 12;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel3.Location = new Point(154, 12);
            panel3.Name = "panel3";
            panel3.Size = new Size(1087, 53);
            panel3.TabIndex = 13;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel4.Location = new Point(154, 827);
            panel4.Name = "panel4";
            panel4.Size = new Size(1087, 53);
            panel4.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1394, 892);
            ControlBox = false;
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(buttonExit);
            Controls.Add(textBoxCodeForExit);
            Controls.Add(labelForCode);
            Controls.Add(buttonMinimize);
            Controls.Add(labelForWarning);
            Controls.Add(labelHeader);
            Controls.Add(labelTime);
            Controls.Add(labelForOffPC);
            Controls.Add(pictureBoxSchemaExit);
            Name = "Form1";
            Text = "SystemNotificationPeople";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxSchemaExit).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxSchemaExit;
        private Label labelForOffPC;
        private System.Windows.Forms.Timer timer1;
        private Label labelTime;
        private Label labelHeader;
        private Label labelForWarning;
        private Button buttonMinimize;
        private Label labelForCode;
        private TextBox textBoxCodeForExit;
        private Button buttonExit;
        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
    }
}
