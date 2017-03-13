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
            this.IsRunningCheckBox = new System.Windows.Forms.CheckBox();
            this.IsMinimizedCheckBox = new System.Windows.Forms.CheckBox();
            this.inaudible_sound = new System.Windows.Forms.RadioButton();
            this.silent_sound = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RightClickMenuStrip.SuspendLayout();
            this.TabsMenu1.SuspendLayout();
            this.MainPage.SuspendLayout();
            this.TabSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startStopButton
            // 
            this.startStopButton.Location = new System.Drawing.Point(8, 15);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(53, 23);
            this.startStopButton.TabIndex = 0;
            this.startStopButton.Text = "Start";
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
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
            this.runningLabel.Location = new System.Drawing.Point(156, 20);
            this.runningLabel.Name = "runningLabel";
            this.runningLabel.Size = new System.Drawing.Size(45, 13);
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
            this.RightClickMenuStrip.Size = new System.Drawing.Size(150, 106);
            // 
            // toolStripStart
            // 
            this.toolStripStart.Name = "toolStripStart";
            this.toolStripStart.Size = new System.Drawing.Size(149, 30);
            this.toolStripStart.Text = "[temp]";
            this.toolStripStart.Click += new System.EventHandler(this.toolStripStart_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(146, 6);
            // 
            // toolStripExit
            // 
            this.toolStripExit.Name = "toolStripExit";
            this.toolStripExit.Size = new System.Drawing.Size(149, 30);
            this.toolStripExit.Text = "Exit";
            this.toolStripExit.Click += new System.EventHandler(this.toolStripExit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(146, 6);
            // 
            // toolStripAbout
            // 
            this.toolStripAbout.Name = "toolStripAbout";
            this.toolStripAbout.Size = new System.Drawing.Size(149, 30);
            this.toolStripAbout.Text = "About";
            this.toolStripAbout.Click += new System.EventHandler(this.toolStripAbout_Click);
            // 
            // TabsMenu1
            // 
            this.TabsMenu1.Controls.Add(this.MainPage);
            this.TabsMenu1.Controls.Add(this.TabSettings);
            this.TabsMenu1.Location = new System.Drawing.Point(3, 2);
            this.TabsMenu1.Name = "TabsMenu1";
            this.TabsMenu1.SelectedIndex = 0;
            this.TabsMenu1.Size = new System.Drawing.Size(291, 91);
            this.TabsMenu1.TabIndex = 3;
            // 
            // MainPage
            // 
            this.MainPage.Controls.Add(this.startStopButton);
            this.MainPage.Controls.Add(this.label1);
            this.MainPage.Controls.Add(this.runningLabel);
            this.MainPage.Location = new System.Drawing.Point(4, 22);
            this.MainPage.Name = "MainPage";
            this.MainPage.Padding = new System.Windows.Forms.Padding(3);
            this.MainPage.Size = new System.Drawing.Size(283, 52);
            this.MainPage.TabIndex = 0;
            this.MainPage.Text = "Main";
            this.MainPage.UseVisualStyleBackColor = true;
            // 
            // TabSettings
            // 
            this.TabSettings.Controls.Add(this.groupBox1);
            this.TabSettings.Controls.Add(this.IsRunningCheckBox);
            this.TabSettings.Controls.Add(this.IsMinimizedCheckBox);
            this.TabSettings.Location = new System.Drawing.Point(4, 22);
            this.TabSettings.Name = "TabSettings";
            this.TabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.TabSettings.Size = new System.Drawing.Size(283, 65);
            this.TabSettings.TabIndex = 1;
            this.TabSettings.Text = "Settings";
            this.TabSettings.UseVisualStyleBackColor = true;
            // 
            // IsRunningCheckBox
            // 
            this.IsRunningCheckBox.AutoSize = true;
            this.IsRunningCheckBox.Location = new System.Drawing.Point(6, 36);
            this.IsRunningCheckBox.Name = "IsRunningCheckBox";
            this.IsRunningCheckBox.Size = new System.Drawing.Size(98, 21);
            this.IsRunningCheckBox.TabIndex = 1;
            this.IsRunningCheckBox.Text = "Start Running";
            this.IsRunningCheckBox.UseVisualStyleBackColor = true;
            this.IsRunningCheckBox.CheckedChanged += new System.EventHandler(this.IsRunningCheckBox_CheckedChanged);
            // 
            // IsMinimizedCheckBox
            // 
            this.IsMinimizedCheckBox.AutoSize = true;
            this.IsMinimizedCheckBox.Location = new System.Drawing.Point(6, 13);
            this.IsMinimizedCheckBox.Name = "IsMinimizedCheckBox";
            this.IsMinimizedCheckBox.Size = new System.Drawing.Size(104, 21);
            this.IsMinimizedCheckBox.TabIndex = 0;
            this.IsMinimizedCheckBox.Text = "Start Minimized";
            this.IsMinimizedCheckBox.UseVisualStyleBackColor = true;
            this.IsMinimizedCheckBox.CheckedChanged += new System.EventHandler(this.IsMinimizedCheckBox_CheckedChanged);
            // 
            // inaudible_sound
            // 
            this.inaudible_sound.AutoSize = true;
            this.inaudible_sound.Location = new System.Drawing.Point(6, 14);
            this.inaudible_sound.Name = "inaudible_sound";
            this.inaudible_sound.Size = new System.Drawing.Size(107, 20);
            this.inaudible_sound.TabIndex = 2;
            this.inaudible_sound.TabStop = true;
            this.inaudible_sound.Text = "Inaudible sound";
            this.inaudible_sound.UseVisualStyleBackColor = true;
            this.inaudible_sound.CheckedChanged += new System.EventHandler(this.inaudible_sound_CheckedChanged);
            // 
            // silent_sound
            // 
            this.silent_sound.AutoSize = true;
            this.silent_sound.Location = new System.Drawing.Point(6, 36);
            this.silent_sound.Name = "silent_sound";
            this.silent_sound.Size = new System.Drawing.Size(67, 20);
            this.silent_sound.TabIndex = 3;
            this.silent_sound.TabStop = true;
            this.silent_sound.Text = "Silence";
            this.silent_sound.UseVisualStyleBackColor = true;
            this.silent_sound.CheckedChanged += new System.EventHandler(this.silent_sound_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.inaudible_sound);
            this.groupBox1.Controls.Add(this.silent_sound);
            this.groupBox1.Location = new System.Drawing.Point(125, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 61);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Audio Type";
            // 
            // SPDIFKAGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 95);
            this.Controls.Add(this.TabsMenu1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
    }
}

