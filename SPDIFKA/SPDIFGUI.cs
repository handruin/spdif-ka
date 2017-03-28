using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using NAudio.Wave;
using SPDIFKA.Lib;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using NLog;

namespace SPDIFKA {
    public partial class SPDIFKAGUI : Form, IMMNotificationClient {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private const string AppName = "SPDIF-KA";
        private const string StoppedMessage = "stopped";
        private const string RunningMessage = "running";
        private static string ToolStripStartText = "Start " + AppName;
        private static string ToolStripStopText = "Stop " + AppName;
        private const string UNPLUGGED_SUFFIX = " (*Unplugged*)";
        private bool IsAppVisible = true;
        private bool IsAnyTargetedDeviceUnplugged;
        private bool IsRegisteredToAudioDeviceNotifications;

        private static readonly string Version = System.Reflection.Assembly.GetExecutingAssembly()
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
            this.spdifka.BalloonTipText = AppName + " - " + StoppedMessage;
            this.spdifka.BalloonTipTitle = AppName;
            this.spdifka.Text = AppName + " - " + StoppedMessage;
            this.spdifka.Icon = Properties.Resources.bar_chart_64_red;

            this.toolStripStart.Text = ToolStripStartText;
            this.spdifka.ContextMenuStrip = this.RightClickMenuStrip;
            this.Resize += this.Form1_Resize;
            this.runningLabel.Text = StoppedMessage;
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
            if (!this.IsAppVisible) {
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
            if (this.ShouldRegisterToAudioDeviceNotifications()) {
                this.RegisterToAudioDeviceNotifications();
            }
            SystemEvents.PowerModeChanged += this.OnPowerChange;
        }

        private bool ShouldRegisterToAudioDeviceNotifications() {
            foreach (var deviceName in UserPrefs.TargetedDeviceNames) {
                if (deviceName != UserPreferences.DEFAULT_AUDIO_DEVICE) {
                    return true;
                }
            }
            return false;
        }

        private MMDeviceEnumerator MMDeviceEnumerator;
        private void RegisterToAudioDeviceNotifications() {
            if (this.IsRegisteredToAudioDeviceNotifications) return;
            this.IsRegisteredToAudioDeviceNotifications = true;
            //DeviceNotification.RegisterDeviceNotification(this.Handle);

            // Credit: http://stackoverflow.com/a/33945287/541420
            this.MMDeviceEnumerator = new MMDeviceEnumerator();
            var endpoints = this.MMDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            foreach (var endpoint in endpoints) {
                Logger.Trace($"{endpoint.State} > {endpoint.DeviceFriendlyName} [{endpoint.ID}]");
            }
            this.MMDeviceEnumerator.RegisterEndpointNotificationCallback(this);
        }

        private void UnregisterFromAudioDeviceNotifications() {
            if (!this.IsRegisteredToAudioDeviceNotifications) return;
            this.IsRegisteredToAudioDeviceNotifications = false;
            //DeviceNotification.UnregisterDeviceNotification();

            this.MMDeviceEnumerator.UnregisterEndpointNotificationCallback(this);
            this.MMDeviceEnumerator.Dispose();
        }

        void IMMNotificationClient.OnDeviceStateChanged(string deviceId, DeviceState newState) {
            Logger.Trace("OnDeviceStateChanged [{0}] : {1}", deviceId, newState);
            this.InvokeOnDeviceChangedAsync();
        }

        void IMMNotificationClient.OnDeviceAdded(string pwstrDeviceId) {
            Logger.Trace("OnDeviceAdded [{0}]", pwstrDeviceId);
            this.InvokeOnDeviceChangedAsync();

        }

        void IMMNotificationClient.OnDeviceRemoved(string deviceId) {
            Logger.Trace("OnDeviceRemoved [{0}]", deviceId);
            this.InvokeOnDeviceChangedAsync();
        }

        void IMMNotificationClient.OnDefaultDeviceChanged(DataFlow flow, Role role, string defaultDeviceId) { }
        void IMMNotificationClient.OnPropertyValueChanged(string pwstrDeviceId, PropertyKey key) { }

        void InvokeOnDeviceChangedAsync() {
            Task.Run(() => {
                this.Invoke((MethodInvoker)(() => {
                    this.OnDeviceChanged();
                }));
            });
        }
        void OnDeviceChanged() {
            this.ReloadDevices();
            this.RestartAudioControlIfRunning();
        }

