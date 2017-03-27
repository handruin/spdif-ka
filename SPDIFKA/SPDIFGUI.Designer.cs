namespace SPDIFKA
{
    partial class SPDIFKAGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SPDIFKAGUI));
            this.startStopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.spdifka = new System.Windows.Forms.NotifyIcon(this.components);
            this.runningLabel = new System.Windows.Forms.Label();
            this.RightClickMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripStart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.TabsMenu1 = new System.Windows.Forms.TabControl();
            this.MainPage = new System.Windows.Forms.TabPage();
            this.TabSettings = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.inaudible_sound = new System.Windows.Forms.RadioButton();
            this.silent_sound = new System.Windows.Forms.RadioButton();
            this.IsRunningCheckBox = new System.Windows.Forms.CheckBox();
            this.IsMinimizedCheckBox = new System.Windows.Forms.CheckBox();
            this.devicesTabPage = new System.Windows.Forms.TabPage();
            this.comboBoxWaveOutDevice = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.IsStartWithWindowsCheckbox = new System.Windows.Forms.CheckBox();
            this.RightClickMenuStrip.SuspendLayout();
            this.TabsMenu1.SuspendLayout();
            this.MainPage.SuspendLayout();
            this.TabSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.devicesTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // startStopButton
            // 
            this.startStopButton.Location = new System.Drawing.Point(16, 29);
            this.startStopButton.Margin = new System.Windows.Forms.Padding(6);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(106, 44);
            this.startStopButton.TabIndex = 0;
            this.startStopButton.Text = "Start";
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "SPDIF Keep Alive:";
            // 
            // spdifka
            // 
            this.spdifka.Icon = ((System.Drawing.Icon)(resources.GetObject("spdifka.Icon")));
            this.spdifka.Text = "SPDIF-KA";
            this.spdifka.Visible = true;
            this.spdifka.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // runningLabel
            // 
            this.runningLabel.AutoSize = true;
            this.runningLabel.Location = new System.Drawing.Point(312, 38);
            this.runningLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.runningLabel.Name = "runningLabel";
            this.runningLabel.Size = new System.Drawing.Size(89, 25);
            this.runningLabel.TabIndex = 2;
            this.runningLabel.Text = "stopped";
            // 
            // RightClickMenuStrip
            // 
            this.RightClickMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.RightClickMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStart,
            this.toolStripSeparator1,
            this.toolStripExit,
            this.toolStripSeparator2,
            this.toolStripAbout});
            this.RightClickMenuStrip.Name = "contextMenuStrip1";
            this.RightClickMenuStrip.Size = new System.Drawing.Size(186, 130);
            // 
            // toolStripStart
            // 
            this.toolStripStart.Name = "toolStripStart";
            this.toolStripStart.Size = new System.Drawing.Size(185, 38);
            this.toolStripStart.Text = "[temp]";
            this.toolStripStart.Click += new System.EventHandler(this.toolStripStart_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(182, 6);
            // 
            // toolStripExit
            // 
            this.toolStripExit.Name = "toolStripExit";
            this.toolStripExit.Size = new System.Drawing.Size(185, 38);
            this.toolStripExit.Text = "Exit";
            this.toolStripExit.Click += new System.EventHandler(this.toolStripExit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(182, 6);
            // 
            // toolStripAbout
            // 
            this.toolStripAbout.Name = "toolStripAbout";
            this.toolStripAbout.Size = new System.Drawing.Size(185, 38);
            this.toolStripAbout.Text = "About";
            this.toolStripAbout.Click += new System.EventHandler(this.toolStripAbout_Click);
            // 
            // TabsMenu1
            // 
            this.TabsMenu1.Controls.Add(this.MainPage);
            this.TabsMenu1.Controls.Add(this.TabSettings);
            this.TabsMenu1.Controls.Add(this.devicesTabPage);
            this.TabsMenu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabsMenu1.Location = new System.Drawing.Point(0, 0);
            this.TabsMenu1.Margin = new System.Windows.Forms.Padding(6);
            this.TabsMenu1.Name = "TabsMenu1";
            this.TabsMenu1.SelectedIndex = 0;
            this.TabsMenu1.Size = new System.Drawing.Size(582, 273);
            this.TabsMenu1.TabIndex = 3;
            // 
            // MainPage
            // 
            this.MainPage.Controls.Add(this.startStopButton);
            this.MainPage.Controls.Add(this.label1);
            this.MainPage.Controls.Add(this.runningLabel);
            this.MainPage.Location = new System.Drawing.Point(8, 39);
            this.MainPage.Margin = new System.Windows.Forms.Padding(6);
            this.MainPage.Name = "MainPage";
            this.MainPage.Padding = new System.Windows.Forms.Padding(6);
            this.MainPage.Size = new System.Drawing.Size(566, 226);
            this.MainPage.TabIndex = 0;
            this.MainPage.Text = "Main";
            this.MainPage.UseVisualStyleBackColor = true;
            // 
            // TabSettings
            // 
            this.TabSettings.Controls.Add(this.IsStartWithWindowsCheckbox);
            this.TabSettings.Controls.Add(this.groupBox1);
            this.TabSettings.Controls.Add(this.IsRunningCheckBox);
            this.TabSettings.Controls.Add(this.IsMinimizedCheckBox);
            this.TabSettings.Location = new System.Drawing.Point(8, 39);
            this.TabSettings.Margin = new System.Windows.Forms.Padding(6);
            this.TabSettings.Name = "TabSettings";
            this.TabSettings.Padding = new System.Windows.Forms.Padding(6);
            this.TabSettings.Size = new System.Drawing.Size(566, 226);
            this.TabSettings.TabIndex = 1;
            this.TabSettings.Text = "Settings";
            this.TabSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.inaudible_sound);
            this.groupBox1.Controls.Add(this.silent_sound);
            this.groupBox1.Location = new System.Drawing.Point(250, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(310, 117);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Audio Type";
            // 
            // inaudible_sound
            // 
            this.inaudible_sound.AutoSize = true;
            this.inaudible_sound.Location = new System.Drawing.Point(12, 27);
            this.inaudible_sound.Margin = new System.Windows.Forms.Padding(6);
            this.inaudible_sound.Name = "inaudible_sound";
            this.inaudible_sound.Size = new System.Drawing.Size(195, 29);
            this.inaudible_sound.TabIndex = 2;
            this.inaudible_sound.TabStop = true;
            this.inaudible_sound.Text = "Inaudible sound";
            this.inaudible_sound.UseVisualStyleBackColor = true;
            this.inaudible_sound.CheckedChanged += new System.EventHandler(this.inaudible_sound_CheckedChanged);
            // 
            // silent_sound
            // 
            this.silent_sound.AutoSize = true;
            this.silent_sound.Location = new System.Drawing.Point(12, 69);
            this.silent_sound.Margin = new System.Windows.Forms.Padding(6);
            this.silent_sound.Name = "silent_sound";
            this.silent_sound.Size = new System.Drawing.Size(114, 29);
            this.silent_sound.TabIndex = 3;
            this.silent_sound.TabStop = true;
            this.silent_sound.Text = "Silence";
            this.silent_sound.UseVisualStyleBackColor = true;
            this.silent_sound.CheckedChanged += new System.EventHandler(this.silent_sound_CheckedChanged);
            // 
            // IsRunningCheckBox
            // 
            this.IsRunningCheckBox.AutoSize = true;
            this.IsRunningCheckBox.Location = new System.Drawing.Point(12, 69);
            this.IsRunningCheckBox.Margin = new System.Windows.Forms.Padding(6);
            this.IsRunningCheckBox.Name = "IsRunningCheckBox";
            this.IsRunningCheckBox.Size = new System.Drawing.Size(175, 29);
            this.IsRunningCheckBox.TabIndex = 1;
            this.IsRunningCheckBox.Text = "Start Running";
            this.IsRunningCheckBox.UseVisualStyleBackColor = true;
            this.IsRunningCheckBox.CheckedChanged += new System.EventHandler(this.IsRunningCheckBox_CheckedChanged);
            // 
            // IsMinimizedCheckBox
            // 
            this.IsMinimizedCheckBox.AutoSize = true;
            this.IsMinimizedCheckBox.Location = new System.Drawing.Point(12, 25);
            this.IsMinimizedCheckBox.Margin = new System.Windows.Forms.Padding(6);
            this.IsMinimizedCheckBox.Name = "IsMinimizedCheckBox";
            this.IsMinimizedCheckBox.Size = new System.Drawing.Size(192, 29);
            this.IsMinimizedCheckBox.TabIndex = 0;
            this.IsMinimizedCheckBox.Text = "Start Minimized";
            this.IsMinimizedCheckBox.UseVisualStyleBackColor = true;
            this.IsMinimizedCheckBox.CheckedChanged += new System.EventHandler(this.IsMinimizedCheckBox_CheckedChanged);
            // 
            // devicesTabPage
            // 
            this.devicesTabPage.Controls.Add(this.comboBoxWaveOutDevice);
            this.devicesTabPage.Controls.Add(this.label2);
            this.devicesTabPage.Location = new System.Drawing.Point(8, 39);
            this.devicesTabPage.Name = "devicesTabPage";
            this.devicesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.devicesTabPage.Size = new System.Drawing.Size(566, 226);
            this.devicesTabPage.TabIndex = 2;
            this.devicesTabPage.Text = "Devices";
            this.devicesTabPage.UseVisualStyleBackColor = true;
            // 
            // comboBoxWaveOutDevice
            // 
            this.comboBoxWaveOutDevice.CheckOnClick = true;
            this.comboBoxWaveOutDevice.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.comboBoxWaveOutDevice.FormattingEnabled = true;
            this.comboBoxWaveOutDevice.Items.AddRange(new object[] {
            "Default playback device"});
            this.comboBoxWaveOutDevice.Location = new System.Drawing.Point(3, 37);
            this.comboBoxWaveOutDevice.Name = "comboBoxWaveOutDevice";
            this.comboBoxWaveOutDevice.ScrollAlwaysVisible = true;
            this.comboBoxWaveOutDevice.Size = new System.Drawing.Size(560, 186);
            this.comboBoxWaveOutDevice.TabIndex = 1;
            this.comboBoxWaveOutDevice.SelectedIndexChanged += new System.EventHandler(this.comboBoxWaveOutDevice_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(484, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select devices on which sound should be played:";
            // 
            // IsStartWithWindowsCheckbox
            // 
            this.IsStartWithWindowsCheckbox.AutoSize = true;
            this.IsStartWithWindowsCheckbox.Location = new System.Drawing.Point(12, 110);
            this.IsStartWithWindowsCheckbox.Margin = new System.Windows.Forms.Padding(6);
            this.IsStartWithWindowsCheckbox.Name = "IsStartWithWindowsCheckbox";
            this.IsStartWithWindowsCheckbox.Size = new System.Drawing.Size(226, 29);
            this.IsStartWithWindowsCheckbox.TabIndex = 5;
            this.IsStartWithWindowsCheckbox.Text = "Start with Windows";
            this.IsStartWithWindowsCheckbox.UseVisualStyleBackColor = true;
            this.IsStartWithWindowsCheckbox.CheckedChanged += new System.EventHandler(this.IsStartWithWindowsCheckbox_CheckedChanged);
            // 
            // SPDIFKAGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 273);
            this.Controls.Add(this.TabsMenu1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "SPDIFKAGUI";
            this.Text = "SPDIF-KA";
            this.RightClickMenuStrip.ResumeLayout(false);
            this.TabsMenu1.ResumeLayout(false);
            this.MainPage.ResumeLayout(false);
            this.MainPage.PerformLayout();
            this.TabSettings.ResumeLayout(false);
            this.TabSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.devicesTabPage.ResumeLayout(false);
            this.devicesTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon spdifka;
        private System.Windows.Forms.Label runningLabel;
        private System.Windows.Forms.ContextMenuStrip RightClickMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripStart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripAbout;
        private System.Windows.Forms.TabControl TabsMenu1;
        private System.Windows.Forms.TabPage MainPage;
        private System.Windows.Forms.TabPage TabSettings;
        private System.Windows.Forms.CheckBox IsRunningCheckBox;
        private System.Windows.Forms.CheckBox IsMinimizedCheckBox;
        private System.Windows.Forms.RadioButton silent_sound;
        private System.Windows.Forms.RadioButton inaudible_sound;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage devicesTabPage;
        private System.Windows.Forms.CheckedListBox comboBoxWaveOutDevice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox IsStartWithWindowsCheckbox;
    }
}

