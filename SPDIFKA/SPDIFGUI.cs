using System;
using System.Threading;
using System.Windows.Forms;

namespace SPDIFKA
{
    public partial class SPDIFKAGUI : Form
    {
        private static String name = "SPDIF-KA";
        private static String stoppedMessage = "stopped";
        private static String startMessage = "running";
        private static String toolStripStartText = "Start " + name;
        private static String toolStripStopText = "Stop " + name;
        private bool isAppVisible = true;

        private static String version = System.Reflection.Assembly.GetExecutingAssembly()
                                           .GetName()
                                           .Version
                                           .ToString();
        private static UserPreferences UserPrefs = new UserPreferences();

        /// <summary>
        /// General initialization.
        /// </summary>
        public SPDIFKAGUI()
        {
            InitializeComponent();
            this.ShowIcon = true;
            this.Icon = Properties.Resources.bar_chart_64_red;
            this.MaximizeBox = false;

            this.NotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotifyIcon.BalloonTipText = name + " - " + stoppedMessage;
            this.NotifyIcon.BalloonTipTitle = name;
            this.NotifyIcon.Text = name + " - " + stoppedMessage;
            this.NotifyIcon.Icon = Properties.Resources.bar_chart_64_red;

            toolStripStart.Text = toolStripStartText;
            this.NotifyIcon.ContextMenuStrip = RightClickMenuStrip;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            runningLabel.Text = stoppedMessage;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.loadState();
        }

        /// <summary>
        /// Overriding this method to solve an issue related to starting this application in a minimized state.
        /// This now allows the utility to start minimized and hides this utility from the alt + tab menu.
        /// </summary>
        /// <param name="isVisible"></param>
        protected override void SetVisibleCore(bool isVisible)
        {
            if (!isAppVisible)
            {
                isVisible = false;
                if (!this.IsHandleCreated) CreateHandle();
            }
            base.SetVisibleCore(isVisible);
        }

        /// <summary>
        /// Load the settings and state of the application that were previously saved.
        /// </summary>
        private void loadState()
        {
            //Update the visual check boxes with saved state.
            IsMinimizedCheckBox.Checked = UserPrefs.IsHidden;
            IsRunningCheckBox.Checked = UserPrefs.IsRunning;
            IsMinimizeToNotificationCheckbox.Checked = UserPrefs.IsMinimizeToNotificationArea;
            IsMinimizeOnCloseCheckbox.Checked = UserPrefs.IsMinimizedOnClose;
            if (UserPrefs.SoundType == UserPreferences.Sound.Silent)
            {
                silent_sound.Checked = true;
                inaudible_sound.Checked = false;
            }

            if (UserPrefs.SoundType == UserPreferences.Sound.Inaudible)
            {
                silent_sound.Checked = false;
                inaudible_sound.Checked = true;
            }

            if (UserPrefs.IsHidden)
            {
                this.minimize();
            }
            
            if (UserPrefs.IsRunning)
            {
                manageStartStop();
            }
        }

        /// <summary>
        /// General destructor
        /// </summary>
        ~SPDIFKAGUI()
        {
            this.Resize -= new System.EventHandler(this.Form1_Resize);
        }

        /// <summary>
        /// Start and Stop button for changing the audio state of this tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e)
        {
            manageStartStop();
        }

