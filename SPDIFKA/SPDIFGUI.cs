using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using NAudio.Wave;
using SPDIFKA.Lib;

namespace SPDIFKA {
    public partial class SPDIFKAGUI : Form {
        private const string name = "SPDIF-KA";
        private const string stoppedMessage = "stopped";
        private const string startMessage = "running";
        private static string toolStripStartText = "Start " + name;
        private static string toolStripStopText = "Stop " + name;
        private const string UNPLUGGED_SUFFIX = " (*Unplugged*)";
        private bool isAppVisible = true;

        private static readonly string version = System.Reflection.Assembly.GetExecutingAssembly()
                                           .GetName()
                                           .Version
                                           .ToString();
        public static readonly UserPreferences UserPrefs = new UserPreferences();
        private readonly bool IsInitializing;

        /// <summary>
        /// General initialization.
        /// </summary>
        public SPDIFKAGUI() {
            this.IsInitializing = true;
            this.InitializeComponent();

            this.ShowIcon = true;
            this.Icon = Properties.Resources.bar_chart_64_red;

            // Create the ToolTip and associate with the Form container.
            var toolTip1 = new ToolTip {
                AutoPopDelay = 10000,
                InitialDelay = 500,
                ReshowDelay = 500,
                ShowAlways = true
            };
            toolTip1.SetToolTip(this.IsStartWithWindowsCheckbox, "If checked, will add a shortcut to the current executable in Windows Start Menu Startup Folder for the current user. Uncheck to delete the shortcut.");
            this.MaximizeBox = false;

            this.spdifka.BalloonTipIcon = ToolTipIcon.Info;
            this.spdifka.BalloonTipText = name + " - " + stoppedMessage;
            this.spdifka.BalloonTipTitle = name;
            this.spdifka.Text = name + " - " + stoppedMessage;
            this.spdifka.Icon = Properties.Resources.bar_chart_64_red;

            this.toolStripStart.Text = toolStripStartText;
            this.spdifka.ContextMenuStrip = this.RightClickMenuStrip;
            this.Resize += this.Form1_Resize;
            this.runningLabel.Text = stoppedMessage;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.LoadState();
            this.IsInitializing = false;
        }

        /// <summary>
        /// Overriding this method to solve an issue related to starting this application in a minimized state.
        /// This now allows the utility to start minimized and hides this utility from the alt + tab menu.
        /// </summary>
        /// <param name="isVisible"></param>
        protected override void SetVisibleCore(bool isVisible) {
            if (!this.isAppVisible) {
                isVisible = false;
                if (!this.IsHandleCreated) this.CreateHandle();
            }
            base.SetVisibleCore(isVisible);
        }

        /// <summary>
        /// Load the settings and state of the application that were previously saved.
        /// </summary>
        private void LoadState() {
            //Update the visual check boxes with saved state.
            this.IsMinimizedCheckBox.Checked = UserPrefs.IsHidden;
            this.IsRunningCheckBox.Checked = UserPrefs.IsRunning;
            this.IsMinimizeToNotificationCheckbox.Checked = UserPrefs.IsMinimizeToNotificationArea;
            this.IsMinimizeOnCloseCheckbox.Checked = UserPrefs.IsMinimizedOnClose;
            this.silent_sound.Checked = UserPrefs.SoundType == UserPreferences.Sound.Silent;
            this.inaudible_sound.Checked = UserPrefs.SoundType == UserPreferences.Sound.Inaudible;
            this.IsStartWithWindowsCheckbox.Checked = UserPrefs.IsStartWithWindows;
            if (UserPrefs.IsHidden) {
                this.Minimize();
            }
            if (UserPrefs.IsRunning) {
                this.ToggleStartStop();
            }
            this.ReloadDevices();
            this.RegisterOrUnregisterUsbDeviceNotificationBasedOnEnabledDevices();
            SystemEvents.PowerModeChanged += this.OnPowerChange;
        }

        private void OnPowerChange(object s, PowerModeChangedEventArgs e) {
            switch (e.Mode) {
                case PowerModes.Resume:
                    this.ReloadDevices();
                    this.RestartAudioControlIfRunning();
                    break;
                case PowerModes.Suspend:
                    break;
            }
        }