        private void OnPowerChange(object s, PowerModeChangedEventArgs e) {
            if (e.Mode != PowerModes.Resume) return;
            Task.Run(() => {
                this.Invoke((MethodInvoker)(() => {
                    this.OnDeviceChanged();
                    if (!this.IsAnyTargetedDeviceUnplugged) return;
                    var timer = new Timer { Interval = 5000 };
                    timer.Tick += (s2, e2) => {
                        this.OnDeviceChanged();
                        timer.Dispose();
                    };
                    timer.Start();
                }));
            });
        }

        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);
            switch (m.Msg) {
                case DeviceNotification.WmDevicechange:
                    switch ((int)m.WParam) {
                        case DeviceNotification.DbtDeviceRemoveComplete:
                        case DeviceNotification.DbtDeviceArrival:
                        case DeviceNotification.DbtDevNodesChanged:
                            this.InvokeOnDeviceChangedAsync();
                            break;
                    }
                    break;
                    //case UsbNotification.WmDisplayChange:
                    //    this.InvokeOnDeviceChangedAsync();
                    //    break;
            }
        }

        private void ReloadDevices() {
            this.comboBoxWaveOutDevice.Items.Clear();
            this.comboBoxWaveOutDevice.Items.Add(UserPreferences.DEFAULT_AUDIO_DEVICE);
            this.IsAnyTargetedDeviceUnplugged = false;
            for (var deviceId = -1; deviceId < WaveOut.DeviceCount; deviceId++) {
                var capabilities = WaveOut.GetCapabilities(deviceId);
                this.comboBoxWaveOutDevice.Items.Add(capabilities.ProductName);
            }
            foreach (var deviceName in UserPrefs.TargetedDeviceNames) {
                var plugged = false;
                for (var i = 0; i < this.comboBoxWaveOutDevice.Items.Count; i++) {
                    if (this.comboBoxWaveOutDevice.Items[i].ToString().Replace(UNPLUGGED_SUFFIX, "") == deviceName) {
                        this.comboBoxWaveOutDevice.SetItemChecked(i, true);
                        plugged = true;
                    }
                }
                if (!plugged) {
                    this.comboBoxWaveOutDevice.Items.Add(deviceName + UNPLUGGED_SUFFIX);
                    this.comboBoxWaveOutDevice.SetItemChecked(this.comboBoxWaveOutDevice.Items.Count - 1, true);
                    this.IsAnyTargetedDeviceUnplugged = true;
                }
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
            this.MinimizeToNotificationArea();

        }

        private void MinimizeToNotificationArea() {
            if (UserPrefs.IsMinimizeToNotificationArea) {
                this.IsAppVisible = false;
                this.ShowInTaskbar = false;
                this.Hide();
            }
            else {
                this.IsAppVisible = true;
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }

        /// <summary>
        /// Restore the window to normal user operation mode.
        /// </summary>
        private void Restore() {
            this.IsAppVisible = true;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
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
            this.ExitApplication();
        }

        private void SPDIFKAGUI_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing) {
                if (UserPrefs.IsMinimizedOnClose) {
                    this.Minimize();
                    e.Cancel = true;
                }
                else {
                    this.ExitApplication();
                }
            }
        }

        private void ExitApplication() {
            this.spdifka.Icon = null;  //Ensure tray icon does not persist after close.
            Application.Exit();
        }

        /// <summary>
        /// Tool tip icon menu About popup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripAbout_Click(object sender, EventArgs e) {
            MessageBox.Show("Copyright 2017 handruin.com - Version " + Version, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Generic method to handle the starting and stopping of the SPDIF keep alive audio clip playback.
        /// </summary>
        private void ToggleStartStop() {
            if (!AudioControl.Instance.Value.IsRunning) {
                this.spdifka.Text = AppName + " - " + RunningMessage;
                this.toolStripStart.Text = ToolStripStopText;
                this.runningLabel.Text = RunningMessage;
                this.spdifka.BalloonTipText = AppName + " - " + RunningMessage;
                this.startStopButton.Text = "Stop";
                AudioControl.Instance.Value.Start();
                this.UpdateTrayIconWhenRunning(isRunning: true);
            }
            else {
                this.spdifka.Text = AppName + " - " + StoppedMessage;
                this.startStopButton.Text = "Start";
                this.toolStripStart.Text = ToolStripStartText;
                this.runningLabel.Text = StoppedMessage;
                this.spdifka.BalloonTipText = AppName + " - " + StoppedMessage;
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
            if (UserPrefs.TargetedDeviceNames == null) {
                UserPrefs.TargetedDeviceNames = Properties.Settings.Default.TargetedDeviceNames;
                UserPrefs.Save();
            }
            UserPrefs.TargetedDeviceNames.Clear();
            for (var i = 0; i < this.comboBoxWaveOutDevice.Items.Count; i++) {
                var deviceName = this.comboBoxWaveOutDevice.Items[i].ToString().Replace(UNPLUGGED_SUFFIX, "");
                if (this.comboBoxWaveOutDevice.GetItemChecked(i)) {
                    if (!UserPrefs.TargetedDeviceNames.Contains(deviceName)) {
                        UserPrefs.TargetedDeviceNames.Add(deviceName);
                    }
                }
                else {
                    UserPrefs.TargetedDeviceNames.Remove(deviceName);
                }
            }
            UserPrefs.Save();
            if (this.ShouldRegisterToAudioDeviceNotifications()) {
                this.RegisterToAudioDeviceNotifications();
            }
            else {
                this.UnregisterFromAudioDeviceNotifications();
            }
            this.RestartAudioControlIfRunning();
        }

        private void TabsMenu1_Click(object sender, EventArgs e) {
            this.OnDeviceChanged();
        }
    }
}