        /// <summary>
        /// Resize event to manage the windows tool bar functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.minimize();
            }
        }

        /// <summary>
        /// Minimize the application into the task bar.
        /// </summary>
        private void minimize()
        {
            
            NotifyIcon.Visible = true;
            this.minimized_to_notificaton_area();
            
        }

        private void minimized_to_notificaton_area()
        {
            if (UserPrefs.IsMinimizeToNotificationArea)
            {
                this.isAppVisible = false;
                this.ShowInTaskbar = false;
                this.Hide();
            }
            else
            {
                this.isAppVisible = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = true;
            }
        }

        /// <summary>
        /// Restore the window to normal user operation mode.
        /// </summary>
        private void restore()
        {
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
        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                DoubleClickTimer.Interval = (int)(SystemInformation.DoubleClickTime);
                DoubleClickTimer.Start(); // wait to ensure the user has not started a double click
            }
        }

        private void DoubleClickTimer_Tick(object sender, EventArgs e)
        {
            DoubleClickTimer.Stop();
            this.restore();
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                DoubleClickTimer.Stop();
                this.restore();
            }
        }

        /// <summary>
        /// Tool tip icon menu start and stop management.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStart_Click(object sender, EventArgs e)
        {
            manageStartStop();
        }

        /// <summary>
        /// Tool tip icon menu program exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripExit_Click(object sender, EventArgs e)
        {
            this.exit_application();
        }

        private void SPDIFKAGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (UserPrefs.IsMinimizedOnClose)
                {
                    this.minimize();
                    e.Cancel = true;
                }
                else
                {
                    this.exit_application();
                }
            }
        }

        private void exit_application()
        {
            AudioControl.Instance.stop();  //Ensure audio stops before exiting.
            NotifyIcon.Icon = null;  //Ensure tray icon does not persist after close.
            Application.Exit();
        }

        /// <summary>
        /// Tool tip icon menu About popup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copyright 2017 handruin.com - Version " + version, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Generic method to handle the starting and stopping of the SPDIF keep alive audio clip playback.
        /// </summary>
        private void manageStartStop()
        {
            if (!AudioControl.Instance.isRunning())
            {
                this.NotifyIcon.Text = name + " - " + startMessage;
                toolStripStart.Text = toolStripStopText;
                runningLabel.Text = startMessage;
                this.NotifyIcon.BalloonTipText = name + " - " + startMessage;
                startStopButton.Text = "Stop";
                AudioControl.Instance.start();
                this.updateTrayIconWhenRunning(true);
            }
            else
            {
                this.NotifyIcon.Text = name + " - " + stoppedMessage;
                startStopButton.Text = "Start";
                toolStripStart.Text = toolStripStartText;
                runningLabel.Text = stoppedMessage;
                this.NotifyIcon.BalloonTipText = name + " - " + stoppedMessage;
                AudioControl.Instance.stop();
                this.updateTrayIconWhenRunning(false);
            }
        }

        /// <summary>
        /// Restart the audio control only if the audio was already running.
        /// </summary>
        private void restartAudioControl()
        {
            if (AudioControl.Instance.isRunning())
            {
                AudioControl.Instance.stop();
                AudioControl.Instance.start();
            }
        }

        /// <summary>
        /// Update the visual icon in the tray to represent the application state.
        /// </summary>
        /// <param name="isRunning"></param>
        private void updateTrayIconWhenRunning(Boolean isRunning)
        {
            if (isRunning)
            {
                NotifyIcon.Icon = Properties.Resources.bar_chart_64_green;
                this.Icon = Properties.Resources.bar_chart_64_green;
            }
            else
            {
                NotifyIcon.Icon = Properties.Resources.bar_chart_64_red;
                this.Icon = Properties.Resources.bar_chart_64_red;
            }
        }

        /// <summary>
        /// Handle event when IsMinimized CheckBox is selected/deselected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsMinimizedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UserPrefs.IsHidden = IsMinimizedCheckBox.Checked;
            UserPrefs.Save();
        }

        /// <summary>
        /// Handle event when IsRunning CheckBox is selected/deselected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsRunningCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UserPrefs.IsRunning = IsRunningCheckBox.Checked;
            UserPrefs.Save();
        }

        private void minimizeToNotification_CheckedChanged(object sender, EventArgs e)
        {
            UserPrefs.IsMinimizeToNotificationArea = IsMinimizeToNotificationCheckbox.Checked;
            UserPrefs.Save();
        }

        private void MinimizToNotificationOnClose_CheckedChanged(object sender, EventArgs e)
        {
            UserPrefs.IsMinimizedOnClose = IsMinimizeOnCloseCheckbox.Checked;
            UserPrefs.Save();
        }

        /// <summary>
        /// Handle event when inaudible sound radio button is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inaudible_sound_CheckedChanged(object sender, EventArgs e)
        {
            if (inaudible_sound.Checked)
            {
                UserPrefs.SoundType = UserPreferences.Sound.Inaudible;
                UserPrefs.Save();
                restartAudioControl();
            }
        }

        /// <summary>
        /// Handle event when silent sound radio button is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void silent_sound_CheckedChanged(object sender, EventArgs e)
        {
            if (silent_sound.Checked)
            {
                UserPrefs.SoundType = UserPreferences.Sound.Silent;
                UserPrefs.Save();
                restartAudioControl();
            }
        }
    }
}