        private bool IsRegisteredToUsbDeviceNotification = false;
        private void RegisterOrUnregisterUsbDeviceNotificationBasedOnEnabledDevices() {
            foreach (var deviceName in UserPrefs.EnabledDeviceNames) {
                if (deviceName != UserPreferences.DEFAULT_AUDIO_DEVICE) {
                    if (!this.IsRegisteredToUsbDeviceNotification) {
                        this.IsRegisteredToUsbDeviceNotification = true;
                        UsbNotification.RegisterUsbDeviceNotification(this.Handle);
                    }
                    return;
                }
            }
            if (this.IsRegisteredToUsbDeviceNotification) {
                this.IsRegisteredToUsbDeviceNotification = false;
                UsbNotification.UnregisterUsbDeviceNotification();
            }
        }

        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);
            if (m.Msg == UsbNotification.WmDevicechange) {
                switch ((int)m.WParam) {
                    case UsbNotification.DbtDeviceremovecomplete:
                        this.ReloadDevices();
                        this.RestartAudioControlIfRunning();
                        break;
                    case UsbNotification.DbtDevicearrival:
                        this.ReloadDevices();
                        this.RestartAudioControlIfRunning();
                        break;
                }
            }
        }

        private void ReloadDevices() {
            if (WaveOut.DeviceCount <= 0) return;
            this.comboBoxWaveOutDevice.Items.Clear();
            this.comboBoxWaveOutDevice.Items.Add(UserPreferences.DEFAULT_AUDIO_DEVICE);
            for (var deviceId = -1; deviceId < WaveOut.DeviceCount; deviceId++) {
                var capabilities = WaveOut.GetCapabilities(deviceId);
                this.comboBoxWaveOutDevice.Items.Add(capabilities.ProductName);
            }
            foreach (var deviceName in UserPrefs.EnabledDeviceNames) {
                var found = false;
                for (int i = 0; i < this.comboBoxWaveOutDevice.Items.Count; i++) {
                    if (this.comboBoxWaveOutDevice.Items[i].ToString().Replace(UNPLUGGED_SUFFIX, "") == deviceName) {
                        this.comboBoxWaveOutDevice.SetItemChecked(i, true);
                        found = true;
                    }
                }
                if (!found) this.comboBoxWaveOutDevice.Items.Add(deviceName + UNPLUGGED_SUFFIX);
            }
        }

        /// <summary>
        /// General destructor
        /// </summary>
        ~SPDIFKAGUI() {
            this.Resize -= this.Form1_Resize;
        }

        /// <summary>
        /// Start and Stop button for changing the audio state of this tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e) {
            this.ToggleStartStop();
        }

        /// <summary>
        /// Resize event to manage the windows tool bar functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) {
                this.Minimize();
            }
        }

        /// <summary>
        /// Minimize the application into the task bar.
        /// </summary>
        private void Minimize() {

            this.spdifka.Visible = true;
            this.minimized_to_notificaton_area();

        }

        private void minimized_to_notificaton_area() {
            if (UserPrefs.IsMinimizeToNotificationArea) {
                this.isAppVisible = false;
                this.ShowInTaskbar = false;
                this.Hide();
            }
            else {
                this.isAppVisible = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = true;
            }
        }

        /// <summary>
        /// Restore the window to normal user operation mode.
        /// </summary>
        private void Restore() {
            this.isAppVisible = true;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
            this.Activate();
        }

        /// <summary>
        /// Single mouse click event to manage the restoring of the tool window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button.Equals(MouseButtons.Left)) {
                this.Restore();
            }
        }

        /// <summary>
        /// Tool tip icon menu start and stop management.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStart_Click(object sender, EventArgs e) {
            this.ToggleStartStop();
        }

        /// <summary>
        /// Tool tip icon menu program exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripExit_Click(object sender, EventArgs e) {
            AudioControl.Instance.Value.Stop();  //Ensure audio stops before exiting.
            this.Exit_application();
        }

        private void SPDIFKAGUI_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing) {
                if (UserPrefs.IsMinimizedOnClose) {
                    this.Minimize();
                    e.Cancel = true;
                }
                else {
                    this.Exit_application();
                }
            }
        }

        private void Exit_application() {
            this.spdifka.Icon = null;  //Ensure tray icon does not persist after close.
            Application.Exit();
        }

        /// <summary>
        /// Tool tip icon menu About popup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripAbout_Click(object sender, EventArgs e) {
            MessageBox.Show("Copyright 2017 handruin.com - Version " + version, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Generic method to handle the starting and stopping of the SPDIF keep alive audio clip playback.
        /// </summary>
        private void ToggleStartStop() {
            if (!AudioControl.Instance.Value.IsRunning) {
                this.spdifka.Text = name + " - " + startMessage;
                this.toolStripStart.Text = toolStripStopText;
                this.runningLabel.Text = startMessage;
                this.spdifka.BalloonTipText = name + " - " + startMessage;
                this.startStopButton.Text = "Stop";
                AudioControl.Instance.Value.Start();
                this.UpdateTrayIconWhenRunning(isRunning: true);
            }
            else {
                this.spdifka.Text = name + " - " + stoppedMessage;
                this.startStopButton.Text = "Start";
                this.toolStripStart.Text = toolStripStartText;
                this.runningLabel.Text = stoppedMessage;
                this.spdifka.BalloonTipText = name + " - " + stoppedMessage;
                AudioControl.Instance.Value.Stop();
                this.UpdateTrayIconWhenRunning(isRunning: false);
            }
        }      
        
        /// <summary>
        /// Restart the audio control only if the audio was already running.
        /// </summary>
        private void RestartAudioControlIfRunning() {
            if (AudioControl.Instance.Value.IsRunning) {
                AudioControl.Instance.Value.Stop();
                AudioControl.Instance.Value.Start();
            }
        }

        /// <summary>
        /// Update the visual icon in the tray to represent the application state.
        /// </summary>
        /// <param name="isRunning"></param>
        private void UpdateTrayIconWhenRunning(bool isRunning) {
            if (isRunning) {
                this.spdifka.Icon = Properties.Resources.bar_chart_64_green;
                this.Icon = Properties.Resources.bar_chart_64_green;
            }
            else {
                this.spdifka.Icon = Properties.Resources.bar_chart_64_red;
                this.Icon = Properties.Resources.bar_chart_64_red;
            }
        }

        /// <summary>
        /// Handle event when IsMinimized CheckBox is selected/deselected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsMinimizedCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (this.IsInitializing) return;
            UserPrefs.IsHidden = this.IsMinimizedCheckBox.Checked;
            UserPrefs.Save();
        }

        /// <summary>
        /// Handle event when IsRunning CheckBox is selected/deselected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsRunningCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (this.IsInitializing) return;
            UserPrefs.IsRunning = this.IsRunningCheckBox.Checked;
            UserPrefs.Save();
        }

        private void MinimizeToNotification_CheckedChanged(object sender, EventArgs e) {
            if (this.IsInitializing) return;
            UserPrefs.IsMinimizeToNotificationArea = this.IsMinimizeToNotificationCheckbox.Checked;
            UserPrefs.Save();
        }

        private void MinimizeToNotificationOnClose_CheckedChanged(object sender, EventArgs e) {
            if (this.IsInitializing) return;
            UserPrefs.IsMinimizedOnClose = this.IsMinimizeOnCloseCheckbox.Checked;
            UserPrefs.Save();
        }

        private void IsStartWithWindowsCheckbox_CheckedChanged(object sender, EventArgs e) {
            if (this.IsInitializing) return;
            UserPrefs.IsStartWithWindows = this.IsStartWithWindowsCheckbox.Checked;
            var targetPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            if (UserPrefs.IsStartWithWindows) {
                ShortcutToolbox.CreateShortcutInStartUpFolder(targetPath: targetPath, startInFolderPath: Path.GetDirectoryName(Application.StartupPath), description: "");
            }
            else {
                ShortcutToolbox.DeleteShortcutInStartUpFolder(targetPath: targetPath);
            }
            UserPrefs.Save();
        }

        /// <summary>
        /// Handle event when inaudible sound radio button is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inaudible_sound_CheckedChanged(object sender, EventArgs e) {
            if (this.IsInitializing) return;
            if (this.inaudible_sound.Checked) {
                UserPrefs.SoundType = UserPreferences.Sound.Inaudible;
                UserPrefs.Save();
                this.RestartAudioControlIfRunning();
            }
        }

        /// <summary>
        /// Handle event when silent sound radio button is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void silent_sound_CheckedChanged(object sender, EventArgs e) {
            if (this.IsInitializing) return;
            if (this.silent_sound.Checked) {
                UserPrefs.SoundType = UserPreferences.Sound.Silent;
                UserPrefs.Save();
                this.RestartAudioControlIfRunning();
            }
        }

        private void comboBoxWaveOutDevice_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.IsInitializing) return;
            if (UserPrefs.EnabledDeviceNames == null) {
                UserPrefs.EnabledDeviceNames = Properties.Settings.Default.EnabledDeviceNames;
                UserPrefs.Save();
            }
            UserPrefs.EnabledDeviceNames.Clear();
            for (int i = 0; i < this.comboBoxWaveOutDevice.Items.Count; i++) {
                var deviceName = this.comboBoxWaveOutDevice.Items[i].ToString().Replace(UNPLUGGED_SUFFIX, "");
                if (this.comboBoxWaveOutDevice.GetItemChecked(i)) {
                    if (!UserPrefs.EnabledDeviceNames.Contains(deviceName)) {
                        UserPrefs.EnabledDeviceNames.Add(deviceName);
                    }
                }
                else {
                    UserPrefs.EnabledDeviceNames.Remove(deviceName);
                }
            }
            UserPrefs.Save();
            this.RegisterOrUnregisterUsbDeviceNotificationBasedOnEnabledDevices();
            this.RestartAudioControlIfRunning();
        }
    }
}
